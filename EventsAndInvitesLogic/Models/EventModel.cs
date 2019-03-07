using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsAndInvitesLogic.Models
{
    /// <summary>
    /// Represents one event
    /// </summary>
    public class EventModel
    {
        /// <summary>
        /// Unique reference identifier
        /// </summary>
        public int Id { get; set; }

        [System.ComponentModel.DisplayName("Event Name")]
        /// <summary>
        /// Name given to the event
        /// </summary>
        /// 
        public string EventName { get; set; }

        [System.ComponentModel.DisplayName("Event Date")]
        /// <summary>
        /// The date the event starts
        /// </summary>
        public DateTime EventDate { get; set; }

        [System.ComponentModel.DisplayName("Event Venue")]
        /// <summary>
        /// A short one line venue name
        /// </summary>
       

        public int EventLocationId { get; set; }
        /// <summary>
        /// Address of venue, same as if you were sending a letter
        /// </summary>
        public LocationModel EventLocation { get; set; }


        [System.ComponentModel.DisplayName("Event Desc")]
        /// <summary>
        /// Long string describing the event
        /// </summary>
        public string EventDescription { get; set; }
        
        /// <summary>
        /// The time the event starts
        /// </summary>
        public TimeSpan StartTime { get; set; }

        /// <summary>
        /// The time the event ends
        /// </summary>
        public TimeSpan EndTime { get; set; }


        [System.ComponentModel.DisplayName("Event Type")]
        /// <summary>
        /// The ID of Event Type
        /// </summary>
        public List<EventTypeModel> EventType { get; set; }

        [System.ComponentModel.DisplayName("Event Owner")]
        /// <summary>
        /// The ID of the Event Owner
        /// </summary>
        public int MaxNumberOfGuests { get; set; }
        public decimal EstimatedCost { get; set; }

        public List<EventOwnerModel> EventOwners { get; set; }

        public string ListBoxDisplay
        {
            get
            {
                return $"{ EventName } - { EventLocation.Venue.VenueName } - {EventDate.ToShortDateString() }";
            }
        }


        public EventModel()
        { }

        public EventModel(string eventName, string eventDate, string eventVenue, string venueFullAddress, string eventDescription, EventTypeModel eventType)
        {
            EventName = eventName;

            //EventDate = eventDate;
            DateTime eventDateValue = new DateTime(1900, 1, 1);
            DateTime.TryParse(eventDate, out eventDateValue);
            EventDate = eventDateValue;


            EventDescription = eventDescription;

        }
    }
}
