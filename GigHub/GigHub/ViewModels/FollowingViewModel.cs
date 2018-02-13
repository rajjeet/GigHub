using System.Collections.Generic;
using GigHub.Models;

namespace GigHub.ViewModels
{
    public class FollowingViewModel
    {
        public IEnumerable<ApplicationUser> Followees { get; set; }
    }
}