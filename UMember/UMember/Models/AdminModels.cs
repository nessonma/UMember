using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace UMember.Models
{
    public class LogOnModel
    {
        [Required]
        [Display(Name = "管理员账户")]
        public string Admin_Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Admin_Password { get; set; }
        [Required]
        [Display(Name = "验证码")]
        public string ValidateCode { get; set; }
        
    }
    public class ChangePasswordView
    {
        
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "旧密码")]
        public string Admin_Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "新密码")]
        public string New_Admin_Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "确认新密码")]
        public string Confirm_New_Admin_Password { get; set; }
    }

    public class AdminView
    {
        [Required]
        [Display(Name = "管理员")]
        public string Admin_Name { get; set; }

        [Required]        
        [Display(Name = "角色")]
        public int Role_ID { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Admin_Password { get; set; }

        
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        public string Confirm_Admin_Password { get; set; }

        
        public int Admin_ID { get; set; }


    }
}