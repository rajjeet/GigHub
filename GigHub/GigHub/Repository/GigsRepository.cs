using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Repository
{
    public class GigsRepository
    {
        private readonly ApplicationDbContext _context;

        public GigsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Gig> GetGigsForArtist(string userId)
        {
            return _context.Gigs
                .Include(g => g.Genre)
                .Where(g =>
                    g.ArtistId == userId &&
                    g.DateTime > DateTime.Now &&
                    !g.IsCancelled)
                .ToList();
        }

        public IEnumerable<Gig> GetAttendingGigsForUser(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();
        }

        public Gig GetGigByGigId(int id)
        {
            return _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Artist.Followers)
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .Single(g => g.Id == id);
        }

        public void Add(Gig gig)
        {
            _context.Gigs.Add(gig);
        }
    }
}