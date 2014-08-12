using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UMember.Controllers
{
    public class AllyGradeController : Controller
    {
        private dbTiderEntities db = new dbTiderEntities();

        //
        // GET: /AllyGrade/

        public ActionResult Index()
        {
            return View(db.tbAllyGrade.ToList());
        }

        //
        // GET: /AllyGrade/Details/5

        public ActionResult Details(int id = 0)
        {
            tbAllyGrade tballygrade = db.tbAllyGrade.Find(id);
            if (tballygrade == null)
            {
                return HttpNotFound();
            }
            return View(tballygrade);
        }

        //
        // GET: /AllyGrade/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /AllyGrade/Create

        [HttpPost]
        public ActionResult Create(tbAllyGrade tballygrade)
        {
            if (ModelState.IsValid)
            {
                db.tbAllyGrade.Add(tballygrade);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tballygrade);
        }

        //
        // GET: /AllyGrade/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbAllyGrade tballygrade = db.tbAllyGrade.Find(id);
            if (tballygrade == null)
            {
                return HttpNotFound();
            }
            return View(tballygrade);
        }

        //
        // POST: /AllyGrade/Edit/5

        [HttpPost]
        public ActionResult Edit(tbAllyGrade tballygrade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tballygrade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tballygrade);
        }

        //
        // GET: /AllyGrade/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbAllyGrade tballygrade = db.tbAllyGrade.Find(id);
            if (tballygrade == null)
            {
                return HttpNotFound();
            }
            return View(tballygrade);
        }

        //
        // POST: /AllyGrade/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbAllyGrade tballygrade = db.tbAllyGrade.Find(id);
            db.tbAllyGrade.Remove(tballygrade);
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