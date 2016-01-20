using CustomerMsgApp.Model;
using CustomerMsgApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CustomerMsgApp.Controllers
{
    public class NotificationResponseController : ApiController
    { //http://localhost:49732/api/NotificationResponseController/SavenotificationResponse
        [HttpPost]
        public HttpResponseMessage SavenotificationResponse(NotificationMessageReceived NotificationMessageReceived)
        {
            using (var db = new CustomerContext())
            {
                db.NotificationMessageReceived.Add(NotificationMessageReceived);
                db.SaveChanges();

                string message = System.Configuration.ConfigurationManager.AppSettings["AckMessage"];
                NotificationService.SendSMS("FromNo", NotificationMessageReceived.From, message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, NotificationMessageReceived);
        }
    }
}
