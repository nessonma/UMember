using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UMember.Controllers
{
    public class JiangjinController : Controller
    {
        //股东分红
        // GET: /Jiangjin/

        public ActionResult GuDongFenHong()
        {
            return View();
        }


        public ActionResult DayCompute()
        {
            return View();
        }

        public ActionResult MonthCompute()
        {
            return View();
        }

    }
}
