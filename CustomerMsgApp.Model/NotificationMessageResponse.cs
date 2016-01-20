using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerMsgApp.Model
{
    [Table("NotificationMessageReceived")]
    public class NotificationMessageReceived
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string From { get; set; }
        [StringLength(50)]
        public string To { get; set; }
        public string Message { get; set; }
        public DateTime ReceivedDate { get; set; }

        public NotificationMessageReceived()
        {
            ReceivedDate = DateTime.Now;
        }
    }
}
