using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
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

            if (_context.Attendances.Any(a => a.AttendeeId == _attendeeId && a.GigId == dto.GigId))
                return BadRequest("User is already registered to this gig.");

            var attendance = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = _attendeeId

            };
            _context.Attendances.Add(attendance);
            _context.SaveChanges();

            return Ok("User sucessfully registered to this gig!");
        }

        [HttpDelete]
        public IHttpActionResult Unattend(int id)
        {
            var userId = User.Identity.GetUserId();
            var attendance = _context.Attendances
                .SingleOrDefault(a => a.GigId == id && a.AttendeeId == userId);

            if (attendance == null)
                return NotFound();

            _context.Entry(attendance).State = EntityState.Deleted;
            _context.SaveChanges();

            return Ok(id);
        }
    }
}
