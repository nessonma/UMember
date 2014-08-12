using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UMember.Controllers
{
    public class MemberInfoController : Controller
    {
        private dbTiderEntities db = new dbTiderEntities();

        //
        // GET: /MemberInfo/

        public ActionResult Index()
        {
            var tbmemberinfo = db.tbMemberInfo.Include(t => t.tbAllyGrade).Include(t => t.tbMemberAccount).Include(t => t.tbSCenterInfo);
            return View(tbmemberinfo.ToList());
        }

        //
        // GET: /MemberInfo/Details/5

        public ActionResult Details(int id = 0)
        {
            tbMemberInfo tbmemberinfo = db.tbMemberInfo.Find(id);
            if (tbmemberinfo == null)
            {
                return HttpNotFound();
            }
            return View(tbmemberinfo);
        }

        //
        // GET: /MemberInfo/Create

        public ActionResult Create()
        {
            ViewBag.AllyGrade_ID = new SelectList(db.tbAllyGrade, "AllyGrade_ID", "AllyGrade_Name");
            ViewBag.Member_ID = new SelectList(db.tbMemberAccount, "Member_ID", "Member_ID");
            ViewBag.SCenter_ID = new SelectList(db.tbSCenterInfo, "SCenter_ID", "SCenter_Name");
            return View();
        }

        //
        // POST: /MemberInfo/Create

        [HttpPost]
        public ActionResult Create(tbMemberInfo tbmemberinfo)
        {
            if (ModelState.IsValid)
            {
                db.tbMemberInfo.Add(tbmemberinfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AllyGrade_ID = new SelectList(db.tbAllyGrade, "AllyGrade_ID", "AllyGrade_Name", tbmemberinfo.AllyGrade_ID);
            ViewBag.Member_ID = new SelectList(db.tbMemberAccount, "Member_ID", "Member_ID", tbmemberinfo.Member_ID);
            ViewBag.SCenter_ID = new SelectList(db.tbSCenterInfo, "SCenter_ID", "SCenter_Name", tbmemberinfo.SCenter_ID);
            return View(tbmemberinfo);
        }

        //
        // GET: /MemberInfo/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbMemberInfo tbmemberinfo = db.tbMemberInfo.Find(id);
            if (tbmemberinfo == null)
            {
                return HttpNotFound();
            }
            ViewBag.AllyGrade_ID = new SelectList(db.tbAllyGrade, "AllyGrade_ID", "AllyGrade_Name", tbmemberinfo.AllyGrade_ID);
            ViewBag.Member_ID = new SelectList(db.tbMemberAccount, "Member_ID", "Member_ID", tbmemberinfo.Member_ID);
            ViewBag.SCenter_ID = new SelectList(db.tbSCenterInfo, "SCenter_ID", "SCenter_Name", tbmemberinfo.SCenter_ID);
            return View(tbmemberinfo);
        }

        //
        // POST: /MemberInfo/Edit/5

        [HttpPost]
        public ActionResult Edit(tbMemberInfo tbmemberinfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbmemberinfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AllyGrade_ID = new SelectList(db.tbAllyGrade, "AllyGrade_ID", "AllyGrade_Name", tbmemberinfo.AllyGrade_ID);
            ViewBag.Member_ID = new SelectList(db.tbMemberAccount, "Member_ID", "Member_ID", tbmemberinfo.Member_ID);
            ViewBag.SCenter_ID = new SelectList(db.tbSCenterInfo, "SCenter_ID", "SCenter_Name", tbmemberinfo.SCenter_ID);
            return View(tbmemberinfo);
        }

        //
        // GET: /MemberInfo/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbMemberInfo tbmemberinfo = db.tbMemberInfo.Find(id);
            if (tbmemberinfo == null)
            {
                return HttpNotFound();
            }
            return View(tbmemberinfo);
        }

        //
        // POST: /MemberInfo/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbMemberInfo tbmemberinfo = db.tbMemberInfo.Find(id);
            db.tbMemberInfo.Remove(tbmemberinfo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}