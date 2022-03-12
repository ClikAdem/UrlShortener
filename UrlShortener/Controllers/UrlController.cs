using Microsoft.AspNetCore.Mvc;
using UrlShortener.Model;
using UrlShortener.RepositoryServices;

namespace UrlShortener.Controllers
{
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly ILogger<UrlController> _logger;
        private readonly IUrlService _urlService;

        public UrlController(ILogger<UrlController> logger, IUrlService urlService)
        {
            _logger = logger;
            _urlService = urlService;
        }

        [HttpPost]
        [Route("[controller]/shortenurl")]
        public async Task<IActionResult> ShortenUrl(string url)
        {
            try
            {
                var existingUrl = await _urlService.GetByLongUrlAsync(url);
                if (existingUrl != null)
                {
                    return new JsonResult(new
                    {
                        code = 201,
                        data = new
                        {
                            longUrl = existingUrl.LongUrl,
                            shortUrl = existingUrl.ShortUrl
                        }
                    });
                }

                var newUrl = new Url(url);
                await _urlService.CreateAsync(newUrl);

                return new JsonResult(new
                {
                    code = 201,
                    data = new
                    {
                        longUrl = newUrl.LongUrl,
                        shortUrl = newUrl.ShortUrl
                    }
                });
            }
            catch (Exception ex)
            {

                return new JsonResult(new
                {
                    code = 400,
                    message = ex.Message,
                });
            }
        }

        [HttpGet]
        [Route("[controller]/redirecturl")]
        public async Task<IActionResult> RedirectUrl(string url)
        {
            try
            {
                var existingUrl = await _urlService.GetByShortUrlAsync(url);

                if (existingUrl == null)
                {
                    return new JsonResult(new
                    {
                        code = 404,
                        message = "Short url is not found",
                    });
                }

                return new JsonResult(new
                {
                    code = 200,
                    data = new
                    {
                        shortUrl = existingUrl.ShortUrl,
                        redirectedUrl = existingUrl.LongUrl,
                        
                    }
                });
            }
            catch (Exception ex)
            {

                return new JsonResult(new
                {
                    code = 503,
                    message = ex.Message,
                });
            }
        }

        [HttpGet]
        [Route("[controller]/customurl")]
        public async Task<IActionResult> CustomUrl(string url , string chosenUrl)
        {
            try
            {
                
                var newShortUrl =  Url;
                var newLongUrl = new Url(url);

                newLongUrl.ShortUrl = chosenUrl;


                var existingUrl = await _urlService.GetByLongUrlAsync(url);
                if (existingUrl != null && existingUrl.ShortUrl== chosenUrl)
                {
                    return new JsonResult(new
                    {
                        code = 201,
                        data = new
                        {
                            longUrl = existingUrl.LongUrl,
                            shortUrl = existingUrl.ShortUrl
                        }
                    });
                }

                await _urlService.CreateAsync(newLongUrl);

                return new JsonResult(new
                {
                    code = 201,
                    data = new
                    {
                        longUrl = newLongUrl.LongUrl,
                        shortUrl = newLongUrl.ShortUrl
                    }
                });
            }
            catch (Exception ex)
            {

                return new JsonResult(new
                {
                    code = 503,
                    message = ex.Message,
                });
            }
        }
    }
}