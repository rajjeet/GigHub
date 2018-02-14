﻿using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    public class GigsController : ApiController
    {
        private ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userId);
            gig.IsCancelled = true;

            _context.SaveChanges();

            return Ok("Gig cancelled.");
        }
    }
}
