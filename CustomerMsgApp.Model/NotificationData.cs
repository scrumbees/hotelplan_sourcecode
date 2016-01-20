using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerMsgApp.Model
{
    [Table("NotificationData")]
    public class NotificationData
    {
        [Key]
        public Int64 BookingRef { get; set; }
        [StringLength(10)]
        public string TourOpCode { get; set; }
        public int PassengerId { get; set; }
        [StringLength(20)]
        public string Title { get; set; }
        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string Surname { get; set; }
        [StringLength(20)]
        public string MobileNo { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(10)]
        public string DirectOrAgent { get; set; }
        public DateTime StartDate { get; set; }
        [StringLength(5)]
        public string DeparturePoint { get; set; }
        [StringLength(5)]
        public string ArrivalPoint { get; set; }
        [StringLength(10)]
        public string TravelDate { get; set; }
        [StringLength(20)]
        public string TravelDepatureTime { get; set; }
        [StringLength(20)]
        public string TravelArrivalTime { get; set; }
        [StringLength(1)]
        public string TravelDirection { get; set; }
        [StringLength(10)]
        public string TransportCarrier { get; set; }
        [StringLength(10)]
        public string TransportNumber { get; set; }
        [StringLength(5)]
        public string TransportType { get; set; }
        [StringLength(2)]
        public string TransportChain { get; set; }
        [StringLength(2)]
        public string CountryCode { get; set; }
        [StringLength(50)]
        public string CountryName { get; set; }
        [StringLength(4)]
        public string ResortCode { get; set; }
        [StringLength(50)]
        public string ResortName { get; set; }
        [StringLength(8)]
        public string AccommodationCode { get; set; }
        [StringLength(50)]
        public string AccommodationName { get; set; }
        public bool Seed { get; set; }
        //[StringLength(255)]
        //public string FlightChain { get; set; }
        //[StringLength(255)]
        //public string FullName { get; set; }

        //[StringLength(25)]
        //public string Error { get; set; }
        //[StringLength(4000)]
        //public string ErrorDetails { get; set; }

        public NotificationData()
        {
            Seed = false;

        }
    }
}
