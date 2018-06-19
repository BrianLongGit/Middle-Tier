using Models.Domain.Events;
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
    public class EventListingService : BaseService, IEventListingService
    {
        //Get Events By Follower Id
        public List<EventListing> GetEventsByFollowerId(int id)
        {
            List<EventListing> list = new List<EventListing>();
            DataProvider.ExecuteCmd("dbo.Events_SelectAllEventsFollowingUserId",

                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@UserBaseId", id);
                },
                singleRecordMapper: (IDataReader reader, short set) =>
                {
                    list.Add(DataMapper<EventListing>.Instance.MapToObject(reader));
                });
            return list;
        }
        //Get Events By Event Type Id
        public List<EventListing> GetEventsByEventTypeId(int EventTypeId)
        {
            List<EventListing> list = new List<EventListing>();
            DataProvider.ExecuteCmd("dbo.Events_SelectEventsByEventTypeId",

                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@EventTypeId", EventTypeId);
                },
                singleRecordMapper: (IDataReader reader, short set) =>
                {
                    list.Add(DataMapper<EventListing>.Instance.MapToObject(reader));
                });
            return list;
        }
    }
}
