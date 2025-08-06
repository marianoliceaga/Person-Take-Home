
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonAPI.Domain.Entities;
using PersonAPI.Domain.ValueObjects;

namespace PersonAPI.Infrastructure.Data.Configurations;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasKey(p => p.Id);

        builder.OwnsOne(p => p.Name, nameBuilder =>
        {
            nameBuilder.Property(n => n.GivenName)
                .HasMaxLength(100)
                .IsRequired();

            nameBuilder.Property(n => n.Surname)
                .HasMaxLength(100);
        });

        builder.OwnsOne(p => p.Gender, genderBuilder =>
        {
            genderBuilder.Property(g => g.Value)
                .HasMaxLength(50)
                .IsRequired();
        });

        builder.OwnsOne(p => p.BirthLocation, locationBuilder =>
        {
            locationBuilder.Property(l => l.City)
                .HasMaxLength(100);

            locationBuilder.Property(l => l.State)
                .HasMaxLength(100);

            locationBuilder.Property(l => l.Country)
                .HasMaxLength(100);
        });

        builder.OwnsOne(p => p.DeathLocation, locationBuilder =>
        {
            locationBuilder.Property(l => l.City)
                .HasMaxLength(100);

            locationBuilder.Property(l => l.State)
                .HasMaxLength(100);

            locationBuilder.Property(l => l.Country)
                .HasMaxLength(100);
        });

        builder.Property(p => p.BirthDate);

        builder.Property(p => p.DeathDate);

        builder.Property(p => p.CreatedAt);

        builder.Property(p => p.UpdatedAt)
            .IsRequired();

        builder.Property(p => p.Version)
            .IsRequired();
    }
}


