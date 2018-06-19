using Models.Domain.Events;
using Models.Requests.Events;
using Models.Responses;
using Services;
using Services.Interfaces.Events;
using Services.Interfaces.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
//Created by Brian
namespace Prospect.Web.Controllers.Api.Events
{
    [RoutePrefix("api/events")]
    public class EventController : ApiController
    {
        IEventService _eventService;
        IErrorLogService _errorLogService;
        IAuthenticationService _authenticationService;
        public ProspectEventController(IEventService eventService, IErrorLogService errorLogService, IAuthenticationService authenticationService)
        {
            _eventService = eventService;
            _errorLogService = errorLogService;
            _authenticationService = authenticationService;
        }

        [Route(), HttpGet]
        public IHttpActionResult GetAll()
        {
            try
            {
                ItemsResponse<Event> response = new ItemsResponse<Event>
                {
                    Items = _eventService.GetAll()
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

        [AllowAnonymous]
        [Route("recent"), HttpGet]
        public IHttpActionResult GetRecentEvents()
        {
            try
            {
                ItemsResponse<Event> response = new ItemsResponse<Event>
                {
                    Items = _eventService.GetRecentEvents()
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

        [Route("{id:int}"), HttpGet]
        public IHttpActionResult GetById(int id)
        {
            try
            {

                ItemResponse<Event> response = new ItemResponse<Event>
                {
                    Item = _eventService.GetById(id)
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


        [Route("currentuser"), HttpGet]
        public IHttpActionResult GetEventsByAthleteId()
        {
            try
            {
                int id = _authenticationService.GetCurrentUserId();//dependency injection - prospect event class uses authentication service class

                ItemsResponse<Event> response = new ItemsResponse<Event>
                {
                    Items = _eventService.GetEventsByAthleteId(id)
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

        [Route(), HttpPost]
        public IHttpActionResult Post(EventAddRequest model)
        {
            try
            {
                model.CreatedById = _authenticationService.GetCurrentUserId();
                if (!ModelState.IsValid) { return new ResponseMessageResult(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState)); }
                ItemResponse<int> response = new ItemResponse<int>
                {
                    Item = _eventService.Post(model)
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

        [Route("{id:int}"), HttpPut]
        public IHttpActionResult Put(EventUpdateRequest model)
        {
            try
            {
                if (!ModelState.IsValid) { return BadRequest(ModelState); }

                _eventService.Put(model);

                return Ok(new SuccessResponse());


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

        [Route("{id:int}"), HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {

                _eventService.Delete(id);


                return Ok(new SuccessResponse());

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
        [AllowAnonymous]
        [Route("public/{id:int}"), HttpGet]
        public IHttpActionResult GetEventPublicInfo(int id)
        {
            try
            {
                ItemResponse<EventPublic> response = new ItemResponse<EventPublic>()
                {
                    Item = _eventService.GetEventPublicInfo(id)
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
