using System.Collections.Generic;
using System.Linq;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Persistance.Repositories
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