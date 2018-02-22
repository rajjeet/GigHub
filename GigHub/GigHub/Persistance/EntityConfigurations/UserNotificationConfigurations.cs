using System.Data.Entity.ModelConfiguration;
using GigHub.Core.Models;

namespace GigHub.Persistance.EntityConfigurations
{
    public class UserNotificationConfigurations : EntityTypeConfiguration<UserNotification>
    {
        public UserNotificationConfigurations()
        {
            HasRequired(n => n.User)
                .WithMany(u => u.UserNotifications)
                .WillCascadeOnDelete(false);
        }

        
    }
}