using System.Linq.Expressions;
using UrlShortener.Model;

namespace UrlShortener.RepositoryServices
{
    public interface IUrlService
    {
        Task<IEnumerable<Url>> GetAllAsync();
        Task<IEnumerable<Url>> GetAllActiveAsync();
        Task<Url> GetByIdAsync(int id);
        Task<Url> GetByLongUrlAsync(string longUrl);
        Task<Url> GetByShortUrlAsync(string shortUrl);
        Task<bool> CreateAsync(Url entity);
        Task<bool> UpdateAsync(Url entity);
        Task<bool> DeleteAsync(Url entity);
        Task<IEnumerable<Url>> FindAsync(Expression<Func<Url, bool>> predicate);
    }
}
