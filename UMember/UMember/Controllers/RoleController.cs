using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

using PagedList;
using UMember.Models;
namespace UMember.Controllers
{
    public class RoleController : Controller
    {
        private dbTiderEntities db = new dbTiderEntities();

        //
        // GET: /Role/

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

            var roles = from s in db.tbRole
                             select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                roles = roles.Where(s => s.Role_Name.ToUpper()==searchString.ToUpper());
            }
            roles = roles.OrderBy(s => s.Role_Name);
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(roles.ToPagedList(pageNumber, pageSize));
        }

       
        //
        // GET: /Role/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Role/Create

        [HttpPost]
        public ActionResult Create(RoleView rolevView)
        {
            
            if (ModelState.IsValid)
            {
                tbRole role = new tbRole();
                role.Role_Name = rolevView.Role_Name;
                role.Is_Delete = rolevView.Is_Delete;
                role.Is_Hide = rolevView.Is_Hide;
                db.tbRole.Add(role);

                foreach (string s in rolevView.Menu_Name)
                {
                    tbRoleMenu roleMenu = new tbRoleMenu();
                    roleMenu.Role_ID = role.Role_ID;
                    roleMenu.Menu_Name = s;
                    db.tbRoleMenu.Add(roleMenu);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rolevView);
        }

        //
        // GET: /Role/Edit/5

        public ActionResult Edit(int id = 0)
        {
            
            tbRole tbrole = db.tbRole.Find(id);
            RoleView rv = new RoleView();
            rv.Role_ID = id;
            rv.Role_Name = tbrole.Role_Name;
            if (tbrole.Is_Hide == true)
                rv.Is_Hide = true;
            if (tbrole.Is_Delete == true)
                rv.Is_Delete = true;
           
            var query= from s in db.tbRoleMenu
                       where s.Role_ID==id
                       select s;
            if (query.ToList().Count > 0)
            {
                int i = 0;
                string[] menus = new string[query.ToList().Count];
                foreach (tbRoleMenu rm in query.ToList())
                {
                    menus[i] = rm.Menu_Name;
                    i++;
                }
                rv.Menu_Name = menus;

            }
            else
            {
                string[] menus = new string[1];
                menus[0] = "";
                rv.Menu_Name = menus;
            }
            
            if (tbrole == null)
            {
                return HttpNotFound();
            }
            return View(rv);
        }

        //
        // POST: /Role/Edit/5

        [HttpPost]
        public ActionResult Edit(RoleView rolevView)
        {
            
            if (ModelState.IsValid)
            {
                tbRole role = db.tbRole.Find(rolevView.Role_ID);
                role.Role_Name = rolevView.Role_Name;
                role.Is_Delete = rolevView.Is_Delete;
                role.Is_Hide = rolevView.Is_Hide;
                db.Entry(role).State=EntityState.Modified;

                var query = from s in db.tbRoleMenu
                            where s.Role_ID == role.Role_ID
                            select s;

                foreach(tbRoleMenu m in query.ToList())
                {
                    db.tbRoleMenu.Remove(m);
                }
                
                foreach (string s in rolevView.Menu_Name)
                {
                    tbRoleMenu roleMenu = new tbRoleMenu();
                    roleMenu.Role_ID = role.Role_ID;
                    roleMenu.Menu_Name = s;
                    db.tbRoleMenu.Add(roleMenu);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rolevView);
        }

       
       
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}