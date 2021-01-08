using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;

namespace Forward.Client
{
    class accessweb
    {
        public string WEBURL;
        private static BackgroundWorker bgWorker = new BackgroundWorker();
        public  void InitializeBackgroundWorker()
        {
            bgWorker.WorkerReportsProgress = true;
            bgWorker.WorkerSupportsCancellation = true;
            bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
            bgWorker.ProgressChanged += new ProgressChangedEventHandler(bgWorker_ProgessChanged);
            bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorker_WorkerCompleted);
        }
        public  void BGWorkerStart()
        {
            if (bgWorker.IsBusy)
                return;
            //this.progressBar1.Maximum = 100;
            //this.btnStart.Enabled = false;
            //this.btnStop.Enabled = true;
            bgWorker.RunWorkerAsync("hello");
        }

        public  void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //for (int i = 0; i <= 100; i++)
            //{
            //    if (bgWorker.CancellationPending)
            //    {
            //        e.Cancel = true;
            //        return;
            //    }
            //    else
            //    {
            //        bgWorker.ReportProgress(i, "Working");
            //        System.Threading.Thread.Sleep(10);
            //    }
            //}

            GoTo(WEBURL);


        }
        public  void GoTo(string PageUrl)
        {
            try
            {
                //string PageUrl = "http://www.baidu.com"; //需要获取源代码的网页
                WebRequest request = WebRequest.Create(PageUrl); //WebRequest.Create方法，返回WebRequest的子类HttpWebRequest
                WebResponse response = request.GetResponse(); //WebRequest.GetResponse方法，返回对 Internet 请求的响应
                                                              //Stream resStream = response.GetResponseStream(); //WebResponse.GetResponseStream 方法，从 Internet 资源返回数据流。
                                                              //Encoding enc = Encoding.GetEncoding("GB2312"); // 如果是乱码就改成 utf-8 / GB2312
                                                              //StreamReader sr = new StreamReader(resStream, enc); //命名空间:System.IO。 StreamReader 类实现一个 TextReader (TextReader类，表示可读取连续字符系列的读取器)，使其以一种特定的编码从字节流中读取字符。
                                                              //ContentHtml.Text = sr.ReadToEnd(); //输出(HTML代码)，ContentHtml为Multiline模式的TextBox控件
                                                              //resStream.Close();
                                                              //sr.Close();
            }
            catch (Exception)
            {

            }

        }
        public  void bgWorker_ProgessChanged(object sender, ProgressChangedEventArgs e)
        {
            //string state = (string)e.UserState;//接收ReportProgress方法传递过来的userState
            //this.progressBar1.Value = e.ProgressPercentage;
            //this.label1.Text = "处理进度:" + Convert.ToString(e.ProgressPercentage) + "%";
        }

        public  void bgWorker_WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //if (e.Error != null)
            //{
            //    MessageBox.Show(e.Error.ToString());
            //    return;
            //}
            //if (!e.Cancelled)
            //    this.label1.Text = "处理完毕!";
            //else
            //    this.label1.Text = "处理终止!";
        }

        private  void bgStop()
        {
            bgWorker.CancelAsync();
        }
    }
}
