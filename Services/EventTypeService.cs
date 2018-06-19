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
    public class EventTypeService: BaseService, IEventTypeService
    {
        // Get all event types
        public List<EventType> GetAll()
        {
            List<EventType> list = new List<EventType>();
            DataProvider.ExecuteCmd("dbo.Events_EventType_SelectAll",
               inputParamMapper: null,
               singleRecordMapper: (IDataReader reader, short set) =>
                {
                    list.Add(DataMapper<EventType>.Instance.MapToObject(reader));
                });
            return list;
        }   
        //Get Event Type by Id
        public EventType GetById(int id)
        {
            EventType eventType = new EventType();
            DataProvider.ExecuteCmd("dbo.Events_EventType_SelectById",

                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", id);
                },
                singleRecordMapper: (IDataReader reader, short set) =>
                {
                    eventType= DataMapper<EventType>.Instance.MapToObject(reader);
                });
            return eventType;
        }
        //Insert new Event Type
        public int Post(EventTypeAddRequest model)
        {
            int id = 0;

            DataProvider.ExecuteNonQuery("dbo.Events_EventType_Insert", 
                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@TypeName", model.TypeName);
                    paramCollection.AddWithValue("@TypeDescription", model.TypeDescription);

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
        //Update Event Type
        public void Put(EventTypeUpdateRequest model)
        {
            DataProvider.ExecuteNonQuery("dbo.Events_EventType_Update",
                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", model.Id);
                    paramCollection.AddWithValue("@TypeName", model.TypeName);
                    paramCollection.AddWithValue("@TypeDescription", model.TypeDescription);
                });
        }
        //Delete Event Type
        public void Delete(int id)
        {
            DataProvider.ExecuteNonQuery("dbo.Events_EventType_Delete",
                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {

                    paramCollection.AddWithValue("@Id", id);
                }
             );

        }

    }
}




