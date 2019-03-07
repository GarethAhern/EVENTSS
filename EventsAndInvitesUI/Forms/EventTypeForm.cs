using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EventsAndInvitesLibrary;
using EventsAndInvitesLogic.Models;

namespace EventsAndInvitesUI
{
    public partial class EventTypeForm : Form
    {

        private List<EventTypeModel> existingEventTypes = GlobalConfig.Connection.GetEventTypes_All();

        public EventTypeForm()
        {
            InitializeComponent();

            WireUpListBox();
        }

        private void WireUpListBox()
        {
            existingEventTypesListBox.DataSource = null;

            existingEventTypesListBox.DataSource = existingEventTypes;
            existingEventTypesListBox.DisplayMember = "EventType";
        }

        private void createEventTypeButton_Click(object sender, EventArgs e)
        {
            if (ValidNewEventType())
            {
                EventTypeModel m = new EventTypeModel(newEventTypeTextBox.Text);

                GlobalConfig.Connection.CreateEventType(m);
                newEventTypeTextBox.Text = "";
                existingEventTypes.Add(m);
                WireUpListBox();
            }
        }

        private bool ValidNewEventType()
        {
            if (string.IsNullOrEmpty(newEventTypeTextBox.Text))
            {
                MessageBox.Show("Event name is empty", "Please Fill In Event Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            foreach (EventTypeModel itm in existingEventTypes)
            {
                if (itm.EventType.ToLower() == newEventTypeTextBox.Text.ToLower())
                {
                    MessageBox.Show("This Event Type already exists", "Duplicate Event Type", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return true;
        }

        private void existingEventTypesListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DialogResult res = MessageBox.Show("Do you want to delete this event type?", "Confirm deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (res == DialogResult.Yes)
            {
                EventTypeModel p = (EventTypeModel)existingEventTypesListBox.SelectedItem;

                //Check if this EventType is in use
                List<EventModel> eventModel = GlobalConfig.Connection.GetEvent_ByEventTypeId(p.Id);

                if (eventModel.Count > 0)
                {
                    //If it is in use then tell user it cannot be delete
                    MessageBox.Show("I am not going to delete this Event Type because it is in use", $"{ p.EventType } NOT Removed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    //Else - delete from SQL and updated form
                    GlobalConfig.Connection.DeleteEventTypes_ById(p.Id);
                    existingEventTypes.Remove(p);
                    WireUpListBox();
                    MessageBox.Show("This Event Type has been deleted", $"{ p.EventType } Removed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        //TODO
        //am i going to keep this or delete the ability to group event types?
        //If i delete then i need to get rid of dbo.Event_Types_GroupNames
        //I if i keep then i need to come up with a better table naming systing
        private void showHideEventGrouperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Size smallSize = new Size(388, 605);
            Size BigSize = new Size(1143, 605);

            if (this.Size == smallSize)
            {
                this.Size = BigSize;
            }
            else
            {
                this.Size = smallSize;
            }
        }



        private void existingEventTypesListBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (existingEventTypesListBox.Items.Count == 0)
                return;

            int index = existingEventTypesListBox.IndexFromPoint(e.X, e.Y);
            if (index != -1)
            {
            EventTypeModel s = (EventTypeModel)existingEventTypesListBox.Items[index];
            DragDropEffects dde1 = DoDragDrop(s,DragDropEffects.All);

            }

        }

        private void listBox1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
                EventTypeModel et = (EventTypeModel)e.Data.GetData(typeof(EventTypeModel));

                listBox1.Items.Add(et);
                listBox1.DisplayMember = "EventType";
        }
    }
}

