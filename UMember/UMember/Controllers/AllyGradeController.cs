using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using UMember.Models;

namespace UMember.Controllers
{
    public class AllyGradeController : Controller
    {
        private dbTiderEntities db = new dbTiderEntities();
        //
        // GET: /AllyGrade/

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var allyGrades = from s in db.tbAllyGrade
                             where (s.Is_Delete == false || s.Is_Delete == null)
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                allyGrades = allyGrades.Where(s => s.AllyGrade_Name.ToUpper()==searchString.ToUpper()); 
            }
            allyGrades = allyGrades.OrderBy(s => s.AllyGrade_ID);
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(allyGrades.ToPagedList(pageNumber, pageSize));
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
        public ActionResult Create(AllyGradeView view )
        {
            if (ModelState.IsValid)
            {
                tbAllyGrade tballygrade = new tbAllyGrade();// ViewModelConvert.ConvertViewToModel(view);
                tballygrade.Is_Delete = false;
                tballygrade.Is_Hide = false;
                db.tbAllyGrade.Add(tballygrade);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
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
            AllyGradeView model = new AllyGradeView();// ViewModelConvert.ConvertViewToModel(tballygrade);
            return View(model);
        }

        //
        // POST: /AllyGrade/Edit/5

        [HttpPost]
        public ActionResult Edit(AllyGradeView view)
        {
            if (ModelState.IsValid)
            {
                tbAllyGrade tballygrade = new tbAllyGrade();// ViewModelConvert.ConvertViewToModel(view);
                db.Entry(tballygrade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(view);
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
            tballygrade.Is_Delete = true;
            db.Entry(tballygrade).State = EntityState.Modified;
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