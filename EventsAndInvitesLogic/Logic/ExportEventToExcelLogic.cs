using EventsAndInvitesLogic.Models;
using System;
using System.Collections.Generic;
using EventsAndInvitesLibrary;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data;
using System.Windows.Forms;

namespace EventsAndInvitesLogic
{
    public static class ExportEventToExcelLogic
    {
        public static void ExportEventData(EventModel model, DataGridView invitedDGV, DataGridView acceptedDGV)
        {
            //Create the spreadsheet
            Excel.Application xla = new Excel.Application();

            Excel.Workbook wb = xla.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
            Excel.Worksheet ws = (Excel.Worksheet)xla.ActiveSheet;

            xla.Visible = true;

            //Populate it
            int inviteSumRow = BasicDataToExcel(ws, model, invitedDGV, acceptedDGV);
            CostingsDataToExcel(wb, model, inviteSumRow);

            //So it looks pretty select the tab with the basic data
            ws.Select();
        }
        private static int BasicDataToExcel(Excel.Worksheet ws,EventModel model, DataGridView invitedDGV, DataGridView acceptedDGV)
        {
            ws.Select();

            ws.Name = "Invites";

            ws.Cells[1, 1] = "Event Name :";
            ws.Cells[1, 2] = model.EventName;

            ws.Cells[2, 1] = model.EventDescription;
            ws.get_Range("A2", "M2").Merge();
            ws.get_Range("A2").WrapText = true;
            ws.get_Range("A2").EntireRow.RowHeight = Math.Ceiling((double)model.EventDescription.Length / 150) * 22;
            ws.get_Range("A2:M2").VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
            ws.get_Range("A2:M2").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;

            ws.Cells[3, 1] = "Event Date :";
            ws.Cells[3, 2] = model.EventDate;
            ws.Cells[4, 1] = "Start Time :";
            ws.Cells[4, 2] = model.StartTime.ToString();
            ws.Cells[5, 1] = "End Time :";
            ws.Cells[5, 2] = model.EndTime.ToString();
            ws.Cells[6, 1] = "Venue :";
            ws.Cells[6, 2] = model.EventLocation.Venue.VenueName;

            ws.Cells[7, 1] = "Address :";
            ws.Cells[8, 2] = model.EventLocation.VenueAddress.AddressLine1;

            int i = 1;
            ws.Cells[8 + i, 2] = AddressLogic.ReturnAddressLineIfNotNull(model.EventLocation.VenueAddress.AddressLine2, ref i);
            ws.Cells[8 + i, 2] = AddressLogic.ReturnAddressLineIfNotNull(model.EventLocation.VenueAddress.AddressLine3, ref i);
            ws.Cells[8 + i, 2] = AddressLogic.ReturnAddressLineIfNotNull(model.EventLocation.VenueAddress.AddressLine4, ref i);
            ws.Cells[8 + i, 2] = AddressLogic.ReturnAddressLineIfNotNull(model.EventLocation.VenueAddress.Area, ref i);
            ws.Cells[8 + i, 2] = model.EventLocation.VenueAddress.PostCode;
            ws.Cells[8 + i, 2].NumberFormat = "@";

            int colHeaderRow = 8 + i + 3;

            //Add table headers going cell by cell.

            ws.Cells[colHeaderRow, 1] = "Title";
            ws.Cells[colHeaderRow, 1].Font.Bold = true;
            ws.Cells[colHeaderRow, 2] = "First Name";
            ws.Cells[colHeaderRow, 2].Font.Bold = true;
            ws.Cells[colHeaderRow, 3] = "Last Name";
            ws.Cells[colHeaderRow, 3].Font.Bold = true;
            ws.Cells[colHeaderRow, 4] = "Business Name";
            ws.Cells[colHeaderRow, 4].Font.Bold = true;
            ws.Cells[colHeaderRow, 5] = "Email Address";
            ws.Cells[colHeaderRow, 5].Font.Bold = true;
            ws.Cells[colHeaderRow, 6] = "Address";
            ws.Cells[colHeaderRow, 6].Font.Bold = true;
            ws.Cells[colHeaderRow, 7] = "Cell Phone";
            ws.Cells[colHeaderRow, 7].Font.Bold = true;
            ws.Cells[colHeaderRow, 8] = "Work Phone";
            ws.Cells[colHeaderRow, 8].Font.Bold = true;
            ws.Cells[colHeaderRow, 9] = "No. Invites";
            ws.Cells[colHeaderRow, 9].Font.Bold = true;
            ws.Cells[colHeaderRow, 10] = "Invite Confirmed?";
            ws.Cells[colHeaderRow, 10].Font.Bold = true;

            //Now load Custom QuestionS
            bool CustomQs = (GlobalConfig.Connection.GetCustomQuestionCount(model.Id) > 0);
            if (CustomQs)
            {
                int Qcol = 1;
                List<string> CustomQuestions = GlobalConfig.Connection.GetCustomQuestions(model.Id);
                foreach (string customQuestion in CustomQuestions)
                {
                    ws.Cells[colHeaderRow, 10 + Qcol] = customQuestion;
                    ws.Cells[colHeaderRow, 10 + Qcol].Font.Bold = true;
                    Qcol++;
                }
            }

            //These should be text to make sure any leading 0s are kept
            ws.get_Range("G:H").NumberFormat = "@";

            int TotalInvites = 0;
            int j = colHeaderRow;

            foreach (DataGridViewRow item in acceptedDGV.Rows)
            {
                InviteModel invite = (InviteModel)item.Tag;
                j++;

                ws.Cells[j, 1] = invite.Client.Title;
                ws.Cells[j, 2] = invite.Client.FirstName;
                ws.Cells[j, 3] = invite.Client.LastName;
                ws.Cells[j, 4] = invite.Client.BusinessName;
                ws.Cells[j, 5] = invite.Client.EmailAddress;

                if (invite.Client.HomeAddress != null)
                {
                    ws.Cells[j, 6] = invite.Client.HomeAddress.FullMultiLineAddress;
                }

                ws.Cells[j, 7] = invite.Client.CellPhone;
                ws.Cells[j, 8] = invite.Client.WorkPhone;
                ws.Cells[j, 9] = invite.PlacesReserved;
                TotalInvites += invite.PlacesReserved;

                ws.Cells[j, 10] = true;

                if (CustomQs)
                {
                    DataTable dt = GlobalConfig.Connection.GetClientsInvitationAnswers(invite.ClientId, invite.EventId);

                    foreach (DataRow row in dt.Rows)
                    {
                        if (row[0].ToString().Length > 0)
                        {
                            ws.Cells[j, 10 + Convert.ToInt32(row["ColumnNumber"].ToString()) - 2] = row["Response"].ToString();
                        }

                    }
                }
            }

            foreach (DataGridViewRow item in invitedDGV.Rows)
            {
                InviteModel invite = (InviteModel)item.Tag;
                j++;

                ws.Cells[j, 1] = invite.Client.Title;
                ws.Cells[j, 2] = invite.Client.FirstName;
                ws.Cells[j, 3] = invite.Client.LastName;
                ws.Cells[j, 4] = invite.Client.BusinessName;
                ws.Cells[j, 5] = invite.Client.EmailAddress;

                if (invite.Client.HomeAddress != null)
                {
                    ws.Cells[j, 6] = invite.Client.HomeAddress.FullMultiLineAddress;
                }

                ws.Cells[j, 7] = invite.Client.CellPhone;
                ws.Cells[j, 8] = invite.Client.WorkPhone;
                ws.Cells[j, 9] = invite.PlacesReserved;
                TotalInvites += invite.PlacesReserved;

                ws.Cells[j, 10] = false;

            }

            ws.Cells[j + 1, 8] = "Total Invites";
            ws.Cells[j + 1, 9] = "=sum(I" + (8 + i + 4) + ":I" + j + ")";
            ws.Cells[j + 1, 9].Font.Bold = true;

            ws.get_Range("I:I").Columns.NumberFormat = "0";
            ws.Cells[j + 1, 10] = "(Max = " + model.MaxNumberOfGuests + ")";

            ws.Cells[j + 2, 8] = "Confirmed Places";
            ws.Cells[j + 2, 9] = "=sumif(J" + (8 + i + 4) + ": J" + j + ", TRUE, I" + (8 + i + 4) + ": I" + j + ")";
            ws.Cells[j + 2, 9].Font.Bold = true;

            int inviteSumRow = j + 2;

            ws.get_Range("A:Z").Columns.AutoFit();

            ws.Cells[7, 2] = model.EventLocation.VenueAddress.FullMultiLineAddress;
            ws.Cells[7, 2].WrapText = false;

            return inviteSumRow;
        }
        private static void CostingsDataToExcel(Excel.Workbook wb, EventModel model, int inviteSumRow)
        {

            //Excel.Sheets worksheets = wb.Worksheets;
            //worksheets[1]
            Excel.Worksheet ws = wb.Worksheets.Add(Type.Missing, wb.Worksheets[wb.Worksheets.Count], Type.Missing, Type.Missing);
            ws.Name = "Costings";

            ws.Cells[1, 1] = "Estimated Cost";
            ws.Cells[1, 1].Font.Bold = true;
            ws.Cells[1, 2] = model.EstimatedCost;

            ws.Cells[4, 1] = "By Event Owners";
            ws.Cells[4, 1].Font.Bold = true;

            ws.Cells[5, 1] = "Staff Member";
            ws.Cells[5, 1].Font.Bold = true;
            ws.Cells[5, 2] = "Cost %";
            ws.Cells[5, 2].Font.Bold = true;
            ws.Cells[5, 3] = "Cost $";
            ws.Cells[5, 3].Font.Bold = true;

            int j = 1;
            foreach (EventOwnerModel owner in model.EventOwners)
            {
                ws.Cells[5 + j, 1] = owner.Staff.FullName;
                ws.Cells[5 + j, 2] = (double)owner.CostPercentage / 100;
                ws.Cells[5 + j, 2].NumberFormat = "###,##%";
                ws.Cells[5 + j, 3] = "=R1C2*RC[-1]";
                ws.Cells[5 + j, 3].Style.NumberFormat = "\"$\"#,##0";
                j++;
            }

            int k = 5 + j;

            ws.Cells[k + 2, 1] = "By Guests";
            ws.Cells[k + 2, 1].Font.Bold = true;

            ws.Cells[k + 3, 1] = "No. Attendees";
            ws.Cells[k + 3, 1].Font.Bold = true;
            ws.Cells[k + 3, 2] = "=Invites!I" + inviteSumRow;
            ws.Cells[k + 3, 2].NumberFormat = "@";

            ws.Cells[k + 4, 1] = "Cost Per Attendant";
            ws.Cells[k + 4, 1].Font.Bold = true;
            ws.Cells[k + 4, 2] = "= R1C2 / R[-1]C";

            ws.get_Range("A:C").Columns.AutoFit();
        }

