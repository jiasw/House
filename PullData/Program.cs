using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using Topshelf;

namespace PullData
{

    class Program
    {


        static void Main(string[] args)
        {

            HostFactory.Run(x =>                                 //1
            {
                x.Service<LianjiaData>(s =>                        //2
                {
                    s.ConstructUsing(name => new LianjiaData());     //3
                    s.WhenStarted(tc => tc.Start());              //4
                    s.WhenStopped(tc => tc.Stop());               //5
                });
                x.RunAsLocalSystem();                            //6

                x.SetDescription("定时获取链家网在郑州地区的二手房价格");        //7
                x.SetDisplayName("Houseanalyse");                       //8
                x.SetServiceName("Houseanalyse");                       //9
            });

            LogNetHelper.Info("Service Start");

        }

    }

    public class LianjiaData
    {
        NameValueCollection props = null;
        static StdSchedulerFactory factory = null;
        IScheduler scheduler = null;
        static string StrCron = "";
        static LianjiaData() {
            StrCron = ConfigurationManager.AppSettings["TaskCronstr"];
            // Grab the Scheduler instance from the Factory
            NameValueCollection props = new NameValueCollection
                {
                    { "quartz.serializer.type", "binary" }
                };
            factory = new StdSchedulerFactory(props);
        }

        public async void Start() {
            
            
            scheduler = await factory.GetScheduler();

            // and start it off
            await scheduler.Start();

            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<GetHouseInfoJob>()
                .WithIdentity("job1", "group1")
                .Build();

            // Trigger the job to run now, and then repeat every 10 seconds
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .WithCronSchedule(StrCron)
                .Build();

            // Tell quartz to schedule the job using our trigger
            await scheduler.ScheduleJob(job, trigger);
 
        }
        public async void Stop() {
            // and last shut down the scheduler when you are ready to close your program
            if (scheduler!=null)
            {
                await scheduler.Shutdown();
            }
            
        }



    }
}
