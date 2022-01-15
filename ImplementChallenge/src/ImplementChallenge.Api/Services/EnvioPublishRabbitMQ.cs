using ImplementChallenge.Api.Interfaces;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using ImplementChallenge.Api.ViewModels;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using ImplementChallenge.Api.Extensions;

namespace ImplementChallenge.Api.Services
{
    public class EnvioPublishRabbitMQ : IEnvioPublishRabbitMQ
    {
        private readonly ConfiguracaoFilaRabbit _configuration;

        public EnvioPublishRabbitMQ(IOptions<ConfiguracaoFilaRabbit> configuration)
        {
            _configuration = configuration.Value;
        }

        public void EnviaPublishRabbit<Entity>(Entity ViewModel)
        {
            var connectionDocker = new ConnectionFactory()
            {
                Uri = new Uri(_configuration.UrlAcessoRabbit),
                NetworkRecoveryInterval = TimeSpan.FromSeconds(10),
                AutomaticRecoveryEnabled = true
            };

            var factory = connectionDocker;
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "insertUsuario",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = JsonSerializer.Serialize(ViewModel);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                routingKey: "insertUsuario",
                basicProperties: null,
                body: body);
            }
        }

    }
}
