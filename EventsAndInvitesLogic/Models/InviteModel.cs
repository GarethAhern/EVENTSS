using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsAndInvitesLogic.Models
{
    public class InviteModel
    {
        /// <summary>
        /// Unique reference identifier
        /// </summary>
        public int Id { get; set; }
        public ClientModel Client { get; set; }
        public int ClientId { get; set; }
        public EventModel Event { get; set; }
        public int EventId { get; set; }

        /// <summary>
        /// If False then member is "Proposed" to be invited
        /// If True then member has been approved to be invited & may have now been invited (depending on other values properties)
        /// </summary>
        public bool InviteApproved { get; set; }
        public DateTime? EmailSentDate { get; set; }
        /// <summary>
        /// 1,2,3... => yes they are coming and they want this many tickets
        /// 0 => no they are not coming
        /// Null => don't know - check EmailSentDate to see if they have been invited or just shortlisted
        /// </summary>

        public bool? ClientAttending { get; set; }
        public int PlacesReserved { get; set; }
    }
}
