using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UMember.Controllers
{
    public class MemberParentTreeController : Controller
    {
        //安置关系图
        // GET: /MemberParentTree/

        public ActionResult Index(int Member_ID)
        {
            return View();
        }

    }
}
