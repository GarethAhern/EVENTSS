using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsAndInvitesLogic.Models
{
    public class EventTypeModel
    {
        /// <summary>
        /// The unique identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Description of the category of the event
        /// e.g Rugby, Family Event, etc
        /// </summary>
        public string EventType { get; set; }

        public EventTypeModel()
        { }

        public EventTypeModel(string eventType)
        {
            EventType = eventType;
        }
    }
}
