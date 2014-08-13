using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Sql;
using System.Data.SqlClient;
//using Microsoft.SqlServer.Management.Smo;
//using Microsoft.SqlServer.Management.Common;

namespace UMember.Controllers
{
    public class DataBaseStateController : Controller
    {
        private dbTiderEntities db = new dbTiderEntities();

        //
        // GET: /DataBaseState/

      
       

        public ActionResult Index()
        {
            ViewBag.State = 0;
            ViewBag.Reason = "";
            int id = (from s in db.tbDataBaseState
                         select s.id).Max();
            if (id != 0)
            {
                tbDataBaseState dbstate = db.tbDataBaseState.Find(id);
                if (dbstate.Is_Closed == true)
                    ViewBag.State = 1;
                ViewBag.Reason = dbstate.Reason;
            }
            return View();
        }

        //
        // POST: /DataBaseState/Delete/5

        [HttpPost]
        public ActionResult Close(tbDataBaseState tbdatabasestate)
        {
            int id = (from s in db.tbDataBaseState
                      select s.id).Max();
            tbDataBaseState dbstate = db.tbDataBaseState.Find(id);
            if (dbstate.Is_Closed == false)
            tbdatabasestate.Is_Closed = true;
            else
                tbdatabasestate.Is_Closed = true;
            db.tbDataBaseState.Add(tbdatabasestate);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult BackUp(string tip)
        {
            ViewBag.Tip = tip;
            return View();
        }

        //
        // POST: /DataBaseState/Delete/5

        [HttpPost]
        public ActionResult BackUpPost()
        {
            string result= "备份成功"; 
            SqlConnection con = new SqlConnection(db.Database.Connection.ConnectionString);
            try
            {
                string dt = string.Format("{0:u}", DateTime.Now);
                dt = dt.Replace("-", "_");
                dt = dt.Replace(":", "_");
                dt = dt.Replace(" ", "_");
                
                //string path = @"C:\Program Files (x86)\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\Backup\dbfwmember_" + dt + ".Bak";
                //string path = @"C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\Backup\dbfwmember_" + dt + ".Bak";
                //"FWDingBo_"备份文件名
                string path = System.Configuration.ConfigurationManager.AppSettings["backupaddress"] + "FWDingBo_" + dt + ".Bak"; ;
                // backup database FWDingBo to disk=指数据库名称
                string backupstr = "backup database FWDingBo to disk='" + path + "';";
                //SqlConnection con = new SqlConnection("server=.;uid=sa;pwd=sa;");

                SqlCommand cmd = new SqlCommand(backupstr, con);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                System.Web.HttpContext.Current.Session["BackUp"] = 1;
                return RedirectToAction("BackUp", new { tip = result });
            }
            catch (Exception ex)
            {
                result = "备份失败";
                con.Close();
                return RedirectToAction("BackUp", new { tip = result });
            }
            




            //try
            //{
            //    Backup backup = new Backup();
            //    backup.Action = BackupActionType.Database;
            //    backup.BackupSetName = "Database Backup";
            //    backup.BackupSetDescription = "Full database backup";
            //    backup.Database = "dbfwmember";
            //    backup.Initialize = true;
            //    backup.ContinueAfterError = true;
            //    backup.LogTruncation = BackupTruncateLogType.Truncate;

            //    string dt = string.Format("{0:u}", DateTime.Now);
            //    dt = dt.Replace("-","_");
            //    dt = dt.Replace(":", "_");
            //    dt = dt.Replace(" ", "_");
                
            //    //string name = @"C:\Program Files (x86)\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\Backup\dbfwmember_"+dt+".Bak";
            //    string name = @"C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\Backup\dbfwmember_" + dt + ".Bak";
            //    BackupDeviceItem device = new BackupDeviceItem(name, DeviceType.File);
            //    backup.Devices.Add(device);

            //    ServerConnection serConn = new ServerConnection(".");
            //    Server sqlServer = new Server(serConn);
            //    backup.SqlBackup(sqlServer);

            //    backup.Devices.Remove(device);
            //    return RedirectToAction("BackUp");
            //}
            //catch (Exception ex)
            //{
            //    return RedirectToAction("BackUp");
            //}
            
        }



    }
}