using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UMember.Controllers
{
    public class SCenterInfoController : Controller
    {
        private dbTiderEntities db = new dbTiderEntities();

        //
        // GET: /SCenterInfo/

        public ActionResult Index()
        {
            var tbscenterinfo = db.tbSCenterInfo.Include(t => t.tbSCenterAccount);
            return View(tbscenterinfo.ToList());
        }

        //
        // GET: /SCenterInfo/Details/5

        public ActionResult Details(int id = 0)
        {
            tbSCenterInfo tbscenterinfo = db.tbSCenterInfo.Find(id);
            if (tbscenterinfo == null)
            {
                return HttpNotFound();
            }
            return View(tbscenterinfo);
        }

        //
        // GET: /SCenterInfo/Create

        public ActionResult Create()
        {
            ViewBag.SCenter_ID = new SelectList(db.tbSCenterAccount, "SCenter_ID", "SCenter_ID");
            return View();
        }

        //
        // POST: /SCenterInfo/Create

        [HttpPost]
        public ActionResult Create(tbSCenterInfo tbscenterinfo)
        {
            if (ModelState.IsValid)
            {
                db.tbSCenterInfo.Add(tbscenterinfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SCenter_ID = new SelectList(db.tbSCenterAccount, "SCenter_ID", "SCenter_ID", tbscenterinfo.SCenter_ID);
            return View(tbscenterinfo);
        }

        //
        // GET: /SCenterInfo/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbSCenterInfo tbscenterinfo = db.tbSCenterInfo.Find(id);
            if (tbscenterinfo == null)
            {
                return HttpNotFound();
            }
            ViewBag.SCenter_ID = new SelectList(db.tbSCenterAccount, "SCenter_ID", "SCenter_ID", tbscenterinfo.SCenter_ID);
            return View(tbscenterinfo);
        }

        //
        // POST: /SCenterInfo/Edit/5

        [HttpPost]
        public ActionResult Edit(tbSCenterInfo tbscenterinfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbscenterinfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SCenter_ID = new SelectList(db.tbSCenterAccount, "SCenter_ID", "SCenter_ID", tbscenterinfo.SCenter_ID);
            return View(tbscenterinfo);
        }

        //
        // GET: /SCenterInfo/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbSCenterInfo tbscenterinfo = db.tbSCenterInfo.Find(id);
            if (tbscenterinfo == null)
            {
                return HttpNotFound();
            }
            return View(tbscenterinfo);
        }

        //
        // POST: /SCenterInfo/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbSCenterInfo tbscenterinfo = db.tbSCenterInfo.Find(id);
            db.tbSCenterInfo.Remove(tbscenterinfo);
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