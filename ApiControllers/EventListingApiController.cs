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
    [RoutePrefix("api/events")]
    public class EventListingController : ApiController
    {
       IEventListingService _eventListingService;
       IErrorLogService _errorLogService;
       IAuthenticationService _authenticationService;
       public EventListingController(IEventListingService eventListingService, IErrorLogService errorLogService, IAuthenticationService authenticationService)
       {
            _eventListingService = eventListingService;
            _errorLogService = errorLogService;
            _authenticationService = authenticationService;
       }

        [Route("currentfan"), HttpGet]
        public IHttpActionResult GetEventsByFollowerId()
        {
            try
            {
                int UserBaseId = _authenticationService.GetCurrentUserId();//dependency injection - event class uses authentication service class

                ItemsResponse<EventListing> response = new ItemsResponse<EventListing>
                {
                    Items = _eventListingService.GetEventsByFollowerId(UserBaseId)
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
