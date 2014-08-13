using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace UMember.Controllers
{
    public class NewsController : Controller
    {
        private dbTiderEntities db = new dbTiderEntities();

        //
        // GET: /News/

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
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

            var news = from s in db.tbNews
                       where s.Is_Delete==null || s.Is_Delete==false
                             select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                news = news.Where(s => s.Title.ToUpper()==searchString.ToUpper());
            }
            news = news.OrderByDescending(s => s.News_ID);
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(news.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /News/Details/5

        public ActionResult Details(int id = 0)
        {

            tbNews tbnews = db.tbNews.Find(id);
            if (tbnews == null)
            {
                return HttpNotFound();
            }
            return View(tbnews);
        }

        //
        // GET: /News/Create

        public ActionResult Create()
        {
            
            return View();
        }

        //
        // POST: /News/Create

        [HttpPost]
        public ActionResult Create(tbNews tbnews)
        {
            
            if (ModelState.IsValid)
            {
                tbnews.Release_Time = DateTime.Now;
                db.tbNews.Add(tbnews);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbnews);
        }

        //
        // GET: /News/Edit/5

        public ActionResult Edit(int id = 0)
        {
           
            tbNews tbnews = db.tbNews.Find(id);
            if (tbnews == null)
            {
                return HttpNotFound();
            }
           
            return View(tbnews);
        }

        //
        // POST: /News/Edit/5

        [HttpPost]
        public ActionResult Edit(tbNews tbnews)
        {
            
            if (ModelState.IsValid)
            {
                tbnews.Release_Time = DateTime.Now;
                db.Entry(tbnews).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbnews);
        }

        //
        // GET: /News/Delete/5

        public ActionResult Delete(int id = 0)
        {
            
            tbNews tbnews = db.tbNews.Find(id);
            if (tbnews == null)
            {
                return HttpNotFound();
            }
            return View(tbnews);
        }

        //
        // POST: /News/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            tbNews tbnews = db.tbNews.Find(id);
            tbnews.Is_Delete = true;
            db.Entry(tbnews).State = EntityState.Modified;
            db.SaveChanges();
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