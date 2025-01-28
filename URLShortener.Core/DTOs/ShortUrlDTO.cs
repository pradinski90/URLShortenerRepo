using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLShortener.Core.DTOs
{
    public class ShortUrlDTO
    {
        public string FullUrl { get; set; }
        public string ShortUrlSuffix { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
