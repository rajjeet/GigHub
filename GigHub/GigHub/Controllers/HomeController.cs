using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();            
        }

        public ActionResult Index(string query = null)
        {
            var upcomingGigs = _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => 
                    g.DateTime > DateTime.Now &&
                    !g.IsCancelled);

            if (!String.IsNullOrWhiteSpace(query))
            {
                upcomingGigs = upcomingGigs.Where(g =>
                    g.Artist.Name.Contains(query) ||
                    g.Genre.Name.Contains(query) ||
                    g.Venue.Contains(query)); 
            }

            var userId = User.Identity.GetUserId();
            var attendences = _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .ToList()
                .ToLookup(a => a.GigId);

            var homeViewModel = new GigsViewModel
            {
                UpcomingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs",
                SearchTerm = query,
                Attendances = attendences
            };

            return View("Gigs", homeViewModel);
        }

        

    }
}