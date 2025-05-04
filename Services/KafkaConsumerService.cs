using Confluent.Kafka;
using IoTMonitoringAPI.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

public class KafkaConsumerService(ILogger<KafkaConsumerService> logger, IConfiguration configuration) : BackgroundService
{
    private readonly ILogger<KafkaConsumerService> _logger = logger;
    private readonly IConfiguration _configuration = configuration;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = _configuration["Kafka:BootstrapServers"],
            GroupId = "sensor-consumer-group",
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = true
        };

        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        consumer.Subscribe(_configuration["Kafka:Topic"]);

        while (!stoppingToken.IsCancellationRequested)
        {
            var batch = new List<string>();
            var startTime = DateTime.UtcNow;

            // Lee durante 15 segundos todos los mensajes disponibles
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var cr = consumer.Consume(TimeSpan.FromMilliseconds(500));
                    if (cr != null)
                    {
                        batch.Add(cr.Message.Value);
                    }
                }
                catch (ConsumeException e)
                {
                    _logger.LogError("Error al consumir: {error}", e.Error.Reason);
                }
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }

            if (batch.Count > 0)
            {
                _logger.LogInformation("Procesando lote de {count} mensajes", batch.Count);

                foreach (var msg in batch)
                {
                    _logger.LogInformation("Mensaje: {message}", msg);

                    // Opcional: deserializa si es un JSON válido
                    var data = JsonConvert.DeserializeObject<SensorData>(msg);
                    Console.WriteLine(data.SensorName);
                }
            }
        }

        consumer.Close();
    }
}
