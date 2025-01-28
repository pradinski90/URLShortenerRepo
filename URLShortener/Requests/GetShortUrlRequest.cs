namespace URLShortener.API.Requests
{
    public class GetShortUrlRequest
    {
        public const string Route = "/api/v1/url";
        public string UrlSuffix { get; set; }
    }
}
