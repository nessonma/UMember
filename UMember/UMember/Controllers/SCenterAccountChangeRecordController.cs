using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UMember.Controllers
{
    public class SCenterAccountChangeRecordController : Controller
    {
        private dbTiderEntities db = new dbTiderEntities();

        //
        // GET: /SCenterAccountChangeRecord/

        public ActionResult Index()
        {
            var tbscenteraccountchangerecord = db.tbSCenterAccountChangeRecord.Include(t => t.tbSCenterInfo);
            return View(tbscenteraccountchangerecord.ToList());
        }

        //
        // GET: /SCenterAccountChangeRecord/Details/5

        public ActionResult Details(int id = 0)
        {
            tbSCenterAccountChangeRecord tbscenteraccountchangerecord = db.tbSCenterAccountChangeRecord.Find(id);
            if (tbscenteraccountchangerecord == null)
            {
                return HttpNotFound();
            }
            return View(tbscenteraccountchangerecord);
        }

        //
        // GET: /SCenterAccountChangeRecord/Create

        public ActionResult Create()
        {
            ViewBag.SCenter_ID = new SelectList(db.tbSCenterInfo, "SCenter_ID", "SCenter_Name");
            return View();
        }

        //
        // POST: /SCenterAccountChangeRecord/Create

        [HttpPost]
        public ActionResult Create(tbSCenterAccountChangeRecord tbscenteraccountchangerecord)
        {
            if (ModelState.IsValid)
            {
                db.tbSCenterAccountChangeRecord.Add(tbscenteraccountchangerecord);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SCenter_ID = new SelectList(db.tbSCenterInfo, "SCenter_ID", "SCenter_Name", tbscenteraccountchangerecord.SCenter_ID);
            return View(tbscenteraccountchangerecord);
        }

        //
        // GET: /SCenterAccountChangeRecord/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbSCenterAccountChangeRecord tbscenteraccountchangerecord = db.tbSCenterAccountChangeRecord.Find(id);
            if (tbscenteraccountchangerecord == null)
            {
                return HttpNotFound();
            }
            ViewBag.SCenter_ID = new SelectList(db.tbSCenterInfo, "SCenter_ID", "SCenter_Name", tbscenteraccountchangerecord.SCenter_ID);
            return View(tbscenteraccountchangerecord);
        }

        //
        // POST: /SCenterAccountChangeRecord/Edit/5

        [HttpPost]
        public ActionResult Edit(tbSCenterAccountChangeRecord tbscenteraccountchangerecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbscenteraccountchangerecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SCenter_ID = new SelectList(db.tbSCenterInfo, "SCenter_ID", "SCenter_Name", tbscenteraccountchangerecord.SCenter_ID);
            return View(tbscenteraccountchangerecord);
        }

        //
        // GET: /SCenterAccountChangeRecord/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbSCenterAccountChangeRecord tbscenteraccountchangerecord = db.tbSCenterAccountChangeRecord.Find(id);
            if (tbscenteraccountchangerecord == null)
            {
                return HttpNotFound();
            }
            return View(tbscenteraccountchangerecord);
        }

        //
        // POST: /SCenterAccountChangeRecord/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbSCenterAccountChangeRecord tbscenteraccountchangerecord = db.tbSCenterAccountChangeRecord.Find(id);
            db.tbSCenterAccountChangeRecord.Remove(tbscenteraccountchangerecord);
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