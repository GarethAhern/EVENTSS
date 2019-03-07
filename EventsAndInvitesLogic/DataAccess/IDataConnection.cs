using EventsAndInvitesLogic.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsAndInvitesLibrary.DataAccess
{
    public interface IDataConnection
    {
        void DeleteInviteInfo(int EventInviteId);
        void UpSertClient(ClientModel model);
        void UpSertEvent(EventModel model);
        void UpSertInviteInfo(InviteModel model);
        void DeleteAddress(int addressId);
        void UpSertAddress_ByAddressId(AddressModel model);
        void UpSertVenue_ByVenueId(VenueModel model);
        /// <summary>
        /// You can only create or delete an event type
        /// I don't want people changing the name of an event type because it will affect all linked events
        /// </summary>
        /// <param name="model"></param>
        void CreateEventType(EventTypeModel model);

        void CreateLocationId(LocationModel model);


        List<LocationModel> GetEventLocations_All();
        List<VenueModel> GetEventVenues_All();
        List<LocationModel> GetEventLocations_ByVenueName(string venueName);
        List<AddressModel> GetEventAddresses_ByVenueId(int venueId);
        void GetLocationId_ByLocationInfo(LocationModel model);
        LocationModel GetEventLocation_ByLocationId(int locationId);
        /// <summary>
        /// Returns a list of Address Areas e.g Auckalnd, Wellington, Christchurch etc
        /// </summary>
        /// <returns></returns>
        List<string> spGetAddressArea_All();




        List<EventTypeModel> GetEventTypes_All();
        List<EventTypeModel> GetEventTypes_ByEventId(int eventId);
        List<EventTypeModel> GetEventTypes_InUse();
        List<ClientModel> GetInviteableClients_ByEventID(int eventId = 0);
        List<ClientModel> GetClients_All();
        List<EventModel> GetEvent_ByEventTypeId(int eventId);

        List<StaffModel> GetStaff_All();
        List<StaffModel> GetStaff_Actives();
        StaffModel GetStaff_ById(int StaffId);
        List<EventOwnerModel> GetEventOwners_ByEventId(int eventId);
        List<EventModel> GetEvents_All();

        List<InviteModel> GetInviteList_ByEvent(int eventId);

        List<EventModel> GetEvents_ByVenueAddressId(AddressModel model);
        List<EventModel> GetEvents_ByVenueId(VenueModel model);

        void DeleteEventTypes_ById(int Id);
        void ExecuteSQL(string model);



        //Invitaiton Questions
        List<string> GetInvitationQuestions_ByEventId(int EventId);
        void CreateInvitationQuestion(int ColNo, string QuestionText);
        void DeleteInvitationQuestions(int EventId);
        void UpdateInvitationQuestions(int EventId);
        int GetClientID(int EventId, string EmailAddress);
        int GetInviteId(int EventId, int ClientId);
        void CreateInvitationResponse(int InviteId, int ColumnNumber, string Response);
        void DeleteInvitationQuestionResponses_ByInviteId(int InviteId);
        void DeleteInvitationQuestionResponses_ByEventId(int InviteId);
        void DeleteAttendingResponses(int EventId);
        int GetCustomQuestionCount(int EventId);
        List<string> GetCustomQuestions(int EventId);
        DataTable GetClientsInvitationAnswers(int ClientId, int EventId);

    }
}
