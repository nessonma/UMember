using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UMember.Controllers
{
    public class SCenterAccountController : Controller
    {
        private dbTiderEntities db = new dbTiderEntities();

        //
        // GET: /SCenterAccount/

        public ActionResult Index()
        {
            var tbscenteraccount = db.tbSCenterAccount.Include(t => t.tbSCenterInfo);
            return View(tbscenteraccount.ToList());
        }

        //
        // GET: /SCenterAccount/Details/5

        public ActionResult Details(int id = 0)
        {
            tbSCenterAccount tbscenteraccount = db.tbSCenterAccount.Find(id);
            if (tbscenteraccount == null)
            {
                return HttpNotFound();
            }
            return View(tbscenteraccount);
        }

        //
        // GET: /SCenterAccount/Create

        public ActionResult Create()
        {
            ViewBag.SCenter_ID = new SelectList(db.tbSCenterInfo, "SCenter_ID", "SCenter_Name");
            return View();
        }

        //
        // POST: /SCenterAccount/Create

        [HttpPost]
        public ActionResult Create(tbSCenterAccount tbscenteraccount)
        {
            if (ModelState.IsValid)
            {
                db.tbSCenterAccount.Add(tbscenteraccount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SCenter_ID = new SelectList(db.tbSCenterInfo, "SCenter_ID", "SCenter_Name", tbscenteraccount.SCenter_ID);
            return View(tbscenteraccount);
        }

        //
        // GET: /SCenterAccount/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbSCenterAccount tbscenteraccount = db.tbSCenterAccount.Find(id);
            if (tbscenteraccount == null)
            {
                return HttpNotFound();
            }
            ViewBag.SCenter_ID = new SelectList(db.tbSCenterInfo, "SCenter_ID", "SCenter_Name", tbscenteraccount.SCenter_ID);
            return View(tbscenteraccount);
        }

        //
        // POST: /SCenterAccount/Edit/5

        [HttpPost]
        public ActionResult Edit(tbSCenterAccount tbscenteraccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbscenteraccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SCenter_ID = new SelectList(db.tbSCenterInfo, "SCenter_ID", "SCenter_Name", tbscenteraccount.SCenter_ID);
            return View(tbscenteraccount);
        }

        //
        // GET: /SCenterAccount/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbSCenterAccount tbscenteraccount = db.tbSCenterAccount.Find(id);
            if (tbscenteraccount == null)
            {
                return HttpNotFound();
            }
            return View(tbscenteraccount);
        }

        //
        // POST: /SCenterAccount/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbSCenterAccount tbscenteraccount = db.tbSCenterAccount.Find(id);
            db.tbSCenterAccount.Remove(tbscenteraccount);
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