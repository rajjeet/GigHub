using System.Collections.Generic;
using GigHub.Models;

namespace GigHub.Repository
{
    public interface IAttendancesRepository
    {
        IEnumerable<Attendance> GetAttendancesForUser(string userId);
    }
}