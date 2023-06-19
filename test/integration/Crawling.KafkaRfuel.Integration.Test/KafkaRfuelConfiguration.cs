using System.Collections.Generic;
using CluedIn.Crawling.KafkaRfuel.Core;

namespace CluedIn.Crawling.KafkaRfuel.Integration.Test
{
  public static class KafkaRfuelConfiguration
  {
    public static Dictionary<string, object> Create()
    {
      return new Dictionary<string, object>
            {
                { KafkaRfuelConstants.KeyName.ApiKey, "demo" }
            };
    }
  }
}
