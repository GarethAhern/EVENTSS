using EventsAndInvitesLogic.Models;
using System.Collections.Generic;
using System.Linq;

namespace EventsAndInvitesLogic
{
   public static class GetInviteListsLogic
    {
        //Returns Lists of clients
        public static List<InviteModel> GetProposedInvitation(List<InviteModel> guestList)
        {
            return guestList.Where(x => x.InviteApproved == false && x.EmailSentDate == null && x.ClientAttending == null).ToList();
        }
        public static List<InviteModel> GetApprovedInvitation(List<InviteModel> guestList)
        {
            return guestList.Where(x => x.InviteApproved == true && x.EmailSentDate == null && x.ClientAttending == null).ToList();
        }
        public static List<InviteModel> GetNotRespondedInvitations(List<InviteModel> guestList)
        {
            return guestList.Where(x => (x.EmailSentDate != null) && (x.ClientAttending == null)).ToList();
        }
        public static List<InviteModel> GetAcceptedInvitations(List<InviteModel> guestList)
        {
            return guestList.Where(x => (x.EmailSentDate != null) && (x.ClientAttending == true)).ToList();
        }
       public static List<InviteModel> GetNotAttendingClients(List<InviteModel> guestList)
        {
            return guestList.Where(x => (x.EmailSentDate != null) && (x.ClientAttending == false)).ToList();
        }

        //Calculates how many places have been reserved

        public static int CalculateNumberOfProposedInvitations(List<InviteModel> guestList)
        {
            int output = 0;

            List<InviteModel> accepted = GetInviteListsLogic.GetProposedInvitation(guestList);
            foreach (InviteModel invite in accepted)
            {
                output += invite.PlacesReserved;
            }
            return output;
        }

        public static int CalculateNumberOfApprovedInvitations(List<InviteModel> guestList)
        {
            int output = 0;

            List<InviteModel> accepted = GetInviteListsLogic.GetApprovedInvitation(guestList);
            foreach (InviteModel invite in accepted)
            {
                output += invite.PlacesReserved;
            }
            return output;
        }

        public static int CalculateNumberOfInvited(List<InviteModel> guestList)
        {
            int output = 0;

            List<InviteModel> invited = GetInviteListsLogic.GetNotRespondedInvitations(guestList);
            foreach (InviteModel invite in invited)
            {
                output += invite.PlacesReserved;
            }
            return output;
        }
        public static int CalculateNumberOfAcceptedInvitations(List<InviteModel> guestList)
        {
            int output = 0;

            List<InviteModel> accepted = GetInviteListsLogic.GetAcceptedInvitations(guestList);
            foreach (InviteModel invite in accepted)
            {
                output += invite.PlacesReserved;
            }
            return output;
        }
    }
}