        public static void ExportClientList(DataGridView invitationsDGV,string listDescription)
        {
            Excel.Application xla = new Excel.Application();

            Excel.Workbook wb = xla.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
            Excel.Worksheet ws = (Excel.Worksheet)xla.ActiveSheet;

            int i = 3;
            //Add table headers going cell by cell.

            ws.Cells[2, 1] = "Title";
            ws.Cells[2, 2] = "First Name";
            ws.Cells[2, 3] = "Last Name";
            ws.Cells[2, 4] = "Email Address";
            ws.Cells[2, 5] = "Business Name";
            ws.Cells[2, 6] = "Cell Phone";
            ws.Cells[2, 7] = "Work Phone";

            //These should be text to make sure any leading 0s are kept
            ws.get_Range("F:G").NumberFormat = "@";

            //Format A1:D1 as bold, vertical alignment = center.
            ws.get_Range("A1", "G2").Font.Bold = true;
            ws.get_Range("A1", "G2").VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

            xla.Visible = true;

            foreach (DataGridViewRow item in invitationsDGV.Rows)
            {
                InviteModel invite = (InviteModel)item.Tag;

                ws.Cells[i, 1] = invite.Client.Title;
                ws.Cells[i, 2] = invite.Client.FirstName;
                ws.Cells[i, 3] = invite.Client.LastName;
                ws.Cells[i, 4] = invite.Client.EmailAddress;
                ws.Cells[i, 5] = invite.Client.BusinessName;
                ws.Cells[i, 6] = invite.Client.CellPhone;
                ws.Cells[i, 7] = invite.Client.WorkPhone;
                i++;
            }

            ws.get_Range("A:G").Columns.AutoFit();
            ws.Cells[1, 1] = listDescription;
        }
    }
}
