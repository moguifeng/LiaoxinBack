using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Xml.Linq;
using System.Threading;
using OfficeOpenXml;
using System.Drawing.Printing;
using QRCoder;
using System.Drawing.Drawing2D;
using RedpacketTestApp.SQLHelper;


namespace RedpacketTestApp
{
    public partial class Form1 : FormBase
    {

        private static string ConfigPath = FilePathHelper.GetPath(ConfigurationManager.AppSettings["DefaultValuePath"] ?? "~/ConfigFiles/DefaultValue.xml");

        private static string LabelTemplatePath = FilePathHelper.GetPath(ConfigurationManager.AppSettings["LabelTemplatePath"] ?? "~/Images/LabelTemplate.bmp");

        private static string DefaultPrinter = string.Empty;

        /// <summary>
        /// 下偏移
        /// </summary>
        private int ImageLeftMargin = 5;
        /// <summary>
        /// 上偏移
        /// </summary>
        private int ImageTopMargin = 5;

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            string token = txtToken.Text;
            string url = txtWebAPIUrl.Text;
            string redpacketId = txtRedPacketId.Text;
            int threadCount = (int)numMaxThreadCount.Value;
            string clientIds = txtClientIds.Text;
            this.DoWorkAsync((o) =>
            {
                List<string> clientIdList = new List<string>();
                List<IThreadTask> taskModelList = new List<IThreadTask>();
                int taskIndex = 1;
                if (string.IsNullOrEmpty(clientIds))
                {
                    ISQLHelper sqlHelper = SQLProvider.CreateSQLHelper();
                    DataTable dt = sqlHelper.ExecuteDataTable($"SELECT ClientId FROM groupclients WHERE groupid IN (SELECT groupid FROM redpackets WHERE RedPacketId='{redpacketId}')");
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        return "没数据";
                    }

                    foreach (DataRow dr in dt.Rows)
                    {
                        clientIdList.Add( dr["ClientId"] + "");
                    }
                }
                else
                {
                    clientIdList.AddRange(clientIds.Split(','));
                }

              
                foreach (string clientId in clientIdList)
                {
                    TestTaskModel taskModel = new TestTaskModel();
                    taskModel.Index = taskIndex++;
                    taskModel.Token = token;
                    taskModel.Url = url;
                    taskModel.RedpacketId = redpacketId;
                    taskModel.ClientId = clientId;
                    taskModelList.Add(taskModel);
                }
                MultiTaskControler taskControler = new MultiTaskControler();
                taskControler.MaxThreadCount = threadCount;
                taskControler.SleepmilliSeconds = 500;
                taskControler.TestList = taskModelList;
                taskControler.MultiThreadRun();

                return "";

            }, null, (resultMsg) => //显示结果（此处用于对上面结果的处理，比如显示到界面上）
            {
                if (!string.IsNullOrEmpty(resultMsg+""))
                {
                   MessageBox.Show(resultMsg);
                }
            });

        }


        public string GetTempFileName(string extension)
        {
            string tempFileName = Path.GetTempFileName();
            string newTempFileName = Path.ChangeExtension(tempFileName, extension);
            File.Move(tempFileName, newTempFileName);
            return newTempFileName;
        }

    }


}
