using Dapper;
using EventsAndInvitesLogic.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsAndInvitesLibrary.DataAccess
{
    class SQLConnection : IDataConnection
    {
        //private const string db = "SalesSmartConnection";
        private const string db = "EventManagementConnection";
        public void UpSertClient(ClientModel model)
        {

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                //Using Dapper
                var p = new DynamicParameters();

                //Add Parameters
                p.Add("@FirstName", model.FirstName);
                p.Add("@LastName", model.LastName);
                p.Add("@Title", model.Title);
                p.Add("@FormalSalutation", model.FormalSalutation);
                p.Add("@BusinessName", model.BusinessName);
                p.Add("@EmailAddress", model.EmailAddress);
                p.Add("@CellPhone", model.CellPhone);
                p.Add("@WorkPhone", model.WorkPhone);
                p.Add("@id", model.Id, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);

                connection.Execute("dbo.spUpSertClient", p, commandType: CommandType.StoredProcedure);

                //Now retrieve the new identity PK
                model.Id = p.Get<int>("@id");
            }
        }

        /// <summary>
        /// This inserts a new Event and updates and existing event based on EventModel.Id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void UpSertEvent(EventModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                //Using Dapper
                var p = new DynamicParameters();

                //Add Parameters
                p.Add("@EventName", model.EventName);
                p.Add("@EventDate", model.EventDate);
                p.Add("@EventLocationId", model.EventLocationId);
                p.Add("@EventDescription", model.EventDescription);
                p.Add("@MaxNumberOfGuests", model.MaxNumberOfGuests);
                p.Add("@EstimatedCost", model.EstimatedCost);
                p.Add("@StartTime", model.StartTime);
                p.Add("@EndTime", model.EndTime);
                p.Add("@id", model.Id, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);

                connection.Execute("dbo.spUpSertEvent_ByEventID", p, commandType: CommandType.StoredProcedure);

                //Now retrieve the new identity PK
                model.Id = p.Get<int>("@id");

                //Now loop delete any existing Event Owners and replace with new Event Owners

                //delete event owners where eventid = model.id
                p = new DynamicParameters();
                p.Add("@EventsId", model.Id);
                connection.Execute("dbo.spDeleteEventOwners_ByEventsId", p, commandType: CommandType.StoredProcedure);

                //loop through and add
                foreach (EventOwnerModel owner in model.EventOwners)
                {
                    if (owner.CostPercentage != 0) //No point loading someone if they are 0% - although hopefully no 0's would every get this far anyway
                    {
                        p = new DynamicParameters();
                        p.Add("@EventsId", model.Id);
                        p.Add("@StaffId", owner.StaffId);
                        p.Add("@CostPercentage", owner.CostPercentage);
                        p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                        connection.Execute("dbo.spCreateEventOwner", p, commandType: CommandType.StoredProcedure);

                        owner.Id = p.Get<int>("@id");
                    }
                }

                //Do Same for EventType
                p = new DynamicParameters();
                p.Add("@EventId", model.Id);
                connection.Execute("dbo.spDeleteEventTypesLookup_ByEventId", p, commandType: CommandType.StoredProcedure);

                foreach (EventTypeModel eventType in model.EventType)
                {
                    p = new DynamicParameters();
                    p.Add("@EventId", model.Id);
                    p.Add("@EventTypeId", eventType.Id);

                    p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                    connection.Execute("dbo.spCreateEventTypeList", p, commandType: CommandType.StoredProcedure);

                    eventType.Id = p.Get<int>("@id");
                }
            }
        }

        public void UpSertInviteInfo(InviteModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                if (model.Id == 0)
                {
                    p.Add("@EventId", model.Event.Id);
                    p.Add("@ClientId", model.Client.Id);
                }
                else
                {
                    p.Add("@EventId", 0);
                    p.Add("@ClientId", 0);
                }
                p.Add("@InviteApproved", model.InviteApproved);
                p.Add("@EmailSentDate", model.EmailSentDate);
                p.Add("@ClientAttending", model.ClientAttending);
                p.Add("@PlacesReserved", model.PlacesReserved);
                p.Add("@id", model.Id, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
                connection.Execute("dbo.spUpSertInvitationInfo_ByInviteId", p, commandType: CommandType.StoredProcedure);

                model.Id = p.Get<int>("@id");
            }
        }
        public void CreateEventType(EventTypeModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();

                //Add Parameters
                p.Add("@NewEventType", model.EventType);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute("dbo.spCreateEventType", p, commandType: CommandType.StoredProcedure);

                model.Id = p.Get<int>("@id");
            }
        }

        public void ExecuteSQL(string model)
        {
            //TODO - This probably shouldn't be used anywhere, but is good for passing test SQL through
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                connection.Query(model);
            }
        }

        public void DeleteEventTypes_ById(int Id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@Id", Id);
                connection.Query<StaffModel>("dbo.spDeleteEventTypes_ById", p, commandType: CommandType.StoredProcedure);
            }
        }
        public void DeleteInviteInfo(int EventInviteId)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();

                //Add Parameters
                p.Add("@id", EventInviteId);
                connection.Query("dbo.spDeleteEventInvite_ById", p, commandType: CommandType.StoredProcedure);
            }
        }
        public void CreateLocationId(LocationModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@VenueId", model.Venue.Id);
                p.Add("@VenueAddressId", model.VenueAddress.Id);
                p.Add("@Id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Query<StaffModel>("dbo.spCreateLocation", p, commandType: CommandType.StoredProcedure);

                model.Id = p.Get<int>("@Id");
            }
        }
        public void UpSertAddress_ByAddressId(AddressModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@AddressId", model.Id);
                p.Add("@AddressLine1", model.AddressLine1);
                p.Add("@AddressLine2", model.AddressLine2);
                p.Add("@AddressLine3", model.AddressLine3);
                p.Add("@AddressLine4", model.AddressLine4);
                p.Add("@Area", model.Area);
                p.Add("@PostCode", model.PostCode);
                p.Add("@AddressId", model.Id, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
                connection.Query<StaffModel>("dbo.spUpSertAddress_ByAddressId", p, commandType: CommandType.StoredProcedure);

                model.Id = p.Get<int>("@AddressId");
            }
        }
        public void DeleteAddress(int addressId)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@Id", addressId);
                connection.Query("dbo.spDeleteAddress_ByAddressId", p, commandType: CommandType.StoredProcedure);
            }
        }
        public void UpSertVenue_ByVenueId(VenueModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@VenueName", model.VenueName);
                p.Add("@Id", model.Id, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
                connection.Query<StaffModel>("dbo.spUpSertVenue_ByVenueId", p, commandType: CommandType.StoredProcedure);

                model.Id = p.Get<int>("@Id");
            }
        }

        public List<LocationModel> GetEventLocations_All()
        {
            List<LocationModel> output;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.Query<LocationModel>("dbo.spGetLocations_All").ToList();

                foreach (var item in output)
                {
                    var p = new DynamicParameters();
                    p.Add("@LocationId", item);
                    item.VenueAddress = connection.Query<AddressModel>("dbo.spGetAddress_ByLocationId", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }

            }

            return output;
        }
        public List<VenueModel> GetEventVenues_All()
        {
            List<VenueModel> output;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.Query<VenueModel>("dbo.spGetVenues_All").ToList();
            }

            return output;
        }

        public List<LocationModel> GetEventLocations_ByVenueName(string venueName)
        {
            List<LocationModel> output;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@VenueName", venueName);
                output = connection.Query<LocationModel>("dbo.spGetEvents_ByEventTypeID", p, commandType: CommandType.StoredProcedure).ToList();
            }

            return output;
        }

        public LocationModel GetEventLocation_ByLocationId(int locationId)
        {
            LocationModel output = new LocationModel();

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                output.Id = locationId;

                //Now add Venue Information
                var p = new DynamicParameters();
                p.Add("@LocationId", locationId);
                output.Venue = connection.Query<VenueModel>("dbo.spGetVenue_ByLocationId", p, commandType: CommandType.StoredProcedure).FirstOrDefault();

                //Now add Address Information
                p = new DynamicParameters();
                p.Add("@LocationId", locationId);
                output.VenueAddress = connection.Query<AddressModel>("dbo.spGetAddress_ByLocationId", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }

            return output;
        }
        public List<AddressModel> GetEventAddresses_ByVenueId(int venueId)
        {
            List<AddressModel> output;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {

                //Now add Venue Information
                var p = new DynamicParameters();
                p.Add("@VenueId", venueId);
                output = connection.Query<AddressModel>("dbo.spGetAddresses_ByVenueId", p, commandType: CommandType.StoredProcedure).ToList();

            }

            return output;
        }
        public void GetLocationId_ByLocationInfo(LocationModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@VenueId", model.Venue.Id);
                p.Add("@VenueAddressId", model.VenueAddress.Id);
                p.Add("@Id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Query<int>("dbo.spGetLocationId_ByLocationInfo", p, commandType: CommandType.StoredProcedure);

                model.Id = p.Get<int>("@Id");
            }
        }

        public List<EventTypeModel> GetEventTypes_All()
        {
            List<EventTypeModel> output;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.Query<EventTypeModel>("dbo.spGetEventTypes_All").ToList();
            }

            return output;
        }

        public List<EventTypeModel> GetEventTypes_InUse()
        {
            List<EventTypeModel> output;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.Query<EventTypeModel>("dbo.spGetEventTypes_InUse").ToList();
            }

            return output;
        }

        public List<EventModel> GetEvents_All()
        {
            List<EventModel> output;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.Query<EventModel>("dbo.spGetEvents_All").ToList();
                foreach (EventModel model in output)
                {
                    model.EventOwners = GlobalConfig.Connection.GetEventOwners_ByEventId(model.Id);

                    model.EventType = GlobalConfig.Connection.GetEventTypes_ByEventId(model.Id);

                    model.EventLocation = GlobalConfig.Connection.GetEventLocation_ByLocationId(model.EventLocationId);
                }
            }
            return output;
        }
        public List<EventTypeModel> GetEventTypes_ByEventId(int eventId)
        {
            List<EventTypeModel> output;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@EventId", eventId);
                output = connection.Query<EventTypeModel>("dbo.spGetEventTypes_ByEventId", p, commandType: CommandType.StoredProcedure).ToList();
            }

            return output;
        }

        public List<EventModel> GetEvent_ByEventTypeId(int eventId)
        {
            List<EventModel> output;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@EventTypeID", eventId);
                output = connection.Query<EventModel>("dbo.spGetEvents_ByEventTypeID", p, commandType: CommandType.StoredProcedure).ToList();
            }
            return output;
        }

        public List<ClientModel> GetInviteableClients_ByEventID(int eventId)
        {
            List<ClientModel> output;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@EventId", eventId);
                output = connection.Query<ClientModel>("dbo.spGetInviteableClients_ByEventId", p, commandType: CommandType.StoredProcedure).ToList();
            }

            return output;
        }
        public List<ClientModel> GetClients_All()
        {
            List<ClientModel> output;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.Query<ClientModel>("dbo.spGetClients_All").ToList();
            }

            return output;
        }
        public List<InviteModel> GetInviteList_ByEvent(int eventId)
        {
            List<InviteModel> output;
            List<ClientModel> allClients = GlobalConfig.Connection.GetClients_All();
            List<EventModel> allEvents = GlobalConfig.Connection.GetEvents_All();

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@EventId", eventId);
                output = connection.Query<InviteModel>("dbo.spGetInvitationList_ByEvent", p, commandType: CommandType.StoredProcedure).ToList();

                //Populate Clients
                foreach (InviteModel guest in output)
                {
                    guest.Client = allClients.Where(x => x.Id == guest.ClientId).FirstOrDefault();
                    guest.Event = allEvents.Where(x => x.Id == guest.EventId).FirstOrDefault();
                }
            }

            return output;
        }

        public List<EventModel> GetEvents_ByVenueAddressId(AddressModel model)
        {
            List<EventModel> output;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@VenueAddressId", model.Id);
                output = connection.Query<EventModel>("dbo.spGetEvents_ByVenueAddressId", p, commandType: CommandType.StoredProcedure).ToList();
            }

            return output;
        }
        public List<EventModel> GetEvents_ByVenueId(VenueModel model)
        {
            List<EventModel> output;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@VenueId", model.Id);
                output = connection.Query<EventModel>("dbo.spGetEvents_ByVenueId", p, commandType: CommandType.StoredProcedure).ToList();
            }

            return output;
        }



        public List<StaffModel> GetStaff_All()
        {
            List<StaffModel> output;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.Query<StaffModel>("dbo.spGetStaff_All").ToList();
            }

            return output;
        }
        public List<StaffModel> GetStaff_Actives()
        {
            List<StaffModel> output;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.Query<StaffModel>("dbo.spGetStaff_Actives").ToList();
            }

            return output;
        }
        public StaffModel GetStaff_ById(int StaffId)
        {
            StaffModel output;
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@Id", StaffId);
                output = connection.Query<StaffModel>("dbo.spGetStaff_ById", p, commandType: CommandType.StoredProcedure).First();

            }
            return output;
        }
        public List<EventOwnerModel> GetEventOwners_ByEventId(int eventId)
        {
            List<EventOwnerModel> output;
            List<StaffModel> allStaff = GlobalConfig.Connection.GetStaff_All();

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@EventId", eventId);
                output = connection.Query<EventOwnerModel>("dbo.spGetEventOwners_ByEventId", p, commandType: CommandType.StoredProcedure).ToList();

                //Populate Staff
                foreach (EventOwnerModel staff in output)
                {
                    staff.Staff = allStaff.Where(x => x.Id == staff.StaffId).FirstOrDefault();
                }
            }

            return output;
        }
        public List<string> spGetAddressArea_All()
        {
            List<string> output;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.Query<string>("dbo.spGetAddressArea_All").ToList();
            }

            return output;
        }
        public List<string> GetInvitationQuestions_ByEventId(int EventId)
        {
            List<string> output;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@EventId", EventId);
                output = connection.Query<string>("dbo.spGetInvitationQuestion_ByEventId", p, commandType: CommandType.StoredProcedure).ToList();
            }

            return output;
        }

        public void CreateInvitationQuestion(int ColNo, string QuestionText)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@ColNum", ColNo);
                p.Add("@QuestionText", QuestionText);
                connection.Execute("dbo.spCreateInvitationQuestion", p, commandType: CommandType.StoredProcedure);
            }
        }

        public void DeleteInvitationQuestions(int EventId)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@EventId", EventId);
                connection.Execute("dbo.spDeleteInvitationQuestions_ByEventId", p, commandType: CommandType.StoredProcedure);
            }
        }
        public void UpdateInvitationQuestions(int EventId)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@EventId", EventId);
                connection.Execute("dbo.spUpdateInvitationQuestions_NewEventId", p, commandType: CommandType.StoredProcedure);
            }
        }
        public int GetClientID(int EventId, string EmailAddress)
        {
            int output;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@EventId", EventId);
                p.Add("@Email", EmailAddress);
                output = connection.Query<int>("dbo.spGetClientID_ByEmailAndEventID", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }

            return output;
        }
        public int GetInviteId(int EventId, int ClientId)
        {
            int output;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@EventId", EventId);
                p.Add("@ClientId", ClientId);
                output = connection.Query<int>("dbo.spGetInviteId_ByEventIdAndClientId", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }

            return output;
        }
        public void CreateInvitationResponse(int InviteId, int ColumnNumber, string Response)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@InviteId", InviteId);
                p.Add("@ColumnNumber", ColumnNumber);
                p.Add("@Response", Response);

                connection.Execute("dbo.spCreateInvitationResponse", p, commandType: CommandType.StoredProcedure);
            }
        }
        public void DeleteInvitationQuestionResponses_ByInviteId(int InviteId)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@InviteId", InviteId);
                connection.Execute("dbo.spDeleteInvitationQuestionResponses_ByInviteId", p, commandType: CommandType.StoredProcedure);
            }
        }
        public void DeleteInvitationQuestionResponses_ByEventId(int EventId)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@EventId", EventId);
                connection.Execute("dbo.spDeleteInvitationQuestionResponses_ByEventId", p, commandType: CommandType.StoredProcedure);
            }
        }
        public void DeleteAttendingResponses(int EventId)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@EventId", EventId);
                connection.Execute("dbo.spDeleteAttendingResponses_ByEventId", p, commandType: CommandType.StoredProcedure);
            }
        }
        public int GetCustomQuestionCount(int EventId)
        {
            int output;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@EventId", EventId);
                output = connection.Query<int>("dbo.spGetCustomQuestionCount_ByEventId", p, commandType: CommandType.StoredProcedure).FirstOrDefault(); ;

            }
            return output;
        }

        public List<string> GetCustomQuestions(int EventId)
        {
            List<string> output;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@EventId", EventId);
                output = connection.Query<string>("dbo.spGetInvitationQuestion_ByEventId", p, commandType: CommandType.StoredProcedure).ToList();
            }

            return output;
        }

        public DataTable GetClientsInvitationAnswers(int ClientId, int EventId)
        {
            DataTable output = new DataTable();

            using (var adapter = new SqlDataAdapter($"EXEC spGetClientsInvitationQuestionAnswers { ClientId } , { EventId }", GlobalConfig.CnnString(db)))
            {
                adapter.Fill(output);
            }
            return output;
        }
    }
}
