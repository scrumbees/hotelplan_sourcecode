using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerMsgApp.Model
{
    [Table("NotificationLog")]
    public class NotificationLog
    {
        [Key]
        public int Id { get; set; }
        public int Type { get; set; }
        [StringLength(100)]
        public string SentTo { get; set; }
        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string Surname { get; set; }
        [StringLength(100)]
        public string SentFrom { get; set; }
        public string Message { get; set; }
        [StringLength(10)]
        public string Status { get; set; }
        [StringLength(4000)]
        public string LogComment { get; set; }
        public bool Seed { get; set; }
        public bool TestMode { get; set; }
        public DateTime CreateDate { get; set; }

        public NotificationLog()
        {
            CreateDate = DateTime.Now;
        }
    }
}
