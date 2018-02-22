using System.Data.Entity.ModelConfiguration;
using GigHub.Core.Models;

namespace GigHub.Persistance.EntityConfigurations
{
    public class NotificationConfigurations : EntityTypeConfiguration<Notification>
    {
        public NotificationConfigurations()
        {
            HasRequired(n => n.Gig);
        }
    }
}