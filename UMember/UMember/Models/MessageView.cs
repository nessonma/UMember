using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace UMember.Models
{
    public class MessageView
    {

        public string Category { get; set; }
        public string Admin_Name { get; set; }
        public string Member_Name { get; set; }
        public string Message { get; set; }
        public string Reply { get; set; }
        public int Message_ID { get; set; }
        public DateTime Message_Time { get; set; }
        public Nullable<DateTime> Reply_Time { get; set; }
        public bool State { get; set; }
    }
}