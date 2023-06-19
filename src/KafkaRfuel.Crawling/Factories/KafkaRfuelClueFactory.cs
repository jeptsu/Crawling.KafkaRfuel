using System;
using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.KafkaRfuel.Core;
using RuleConstants = CluedIn.Core.Constants.Validation.Rules;

namespace CluedIn.Crawling.KafkaRfuel.Factories
{
    public class KafkaRfuelClueFactory : ClueFactory
    {
        public KafkaRfuelClueFactory()
            : base(KafkaRfuelConstants.CodeOrigin, KafkaRfuelConstants.ProviderRootCodeValue)
        {
        }

        protected override Clue ConfigureProviderRoot([NotNull] Clue clue)
        {
            if (clue == null)
            {
                throw new ArgumentNullException(nameof(clue));
            }

            var data = clue.Data.EntityData;
            data.Name = KafkaRfuelConstants.CrawlerName;
            data.Uri = new Uri(KafkaRfuelConstants.Uri);
            data.Description = KafkaRfuelConstants.CrawlerDescription;

            clue.ValidationRuleSuppressions.AddRange(new[] {RuleConstants.PROPERTIES_001_MustExist,});

            clue.ValidationRuleSuppressions.AddRange(new[]
            {
                RuleConstants.METADATA_002_Uri_MustBeSet, RuleConstants.PROPERTIES_002_Unknown_VocabularyKey_Used
            });

            return clue;
        }
    }
}
