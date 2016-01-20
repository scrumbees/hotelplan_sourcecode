using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerMsgApp.Model
{
    [Table("NotificationDataSeeds")]
    public class NotificationDataSeeds
    {
        [Key]
        public int Id { get; set; }
        [StringLength(20)]
        public string Title { get; set; }
        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string Surname { get; set; }
        [StringLength(2)]
        public string CountryCode { get; set; }
        [StringLength(50)]
        public string CountryName { get; set; }
        [StringLength(4)]
        public string ResortCode { get; set; }
        [StringLength(50)]
        public string ResortName { get; set; }
        [StringLength(5)]
        public string ArrivalPoint { get; set; }
        [StringLength(100)]
        public string Role { get; set; }
        [StringLength(20)]        
        public string MobileNo { get; set; }
        [StringLength(50)]
        public string Email { get; set; }        
    }
}
