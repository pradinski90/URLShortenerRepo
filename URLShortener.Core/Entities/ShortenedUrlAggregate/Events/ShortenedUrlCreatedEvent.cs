using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.Core.Entities.ShortUrlAggregate;
using URLShortener.Core.Messaging;

namespace URLShortener.Core.Entities.ShortenedUrlAggregate.Events
{
    public class ShortenedUrlCreatedEvent : Event
    {
        public ShortenedUrl ShortenedUrl { get; set; }
        public ShortenedUrlCreatedEvent(ShortenedUrl shortenedUrl)
        {
            ShortenedUrl = shortenedUrl;
        }
    }
}
