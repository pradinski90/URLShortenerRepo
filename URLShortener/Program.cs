using Autofac;
using Autofac.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using URLShortener.Core.Entities.ShortenedUrlAggregate.Commands;
using URLShortener.Core.Interfaces;
using URLShortener.Core.Interfaces.Repositories;
using URLShortener.Core.Interfaces.Services;
using URLShortener.Core.Services;
using URLShortener.Infrastructure.Behaviors;
using URLShortener.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<UrlDbContext>(options =>
    {
        Console.WriteLine(builder.Configuration.GetConnectionString("SQL"));
        options.UseSqlServer(builder.Configuration.GetConnectionString("SQL"),
            sqlServerOptionsAction: sqlOptions =>
            {
                sqlOptions.MigrationsAssembly("URLShortener.Infrastructure");
                sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
            });
    },
    ServiceLifetime.Scoped
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
//    .AsImplementedInterfaces();

//// Register all the Command classes (they implement IRequestHandler) in assembly holding the Commands
//builder.RegisterAssemblyTypes(typeof(CreatePostCommand).GetTypeInfo().Assembly)
//    .AsClosedTypesOf(typeof(IRequestHandler<,>));

//// Register the DomainEventHandler classes (they implement INotificationHandler<>) in assembly holding the Domain Events
//builder.RegisterAssemblyTypes(typeof(PostCreatedEmailNotificationHandler).GetTypeInfo().Assembly)
//    .AsClosedTypesOf(typeof(INotificationHandler<>));


//builder.Register<ServiceFactory>(context =>
//{
//    var componentContext = context.Resolve<IComponentContext>();
//    return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
//});

//builder.RegisterGeneric(typeof(LoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>));

builder.Services.AddScoped<IShortUrlRepository, ShortUrlRepository>();
builder.Services.AddScoped<IShortUrlService, ShortUrlService>();
builder.Services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateShortUrlCommand).Assembly));

builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    //https://localhost:8081/swagger/index.html
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<UrlDbContext>();
    db.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
