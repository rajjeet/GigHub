using GigHub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.Persistance.EntityConfigurations
{
    public class UserNotificationConfigurations : EntityTypeConfiguration<UserNotification>
    {
        public UserNotificationConfigurations()
        {
            HasKey(un => new { un.UserId, un.NotificationId });

            HasRequired(n => n.User)
                .WithMany(u => u.UserNotifications)
                .WillCascadeOnDelete(false);
        }
    }
}