using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IGigsRepository
    {
        IEnumerable<Gig> GetGigsForArtist(string userId);
        IEnumerable<Gig> GetAttendingGigsForUser(string userId);
        Gig GetGigByGigId(int id);
        void Add(Gig gig);
    }
}