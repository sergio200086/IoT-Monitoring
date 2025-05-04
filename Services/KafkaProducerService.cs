using Confluent.Kafka;
using System;
using System.Threading.Tasks;

namespace IoTMonitoringAPI.Services
{
    public class KafkaProducerService
    {
        private readonly string _bootstrapServers = "localhost:9092";
        private readonly string _topic = "iotSensor";

        public async Task SendMessageAsync(string message)
        {

            var config = new ProducerConfig { BootstrapServers = _bootstrapServers };
            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    var result = await producer.ProduceAsync(_topic, new Message<Null, string> { Value = message });
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Error enviando mensaje: {e.Error.Reason}");
                }
            }
        }

    }
}
