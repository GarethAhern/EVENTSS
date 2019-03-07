using EventsAndInvitesLibrary;
using EventsAndInvitesLogic.Models;
using System;
using System.Data;
using System.Windows.Forms;

namespace EventsAndInvitesLogic
{
    public static class ResponseLogic
    {

        public static bool CheckCSVData(DataTable csvDT, ref DataTable ResponsesListDT, int eventId)
        {
            foreach (DataRow row in csvDT.Rows)
            {
                string emailAddress = row[0].ToString();

                //use email address and EventId to get ClientID
                int ClientId = GlobalConfig.Connection.GetClientID(eventId, emailAddress);
                //Check ClientId is valid
                if (ClientId == 0)
                {
                    MessageBox.Show($"Could not find client record with email address of { emailAddress }", "No Data Loaded", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //use ClientId and EventId to get InviteID
                int InviteId = GlobalConfig.Connection.GetInviteId(eventId, ClientId);
                //Check InviteId is valid
                if (InviteId == 0)
                {
                    MessageBox.Show($"Invalid Attending value of {row[1].ToString() } for { emailAddress }", "No Data Loaded", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check if attending & number of reservations are valid 
                bool ClientAttending;
                int PlacesReserved;
                if (!Boolean.TryParse(row[1].ToString(), out ClientAttending))
                {
                    MessageBox.Show($"Invalid Attending value of \"{row[1].ToString() }\" for { emailAddress }", "No Data Loaded", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (!Int32.TryParse(row[2].ToString(), out PlacesReserved))
                {
                    //If they are not attending then it doesn't matter if number of reservations is blank => invalid
                    if (ClientAttending)
                    {
                        MessageBox.Show($"Invalid number of places reserved of \"{row[2].ToString() }\" for { emailAddress }", "No Data Loaded", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

                //// Change email to InviteId and save in new DataTable
                DataRow r = csvDT.NewRow();
                r.ItemArray = row.ItemArray;
                r[0] = InviteId;
                ResponsesListDT.Rows.Add(r.ItemArray);
                //}
            }

            return true;
        }

        public static void LoadBasicReposnses(DataTable dt)
        {
            foreach (DataRow response in dt.Rows)
            {
                InviteModel invite = new InviteModel();

                invite.Id = Convert.ToInt32(response[0].ToString());
                invite.ClientAttending = Convert.ToBoolean(response[1].ToString());
                if (invite.ClientAttending == true)
                {
                    invite.PlacesReserved = Convert.ToInt32(response[2].ToString());
                }

                GlobalConfig.Connection.UpSertInviteInfo(invite);
            }
        }
        public static void LoadCustomResponses(DataTable dt)
        {
            if (dt.Columns.Count > 3)//The frist three columns are basic responses
            {
                foreach (DataRow response in dt.Rows)
                {
                    int InviteId = Convert.ToInt32(response[0].ToString());
                    GlobalConfig.Connection.DeleteInvitationQuestionResponses_ByInviteId(InviteId);

                    for (int i = 3; i < dt.Columns.Count; i++)
                    {
                        GlobalConfig.Connection.CreateInvitationResponse(InviteId, i, response[i].ToString());

                    }

                }
            }
        }
    }
}
