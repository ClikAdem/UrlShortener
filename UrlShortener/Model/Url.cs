
using System.Security.Cryptography;
using System.Text;

namespace UrlShortener.Model
{
    public class Url : BaseEntity
    {
        public Url(string longUrl)
        {
            _ = longUrl ?? throw new ArgumentNullException(nameof(longUrl));

            Validate(longUrl);
            var shortenedUrl = ShortenUrl(longUrl);
            Validate(shortenedUrl);

            this.LongUrl = longUrl;
            this.ShortUrl = shortenedUrl;

        }

        public string LongUrl { get; set; }

        public string ShortUrl { get; set; }

        //public string RevokeCode { get; set; }

        private bool Validate(string longUrl)
        {
            //var url = new Uri(longUrl);
            var result = Uri.IsWellFormedUriString(longUrl, UriKind.Absolute);

            if (!result)
            {
                throw new Exception("Given url is not valid");
            }

            return false;
        }
        private static string GetHash(string AbsolutePath)
        {

            using (SHA256 sha256Hash = SHA256.Create())
            {
                var hash = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(AbsolutePath));

                // Hash is only alpha numeric to prevent charecters that may break the url
                return string.Concat(Convert.ToBase64String(hash).ToCharArray().Where(x => char.IsLetterOrDigit(x)).Take(6));
            }
        }
        private static string ShortenUrl(string longUrl)
        {
            var url = new Uri(longUrl);
            var splittedHost = url.Host.Split(".");
            if (splittedHost.Length >= 3)
                return string.Concat(url.Scheme, Uri.SchemeDelimiter, string.Join(".", splittedHost[1..(splittedHost.Length - 1)]), ".", GetHash(url.AbsolutePath));
            else if(splittedHost.Length == 2)
                return string.Concat(url.Scheme, Uri.SchemeDelimiter, string.Join(".", splittedHost[0..(splittedHost.Length - 1)]), ".", GetHash(url.AbsolutePath));
            else
                return string.Concat(url.Scheme, Uri.SchemeDelimiter, string.Join(".", splittedHost[0..(splittedHost.Length)]), ".", GetHash(url.AbsolutePath));

        }        
    }
}
