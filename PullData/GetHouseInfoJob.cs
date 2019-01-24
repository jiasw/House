using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotnetSpider.Core;
using DotnetSpider.Core.Pipeline;
using DotnetSpider.Core.Processor;
using DotnetSpider.Core.Scheduler;
using DotnetSpider.Core.Selector;
using GetHouseInfo.DataBase.Model;
using LearnElasticsearch.DataBase.DAL;
using Quartz;

namespace PullData
{
    public class GetHouseInfoJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Factory.StartNew(() => {
                LogNetHelper.Info("<<<<<<<<<<执行获取数据服务>>>>>>>>>>");
                Repository<HouseInfo>.Delete(n => n.ID>0);//先清空数据表数据
            });

              
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

    }
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
     
}
