using System;
using System.Collections.Generic;
using AutoFixture.Xunit2;
using Xunit;

namespace CluedIn.Provider.KafkaRfuel.Unit.Test.KafkaRfuelProvider
{
    public static class WebhookManagementEndpoints
    {
        public class FailureCases : KafkaRfuelProviderTest
        {
            [Theory]
            [InlineData(null)]
            public void NullParameterThrowsArgumentNullException(IEnumerable<string> ids)
            {
                Assert.Throws<ArgumentNullException>(() => Sut.WebhookManagementEndpoints(ids));
            }
        }

        public class PassCases : KafkaRfuelProviderTest
        {
            [Theory]
            [InlineAutoData]
            public void PublicMethodThrowsNotImplementedException(IEnumerable<string> ids)
            {
                Assert.Throws<NotImplementedException>(() => Sut.WebhookManagementEndpoints(ids));
            }
        }
    }
}
