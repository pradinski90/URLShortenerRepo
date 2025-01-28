using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using URLShortener.Core.DTOs;
using URLShortener.Core.Entities.ShortenedUrlAggregate.Events;
using URLShortener.Core.Entities.ShortenedUrlAggregate.Specifications;
using URLShortener.Core.Entities.ShortUrlAggregate;
using URLShortener.Core.Extensions;
using URLShortener.Core.Interfaces;
using URLShortener.Core.Interfaces.Repositories;
using URLShortener.Core.Interfaces.Services;
using URLShortener.Core.Utilities;

namespace URLShortener.Core.Services
{
    public class ShortUrlService : IShortUrlService
    {
        private readonly IShortUrlRepository _shortenedUrlRepository;
        private readonly ILogger<ShortUrlService> _logger;
        private readonly IDistributedCache _cache;
        //private readonly IMediatorHandler _mediatorHandler;

        public ShortUrlService(IShortUrlRepository shortenedUrlRepository,
            ILogger<ShortUrlService> logger, 
            IDistributedCache cache)
        {
            _shortenedUrlRepository = shortenedUrlRepository ?? throw new ArgumentNullException(nameof(shortenedUrlRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
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

                var serializedData = JsonSerializer.Serialize(newShortenedUrl);
                //options can be added in the params use defaults
                await _cache.SetStringAsync(newShortenedUrl.ShortUrlSuffix, serializedData);

                //Publish event for further proessing you can have outbox pattern with a tansaction
                //await _mediatorHandler.PublishEvent(newShortenedUrlCreatedEvent);
            }

            return Result<ShortUrlDTO>.Created(newShortenedUrl.ToShortUrlDTO());
        }
        public async Task<Result<ShortUrlDTO>> GetAsync(string shortUrlSuffix, CancellationToken cancellationToken)
        {
            var shortenedUrl = await _cache.GetOrAddAsync<ShortenedUrl>(shortUrlSuffix, async () =>
            {
                return await _shortenedUrlRepository.FirstOrDefaultAsync(new UrlByShortUrlSuffixSpecifications(shortUrlSuffix), cancellationToken);
            });

            if (shortenedUrl == null) return Result<ShortUrlDTO>.NotFound();

            _logger.LogInformation("-- ShortenedUrl url Entry found: {@Id}", shortenedUrl.Id);

            return Result<ShortUrlDTO>.Success(shortenedUrl.ToShortUrlDTO());
        }
    }
}
