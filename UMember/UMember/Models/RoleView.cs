using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace UMember.Models
{
    public class RoleView
    {
        
        [Required]
        [Display(Name = "角色名称")] 
        public string Role_Name { get; set; }

        [Display(Name = "权限")]
        public string[] Menu_Name { get; set; }

        public int Role_ID { get; set; }
        public bool Is_Delete { get; set; }
        public bool Is_Hide { get; set; }
    }
}