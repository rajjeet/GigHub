using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public FollowingsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (_context.Followings.Any(f => f.FolloweeId == dto.FolloweeId && f.FollowerId == userId))
                return BadRequest("Following already exists");

            var following = new Following
            {
                FollowerId = userId,
                FolloweeId = dto.FolloweeId
            };

            _context.Followings.Add(following);
            _context.SaveChanges();

            return Ok("Following created!");
        }

        [HttpDelete]
        public IHttpActionResult Unfollow(string id)
        {
            var userId = User.Identity.GetUserId();
            var following = _context.Followings
                .SingleOrDefault(f => f.FolloweeId == id && f.FollowerId == userId);

            if (following == null)
                return NotFound();

            _context.Entry(following).State = EntityState.Deleted;
            _context.SaveChanges();

            return Ok(id);
        }
    }
}
