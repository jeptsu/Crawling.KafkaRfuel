using CluedIn.Core;
using CluedIn.Crawling.KafkaRfuel.Core;

using ComponentHost;

namespace CluedIn.Crawling.KafkaRfuel
{
    [Component(KafkaRfuelConstants.CrawlerComponentName, "Crawlers", ComponentType.Service, Components.Server, Components.ContentExtractors, Isolation = ComponentIsolation.NotIsolated)]
    public class KafkaRfuelCrawlerComponent : CrawlerComponentBase
    {
        public KafkaRfuelCrawlerComponent([NotNull] ComponentInfo componentInfo)
            : base(componentInfo)
        {
        }
    }
}

