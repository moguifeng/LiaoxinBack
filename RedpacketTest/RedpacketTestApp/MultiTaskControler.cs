using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;

namespace RedpacketTestApp
{

    public class TestTaskModel : IThreadTask
    {
        public int Index { get; set; }

        public string ErrorMsg { get; set; }

        public ThreadTaskStatus ThreadTaskStatus { get; set; }

        public string Token { get; set; }

        public string Url { get; set; }

        public string RedpacketId { get; set; }

        public string ClientId { get; set; }

       

        public void Run()
        {
            ThreadTaskStatus = ThreadTaskStatus.Running;
            try
            {

                Encoding requestCoding = Encoding.UTF8;

                Encoding getCoding = Encoding.UTF8;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "POST";
                if (requestCoding == Encoding.UTF8)
                    request.ContentType = "application/json; charset=utf-8";
                else
                    request.ContentType = "application/json; charset=gb2312";
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.1.4322)";
                byte[] bs = requestCoding.GetBytes($"{{\"RedPacketId\":\"{RedpacketId}\",\"ClientId\":\"{ClientId}\"}}");
                request.ContentLength = bs.Length;

                SetHeaderValue(request.Headers, "token", Token);
                using (Stream myRequestStream = request.GetRequestStream())
                {
                    //using (StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312")))
                    //{
                    //myStreamWriter.Write(postDataStr);

                    myRequestStream.Write(bs, 0, bs.Length);

                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    //if (cookie != null)
                    //{
                    //    response.Cookies = cookie.GetCookies(response.ResponseUri);
                    //}
                    using (Stream myResponseStream = response.GetResponseStream())
                    {
                        using (StreamReader myStreamReader = new StreamReader(myResponseStream, getCoding))
                        {
                            string retString = myStreamReader.ReadToEnd();
                            ErrorMsg = retString;
                        }
                    }
                    // }
                }
                ThreadTaskStatus = ThreadTaskStatus.Finish;
            }
            catch(Exception ex)
            {
                ThreadTaskStatus = ThreadTaskStatus.Error;
            }
        }

        public void RollBack()
        {

        }

