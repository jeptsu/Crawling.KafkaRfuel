using CluedIn.Core.Crawling;
using CluedIn.Crawling;
using CluedIn.Crawling.KafkaRfuel;
using CluedIn.Crawling.KafkaRfuel.Infrastructure.Factories;
using Moq;
using Shouldly;
using Xunit;

namespace Crawling.KafkaRfuel.Unit.Test
{
    public class KafkaRfuelCrawlerBehaviour
    {
        private readonly ICrawlerDataGenerator _sut;

        public KafkaRfuelCrawlerBehaviour()
        {
            var nameClientFactory = new Mock<IKafkaRfuelClientFactory>();

            _sut = new KafkaRfuelCrawler(nameClientFactory.Object);
        }

        [Fact]
        public void GetDataReturnsData()
        {
            var jobData = new CrawlJobData();

            _sut.GetData(jobData)
                .ShouldNotBeNull();
        }
    }
}
