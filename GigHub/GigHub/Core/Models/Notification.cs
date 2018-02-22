using System;

namespace GigHub.Core.Models
{
    public class Notification
    {
        public int Id { get; private set; }
        public NotificationType Type { get; private set; }
        public DateTime DateTime { get; private set; }
        public DateTime? OriginalDateTime { get; private set; }
        public string OriginalVenue { get; private set; }

        public Gig Gig { get; private set; }

        protected Notification() { }

        private Notification(NotificationType notificationType, Gig gig)
        {
            if (gig == null)
                throw new ArgumentNullException(nameof(gig));

            DateTime = DateTime.Now;
            Type = notificationType;
            Gig = gig;
        }

        public static Notification GigUpdated(Gig gig, DateTime originalDateTime, string originalVenue)
        {
            return new Notification(NotificationType.GigUpdated, gig)
            {
                OriginalDateTime = originalDateTime,
                OriginalVenue = originalVenue
            };
        }

        public static Notification GigCreated(Gig gig)
        {
            return new Notification(NotificationType.GigCreated, gig);
        }

        public static Notification GigCancelled(Gig gig)
        {
            return new Notification(NotificationType.GigCancelled, gig);
        }

    }
}