using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersAPI.Domain.Interfaces.Messages;
using UsersAPI.Domain.ValueObjects;
using UsersAPI.Infra.Messages.Settings;

namespace UsersAPI.Infra.Messages.Producers
{
    public class UserMessageProducer : IUserMessageProducer
    {
        private readonly RabbitMQSettings? _rabbitMQSettings;

        public UserMessageProducer(IOptions<RabbitMQSettings?> rabbitMQSettings)
        {
            _rabbitMQSettings = rabbitMQSettings.Value;
        }

        public void Send(UserMessageVO userMessage)
        {
            //Conectando no servidor do RabbitMQ
            var connectionFactory = new ConnectionFactory() { Uri = new Uri(_rabbitMQSettings?.Url) };
            using (var connection = connectionFactory.CreateConnection())
            {
                using (var model = connection.CreateModel())
                {
                    //criando a fila
                    model.QueueDeclare(
                        queue: _rabbitMQSettings.Queue, //nome da fila
                        durable: true, //não apagar as filas ao desligar ou reiniciar o broker
                        autoDelete: false, //apagar ou não a fila quando ela estiver sem mensagens (vazia)
                        exclusive: false, //fila exclusiva para uma unica aplicaçao ou não
                        arguments: null
                        );

                    //gravando mensagem na fila
                    model.BasicPublish(
                        exchange: string.Empty,
                        routingKey: _rabbitMQSettings.Queue,
                        basicProperties: null,
                        body: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(userMessage))
                        );
                }
            }
        }
    }
}
