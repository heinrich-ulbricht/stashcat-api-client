namespace StashCat.Notifications;

using System.Diagnostics;
using System.Net.WebSockets;
using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Nito.AsyncEx;
using SocketIOClient;
using SocketIOClient.Transport;
using StashCat.Notifications.Model;

public class StashCatNotificationClient
{
    private readonly ILogger _logger;
    private readonly Configuration _configuration;
    private readonly SocketIO _pushClient;
    private AsyncManualResetEvent _connected = new AsyncManualResetEvent(false);

    public event EventHandler<List<MessagePayload>>? OnMessageChanged;
    public event EventHandler<(string, string)>? OnAny;

    public StashCatNotificationClient(ILogger logger, Configuration configuration)
    {
        _logger = logger;
        _configuration = configuration;
        _pushClient = CreatePushClient();
    }

    private SocketIO CreatePushClient()
    {
        var pushClient = new SocketIO(_configuration.SocketIoUri, new SocketIOOptions()
        {
            Path = _configuration.SocketIoPath,
            EIO = _configuration.SocketIoEio
        });

        pushClient.ClientWebSocketProvider = () =>
        {
            var clientWebSocket = new DefaultClientWebSocket
            {
                ConfigOptions = o =>
                {
                    var options = o as ClientWebSocketOptions;
                    if (options == null)
                    {
                        return;
                    }
                    options.SetRequestHeader("User-Agent", "Mozilla/5.0 (X11; Fedora; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.110 Safari/537.36");
                    options.SetRequestHeader("Origin", "https://app.schul.cloud");
                    options.SetRequestHeader("Pragma", "no-cache");
                    options.SetRequestHeader("Cache-Control", "no-cache");
                }
            };
            return clientWebSocket;
        };


        pushClient.OnConnected += (sender, e) =>
        {
            _logger.LogDebug("Connected!");
            _connected.Set();
        };

        pushClient.On(StashCatServerEventType.message_changed, response =>
        {
            var payload = response.GetValue<List<MessagePayload>>();
            _logger.LogDebug($"{DateTime.Now} Message changed: " + JsonConvert.SerializeObject(payload));
            if (null != OnMessageChanged)
            {
                OnMessageChanged(pushClient, payload);
            }
        });

        pushClient.On(StashCatServerEventType.new_device_connected, response =>
        {
            var payload = response.GetValue<NewDeviceConnectedPayload>();
            _logger.LogDebug($"{DateTime.Now} New device connected: " + JsonConvert.SerializeObject(payload));
        });

        pushClient.On(StashCatServerEventType.online_status_change, response =>
        {
            var payload = response.GetValue<OnlineStatusChangePayload>();
            _logger.LogDebug("Online status change: " + JsonConvert.SerializeObject(payload));
        });

        pushClient.OnAny((eventName, response) =>
        {
            var text = $"{DateTime.Now} Got event '{eventName}' with payload: " + response.ToString();
            _logger.LogDebug(text);
            if (null != OnAny)
            {
                OnAny(pushClient, (eventName, response.ToString()));
            }
        });

        pushClient.OnDisconnected += (sender, e) =>
        {
            _logger.LogInformation("OnDisconnected");
            _connected.Reset();
        };
        pushClient.OnReconnected += (sender, e) =>
        {
            _logger.LogInformation("OnReconnected");
            _connected.Set();
        };
        pushClient.OnReconnectError += (sender, e) =>
        {
            _logger.LogError(e, "OnReconnectError");
            _connected.Reset();
        };
        pushClient.OnReconnectFailed += (sender, e) =>
        {
            _logger.LogWarning("OnReconnectFailed");
            _connected.Reset();
        };
        pushClient.OnPing += (sender, e) =>
        {
            _logger.LogDebug($"{DateTime.Now} Ping");
        };

        pushClient.OnError += (sender, e) =>
        {
            _logger.LogError("OnError: " + e);
            if (pushClient.Connected)
            {
                _connected.Set();
            }
            else
            {
                _connected.Reset();
            }
        };
        return pushClient;
    }

    public async Task<bool> GetAuthenticatedStatusAsync()
    {
        var tcs = new TaskCompletionSource<bool>();
        await _pushClient.EmitAsync("heartbeat", response =>
        {
            var isAuth = response.GetValue<bool>();
            tcs.SetResult(isAuth);
        });
        return await tcs.Task;
    }

    public async Task<bool> ConnectAndAuthenticateAsync(CancellationToken token = default)
    {
        _logger.LogDebug("Entering {Method}...", nameof(ConnectAndAuthenticateAsync));
        _logger.LogDebug("Initiating connection to the notification backend");
        await _pushClient.ConnectAsync();
        _logger.LogDebug("Waiting for connection...");
        await _connected.WaitAsync(token);
        _logger.LogDebug("Got 'connected' event, we are connected");
        var userIdPayload = new UserIdPayload()
        {
            client_key = _configuration.ClientKey,
            device_id = _configuration.UniqueDeviceId,
            hidden_id = _configuration.SocketId
        };
        _logger.LogDebug("Sending authentification data to notification backend");
        await _pushClient.EmitAsync(StashCatClientMessageType.userid, userIdPayload);

        _logger.LogDebug("Waiting for authenticated status...");
        var count = 0;
        var isAuthenticated = false;
        while (!(isAuthenticated = await GetAuthenticatedStatusAsync()))
        {
            await Task.Delay(1000);
            _logger.LogDebug("Waiting for authenticated status...");
            if (++count > 10)
                break;
        }
        _logger.LogDebug("DONE: Waiting for authenticated status. Result is: {Result}", isAuthenticated);
        _logger.LogDebug("Leaving {Method}", nameof(ConnectAndAuthenticateAsync));
        return isAuthenticated;
    }
}