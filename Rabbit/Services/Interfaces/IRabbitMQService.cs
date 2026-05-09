using Rabbit.CustomDI.ServiceInterfaces;

namespace Rabbit.Services.Interfaces
{
    public interface IRabbitMQService : IScopedServiceInterface
    {
        void GetMessage(object obj);
        Task<string> GetMessage();
    }
}
