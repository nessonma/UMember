using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UMember.Controllers
{
    public class WithdrawRulesController : Controller
    {
        private dbTiderEntities db = new dbTiderEntities();

        //
        // GET: /WithdrawRules/

        public ActionResult Index()
        {
            return View(db.tbWithdrawRules.ToList());
        }

        //
        // GET: /WithdrawRules/Details/5

        public ActionResult Details(int id = 0)
        {
            tbWithdrawRules tbwithdrawrules = db.tbWithdrawRules.Find(id);
            if (tbwithdrawrules == null)
            {
                return HttpNotFound();
            }
            return View(tbwithdrawrules);
        }

        //
        // GET: /WithdrawRules/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /WithdrawRules/Create

        [HttpPost]
        public ActionResult Create(tbWithdrawRules tbwithdrawrules)
        {
            if (ModelState.IsValid)
            {
                db.tbWithdrawRules.Add(tbwithdrawrules);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbwithdrawrules);
        }

        //
        // GET: /WithdrawRules/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbWithdrawRules tbwithdrawrules = db.tbWithdrawRules.Find(id);
            if (tbwithdrawrules == null)
            {
                return HttpNotFound();
            }
            return View(tbwithdrawrules);
        }

        //
        // POST: /WithdrawRules/Edit/5

        [HttpPost]
        public ActionResult Edit(tbWithdrawRules tbwithdrawrules)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbwithdrawrules).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbwithdrawrules);
        }

        //
        // GET: /WithdrawRules/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbWithdrawRules tbwithdrawrules = db.tbWithdrawRules.Find(id);
            if (tbwithdrawrules == null)
            {
                return HttpNotFound();
            }
            return View(tbwithdrawrules);
        }

        //
        // POST: /WithdrawRules/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbWithdrawRules tbwithdrawrules = db.tbWithdrawRules.Find(id);
            db.tbWithdrawRules.Remove(tbwithdrawrules);
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