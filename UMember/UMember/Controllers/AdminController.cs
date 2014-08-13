using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using UMember.Models;
using System.Web.Security;
namespace UMember.Controllers
{
    public class AdminController : Controller
    {
        private dbTiderEntities db = new dbTiderEntities();

        //
        // GET: /Admin/

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

            var admins = from s in db.tbAdmin
                         where (s.Is_Delete==false || s.Is_Delete==null)
                             select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                admins = admins.Where(s => s.Admin_Name.ToUpper()==searchString.ToUpper());
            }
            admins = admins.OrderBy(s => s.Admin_Name);
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(admins.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Admin/Details/5

        public ActionResult Details(int id = 0)
        {
            tbAdmin tbadmin = db.tbAdmin.Find(id);
            if (tbadmin == null)
            {
                return HttpNotFound();
            }
            return View(tbadmin);
        }

        //
        // GET: /Admin/Create

        public ActionResult Create()
        {
            ViewBag.Role_ID = new SelectList(db.tbRole, "Role_ID", "Role_Name");
            return View();
        }

        //
        // POST: /Admin/Create

        [HttpPost]
        public ActionResult Create(AdminView v)
        {
            if (ModelState.IsValid)
            {
                if (v.Admin_Password == v.Confirm_Admin_Password)
                {
                    tbAdmin tbadmin = new tbAdmin();
                    tbadmin.Admin_Name = v.Admin_Name;
                    tbadmin.Admin_Password = FormsAuthentication.HashPasswordForStoringInConfigFile(v.Admin_Password, "SHA1");
                    tbadmin.Role_ID = v.Role_ID;
                    db.tbAdmin.Add(tbadmin);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Confirm_New_Admin_Password", "确认密码与密码不一致");
                    return View();
                }
            }

            ViewBag.Role_ID = new SelectList(db.tbRole, "Role_ID", "Role_Name", v.Role_ID);
            return View(v);
        }

        //
        // GET: /Admin/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbAdmin tbadmin = db.tbAdmin.Find(id);
            if (tbadmin == null)
            {
                return HttpNotFound();
            }
            AdminView v = new AdminView();
            v.Admin_Name = tbadmin.Admin_Name;
            v.Role_ID = tbadmin.Role_ID;
            v.Admin_ID = tbadmin.Admin_ID;
            ViewBag.Role_ID = new SelectList(db.tbRole, "Role_ID", "Role_Name", tbadmin.Role_ID);
            return View(v);
        }

        //
        // POST: /Admin/Edit/5

        [HttpPost]
        public ActionResult Edit(AdminView v)
        {
            tbAdmin tbadmin = db.tbAdmin.Find(v.Admin_ID);
            if (ModelState.IsValid)
            {
                if (v.Admin_Password == v.Confirm_Admin_Password)
                {
                    tbadmin.Admin_Name = v.Admin_Name;
                    tbadmin.Admin_Password = FormsAuthentication.HashPasswordForStoringInConfigFile(v.Admin_Password, "SHA1");
                    tbadmin.Role_ID = v.Role_ID;
                    db.Entry(tbadmin).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Confirm_New_Admin_Password", "确认密码与密码不一致");
                    return View();
                }
            }
            ViewBag.Role_ID = new SelectList(db.tbRole, "Role_ID", "Role_Name", tbadmin.Role_ID);
            return View(v);
        }

        //
        // GET: /Admin/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbAdmin tbadmin = db.tbAdmin.Find(id);
            if (tbadmin == null)
            {
                return HttpNotFound();
            }
            return View(tbadmin);
        }

        //
        // POST: /Admin/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbAdmin tbadmin = db.tbAdmin.Find(id);
            tbadmin.Is_Delete = true;
            db.Entry(tbadmin).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult ChangePassword(string Tip)
        {
            ChangePasswordView cpv = new ChangePasswordView();
            ViewBag.Tip = Tip;
            return View(cpv);
        }

        //
        // POST: /Admin/Edit/5

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordView cpv)
        {
            if (ModelState.IsValid)
            {
                if (cpv.New_Admin_Password == cpv.Confirm_New_Admin_Password)
                {

                    int id = int.Parse(System.Web.HttpContext.Current.Session["Admin_ID"].ToString());
                    tbAdmin admin = db.tbAdmin.Find(id);
                    admin.Admin_Password = FormsAuthentication.HashPasswordForStoringInConfigFile(cpv.New_Admin_Password, "SHA1"); 
                    db.Entry(admin).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.Tip = "密码修改成功";
                    return View();
                }
                else
                {
                    ModelState.AddModelError("Confirm_New_Admin_Password", "确认新密码与新密码不一致");
                    return View();
                }
            }
            ViewBag.Tip = "密码修改失败";
            return View();
        }
    }
}