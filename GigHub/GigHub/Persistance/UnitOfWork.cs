using GigHub.Models;
using GigHub.Repository;

namespace GigHub.Persistance
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public GigsRepository Gigs { get; private set; }
        public FollowingsRepository Followings { get; set; }
        public GenreRepository Genres { get; set; }
        public AttendancesRepository Attendances { get; set; }

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