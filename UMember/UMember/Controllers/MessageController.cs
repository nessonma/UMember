using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using UMember.Models;

namespace UMember.Controllers
{
    public class MessageController : Controller
    {
        private dbTiderEntities db = new dbTiderEntities();

        //
        // GET: /Request/

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {

            ViewBag.CurrentSort = sortOrder;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var requests1 = from s in db.tbMessage
                            select new MessageView
                            {
                                //Message_ID = s.Message_ID,
                                //Member_Name = s.tbMemberInfo,
                                //Message = s.Message,
                                //Message_Time = s.Message_Time ?? DateTime.Now,
                                //Admin_Name = s.tbAdmin.Admin_Name,
                                //Reply = s.Reply,
                                //Reply_Time = s.Reply_Time,
                                //State = s.State ?? false,
                                //Category = s.Category

                            };
            if (!String.IsNullOrEmpty(searchString))
            {
                requests1 = requests1.Where(s => s.Member_Name.ToUpper() == searchString.ToUpper());

            }
            if (System.Web.HttpContext.Current.Session["Role_Name"].ToString().Contains("财务"))
            {
                requests1 = requests1.Where(s => s.Category.ToUpper() == "财务");
            }
            else if (System.Web.HttpContext.Current.Session["Role_Name"].ToString().Contains("物流"))
            {
                requests1 = requests1.Where(s => s.Category.ToUpper() == "物流");
            }



            var request = requests1.OrderBy(s => s.Reply_Time).ThenByDescending(s => s.Message_ID);
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(request.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Request/Details/5

        public ActionResult Details(int id = 0)
        {
            tbMessage tbrequest = db.tbMessage.Find(id);
            if (tbrequest == null)
            {
                return HttpNotFound();
            }
            return View(tbrequest);
        }

        //
        // GET: /Request/Create

        public ActionResult Create()
        {

            return View();
        }

        //
        // POST: /Request/Create

        [HttpPost]
        public ActionResult Create(MessageView msgview)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(msgview.Member_Name))
                {
                    ModelState.AddModelError("Member_Name", "会员名字不能为空");
                    return View();
                }

                if (string.IsNullOrEmpty(msgview.Message))
                {
                    ModelState.AddModelError("Message", "留言不能为空");
                    return View();
                }
                var query = from s in db.tbMemberInfo
                            where s.Member_Name.Trim().ToLower() == msgview.Member_Name.Trim().ToLower()
                            select s;
                List<tbMemberInfo> list = query.ToList();
                if (list.Count == 0)
                {
                    ModelState.AddModelError("Member_Name", "会员不存在");
                    return View();
                }

                tbMessage tbmessage = new tbMessage();
                //tbmessage.Message_Time = DateTime.Now;
                //tbmessage.Member_ID = list[0].Member_ID;
                //tbmessage.Message = msgview.Message;
                //tbmessage.Admin_ID = int.Parse(System.Web.HttpContext.Current.Session["Admin_ID"].ToString());
                //tbmessage.Message_Type = 2;
                db.tbMessage.Add(tbmessage);
                db.SaveChanges();
                return RedirectToAction("LeadIndex");
            }

            return View(msgview);
        }

        //
        // GET: /Request/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbMessage tbrequest = db.tbMessage.Find(id);
            if (tbrequest == null)
            {
                return HttpNotFound();
            }
           // ViewBag.Admin_ID = new SelectList(db.tbAdmin, "Admin_ID", "Admin_Name", tbrequest.Admin_ID);
           // ViewBag.Member_ID = new SelectList(db.tbMemberInfo, "Member_ID", "Member_Name", tbrequest.Member_ID);
            return View(tbrequest);
        }

        //
        // POST: /Request/Edit/5

        [HttpPost]
        public ActionResult Edit(tbMessage tbmessage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbmessage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.Admin_ID = new SelectList(db.tbAdmin, "Admin_ID", "Admin_Name", tbmessage.Admin_ID);
            //ViewBag.Member_ID = new SelectList(db.tbMemberInfo, "Member_ID", "Member_Name", tbmessage.Member_ID);
            return View(tbmessage);
        }

        //
        // GET: /Request/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbMessage tbrequest = db.tbMessage.Find(id);
            if (tbrequest == null)
            {
                return HttpNotFound();
            }
            return View(tbrequest);
        }

        //
        // POST: /Request/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbMessage tbrequest = db.tbMessage.Find(id);
            db.tbMessage.Remove(tbrequest);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Answer(int id = 0)
        {

            tbMessage tbrequest = db.tbMessage.Find(id);
            if (tbrequest == null)
            {
                return HttpNotFound();
            }

            return View(tbrequest);
        }

        [HttpPost]
        public ActionResult Answer(MessageView request)
        {

            if (ModelState.IsValid)
            {
                tbMessage tbrequest = db.tbMessage.Find(request.Message_ID);
                tbrequest.Reply_Time = DateTime.Now;
                //tbrequest.Reply = request.Reply;
                tbrequest.State = true;
               // tbrequest.Admin_ID = int.Parse(System.Web.HttpContext.Current.Session["Admin_ID"].ToString());
                db.Entry(tbrequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        //
        // GET: /Request/

        public ActionResult LeadIndex(string sortOrder, string currentFilter, string searchString, int? page)
        {

            ViewBag.CurrentSort = sortOrder;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var requests1 = from s in db.tbMessage
                            //where s.Message_Type == 2

                            select new MessageView
                                           {
                                               //Message_ID = s.Message_ID,
                                               //Member_Name = s.tbMemberInfo.Member_Name,
                                               //Message = s.Message,
                                               //Message_Time = s.Message_Time ?? DateTime.Now,
                                               //Admin_Name = s.tbAdmin.Admin_Name,
                                               //State = s.State ?? false,
                                               //Reply = s.Reply,
                                               //Reply_Time = s.Reply_Time,
                                               
                                           };
            if (!String.IsNullOrEmpty(searchString))
            {
                requests1 = requests1.Where(s => s.Member_Name.ToUpper() == searchString.ToUpper());

            }

            var request = requests1.OrderBy(s => s.Reply_Time).ThenByDescending(s => s.Message_ID);
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(request.ToPagedList(pageNumber, pageSize));
        }



    }
}