using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.Core.Entities.ShortUrlAggregate;
using URLShortener.Core.Messaging;

namespace URLShortener.Core.Entities.ShortenedUrlAggregate.Specifications
{
    public class UrlByShortUrlSuffixSpecifications : Specification<ShortenedUrl>
    {
        public UrlByShortUrlSuffixSpecifications(string shortUrlSuffix)
        {
            Query.Where(c => shortUrlSuffix == c.ShortUrlSuffix);
        }
    }
}
