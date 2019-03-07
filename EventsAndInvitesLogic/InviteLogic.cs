using EventsAndInvitesLibrary;
using EventsAndInvitesLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventsAndInvitesLogic
{
    public static class InviteLogic
    {

        public static void SendInvites(DataGridView dgv)
        {
            foreach (DataGridViewRow dgvRow in dgv.SelectedRows)
            {
                InviteModel selectedInvite = (InviteModel)dgvRow.Tag;
                
                selectedInvite.EmailSentDate = DateTime.Now;
                selectedInvite.PlacesReserved = Convert.ToInt32(dgvRow.Cells[0].Value);

                GlobalConfig.Connection.UpSertInviteInfo(selectedInvite);

                dgv.Rows.RemoveAt(dgvRow.Index);
            }
        }

        public static bool CheckUserWantsToSendEmail()
        {
            bool output = false;

            DialogResult dr = MessageBox.Show("Send email to selected members asking if they want to attend the event?", "Please confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            output = (dr == DialogResult.Yes);

            return output;
        }

        public static void InvitesAccepted(DataGridView dgv)
        {
            foreach (DataGridViewRow dgvRow in dgv.SelectedRows)
            {
                InviteModel selectedInvite = (InviteModel)dgvRow.Tag;

                selectedInvite.ClientAttending = true;
                selectedInvite.PlacesReserved = Convert.ToInt32(dgvRow.Cells[0].Value);

                GlobalConfig.Connection.UpSertInviteInfo(selectedInvite);

                dgv.Rows.RemoveAt(dgvRow.Index);
            }

        }

        public static void InvitesRejected(DataGridView dgv)
        {
            foreach (DataGridViewRow dgvRow in dgv.SelectedRows)
            {
                InviteModel selectedInvite = (InviteModel)dgvRow.Tag;
                selectedInvite.ClientAttending = false;

                GlobalConfig.Connection.UpSertInviteInfo(selectedInvite);

                dgv.Rows.RemoveAt(dgvRow.Index);
            }
        }

        public static void CancelInvites(DataGridView dgv)
        {
            foreach (DataGridViewRow dgvRow in dgv.SelectedRows)
            {
                InviteModel selectedInvite = (InviteModel)dgvRow.Tag;
                selectedInvite.ClientAttending = false;
                selectedInvite.PlacesReserved = 0;

                GlobalConfig.Connection.UpSertInviteInfo(selectedInvite);

                dgv.Rows.RemoveAt(dgvRow.Index);
            }
        }

        public static void AddToShortList(DataGridView dgv)
        {
            foreach (DataGridViewRow dgvRow in dgv.SelectedRows)
            {
                InviteModel selectedInvite = (InviteModel)dgvRow.Tag;
 
                GlobalConfig.Connection.UpSertInviteInfo(selectedInvite);

                dgv.Rows.RemoveAt(dgvRow.Index);
            }
        }

        public static ListViewItem PopulateListViewItems(InviteModel invite)
        {
            ListViewItem item = new ListViewItem(invite.PlacesReserved.ToString());
            item.SubItems.Add(invite.Client.FirstName);
            item.SubItems.Add(invite.Client.LastName);
            item.SubItems.Add(invite.Client.BusinessName);
            item.SubItems.Add(invite.Client.EmailAddress);
            item.SubItems.Add(invite.Client.WorkPhone + " / " + invite.Client.CellPhone);
            //Add class to Tag so I can easilty extract this information again
            item.Tag = invite;

            return item;
        }
        public static void PopulateDGVRow(InviteModel invite, DataGridView dgv)
        {
            int rowId = dgv.Rows.Add();

            DataGridViewRow row = dgv.Rows[rowId];

            row.Cells[0].Value = Convert.ToInt32(invite.PlacesReserved);
            row.Cells[0].ValueType = typeof(Int32);

            row.Cells[1].Value = invite.Client.FirstName;
            row.Cells[2].Value = invite.Client.LastName;
            row.Cells[3].Value = invite.Client.BusinessName;
            row.Cells[4].Value = invite.Client.EmailAddress;
            row.Cells[5].Value = invite.Client.WorkPhone + " / " + invite.Client.CellPhone;

            //Add class to Tag so I can easilty extract this information again
            row.Tag = invite;
        }

        public static void invitesDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            DataGridViewRow dgvRow = dgv.Rows[e.RowIndex];

            DataGridViewComboBoxCell cb = (DataGridViewComboBoxCell)dgvRow.Cells[0];
            if (cb.Value != null)
            {
                InviteModel selectedInvite = (InviteModel)dgvRow.Tag;
                selectedInvite.PlacesReserved = Convert.ToInt32(cb.Value);

                if (selectedInvite.Id != 0)
                {
                    GlobalConfig.Connection.UpSertInviteInfo(selectedInvite);
                }
            }
        }

        public static void invitesDataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv.IsCurrentCellDirty)
            {
                // This fires the cell value changed handler below
                dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
    }


}
