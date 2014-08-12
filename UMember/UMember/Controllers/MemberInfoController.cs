using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UMember.Controllers
{
    public class MemberInfoController : Controller
    {
        private dbTiderEntities db = new dbTiderEntities();

        /// <summary>
        /// 秒结的算法
        /// </summary>
        public void SecondCompute()
        {
            
        }



        //会员信息表---后台管理员使用
        // GET: /MemberInfo/

        public ActionResult Index()
        {
            var tbmemberinfo = db.tbMemberInfo.Include(t => t.tbAllyGrade).Include(t => t.tbMemberAccount).Include(t => t.tbSCenterInfo);
            return View(tbmemberinfo.ToList());
        }

       

        //注册会员
        // GET: /MemberInfo/Create

        public ActionResult Create()
        {
            ViewBag.AllyGrade_ID = new SelectList(db.tbAllyGrade, "AllyGrade_ID", "AllyGrade_Name");
            ViewBag.Member_ID = new SelectList(db.tbMemberAccount, "Member_ID", "Member_ID");
            ViewBag.SCenter_ID = new SelectList(db.tbSCenterInfo, "SCenter_ID", "SCenter_Name");
            return View();
        }

        //
        // POST: /MemberInfo/Create

        [HttpPost]
        public ActionResult Create(tbMemberInfo tbmemberinfo)
        {
            if (ModelState.IsValid)
            {
                db.tbMemberInfo.Add(tbmemberinfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AllyGrade_ID = new SelectList(db.tbAllyGrade, "AllyGrade_ID", "AllyGrade_Name", tbmemberinfo.AllyGrade_ID);
            ViewBag.Member_ID = new SelectList(db.tbMemberAccount, "Member_ID", "Member_ID", tbmemberinfo.Member_ID);
            ViewBag.SCenter_ID = new SelectList(db.tbSCenterInfo, "SCenter_ID", "SCenter_Name", tbmemberinfo.SCenter_ID);
            return View(tbmemberinfo);
        }

        //修改会员信息--待用
        // GET: /MemberInfo/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbMemberInfo tbmemberinfo = db.tbMemberInfo.Find(id);
            if (tbmemberinfo == null)
            {
                return HttpNotFound();
            }
            ViewBag.AllyGrade_ID = new SelectList(db.tbAllyGrade, "AllyGrade_ID", "AllyGrade_Name", tbmemberinfo.AllyGrade_ID);
            ViewBag.Member_ID = new SelectList(db.tbMemberAccount, "Member_ID", "Member_ID", tbmemberinfo.Member_ID);
            ViewBag.SCenter_ID = new SelectList(db.tbSCenterInfo, "SCenter_ID", "SCenter_Name", tbmemberinfo.SCenter_ID);
            return View(tbmemberinfo);
        }

        //
        // POST: /MemberInfo/Edit/5

        [HttpPost]
        public ActionResult Edit(tbMemberInfo tbmemberinfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbmemberinfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AllyGrade_ID = new SelectList(db.tbAllyGrade, "AllyGrade_ID", "AllyGrade_Name", tbmemberinfo.AllyGrade_ID);
            ViewBag.Member_ID = new SelectList(db.tbMemberAccount, "Member_ID", "Member_ID", tbmemberinfo.Member_ID);
            ViewBag.SCenter_ID = new SelectList(db.tbSCenterInfo, "SCenter_ID", "SCenter_Name", tbmemberinfo.SCenter_ID);
            return View(tbmemberinfo);
        }

        //删除会员---待用
        // GET: /MemberInfo/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbMemberInfo tbmemberinfo = db.tbMemberInfo.Find(id);
            if (tbmemberinfo == null)
            {
                return HttpNotFound();
            }
            return View(tbmemberinfo);
        }

        //
        // POST: /MemberInfo/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbMemberInfo tbmemberinfo = db.tbMemberInfo.Find(id);
            db.tbMemberInfo.Remove(tbmemberinfo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        /// <summary>
        /// 推荐额度
        /// </summary>
        /// <returns></returns>
        public ActionResult RecommendAmountIndex()
        {
            return View();
        }

        /// <summary>
        /// 会员升级
        /// </summary>
        /// <returns></returns>
        public ActionResult UpGradeIndex()
        {
            return View();
        }

        /// <summary>
        /// 会员升级经理
        /// </summary>
        /// <returns></returns>
        public ActionResult UpMarketingManagerLevelIndex()
        {
            return View();
        }
    }
}