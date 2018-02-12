using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private readonly ApplicationDbContext _context;
        private string _attendeeId;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            _attendeeId = User.Identity.GetUserId();

            if (_context.Attendences.Any(a => a.AttendeeId == _attendeeId && a.GigId == dto.GigId))
                return BadRequest("User is already registered to this gig.");

            var attendance = new Attendence
            {
                GigId = dto.GigId,
                AttendeeId = _attendeeId

            };
            _context.Attendences.Add(attendance);
            _context.SaveChanges();

            return Ok("User sucessfully registered to this gig!");
        }

    }
}
