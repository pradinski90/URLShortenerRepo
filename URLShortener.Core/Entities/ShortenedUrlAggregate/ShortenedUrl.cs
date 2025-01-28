using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using URLShortener.Core.Interfaces;
using URLShortener.Core.Utilities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace URLShortener.Core.Entities.ShortUrlAggregate
{
    public class ShortenedUrl : BaseEntity, IAggregateRoot
    {
        public string FullUrl { get; private set; }
        public string ShortUrlSuffix { get; private set; }
        public DateTime DateCreated { get; private set; }

        //EF initialization
        private ShortenedUrl() { }
        
        public ShortenedUrl(string fullUrl, DateTime dateCreated)
        {
            Guard.Against.NullOrEmpty(fullUrl, nameof(fullUrl));

            this.FullUrl = fullUrl;
            this.DateCreated = dateCreated;
        }

        public void SetShortUrlSuffix()
        {
            var base62Converter = new Base62Converter();
            this.ShortUrlSuffix = base62Converter.Encode(this.Id.ToString());
        }
    }
}
