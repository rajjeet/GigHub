using GigHub.Models;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Repository
{
    public class AttendancesRepository : IAttendancesRepository
    {
        private readonly ApplicationDbContext _context;

        public AttendancesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Attendance> GetAttendancesForUser(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .ToList();
        }

    }
}