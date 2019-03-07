using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsAndInvitesLogic.Models
{
    /// <summary>
    /// Staff are BNZ employees that can be event owners.
    /// Generally going to be team leaders.
    /// </summary>
    public class StaffModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public bool Active { get; set; }
    }
}
