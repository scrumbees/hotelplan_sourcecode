using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerMsgApp.Model
{
    public  class NotificationSearch 
    {
        public int sEcho { get; set; }
        public string sSearch { get; set; }
        public int iDisplayLength { get; set; }
        public int iDisplayStart { get; set; }
        public int iColumns { get; set; }
        public int iSortingCols { get; set; }
        public string sColumns { get; set; }
        public int iSortCol_0 { get; set; }
        public string sSortDir_0 { get; set; }

        public string TourOpCode { get; set; }
        public string DirectOrAgent { get; set; }
        public DateTime? StartDate { get; set; }
        public string DeparturePoint { get; set; }
        public string ArrivalPoint { get; set; }
        public string TravelDate { get; set; }
        public string TravelDepatureTime { get; set; }
        public string TravelArrivalTime { get; set; }
        public string TravelDirection { get; set; }
        public string TransportCarrier { get; set; }
        public string TransportNumber { get; set; }
        public string TransportType { get; set; }
        public string TransportChain { get; set; }
        public string CountryName { get; set; }
        public string ResortName { get; set; }
        public string AccommodationName { get; set; }
        public bool Seed { get; set; }
        public string MessageEmail { get; set; }

        public string Message { get; set; }
        public int EmailCount { get; set; }
        public int MobileCount { get; set; }
        public string Password { get; set; }
        public int SendCountSMS { get; set; }
    }   
}
