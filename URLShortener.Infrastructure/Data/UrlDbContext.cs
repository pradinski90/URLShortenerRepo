using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using URLShortener.Core.Entities.ShortUrlAggregate;
using URLShortener.Core.Messaging;

namespace URLShortener.Infrastructure.Data
{
    public class UrlDbContext : DbContext
    {
        public UrlDbContext(DbContextOptions<UrlDbContext> options) : base(options)
        {
        }
        public DbSet<ShortenedUrl> Urls { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            builder.Entity<ShortenedUrl>()
                .HasIndex(u => u.ShortUrlSuffix)
                .IsUnique();
        }
    }
}
