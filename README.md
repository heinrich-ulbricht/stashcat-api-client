# Unofficial StashCat API Client

Based on the Python implementation <https://gitlab.com/aeberhardt/stashcat-api-client>

## Motivation

[StashCat](https://stashcat.com/en/) is the backend powering the schul.cloud app that is used by schools in Germany to communicate with parents. Unfortunately the Android schul.cloud app depends on Google push notifications which don't work on de-Googled phones (e.g. running [LineageOS](https://lineageos.org/)). But without notifications you would constantly have to open the app to see if something is new.

I used this API client to build my own notification system via Signal using <https://github.com/bbernhard/signal-cli-rest-api>. It's thus reduced to getting basic information about channels and conversations.

And if somebody from StashCat sees this: please support notifications on de-Googled phones. Signal, Threema, Teams - they all support it.

## How to use

Using it goes along those lines:

```csharp
var scClient = new StashCatApiClient(_logger, configuration);
// scClient.Cache = ... something IDistributableCache
await scClient.LoginAsync(username, password);
await scClient.GetPrivateKeyAsync();
await scClient.GetConversationsAsync();
// scClient.Conversations now contains conversation info
await scClient.GetChannelsAsync();
// scClient.Channels now contains channel info
```

It's been tested on a .NET 6 (isolated) Azure Function. Provide a cache instance to prevent an in-app notification every time you call `LoginAsync`. Any `IDistributableCache` implementation will do, e.g. <https://www.nuget.org/packages/DistributedCache.AzureTableStorage>. The login reponse will be stored there including the client key that is needed to call endpoints. Protect this cache from third parties as it contains secrets.

## Disclaimer

"StashCat" belongs to [stashcat GmbH](https://stashcat.com/en/legal-notice/), I am in no way affiliated to them and they have nothing to do with this repo.
