using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.ViewModels
{
    public class GigDetailViewModel
    {
        public string Artist { get; set; }
        public string Venue { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsUserAuthenticated { get; set; }
        public string UserId { get; set; }
        
        public ICollection<Following> Followers { get; set; }
        public ICollection<Attendance> Attendances { get; set; }

        public bool IsFollowing
        {
            get { return Followers.Select(f => f.FollowerId).Contains(UserId); }
        }

        public bool IsAttending
        {
            get { return Attendances.Select(a => a.AttendeeId).Contains(UserId); }
        }

        public string Date => DateTime.ToString("MMM dd");
        public string Time => DateTime.ToString("HH:mm tt");
    }
}