using Ardalis.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.Core.DTOs;
using URLShortener.Core.Interfaces.Services;
using URLShortener.Core.Messaging;

namespace URLShortener.Core.Entities.ShortenedUrlAggregate.Commands
{
    public class CreateShortUrlCommandHandler : CommandHandler<CreateShortUrlCommand, Result<ShortUrlDTO>>
    {
        private readonly IShortUrlService _shortUrlService;

        public CreateShortUrlCommandHandler(IShortUrlService shortUrlService)
        {
            _shortUrlService = shortUrlService ?? throw new ArgumentNullException(nameof(shortUrlService));
        }
        public override async Task<Result<ShortUrlDTO>> Handle(CreateShortUrlCommand command, CancellationToken cancellationToken)
        {
            return await _shortUrlService.CreateAsync(new ShortUrlDTO
            {
                FullUrl = command.FullUrl,
                ShortUrlSuffix = command.ShortUrlSuffix,
                DateCreated = command.DateCreated,
            }, cancellationToken);
        }
    }
}
