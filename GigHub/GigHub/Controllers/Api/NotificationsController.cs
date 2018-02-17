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

        [Authorize]
        [HttpPatch]
        public IHttpActionResult ReadNotifications()
        {
            var userId = User.Identity.GetUserId();
            
            var user = _context.Users
                .Include(u => u.UserNotifications)
                .Single(u => u.Id == userId);
                
            user.MarkUnreadNotificationsAsRead();

            _context.SaveChanges();
                        
            return Ok();
        }

    }
}
