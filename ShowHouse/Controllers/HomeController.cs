using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ShowHouse.DataBase.DAL;
using ShowHouse.DataBase.Model;

namespace ShowHouse.Controllers
{
    //通用数据模块
[Serializable]
   public class DataModel {
        public string Name { get; set; }
        public float Num { get; set; }
    }


public class Step
{
    public decimal Start { get; set; }
    public decimal End { get; set; }
}

public class HashPoint
{
    public decimal Start { get; set; }
    public decimal End { get; set; }
}
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }



        public string BaseCount()
        {
            int[] BaseCountArr = { 0, 0, 0,0,0,0 };

            Task<int>[] Tasklist = new Task<int>[BaseCountArr.Length];
            Func<object, int> getnum = (object Index) =>
            {
                int index = (int)Index;
                int ResultCount = 0;
                string strSql = "";
                if (index == 0)
                {
                    strSql = @"SELECT count(DISTINCT(community)) AS communitynum FROM HouseInfo;";
                }
                else if (index == 1)
                {
                    strSql = @"select COUNT(1) AS housenum FROM HouseInfo;";
                }
                else if (index == 2)
                {
                    strSql = @"SELECT count(DISTINCT(area)) AS areanum FROM HouseInfo;";
                }
                else if (index==3)
                {
                    strSql = @"SELECT max(price) AS areanum FROM HouseInfo;";
                }
                else if (index == 4)
                {
                    strSql = @"SELECT max(PriceCount) AS areanum FROM HouseInfo;";
                }
                else if (index == 5)
                {
                    strSql = @"SELECT max(cast(HouseArea as DECIMAL)) AS areanum FROM HouseInfo;";
                }
                DataTable dt = Repository<HouseInfo>.Query(strSql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ResultCount = int.Parse(dt.Rows[0][0].ToString());
                }
                return ResultCount;
            };
            for (int i = 0; i < BaseCountArr.Length; i++)
            {
                Tasklist[i] = Task.Factory.StartNew(getnum, i);
            }
            Task.WaitAll(Tasklist);
            for (int i = 0; i < Tasklist.Length; i++)
            {
                BaseCountArr[i] = Tasklist[i].Result;
            }
            string Result = string.Join(",", BaseCountArr);
            return Result;
        }
    
        public JsonResult AreaHouseCount() {
            DataTable dt = Repository<HouseInfo>.Query(@"SELECT COUNT(area) AS num, area FROM HouseInfo GROUP BY area 
ORDER BY num DESC;");
            List<DataModel> list = dt.ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        
        }

        /// <summary>
        /// 总价格占比
        /// </summary>
        /// <returns></returns>
        public JsonResult PriceCountPie(int step = 10)
        {
            DataTable dt = Repository<HouseInfo>.Query(@"SELECT MIN(PriceCount) AS minarea,MAX(PriceCount) AS maxarea  FROM HouseInfo;");
            List<DataModel> list = new List<DataModel>();
            if (dt != null)
            {
                decimal start = decimal.Parse(dt.Rows[0]["minarea"].ToString());
                decimal end = decimal.Parse(dt.Rows[0]["maxarea"].ToString());
                Step[] steparr = GetTitleByBlcok(start, end, step);
                Task<int>[] tasklist = new Task<int>[step];
                Func<object, int> func = (p_step) =>
                {
                    Step s = (Step)p_step;
                    int result = 0;
                    DataTable dtnum = Repository<HouseInfo>.Query(string.Format(@"SELECT count(1) as num FROM HouseInfo WHERE PriceCount >{0} AND PriceCount <={1};", s.Start, s.End));
                    if (dtnum != null && dtnum.Rows.Count > 0)
                    {
                        result = int.Parse(dtnum.Rows[0]["num"].ToString());
                    }
                    else
                    {
                        result = 0;
                    }
                    return result;
                };

                for (int i = 0; i < steparr.Length; i++)
                {
                    tasklist[i] = Task.Factory.StartNew(func, steparr[i]);
                }
                Task.WaitAll(tasklist);
                for (int i = 0; i < step; i++)
                {
                    list.Add(new DataModel()
                    {
                        Name = steparr[i].Start + "-" + steparr[i].End,
                        Num = tasklist[i].Result,
                    });
                }

            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 面积占比
        /// </summary>
        /// <returns></returns>
        public JsonResult AreaPie(int step=10) {
            DataTable dt = Repository<HouseInfo>.Query(@"SELECT MIN(cast(HouseArea as decimal)) AS minarea,MAX(cast(HouseArea as decimal)) AS maxarea  FROM HouseInfo;");
            List<DataModel> list = new List<DataModel>();
            if (dt!=null)
            {
                decimal start = decimal.Parse(dt.Rows[0]["minarea"].ToString());
                decimal end = decimal.Parse(dt.Rows[0]["maxarea"].ToString());
                Step[] steparr = GetTitleByBlcok(start, end, step);
                Task<int>[] tasklist=new Task<int>[step];
                Func<object, int> func = (p_step) => {
                    Step s = (Step)p_step;
                    int result = 0;
                    DataTable dtnum = Repository<HouseInfo>.Query(string.Format(@"SELECT count(1) as num FROM HouseInfo WHERE cast(HouseArea as decimal) >{0} AND cast(HouseArea as decimal) <={1};",s.Start,s.End));
                    if (dtnum != null && dtnum.Rows.Count > 0)
                    {
                        result = int.Parse(dtnum.Rows[0]["num"].ToString());
                    }
                    else {
                        result = 0;
                    }
                    return result;
                };

                for (int i = 0; i < steparr.Length; i++)
                {
                    tasklist[i] = Task.Factory.StartNew(func, steparr[i]);
                }
                Task.WaitAll(tasklist);
                for (int i = 0; i < step; i++)
                {
                    list.Add(new DataModel() { 
                    Name=steparr[i].Start+"-"+steparr[i].End,
                    Num=tasklist[i].Result,
                    });
                }

            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 单价占比
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        public JsonResult PricePie(int step = 10)
        {
            DataTable dt = Repository<HouseInfo>.Query(@"SELECT MIN(Price) AS minarea,MAX(Price) AS maxarea  FROM HouseInfo;");
            List<DataModel> list = new List<DataModel>();
            if (dt != null)
            {
                decimal start = decimal.Parse(dt.Rows[0]["minarea"].ToString());
                decimal end = decimal.Parse(dt.Rows[0]["maxarea"].ToString());
                Step[] steparr = GetTitleByBlcok(start, end, step);
                Task<int>[] tasklist = new Task<int>[step];
                Func<object, int> func = (p_step) =>
                {
                    Step s = (Step)p_step;
                    int result = 0;
                    DataTable dtnum = Repository<HouseInfo>.Query(string.Format(@"SELECT count(1) as num FROM HouseInfo WHERE Price >{0} AND Price <={1};", s.Start, s.End));
                    if (dtnum != null && dtnum.Rows.Count > 0)
                    {
                        result = int.Parse(dtnum.Rows[0]["num"].ToString());
                    }
                    else
                    {
                        result = 0;
                    }
                    return result;
                };

                for (int i = 0; i < steparr.Length; i++)
                {
                    tasklist[i] = Task.Factory.StartNew(func, steparr[i]);
                }
                Task.WaitAll(tasklist);
                for (int i = 0; i < step; i++)
                {
                    list.Add(new DataModel()
                    {
                        Name = steparr[i].Start + "-" + steparr[i].End,
                        Num = tasklist[i].Result,
                    });
                }

            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// 根据最大数，最小数，数量间距获取标题
        /// </summary>
        /// <param name="start">最小数</param>
        /// <param name="end">最大数</param>
        /// <param name="block">分为几块</param>
        /// <returns></returns>
        public Step[] GetTitleByBlcok(decimal start = 0, decimal end=99999, int block = 10)
        {
            Step[] steparr = new Step[block];
            decimal step = (end - start) / 10;
            string[] result = new string[block];
            decimal temptitle = 0;
            for (int i = 0; i < block; i++)
            {
                steparr[i] = new Step()
                {
                    Start = start + step * i,
                    End = start + step * (i + 1)
                };
                
            }
            return steparr;
        }


        /// <summary>
        /// 查找排名前几位的小区销售数量
        /// </summary>
        /// <param name="Order">order>0 :查询排名前order位数据，：查询所有，desc:0:正序，1 倒叙</param>
        /// <returns></returns>
        public JsonResult CommunityHouseCount(int Order=15,int desc=0)
        {
            string descby = desc == 0 ? "desc" : "ASC";
            string Topby = Order > 0 ? " limit 0, " + Order : "";
            List<DataModel> list = Repository<HouseInfo>.Query(string.Format(@"SELECT  COUNT(community) AS num, community as area FROM HouseInfo GROUP BY community 
ORDER BY num {1} {0};", Topby, descby)).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        public JsonResult PriceArea()
        {
            List<HashPoint> list = Repository<HouseInfo>.Query(@"SELECT Price,cast(HouseArea as decimal) AS HouseArea FROM HouseInfo;").ToList1();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PriceCountArea()
        {
            List<HashPoint> list = Repository<HouseInfo>.Query(@"SELECT PriceCount as Price,cast(HouseArea as decimal) AS HouseArea FROM HouseInfo;").ToList1();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PriceCountPrice()
        {
            List<HashPoint> list = Repository<HouseInfo>.Query(@"SELECT Price,PriceCount AS HouseArea  FROM HouseInfo;").ToList1();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }

    public static class DataTool {
        public static List<DataModel> ToList(this DataTable dt)
        {
            List<DataModel> list = new List<DataModel>();
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    list.Add(new DataModel()
                    {
                        Name = item["area"].ToString(),
                        Num = int.Parse(item["num"].ToString())
                    });
                }
            }
            return list;
        }
        public static List<HashPoint> ToList1(this DataTable dt)
        {
            List<HashPoint> list = new List<HashPoint>();
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    list.Add(new HashPoint()
                    {
                        Start = decimal.Parse(item["Price"].ToString()),
                        End = decimal.Parse(item["HouseArea"].ToString())
                    });
                }
            }
            return list;
        }
    }
}
