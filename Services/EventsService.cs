using Models.Domain.Campaigns;
using Models.Domain.Events;
using Models.Requests.Events;
using Services.Interfaces.Events;
using Services.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Events
{
    public class EventService : BaseService, IEventService
    {
        //Get all events
        public List<Event> GetAll()
        {
            List<Event> list = new List<Event>();
            DataProvider.ExecuteCmd("dbo.Events_SelectAll",
               inputParamMapper: null,
               singleRecordMapper: (IDataReader reader, short set) =>
               {
                   list.Add(DataMapper<Event>.Instance.MapToObject(reader));
               });
            return list;
        }
        
        //Get Events By Id
        public Event GetById(int id)
        {
            Event event = new Event();
            DataProvider.ExecuteCmd("dbo.Events_SelectById",

                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", id);
                },
                singleRecordMapper: (IDataReader reader, short set) =>
                {
                    event = DataMapper<Event>.Instance.MapToObject(reader);
                });
            return event;
        }
        
        //Get Events By Athlete Id's
        public List<Event> GetEventsByAthleteId(int id)
        {
            List<Event> list = new List<Event>();
            DataProvider.ExecuteCmd("dbo.Events_GetEventsByAthleteId",

                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", id);
                },
                singleRecordMapper: (IDataReader reader, short set) =>
                {
                    list.Add(DataMapper<Event>.Instance.MapToObject(reader));
                });
            return list;
        }

        //Create new Events
        public int Post(EventAddRequest model)
        {
            int id = 0;

            if (!model.ExternalSiteUrl.Contains("http:"))
                model.ExternalSiteUrl = "http://" + model.ExternalSiteUrl;

            DataProvider.ExecuteNonQuery("dbo.Events_Insert",

               inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@AddressTypeId", model.AddressTypeId);
                   paramCollection.AddWithValue("@StreetAddress", model.StreetAddress);
                   paramCollection.AddWithValue("@City", model.City);
                   paramCollection.AddWithValue("@StateProvinceId", model.StateProvinceId);
                   paramCollection.AddWithValue("@PostalCode", model.PostalCode);


                   paramCollection.AddWithValue("@EventName", model.EventName);
                   paramCollection.AddWithValue("@EventTypeId", model.EventTypeId);
                   paramCollection.AddWithValue("@EventDescription", model.EventDescription);
                   paramCollection.AddWithValue("@AddressId", model.AddressId);
                   paramCollection.AddWithValue("@StartDate", model.StartDate);
                   paramCollection.AddWithValue("@EndDate", model.EndDate);
                   paramCollection.AddWithValue("@StartTime", model.StartTime);
                   paramCollection.AddWithValue("@EndTime", model.EndTime);
                   paramCollection.AddWithValue("@IsAllDayEvent", model.IsAllDayEvent);
                   paramCollection.AddWithValue("@CanRepeat", model.CanRepeat);
                   paramCollection.AddWithValue("@TicketPrice", model.TicketPrice);
                   paramCollection.AddWithValue("@PhotoUrl", model.PhotoUrl);
                   paramCollection.AddWithValue("@ExternalSiteUrl", model.ExternalSiteUrl);
                   paramCollection.AddWithValue("@IsAdminApproved", model.IsAdminApproved);
                   paramCollection.AddWithValue("@CreatedById", model.CreatedById);
                   paramCollection.AddWithValue("@ModifiedById", model.ModifiedById);


                   SqlParameter paramId = new SqlParameter("@Id", SqlDbType.Int);
                   paramId.Direction = ParameterDirection.Output;
                   paramId.Value = id;
                   paramCollection.Add(paramId);


               },
                 returnParameters: delegate (SqlParameterCollection paramCollection)
                 {
                     int.TryParse(paramCollection["@Id"].Value.ToString(), out id);
                 });
            return id;
        }
        
        //Update events
        public void Put(EventUpdateRequest model)
        {

            DataProvider.ExecuteNonQuery("dbo.Events_Update",
                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                 {
                     paramCollection.AddWithValue("@AddressTypeId", model.AddressTypeId);
                     paramCollection.AddWithValue("@StreetAddress", model.StreetAddress);
                     paramCollection.AddWithValue("@City", model.City);
                     paramCollection.AddWithValue("@StateProvinceId", model.StateProvinceId);
                     paramCollection.AddWithValue("@PostalCode", model.PostalCode);

                     paramCollection.AddWithValue("@Id", model.Id);
                     paramCollection.AddWithValue("@EventName", model.EventName);
                     paramCollection.AddWithValue("@EventTypeId", model.EventTypeId);
                     paramCollection.AddWithValue("@EventDescription", model.EventDescription);
                     paramCollection.AddWithValue("@AddressId", model.AddressId);
                     paramCollection.AddWithValue("@StartDate", model.StartDate);
                     paramCollection.AddWithValue("@EndDate", model.EndDate);
                     paramCollection.AddWithValue("@StartTime", model.StartTime);
                     paramCollection.AddWithValue("@EndTime", model.EndTime);
                     paramCollection.AddWithValue("@IsAllDayEvent", model.IsAllDayEvent);
                     paramCollection.AddWithValue("@CanRepeat", model.CanRepeat);
                     paramCollection.AddWithValue("@TicketPrice", model.TicketPrice);
                     paramCollection.AddWithValue("@PhotoUrl", model.PhotoUrl);
                     paramCollection.AddWithValue("@ExternalSiteUrl", model.ExternalSiteUrl);
                     paramCollection.AddWithValue("@IsAdminApproved", model.IsAdminApproved);
                     paramCollection.AddWithValue("@CreatedById", model.CreatedById);
                     paramCollection.AddWithValue("@ModifiedById", model.ModifiedById);
                 });
        }
        
        //Delete Events
        public void Delete(int id)
        {
            DataProvider.ExecuteNonQuery("dbo.Events_Delete",
                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", id);
                });


        }
    }
}


