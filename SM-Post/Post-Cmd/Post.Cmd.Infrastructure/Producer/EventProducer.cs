
using Confluent.Kafka;
using CQRS.Core.Events;
using CQRS.Core.Producer;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Post.Cmd.Infrastructure.Producer {

    public class EventProducer : IEventProducer
    {
        private readonly ProducerConfig _config;

        public EventProducer(IOptions<ProducerConfig> config)
        {
            _config = config.Value;
            var testConfig = new ProducerConfig {
                BootstrapServers = "localhost:9092"
            };
        }

        public async Task ProduceAsync<T>(string topic, T @event) where T : BaseEvent
        {
            using var producer = new ProducerBuilder<string, string>(_config)
            .SetKeySerializer(Serializers.Utf8)
            .SetValueSerializer(Serializers.Utf8)
            .Build();

            var eventMessage = new Message<string, string>{
                Key = Guid.NewGuid().ToString(),
                Value = JsonSerializer.Serialize(@event, @event.GetType())
            };
            var deliveryResult = await producer.ProduceAsync(topic, eventMessage);
            if(deliveryResult.Status == PersistenceStatus.NotPersisted)
            {
                throw new Exception($"Could not produce {@event.GetType().Name} message to topic {topic} due to following reasons {deliveryResult.Message}.");
            }
        }
    }

}