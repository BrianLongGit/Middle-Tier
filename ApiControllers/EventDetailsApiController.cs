using Models.Domain.Events;
using Models.Responses;
using Services;
using Services.Interfaces.Events;
using Services.Interfaces.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Web.Controllers.Api.Events
{
    [RoutePrefix("api/eventdetails")]
    public class EventDetailsController : ApiController
    {
        IEventDetailsPageService _eventDetailsPageService;
        IEventListingService _eventListingService;
        IErrorLogService _errorLogService;
        IAuthenticationService _authenticationService;


        public EventDetailsController(IEventDetailsPageService eventDetailsPageService, IErrorLogService errorLogService, IEventListingService eventListingService, IAuthenticationService authenticationService)
        {
            _eventDetailsPageService = eventDetailsPageService;
            _eventListingService = eventListingService;
            _errorLogService = errorLogService;
            _authenticationService = authenticationService;
        }

        [Route("{id:int}"), HttpGet]
        public IHttpActionResult GetById(int id)
        {
            try
            {
               
               int userBaseId = _authenticationService.GetCurrentUserId();

                ItemResponse<EventDetailsPage> response = new ItemResponse<EventDetailsPage>
                {
                    Item = _eventDetailsPageService.GetById(id, userBaseId)
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                _errorLogService.Post(new Models.Requests.Logs.ErrorLogAddRequest
                {
                    ErrorSourceTypeId = 1,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    Title = "Error in " + GetType().Name + " " + System.Reflection.MethodBase.GetCurrentMethod().Name
                });
                return BadRequest(ex.Message);
            }
        }

        [Route("events/{id:int}"), HttpGet]
        public IHttpActionResult GetEventsByEventTypeId(int id)
        {
            try
            {
               

                ItemsResponse<EventListing> response = new ItemsResponse<EventListing>
                {
                    Items = _eventListingService.GetEventsByEventTypeId(id)
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                _errorLogService.Post(new Models.Requests.Logs.ErrorLogAddRequest
                {
                    ErrorSourceTypeId = 1,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    Title = "Error in " + GetType().Name + " " + System.Reflection.MethodBase.GetCurrentMethod().Name
                });
                return BadRequest(ex.Message);

            }
        }
    }
}
