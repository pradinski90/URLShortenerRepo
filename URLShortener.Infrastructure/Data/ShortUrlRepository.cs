using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.Core.Entities.ShortUrlAggregate;
using URLShortener.Core.Interfaces.Repositories;

namespace URLShortener.Infrastructure.Data
{
    public class ShortUrlRepository : EfRepository<ShortenedUrl>, IShortUrlRepository
    {
        public ShortUrlRepository(UrlDbContext dbContext): base(dbContext) { }

        public override async Task<ShortenedUrl> AddAsync(ShortenedUrl entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.Urls.AddAsync(entity);

            entity.SetShortUrlSuffix();

            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity;
        }
    }
}
