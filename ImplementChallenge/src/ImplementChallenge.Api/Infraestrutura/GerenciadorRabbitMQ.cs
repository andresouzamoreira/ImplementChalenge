using ImplementChallenge.Api.Extensions;
using ImplementChallenge.Api.Interfaces;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ImplementChallenge.Api.Infraestrutura
{
    public class GerenciadorRabbitMQ : IFila
    {
        private readonly ConfiguracaoFilaRabbit _configuration;
        private readonly ConnectionFactory _connectionFactory;


        public GerenciadorRabbitMQ(IOptions<ConfiguracaoFilaRabbit> configuration)
        {
            _configuration = configuration.Value;
            _connectionFactory = new ConnectionFactory()
            {
                Uri = new Uri(_configuration.UrlAcessoRabbit),
                NetworkRecoveryInterval = TimeSpan.FromSeconds(10),
                AutomaticRecoveryEnabled = true,
            };
        }
        public void Adicionar(object obj)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "insertUsuario",
                                       durable: false,
                                       exclusive: false,
                                       autoDelete: false,
                                       arguments: null);

                string message = JsonSerializer.Serialize(obj);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                routingKey: "insertUsuario",
                basicProperties: null,
                body: body);
            }
        }

    }
}


