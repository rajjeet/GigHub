using GigHub.Core.Models;
using System.Collections.Generic;

namespace GigHub.Core.Repositories
{
    public interface IGigsRepository
    {
        IEnumerable<Gig> GetAllFutureGigs();
        IEnumerable<Gig> GetGigsForArtist(string userId);
        IEnumerable<Gig> GetAttendingGigsForUser(string userId);
        Gig GetGigByGigId(int id);
        void Add(Gig gig);
    }
}