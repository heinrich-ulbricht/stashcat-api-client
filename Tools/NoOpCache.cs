namespace StashCat.Tools;

using Microsoft.Extensions.Caching.Distributed;

public class NoOpCache : IDistributedCache
{
    byte[] IDistributedCache.Get(string key)
    {
        return new byte[0];
    }

    Task<byte[]> IDistributedCache.GetAsync(string key, CancellationToken token)
    {
        return Task.FromResult(new byte[0]);
    }

    void IDistributedCache.Refresh(string key)
    {
    }

    Task IDistributedCache.RefreshAsync(string key, CancellationToken token)
    {
        return Task.CompletedTask;
    }

    void IDistributedCache.Remove(string key)
    {
    }

    Task IDistributedCache.RemoveAsync(string key, CancellationToken token)
    {
        return Task.CompletedTask;
    }

    void IDistributedCache.Set(string key, byte[] value, DistributedCacheEntryOptions options)
    {
    }

    Task IDistributedCache.SetAsync(string key, byte[] value, DistributedCacheEntryOptions options, CancellationToken token)
    {
        return Task.CompletedTask;
    }
}