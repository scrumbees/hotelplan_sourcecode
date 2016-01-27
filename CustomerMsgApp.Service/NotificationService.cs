using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerMsgApp.Model;
using System.Net.Mail;
using System.Configuration;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using Twilio;
using System.Data.Entity;

namespace CustomerMsgApp.Service
{
    public partial class NotificationService
    {
        private readonly CustomerContext _CustomerService;

        public NotificationService()
        {
            this._CustomerService = new CustomerContext();
        }

        public dynamic Login(string UserName, string Password)
        {
            if (UserName == "" || Password == "")
            {
                return false;
            }

            var user = (from u in _CustomerService.User
                        join r in _CustomerService.UserRole on u.Id equals r.UserId where u.UserName == UserName && u.Password == Password
                        select new
                        {
                            Id = u.Id,
                            UserName = u.UserName,
                            Password = u.Password,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            MobileNo = u.MobileNo,
                            Email = u.Email,
                            RoleId = r.RoleId
                        });

            string uname = UserName;
            string pwd = Password;

            if (UserName != string.Empty && Password != string.Empty)
            {

                if(user == null)
                {
                    return null;
                }
                else
                {
                    return user;
                }
            }
            else
            {
                return false;
            }
        }

        public IQueryable<NotificationData> NotificationDataList(NotificationSearch NotificationSearch)
        {
            try
            {
                var NotificationDataList = _CustomerService.NotificationData.AsQueryable();
                if (!string.IsNullOrEmpty(NotificationSearch.TourOpCode))
                {
                    NotificationDataList = from e in NotificationDataList where e.TourOpCode.ToString().Trim().ToLower() == NotificationSearch.TourOpCode.ToString().Trim().ToLower() select e;
                }
                if (!string.IsNullOrEmpty(NotificationSearch.DirectOrAgent))
                {
                    NotificationDataList = from e in NotificationDataList where e.DirectOrAgent.ToString().Trim().ToLower() == NotificationSearch.DirectOrAgent.ToString().Trim().ToLower() select e;
                }
                if (NotificationSearch.StartDate != null)
                {
                    NotificationDataList = from e in NotificationDataList where DbFunctions.TruncateTime(e.StartDate) == DbFunctions.TruncateTime(NotificationSearch.StartDate) select e;
                }
                if (!string.IsNullOrEmpty(NotificationSearch.DeparturePoint))
                {
                    NotificationDataList = from e in NotificationDataList where e.DeparturePoint.ToString().Trim().ToLower() == NotificationSearch.DeparturePoint.ToString().Trim().ToLower() select e;
                }
                if (!string.IsNullOrEmpty(NotificationSearch.ArrivalPoint))
                {
                    NotificationDataList = from e in NotificationDataList where e.ArrivalPoint.ToString().Trim().ToLower() == NotificationSearch.ArrivalPoint.ToString().Trim().ToLower() select e;
                }
                if (NotificationSearch.TravelDate != null)
                {
                    NotificationDataList = from e in NotificationDataList where e.TravelDate == NotificationSearch.TravelDate select e;
                }
                if (!string.IsNullOrEmpty(NotificationSearch.TravelDepatureTime))
                {
                    NotificationDataList = from e in NotificationDataList where e.TravelDepatureTime.ToString().Trim().ToLower().Replace(" ", "") == NotificationSearch.TravelDepatureTime.ToString().Trim().ToLower().Replace(" ", "") select e;
                }
                if (!string.IsNullOrEmpty(NotificationSearch.TravelArrivalTime))
                {
                    NotificationDataList = from e in NotificationDataList where e.TravelArrivalTime.ToString().Trim().ToLower().Replace(" ", "") == NotificationSearch.TravelArrivalTime.ToString().Trim().ToLower().Replace(" ", "") select e;
                }
                if (!string.IsNullOrEmpty(NotificationSearch.TravelDirection))
                {
                    NotificationDataList = from e in NotificationDataList where e.TravelDirection.ToString().Trim().ToLower() == NotificationSearch.TravelDirection.ToString().Trim().ToLower() select e;
                }
                if (!string.IsNullOrEmpty(NotificationSearch.TransportCarrier))
                {
                    NotificationDataList = from e in NotificationDataList where e.TransportCarrier.ToString().Trim().ToLower() == NotificationSearch.TransportCarrier.ToString().Trim().ToLower() select e;
                }
                if (!string.IsNullOrEmpty(NotificationSearch.TransportNumber))
                {
                    NotificationDataList = from e in NotificationDataList where e.TransportNumber.ToString().Trim().ToLower() == NotificationSearch.TransportNumber.ToString().Trim().ToLower() select e;
                }
                if (!string.IsNullOrEmpty(NotificationSearch.TransportType))
                {
                    NotificationDataList = from e in NotificationDataList where e.TransportType.ToString().Trim().ToLower() == NotificationSearch.TransportType.ToString().Trim().ToLower() select e;
                }
                if (!string.IsNullOrEmpty(NotificationSearch.TransportChain))
                {
                    NotificationDataList = from e in NotificationDataList where e.TransportChain.ToString().Trim().ToLower() == NotificationSearch.TransportChain.ToString().Trim().ToLower() select e;
                }
                if (!string.IsNullOrEmpty(NotificationSearch.CountryName))
                {
                    NotificationDataList = from e in NotificationDataList where e.ResortCode.Substring(0, 2).Trim().ToLower() == NotificationSearch.CountryName.Trim().ToLower() select e;
                }
                if (!string.IsNullOrEmpty(NotificationSearch.ResortName))
                {
                    NotificationDataList = from e in NotificationDataList where e.AccommodationCode.Substring(0, 2).Trim().ToLower() == NotificationSearch.ResortName.Trim().ToLower() select e;
                }
                if (!string.IsNullOrEmpty(NotificationSearch.AccommodationName))
                {
                    //var AccommodationCode = (from row in _CustomerService.NotificationData where row.AccommodationName.Trim().ToLower().ToString() == NotificationSearch.AccommodationName.Trim().ToLower().ToString() select row.AccommodationCode).FirstOrDefault();
                    NotificationDataList = from e in NotificationDataList where e.AccommodationCode.Trim().ToLower() == NotificationSearch.AccommodationName.Trim().ToLower() select e;
                }

                return NotificationDataList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<NotificationLog> NotificationLogList(MessageLogSearch MessageLogSearch)
        {
            try
            {
                var NotificationLogList = _CustomerService.NotificationLog.AsQueryable();
                if (!string.IsNullOrEmpty(MessageLogSearch.SentTo))
                {
                    NotificationLogList = from e in NotificationLogList where e.SentTo.ToString().Trim().ToLower() == MessageLogSearch.SentTo.ToString().Trim().ToLower() select e;
                }
                if (!string.IsNullOrEmpty(MessageLogSearch.FirstName))
                {
                    NotificationLogList = from e in NotificationLogList where e.FirstName.ToString().Trim().ToLower() == MessageLogSearch.FirstName.ToString().Trim().ToLower() select e;
                }
                if (!string.IsNullOrEmpty(MessageLogSearch.Surname))
                {
                    NotificationLogList = from e in NotificationLogList where e.Surname.ToString().Trim().ToLower() == MessageLogSearch.Surname.ToString().Trim().ToLower() select e;
                }
                if (!string.IsNullOrEmpty(MessageLogSearch.SentFrom))
                {
                    NotificationLogList = from e in NotificationLogList where e.SentFrom.ToString().Trim().ToLower() == MessageLogSearch.SentFrom.ToString().Trim().ToLower() select e;
                }
                if (MessageLogSearch.CreateDate != null)
                {
                    NotificationLogList = from e in NotificationLogList where DbFunctions.TruncateTime(e.CreateDate) == DbFunctions.TruncateTime(MessageLogSearch.CreateDate) select e;
                }
                return NotificationLogList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public dynamic GetAllCustomer(NotificationSearch NotificationSearch)
        {
            var NotificationDataList = this.NotificationDataList(NotificationSearch);

            var EmailCount = NotificationDataList.Where(x => !string.IsNullOrEmpty(x.Email)).Count();
            var MobileCount = NotificationDataList.Where(x => !string.IsNullOrEmpty(x.MobileNo)).Count();

            var notifications = (from n in NotificationDataList
                                 select new
                                 {
                                     n.BookingRef,
                                     n.TourOpCode,
                                     n.PassengerId,
                                     n.Title,
                                     n.FirstName,
                                     n.Surname,
                                     n.MobileNo,
                                     n.Email,
                                     n.DirectOrAgent,
                                     n.StartDate,
                                     n.TravelDate,
                                     n.DeparturePoint,
                                     n.ArrivalPoint,
                                     n.TransportChain,
                                     n.TransportCarrier,
                                     n.TransportNumber
                                 }).ToList();

            var customer = (from row in notifications
                            select new
                            {
                                BookingRef = row.BookingRef,
                                TourOpCode = row.TourOpCode,
                                PassengerId = row.PassengerId,
                                FullName = string.Format("{0}, {1} {2}", row.Surname, row.Title, row.FirstName),
                                MobileNo = row.MobileNo,
                                Email = row.Email,
                                DirectOrAgent = row.DirectOrAgent,
                                StartDate = row.StartDate,
                                TravelDate = row.TravelDate,
                                TransportNumber = row.TransportNumber,
                                FlightChain = string.Format("{0}-{1}-{2}", row.DeparturePoint, row.ArrivalPoint, row.TransportChain),
                                DeparturePoint = row.DeparturePoint,
                                ArrivalPoint = row.ArrivalPoint
                            }).Where(x => !string.IsNullOrEmpty(x.Email) || !string.IsNullOrEmpty(x.MobileNo)).OrderByDescending(x => x.StartDate).AsQueryable();

            if (NotificationSearch.iSortingCols == 1)
            {
                if (NotificationSearch.sSortDir_0 == "asc" && NotificationSearch.iSortCol_0 == 0) { customer = customer.OrderBy(p => p.BookingRef); }
                if (NotificationSearch.sSortDir_0 == "desc" && NotificationSearch.iSortCol_0 == 0) { customer = customer.OrderByDescending(p => p.BookingRef); }

                if (NotificationSearch.sSortDir_0 == "asc" && NotificationSearch.iSortCol_0 == 1) { customer = customer.OrderBy(p => p.TourOpCode); }
                if (NotificationSearch.sSortDir_0 == "desc" && NotificationSearch.iSortCol_0 == 1) { customer = customer.OrderByDescending(p => p.TourOpCode); }

                if (NotificationSearch.sSortDir_0 == "asc" && NotificationSearch.iSortCol_0 == 2) { customer = customer.OrderBy(p => p.FullName); }
                if (NotificationSearch.sSortDir_0 == "desc" && NotificationSearch.iSortCol_0 == 2) { customer = customer.OrderByDescending(p => p.FullName); }

                if (NotificationSearch.sSortDir_0 == "asc" && NotificationSearch.iSortCol_0 == 3) { customer = customer.OrderBy(p => p.MobileNo); }
                if (NotificationSearch.sSortDir_0 == "desc" && NotificationSearch.iSortCol_0 == 3) { customer = customer.OrderByDescending(p => p.MobileNo); }

                if (NotificationSearch.sSortDir_0 == "asc" && NotificationSearch.iSortCol_0 == 4) { customer = customer.OrderBy(p => p.DirectOrAgent); }
                if (NotificationSearch.sSortDir_0 == "desc" && NotificationSearch.iSortCol_0 == 4) { customer = customer.OrderByDescending(p => p.DirectOrAgent); }

                if (NotificationSearch.sSortDir_0 == "asc" && NotificationSearch.iSortCol_0 == 5) { customer = customer.OrderBy(p => p.TravelDate); }
                if (NotificationSearch.sSortDir_0 == "desc" && NotificationSearch.iSortCol_0 == 5) { customer = customer.OrderByDescending(p => p.TravelDate); }

                if (NotificationSearch.sSortDir_0 == "asc" && NotificationSearch.iSortCol_0 == 6) { customer = customer.OrderBy(p => p.DeparturePoint); }
                if (NotificationSearch.sSortDir_0 == "desc" && NotificationSearch.iSortCol_0 == 6) { customer = customer.OrderByDescending(p => p.DeparturePoint); }

                if (NotificationSearch.sSortDir_0 == "asc" && NotificationSearch.iSortCol_0 == 7) { customer = customer.OrderBy(p => p.ArrivalPoint); }
                if (NotificationSearch.sSortDir_0 == "desc" && NotificationSearch.iSortCol_0 == 7) { customer = customer.OrderByDescending(p => p.ArrivalPoint); }

                if (NotificationSearch.sSortDir_0 == "asc" && NotificationSearch.iSortCol_0 == 8) { customer = customer.OrderBy(p => p.TransportNumber); }
                if (NotificationSearch.sSortDir_0 == "desc" && NotificationSearch.iSortCol_0 == 8) { customer = customer.OrderByDescending(p => p.TransportNumber); }

                if (NotificationSearch.sSortDir_0 == "asc" && NotificationSearch.iSortCol_0 == 9) { customer = customer.OrderBy(p => p.FlightChain); }
                if (NotificationSearch.sSortDir_0 == "desc" && NotificationSearch.iSortCol_0 == 9) { customer = customer.OrderByDescending(p => p.FlightChain); }
            }
            var datatable = new
            {
                sEcho = NotificationSearch.sEcho,
                iTotalRecords = customer.OrderByDescending(e => e.StartDate).Skip(NotificationSearch.iDisplayStart).Take(NotificationSearch.iDisplayLength).Count(),
                iTotalDisplayRecords = customer.Count(),
                aaData = customer.OrderByDescending(e => e.StartDate).Skip(NotificationSearch.iDisplayStart).Take(NotificationSearch.iDisplayLength).ToList(),
                EmailCount = EmailCount,
                MobileCount = MobileCount

            };
            return datatable;
        }

        public dynamic GetAllMessageLog(MessageLogSearch MessageLogSearch)
        {
            var NotificationLogList = this.NotificationLogList(MessageLogSearch);

            var notifications = (from n in NotificationLogList
                                 select new
                                 {
                                     n.SentTo,
                                     n.FirstName,
                                     n.Surname,
                                     n.SentFrom,
                                     n.Message,
                                     n.Status,
                                     n.LogComment,
                                     n.CreateDate
                                 }).ToList();

            var customer = (from row in notifications
                            select new
                            {
                                SentTo = row.SentTo,
                                FullName = row.Surname + " " + row.FirstName,
                                SentFrom = row.SentFrom,
                                Message = row.Message,
                                Status = row.Status,
                                LogComment = row.LogComment,
                                CreateDate = row.CreateDate
                            }).OrderByDescending(x => x.CreateDate).AsQueryable();

            if (MessageLogSearch.iSortingCols == 1)
            {
                if (MessageLogSearch.sSortDir_0 == "asc" && MessageLogSearch.iSortCol_0 == 0) { customer = customer.OrderBy(p => p.SentTo); }
                if (MessageLogSearch.sSortDir_0 == "desc" && MessageLogSearch.iSortCol_0 == 0) { customer = customer.OrderByDescending(p => p.SentTo); }

                if (MessageLogSearch.sSortDir_0 == "asc" && MessageLogSearch.iSortCol_0 == 1) { customer = customer.OrderBy(p => p.FullName); }
                if (MessageLogSearch.sSortDir_0 == "desc" && MessageLogSearch.iSortCol_0 == 1) { customer = customer.OrderByDescending(p => p.FullName); }

                if (MessageLogSearch.sSortDir_0 == "asc" && MessageLogSearch.iSortCol_0 == 2) { customer = customer.OrderBy(p => p.SentFrom); }
                if (MessageLogSearch.sSortDir_0 == "desc" && MessageLogSearch.iSortCol_0 == 2) { customer = customer.OrderByDescending(p => p.SentFrom); }

                if (MessageLogSearch.sSortDir_0 == "asc" && MessageLogSearch.iSortCol_0 == 3) { customer = customer.OrderBy(p => p.Message); }
                if (MessageLogSearch.sSortDir_0 == "desc" && MessageLogSearch.iSortCol_0 == 3) { customer = customer.OrderByDescending(p => p.Message); }

                if (MessageLogSearch.sSortDir_0 == "asc" && MessageLogSearch.iSortCol_0 == 4) { customer = customer.OrderBy(p => p.Status); }
                if (MessageLogSearch.sSortDir_0 == "desc" && MessageLogSearch.iSortCol_0 == 4) { customer = customer.OrderByDescending(p => p.Status); }

                if (MessageLogSearch.sSortDir_0 == "asc" && MessageLogSearch.iSortCol_0 == 5) { customer = customer.OrderBy(p => p.LogComment); }
                if (MessageLogSearch.sSortDir_0 == "desc" && MessageLogSearch.iSortCol_0 == 5) { customer = customer.OrderByDescending(p => p.LogComment); }

                if (MessageLogSearch.sSortDir_0 == "asc" && MessageLogSearch.iSortCol_0 == 6) { customer = customer.OrderBy(p => p.CreateDate); }
                if (MessageLogSearch.sSortDir_0 == "desc" && MessageLogSearch.iSortCol_0 == 6) { customer = customer.OrderByDescending(p => p.CreateDate); }
            }
            var datatable = new
            {
                sEcho = MessageLogSearch.sEcho,
                iTotalRecords = customer.Skip(MessageLogSearch.iDisplayStart).Take(MessageLogSearch.iDisplayLength).Count(),
                iTotalDisplayRecords = customer.Count(),
                aaData = customer.Skip(MessageLogSearch.iDisplayStart).Take(MessageLogSearch.iDisplayLength).ToList()
            };
            return datatable;
        }

        public bool CheckExists(NotificationSearch NotificationSearch)
        {
            var db = new CustomerContext();
            bool flag = false;
            var NotificationDataList = this.NotificationDataList(NotificationSearch);
            try
            {
                
                foreach (var item in NotificationDataList)
                {
                    if (!string.IsNullOrEmpty(item.Email))
                    {
                        var logemail = db.NotificationLog.Where(x => x.SentTo.Trim().ToLower() == item.Email.Trim().ToLower()).Count();
                        if (logemail > 0)
                        {
                            flag = false;
                        }
                        else
                        {
                            flag = true;
                        }
                    }
                    if (!string.IsNullOrEmpty(item.MobileNo))
                    {
                        var logemail = db.NotificationLog.Where(x => x.SentTo == item.MobileNo).Count();
                        if (logemail > 0)
                        {
                            flag = false;
                        }
                        else
                        {
                            flag = true;
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
            return flag;
        }

        public List<NotificationData> GetCustomerSendMessage(NotificationSearch notificationSearch, int approvedCount)
        {
            try
            {
                var notificationDataSeedList = _CustomerService.NotificationDataSeeds.AsQueryable();
                //TODO: Allow seeds against UK deaprture airports
                //if (!string.IsNullOrEmpty(notificationSearch.DeparturePoint))
                //{
                //    notificationDataSeedList = from e in notificationDataSeedList where e.ArrivalPoint.ToString().Trim().ToLower() 
                //                                   == notificationSearch.DeparturePoint.ToString().Trim().ToLower() select e;
                //}
                if (!string.IsNullOrEmpty(notificationSearch.ArrivalPoint))
                {
                    notificationDataSeedList = from e in notificationDataSeedList
                                               where
                                                   (e.ArrivalPoint.ToString().Trim().ToLower() ==
                                                   notificationSearch.ArrivalPoint.ToString().Trim().ToLower())
                                                   || (e.ArrivalPoint.ToString().Trim().ToLower() ==
                                           notificationSearch.DeparturePoint.ToString().Trim().ToLower())
                                               select e;
                }
                if (!string.IsNullOrEmpty(notificationSearch.CountryName))
                {
                    notificationDataSeedList = from e in notificationDataSeedList where e.CountryCode == ((from row in _CustomerService.NotificationDataSeeds where row.CountryName.ToLower().Trim() == notificationSearch.CountryName.ToLower().Trim() select row.CountryCode).FirstOrDefault()) select e;
                }
                if (!string.IsNullOrEmpty(notificationSearch.ResortName))
                {
                    notificationDataSeedList = from e in notificationDataSeedList where e.ResortCode == ((from row in _CustomerService.NotificationDataSeeds where row.CountryName.ToLower().Trim() == notificationSearch.CountryName.ToLower().Trim() select row.CountryCode).FirstOrDefault()) select e;
                }

                var notificationDataList = this.NotificationDataList(notificationSearch);

                var mobileCount = notificationDataList.Count(x => !string.IsNullOrEmpty(x.MobileNo));

                if (mobileCount != approvedCount)
                {
                    return null;
                }

                var customer = (from row in notificationDataList
                                select row).Where(x => !string.IsNullOrEmpty(x.Email) || !string.IsNullOrEmpty(x.MobileNo)).OrderByDescending(x => x.StartDate).ToList();

                foreach (var seed in notificationDataSeedList)
                {
                    customer.Add(new NotificationData()
                    {
                        Title = seed.Title,
                        FirstName = seed.FirstName,
                        Surname = seed.Surname,
                        Email = seed.Email,
                        MobileNo = seed.MobileNo,
                        Seed = true
                    }
                        );
                }

                return customer;

            }
            catch (Exception e)
            {
                return null;
                throw;
            }
        }

        public List<string> GetTourOpCode()
        {
            try
            {
                var datalist = _CustomerService.NotificationData.Where(n => !string.IsNullOrEmpty(n.TourOpCode)).AsQueryable();
                var data = (from row in datalist
                            where row.TourOpCode != null
                            select row.TourOpCode).Distinct().OrderBy(x => x).ToList();
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<string> GetDeparturePoint()
        {
            try
            {
                var datalist = _CustomerService.NotificationData.AsQueryable();
                var data = (from row in datalist
                            where row.DeparturePoint != null
                            select row.DeparturePoint).Distinct().OrderBy(x => x).ToList();
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<string> GetArrivalPoint()
        {
            try
            {
                var datalist = _CustomerService.NotificationData.AsQueryable();
                var data = (from row in datalist
                            where row.ArrivalPoint != null
                            select row.ArrivalPoint).Distinct().OrderBy(x => x).ToList();
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<string> GetTravelDate()
        {
            try
            {
                var datalist = _CustomerService.NotificationData.AsQueryable();
                var data = (from row in datalist
                            where row.TravelDate != null
                            select row.TravelDate).Distinct().OrderBy(x => x).ToList();
                return data;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<string> GetTravelDirection()
        {
            try
            {
                var datalist = _CustomerService.NotificationData.AsQueryable();
                var data = (from row in datalist
                            where row.TravelDirection != null
                            select row.TravelDirection).Distinct().OrderBy(x => x).ToList();
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<string> GetTransportCarrier()
        {
            try
            {
                var datalist = _CustomerService.NotificationData.AsQueryable();
                var data = (from row in datalist
                            where row.TransportCarrier != null
                            select row.TransportCarrier).Distinct().OrderBy(x => x).ToList();
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<string> GetTransportNumber()
        {
            try
            {
                var datalist = _CustomerService.NotificationData.AsQueryable();
                var data = (from row in datalist
                            where row.TransportNumber != null
                            select row.TransportNumber).Distinct().OrderBy(x => x).ToList();
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<string> GetTransportType()
        {
            try
            {
                var datalist = _CustomerService.NotificationData.AsQueryable();
                var data = (from row in datalist
                            where row.TransportType != null
                            select row.TransportType).Distinct().OrderBy(x => x).ToList();
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<string> GetTransportChain()
        {
            try
            {
                var datalist = _CustomerService.NotificationData.AsQueryable();
                var data = (from row in datalist
                            where row.TransportChain != null
                            select row.TransportChain).Distinct().OrderBy(x => x).ToList();
                return data;
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
                var datalist = _CustomerService.NotificationData.AsQueryable();
                var data = (from row in datalist
                            where row.CountryName != null
                            select new
                            {
                                row.CountryName,
                                CountryCode = row.ResortCode.Substring(0, 2)
                            }).Distinct().OrderBy(x => x).ToList();
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public dynamic GetResortName(string countryCode)
        {
            try
            {
                var datalist = _CustomerService.NotificationData.AsQueryable();
                var data = (from row in datalist
                            where row.ResortName != null && row.ResortCode.Substring(0, 2) == countryCode
                            select new
                            {
                                row.ResortName,
                                ResortCode = row.AccommodationCode.Substring(0, 2)
                            }).Distinct().OrderBy(x => x).ToList();
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public dynamic GetAccommodationName(string accommodationcode)
        {
            try
            {
                var datalist = _CustomerService.NotificationData.AsQueryable();
                var data = (from row in datalist
                            where row.AccommodationName != null && row.AccommodationCode.Substring(0, 2) == accommodationcode
                            select new
                            {
                                row.AccommodationName,
                                AccommodationCode = row.AccommodationCode
                            }).Distinct().OrderBy(x => x).ToList();
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string SendEmail(string fromEmailAddress, string toEmailAddress, string emailSubject, string emailMessage)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(fromEmailAddress);
                mailMessage.Subject = emailSubject;
                mailMessage.Body = emailMessage;
                mailMessage.IsBodyHtml = true;
                mailMessage.To.Add(new MailAddress(toEmailAddress));

                Object state = mailMessage;
                var smtpClient = new SmtpClient();
                smtpClient.SendAsync(mailMessage, state);

                return state.ToString().Substring(0, 10);
            }
            catch (Exception e)
            {
                return e.Message.Substring(0, 10);
                throw e;
            }
        }

        public static string SendSMS(string fromNumber, string toNumber, string Message)
        {
            try
            {


                string ACCOUNT_SID = ConfigurationManager.AppSettings["ACCOUNTSID"];
                string AUTH_TOKEN = ConfigurationManager.AppSettings["AUTHTOKEN"];

                TwilioRestClient client = new TwilioRestClient(ACCOUNT_SID, AUTH_TOKEN);
                var res = client.SendMessage(fromNumber, toNumber, Message);

                if (res.ErrorCode.HasValue)
                {
                    return res.ErrorCode.ToString();
                }
                else
                {
                    return res.Status;
                }
            }
            catch (Exception e)
            {
                return e.Message.Substring(0, 10);
                throw;
            }
        }

        public static bool NotificationLogAdd(int Type, string SentTo, string FirstName, string Surname, string SentFrom, string Message, string Status, string LogComment, bool Seed, bool TestMode)
        {
            try
            {
                using (CustomerContext db = new CustomerContext())
                {
                    NotificationLog notificationlog = new NotificationLog();
                    notificationlog.Type = Type;
                    notificationlog.SentTo = SentTo;
                    notificationlog.FirstName = FirstName;
                    notificationlog.Surname = Surname;
                    notificationlog.SentFrom = SentFrom;
                    notificationlog.Message = Message;
                    notificationlog.Status = Status;
                    notificationlog.LogComment = LogComment;
                    notificationlog.Seed = Seed;
                    notificationlog.TestMode = TestMode;
                    db.NotificationLog.Add(notificationlog);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

    public class CountryDetails
    {
        public string NewCountryName { get; set; }
        public string NewCountryCode { get; set; }
    }
}
