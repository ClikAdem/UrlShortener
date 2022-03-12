using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UrlShortener.Data;
using UrlShortener.Model;

namespace UrlShortener.RepositoryServices
{
    public class UrlService : IUrlService
    {
        private readonly UrlDbContext _context;
        internal DbSet<Url> _dbSet;

        public UrlService(UrlDbContext context)
        {
            this._context = context;
            this._dbSet = context.Set<Url>();
        }

        public virtual async Task<IEnumerable<Url>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<IEnumerable<Url>> GetAllActiveAsync()
        {
            return await _dbSet.Where(x => x.IsActive).ToListAsync();
        }

        public virtual async Task<Url> GetByIdAsync(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<Url> GetByLongUrlAsync(string longUrl)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _dbSet.FirstOrDefaultAsync(x => x.LongUrl == longUrl);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<Url> GetByShortUrlAsync(string shortUrl)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _dbSet.FirstOrDefaultAsync(x => x.ShortUrl == shortUrl);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<bool> CreateAsync(Url entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(Url entity)
        {
            entity.UpdatedAt = DateTime.Now;

            await Task.Run(() => { _context.Update(entity); });
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Url entity)
        {
            entity.DeletedAt = DateTime.Now;
            entity.IsActive = false;

            await Task.Run(() => { _context.Update(entity); });
            await _context.SaveChangesAsync();
            return true;
        }

        public virtual async Task<IEnumerable<Url>> FindAsync(Expression<Func<Url, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }
    }
}
