using EventsAndInvitesLogic.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace EventsAndInvitesLogic
{
  public static class SearchLogic
    {
        public static List<InviteModel> SearchInvite(this List<InviteModel> filtered,string searchValue)
        {
            List<InviteModel> searchFilter = new List<InviteModel>();

            searchFilter.AddRange(filtered.Where(x => x.Client.EmailAddress != null && x.Client.EmailAddress.ToLower().Contains(searchValue.ToLower())).ToList());
            searchFilter.AddRange(filtered.Where(x => x.Client.BusinessName != null && x.Client.BusinessName.ToLower().Contains(searchValue)).ToList());
            searchFilter.AddRange(filtered.Where(x => x.Client.FirstName != null && x.Client.FirstName.ToLower().Contains(searchValue)).ToList());
            searchFilter.AddRange(filtered.Where(x => x.Client.LastName != null && x.Client.LastName.ToLower().Contains(searchValue)).ToList());

            return searchFilter.Distinct().ToList();
        }



        public static List<EventModel> FilterEvent_FutureDataOnly(this List<EventModel> filtered)
        {
            DateTime CurrentDate = DateTime.Today;
            return new List<EventModel>(filtered.Where(x => x.EventDate >= CurrentDate));
        }

        public static List<EventModel> FilterEvent_Dates(this List<EventModel> filtered, ListBox monthsFilterListBox, ListBox yearsFilterListBox)
        {
            filtered = filtered.FilterEvent_SelectedMonths(monthsFilterListBox);
            filtered = filtered.FilterEvent_SelectedYears(yearsFilterListBox);
            return filtered;
        }
        private static List<EventModel> FilterEvent_SelectedMonths(this List<EventModel> filtered, ListBox monthsFilterListBox)
        {
            List<int> selectedMonths = new List<int>();
            foreach (var item in monthsFilterListBox.SelectedItems)
            {
                selectedMonths.Add((int)DateTime.ParseExact(item.ToString(), "MMM", CultureInfo.CurrentCulture).Month);
            }

            if (selectedMonths.Count > 0)
            {
                filtered = new List<EventModel>(filtered.Where(x => selectedMonths.Contains(x.EventDate.Month)));
            }

            return filtered;
        }
        private static List<EventModel> FilterEvent_SelectedYears(this List<EventModel> filtered, ListBox yearsFilterListBox)
        {
            List<int> selectedYears = new List<int>();
            foreach (var item in yearsFilterListBox.SelectedItems)
            {
                selectedYears.Add((int)DateTime.ParseExact(item.ToString(), "yyyy", CultureInfo.CurrentCulture).Year);
            }

            if (selectedYears.Count > 0)
            {
                filtered = new List<EventModel>(filtered.Where(x => selectedYears.Contains(x.EventDate.Year)));
            }
            return filtered;
        }

        public static List<EventModel> FilterEvent_SearchBox(this List<EventModel> filtered, string searchValue, bool includeEventDesc)
        {
            List<EventModel> searchFilter = new List<EventModel>();

            //If I always include event descrription then filtering won't filter out many events
            if (includeEventDesc)
            {
                searchFilter.AddRange(filtered.Where(x => x.EventDescription.ToLower().Contains(searchValue.ToLower())).ToList());
            }

            searchFilter.AddRange(filtered.Where(x => x.EventName.ToLower().Contains(searchValue)).ToList());
            searchFilter.AddRange(filtered.Where(x => x.EventLocation.Venue.VenueName.ToLower().Contains(searchValue)).ToList());
            searchFilter.AddRange(filtered.Where(x => x.EventLocation.VenueAddress.Area.ToLower().Contains(searchValue)).ToList());

            //Can't work out how to do this bit like above as it is a List within the List
            foreach (var filteredEvent in filtered)
            {
                //Take each event of the filtered list
                foreach (var eo in filteredEvent.EventOwners)
                {
                    //Loop through all the EventOwners of this event 
                    if (eo.Staff.FullName.ToLower().Contains(searchValue))
                    {
                        searchFilter.Add(filteredEvent);
                        continue;
                    }
                }

                foreach (var et in filteredEvent.EventType)
                {
                    //And loop through all the event types of this event
                    if (et.EventType.ToLower().Contains(searchValue))
                    {
                        searchFilter.Add(filteredEvent);
                        continue;
                    }
                }
            }

            return searchFilter;
        }
    }
}
