using GigHub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.Persistance.EntityConfigurations
{
    public class ApplicationUserConfigurations : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserConfigurations()
        {
               HasMany(u => u.Followers)
                .WithRequired(f => f.Followee)
                .WillCascadeOnDelete(false);

               HasMany(u => u.Followees)
                .WithRequired(f => f.Follower)
                .WillCascadeOnDelete(false);
        }
    }
}