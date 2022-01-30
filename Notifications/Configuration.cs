namespace StashCat.Notifications;

public class Configuration
{
    public string SocketIoUri { get; set; } = "https://push.stashcat.com";
    public string SocketIoPath { get; set; } = "/socket.io";
    public int SocketIoEio { get; set; } = 3; // Engine.IO version, visible in web client as "3" (might change with future version)
    public string UniqueDeviceId { get; set; }
    public string ClientKey { get; set; }
    public string SocketId { get; set; }

    public Configuration(string deviceId, string clientKey, string socketId)
    {
        UniqueDeviceId = deviceId;
        ClientKey = clientKey;
        SocketId = socketId;
    }
}