using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using EventsAndInvitesUI.Interfaces;
using EventsAndInvitesLogic.Models;
using EventsAndInvitesLibrary;
using System.Globalization;
using System.Drawing;
using EventsAndInvitesLogic;

namespace EventsAndInvitesUI
{
    public partial class MainDashBoards : Form, IEventRequester
    {
        private List<EventModel> existingEvents = GlobalConfig.Connection.GetEvents_All();
        private List<ListViewItem> selectedMonths = new List<ListViewItem>();
        public MainDashBoards()
        {
            InitializeComponent();
            //TODO - list should start of ascending and limited to only future events
            WireUpEventsListBox();
            WireUpDateListBoxes();
        }

        private void WireUpEventsListBox()
        {
            existingEventsListBox.DataSource = null;
            List<EventModel> filtered = existingEvents;

            //### Check if future events only filter is ticked or not ###
            if (showOnlyFutureEventsCheckBox.Checked)
            {
                filtered = filtered.FilterEvent_FutureDataOnly();
            }

            filtered = filtered.FilterEvent_Dates(monthsFilterListBox,yearsFilterListBox);

            //### Search Box ###
            string searchValue = searchEventsTextBox.Text.ToLower();
            if (searchValue.Length > 0 && searchEventsTextBox.ForeColor != SystemColors.GrayText)
            {
                filtered = filtered.FilterEvent_SearchBox(searchValue, includeEventDescriptionCheckBox.Checked);
            }

            filtered = filtered.Distinct().OrderBy(x => x.ListBoxDisplay).ToList();

            existingEventsListBox.DataSource = filtered;
            existingEventsListBox.DisplayMember = "ListBoxDisplay";
            existingEventsListBox.SelectedIndex = -1;
        }

        private void WireUpDateListBoxes()
        {
            //Years List Box
            List<int> eventYears = existingEvents.Select(x => x.EventDate.Year).Distinct().ToList();
            foreach (int year in eventYears)
            {
                yearsFilterListBox.Items.Add(year);
            }

            //Months List Box
            monthsFilterListBox.Items.Clear();

            for (int i = 1; i <= 12; i++)
            {
                monthsFilterListBox.Items.Add(DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(i));
            }
        }

        private void createNewEventToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EventForm newForm = new EventForm(this);
            newForm.Show();
        }

        private void existingEventsListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            EventModel currentObject = (EventModel)existingEventsListBox.SelectedItem;
            if (currentObject != null)
            {
                EventForm newForm = new EventForm(this);
                newForm.PopulateEventForm(currentObject);
                newForm.Text = "View / Edit Event";
                newForm.Show();
            }

        }
        private void viewRegistrationSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegistrationForm newForm = new RegistrationForm();
            newForm.Show();
        }

        //### These fire off the WireUpEventListBox ###
        private void showOnlyFutureEventsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            WireUpEventsListBox();
        }
        private void includeEventDescriptionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            WireUpEventsListBox();
        }
        public void EventComplete(EventModel model)
        {
            existingEvents.Remove(model);
            existingEvents.Add(model);
            WireUpEventsListBox();
        }
        private void monthsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            WireUpEventsListBox();
        }
        private void yearsFilterListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            WireUpEventsListBox();
        }
        private void searchEventsTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || searchEventsTextBox.TextLength == 0)
            {
                WireUpEventsListBox();
            }
        }
        //### These fire off the WireUpEventListBox ###


        private void createNewEventToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            EventForm newForm = new EventForm(this);
            newForm.Show();
        }
        private void searchEventsTextBox_Enter(object sender, EventArgs e)
        {
            if (searchEventsTextBox.Text == "Search for Event...")
            {
                searchEventsTextBox.Text = "";
                searchEventsTextBox.ForeColor = SystemColors.WindowText;
            }
        }
        private void searchEventsTextBox_Leave(object sender, EventArgs e)
        {
            if (searchEventsTextBox.Text.Length == 0)
            {
                searchEventsTextBox.Text = "Search for Event...";
                searchEventsTextBox.ForeColor = SystemColors.GrayText;
            }
        }
    }
}