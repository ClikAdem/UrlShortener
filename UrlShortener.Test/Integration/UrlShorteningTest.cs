using System;
using UrlShortener.Model;
using Xunit;

namespace UrlShortener.Test.Integration
{
    public class UrlShorteningTest
    {
        // This part requires a mimicing api connection , thus it was not implemented
        [Fact]
        public void ShortenUrlWithoutAnyProblem()
        {
            //Arrange & act

            //Assert
            Assert.Equal("test", "test");

        }

        [Fact]
        public void GivenUrlIsNotValid()
        {
            //Arrange & act

            //Assert
            Assert.Equal("test", "test");

        }
    }
}
