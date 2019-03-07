using EventsAndInvitesLibrary;
using EventsAndInvitesLogic;
using EventsAndInvitesLogic.Models;
using EventsAndInvitesUI.Interfaces;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventsAndInvitesUI
{
    public partial class LoadResponses : Form
    {
        int eventId;
        DataTable customQuestionsDT = new DataTable();
        private IInvitationRequester callingForm;

        public LoadResponses(int EventId, IInvitationRequester caller)
        {
            eventId = EventId;
            callingForm = caller;

            InitializeComponent();
            PopulateDGV();

        }
        public void PopulateDGV()
        {
            List<string> existingQuestions = GlobalConfig.Connection.GetInvitationQuestions_ByEventId(eventId);

            //This will hold the three columns that all loads need
            //email address / Accept or decline invite response / number of places reserved
            PopulateDefaultColumnsDGV();

            customQuestionsDT = new DataTable();
            PopulateDGVColumns(customQuestionsDT);
 

            int colNum = 4;
            foreach (string Q in existingQuestions)
            {
                customQuestionsDT.Rows.Add(colNum.ToString(), Q);
                colNum++;
            }

            customColumnHeadersDGV.DataSource = customQuestionsDT;
            customColumnHeadersDGV.Columns[0].ReadOnly = true;
        }

        private void PopulateDGVColumns(DataTable dt)
        {
            dt.Columns.Add("Old Column Order", typeof(string));
            dt.Columns.Add("Invitation Question", typeof(string));
        }
        private void PopulateDefaultColumnsDGV()
        {
            DataTable defaultColumnsDT = new DataTable();
            PopulateDGVColumns(defaultColumnsDT);

            defaultColumnsDT.Rows.Add(1, "Email Address");
            defaultColumnsDT.Rows.Add(2, "Accept/Reject Invite");
            defaultColumnsDT.Rows.Add(3, "Places Reserved");

            defaultColumnHeadersDGV.DataSource = defaultColumnsDT;
        }
        private void moveQuestionUpButton_Click(object sender, EventArgs e)
        {
            int rowIndex;

            if (customColumnHeadersDGV.SelectedRows.Count == 1)
            {
                rowIndex = customColumnHeadersDGV.SelectedRows[0].Index;
                if (rowIndex > 0 && rowIndex < customColumnHeadersDGV.Rows.Count - 1)
                {
                    DataRow row = customQuestionsDT.NewRow();

                    row[0] = customColumnHeadersDGV.Rows[rowIndex].Cells[0].Value.ToString();
                    row[1] = customColumnHeadersDGV.Rows[rowIndex].Cells[1].Value.ToString();
                    customQuestionsDT.Rows.RemoveAt(rowIndex);
                    customQuestionsDT.Rows.InsertAt(row, rowIndex - 1);

                    customColumnHeadersDGV.ClearSelection();
                    customColumnHeadersDGV.Rows[rowIndex - 1].Selected = true;
                }
            }
        }

        private void sendInviteButton_Click(object sender, EventArgs e)
        {
            int rowIndex;

            if (customColumnHeadersDGV.SelectedRows.Count == 1)
            {
                rowIndex = customColumnHeadersDGV.SelectedRows[0].Index;
                if (rowIndex < customColumnHeadersDGV.Rows.Count - 2)
                {
                    DataRow row = customQuestionsDT.NewRow();

                    row[0] = customColumnHeadersDGV.Rows[rowIndex].Cells[0].Value.ToString();
                    row[1] = customColumnHeadersDGV.Rows[rowIndex].Cells[1].Value.ToString();
                    customQuestionsDT.Rows.RemoveAt(rowIndex);
                    customQuestionsDT.Rows.InsertAt(row, rowIndex + 1);

                    customColumnHeadersDGV.ClearSelection();
                    customColumnHeadersDGV.Rows[rowIndex + 1].Selected = true;
                }
            }
        }


        private void saveChangesButton_Click(object sender, EventArgs e)
        {
            RefreshInvitationQuestionDB();
            PopulateDGV();
        }

        private void RefreshInvitationQuestionDB()
        {
            //check if data has been loaded
            //if it has warn them that this may error the results
            //ask if they want to wipe old questions?

            List<string> list = (from row in customQuestionsDT.AsEnumerable()
                                 select row.Field<string>(1)).ToList<string>();


            //I don't want to risk deleting all the questions and then messing up the import
            //and I don't want to have to go row by row updating and inserting - so:

            //Insert any InvitationQuestions with EventId = 0 (I don't expect there to be any)
            //Then insert all InvitationQuestions with EventId = 0
            //Delete current InvitationQuestions with old EventId
            //Update InvitationQuestions EventId from = 0 to 


            //Delete any EventId = 0 rows
            GlobalConfig.Connection.DeleteInvitationQuestions(0);


            //Import new questions with EventId = 0
            int ColNum = 3;
            foreach (string item in list)
            {
                GlobalConfig.Connection.CreateInvitationQuestion(ColNum, item);
                ColNum++;
            }

            //If this is successfull we now delete all old values
            GlobalConfig.Connection.DeleteInvitationQuestions(eventId);

            //update newly inserted 0s with correct EventId
            GlobalConfig.Connection.UpdateInvitationQuestions(eventId);
        }

        private void loadDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "CSV|*.csv";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                DataTable csvDT = GetDataTabletFromCSVFile(ofd.FileName);
                int expectedColumnCount = GlobalConfig.Connection.GetCustomQuestionCount(eventId) + 3;

                if (csvDT.Columns.Count < expectedColumnCount)
                {
                    MessageBox.Show($"You don't have enough responses I can only find {csvDT.Columns.Count} columns of data, but i am expecting { expectedColumnCount }");
                    return;
                }
                if (csvDT.Columns.Count > expectedColumnCount)
                {
                    MessageBox.Show("You don't have too many responses, maybe you have blank columns to right of data");
                    return;
                }
                DataTable ResponsesListDT = csvDT.Clone();


                //check csvDT rows
                if (ResponseLogic.CheckCSVData(csvDT, ref ResponsesListDT, eventId) == false)
                {
                    //If there are any errors with the data it will exit outbefore trying to load any data
                    //Error message is show by CheckCSVData
                    return;
                }


                DialogResult dr = MessageBox.Show("This will replace the responses for all clients on the spreadsheet \nThis will NOT affect responses for anyone not on the spreadsheet", "Press Yes to Continue", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    //Now that all the data has been checked
                    //Loop through the DataTable and load the data
                    ResponseLogic.LoadBasicReposnses(ResponsesListDT);
                    ResponseLogic.LoadCustomResponses(ResponsesListDT);
                    callingForm.RefreshAllInvitesListView();
                    MessageBox.Show("Data processed", "Load Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No data processed", "Load aborted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private static DataTable GetDataTabletFromCSVFile(string csv_file_path)
        {
            DataTable csvData = new DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFields = csvReader.ReadFields();
                    foreach (string column in colFields)
                    {
                        DataColumn datecolumn = new DataColumn(column);
                        datecolumn.AllowDBNull = true;
                        csvData.Columns.Add(datecolumn);
                    }
                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        //Making empty value as null
                        for (int i = 0; i < fieldData.Length; i++)
                        {
                            if (fieldData[i] == "")
                            {
                                fieldData[i] = null;
                            }
                        }
                        csvData.Rows.Add(fieldData);
                    }
                }
            }
            catch
            {
                return null;
            }
            return csvData;
        }

        private void clearCurrentResponsesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("This will delete all custom responses for this event\nIt will NOT affect if client accepted/rejected the invitation", "Press Yes to Continue", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                GlobalConfig.Connection.DeleteInvitationQuestionResponses_ByEventId(eventId);
                MessageBox.Show("Custom Responses Deleted", "No Going Back Now", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
                MessageBox.Show("Data was not deleted", "Nothing Actioned", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
      

        private void clearInviteAcceptRejectStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("This will reset all responses for the event\ni.e move any Accepted & Rejected Clients to Invited (and delete custom responses)", "Press Yes to Continue", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                Random random = new Random();
                int A = random.Next(1, 10);
                int B = random.Next(1, 10);

                string additionResponse = Interaction.InputBox($"If you delete the responses they are gone forever \nWhat is { A } + { B } ?", "Input Reservation Number", string.Empty, -1, -1);
                int additionAnswer;
                if (int.TryParse(additionResponse, out additionAnswer))
                {
                    if (additionAnswer == A + B)
                    {
                        GlobalConfig.Connection.DeleteAttendingResponses(eventId);
                        GlobalConfig.Connection.DeleteInvitationQuestionResponses_ByEventId(eventId);
                        callingForm.RefreshAllInvitesListView();

                        MessageBox.Show("All Responses Deleted", "No Going Back Now", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                MessageBox.Show("Data was not deleted", "Nothing Actioned", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void defaultColumnHeadersDGV_SelectionChanged(object sender, EventArgs e)
        {
            defaultColumnHeadersDGV.ClearSelection();
        }
    }
}

