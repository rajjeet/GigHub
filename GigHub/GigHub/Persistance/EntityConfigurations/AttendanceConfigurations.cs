using System.Data.Entity.ModelConfiguration;
using GigHub.Core.Models;

namespace GigHub.Persistance.EntityConfigurations
{
    public class AttendanceConfigurations : EntityTypeConfiguration<Attendance>
    {
        public AttendanceConfigurations()
        {
            HasKey(a => new {a.GigId, a.AttendeeId});
        }
    }
}