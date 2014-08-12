using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UMember.Controllers
{
    public class JmailController : Controller
    {
        private dbTiderEntities db = new dbTiderEntities();

        //发送邮件
        // GET: /Jmail/

        public ActionResult Index()
        {
            return View(db.tbJmail.ToList());
        }

        //
        // GET: /Jmail/Details/5

        public ActionResult Details(int id = 0)
        {
            tbJmail tbjmail = db.tbJmail.Find(id);
            if (tbjmail == null)
            {
                return HttpNotFound();
            }
            return View(tbjmail);
        }

        //
        // GET: /Jmail/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Jmail/Create

        [HttpPost]
        public ActionResult Create(tbJmail tbjmail)
        {
            if (ModelState.IsValid)
            {
                db.tbJmail.Add(tbjmail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbjmail);
        }

        //
        // GET: /Jmail/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbJmail tbjmail = db.tbJmail.Find(id);
            if (tbjmail == null)
            {
                return HttpNotFound();
            }
            return View(tbjmail);
        }

        //
        // POST: /Jmail/Edit/5

        [HttpPost]
        public ActionResult Edit(tbJmail tbjmail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbjmail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbjmail);
        }

        //
        // GET: /Jmail/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbJmail tbjmail = db.tbJmail.Find(id);
            if (tbjmail == null)
            {
                return HttpNotFound();
            }
            return View(tbjmail);
        }

        //
        // POST: /Jmail/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbJmail tbjmail = db.tbJmail.Find(id);
            db.tbJmail.Remove(tbjmail);
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