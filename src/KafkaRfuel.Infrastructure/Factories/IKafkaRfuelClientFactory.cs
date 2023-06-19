using CluedIn.Crawling.KafkaRfuel.Core;

namespace CluedIn.Crawling.KafkaRfuel.Infrastructure.Factories
{
    public interface IKafkaRfuelClientFactory
    {
        KafkaRfuelClient CreateNew(KafkaRfuelCrawlJobData KafkaRfuelCrawlJobData);
    }
}
