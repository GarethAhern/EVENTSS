using EventsAndInvitesLibrary;
using EventsAndInvitesLogic;
using EventsAndInvitesLogic.Models;
using EventsAndInvitesUI.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EventsAndInvitesUI
{
    public partial class EventForm : Form, ILocationRequester, IInvitationRequester
    {
        /// <summary>
        /// List of all Event Type Models, used to populate the CheckedListBox
        /// </summary>
        private List<EventTypeModel> eventTypes = GlobalConfig.Connection.GetEventTypes_All();
        /// <summary>
        /// List of all BNZ staff members that are currently active, used to populate the staff combo box
        /// </summary>
        private List<StaffModel> selectableStaff = GlobalConfig.Connection.GetStaff_Actives();
        /// <summary>
        /// This is a list of every client who is either on the shortlist, been invited with no response and rejected / accepted an invite
        /// </summary>
        List<InviteModel> guestList = new List<InviteModel>();

        private EventModel eventModel = new EventModel();
        private IEventRequester callingForm;

        //### Sets up form ###
        public EventForm(IEventRequester caller)
        {
            InitializeComponent();

            //This create a variable at class level
            //it stores whatever is passed into the constructor
            callingForm = caller;

            PopulateListViewColumns(inviteListTabPage);
            FindDGVs(inviteListTabPage);
            EventTypeSetUp();
            PopulateSelectableStaffListBox();

            DefaultSearchInviteListsToolStripTextBoxSettings();

            proposedInvitationDataGridView.Font = SetFont();
            approvedInvitationDataGridView.Font = SetFont();
            invitedDataGridView.Font = SetFont();
            acceptedInvitationDataGridView.Font = SetFont();
            rejectedInvitationDataGridView.Font = SetFont();
        }
        private Font SetFont()
        {
            return new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }
        private void EventTypeSetUp()
        {
            eventTypeCheckedListBox.DataSource = null;
            eventTypeCheckedListBox.DataSource = eventTypes;
            eventTypeCheckedListBox.DisplayMember = "EventType";
        }

        private void PopulateSelectableStaffListBox()
        {
            staffComboBox.Items.Clear();

            foreach (StaffModel staff in selectableStaff)
            {
                staffComboBox.Items.Add(staff);
            }

            staffComboBox.DisplayMember = "FullName";
            staffComboBox.SelectedIndex = -1;
        }


        public void FindDGVs(Control control)
        {

            if (control.GetType() == typeof(DataGridView))
            {
                DataGridView dgv = (DataGridView)control;
                ColumnHeaderLogic.PopulateDGVColumns(dgv);
            }
            else
            {
                foreach (Control Ctrl in control.Controls)
                {
                    FindDGVs(Ctrl);
                }
            }
        }
 
        /// <summary>
        /// Loops through all controls and populates any List View with default columns
        /// </summary>
        /// <param name="control"></param>
        public void PopulateListViewColumns(Control control)
        {
            if (control.GetType() == typeof(ListView))
            {
                ListView LstV = (ListView)control;
                ColumnHeaderLogic.PopulateListViewColumns(LstV);

                LstV.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            else
            {
                foreach (Control Ctrl in control.Controls)
                {
                    PopulateListViewColumns(Ctrl);
                }
            }
        }

        public void PopulateEventForm(EventModel model)
        {
            //This is called if the form is being updated
            //I need to pass an existing Event into the form and populate the fields
            eventModel = model;

            eventNameTextBox.Text = eventModel.EventName;
            eventDateTimeDatePicker.Value = eventModel.EventDate;

            startTimeDateTimePicker.Value = DateTime.Today.Add(eventModel.StartTime);
            endTimeDateTimePicker.Value = DateTime.Today.Add(eventModel.EndTime);


            //Refresh the address in the unlikely event that the address this links to has been edited
            eventModel.EventLocation = GlobalConfig.Connection.GetEventLocation_ByLocationId(model.EventLocationId);

            eventVenueTextBox.Text = eventModel.EventLocation.Venue.VenueName;
            eventAddressTextBox.Text = eventModel.EventLocation.VenueAddress.FullMultiLineAddress;


            eventDescriptionTextBox.Text = eventModel.EventDescription;

            maxNumberOfGuestsNumericUpDown.Value = eventModel.MaxNumberOfGuests;
            eventCostEstimateNumericUpDown.Value = eventModel.EstimatedCost;

            //Populate CheckListBox with Event Types
            foreach (EventTypeModel item in model.EventType)
            {
                for (int x = 0; x < eventTypeCheckedListBox.Items.Count; x++)
                {
                    EventTypeModel ev = (EventTypeModel)eventTypeCheckedListBox.Items[x];
                    if (ev.Id == item.Id)
                    {
                        eventTypeCheckedListBox.SetItemChecked(x, true);
                    }
                }
            }

            //Populate DataGridView with the owners
            List<EventOwnerModel> owners = GlobalConfig.Connection.GetEventOwners_ByEventId(model.Id);

            foreach (EventOwnerModel owner in owners)
            {
                int rowIndex = eventOwnerDataGridView.Rows.Add();
                DataGridViewRow row = eventOwnerDataGridView.Rows[rowIndex];

                row.Cells[0].Value = owner.StaffId;
                row.Cells[1].Value = owner.Staff.FullName;
                row.Cells[2].Value = owner.CostPercentage;

                selectableStaff.RemoveAll(x => x.Id == owner.Staff.Id);
            }

            PopulateSelectableStaffListBox();

            RefreshAllInvitesListView();

            saveEventButton.Text = "Update Event";
        }
        //### Sets up form ###


        private void eventTypeLabel_Click(object sender, EventArgs e)
        {
            //This allows the user to create a new (or delete) an Event Type
            EventTypeForm newForm = new EventTypeForm();
            newForm.ShowDialog();

            eventTypes = GlobalConfig.Connection.GetEventTypes_All();

            RefreshEventTypeListViewAndKeepExistingSelections();
        }
        private void RefreshEventTypeListViewAndKeepExistingSelections()
        {
            //Record all selected event types
            List<EventTypeModel> selectedEvents = new List<EventTypeModel>();
            if (eventTypeCheckedListBox.SelectedItems.Count > 0)
            {
                foreach (object item in eventTypeCheckedListBox.CheckedItems)
                {
                    selectedEvents.Add((EventTypeModel)item);
                }
            }

            //Reset eventTypesCheckListBox
            EventTypeSetUp();

            //Reselect the event types
            for (int x = 0; x < eventTypeCheckedListBox.Items.Count; x++)
            {
                EventTypeModel ev = (EventTypeModel)eventTypeCheckedListBox.Items[x];

                if (selectedEvents.Count(s => s.Id == ev.Id) == 1)
                {
                    eventTypeCheckedListBox.SetItemChecked(x, true);
                }
            }
        }
        private void createEventButton_Click(object sender, EventArgs e)
        {
            if (ValidBasicInputDataInput())
            {
                eventModel.EventName = eventNameTextBox.Text;

                DateTime eventDateValue = new DateTime(1900, 1, 1);
                DateTime.TryParse(eventDateTimeDatePicker.Value.ToShortDateString(), out eventDateValue);
                eventModel.EventDate = eventDateValue;

                eventModel.EventDescription = eventDescriptionTextBox.Text;

                //### set start and end date ###
                DateTime dt = new DateTime();
                TimeSpan ts = new TimeSpan();

                dt = startTimeDateTimePicker.Value;
                ts = dt.TimeOfDay;
                eventModel.StartTime = new TimeSpan(ts.Hours, ts.Minutes, 0); //This last bit removes the seconds

                dt = endTimeDateTimePicker.Value;
                ts = dt.TimeOfDay;
                eventModel.EndTime = new TimeSpan(ts.Hours, ts.Minutes, 0);
                //### set start and end date ###

                eventModel.MaxNumberOfGuests = (int)maxNumberOfGuestsNumericUpDown.Value;
                eventModel.EstimatedCost = (decimal)eventCostEstimateNumericUpDown.Value;

                eventModel.EventOwners = new List<EventOwnerModel>();

                foreach (DataGridViewRow dgvRow in eventOwnerDataGridView.Rows)
                {
                    EventOwnerModel eventOwner = new EventOwnerModel();
                    eventOwner.StaffId = (int)dgvRow.Cells[0].Value;
                    eventOwner.Staff = GlobalConfig.Connection.GetStaff_ById(eventOwner.StaffId);
                    eventOwner.CostPercentage = Convert.ToInt32(dgvRow.Cells[2].Value.ToString());
                    eventModel.EventOwners.Add(eventOwner);
                }


                eventModel.EventType = new List<EventTypeModel>();

                foreach (object ev in eventTypeCheckedListBox.CheckedItems)
                {
                    EventTypeModel eventType = new EventTypeModel();
                    eventType = (EventTypeModel)ev;

                    eventModel.EventType.Add(eventType);
                }

                GlobalConfig.Connection.UpSertEvent(eventModel);

                RefreshAllInvitesListView();
                callingForm.EventComplete(eventModel);

                saveEventButton.Text = "Update Event";
                this.Text = "View / Edit Event";
            }
        }
        /// <summary>
        /// Validates the data inputed onto he Basic Input tab
        /// </summary>
        /// <returns></returns>
        private bool ValidBasicInputDataInput()
        {
            if (string.IsNullOrEmpty(eventNameTextBox.Text))
            {
                MessageBox.Show("Event name is empty");
                return false;
            }

            if (eventDateTimeDatePicker.Value.Date < DateTime.Today)
            {
                DialogResult dr = MessageBox.Show("This event is in the past... \n\nMaybe you should press No and change the date. ", "Create Event in Past?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.No)
                {
                    return false;
                }
            }

            //#### Check the time of the event ####
            TimeSpan startTime;
            TimeSpan endTime;

            if (!TimeSpan.TryParse(startTimeDateTimePicker.Text, out startTime))
            {
                MessageBox.Show($"Start Time of { startTimeDateTimePicker.Text } not in correct format");
                return false;
            }

            if (!TimeSpan.TryParse(endTimeDateTimePicker.Text, out endTime))
            {
                MessageBox.Show($"End Time of { endTimeDateTimePicker.Text } not in correct format");
                return false;
            }

            if (startTime > endTime)
            {
                MessageBox.Show($"Event ends before it starts");
                return false;
            }
            //#### Check the time of the event ####



            //#### Check Location information has been set ####
            if (string.IsNullOrEmpty(eventVenueTextBox.Text))
            {
                MessageBox.Show("Event venue is empty");
                return false;
            }

            if (string.IsNullOrEmpty(eventAddressTextBox.Text))
            {
                MessageBox.Show("Venue Full Address is empty");
                return false;
            }


            if (string.IsNullOrEmpty(eventDescriptionTextBox.Text))
            {
                MessageBox.Show("Event description is empty");
                return false;
            }
            //#### Check Location information has been set ####


            uint guestCount;
            if (!uint.TryParse(maxNumberOfGuestsNumericUpDown.Value.ToString(), out guestCount))
            {
                MessageBox.Show("Error with max number of guests number entered");
                return false;
            }

            //#### Check Event Owner information ####
            int cumulativeCostPercentage = 0;
            foreach (DataGridViewRow owner in eventOwnerDataGridView.Rows)
            {
                string ownerFullName = owner.Cells[1].Value.ToString();

                int costPercentageCheck;
                //Check all the rows have valid percentage figures
                if (!int.TryParse(owner.Cells[2].Value.ToString(), out costPercentageCheck))
                {
                    MessageBox.Show($"{ ownerFullName } has invalid Cost Percentage figure of { owner.Cells[2].Value.ToString() }", "Cost Percentage Sum", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                //TODO check people only appear once
                int staffId;
                //Check all the rows have valid percentage figures
                if (!int.TryParse(owner.Cells[0].Value.ToString(), out staffId))
                {
                    MessageBox.Show($"{ownerFullName } has invalid StaffId of { owner.Cells[0].Value.ToString() }", "Invalid StaffId", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                cumulativeCostPercentage += costPercentageCheck;
            }
            //Check the rows add up to 100%
            if (cumulativeCostPercentage != 100)
            {
                MessageBox.Show($"Cost percentage adds up to { cumulativeCostPercentage }%. Should equal 100%", "Cost Percentage Sum", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //#### Check Event Owner information ####


            //If i get all the way here it means that none of the tests failed
            return true;
        }

        private void eventAddresEditButton_Click(object sender, EventArgs e)
        {
            LocationForm newForm = new LocationForm(this);
            newForm.PopulateLocationModel(eventModel.EventLocation);
            newForm.ShowDialog();
        }
        /// <summary>
        /// Called after user returns from the LocationForm
        /// </summary>
        /// <param name="model"></param>
        public void LocationComplete(LocationModel model)
        {
            eventModel.EventLocationId = model.Id;
            eventModel.EventLocation = model;

            eventVenueTextBox.Text = eventModel.EventLocation.Venue.VenueName;
            eventAddressTextBox.Text = eventModel.EventLocation.VenueAddress.FullMultiLineAddress;
        }
        // ### Invited List tab ###




        //### Refresh Invitation Status ListViews ###
        public void RefreshAllInvitesListView(string optionalSearchString = "")
        {
            RefreshGuestList();

            RefreshProposedInvitations(optionalSearchString);//These clients have been proposed to be invited
            RefreshApprovedInvitations(optionalSearchString);//These clients were proposed and approved
            RefreshInvitedDGV(optionalSearchString); //These clients have been invited, but not confirmed if they are coming yet
            RefreshAttendingDGV(optionalSearchString);//These clients have accepted the invitation
            RefreshRejectedDGV(optionalSearchString);//These clients have rejected the invitation    
     
        }
        private void RefreshGuestList()
        {
            guestList = GlobalConfig.Connection.GetInviteList_ByEvent(eventModel.Id);
        }
        private void RefreshProposedInvitations(string optionalSearchString = "")
        {
            RefreshInvitaionDGV(ref proposedInvitationDataGridView, GetInviteListsLogic.GetProposedInvitation(guestList), optionalSearchString);
            UpdateProposedInvitationLabel();
        }
        private void UpdateProposedInvitationLabel()
        {
            proposedInvitaitonTab.Text = "Proposed Invitations : " + GetInviteListsLogic.CalculateNumberOfProposedInvitations(guestList);
        }
        private void RefreshApprovedInvitations(string optionalSearchString = "")
        {
            RefreshInvitaionDGV(ref approvedInvitationDataGridView, GetInviteListsLogic.GetApprovedInvitation(guestList), optionalSearchString);
            UpdateApprovedInvitationLabel();
        }
        private void UpdateApprovedInvitationLabel()
        {
            approvedInvitationTab.Text = "Approved Invitations : " + GetInviteListsLogic.CalculateNumberOfApprovedInvitations(guestList);
        }
      
        private void RefreshInvitedDGV(string optionalSearchString = "")
        {
            //I use Events_spGetClients_All in case a client is invited and then they unregister - we still want to invite them
            RefreshInvitaionDGV(ref invitedDataGridView, GetInviteListsLogic.GetNotRespondedInvitations(guestList), optionalSearchString);
            UpdatedInvitedInvitationLabels();
        }
        private void UpdatedInvitedInvitationLabels()
        {
            int numberOfInvites = GetInviteListsLogic.CalculateNumberOfInvited(guestList);
            invitedGroupBox.Text = "Invited Clients : " + numberOfInvites;

            PopulatePotentialGuestCount(numberOfInvites + GetInviteListsLogic.CalculateNumberOfAcceptedInvitations(guestList));
        }


        private void RefreshAttendingDGV(string optionalSearchString = "")
        {
            RefreshInvitaionDGV(ref acceptedInvitationDataGridView, GetInviteListsLogic.GetAcceptedInvitations(guestList), optionalSearchString);
            UpdatedAcceptedInvitationLabels();
        }
        private void UpdatedAcceptedInvitationLabels()
        {
            int numberOfAccepted = GetInviteListsLogic.CalculateNumberOfAcceptedInvitations(guestList);
            acceptedInvitationTab.Text = "Accepted Invitations : " + numberOfAccepted;

            PopulatePotentialGuestCount(numberOfAccepted + GetInviteListsLogic.CalculateNumberOfInvited(guestList));
        }


        private void PopulatePotentialGuestCount(int PotentialGuestCount)
        {
            potentialTotalGuestsValue.Text = PotentialGuestCount.ToString();

            bool showPotentialData = (PotentialGuestCount > 0);
            potentialTotalGuestsLabel.Visible = showPotentialData;
            potentialTotalGuestsValue.Visible = showPotentialData;
            potentialTotalInfoBox.Visible = showPotentialData;

            if (PotentialGuestCount > maxNumberOfGuestsNumericUpDown.Value)
            {
                potentialTotalGuestsLabel.ForeColor = Color.Red;
                potentialTotalGuestsValue.ForeColor = Color.Red;
            }
            else
            {
                potentialTotalGuestsLabel.ForeColor = Color.FromArgb(15, 150, 255);
                potentialTotalGuestsValue.ForeColor = Color.FromArgb(15, 150, 255);
            }
        }


        private void RefreshRejectedDGV(string optionalSearchString = "")
        {
            RefreshInvitaionDGV(ref rejectedInvitationDataGridView, GetInviteListsLogic.GetNotAttendingClients(guestList), optionalSearchString);
        }

        private void RefreshInvitaionDGV(ref DataGridView invitationDGV, List<InviteModel> invitationList, string optionalSearchString = "")
        {
            invitationDGV.Rows.Clear();

            if (invitationList.Count > 0)
            {
                invitationDGV.CellValueChanged -= new DataGridViewCellEventHandler(invitesDataGridView_CellValueChanged);
                invitationDGV.CurrentCellDirtyStateChanged -= new EventHandler(InviteLogic.invitesDataGridView_CurrentCellDirtyStateChanged);

                if (optionalSearchString != "")
                {
                    invitationList = invitationList.SearchInvite(optionalSearchString);
                }

                foreach (InviteModel invite in invitationList)
                {
                    InviteLogic.PopulateDGVRow(invite, invitationDGV);
                }

                invitationDGV.CellValueChanged += new DataGridViewCellEventHandler(invitesDataGridView_CellValueChanged);
                invitationDGV.CurrentCellDirtyStateChanged += new EventHandler(InviteLogic.invitesDataGridView_CurrentCellDirtyStateChanged);
            }
        }
        private void invitesDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            InviteLogic.invitesDataGridView_CellValueChanged(sender, e);

            UpdateInviteCountLabels((DataGridView)sender);
        }
        private void UpdateInviteCountLabels(DataGridView dgv)
        {
            if (dgv == proposedInvitationDataGridView)
            {
                UpdateProposedInvitationLabel();
            }
            else if (dgv == approvedInvitationDataGridView)
            {
                UpdateApprovedInvitationLabel();
            }
            else if (dgv == invitedDataGridView)
            {
                UpdatedInvitedInvitationLabels();
            }
            else if (dgv == acceptedInvitationDataGridView)
            {
                UpdatedAcceptedInvitationLabels();
            }

        }
        //### Refresh Invitation Status ListViews ###



        //### User has updated the invitation status ###
        private void sendInviteButton_Click(object sender, EventArgs e)
        {
            //TODO - send an invite
            if (proposedInvitationDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("You have not selected anyone....");
            }
            else
            {
                if (InviteLogic.CheckUserWantsToSendEmail())
                {
                    InviteLogic.SendInvites(proposedInvitationDataGridView);
                    RefreshApprovedInvitations();
                    RefreshInvitedDGV();
                }
            }
        }
        private void acceptInviteButton_Click(object sender, EventArgs e)
        {
            if (invitedDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("You have not selected anyone....");
            }
            else
            {
                DialogResult dr = MessageBox.Show("Have the selected members replied that they are attending?", "Please confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    InviteLogic.InvitesAccepted(invitedDataGridView);

                    RefreshInvitedDGV();
                    RefreshAttendingDGV();
                }
            }
        }
        private void rejectInviteButton_Click(object sender, EventArgs e)
        {
            if (invitedDataGridView.Rows.Count == 0)
            {
                MessageBox.Show("You have not selected anyone....");
            }
            else
            {
                DialogResult dr = MessageBox.Show("Have the selected members replied that they are NOT attending?", "Please confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    foreach (DataGridViewRow selectedItem in invitedDataGridView.SelectedRows)
                    {
                        InviteModel selectedInvite = (InviteModel)selectedItem.Tag;
                        selectedInvite.ClientAttending = false;
                        //selectedInvite.PlacesReserved = 0;
                        GlobalConfig.Connection.UpSertInviteInfo(selectedInvite);
                    }
                    RefreshInvitedDGV();
                    RefreshRejectedDGV();
                }
            }
        }
        //### User has updated the invitation status ###


        //### Edit the Event Owner ###
        private void addEventOwnerButton_Click(object sender, EventArgs e)
        {
            StaffModel staffSelected = (StaffModel)staffComboBox.SelectedItem;
            if (staffSelected != null)
            {
                int rowIndex = eventOwnerDataGridView.Rows.Add();
                DataGridViewRow row = eventOwnerDataGridView.Rows[rowIndex];

                row.Cells[0].Value = staffSelected.Id;
                row.Cells[1].Value = staffSelected.FullName;
                row.Cells[2].Value = 0;

                selectableStaff.Remove(staffSelected);
                PopulateSelectableStaffListBox();
            }
        }
        /// <summary>
        /// If a users changes Cost Percentage to be 0% then I want to remove that row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void eventOwnerDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //Has a  user just changed a cell in the Cost Percentage column?
            if (eventOwnerDataGridView.Columns[e.ColumnIndex].Name == "CostPercentage")
            {
                //Did they set the value to 0?
                if (eventOwnerDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null || eventOwnerDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "0")
                {

                    //Get StaffId and so get StaffModel
                    int StaffId = Convert.ToInt32(eventOwnerDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
                    StaffModel staffMember = GlobalConfig.Connection.GetStaff_ById(StaffId);

                    //Lets remove the row from the DataGridView
                    try
                    {
                        eventOwnerDataGridView.Rows.RemoveAt(e.RowIndex);
                    }
                    catch (Exception)
                    {
                        //Operation is not valid because it results in a reentrant call to the SetCurrentCellAddressCore function
                    }


                    // Add the Staff member back to the combobox so user can reselect if they want to
                    selectableStaff.Add(staffMember);
                    PopulateSelectableStaffListBox();
                }
            }
        }
        //### Edit the Event Owner ###


        //DoubleClick - Allows user to open UpdateInviteStatusForm so they have more space
        private void proposedInvitationDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        { 
            List<InviteModel> proposed = GetInviteListsLogic.GetProposedInvitation(guestList);

            if (proposed.Count > 0)
            {
                UpdateInviteStatusForm newForm = new UpdateInviteStatusForm(this);

                newForm.rejectButtonEnabler = false;
                newForm.removeButtonEnabler = true;
                newForm.shortListButtonEnabler = true;
                newForm.sendButtonEnabler = false;
                newForm.acceptButtonEnabler = false;

                newForm.PopulateDGVModel(proposed);
                newForm.Text = this.Text + "Clients Short Listed To Be Invited";
                newForm.ShowDialog();
            }
        }
        private void approvedInvitationDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            List<InviteModel> approved = GetInviteListsLogic.GetApprovedInvitation(guestList);

            if (approved.Count > 0)
            {
                UpdateInviteStatusForm newForm = new UpdateInviteStatusForm(this);

                newForm.rejectButtonEnabler = false;
                newForm.removeButtonEnabler = true;
                newForm.shortListButtonEnabler = false;
                newForm.sendButtonEnabler = true;
                newForm.acceptButtonEnabler = false;

                newForm.PopulateDGVModel(approved);
                newForm.Text = this.Text + "Clients Short Listed To Be Invited";
                newForm.ShowDialog();
            }
        }
        private void invitedClientsDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            List<InviteModel> invited = GetInviteListsLogic.GetNotRespondedInvitations(guestList);

            if (invited.Count > 0)
            {
                UpdateInviteStatusForm newForm = new UpdateInviteStatusForm(this);

                newForm.rejectButtonEnabler = true;
                newForm.removeButtonEnabler = false;
                newForm.shortListButtonEnabler = false;
                newForm.sendButtonEnabler = false;
                newForm.acceptButtonEnabler = true;

                newForm.PopulateDGVModel(invited);
                newForm.Text = this.Text + "Clients Who Are Invited (No Response Yet)";
                newForm.ShowDialog();
            }
        }
        private void acceptedDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            List<InviteModel> attending  = GetInviteListsLogic.GetAcceptedInvitations(guestList);

            if (attending.Count > 0)
            {
                UpdateInviteStatusForm newForm = new UpdateInviteStatusForm(this);

                newForm.rejectButtonEnabler = false;
                newForm.removeButtonEnabler = false;
                newForm.shortListButtonEnabler = false;
                newForm.sendButtonEnabler = false;
                newForm.acceptButtonEnabler = false;
                
                newForm.PopulateDGVModel(attending);
                newForm.Text = this.Text + " - Clients Who Accepted Invite";
                newForm.ShowDialog();
            }
        }
        private void rejectedDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            List<InviteModel> rejected = GetInviteListsLogic.GetNotAttendingClients(guestList);

            if (rejected.Count > 0)
            {
                UpdateInviteStatusForm newForm = new UpdateInviteStatusForm(this);

                newForm.rejectButtonEnabler = false;
                newForm.removeButtonEnabler = false;
                newForm.shortListButtonEnabler = false;
                newForm.sendButtonEnabler = false;
                newForm.acceptButtonEnabler = false;

                newForm.PopulateDGVModel(rejected);
                newForm.Text = this.Text + "Clients Who Rejected Invite";
                newForm.ShowDialog();
            }
        }
        //DoubleClick - Allows user to open UpdateInviteStatusForm so they have more space

        private void addToPropsedInvitationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (eventModel.Id == 0)
            {
                MessageBox.Show("Please create event before adding guests");
                return;
            }
            else
            {
                UpdateInviteStatusForm newForm = new UpdateInviteStatusForm(this);

                newForm.rejectButtonEnabler = false;
                newForm.removeButtonEnabler = true;
                newForm.shortListButtonEnabler = true;
                newForm.sendButtonEnabler = false;
                newForm.acceptButtonEnabler = false;

                newForm.PopulateDGVModel(GetInviteableClients());

                newForm.Text = this.Text + "Clients Who Can Be Proposed To Be Invited";
                newForm.Show();
            }
        }

        private List<InviteModel> GetInviteableClients()
        {
            List<ClientModel> inviteableClients = GlobalConfig.Connection.GetInviteableClients_ByEventID(eventModel.Id);
            List<InviteModel> inviteList = new List<InviteModel>();

            foreach (ClientModel client in inviteableClients)
            {

                InviteModel invite = new InviteModel();
                invite.Client = client;
                invite.ClientId = client.Id;
                invite.Event = eventModel;
                invite.EventId = eventModel.Id;

                inviteList.Add(invite);
            }

            return inviteList;
        }

        //### Export Data To Excel ###
        private void exportInvitedClientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportEventToExcelLogic.ExportEventData(eventModel, invitedDataGridView, acceptedInvitationDataGridView);
        }

        private void invitedDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            MessageBox.Show("this should delete a row");
        }

        private void loadInviteRepliesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadResponses newForm = new LoadResponses(eventModel.Id, this);
            newForm.ShowDialog();
        }
        //### Export Data To Excel ###

        private void searchInviteListsToolStripTextBox_Leave(object sender, EventArgs e)
        {
            if (searchInviteListsToolStripTextBox.Text.Length == 0)
            {
                DefaultSearchInviteListsToolStripTextBoxSettings();
            }
        }
        private void DefaultSearchInviteListsToolStripTextBoxSettings()
        {
            searchInviteListsToolStripTextBox.Text = "Search Invite List....";
            searchInviteListsToolStripTextBox.ForeColor = SystemColors.GrayText;
        }
        private void searchInviteListsToolStripTextBox_Enter(object sender, EventArgs e)
        {
            if (searchInviteListsToolStripTextBox.Text == "Search Invite List....")
            {
                searchInviteListsToolStripTextBox.Text = "";
                searchInviteListsToolStripTextBox.ForeColor = SystemColors.WindowText;
            }
        }

        private void searchInviteListsToolStripTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || searchInviteListsToolStripTextBox.TextLength == 0)
            {
                RefreshAllInvitesListView(searchInviteListsToolStripTextBox.Text);
            }
        }


    }
}