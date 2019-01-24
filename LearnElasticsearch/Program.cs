using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Dos.ORM;
using DotnetSpider.Core;
using DotnetSpider.Core.Pipeline;
using DotnetSpider.Core.Processor;
using DotnetSpider.Core.Scheduler;
using DotnetSpider.Core.Selector;
using Elasticsearch.Net;
using GetHouseInfo.DataBase.Model;
using LearnElasticsearch.DataBase.DAL;
using LearnElasticsearch.DataModel;
using LearnElasticsearch.Model;
using Nest;

namespace LearnElasticsearch
{
    class Program
    {

        public class Person
        {

            public string FirstName { get; set; }
            public string LastName { get; set; }

            public string MiddleName { get; set; }


        }
        //创建计时器
        private static readonly Stopwatch Watch = new Stopwatch();

        class AutoHomePipe : BasePipeline
        {

            public override void Process(IEnumerable<ResultItems> resultItems, ISpider spider)
            {

                foreach (var resultItem in resultItems)
                {
                    List<HouseInfo> list = resultItem.Results["HouseList"] as List<HouseInfo>;
                    Repository<HouseInfo>.Insert(list);
                    Console.WriteLine("已经存储数据：" + list.Count);
                    //foreach (HouseInfo item in list)
                    //{
                    //    //存储汽车数据
                    //    Console.WriteLine(item);

                    //}
                }
            }


        }

        class AutoHomeProcessor : BasePageProcessor
        {
            protected override void Handle(DotnetSpider.Core.Page page)
            {
                List<HouseInfo> list = new List<HouseInfo>();
                var HouseList = page.Selectable.XPath(".//ul[@class='sellListContent']/li").Nodes();

                foreach (var HouseInfo in HouseList)
                {
                    HouseInfo house = new HouseInfo();

                    house.Title = HouseInfo.XPath("./div[@class='info clear']/div[@class='title']/a").GetValue();
                    house.Community = HouseInfo.XPath("./div[@class='info clear']/div[@class='address']/div[@class='houseInfo']/a").GetValue();
                    house.Url = HouseInfo.XPath("./a[@class='noresultRecommend img ']/@href").GetValue();
                    string Communitystr = HouseInfo.XPath("./div[@class='info clear']/div[@class='address']/div[@class='houseInfo']").GetValue(ValueOption.InnerText);

                    string flood = HouseInfo.XPath("./div[@class='info clear']/div[@class='flood']/div[@class='positionInfo']").GetValue(ValueOption.InnerText);
                    if (!string.IsNullOrEmpty(flood))
                    {
                        string[] ArrCommunity = flood.Split('-');
                        if (ArrCommunity.Length > 0)
                        {
                            //house.Area = ArrCommunity[0];
                            house.Location = ArrCommunity[1];
                        }
                    }
                    string followInfo = HouseInfo.XPath("./div[@class='info clear']/div[@class='followInfo']").GetValue(ValueOption.InnerText);
                    if (!string.IsNullOrEmpty(followInfo))
                    {
                        string[] ArrCommunity = followInfo.Split('/');
                        if (ArrCommunity.Length > 0)
                        {
                            house.WatchCount = int.Parse(ArrCommunity[0].Substring(ArrCommunity[0].IndexOf("人") - 1, 1));
                            house.LeadShowCount = int.Parse(ArrCommunity[1].Substring(ArrCommunity[1].IndexOf("次") - 1, 1));
                        }
                    }

                    
                    house.PriceCount = decimal.Parse(HouseInfo.XPath("./div[@class='info clear']/div[@class='priceInfo']/div[@class='totalPrice']/span").GetValue());
                    house.Price = decimal.Parse(HouseInfo.XPath("./div[@class='info clear']/div[@class='priceInfo']/div[@class='unitPrice']/@data-price").GetValue());



                    list.Add(house);


                }
                page.AddResultItem("HouseList", list);
            }

        }

        class HouseDetailsProcessor : BasePageProcessor
        {