        private void SetHeaderValue(WebHeaderCollection header, string name, string value)
        {
            var property = typeof(WebHeaderCollection).GetProperty("InnerCollection", BindingFlags.Instance | BindingFlags.NonPublic);
            if (property != null)
            {
                var collection = property.GetValue(header, null) as NameValueCollection;
                collection[name] = value;
            }
        }
    }

    public class MultiTaskControler
    {

        public MultiTaskControler()
        {
            SleepmilliSeconds = 1000;
            MaxThreadCount = 4;
        }

        /// <summary>
        /// 锁对象,获取任务时避免冲突
        /// </summary>
        private object lockobj = new object();

        /// <summary>
        /// 睡眠毫秒(等待结果时挂机毫秒)
        /// </summary>
        public int SleepmilliSeconds { get; set; }

        /// <summary>
        /// 超时时长(秒)
        /// </summary>
        public int OverSeconds { get; set; }
        /// <summary>
        /// 最大线程数
        /// </summary>
        public int MaxThreadCount { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public bool Status
        {
            get
            {
                if (TestList == null || TestList.Count == 0)
                {
                    return true;
                }
                else
                {
                    return TestList.Count == TestList.Count(p => p.ThreadTaskStatus == ThreadTaskStatus.Finish || p.ThreadTaskStatus == ThreadTaskStatus.Error);
                }
            }
        }

        private double runSeconds = 0;
        /// <summary>
        /// 运行时长
        /// </summary>
        public double RunSeconds { get { return runSeconds; } }
        /// <summary>
        /// 任务列表
        /// </summary>
        public List<IThreadTask> TestList = new List<IThreadTask>();
        /// <summary>
        /// 通过锁获取未开始任务
        /// </summary>
        /// <returns></returns>
        public IThreadTask GetUnStartTask()
        {
            IThreadTask model = null;
            if (TestList != null)
            {
                lock (lockobj)
                {
                    model = TestList.FirstOrDefault(p => p.ThreadTaskStatus == ThreadTaskStatus.Unstarted);
                    if (model != null)
                    {
                        model.ThreadTaskStatus = ThreadTaskStatus.Ready;
                    }
                }
            }
            return model;
        }
        /// <summary>
        /// 多线程执行
        /// </summary>
        public void MultiThreadRun()
        {
            DateTime timebegin = DateTime.Now;
            int maxThreadCount = MaxThreadCount;
            if (maxThreadCount <= 0)
            {
                maxThreadCount = 1;
            }
            List<Thread> threadList = new List<Thread>();
            for (int i = 0; i < maxThreadCount; i++)
            {
                Thread t = new Thread(new ParameterizedThreadStart(ThreadMainWithParameters));
                threadList.Add(t);
            }
            int runCount = 0, waitCount = 0;
            while (!Status)
            {

                if (threadList.Count(p => p.ThreadState != ThreadState.Stopped && p.ThreadState != ThreadState.Unstarted) == maxThreadCount)
                {
                    //waitCount++;
                    Thread.Sleep(SleepmilliSeconds);
                }
                else
                {
                    //runCount++;
                    Thread thread = threadList.FirstOrDefault(p => p.ThreadState == ThreadState.Unstarted);
                    if (thread == null)
                    {
                        thread = threadList.FirstOrDefault(p => p.ThreadState == ThreadState.Stopped);
                        threadList.Remove(thread);
                        thread = null;
                        if (maxThreadCount > threadList.Count)
                        {
                            thread = new Thread(new ParameterizedThreadStart(ThreadMainWithParameters));
                            threadList.Add(thread);
                        }
                    }
                    if (thread != null)
                    {
                        IThreadTask data = GetUnStartTask();
                        if (data != null)
                        {
                            thread.Start(data);
                        }
                        else
                        {
                            maxThreadCount--;
                        }
                    }
                    if (OverSeconds > 0)
                    {
                        //超时处理
                        if ((DateTime.Now - timebegin).Seconds > OverSeconds)
                        {
                            break;
                        }

                    }
                }

            }
            DateTime timeEnd = DateTime.Now;
            runSeconds = (timeEnd - timebegin).TotalSeconds;
        }

        /// <summary>
        /// 单线程执行
        /// </summary>
        public void SingleThreadRun()
        {
            DateTime timebegin = DateTime.Now;

            while (!this.Status)
            {
                IThreadTask model = GetUnStartTask();
                if (model != null)
                {
                    model.Run();
                }
                else
                {
                    //由于任务的执行有可能是异步处理,所以会出现没执行完但仍继续轮询的情况
                    Thread.Sleep(SleepmilliSeconds);
                }
                if (OverSeconds > 0)
                {
                    if ((DateTime.Now - timebegin).Seconds > OverSeconds)
                    {
                        break;
                    }

                }
            }
            DateTime timeEnd = DateTime.Now;
            runSeconds = (timeEnd - timebegin).TotalSeconds;
        }
        /// <summary>
        /// 多线程托管函数
        /// </summary>
        /// <param name="obj"></param>
        void ThreadMainWithParameters(object obj)
        {
            TestTaskModel entity = obj as TestTaskModel;
            try
            {
                entity.Run();
            }
            catch
            {
                entity.ThreadTaskStatus = ThreadTaskStatus.Error;
            }
        }

    }

    public interface IThreadTask
    {
        int Index { get; set; }

        ThreadTaskStatus ThreadTaskStatus { get; set; }

        string ErrorMsg { get; set; }

        void Run();

        void RollBack();
    }

    public enum ThreadTaskStatus
    {
        Unstarted = 0,
        Ready = 2,
        Running = 3,
        Finish = 4,
        Error = 5
    }
}
