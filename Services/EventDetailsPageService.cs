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
//created by Brian
//Event Details Page Service
namespace Services.Events
{
    public class EventDetailsPageService : BaseService, IEventDetailsPageService
    {
        public EventDetailsPage GetById(int id, int userBaseId)
        {
            EventDetailsPage eventDetails = new EventDetailsPage();
            DataProvider.ExecuteCmd("dbo.events_selecteventbyid",

                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", id);
                    paramCollection.AddWithValue("@UserBaseId", userBaseId);
                },
                singleRecordMapper: (IDataReader reader, short set) =>
                {
                    eventDetails = (Tools.DataMapper<EventDetailsPage>.Instance.MapToObject(reader));//What is tools?
                });
            return eventDetails;
        }
    }
}
