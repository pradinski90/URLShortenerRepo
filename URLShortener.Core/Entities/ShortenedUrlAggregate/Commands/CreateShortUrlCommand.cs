using Ardalis.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.Core.DTOs;
using URLShortener.Core.Messaging;

namespace URLShortener.Core.Entities.ShortenedUrlAggregate.Commands
{
    public class CreateShortUrlCommand : Command<Result<ShortUrlDTO>>
    {
        public CreateShortUrlCommand(string fullUrl)
        {
            this.FullUrl = fullUrl;
        }
        public string FullUrl { get; private set; }
        public string ShortUrlSuffix { get; private set; }
        public DateTime DateCreated { get; private set; } = DateTime.UtcNow;
    }
}
