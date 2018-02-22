using System.Collections.Generic;
using System.Linq;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Persistance.Repositories
{
    public class FollowingsRepository : IFollowingsRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowingsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ApplicationUser> GetFollowersForArtist(string userId)
        {
            return _context.Followings
                .Where(f => f.Follower.Id == userId)
                .Select(f => f.Followee)
                .ToList();
        }
    }
}