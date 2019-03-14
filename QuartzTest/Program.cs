using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using Quartz.Xml;
using Quartz.Simpl;
using System.IO;
using QuartzTest.DbHelp;
using QuartzTest.WebHelp;
namespace QuartzTest
{
    class Program
    {
        static void Main(string[] args)
        {

           // IScheduler scheduler;
           // scheduler = StdSchedulerFactory.GetDefaultScheduler();;
          //  scheduler.Start();
            //IJobDetail job = JobBuilder.Create<TestJob>().WithIdentity("job1", "group1").Build();
            //ITrigger trigger = TriggerBuilder.Create()
            //.WithIdentity("trigger1", "group1")
            //.WithCronSchedule("0/5 * * * * ?")     //5秒执行一次
            //    //.StartAt(runTime)
            //.Build();
            ////4、将任务与触发器添加到调度器中
            //scheduler.ScheduleJob(job, trigger);
            ////5、开始执行
          //  XMLSchedulingDataProcessor processor = new XMLSchedulingDataProcessor(new SimpleTypeLoadHelper());
           // Stream s = new StreamReader("quartz_jobs.xml").BaseStream;
          //  processor.ProcessStream(s, null);
          //  processor.ScheduleJobs(scheduler);
            Console.WriteLine("123");

           // new DbHelper().insert();
           // new WebHelper().getRandom();

            //DateTime dt = DateTime.Now;
           /// Console.WriteLine(dt.ToString("yyyy-MM-dd HH:mm:ss"));
            Console.WriteLine(WebHelper.Post(WebHelper.testUrl, "").ToString());

           
        }
    }
}
