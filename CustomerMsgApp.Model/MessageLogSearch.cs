using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerMsgApp.Model
{
    public class MessageLogSearch
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

        public int Type { get; set; }
        public string SentTo { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string FullName { get; set; }
        public string SentFrom { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public string LogComment { get; set; }
        public bool Seed { get; set; }
        public bool TestMode { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
