using EventsAndInvitesLogic;
using EventsAndInvitesLogic.Models;
using EventsAndInvitesUI.Interfaces;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace EventsAndInvitesUI
{
    public partial class UpdateInviteStatusForm : Form
    {

        List<InviteModel> invites = new List<InviteModel>();

        private IInvitationRequester callingForm;

        //These allow the EventForm to set the enabled status of the buttons
        public bool rejectButtonEnabler;
        public bool removeButtonEnabler;
        public bool shortListButtonEnabler;
        public bool sendButtonEnabler;
        public bool acceptButtonEnabler;

        public UpdateInviteStatusForm(IInvitationRequester caller)
        {
            InitializeComponent();

            callingForm = caller;
        }

        public void PopulateDGVModel(List<InviteModel> models)
        {
            ColumnHeaderLogic.PopulateDGVColumns(invitationsDataGridView);

            foreach (InviteModel invite in models)
            {
                //If user is creating shortlist then places reserved will be 0
                if (invite.Id == 0)
                {
                    invite.PlacesReserved = 1;
                }
                invites.Add(invite);
            }

            PopulateInvitesDGV();
            SetButtonsState();
        }
    
        private void PopulateInvitesDGV()
        {
            invitationsDataGridView.CellValueChanged -= new DataGridViewCellEventHandler(invitesDataGridView_CellValueChanged);
            invitationsDataGridView.CurrentCellDirtyStateChanged -= new EventHandler(InviteLogic.invitesDataGridView_CurrentCellDirtyStateChanged);

            List<InviteModel> filtered = invites;

            //### Search Box ###
            string searchValue = searchClientsTextBox.Text.ToLower();
            if (searchValue.Length > 0 && searchClientsTextBox.ForeColor != SystemColors.GrayText)
            {
                filtered = filtered.SearchInvite(searchValue);
            }

            invitationsDataGridView.Rows.Clear();

            foreach (InviteModel invite in filtered)
            {
                InviteLogic.PopulateDGVRow(invite, invitationsDataGridView);
            }

            invitationsDataGridView.CellValueChanged += new DataGridViewCellEventHandler(invitesDataGridView_CellValueChanged);
            invitationsDataGridView.CurrentCellDirtyStateChanged += new EventHandler(InviteLogic.invitesDataGridView_CurrentCellDirtyStateChanged);
        }

        private void invitesDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            InviteLogic.invitesDataGridView_CellValueChanged(sender, e);
            callingForm.RefreshAllInvitesListView();
        }

        //### Enable / Disable the buttons along top
        private void SetButtonsState()
        {
            SetIndividualButtonState(rejectButtonEnabler, rejectedInviteButton);
            SetIndividualButtonState(removeButtonEnabler, removeInviteButton);
            SetIndividualButtonState(shortListButtonEnabler, shortlistButton);
            SetIndividualButtonState(sendButtonEnabler, sendInviteButton);
            SetIndividualButtonState(acceptButtonEnabler, acceptedInviteButton);
        }
        private void SetIndividualButtonState(bool buttonEnabled, Button btn)
        {
            if (buttonEnabled)
            {
                btn.Enabled = true;
            }
            else
            {
                btn.BackgroundImage = ButtonDisplayLogic.DarkenBitMap(new Bitmap(btn.BackgroundImage));
            }
        }
        //### Enable / Disable the buttons along top


        //Buttons along top have been pressed
        private void eventAcceptedClientButton_Click(object sender, EventArgs e)
        {
            if (invitationsDataGridView.Rows.Count == 0)
            {
                MessageBox.Show("You have not selected anyone....");
            }
            else
            {
                EventsAndInvitesLogic.InviteLogic.InvitesAccepted(invitationsDataGridView);
            }

            callingForm.RefreshAllInvitesListView();
        }
        private void sendEmailToClientButton_Click(object sender, EventArgs e)
        {
            if (invitationsDataGridView.Rows.Count == 0)
            {
                MessageBox.Show("You have not selected anyone....");
            }
            else
            {
                EventsAndInvitesLogic.InviteLogic.SendInvites(invitationsDataGridView);
            }

            callingForm.RefreshAllInvitesListView();
        }
        private void rejectionButton_Click(object sender, EventArgs e)
        {
            if (invitationsDataGridView.Rows.Count == 0)
            {
                MessageBox.Show("You have not selected anyone....");
            }
            else
            {
                EventsAndInvitesLogic.InviteLogic.InvitesRejected(invitationsDataGridView);
            }
            callingForm.RefreshAllInvitesListView();
        }
        private void removeInviteButton_Click(object sender, EventArgs e)
        {
            if (invitationsDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("You have not selected anyone....");
            }
            else
            {
                EventsAndInvitesLogic.InviteLogic.CancelInvites(invitationsDataGridView);
            }

            callingForm.RefreshAllInvitesListView();
        }
        private void shortlistButton_Click(object sender, EventArgs e)
        {
            if (invitationsDataGridView.Rows.Count == 0)
            {
                MessageBox.Show("You have not selected anyone....");
            }
            else
            {
                EventsAndInvitesLogic.InviteLogic.AddToProposedInvitationList(invitationsDataGridView);
            }

            callingForm.RefreshAllInvitesListView();
        }

        private void searchClientsTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || searchClientsTextBox.TextLength == 0)
            {
                PopulateInvitesDGV();
            }
        }

        private void exportToExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportEventToExcelLogic.ExportClientList(invitationsDataGridView, this.Text);
        }

        private void bulkUpdateNoPlacesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string invitesInput = Interaction.InputBox("Input number to change No. Invites for all selected items", "Input Reservation Number", string.Empty, -1, -1);

            int invites;
            if (int.TryParse(invitesInput, out invites))
            {
                if (invites == 0)
                {
                    MessageBox.Show("You can't have zero invites", "Nothing updated", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    foreach (DataGridViewRow row in invitationsDataGridView.SelectedRows)
                    {
                        //updating this value will trigger the event that unpacks the tag in the row, updates and writes the update to SQL
                        row.Cells[0].Value = invites;
                    }
                }
            }
        }

        private void invitationsDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (invitationsDataGridView.SelectedRows.Count > 0)
            {
                bulkUpdateToolStripMenuItem.Enabled = true;
            }
            else
            {
                bulkUpdateToolStripMenuItem.Enabled = false;
            }
        }

        private void searchClientsTextBox_Leave(object sender, EventArgs e)
        {
            if (searchClientsTextBox.Text.Length == 0)
            {
                searchClientsTextBox.Text = "Search for Client...";
                searchClientsTextBox.ForeColor = SystemColors.GrayText;
            }
        }

        private void searchClientsTextBox_Enter(object sender, EventArgs e)
        {
            if (searchClientsTextBox.Text == "Search for Client...")
            {
                searchClientsTextBox.Text = "";
                searchClientsTextBox.ForeColor = SystemColors.WindowText;
            }
        }
    }
}
