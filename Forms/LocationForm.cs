using EventsAndInvitesLibrary;
using EventsAndInvitesLogic.Models;
using EventsAndInvitesUI.Interfaces;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EventsAndInvitesUI
{
    public partial class LocationForm : Form
    {
        private List<VenueModel> venues = GlobalConfig.Connection.GetEventVenues_All();
        private ILocationRequester callingForm;
        private LocationModel eventLocation ;

        public LocationForm(ILocationRequester caller)
        {
            InitializeComponent();
            callingForm = caller;

            PopulateVenueComboBox();
        }

        private void venueSelectorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (venueSelectorComboBox.SelectedIndex != -1)
            {
                ClearAddressLines();
                editAddressButton.Enabled = false;
                deleteAddressButton.Enabled = false;
                deleteVenueButton.Enabled = true;

                PopulateAreaComboBox();
                PopulateAddressComboBox();
                
                //Check if this selected venue has multiple addresses
                //If it does have multiple addresses then let the user switch between them
                bool enableAddressCombo = (addressesComboBox.Items.Count > 1);
                addressesComboBox.Enabled = enableAddressCombo;
                addressesLabel.Enabled = enableAddressCombo;
            }
        }
        /// <summary>
        /// Populates the area box with existing areas from dbo.Address_Area
        /// </summary>
        private void PopulateAreaComboBox()
        {
            areaComboBox.Items.Clear();

            List<string> addressAreas = GlobalConfig.Connection.spGetAddressArea_All();
            foreach (string area in addressAreas)
            {
                areaComboBox.Items.Add(area);
            }
        }
        private void PopulateVenueComboBox()
        {
            venueSelectorComboBox.DataSource = null;
        
            venueSelectorComboBox.DataSource = venues;
            venueSelectorComboBox.DisplayMember = "VenueName";
        }

        private void PopulateAddressComboBox()
        {
            addressesComboBox.DataSource = null;
            VenueModel venue = (VenueModel)venueSelectorComboBox.SelectedItem;

            addressesComboBox.DataSource = GlobalConfig.Connection.GetEventAddresses_ByVenueId(venue.Id);
            addressesComboBox.DisplayMember = "FullMultiLineAddress";
        }
        public void PopulateLocationModel(LocationModel model)
        {
            if (model == null)
            {
                venueSelectorComboBox.SelectedIndex = -1;
                addressesComboBox.SelectedIndex = -1;
                addressesComboBox.Enabled = false;
                ClearAddressLines();
                deleteAddressButton.Enabled = false;
                deleteVenueButton.Enabled = false;
                addNewAddressButton.Enabled = false;
                editAddressButton.Enabled = false;
                eventLocation = new LocationModel();
            }
            else
            {
                eventLocation = model;
                venueSelectorComboBox.Text = model.Venue.VenueName;
                addressesComboBox.Text = model.VenueAddress.FullMultiLineAddress;
            }
   
        }

        private void existingAddressesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (addressesComboBox.SelectedIndex > -1)
            {
                AddressModel address = (AddressModel)addressesComboBox.SelectedItem;

                venueAddressGroupBox.Enabled = false;

                addressLine1TextBox.Text = address.AddressLine1;
                addressLine2TextBox.Text = address.AddressLine2;
                addressLine3TextBox.Text = address.AddressLine3;
                addressLine4TextBox.Text = address.AddressLine4;
                areaComboBox.Text = address.Area;
                postCodeTextBox.Text = address.Area;
                postCodeTextBox.Text = address.PostCode;

                editAddressButton.Enabled = true;
                deleteAddressButton.Enabled = true;
            }
        }

        private void editAddressButton_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("This should only be used to amend an address in cases of typos\n\nIs this a typo?", "Confirm you are correcting a typo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                addressesComboBox.Enabled = false;
                venueSelectorComboBox.Enabled = false;
                venueAddressGroupBox.Enabled = true;
                DisableControls();
            }
        }

        private void DisableControls()
        {
            venueSelectorComboBox.Enabled = false;
            addNewVenueButton.Enabled = false;
            deleteVenueButton.Enabled = false;


            addressesComboBox.Enabled = false;
            editAddressButton.Enabled = false;
            addNewAddressButton.Enabled = false;
            deleteAddressButton.Enabled = false;
        }
        private void submitAddressButton_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                if (venueAddressGroupBox.Enabled)
                {
                    //This means that the user has edited the address so we want to stick the new address details in VenueAddress
                    eventLocation.VenueAddress = PopulateAddress();

                    //If VenueAddress.Id is zero then I am creating a new address (if it isnt 0 then I am just editing an existing address)
                    //If I am creating a new address I want to create a new entry in Location to link the address to the venue
                    if (eventLocation.VenueAddress.Id == 0)
                    {
                        eventLocation.Id = 0;
                    }

                    GlobalConfig.Connection.UpSertAddress_ByAddressId(eventLocation.VenueAddress);
                }
                else
                {
                    //User has not edited the address, just selected it, so I can get the address details from the Combo Box
                    eventLocation.VenueAddress = (AddressModel)addressesComboBox.SelectedItem;
                }

                //We don't update address link until user presses update on the parent form.
                eventLocation.Venue = (VenueModel)venueSelectorComboBox.SelectedItem;

                //Update LocationId
                if (eventLocation.Id == 0)
                {
                    //If eventLocation.Id is zero then I have added a new address, or I am creating a new event, so I need to create or find an entry in Location
                    GlobalConfig.Connection.CreateLocationId(eventLocation);
                }
                else
                {
                    //This is not an newly created address, so there has probably been a change (otherwise user would have probably clicked on the X)
                    //reset eventLocation.Id based on the Venue.Id and VenueAddress.Id)
                    //eventLocation.Id = GlobalConfig.Connection.GetLocationId_ByLocationInfo(eventLocation.Venue.Id, eventLocation.VenueAddress.Id);
                    GlobalConfig.Connection.GetLocationId_ByLocationInfo(eventLocation);
                }

                callingForm.LocationComplete(eventLocation);

                this.Close();
            }
        }

        private AddressModel PopulateAddress()
        {
            AddressModel output = new AddressModel();

            if (addressesComboBox.SelectedIndex != -1)
            {
                output.Id = eventLocation.VenueAddress.Id;
            }

            output.AddressLine1 = addressLine1TextBox.Text.Trim();

            output.AddressLine2 = AssignNonEmptyAddresses(addressLine2TextBox);
            output.AddressLine3 = AssignNonEmptyAddresses(addressLine3TextBox);
            output.AddressLine4 = AssignNonEmptyAddresses(addressLine4TextBox);

            output.Area = areaComboBox.Text.Trim();
            output.PostCode = postCodeTextBox.Text.Trim();

            return output;
        }

        private string AssignNonEmptyAddresses(Control ctrl)
        {
            string output = null;

            if (ctrl.Text.Trim() != "")
            {
                output = ctrl.Text.Trim();
            }

            return output;
        }
        private bool ValidateData()
        {
            if (venueSelectorComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a venue (close this form to cancel your change to this event)");
                return false;
            }

            if (addressLine1TextBox.Text.Trim() == "")
            {
                MessageBox.Show("Address Line 1 is missing");
                return false;
            }
            if (addressLine2TextBox.Text.Trim() == "")
            {
                if (addressLine3TextBox.Text.Trim() != "")
                {
                    MessageBox.Show("Address Line 2 is blank, but there is data in Address Line 3");
                    return false;
                }
            }
            if (addressLine3TextBox.Text.Trim() == "")
            {
                if (addressLine4TextBox.Text.Trim() != "")
                {
                    MessageBox.Show("Address Line 3 is blank, but there is data in Address Line 4");
                    return false;
                }
            }

            if (areaComboBox.Text.Trim() == "")
            {
                MessageBox.Show("Area is missing");
                return false;
            }
            if (postCodeTextBox.Text.Trim() == "")
            {
                MessageBox.Show("Post Code is missing");
                return false;
            }
            return true;
        }

        private void addNewAddressButton_Click(object sender, EventArgs e)
        {
            venueAddressGroupBox.Enabled = true;
            ClearAddressLines();
            eventLocation.VenueAddress.Id = 0;

            addressesComboBox.SelectedIndex = -1;
            DisableControls();
        }
        private void ClearAddressLines()
        {
            addressLine1TextBox.Text = "";
            addressLine2TextBox.Text = "";
            addressLine3TextBox.Text = "";
            addressLine4TextBox.Text = "";
            areaComboBox.SelectedIndex = -1;
            postCodeTextBox.Text = "";
        }
        private void deleteAddressButton_Click(object sender, EventArgs e)
        {
            //Check if the address is used by an event
            var address = (AddressModel)addressesComboBox.SelectedItem;
            if (GlobalConfig.Connection.GetEvents_ByVenueAddressId(address).Count > 0)
            {
                MessageBox.Show($"This address is used by an event so it cannot be deleted", "Not able to delete address", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            DialogResult res = MessageBox.Show($"Are you sure you want to delete this address?", "Please confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (res == DialogResult.Yes)
            {
                //delete from Addresses and from Location
                
                GlobalConfig.Connection.ExecuteSQL($"DELETE FROM dbo.Location WHERE VenueAddressId = { address.Id }");
                GlobalConfig.Connection.DeleteAddress(address.Id);
 
                //Tidy up form
                PopulateAddressComboBox();
                addressesComboBox.SelectedIndex = -1;
                ClearAddressLines();

                deleteAddressButton.Enabled = false;
                editAddressButton.Enabled = false;

            }
        }

        private void deleteVenueButton_Click(object sender, EventArgs e)
        {

            var venue = (VenueModel)venueSelectorComboBox.SelectedItem;

            if (GlobalConfig.Connection.GetEvents_ByVenueId(venue).Count > 0)
            {
                MessageBox.Show($"This venue is used by an event so it cannot be deleted", "Not able to delete venue", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            DialogResult res = MessageBox.Show($"Are you sure you want to delete this venue?", "Please confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (res == DialogResult.Yes)
            {
                //dbo.Venues is linked with dbo.Location so all entries in Location that use this venue should be auto deleted
                GlobalConfig.Connection.ExecuteSQL($"DELETE FROM dbo.Venues where Id = { venue.Id } ");

            venues.Remove(venue);
                PopulateVenueComboBox();
                venueSelectorComboBox.SelectedIndex = -1;
                addressesComboBox.SelectedIndex = -1;
                deleteAddressButton.Enabled = false;
                addNewAddressButton.Enabled = false;
                editAddressButton.Enabled = false;
                deleteVenueButton.Enabled = false;
                ClearAddressLines();
            }
        }

        private void addNewVenueButton_Click(object sender, EventArgs e)
        {
            string newVenueName = Interaction.InputBox("Please enter new venue name. This will update the venue name for any event using this venue", "Enter New Venue Name", venueSelectorComboBox.SelectedText);

            if (newVenueName.Length == 0)
            {
                MessageBox.Show("Error");
                return;
            }

            VenueModel venue = new VenueModel();
            venue.VenueName = newVenueName;

            //Create new venue in SQL
            GlobalConfig.Connection.UpSertVenue_ByVenueId(venue);

            eventLocation = new LocationModel();
            eventLocation.Venue = venue;

            venueSelectorComboBox.DataSource = null;
            venueSelectorComboBox.Items.Add(venue);
            venueSelectorComboBox.DisplayMember = "VenueName";
            venueSelectorComboBox.Text = venue.VenueName;

            DisableControls();
            ClearAddressLines();
            venueAddressGroupBox.Enabled = true;

        }

    }
}
