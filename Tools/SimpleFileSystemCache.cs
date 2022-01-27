namespace StashCat.Tools;

using Microsoft.Extensions.Caching.Distributed;

public class SimpleFileCache : IDistributedCache
{
    string _path;
    public SimpleFileCache(string path)
    {
        _path = path;
    }

    private string GetCacheFilePathFromKey(string key)
    {
        return Path.Combine(_path, $"{key}.txt");
    }

    public byte[] Get(string key)
    {
        try
        {
            return File.ReadAllBytes(GetCacheFilePathFromKey(key));
        }
        catch
        {
#pragma warning disable CS8603 // Possible null reference return.
            return null;
#pragma warning restore CS8603 // Possible null reference return.
        }
    }

    public Task<byte[]> GetAsync(string key, CancellationToken token = default)
    {
        try
        {
            return File.ReadAllBytesAsync(GetCacheFilePathFromKey(key), token);
        }
        catch
        {
#pragma warning disable CS8603 // Possible null reference return.
            return null;
#pragma warning restore CS8603 // Possible null reference return.
        }
    }

    public void Refresh(string key)
    {

    }

    public Task RefreshAsync(string key, CancellationToken token = default)
    {
        return Task.CompletedTask;
    }

    public void Remove(string key)
    {
        if (File.Exists(GetCacheFilePathFromKey(key)))
            File.Delete(GetCacheFilePathFromKey(key));
    }

    public Task RemoveAsync(string key, CancellationToken token = default)
    {
        if (File.Exists(GetCacheFilePathFromKey(key)))
            File.Delete(GetCacheFilePathFromKey(key));
        return Task.CompletedTask;
    }

    public void Set(string key, byte[] value, DistributedCacheEntryOptions options)
    {
        File.WriteAllBytes(GetCacheFilePathFromKey(key), value);
    }

    public Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options, CancellationToken token = default)
    {
        return File.WriteAllBytesAsync(GetCacheFilePathFromKey(key), value, token);
    }
}