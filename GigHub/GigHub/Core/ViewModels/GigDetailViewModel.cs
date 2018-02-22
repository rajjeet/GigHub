using System.Linq;
using GigHub.Core.Models;

namespace GigHub.Core.ViewModels
{
    public class GigDetailViewModel
    {

        public Gig Gig { get; set; }
        public bool IsUserAuthenticated { get; set; }
        public string UserId { get; set; }

        public string Date => Gig.DateTime.ToString("MMM dd");
        public string Time => Gig.DateTime.ToString("HH:mm tt");

        public bool IsFollowing
        {
            get { return Gig.Artist.Followers.Select(f => f.FollowerId).Contains(UserId); }
        }

        public bool IsAttending
        {
            get { return Gig.Attendances.Select(a => a.AttendeeId).Contains(UserId); }
        }

        
    }
}