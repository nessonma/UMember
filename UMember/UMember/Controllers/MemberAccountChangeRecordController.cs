using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UMember.Controllers
{
    public class MemberAccountChangeRecordController : Controller
    {
        private dbTiderEntities db = new dbTiderEntities();

        //
        // GET: /MemberAccountChangeRecord/

        public ActionResult Index()
        {
            var tbmemberaccountchangerecord = db.tbMemberAccountChangeRecord.Include(t => t.tbMemberInfo);
            return View(tbmemberaccountchangerecord.ToList());
        }

        //
        // GET: /MemberAccountChangeRecord/Details/5

        public ActionResult Details(int id = 0)
        {
            tbMemberAccountChangeRecord tbmemberaccountchangerecord = db.tbMemberAccountChangeRecord.Find(id);
            if (tbmemberaccountchangerecord == null)
            {
                return HttpNotFound();
            }
            return View(tbmemberaccountchangerecord);
        }

        //
        // GET: /MemberAccountChangeRecord/Create

        public ActionResult Create()
        {
            ViewBag.Member_ID = new SelectList(db.tbMemberInfo, "Member_ID", "Member_Name");
            return View();
        }

        //
        // POST: /MemberAccountChangeRecord/Create

        [HttpPost]
        public ActionResult Create(tbMemberAccountChangeRecord tbmemberaccountchangerecord)
        {
            if (ModelState.IsValid)
            {
                db.tbMemberAccountChangeRecord.Add(tbmemberaccountchangerecord);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Member_ID = new SelectList(db.tbMemberInfo, "Member_ID", "Member_Name", tbmemberaccountchangerecord.Member_ID);
            return View(tbmemberaccountchangerecord);
        }

        //
        // GET: /MemberAccountChangeRecord/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbMemberAccountChangeRecord tbmemberaccountchangerecord = db.tbMemberAccountChangeRecord.Find(id);
            if (tbmemberaccountchangerecord == null)
            {
                return HttpNotFound();
            }
            ViewBag.Member_ID = new SelectList(db.tbMemberInfo, "Member_ID", "Member_Name", tbmemberaccountchangerecord.Member_ID);
            return View(tbmemberaccountchangerecord);
        }

        //
        // POST: /MemberAccountChangeRecord/Edit/5

        [HttpPost]
        public ActionResult Edit(tbMemberAccountChangeRecord tbmemberaccountchangerecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbmemberaccountchangerecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Member_ID = new SelectList(db.tbMemberInfo, "Member_ID", "Member_Name", tbmemberaccountchangerecord.Member_ID);
            return View(tbmemberaccountchangerecord);
        }

        //
        // GET: /MemberAccountChangeRecord/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbMemberAccountChangeRecord tbmemberaccountchangerecord = db.tbMemberAccountChangeRecord.Find(id);
            if (tbmemberaccountchangerecord == null)
            {
                return HttpNotFound();
            }
            return View(tbmemberaccountchangerecord);
        }

        //
        // POST: /MemberAccountChangeRecord/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbMemberAccountChangeRecord tbmemberaccountchangerecord = db.tbMemberAccountChangeRecord.Find(id);
            db.tbMemberAccountChangeRecord.Remove(tbmemberaccountchangerecord);
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