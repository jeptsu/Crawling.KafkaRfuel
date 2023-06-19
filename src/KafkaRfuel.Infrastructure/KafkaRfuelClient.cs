using System;
using System.Net;
using System.Threading.Tasks;
using CluedIn.Core.Providers;
using CluedIn.Crawling.KafkaRfuel.Core;
using Newtonsoft.Json;
using RestSharp;
using Microsoft.Extensions.Logging;
using Confluent.Kafka;
using System.Collections.Generic;
using CluedIn.Crawling.KafkaRfuel.Core.Models;
using System.Threading;
using EasyNetQ;
using static Confluent.Kafka.ConfigPropertyNames;
using System.Linq;

namespace CluedIn.Crawling.KafkaRfuel.Infrastructure
{
    // TODO - This class should act as a client to retrieve the data to be crawled.
    // It should provide the appropriate methods to get the data
    // according to the type of data source (e.g. for AD, GetUsers, GetRoles, etc.)
    // It can receive a IRestClient as a dependency to talk to a RestAPI endpoint.
    // This class should not contain crawling logic (i.e. in which order things are retrieved)
    public class KafkaRfuelClient
    {
        private const string BaseUri = "http://sample.com";

        private readonly ILogger<KafkaRfuelClient> log;

        private readonly IRestClient client;

        public KafkaRfuelClient(ILogger<KafkaRfuelClient> log, KafkaRfuelCrawlJobData KafkaRfuelCrawlJobData, IRestClient client) // TODO: pass on any extra dependencies
        {
            if (KafkaRfuelCrawlJobData == null)
            {
                throw new ArgumentNullException(nameof(KafkaRfuelCrawlJobData));
            }

            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            this.log = log ?? throw new ArgumentNullException(nameof(log));
            this.client = client ?? throw new ArgumentNullException(nameof(client));

            // TODO use info from KafkaRfuelCrawlJobData to instantiate the connection
            client.BaseUrl = new Uri(BaseUri);
            client.AddDefaultParameter("api_key", KafkaRfuelCrawlJobData.ApiKey, ParameterType.QueryString);

            
        }
        public Config GetKafkaConfig()
        {
            log.LogInformation($"[kafka] Consume Config start");
            string bootstrapServers = "pkc-5m9gg.eastasia.azure.confluent.cloud:9092";
            string groupId = "id0";
            //string topic = "topic_1";
            string username = "OMJJO5DC4ZIF37K4";
            string password = "mF50bMLI0/VT+GDxImGI/nY4NY+B/RXfMD1JxF4boXJazj/wTaVMaCrOIOGtdvAN";

            var config = new ConsumerConfig
            {
                BootstrapServers = bootstrapServers,
                GroupId = groupId,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism = SaslMechanism.Plain,
                SaslUsername = username,
                SaslPassword = password
            };
            log.LogInformation($"[kafka] Consumer config End");
            return config;

        }

        public IEnumerable<string> ConsumeMessages(Config config)
        {
            log.LogInformation($"[kafka] Consume start");
            //List<User> users = new List<User>();
            List<string> messages = new List<string>();

            using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                var topic = "topic_1";
                consumer.Subscribe(topic);



                CancellationTokenSource cts = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) => {
                    e.Cancel = true;
                    cts.Cancel();
                };

                ConsumeResult<Confluent.Kafka.Ignore, string> message = null;

                try
                {
                    while (true)
                    {
                        message = consumer.Consume(cts.Token);
                        log.LogInformation($"Received message: {message.Message.Value}");
                        messages.Add(message.Message.Value);
                        //yield return message;
                    }
                }
                catch (OperationCanceledException)
                {
                    // Consumer is being closed
                }
                catch (ConsumeException e)
                {
                    log.LogInformation($"[kafka] Error occurred: {e.Error.Reason}");
                }

                finally
                {
                    consumer.Close();
                }
                return messages.AsEnumerable();
            }

        }

        private async Task<T> GetAsync<T>(string url)
        {
            var request = new RestRequest(url, Method.GET);

            var response = await client.ExecuteAsync(request, request.Method);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                var diagnosticMessage = $"Request to {client.BaseUrl}{url} failed, response {response.ErrorMessage} ({response.StatusCode})";
                log.LogError(diagnosticMessage);
                throw new InvalidOperationException($"Communication to jsonplaceholder unavailable. {diagnosticMessage}");
            }

            var data = JsonConvert.DeserializeObject<T>(response.Content);

            return data;
        }

        public AccountInformation GetAccountInformation()
        {
            //TODO - return some unique information about the remote data source
            // that uniquely identifies the account
            return new AccountInformation("", "");
        }
    }
}
