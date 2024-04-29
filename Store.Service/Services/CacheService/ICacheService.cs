﻿namespace Store.Service.Services.CacheService
{
    public interface ICacheService
    {
        Task SetCacheResponseAsync(string key, object response, TimeSpan timeToLive);
         Task<string> GetCacheResponseAsync(string key);
    }
}
