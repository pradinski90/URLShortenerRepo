dotnet ef migrations add InitialCreate --context UrlDbContext -p ../URLShortener.Infrastructure/URLShortener.Infrastructure.csproj -s URLShortener.API.csproj -o Data/Migrations
dotnet ef database update

dotnet ef migrations add InitialCreate --context UrlDbContext -p ../URLShortener.Infrastructure/URLShortener.Infrastructure.csproj -s URLShortener.API.csproj -o Data/Migrations

https://localhost:8081/api/v1/url?urlSuffix=Ecnh

redis container - 

redis-cli
HGETALL Ecnh