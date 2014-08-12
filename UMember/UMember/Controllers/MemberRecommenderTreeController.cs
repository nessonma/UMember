using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UMember.Controllers
{
    public class MemberRecommenderTreeController : Controller
    {
        //推荐关系图
        // GET: /MemberRecommenderTree/

        public ActionResult Index(int Member_ID)
        {
            return View();
        }

    }
}
