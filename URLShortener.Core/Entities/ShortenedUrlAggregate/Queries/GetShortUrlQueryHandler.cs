using Ardalis.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.Core.DTOs;
using URLShortener.Core.Interfaces.Services;
using URLShortener.Core.Messaging;

namespace URLShortener.Core.Entities.ShortenedUrlAggregate.Queries
{
    public class GetShortUrlQueryHandler : QueryHandler<GetShortUrlQuery, Result<ShortUrlDTO>>
    {
        private readonly IShortUrlService _shortUrlService;

        public GetShortUrlQueryHandler(IShortUrlService shortUrlService)
        {
            _shortUrlService = shortUrlService ?? throw new ArgumentNullException(nameof(shortUrlService));
        }

        public async override Task<Result<ShortUrlDTO>> Handle(GetShortUrlQuery query, CancellationToken cancellationToken)
        {
            return await _shortUrlService.GetAsync(query.ShortUrlSuffix, cancellationToken);
        }
    }
}
