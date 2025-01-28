using Ardalis.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.Core.DTOs;

namespace URLShortener.Core.Interfaces.Services
{
    public interface IShortUrlService
    {
        Task<Result<ShortUrlDTO>> CreateAsync(ShortUrlDTO shortUrl, CancellationToken cancellationToken);

        Task<Result<ShortUrlDTO>> GetAsync(string shortUrlSuffix, CancellationToken cancellationToken);
    }
}
