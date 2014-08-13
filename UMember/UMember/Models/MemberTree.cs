using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace UMember.Models
{
    public static class MemberTree
    {

        public static void getRecommenderAncestors(int memberID, ref ArrayList arr, ref dbTiderEntities dbContext)
        {
            tbMemberInfo member = dbContext.tbMemberInfo.Find(memberID);
            if (member.R_ID != null)
            {
                arr.Add(member.R_ID);
                getRecommenderAncestors(member.R_ID, ref arr, ref dbContext);
            }
        }
        public static void getParentAncestors(int memberID, ref ArrayList arr, ref dbTiderEntities dbContext)
        {
            tbMemberInfo member = dbContext.tbMemberInfo.Find(memberID);
            if (member.P_ID != null)
            {
                tbMemberInfo parent = dbContext.tbMemberInfo.Find(member.P_ID);
                ParentAncestorPosition position = new ParentAncestorPosition();
                position.parentInfo = parent;
                if (member.Qu == 1)
                    position.Is_Right = true;
                else
                    position.Is_Right = false;
                arr.Add(position);
                getParentAncestors(member.P_ID, ref arr, ref dbContext);
            }

        }


        //获得12层的推荐关系tree
        public static void getRecommenderDescendants(int memberID, ref TreeModel treeList, ref int Width)
        {
            //if (Width > 0 && memberID > 0)
            //{
            //    dbTiderEntities dbContext = new dbTiderEntities();
            //    tbMemberInfo member = dbContext.tbMemberInfo.Find(memberID);
            //    tbMemberInvestmentAmount amount = dbContext.tbMemberInvestmentAmount.Find(memberID);
            //    double sum = 0;

            //    if (amount != null)
            //    {
            //        sum = (amount.R_Sum_Add ?? 0);
            //    }
            //    TreeModel tree = new TreeModel();
            //    tree.Id = memberID;
            //    tree.Name = member.Member_Name + "(" + sum.ToString() + ")";
            //    tree.Is_Enabled = member.Is_Enabled ?? false;

            //    var query = from s in dbContext.tbMemberInfo
            //                where s.Recommender_ID == memberID
            //                select s;
            //    if (query.ToList().Count != 0)
            //    {

            //        foreach (tbMemberInfo m in query.ToList())
            //        {
            //            int Width2 = Width;
            //            getRecommenderDescendants(m.Member_ID, ref tree, ref Width2);
            //        }

            //    }
            //    Width = Width - 1;

            //    treeList.List.Add(tree);


            //}

        }

        //获得层的层级关系
        public static void getParentDescendants(int? memberID, int parentID, bool is_Can_Register, ref List<ParentDescendantPosition> list, ref int Depth)
        {
            //if (Depth > 0)
            //{
            //    if (memberID == null)
            //    {
            //        ParentDescendantPosition empty = new ParentDescendantPosition();
            //        empty.title = "空";
            //        if (is_Can_Register)
            //        {
            //            empty.title = "注册";
            //        }
            //        empty.parent_id = parentID;
            //        empty.height = Depth;
            //        empty.memberInfo = null;
            //        list.Add(empty);
            //        Depth = Depth - 1;
            //        int Depth2 = Depth;
            //        getParentDescendants(null, parentID, false, ref list, ref Depth);
            //        getParentDescendants(null, parentID, false, ref list, ref Depth2);
            //    }
            //    else
            //    {
            //        ParentDescendantPosition position = new ParentDescendantPosition();
            //        dbTiderEntities dbContext = new dbTiderEntities();
            //        tbMemberInfo member = dbContext.tbMemberInfo.Find(memberID);
            //        position.memberInfo = member;
            //        position.height = Depth;
            //        if (member.tbAllyGrade == null)
            //        {
            //            position.title = member.Member_Name;
            //        }
            //        else
            //        {
            //            position.title = member.Member_Name + "(" + member.tbAllyGrade.AllyGrade_Name + ")";
            //        }

            //        if (member.Is_Enabled == true)
            //        {

            //            Depth = Depth - 1;
            //            int Depth2 = Depth;

            //            var query = from s in dbContext.tbMemberInfo
            //                        where s.Parent_ID == member.Member_ID
            //                        select s;

            //            if (query.ToList().Count == 0)
            //            {
            //                getParentDescendants(null, member.Member_ID, true, ref list, ref Depth);
            //                getParentDescendants(null, member.Member_ID, false, ref list, ref Depth2);
            //            }
            //            else if (query.ToList().Count == 1)
            //            {
            //                if (query.ToList()[0].Is_Enabled == true)
            //                {
            //                    getParentDescendants(query.ToList()[0].Member_ID, member.Member_ID, true, ref list, ref Depth);
            //                    getParentDescendants(null, member.Member_ID, true, ref list, ref Depth2);
            //                }
            //                else
            //                {
            //                    getParentDescendants(query.ToList()[0].Member_ID, member.Member_ID, false, ref list, ref Depth);
            //                    getParentDescendants(null, member.Member_ID, false, ref list, ref Depth2);
            //                }
            //            }
            //            else
            //            {
            //                getParentDescendants(query.ToList()[0].Member_ID, member.Member_ID, true, ref list, ref Depth);
            //                getParentDescendants(query.ToList()[1].Member_ID, member.Member_ID, true, ref list, ref Depth2);
            //            }

            //        }
            //        else
            //        {
            //            Depth = Depth - 1;
            //            int Depth2 = Depth;
            //            getParentDescendants(null, parentID, false, ref list, ref Depth);
            //            getParentDescendants(null, parentID, false, ref list, ref Depth2);
            //        }
            //        list.Add(position);

            //    }
            //}

        }

        //获得层的层级关系
        public static void getParentDescendants(int? memberID, int parentID, ref List<ParentDescendantPosition> list, ref int Depth)
        {
            //if (Depth > 0)
            //{
            //    if (memberID == null)
            //    {

            //    }
            //    else
            //    {
            //        ParentDescendantPosition position = new ParentDescendantPosition();
            //        dbTiderEntities dbContext = new dbTiderEntities();
            //        tbMemberInfo member = dbContext.tbMemberInfo.Find(memberID);
            //        position.memberInfo = member;

            //        if (member.Is_Enabled == true)
            //        {

            //            Depth = Depth - 1;
            //            int Depth2 = Depth;

            //            var query = from s in dbContext.tbMemberInfo
            //                        where s.Parent_ID == member.Member_ID
            //                        select s;

            //            if (query.ToList().Count == 0)
            //            {
            //                getParentDescendants(null, member.Member_ID, ref list, ref Depth);
            //                getParentDescendants(null, member.Member_ID, ref list, ref Depth2);
            //            }
            //            else if (query.ToList().Count == 1)
            //            {
            //                if (query.ToList()[0].Is_Enabled == true)
            //                {
            //                    getParentDescendants(query.ToList()[0].Member_ID, member.Member_ID, ref list, ref Depth);
            //                    getParentDescendants(null, member.Member_ID, ref list, ref Depth2);
            //                }
            //                else
            //                {
            //                    getParentDescendants(query.ToList()[0].Member_ID, member.Member_ID, ref list, ref Depth);
            //                    getParentDescendants(null, member.Member_ID, ref list, ref Depth2);
            //                }
            //            }
            //            else
            //            {
            //                getParentDescendants(query.ToList()[0].Member_ID, member.Member_ID, ref list, ref Depth);
            //                getParentDescendants(query.ToList()[1].Member_ID, member.Member_ID, ref list, ref Depth2);
            //            }

            //        }
            //        else
            //        {
            //            Depth = Depth - 1;
            //            int Depth2 = Depth;
            //            getParentDescendants(null, parentID, ref list, ref Depth);
            //            getParentDescendants(null, parentID, ref list, ref Depth2);
            //        }
            //        list.Add(position);

            //    }
            //}

        }


        //获得12层的推荐关系tree
        public static void getRecommenderAmount(int memberID, ref double R_Sum_History, ref int R_Member_Count)
        {
            //dbTiderEntities dbContext = new dbTiderEntities();
            //var query = from s in dbContext.tbMemberInfo
            //            where s.R_ID == memberID
            //            select s;
            //if (query.ToList().Count != 0)
            //{
            //    foreach (tbMemberInfo m in query.ToList())
            //    {
            //        R_Sum_History = R_Sum_History + (m.Sum_PV);
            //        R_Member_Count = R_Member_Count + 1;

            //        getRecommenderAmount(m.Member_ID, ref  R_Sum_History, ref R_Member_Count);
            //    }
            //}



        }

        //获得12层的推荐关系tree
        public static void getRecommenderAmountByTime(int memberID, string startTime, string endTime, ref double R_Sum_History, ref int R_Member_Count)
        {
            //    dbTiderEntities dbContext = new dbTiderEntities();
            //    var query = from s in dbContext.tbMemberInfo
            //                where s.R_ID == memberID
            //                select s;

            //    if (!String.IsNullOrEmpty(startTime))
            //    {
            //        DateTime dt = DateTime.Parse(startTime);
            //        query = query.Where(s => s.Register_Time >= dt);
            //    }
            //    if (!String.IsNullOrEmpty(endTime))
            //    {
            //        DateTime dt = DateTime.Parse(endTime);
            //        query = query.Where(s => s.Register_Time <= dt);
            //    }

            //    if (query.ToList().Count != 0)
            //    {
            //        foreach (tbMemberInfo m in query.ToList())
            //        {
            //            R_Sum_History = R_Sum_History + m.Sum_PV;
            //            R_Member_Count = R_Member_Count + 1;

            //            getRecommenderAmount(m.Member_ID, ref  R_Sum_History, ref R_Member_Count);
            //        }
            //    }

            //}



        }
    }
    public class ParentAncestorPosition
    {
        public tbMemberInfo parentInfo;
        public bool Is_Right;
        public bool Is_Enabled;        
    }

    public class ParentDescendantPosition
    {
        public tbMemberInfo memberInfo;
        public int height;
        public string title;
        public int parent_id;
        

    }

    public class RecommendAmount
    {
        public int Member_ID;
        public double R_Sum_History;
        public int R_Member_Count;
        public string Member_Name;
    }

    public class TreeModel
    {
        public TreeModel()
        {
            this.List = new List<TreeModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool Is_Enabled { get; set; }
        public double SumMarketingPV { get; set; }
        public IList<TreeModel> List { get; private set; }

        public bool IsChild
        {
            get
            {
                return this.List.Count == 0;
            }
        }
        
    }
}