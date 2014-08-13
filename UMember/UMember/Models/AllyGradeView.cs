using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace UMember.Models
{
    public class AllyGradeView
    {
        public int AllyGrade_ID { get; set; }
        [Required]
        [Display(Name = "名称")] 
        public string AllyGrade_Name { get; set; }

        [Required]
        [Display(Name = "加盟额")] 
        public Nullable<double> AllyGrade_Money { get; set; }

        [Required]
        [Display(Name = "折算PV值")] 
        public Nullable<double> PV { get; set; }

        
        public string Description { get; set; }
        [Required]
        [Display(Name = "权重")] 
        public Nullable<int> Weight { get; set; }

        [Required]
        [Display(Name = "最高招商奖励")]
        public Nullable<double> MaxZhaoSangJiang { get; set; }

        [Required]
        [Display(Name = "最高市场补贴系数")]
        public Nullable<double> MaxShiCangBuTieFactor { get; set; }

        public Nullable<bool> Is_Delete { get; set; }
        public Nullable<bool> Is_Hide { get; set; }
    }
}