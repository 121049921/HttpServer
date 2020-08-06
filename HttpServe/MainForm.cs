using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

using HttpMvc;
using System.Threading;
using Uitl;
using System.Net.Http;
using HttpApiMethod;
using System.IO;

namespace HttpServe
{


    public delegate void ConfigHandler(string configJson);

    /// <summary>
    ///
    /// </summary>
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private bool isStartFlag = false;


        private HttpListener httpListener;
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.tabPage1.Text = "启动日志";
            this.tabPage2.Text = "下载日志";
            this.tabPage3.Text = "请求日志";

        }


        private void btnStart_Click(object sender, EventArgs e)
        {
            AppRuntimes.Instance.ApplicattionStart();

            //1.起启http服务
            httpListener = new System.Net.HttpListener();
            httpListener.Prefixes.Add(AppRuntimes.Instance.Url);
            if (httpListener != null && !isStartFlag)
            {
                httpListener.Start();
                lblServerState.BackColor = Color.Red;
                lblServerState.Text = "服务已启动";
                Log.Instance.Info("httpListener服务已启动");
                this.txtURL.Text = AppRuntimes.Instance.Url;
                isStartFlag = true;
                Thread t = new Thread(new ThreadStart(ProcessRequest));
                t.Start();
            }
            string msg = AppRuntimes.Instance.ReadLog();
            this.textBox1.Text = string.Empty;
            this.textBox1.AppendText(msg);

        }

        private void btnStop_Click(object sender, EventArgs e)
        {

            if (httpListener != null && isStartFlag)//&&!httpListener.IsListening
            {
                httpListener.Stop();
                lblServerState.BackColor = Color.Red;
                lblServerState.Text = "服务已停止";
                Log.Instance.Info("服务已启动");
                isStartFlag = false;
            }

        }
        private void ProcessRequest()
        {
            MvcHandler mvcHandler = null;
            while (true)
            {
                try
                {
                    //1.请求
                    HttpListenerContext context = httpListener.GetContext();
                    string rawRurl = context.Request.RawUrl;
                    if (!rawRurl.Contains("/favicon.ico"))
                    {
                        if (IsMvcHandlerFlag(rawRurl))
                        {
                            mvcHandler = (MvcHandler)AppRuntimes.Instance.Container.Resolve<MvcHandler>();


                          
                            mvcHandler.ProcessReqeust(context, AppRuntimes.Instance.RootPath);
                            //2.响应结果
                            mvcHandler.ProcessResponse();
                        }
                        else
                        {
                            OtherHandler otherHandler = (OtherHandler)AppRuntimes.Instance.Container.Resolve<OtherHandler>();
                        }
                    }
                }
                catch (HttpListenerException ex)
                {


                }
                catch (Exception ex)
                {


                }
                finally
                {

                }
            }
        }
        private bool IsMvcHandlerFlag(string rawRurl)
        {
           
            List<string> urlPostfixList = new List<string>() { "aspx,asp,ashx" };
            var flag = true;
            foreach (var urlPostfix in urlPostfixList)
            {
                if (rawRurl.Contains(urlPostfix))
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }

        private void btnTestLog_Click(object sender, EventArgs e)
        {
            Panl();
        }
        private string Panl()
        {

            for (int i = 0; i < 100; i++)
            {
                Task.Run(() =>
                {
                    Log.Instance.Info(string.Format("线程：{0},时间:{1}----{2}", Thread.CurrentThread.ManagedThreadId, DateTime.Now, "-----------"));
                });
            }
            Task.WaitAll();

            return "测试完毕";
        }

        private void btnTestRequest_Click(object sender, EventArgs e)
        {
            string result = string.Empty;
            string uri = AppRuntimes.Instance.Url + "Test/GetListText1?" + "parma1=" + 1 + "&parma2=" + 2;
            var users = new List<User>()
                   {
                    new User(){ Phone="13800138000",Name="xialei" },
                    new User(){ Phone="13800138000",Name="zhangsanfeng" },
                   };
            string requestBody = JsonUitl.ToJson(users);
            result = HttpRequest.Instance.PostRequestComplex(uri, requestBody);


            uri = AppRuntimes.Instance.Url + "Test/GetZip";
            string dirPath = Path.Combine(AppRuntimes.Instance.RootPath, "Image");
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            var fileFullPath = Path.Combine(dirPath, "PadImage.zip");
            if (!File.Exists(fileFullPath))
            {
                File.Create(fileFullPath).Close();
            }
            var downFlag = HttpRequest.Instance.DownloadFile(uri, fileFullPath);

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ConfigForm configForm = new ConfigForm();
            configForm.ConfigHandler =delegate (string json) { this.textBox2.Text = json; };
            configForm.Show();
        }
    }
}
