using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UMember.Controllers
{
    public class MemberAccountController : Controller
    {
        private dbTiderEntities db = new dbTiderEntities();

        //
        // GET: /MemberAccount/

        public ActionResult Index()
        {
            var tbmemberaccount = db.tbMemberAccount.Include(t => t.tbMemberInfo);
            return View(tbmemberaccount.ToList());
        }

        //
        // GET: /MemberAccount/Details/5

        public ActionResult Details(int id = 0)
        {
            tbMemberAccount tbmemberaccount = db.tbMemberAccount.Find(id);
            if (tbmemberaccount == null)
            {
                return HttpNotFound();
            }
            return View(tbmemberaccount);
        }

        //
        // GET: /MemberAccount/Create

        public ActionResult Create()
        {
            ViewBag.Member_ID = new SelectList(db.tbMemberInfo, "Member_ID", "Member_Name");
            return View();
        }

        //
        // POST: /MemberAccount/Create

        [HttpPost]
        public ActionResult Create(tbMemberAccount tbmemberaccount)
        {
            if (ModelState.IsValid)
            {
                db.tbMemberAccount.Add(tbmemberaccount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Member_ID = new SelectList(db.tbMemberInfo, "Member_ID", "Member_Name", tbmemberaccount.Member_ID);
            return View(tbmemberaccount);
        }

        //
        // GET: /MemberAccount/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbMemberAccount tbmemberaccount = db.tbMemberAccount.Find(id);
            if (tbmemberaccount == null)
            {
                return HttpNotFound();
            }
            ViewBag.Member_ID = new SelectList(db.tbMemberInfo, "Member_ID", "Member_Name", tbmemberaccount.Member_ID);
            return View(tbmemberaccount);
        }

        //
        // POST: /MemberAccount/Edit/5

        [HttpPost]
        public ActionResult Edit(tbMemberAccount tbmemberaccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbmemberaccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Member_ID = new SelectList(db.tbMemberInfo, "Member_ID", "Member_Name", tbmemberaccount.Member_ID);
            return View(tbmemberaccount);
        }

        //
        // GET: /MemberAccount/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbMemberAccount tbmemberaccount = db.tbMemberAccount.Find(id);
            if (tbmemberaccount == null)
            {
                return HttpNotFound();
            }
            return View(tbmemberaccount);
        }

        //
        // POST: /MemberAccount/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbMemberAccount tbmemberaccount = db.tbMemberAccount.Find(id);
            db.tbMemberAccount.Remove(tbmemberaccount);
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