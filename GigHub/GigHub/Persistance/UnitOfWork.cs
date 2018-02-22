using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Persistance.Repositories;

namespace GigHub.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IGigsRepository Gigs { get; private set; }
        public IFollowingsRepository Followings { get; set; }
        public IGenreRepository Genres { get; set; }
        public IAttendancesRepository Attendances { get; set; }

        public UnitOfWork()
        {
            _context = new ApplicationDbContext();
            Gigs = new GigsRepository(_context);
            Attendances = new AttendancesRepository(_context);
            Genres = new GenreRepository(_context);
            Followings = new FollowingsRepository(_context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}