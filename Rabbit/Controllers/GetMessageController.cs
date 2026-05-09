using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rabbit.Services.Implementations;
using Rabbit.Services.Interfaces;

namespace Rabbit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetMessageController : ControllerBase
    {
        private readonly IRabbitMQService _rabbitMQService;
        public GetMessageController(IRabbitMQService rabbitMQService)
        {
            _rabbitMQService = rabbitMQService;
        }
        [HttpGet]
        public async Task<string> GetMessage()
        {
            return await _rabbitMQService.GetMessage();
        }

    }
}
