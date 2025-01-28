using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.Core.Entities.ShortUrlAggregate;

namespace URLShortener.Core.Interfaces.Repositories
{   
    public interface IShortUrlRepository : IAsyncRepository<ShortenedUrl>
    {
        Task<ShortenedUrl> AddAsync(ShortenedUrl entity, CancellationToken cancellationToken = default);
        Task<ShortenedUrl?> FirstOrDefaultAsync(ISpecification<ShortenedUrl> spec, CancellationToken cancellationToken = default);
    }
}
