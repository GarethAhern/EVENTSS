using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsAndInvitesLogic.Models
{
    /// <summary>
    /// Will be used as a list to display all the staff invloved with an event and how much they contribute to costs.
    /// </summary>
    public class EventOwnerModel
    {
        /// <summary>
        /// Unique reference identifier
        /// </summary>
        public int Id { get; set; }

        public int StaffId { get; set; }
        public StaffModel Staff { get; set; }

        public int CostPercentage { get; set; }
    }
}
