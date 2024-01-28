using System;
using Confluent.Kafka;
using Newtonsoft.Json;

public class KafkaProducer
{
    private readonly ProducerConfig _config;
    private readonly string _topic;

    public KafkaProducer(string bootstrapServers, string topic)
    {
        _config = new ProducerConfig { BootstrapServers = bootstrapServers };
        _topic = topic;
    }

    public void ProduceMessages(string topic, object message)
    {
        using (var producer = new ProducerBuilder<Null, string>(_config).Build())
        {
            try
            {
                var messageString = JsonConvert.SerializeObject(message);
                producer.Produce(topic, new Message<Null, string> { Value = messageString },
                    (deliveryReport) =>
                    {
                        if (deliveryReport.Error != null)
                        {
                            Console.WriteLine($"Error al enviar mensaje: {deliveryReport.Error.Reason}");
                        }
                        else
                        {
                            Console.WriteLine($"Mensaje enviado: {messageString}");
                        }
                    });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al producir mensajes: {ex.Message}");
            }
        }
    }
}

