using System.Windows.Forms;

namespace EventsAndInvitesLogic
{
   public static class ColumnHeaderLogic
    {
        public static void PopulateListViewColumns(ListView LstV)
        {
            LstV.Columns.Add("No. Invites");
            LstV.Columns.Add("First Name");
            LstV.Columns.Add("Last Name");
            LstV.Columns.Add("Business Name");
            LstV.Columns.Add("Email Address");
            LstV.Columns.Add("Phone Numbers");
        }
        public static void PopulateDGVColumns(DataGridView dgv)
        {
            dgv.Columns.Add("First Name", "First Name");
            dgv.Columns.Add("Last Name", "Last Name");
            dgv.Columns.Add("Business Name", "Business Name");
            dgv.Columns.Add("Email Address", "Email Address");
            dgv.Columns.Add("Phone Numbers", "Phone Numbers");

            dgv.ReadOnly = false;
            foreach (DataGridViewColumn dgvCol in dgv.Columns)
            {
                dgvCol.ReadOnly = true;
            }

            DataGridViewComboBoxColumn cmb = new DataGridViewComboBoxColumn();
            cmb.ValueType = typeof(int);
            cmb.HeaderText = "No. Invites";
            cmb.Name = "No. Invites";
            cmb.ReadOnly = false;
            for (int i = 1; i <= 10; i++)
            {
                cmb.Items.Add(i);
            }
            dgv.Columns.Insert(0, cmb);
        }
    }
}
