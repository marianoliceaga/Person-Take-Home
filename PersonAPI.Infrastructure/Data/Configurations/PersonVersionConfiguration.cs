
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonAPI.Domain.Entities;

namespace PersonAPI.Infrastructure.Data.Configurations;

public class PersonVersionConfiguration : IEntityTypeConfiguration<PersonVersion>
{
    public void Configure(EntityTypeBuilder<PersonVersion> builder)
    {
        builder.HasKey(v => v.Id);

        builder.Property(v => v.PersonId)
            .IsRequired();

        builder.OwnsOne(v => v.Name, nameBuilder =>
        {
            nameBuilder.Property(n => n.GivenName)
                .HasMaxLength(100)
                .IsRequired();

            nameBuilder.Property(n => n.Surname)
                .HasMaxLength(100);
        });

        builder.OwnsOne(v => v.Gender, genderBuilder =>
        {
            genderBuilder.Property(g => g.Value)
                .HasMaxLength(50)
                .IsRequired();
        });

        builder.OwnsOne(v => v.BirthLocation, locationBuilder =>
        {
            locationBuilder.Property(l => l.City)
                .HasMaxLength(100);

            locationBuilder.Property(l => l.State)
                .HasMaxLength(100);

            locationBuilder.Property(l => l.Country)
                .HasMaxLength(100);
        });

        builder.OwnsOne(v => v.DeathLocation, locationBuilder =>
        {
            locationBuilder.Property(l => l.City)
                .HasMaxLength(100);

            locationBuilder.Property(l => l.State)
                .HasMaxLength(100);

            locationBuilder.Property(l => l.Country)
                .HasMaxLength(100);
        });

        builder.Property(v => v.Version)
            .IsRequired();

        builder.Property(v => v.CreatedAt)
            .IsRequired();

        builder.HasIndex(v => new { v.PersonId, v.Version });
    }
}


