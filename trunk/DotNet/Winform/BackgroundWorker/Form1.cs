using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackgroundWorker1
{
    public partial class Form1 : Form
    {
        private System.ComponentModel.BackgroundWorker _demoBGWorker = new System.ComponentModel.BackgroundWorker();
        public Form1()
        {
            InitializeComponent();
            _demoBGWorker.WorkerReportsProgress=true;
            _demoBGWorker.DoWork += BackgroundWorker1_DoWork;
            _demoBGWorker.ProgressChanged += _demoBGWorker_ProgressChanged;
            progressBar1.Maximum = 100;
            _demoBGWorker.RunWorkerAsync(100);
        }

        private void _demoBGWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            string message = e.UserState.ToString();
            label1.Text = message;
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            
            int endNumber = 0;
            if (e.Argument != null)
            {
                endNumber = (int)e.Argument;
            }

            int sum = 0;
            for (int i = 0; i <= endNumber; i++)
            {
                sum += i;

                string message = "Current sum is: " + sum.ToString();
                //ReportProgress 方法把信息传递给 ProcessChanged 事件处理函数。
                //第一个参数类型为 int，表示执行进度。
                //如果有更多的信息需要传递，可以使用 ReportProgress 的第二个参数。
                //这里我们给第二个参数传进去一条消息。
                _demoBGWorker.ReportProgress(i, message);
                Thread.Sleep(600);
            }
        }
    }
}
