using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UMember.Controllers
{
    public class ProductController : Controller
    {
        private dbTiderEntities db = new dbTiderEntities();

        //产品管理
        // GET: /Product/

        public ActionResult Index()
        {
            return View(db.tbProduct.ToList());
        }

        //
        // GET: /Product/Details/5

        public ActionResult Details(int id = 0)
        {
            tbProduct tbproduct = db.tbProduct.Find(id);
            if (tbproduct == null)
            {
                return HttpNotFound();
            }
            return View(tbproduct);
        }

        //
        // GET: /Product/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Product/Create

        [HttpPost]
        public ActionResult Create(tbProduct tbproduct)
        {
            if (ModelState.IsValid)
            {
                db.tbProduct.Add(tbproduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbproduct);
        }

        //
        // GET: /Product/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbProduct tbproduct = db.tbProduct.Find(id);
            if (tbproduct == null)
            {
                return HttpNotFound();
            }
            return View(tbproduct);
        }

        //
        // POST: /Product/Edit/5

        [HttpPost]
        public ActionResult Edit(tbProduct tbproduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbproduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbproduct);
        }

        //
        // GET: /Product/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbProduct tbproduct = db.tbProduct.Find(id);
            if (tbproduct == null)
            {
                return HttpNotFound();
            }
            return View(tbproduct);
        }

        //
        // POST: /Product/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbProduct tbproduct = db.tbProduct.Find(id);
            db.tbProduct.Remove(tbproduct);
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