using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.KafkaRfuel.Vocabularies
{
    public class  UserVocabulary : SimpleVocabulary
    {
        public  UserVocabulary()
        {
            VocabularyName = "KafkaRfuel User"; 
            KeyPrefix      = "kafkarfuel.user"; 
            KeySeparator   = ".";
            Grouping       = EntityType.Person; // TODO: Make sure EntityType is correct.

            //TODO: Make sure that any properties mapped into CluedIn Vocabulary are not in the group.
            AddGroup("KafkaRfuel User Details", group =>
            {
                Registertime = group.Add(new VocabularyKey("Registertime", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                Userid = group.Add(new VocabularyKey("Userid", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                Regionid = group.Add(new VocabularyKey("Regionid", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                Gender = group.Add(new VocabularyKey("Gender", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                
            });

            //TODO: If the property is already set in the clueproducer, it doesn't have to be here.
             
            //TODO: Don't forget to map all possible properties into already existing CluedIn Vocabularies.
        AddMapping(Regionid, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.AddressState);
        
        }


        
        public VocabularyKey Registertime { get; private set; }
        
        public VocabularyKey Userid { get; private set; }
        
        public VocabularyKey Regionid { get; private set; }
        
        public VocabularyKey Gender { get; private set; }
        
    }
}
