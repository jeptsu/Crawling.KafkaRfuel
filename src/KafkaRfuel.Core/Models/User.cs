using Newtonsoft.Json;

namespace CluedIn.Crawling.KafkaRfuel.Core.Models
{
    public class User 
    {

        [JsonProperty("registertime")]
        public string Registertime {get; set;}

        [JsonProperty("userid")]
        public string Userid {get; set;}

        [JsonProperty("regionid")]
        public string Regionid {get; set;}

        [JsonProperty("gender")]
        public string Gender {get; set;}

    }
}
