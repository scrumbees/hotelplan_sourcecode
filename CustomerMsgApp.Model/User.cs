using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerMsgApp.Model
{
    [Table("User")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string UserName { get; set; }
        [StringLength(50)]
        public string Password { get; set; }
        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string LastName { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(20)]
        public string MobileNo { get; set; }
        public DateTime CreateDate { get; set; }
        public User()
        {
            CreateDate = DateTime.Now;
        }
    }

    public class EnumModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public enum RoleName
    {
        send_message = 1,
        audit_log = 2,
        user_management = 3
    }
}
