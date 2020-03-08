using System;
using System.Text;
using Newtonsoft.Json;
using Psychology_Recipient.Domain;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Psychology_Recipient.Rabbit
{
    public class Rabbit
    {
        public void Received()
        {
            var factory = new ConnectionFactory();
            factory.Uri = new System.Uri(Settings.Uri);
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: Settings.KeyRequest,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    var result = JsonConvert.DeserializeObject<InterdepartRequest>(message);
                    Verification(result);
                };
                channel.BasicConsume(queue: Settings.KeyRequest,
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine("Нажмите любую клавишу для вызода из программы");
                Console.ReadLine();
            }
        }
        private void Verification(InterdepartRequest interdepart)
        {
            if (VerificationNumberAndSeries(interdepart))
                Send(interdepart, (int)InterdepartStatus.Success);            
            else
                Send(interdepart, (int)InterdepartStatus.Denied);
        }
        private bool VerificationNumberAndSeries(InterdepartRequest interdepart)
        {
            if (string.IsNullOrWhiteSpace(interdepart.Number))
                return false;
            
            if (string.IsNullOrWhiteSpace(interdepart.Series))
                return false;
            
            if (interdepart.Number.Length != 6)
                return false;
            
            if (interdepart.Series.Length != 4)
                return false;
            
            return true;
        }
        private void Send(InterdepartRequest interdepart, int status)
        {
            InterdepartResponse interdepartResponse = new InterdepartResponse(interdepart.Id, status);
            var factory = new ConnectionFactory();
            factory.Uri = new System.Uri(Settings.Uri);

           using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: Settings.KeyResponse,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            string message = JsonConvert.SerializeObject(interdepartResponse);
            var body = System.Text.Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "", routingKey: Settings.KeyResponse, basicProperties: null, body: body);
        }
        
    }
}