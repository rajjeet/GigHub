using System.Data.Entity.ModelConfiguration;
using GigHub.Core.Models;

namespace GigHub.Persistance.EntityConfigurations
{
    public class GenreConfigurations : EntityTypeConfiguration<Genre>
    {
        public GenreConfigurations()
        {
            Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}