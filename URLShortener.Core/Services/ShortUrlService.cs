using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.Logging;
using URLShortener.Core.DTOs;
using URLShortener.Core.Entities.ShortenedUrlAggregate.Events;
using URLShortener.Core.Entities.ShortenedUrlAggregate.Specifications;
using URLShortener.Core.Entities.ShortUrlAggregate;
using URLShortener.Core.Extensions;
using URLShortener.Core.Interfaces;
using URLShortener.Core.Interfaces.Repositories;
using URLShortener.Core.Interfaces.Services;

namespace URLShortener.Core.Services
{
    public class ShortUrlService : IShortUrlService
    {
        private readonly IShortUrlRepository _shortenedUrlRepository;
        private readonly ILogger<ShortUrlService> _logger;
        //private readonly IMediatorHandler _mediatorHandler;

        public ShortUrlService(IShortUrlRepository shortenedUrlRepository,
            ILogger<ShortUrlService> logger)
        {
            _shortenedUrlRepository = shortenedUrlRepository ?? throw new ArgumentNullException(nameof(shortenedUrlRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result<ShortUrlDTO>> CreateAsync(ShortUrlDTO shortUrl, CancellationToken cancellationToken)
        {
            if (!Uri.TryCreate(shortUrl.FullUrl, UriKind.Absolute, out var url))
            {
                _logger.LogInformation("-- Failed creating ShortenedUrl for LongUrl: {@FullUrl}", shortUrl.FullUrl);

                return Result<ShortUrlDTO>.Error("The provided URL is invalid.");
            }

            var newShortenedUrl = new ShortenedUrl(shortUrl.FullUrl, shortUrl.DateCreated);

            _logger.LogInformation("-- Creating ShortenedUrl - ShortenedUrl: {@FullUrl}, {@Suffix}", newShortenedUrl.FullUrl, newShortenedUrl.ShortUrlSuffix);

            newShortenedUrl = await _shortenedUrlRepository.AddAsync(newShortenedUrl, cancellationToken);

            _logger.LogInformation("-- Created ShortenedUrl - ShortenedUrl: {@FullUrl}, {@Suffix}", newShortenedUrl.FullUrl, newShortenedUrl.ShortUrlSuffix);

            if (newShortenedUrl.Id != 0)
            {
                var newShortenedUrlCreatedEvent = new ShortenedUrlCreatedEvent(newShortenedUrl);

                _logger.LogInformation(
                    "-- Sending event: {@EventName}, {@EventId}, {@IdProperty}",
                    newShortenedUrlCreatedEvent.MessageType,
                    nameof(newShortenedUrlCreatedEvent.ShortenedUrl.Id),
                    newShortenedUrlCreatedEvent.ShortenedUrl.Id);

                //Publish event for further proessing you can have outbox pattern with a tansaction
                //await _mediatorHandler.PublishEvent(newShortenedUrlCreatedEvent);
            }

            return Result<ShortUrlDTO>.Created(newShortenedUrl.ToShortUrlDTO());
        }
        public async Task<Result<ShortUrlDTO>> GetAsync(string shortUrlSuffix, CancellationToken cancellationToken)
        {
            var shortenedUrl = await _shortenedUrlRepository.FirstOrDefaultAsync(new UrlByShortUrlSuffixSpecifications(shortUrlSuffix), cancellationToken);

            if (shortenedUrl == null) return Result<ShortUrlDTO>.NotFound();

            _logger.LogInformation("-- ShortenedUrl url Entry found: {@Id}", shortenedUrl.Id);

            return Result<ShortUrlDTO>.Success(shortenedUrl.ToShortUrlDTO());
        }
    }
}
