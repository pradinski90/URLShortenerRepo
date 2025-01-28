using Ardalis.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.Core.DTOs;
using URLShortener.Core.Messaging;

namespace URLShortener.Core.Entities.ShortenedUrlAggregate.Queries
{
    public class GetShortUrlQuery : Query<Result<ShortUrlDTO>>
    {
        public string ShortUrlSuffix { get; private set; }
        public GetShortUrlQuery(string shortUrlSuffix)
        {
            this.ShortUrlSuffix = shortUrlSuffix;
        }
    }
}
