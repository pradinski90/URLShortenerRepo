namespace URLShortener.API.Requests
{
    public class CreateShortUrlRequest
    {
        public const string Route = "/api/v1/url";
        public string Url { get; set; }
    }
}
