namespace RinhaDeBackEnd2023.Repository.Interfaces
{
    public interface IRedisCacheRepository    
    {
        Task SetAsync<T>(string key, T value, TimeSpan expiration);
        Task<T> GetAsync<T>(string key);
    }
}
