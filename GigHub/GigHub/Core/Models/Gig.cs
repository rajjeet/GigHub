using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GigHub.Core.Models
{
    public class Gig
    {
        public int Id { get; set; }

        public ApplicationUser Artist { get; set; }

        public string ArtistId { get; set; }

        public DateTime DateTime { get; set; }

        public string Venue { get; set; }
        
        public Genre Genre { get; set; }

        public byte GenreId { get; set; }

        public bool IsCancelled { get; private set; }

        public ICollection<Attendance> Attendances { get; private set; }

        public Gig()
        {
            Attendances = new Collection<Attendance>();
        }

        public void Cancel()
        {
            IsCancelled = true;

            foreach (var attendance in Attendances.Select(a => a.Attendee))
            {
                attendance.Notify(Notification.GigCancelled(this));
            }
        }
        
        public void Modify(DateTime dateTime, string venue, byte genre)
        {

            var notification = Notification.GigUpdated(this, DateTime, Venue);

            DateTime = dateTime;            
            Venue = venue;
            GenreId = genre;
          
            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }

        }
    }

}