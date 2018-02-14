using GigHub.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    public class GigsController : ApiController
    {
        private readonly ApplicationDbContext _context;

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

            if (gig.IsCancelled)
                return NotFound();

            gig.IsCancelled = true;

            var notification = new Notification
            {
                DateTime = DateTime.Now,
                Type = NotificationType.GigCancelled,
                Gig = gig
            };


            var attendances = _context.Attendances
                .Where(a => a.GigId == gig.Id)
                .Select(a => a.Attendee)
                .ToList();

            foreach (var attendance in attendances)
            {
                var userNotification = new UserNotification
                {
                    User = attendance,
                    Notification = notification
                };

                _context.UserNotifications.Add(userNotification);
            }

            _context.SaveChanges();

            return Ok("Gig cancelled.");
        }
    }
}
