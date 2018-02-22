using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.ViewModels
{
    public class FollowingViewModel
    {
        public IEnumerable<ApplicationUser> Followees { get; set; }
    }
}