            protected override void Handle(DotnetSpider.Core.Page page)
            {
                HouseInfo Model = new HouseInfo();
                Model.Url = page.Request.Url;
                var HouseInfo = page.Selectable.XPath(".//div[@class='m-content']/div[@class='box-l']").Nodes();
                
                foreach (var item in HouseInfo)
                {
                    //基本信息
                    var BaseInfo = item.XPath(".//*[@id='introduction']/div/div/div[1]/div[2]/ul/li").Nodes();
                    foreach (var baseinfoitem in BaseInfo)
                    {

                        string title = baseinfoitem.XPath(".//span").GetValue(ValueOption.InnerText);
                        if (title == "房屋户型")
                        {
                            //2室2厅1厨1卫
                            string roominfo = baseinfoitem.XPath("./text()").GetValue();
                            if (roominfo.IndexOf("室") > 0)
                            {
                                Model.RoomCount = int.Parse(roominfo.Substring(roominfo.IndexOf("室") - 1, 1));
                            }
                            if (roominfo.IndexOf("厅") > 0)
                            {
                                Model.SaloonCount = int.Parse(roominfo.Substring(roominfo.IndexOf("厅") - 1, 1));
                            }
                            if (roominfo.IndexOf("卫") > 0)
                            {
                                Model.BathRoomCount = int.Parse(roominfo.Substring(roominfo.IndexOf("卫") - 1, 1));
                            }
                        }
                        else
                        {
                            string subitemtext = baseinfoitem.XPath("./text()").GetValue().Trim();
                            if (title == "所在楼层")
                            {
                                Model.Storey = subitemtext;
                            }
                            else if (title == "建筑面积")
                            {
                                Model.HouseArea = subitemtext.Replace("㎡", "");
                            }
                            else if (title == "户型结构")
                            {
                                Model.Housestruct = subitemtext;
                            }
                            else if (title == "套内面积")
                            {
                                Model.HouseInsideArea = subitemtext;
                            }
                            else if (title == "建筑类型")
                            {
                                Model.Building = subitemtext;
                            }
                            else if (title=="房屋朝向")
                            {
                                Model.HouseDirection = subitemtext;
                            }
                            else if (title == "建筑结构")
                            {
                                Model.BuildingStruct = subitemtext;
                            }
                            else if (title == "装修情况")
                            {
                                Model.Ornamant = subitemtext;
                            }
                            else if (title == "梯户比例")
                            {
                                Model.LiftScale = subitemtext;
                            }
                            else if (title == "供暖方式")
                            {
                                Model.WramStyle = subitemtext;
                            }
                            else if (title == "配备电梯")
                            {
                                Model.Lift = subitemtext;
                            }
                            else if (title == "产权年限")
                            {
                                Model.PropertyTime =subitemtext.Replace("年","");
                            }
                        }

                    }

                    var transactionInfo = item.XPath(".//*[@id='introduction']/div/div/div[2]/div[2]/ul/li").Nodes();
                    foreach (var tranitem in transactionInfo)
                    {
                        string title = tranitem.XPath(".//span").GetValue(ValueOption.InnerText);
                        string value = tranitem.XPath("./span[2]").GetValue(ValueOption.InnerText).Trim();
                        if (title=="挂牌时间")
                        {
                            Model.HangOutTime =value;
                        }
                        else if (title == "交易权属")
                        {
                            Model.Ownership = value;
                        }
                        else if (title == "上次交易")
                        {
                            Model.LastSale =value;
                        }
                        else if (title == "房屋用途")
                        {
                            Model.HouseUseto = value;
                        }
                        else if (title == "房屋年限")
                        {
                            Model.HouseTime = value;
                        }
                        else if (title == "产权所属")
                        {
                            Model.Owne = value;
                        }
                        else if (title == "抵押信息")
                        {
                            Model.Mortgage = value;
                        }
                        else if (title == "房本备件")
                        {
                            Model.HouseLicence = value;
                        }
                    }

                    
                    #region 房源特色
                    //var house = item.XPath(".//div[1]/div[@class='introContent showbasemore']/div[@class='baseattribute clear']").Nodes();
                    //foreach (var houseitem in house)
                    //{
                    //    string title = houseitem.XPath(".//div[@class='name']").GetValue();
                    //    string value = houseitem.XPath("../div[@class='content']").GetValue();
                    //    if (title == "售房详情")
                    //    {
                    //        Model.HouseDetails = value;
                    //    }
                    //    else if (title == "税费解析")
                    //    {
                    //        Model.TaxDetails = value;
                    //    }
                    //    else if (title == "权属抵押")
                    //    {
                    //        Model.MortgageDetails = value;
                    //    }
                    //    else if (title == "装修描述")
                    //    {
                    //        Model.OrnamantDetails = value;
                    //    }
                    //    else if (title == "核心卖点")
                    //    {
                    //        Model.SalePoint = value;
                    //    }
                    //}
                    
                    #endregion
                }

                Model.Area = page.Selectable.XPath(".//div[@class='aroundInfo']/div[@class='areaName']/span[@class='info']/a/text()").GetValue();

                page.AddResultItem("HouseInfo", Model);
            }
        }



