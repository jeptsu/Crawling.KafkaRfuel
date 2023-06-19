using Castle.Windsor;
using CluedIn.Core;
using CluedIn.Core.Providers;
using CluedIn.Crawling.KafkaRfuel.Infrastructure.Factories;
using Moq;

namespace CluedIn.Provider.KafkaRfuel.Unit.Test.KafkaRfuelProvider
{
    public abstract class KafkaRfuelProviderTest
    {
        protected readonly ProviderBase Sut;

        protected Mock<IKafkaRfuelClientFactory> NameClientFactory;
        protected Mock<IWindsorContainer> Container;

        protected KafkaRfuelProviderTest()
        {
            Container = new Mock<IWindsorContainer>();
            NameClientFactory = new Mock<IKafkaRfuelClientFactory>();
            var applicationContext = new ApplicationContext(Container.Object);
            Sut = new KafkaRfuel.KafkaRfuelProvider(applicationContext, NameClientFactory.Object);
        }
    }
}
