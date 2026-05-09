using Rabbit.CustomDI.ServiceInterfaces;

namespace Rabbit.Services.Interfaces
{
    public interface IRedisCacheService : ITransientServiceInterface
    {
        T? GetData<T>(string key);
        void SetData<T>(string key, T data);
    }
}
