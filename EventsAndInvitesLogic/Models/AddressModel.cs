using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsAndInvitesLogic.Models
{
    public class AddressModel
    {
        public int Id { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string PostCode { get; set; }

        public string Area { get; set; }

        public string FullMultiLineAddress
        {
            get
            {
                return $"{ AddressLine1 + "," + (AddressLine2 != null ? Environment.NewLine + AddressLine2 +"," + (AddressLine3 != null ? Environment.NewLine + AddressLine3 + "," + (AddressLine4 != null ? Environment.NewLine + AddressLine4 + "," : "") : "") : "") + Environment.NewLine + Area + "," + Environment.NewLine + PostCode }";
            }
        }
    }
}
