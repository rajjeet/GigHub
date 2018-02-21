using System.Collections.Generic;
using GigHub.Models;

namespace GigHub.Repository
{
    public interface IGigsRepository
    {
        IEnumerable<Gig> GetGigsForArtist(string userId);
        IEnumerable<Gig> GetAttendingGigsForUser(string userId);
        Gig GetGigByGigId(int id);
        void Add(Gig gig);
    }
}