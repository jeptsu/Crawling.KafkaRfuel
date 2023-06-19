using System.IO;
using System.Reflection;
using Castle.MicroKernel.Registration;
using CluedIn.Crawling.KafkaRfuel.Core;
using CrawlerIntegrationTesting.Clues;
using Xunit.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using DebugCrawlerHost = CrawlerIntegrationTesting.CrawlerHost.DebugCrawlerHost<CluedIn.Crawling.KafkaRfuel.Core.KafkaRfuelCrawlJobData>;

namespace CluedIn.Crawling.KafkaRfuel.Integration.Test
{
    public class KafkaRfuelTestFixture
    {
        public ClueStorage ClueStorage { get; }
        private readonly DebugCrawlerHost debugCrawlerHost;

        public ILogger<KafkaRfuelTestFixture> Log { get; }

        public KafkaRfuelTestFixture()
        {
            var executingFolder = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;
            debugCrawlerHost = new DebugCrawlerHost(executingFolder, KafkaRfuelConstants.ProviderName, c => {
                c.Register(Component.For<ILogger>().UsingFactoryMethod(_ => NullLogger.Instance).LifestyleSingleton());
                c.Register(Component.For<ILoggerFactory>().UsingFactoryMethod(_ => NullLoggerFactory.Instance).LifestyleSingleton());
            });

            ClueStorage = new ClueStorage();

            Log = debugCrawlerHost.AppContext.Container.Resolve<ILogger<KafkaRfuelTestFixture>>();

            debugCrawlerHost.ProcessClue += ClueStorage.AddClue;

            debugCrawlerHost.Execute(KafkaRfuelConfiguration.Create(), KafkaRfuelConstants.ProviderId);
        }

        public void PrintClues(ITestOutputHelper output)
        {
            foreach(var clue in ClueStorage.Clues)
            {
                output.WriteLine(clue.OriginEntityCode.ToString());
            }
        }

        public void PrintLogs(ITestOutputHelper output)
        {
            //TODO:
            //output.WriteLine(Log.PrintLogs());
        }

        public void Dispose()
        {
        }

    }
}


