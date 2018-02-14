﻿using GigHub.Models;
using GigHub.ViewModels;
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

        public ActionResult Index()
        {
            var upcomingGigs = _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => 
                    g.DateTime > DateTime.Now &&
                    !g.IsCancelled);

            var homeViewModel = new GigsViewModel
            {
                UpcomingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs"
            };

            return View("Gigs", homeViewModel);
        }
    }
}