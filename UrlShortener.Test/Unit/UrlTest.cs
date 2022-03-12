using System;
using UrlShortener.Model;
using Xunit;

namespace UrlShortener.Test.Unit
{
    public class UrlTest
    {
        
        [Fact]
        public void UrlIsNotNull()
        {
            var url = new Url("https://www.sample-site.com/karriere/berufserfahrene/direkteinstieg/");

            Assert.NotNull(url);
        }

        [Fact]
        public void CanShortenUrl()
        {
            var url = new Url("https://www.sample-site.com/karriere/berufserfahrene/direkteinstieg/");

            Assert.Equal($"https://sample-site.j2b8UB", url.ShortUrl);
        }

        [Fact]
        public void UrlIsNotValid()
        {
            var exception = Assert.Throws<Exception>(() => new Url("This is not an url"));
            Assert.Equal($"Given url is not valid", exception.Message);
        }
    }
}