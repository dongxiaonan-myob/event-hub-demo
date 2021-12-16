using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;

namespace EventHubPublisherDemo
{
    class Program
    {
        private const string connectionString = "<event hub connection string>";
        private const string eventHubName = "<event hub name>";

        // number of events to be sent to the event hub
        private const int numOfEvents = 25;

        // The Event Hubs client types are safe to cache and use as a singleton for the lifetime
        // of the application, which is best practice when events are being published or read regularly.        
        static EventHubProducerClient producerClient;    
        static async Task Main(string[] args)
        {
            // Create a producer client that you can use to send events to an event hub
            producerClient = new EventHubProducerClient(connectionString, eventHubName);

            // Create a batch of events 
            using EventDataBatch eventBatch = await producerClient.CreateBatchAsync();

            for (int i = 21; i <= numOfEvents; i++)
            {
                var payload = JsonSerializer.Serialize(new EventPayload() {EventType = eventHubName, Id = i, Description = ""});
                if (! eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes($"Event {payload}"))))
                {
                    // if it is too large for the batch
                    throw new Exception($"Event {i} is too large for the batch and cannot be sent.");
                }
            }

            try
            {
                // Use the producer client to send the batch of events to the event hub
                await producerClient.SendAsync(eventBatch);
                Console.WriteLine($"A batch of {numOfEvents} events has been published.");
            }
            finally
            {
                await producerClient.DisposeAsync();
            }
        }
    }

    class EventPayload
    {
        public string EventType { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
