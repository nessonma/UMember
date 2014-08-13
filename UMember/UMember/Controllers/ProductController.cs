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
    public class ProductController : Controller
    {
        private dbTiderEntities db = new dbTiderEntities();

        //
        // GET: /Product/

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

            var products = from s in db.tbProduct
                           where (s.Is_Delete==false||s.Is_Delete==null)
                             select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.Product_Name.ToUpper()==searchString.ToUpper());
            }
            products = products.OrderBy(s => s.Product_ID);
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));
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
                tbproduct.Is_Delete = false;
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
            tbproduct.Is_Delete = true;
            db.Entry(tbproduct).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult FirstProduct(string Temp_Guid, int Step,int? page)
        {            
            ViewBag.Temp_Guid = Temp_Guid;
            var products = from s in db.tbProduct
                           //where s.Is_Active==true && (s.Is_Delete==null || s.Is_Delete==false)
                           select s;

            products = products.OrderByDescending(s => s.Product_ID);
            ViewBag.Step = Step - 1;
            return View(products.ToList());
        }
    }
}