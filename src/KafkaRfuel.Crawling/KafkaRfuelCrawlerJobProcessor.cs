using CluedIn.Crawling.KafkaRfuel.Core;

namespace CluedIn.Crawling.KafkaRfuel
{
    public class KafkaRfuelCrawlerJobProcessor : GenericCrawlerTemplateJobProcessor<KafkaRfuelCrawlJobData>
    {
        public KafkaRfuelCrawlerJobProcessor(KafkaRfuelCrawlerComponent component) : base(component)
        {
        }
    }
}
