using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 号码预测器
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void 开始同步ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            httpservice httpservice = new httpservice();
            string url = "http://www.17500.cn/ssq/details.php?issue=2003001";

            HttpItem HttpItem = new HttpItem()
            {
                URL = url
            };
            var ss=    httpservice.GetHtml(HttpItem);
            this.textBox1.AppendText(ss.Html);
        }
    }
}
