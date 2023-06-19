using AutoFixture.Xunit2;
using System;
using Xunit;
using CluedIn.Core.Data;
using CluedIn.Crawling;
using CluedIn.Crawling.KafkaRfuel.ClueProducers;
using CluedIn.Crawling.KafkaRfuel.Core.Models;

namespace Crawling.KafkaRfuel.Unit.Test.ClueProducers
{
    public class UserClueProducerTests : BaseClueProducerTest<User>
    {
        protected override BaseClueProducer<User> Sut =>
            new UserClueProducer(_clueFactory.Object);

        protected override EntityType ExpectedEntityType => EntityType.Person;

        [Theory]
        [InlineAutoData]
        public void ClueHasEdgeToFolder(User user)
        {
            var clue = Sut.MakeClue(user, Guid.NewGuid());
            _clueFactory.Verify(
                //TODO verify some methods were called
                );
        }

        //TODO add other tests
    }
}
