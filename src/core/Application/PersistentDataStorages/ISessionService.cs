

namespace Application.PersistentDataStorages
{
    public interface ISessionService
    {
        Task<string> GetAsync(string key);
        Task SetAsync(string key, string value);
    }
}
