using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UMember.Controllers
{
    public class BonusRecordController : Controller
    {
        private dbTiderEntities db = new dbTiderEntities();

        //
        // GET: /BonusRecord/

        public ActionResult Index()
        {
            var tbbonusrecord = db.tbBonusRecord.Include(t => t.tbMemberInfo);
            return View(tbbonusrecord.ToList());
        }

        //
        // GET: /BonusRecord/Details/5

        public ActionResult Details(int id = 0)
        {
            tbBonusRecord tbbonusrecord = db.tbBonusRecord.Find(id);
            if (tbbonusrecord == null)
            {
                return HttpNotFound();
            }
            return View(tbbonusrecord);
        }

        //
        // GET: /BonusRecord/Create

        public ActionResult Create()
        {
            ViewBag.Member_ID = new SelectList(db.tbMemberInfo, "Member_ID", "Member_Name");
            return View();
        }

        //
        // POST: /BonusRecord/Create

        [HttpPost]
        public ActionResult Create(tbBonusRecord tbbonusrecord)
        {
            if (ModelState.IsValid)
            {
                db.tbBonusRecord.Add(tbbonusrecord);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Member_ID = new SelectList(db.tbMemberInfo, "Member_ID", "Member_Name", tbbonusrecord.Member_ID);
            return View(tbbonusrecord);
        }

        //
        // GET: /BonusRecord/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbBonusRecord tbbonusrecord = db.tbBonusRecord.Find(id);
            if (tbbonusrecord == null)
            {
                return HttpNotFound();
            }
            ViewBag.Member_ID = new SelectList(db.tbMemberInfo, "Member_ID", "Member_Name", tbbonusrecord.Member_ID);
            return View(tbbonusrecord);
        }

        //
        // POST: /BonusRecord/Edit/5

        [HttpPost]
        public ActionResult Edit(tbBonusRecord tbbonusrecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbbonusrecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Member_ID = new SelectList(db.tbMemberInfo, "Member_ID", "Member_Name", tbbonusrecord.Member_ID);
            return View(tbbonusrecord);
        }

        //
        // GET: /BonusRecord/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbBonusRecord tbbonusrecord = db.tbBonusRecord.Find(id);
            if (tbbonusrecord == null)
            {
                return HttpNotFound();
            }
            return View(tbbonusrecord);
        }

        //
        // POST: /BonusRecord/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbBonusRecord tbbonusrecord = db.tbBonusRecord.Find(id);
            db.tbBonusRecord.Remove(tbbonusrecord);
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