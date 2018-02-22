﻿using GigHub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.Persistance.EntityConfigurations
{
    public class GigConfigurations : EntityTypeConfiguration<Gig>
    {
        public GigConfigurations()
        {
            Property(g => g.ArtistId)
             .IsRequired();

            Property(g => g.GenreId)
            .IsRequired();

            Property(g => g.Venue)
            .IsRequired()
            .HasMaxLength(255);

            HasMany(g => g.Attendances)
            .WithRequired(a => a.Gig)
            .WillCascadeOnDelete(false);
        }
    }
}