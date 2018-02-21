using GigHub.Repository;

namespace GigHub.Persistance
{
    public interface IUnitOfWork
    {
        IGigsRepository Gigs { get; }
        IFollowingsRepository Followings { get; set; }
        IGenreRepository Genres { get; set; }
        IAttendancesRepository Attendances { get; set; }
        void Complete();
    }
}