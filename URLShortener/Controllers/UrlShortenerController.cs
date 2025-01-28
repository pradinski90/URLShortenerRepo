using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using URLShortener.API.Requests;
using URLShortener.Core.DTOs;
using URLShortener.Core.Entities.ShortenedUrlAggregate.Commands;
using URLShortener.Core.Entities.ShortenedUrlAggregate.Queries;

namespace URLShortener.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UrlShortenerController : ControllerBase
    {
        private readonly ILogger<UrlShortenerController> _logger;
        private readonly IMediator _mediator;

        public UrlShortenerController(ILogger<UrlShortenerController> logger, IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost(CreateShortUrlRequest.Route)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ShortUrlDTO), (int)HttpStatusCode.OK)]
        public async Task<Result<ShortUrlDTO>> Create(CreateShortUrlRequest request)
        {
            var createShortUrlCommand = new CreateShortUrlCommand(request.Url);

            _logger.LogTrace(
                "-- Sending command: {@CommandName}, {@CommandId} {@Command}",
                createShortUrlCommand.GetType(),
                createShortUrlCommand.FullUrl,
                createShortUrlCommand);

            return await _mediator.Send<Result<ShortUrlDTO>>(createShortUrlCommand);
        }

        [HttpGet(GetShortUrlRequest.Route)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ShortUrlDTO), (int)HttpStatusCode.OK)]
        public async Task<Result<ShortUrlDTO>> Get([FromRoute] GetShortUrlRequest request)
        {
            var getShortUrlQuery = new GetShortUrlQuery(request.UrlSuffix);

            _logger.LogTrace(
                "-- Sending query: {@QueryName}, {@QueryId} {@Query}",
                getShortUrlQuery.GetType(),
                getShortUrlQuery.ShortUrlSuffix,
                getShortUrlQuery);

            return await _mediator.Send<Result<ShortUrlDTO>>(getShortUrlQuery);
        }
    }
}
