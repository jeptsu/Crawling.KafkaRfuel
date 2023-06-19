using System;
using CluedIn.Core.Data;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.Helpers;
using CluedIn.Crawling.KafkaRfuel.Vocabularies;
using CluedIn.Crawling.KafkaRfuel.Core.Models;
using CluedIn.Crawling.KafkaRfuel.Core;

namespace CluedIn.Crawling.KafkaRfuel.ClueProducers
{
    public class UserClueProducer : BaseClueProducer<User>
    {
        private readonly IClueFactory factory;

        public UserClueProducer(IClueFactory factory)
        {
            this.factory = factory;
        }

        protected override Clue MakeClueImpl(User input, Guid accountId)
        {
            // TODO: Verify EntityType and identifier are correct
            var clue = factory.Create(EntityType.Person,input.Registertime.ToString(), accountId);

            var userVocabulary = new UserVocabulary();

            var data = clue.Data.EntityData;

            // TODO: Uncomment or delete as appropriate for the different properties
            // if(input.Name != null)
            // {
            //     data.Name = input.Name;
            // }

            // if(input.DisplayName != null)
            // {
            //     data.DisplayName = input.DisplayName;
            // }

            // if(input.Description != null)
            // {
            //     data.Description = input.Description;
            // }



            // TODO: Example of Updated, Modified date being parsed through DateTimeOffset.
            // DateTimeOffset date;
            // if (DateTimeOffset.TryParse(input.CreatedAt, out date) && input.CreatedAt != null){
            //     data.CreatedDate = date;
            // }


            //TODO: Examples of edge creation
            // if (input.MobilePhone != null)
            // {
            //     factory.CreateIncomingEntityReference(clue, EntityType.PhoneNumber, EntityEdgeType.Parent, input.MobilePhone, input.MobilePhone);
            //     data.Properties[userVocabulary.MobilePhone] = input.MobilePhone.PrintIfAvailable();
            // }

            // if (input.WorkPhone != null)
            // {
            //     factory.CreateIncomingEntityReference(clue, EntityType.PhoneNumber, EntityEdgeType.Parent, input.WorkPhone, input.WorkPhone);
            //     data.Properties[userVocabulary.WorkPhone] = input.WorkPhone.PrintIfAvailable();
            // }


            //TODO: Example of PersonReference
            //  if (input.UpdatedBy != null)
            // {
            //     if (input.UpdatedByName != null)
            //     {
            //         var updatedPersonReference = new PersonReference(input.UpdatedByName, new EntityCode(EntityType.Person, KafkaRfuelConstants.CodeOrigin, input.UpdatedBy));
            //         data.LastChangedBy = updatedPersonReference;
            //     }
            //     else
            //     {
            //         var updatedPersonReference = new PersonReference(new EntityCode(EntityType.Person, KafkaRfuelConstants.CodeOrigin, input.UpdatedBy));
            //         data.LastChangedBy = updatedPersonReference;
            //     }
            // }
            if(!string.IsNullOrEmpty(input.Userid))
            {
                data.Codes.Add(new EntityCode("/TestUser", KafkaRfuelConstants.CodeOrigin, input.Userid));

            }
            //TODO: Mapping data into general properties metadata bag.
            //TODO: You should make sure as much data is mapped into specific metadata fields, rather than general .properties. bag.
            data.Properties[userVocabulary.Registertime] = input.Registertime.PrintIfAvailable();
            data.Properties[userVocabulary.Userid] = input.Userid.PrintIfAvailable();
            data.Properties[userVocabulary.Regionid] = input.Regionid.PrintIfAvailable();
            data.Properties[userVocabulary.Gender] = input.Gender.PrintIfAvailable();

            return clue;
        }
    }
}
