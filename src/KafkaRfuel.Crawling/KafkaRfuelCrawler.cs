using System;
using System.Collections.Generic;
using System.Threading;
using CluedIn.Core.Crawling;
using CluedIn.Crawling.KafkaRfuel.Core;
using CluedIn.Crawling.KafkaRfuel.Infrastructure;
using CluedIn.Crawling.KafkaRfuel.Infrastructure.Factories;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Serilog;

namespace CluedIn.Crawling.KafkaRfuel
{
    public class KafkaRfuelCrawler : ICrawlerDataGenerator
    {
        private readonly IKafkaRfuelClientFactory clientFactory;
        private readonly ILogger<KafkaRfuelCrawler> log;
        public KafkaRfuelCrawler(IKafkaRfuelClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        public IEnumerable<object> GetData(CrawlJobData jobData)
        {
            if (!(jobData is KafkaRfuelCrawlJobData KafkaRfuelcrawlJobData))
            {
                yield break;
            }

            var client = clientFactory.CreateNew(KafkaRfuelcrawlJobData);
            var config = client.GetKafkaConfig();

            //retrieve data from provider and yield objects

            log.LogInformation("[kafka] Getdata Start");

            foreach (var message in client.ConsumeMessages(config))
            {
                yield return message;
            }

            log.LogInformation("[kafka] Getdata End");

        }       
    }
}
