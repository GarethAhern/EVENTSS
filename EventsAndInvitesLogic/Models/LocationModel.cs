using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsAndInvitesLogic.Models
{
    public class LocationModel
    {
        public int Id { get; set; }

        public VenueModel Venue { get; set; }

        public AddressModel VenueAddress { get; set; }
    }
}
