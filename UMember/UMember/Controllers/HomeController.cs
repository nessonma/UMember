using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UMember.Models;
using System.Web.Security;
using System.Net;
using System.Text;


namespace UMember.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        dbTiderEntities db = new dbTiderEntities();
        public ActionResult Index()
        {
            return View();           
        }

        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                string secretPassword=FormsAuthentication.HashPasswordForStoringInConfigFile(model.Admin_Password, "SHA1");
                var result = from s in db.tbAdmin
                             where s.Admin_Name == model.Admin_Name && s.Admin_Password == secretPassword && (s.Is_Delete == false || s.Is_Delete == null)
                             select s;
               
                if (Session["ValidateCode"].ToString() != model.ValidateCode)
                {
                    ModelState.AddModelError("ValidateCode", "验证码错误");
                    return View();
                }
                if (result.ToList().Count>0)
                {
                    FormsAuthentication.SetAuthCookie(model.Admin_Name, true);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        System.Web.HttpContext.Current.Session["Admin_ID"] = result.ToList()[0].Admin_ID;
                        System.Web.HttpContext.Current.Session["Admin_Name"] = result.ToList()[0].Admin_Name;
                        System.Web.HttpContext.Current.Session["BackUp"] = 0;
                        tbAdmin admin=db.tbAdmin.Find(result.ToList()[0].Admin_ID);
                        tbRole role=db.tbRole.Find(admin.Role_ID);
                        System.Web.HttpContext.Current.Session["Role_Name"] = role.Role_Name;
                        System.Web.HttpContext.Current.Session["Role_ID"] = role.Role_ID;
                        
                        StringBuilder sb = new StringBuilder();
                        var query = from s in db.tbRoleMenu
                                    where s.Role_ID==role.Role_ID
                                    select s;
                        foreach (tbRoleMenu RoleMenu in query.ToList())
                        {
                            if (RoleMenu.Menu_Name != null)
                            {
                                sb.Append(",").Append(RoleMenu.Menu_Name);
                            }
                        }
                        if (sb.ToString() == string.Empty)
                        {
                            System.Web.HttpContext.Current.Session["Menu_Name"] = string.Empty;
                        }
                        else
                        {
                            System.Web.HttpContext.Current.Session["Menu_Name"] = sb.ToString().Substring(1);
                        }

                        IPHostEntry IpEntry = Dns.GetHostEntry(Dns.GetHostName());
                        string myip = IpEntry.AddressList[1].ToString();
                        if (IpEntry.AddressList.Length > 2)
                        {
                            myip = IpEntry.AddressList[2].ToString();
                        }
                        //tbLog log = new tbLog();
                        //log.Admin_ID = result.ToList()[0].Admin_ID;
                        //log.Login_Time = DateTime.Now;
                        //log.IP = myip;
                        //db.tbLog.Add(log);
                        
                        db.SaveChanges();
                        return RedirectToAction("Index", "Home");
                        
                    }
                }
                else
                {
                    ModelState.AddModelError("", "");
                    
                }
            }

            // If we got this far, something failed, redisplay form
            return View();
        }

        public ActionResult Quit()
        {
            System.Web.HttpContext.Current.Session.Clear();
            return RedirectToAction("LogOn");
        }
        public ActionResult GetValidateCode()
        {
            ValidateCode vCode = new ValidateCode();
            string code = vCode.CreateValidateCode(5);
            Session["ValidateCode"] = code;
            byte[] bytes = vCode.CreateValidateGraphic2(code);
            return File(bytes, @"image/jpeg");
        }
    }
}
