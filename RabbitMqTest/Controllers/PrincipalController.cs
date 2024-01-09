using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMqTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrincipalController : ControllerBase
    {
        [HttpGet("publicar")]
        public IActionResult PublicarMensagem(string mensagem)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "saudacao_1",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);
                string message = mensagem;
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "", routingKey: "saudacao_1", basicProperties: null, body: body);
            }
            return Ok("Mensagem");
        }
    }
}
