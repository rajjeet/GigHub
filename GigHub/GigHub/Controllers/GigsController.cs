using GigHub.Models;
using GigHub.Repository;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly GigsRepository _gigsRepository;
        private readonly FollowingsRepository _followingsRepository;
        private readonly AttendancesRepository _attendancesRepository;
        private readonly GenreRepository _genreRepository;

        public GigsController()
        {
            _context = new ApplicationDbContext();
            _genreRepository = new GenreRepository(_context);
            _followingsRepository = new FollowingsRepository(_context);
            _attendancesRepository = new AttendancesRepository(_context);
            _gigsRepository = new GigsRepository(_context);
        }

        [HttpPost]
        public ActionResult Search(GigsViewModel gigsViewModel)
        {
            return RedirectToAction("Index", "Home", new { query = gigsViewModel.SearchTerm });
        }

        [Authorize]
        public ActionResult Mine()
        {
            var gigs = _gigsRepository.GetGigsForArtist(User.Identity.GetUserId());

            return View(gigs);
        }

        [Authorize]
        public ActionResult Following()
        {
            var userId = User.Identity.GetUserId();
            var followees = _followingsRepository.GetFollowersForArtist(userId);

            var viewModel = new FollowingViewModel
            {
                Followees = followees
            };

            return View(viewModel);
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var upcomingGigs = _gigsRepository.GetAttendingGigsForUser(userId);

            var attendences = _attendancesRepository.GetAttendancesForUser(userId)
                .ToLookup(a => a.GigId);

            var viewModel = new GigsViewModel
            {
                UpcomingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm Attending",
                Attendances = attendences
            };

            return View("Gigs", viewModel);
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Heading = "Create a Gig",
                Genres = _genreRepository.GetGenres()
            };
            return View("GigForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _genreRepository.GetGenres();
                return View("GigForm", viewModel);
            }

            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            var gig = _gigsRepository.GetGigByGigId(id);

            var viewModel = new GigDetailViewModel
            {
                Gig = gig,
                IsUserAuthenticated = User.Identity.IsAuthenticated,
                UserId = User.Identity.GetUserId()
            };

            return View(viewModel);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            //var gig = _context.Gigs
            //    .Single(g => g.Id == id && g.ArtistId == userId);

            var gig = _gigsRepository.GetGigByGigId(id);

            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            var viewModel = new GigFormViewModel
            {
                Heading = "Edit Gig",
                Genres = _genreRepository.GetGenres(),
                Genre = gig.GenreId,
                Venue = gig.Venue,
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Id = gig.Id
            };
            return View("GigForm", viewModel);
        }

        [Authorize]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _genreRepository.GetGenres();
                return View("GigForm", viewModel);
            }

            var gig = _gigsRepository.GetGigByGigId(viewModel.Id);

            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            gig.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.Genre);

            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }
    }
}