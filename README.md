dotnet ef migrations add InitialCreate --context UrlDbContext -p ../URLShortener.Infrastructure/URLShortener.Infrastructure.csproj -s URLShortener.API.csproj -o Data/Migrations
dotnet ef database update