        class HouseDetailsPipe : BasePipeline
        {
            public override void Process(IEnumerable<ResultItems> resultItems, ISpider spider)
            {
                foreach (var resultItem in resultItems)
                {
                    HouseInfo item = resultItem.Results["HouseInfo"] as HouseInfo;

                    Repository<HouseInfo>.Update(item, t => t.Url == item.Url);


                    Console.WriteLine("已经更新数据：" + item.Url);
                }
            }
        }
        /// <summary>
        /// 获取房屋主要信息
        /// </summary>
        private static void GetHouseMainInfo()
        {

            var site = new Site
            {
                CycleRetryTimes = 1,
                SleepTime = 200,
                Headers = new Dictionary<string, string>()
                {
                    { "Accept","text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8" },
                    { "Referer", "https://zz.lianjia.com/ershoufang/ng1nb1mw1f2/"},
                    { "Cache-Control","max-age=0" },
                    { "Connection","keep-alive" },
                    { "Content-Type","application/x-www-form-urlencoded; charset=UTF-8" },
                    { "User-Agent","Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36"}



                },


            };

            List<Request> resList = new List<Request>();
            for (int i = 1; i <= 100; i++)
            {
                Request res = new Request();
                //res.PostBody = string.Format("id=7&j=%7B%22createMan%22%3A%2218273159100%22%2C%22createTime%22%3A1518433690000%2C%22row%22%3A5%2C%22siteUserActivityListId%22%3A8553%2C%22siteUserPageRowModuleId%22%3A84959%2C%22topids%22%3A%22%22%2C%22wherePhase%22%3A%221%22%2C%22wherePreferential%22%3A%220%22%2C%22whereUsertype%22%3A%220%22%7D&page={0}&shopid=83106681", i);
                res.Url = string.Format("https://zz.lianjia.com/ershoufang/pg{0}/", i);
                res.Method = System.Net.Http.HttpMethod.Get;

                resList.Add(res);
            }

            var spider = Spider.Create(site, new QueueDuplicateRemovedScheduler(), new AutoHomeProcessor())
                .AddStartRequests(resList.ToArray())

                .AddPipeline(new AutoHomePipe());
            spider.ThreadNum = 15;

            spider.Run();
        }


        private static void GetHouseDetailsInfo()
        {
            var site = new Site
            {
                CycleRetryTimes = 1,
                SleepTime = 20,
                Headers = new Dictionary<string, string>()
                {
                    { "Accept","text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8" },
                    { "Referer", "https://zz.lianjia.com/ershoufang/ng1nb1mw1f2/"},
                    { "Cache-Control","max-age=0" },
                    { "Connection","keep-alive" },
                    { "Content-Type","application/x-www-form-urlencoded; charset=UTF-8" },
                    { "User-Agent","Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36"}



                },


            };

            List<Request> resList = new List<Request>();
            List<HouseInfo> houseList = Repository<HouseInfo>.GetAll();
            foreach (HouseInfo item in houseList)
            {
                Request res = new Request();
                //res.PostBody = string.Format("id=7&j=%7B%22createMan%22%3A%2218273159100%22%2C%22createTime%22%3A1518433690000%2C%22row%22%3A5%2C%22siteUserActivityListId%22%3A8553%2C%22siteUserPageRowModuleId%22%3A84959%2C%22topids%22%3A%22%22%2C%22wherePhase%22%3A%221%22%2C%22wherePreferential%22%3A%220%22%2C%22whereUsertype%22%3A%220%22%7D&page={0}&shopid=83106681", i);
                res.Url = item.Url;
                res.Method = System.Net.Http.HttpMethod.Get;

                resList.Add(res);
               
                
            }


            var spider = Spider.Create(site, new QueueDuplicateRemovedScheduler(), new HouseDetailsProcessor())
                .AddStartRequests(resList.ToArray())

                .AddPipeline(new HouseDetailsPipe());
            spider.ThreadNum = 20;

            spider.Run();
        }

        private static void GetHouseNull() {
           
            var site = new Site
            {
                CycleRetryTimes = 1,
                SleepTime = 200,
                Headers = new Dictionary<string, string>()
                {
                    { "Accept","text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8" },
                    { "Referer", "https://zz.lianjia.com/ershoufang/ng1nb1mw1f2/"},
                    { "Cache-Control","max-age=0" },
                    { "Connection","keep-alive" },
                    { "Content-Type","application/x-www-form-urlencoded; charset=UTF-8" },
                    { "User-Agent","Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36"}



                },
                
            };

            List<Request> resList = new List<Request>();
            List<HouseInfo> houseList = Repository<HouseInfo>.Query(n=>n.HouseInsideArea==null);
            foreach (HouseInfo item in houseList)
            {
                Request res = new Request();
                //res.PostBody = string.Format("id=7&j=%7B%22createMan%22%3A%2218273159100%22%2C%22createTime%22%3A1518433690000%2C%22row%22%3A5%2C%22siteUserActivityListId%22%3A8553%2C%22siteUserPageRowModuleId%22%3A84959%2C%22topids%22%3A%22%22%2C%22wherePhase%22%3A%221%22%2C%22wherePreferential%22%3A%220%22%2C%22whereUsertype%22%3A%220%22%7D&page={0}&shopid=83106681", i);
                res.Url = item.Url;
                res.Method = System.Net.Http.HttpMethod.Get;
                
                resList.Add(res);


            }


            var spider = Spider.Create(site, new QueueDuplicateRemovedScheduler(), new HouseDetailsProcessor())
                .AddStartRequests(resList.ToArray())
                
                .AddPipeline(new HouseDetailsPipe());
            spider.ThreadNum = 10;

            spider.Run();
        }

        static void Main(string[] args)
        {
            
            GetHouseNull();
           
        }
    }
}
