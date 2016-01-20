using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CustomerMsgApp.Service;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using System.Runtime.CompilerServices;
using CustomerMsgApp.Model;
using System.Threading.Tasks;

namespace CustomerMsgApp.Controllers
{
    public class CustomerAPIController : ApiController
    {
        public NotificationService _NotificationService;

        public CustomerAPIController()
        {
            _NotificationService = new NotificationService();
        }

        #region Method
        // GET api/userapi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/userapi/5
        public string Get(int? id)
        {
            return "value";
        }

        // POST api/userapi
        public void Post([FromBody]string value)
        {
        }

        // PUT api/userapi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpPost]
        public dynamic UserLogin(Login Login)
        {
            try
            {
                if (Login.UserName != null && Login.Password != null)
                {
                    var loginModel = _NotificationService.Login(Login.UserName, Login.Password);
                    if (loginModel != null)
                    {
                        return loginModel;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        public bool CheckExists(NotificationSearch NotificationSearch)
        {
            try
            {
                var loginModel = _NotificationService.CheckExists(NotificationSearch);
                if (loginModel != null)
                {
                    return loginModel;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        public dynamic GetAllNotification(NotificationSearch NotificationSearch)
        {
            try
            {
                dynamic NotificationList;
                NotificationList = _NotificationService.GetAllCustomer(NotificationSearch);
                return NotificationList;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [HttpPost]
        public dynamic GetAllMessageLog(MessageLogSearch MessageLogSearch)
        {
            try
            {
                dynamic NotificationList;
                NotificationList = _NotificationService.GetAllMessageLog(MessageLogSearch);
                return NotificationList;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpPost]
        public string CheckPassword(NotificationSearch NotificationSearch)
        {
            string pwd = System.Configuration.ConfigurationManager.AppSettings["Password"];
            if (NotificationSearch.Password.ToLower() == pwd.ToLower() && NotificationSearch.SendCountSMS == NotificationSearch.MobileCount)
            {
                SendMessage(NotificationSearch, NotificationSearch.SendCountSMS);
                //TODO: Handle response and errors
                return "1";
            }
            else if (NotificationSearch.Password.ToLower() != pwd.ToLower())
            {
                return "3";
            }
            else if (NotificationSearch.SendCountSMS != NotificationSearch.MobileCount)
            {
                return "2";
            }
            else
            {
                return "1";
            }
        }

        [HttpPost]
        public void SendMessage(NotificationSearch NotificationSearch, int approvedCount)
        {
            var testMode = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["TestMode"]);
            string pwd = System.Configuration.ConfigurationManager.AppSettings["Password"];
            if (NotificationSearch.Password.ToLower() == pwd.ToLower())
            {
                var NotificationList = _NotificationService.GetCustomerSendMessage(NotificationSearch, approvedCount)
                    .Select(n => new
                    {
                        n.MobileNo,
                        n.TourOpCode,
                        n.Email,
                        n.Seed,
                        n.FirstName,
                        n.Surname
                    }).Distinct();

                if (NotificationList != null)
                {
                    foreach (var item in NotificationList)
                    {
                        if (!string.IsNullOrEmpty(item.Email))
                        {
                            try
                            {
                                if (!string.IsNullOrEmpty(NotificationSearch.MessageEmail))
                                {
                                    var response = NotificationService.SendEmail("noreply@hotelplan.co.uk", item.Email, "Subject", NotificationSearch.MessageEmail);
                                    NotificationService.NotificationLogAdd(1, item.Email, item.FirstName, item.Surname, "noreply@hotelplan.co.uk", NotificationSearch.MessageEmail, response, response, item.Seed, testMode);
                                }
                                else
                                {
                                    var response = NotificationService.SendEmail("noreply@hotelplan.co.uk", item.Email, "Subject", NotificationSearch.Message);
                                    NotificationService.NotificationLogAdd(1, item.Email, item.FirstName, item.Surname, "noreply@hotelplan.co.uk", NotificationSearch.Message, response, response, item.Seed, testMode);
                                }
                            }
                            catch (Exception e)
                            {
                                NotificationService.NotificationLogAdd(0, item.Email, item.FirstName, item.Surname, "noreply@hotelplan.co.uk", NotificationSearch.Message, "Error", e.Message, item.Seed, testMode);
                            }
                        }
                        if (!string.IsNullOrEmpty(item.MobileNo))
                        {
                            try
                            {
                                var numberToSendTo = item.MobileNo;
                                var numberFrom = "Hotelplan";
                                var message = NotificationSearch.Message;

                                if (item.MobileNo.Substring(0, 1) == "0")
                                {
                                    numberToSendTo = string.Format("+44{0}", item.MobileNo.Substring(1, item.MobileNo.Length - 1));
                                }

                                numberToSendTo = numberToSendTo.Replace(" ", "");

                                switch (item.TourOpCode)
                                {
                                    case "IN":
                                        numberFrom = "Inghams";
                                        break;
                                    case "IT":
                                        numberFrom = "Inghams";
                                        break;
                                    case "ST":
                                        numberFrom = "Ski Total";
                                        break;
                                    case "ES":
                                        numberFrom = "Esprit Sun";
                                        break;
                                    case "EW":
                                        numberFrom = "Esprit Ski";
                                        break;
                                    case "SL":
                                        numberFrom = "Santa";
                                        break;
                                }

                                if (item.Seed)
                                {
                                    message = string.Format("Seed:{0}", message);
                                }

                                var notificationResponse = "open";
                                if (!testMode)
                                {
                                    notificationResponse = NotificationService.SendSMS(numberFrom, numberToSendTo, message);
                                }
                                NotificationService.NotificationLogAdd(2, numberToSendTo, item.FirstName, item.Surname, numberFrom, message, notificationResponse, notificationResponse, item.Seed, testMode);
                            }
                            catch (Exception e)
                            {
                                NotificationService.NotificationLogAdd(0, item.MobileNo, item.FirstName, item.Surname, "", NotificationSearch.Message, "Error", e.Message, item.Seed, testMode);
                            }
                        }
                    }
                }
            }
        }

        public IEnumerable<string> GetTourOpCode()
        {
            try
            {
                dynamic NotificationList;
                NotificationList = _NotificationService.GetTourOpCode().OrderBy(x => x);
                return NotificationList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<string> GetDeparturePoint()
        {
            try
            {
                dynamic NotificationList;
                NotificationList = _NotificationService.GetDeparturePoint();
                return NotificationList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<string> GetArrivalPoint()
        {
            try
            {
                dynamic NotificationList;
                NotificationList = _NotificationService.GetArrivalPoint();
                return NotificationList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<string> GetTravelDate()
        {
            try
            {
                dynamic NotificationList;
                NotificationList = _NotificationService.GetTravelDate();
                return NotificationList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<string> GetTravelDirection()
        {
            try
            {
                dynamic NotificationList;
                NotificationList = _NotificationService.GetTravelDirection();
                return NotificationList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<string> GetTransportCarrier()
        {
            try
            {
                dynamic NotificationList;
                NotificationList = _NotificationService.GetTransportCarrier();
                return NotificationList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<string> GetTransportNumber()
        {
            try
            {
                dynamic NotificationList;
                NotificationList = _NotificationService.GetTransportNumber();
                return NotificationList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<string> GetTransportType()
        {
            try
            {
                dynamic NotificationList;
                NotificationList = _NotificationService.GetTransportType();
                return NotificationList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<string> GetTransportChain()
        {
            try
            {
                dynamic NotificationList;
                NotificationList = _NotificationService.GetTransportChain();
                return NotificationList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public dynamic GetCountryName()
        {
            try
            {
                dynamic NotificationList;
                NotificationList = _NotificationService.GetCountryName();
                return NotificationList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public dynamic GetResortName(string Id)
        {
            try
            {
                dynamic NotificationList;
                NotificationList = _NotificationService.GetResortName(Id);
                return NotificationList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public dynamic GetAccommodationName(string Id)
        {
            try
            {
                dynamic NotificationList;
                NotificationList = _NotificationService.GetAccommodationName(Id);
                return NotificationList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        public class Login
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }
    }
}