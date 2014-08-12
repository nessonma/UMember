using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UMember.Controllers
{
    public class BonusConfigController : Controller
    {
        private dbTiderEntities db = new dbTiderEntities();

        //奖金参数设置
        // GET: /BonusConfig/

        public ActionResult Index()
        {
            return View(db.tbBonusConfig.ToList());
        }

        //
        // GET: /BonusConfig/Details/5

        public ActionResult Details(int id = 0)
        {
            tbBonusConfig tbbonusconfig = db.tbBonusConfig.Find(id);
            if (tbbonusconfig == null)
            {
                return HttpNotFound();
            }
            return View(tbbonusconfig);
        }

        //
        // GET: /BonusConfig/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /BonusConfig/Create

        [HttpPost]
        public ActionResult Create(tbBonusConfig tbbonusconfig)
        {
            if (ModelState.IsValid)
            {
                db.tbBonusConfig.Add(tbbonusconfig);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbbonusconfig);
        }

        //
        // GET: /BonusConfig/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbBonusConfig tbbonusconfig = db.tbBonusConfig.Find(id);
            if (tbbonusconfig == null)
            {
                return HttpNotFound();
            }
            return View(tbbonusconfig);
        }

        //
        // POST: /BonusConfig/Edit/5

        [HttpPost]
        public ActionResult Edit(tbBonusConfig tbbonusconfig)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbbonusconfig).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbbonusconfig);
        }

        //
        // GET: /BonusConfig/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbBonusConfig tbbonusconfig = db.tbBonusConfig.Find(id);
            if (tbbonusconfig == null)
            {
                return HttpNotFound();
            }
            return View(tbbonusconfig);
        }

        //
        // POST: /BonusConfig/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbBonusConfig tbbonusconfig = db.tbBonusConfig.Find(id);
            db.tbBonusConfig.Remove(tbbonusconfig);
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