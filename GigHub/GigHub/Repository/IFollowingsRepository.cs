using System.Collections.Generic;
using GigHub.Models;

namespace GigHub.Repository
{
    public interface IFollowingsRepository
    {
        IEnumerable<ApplicationUser> GetFollowersForArtist(string userId);
    }
}