using AutoMapper;
using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    public class NotificationsController : ApiController
    {
        private ApplicationDbContext _context;

        
        public NotificationsController()
        {
            _context = new ApplicationDbContext();
        }


        [Authorize]
        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .Select(un => un.Notification)
                .Include(n => n.Gig.Artist)
                .ToList();

            return notifications.Select(Mapper.Map<Notification, NotificationDto>);
        }

        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {
            var userId = User.Identity.GetUserId();
            var unreadUserNotifications = _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .ToList();


            unreadUserNotifications.ForEach(un => un.Read());

            _context.SaveChanges();

            return Ok();
        }

    }
}
