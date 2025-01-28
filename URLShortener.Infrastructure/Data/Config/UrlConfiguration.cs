using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.Core.Entities.ShortUrlAggregate;

namespace URLShortener.Infrastructure.Data.Config
{
    public class UrlConfiguration : IEntityTypeConfiguration<ShortenedUrl>
    {
        public void Configure(EntityTypeBuilder<ShortenedUrl> builder)
        {
            builder.ToTable("Urls");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
               .UseHiLo("urls_hilo")
               .IsRequired();

            builder.Property(ci => ci.FullUrl)
                .IsRequired(true);

            builder.Property(ci => ci.ShortUrlSuffix)
                .IsRequired(true)
                .HasMaxLength(50);

            builder.Property(ci => ci.DateCreated)
                .IsRequired(true);

        }
    }

}
