using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.Core.DTOs;
using URLShortener.Core.Entities.ShortUrlAggregate;

namespace URLShortener.Core.Extensions
{
    public static class ShortenedUrlExtensions
    {
        public static ShortUrlDTO ToShortUrlDTO(this ShortenedUrl shortenedUrl)
        {
            return new ShortUrlDTO
            {
                FullUrl = shortenedUrl.FullUrl,
                ShortUrlSuffix = shortenedUrl.ShortUrlSuffix,
                DateCreated = shortenedUrl.DateCreated,
            };
        }

    }
}
