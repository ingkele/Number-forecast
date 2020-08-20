using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            //string url = "http://www.17500.cn/widget/ssq/surveyload.html?issue=2003001";
            string url = "http://www.17500.cn/widget/ssq/surveyload.html?issue=";
            var db = new Model();
            //计算一年开奖多少次
            var yers = GetYearWeekCount();
            foreach (var item in yers)
            {
                for (var i = 1; i <= item.Value; i++)
                {
                    string cc = i.ToString("D3");

                    HttpItem HttpItem = new HttpItem()
                    {
                        URL = url + item.Key.ToString() + cc
                    };
                    var ss = httpservice.GetHtml(HttpItem);
                    JObject json = (JObject)JsonConvert.DeserializeObject(ss.Html);
                    var str = json["award"].ToString().Split(',');

                    db.T_DrawPrize.Add(new T_DrawPrize()
                    {
                        Lssue = item.Key.ToString() + cc,
                        number1 = int.Parse(str[0]),
                        number2 = int.Parse(str[1]),
                        number3 = int.Parse(str[2]),
                        number4 = int.Parse(str[3]),
                        number5 = int.Parse(str[4]),
                        number6 = int.Parse(str[5]),
                        number7 = int.Parse(str[6]),
                    });
                    this.textBox1.AppendText("正在同步：" + item.Key + " 年,第 " + cc + " 期" + json["award"].ToString() + "\r\n");
                }
                db.SaveChanges();
                ViewBind();
            }
        }

        /// <summary>
        /// 计算一年开奖多少次
        /// </summary>
        /// <param name="strYear"></param>
        /// <returns></returns>
        private Dictionary<int, int> GetYearWeekCount()
        {
            Dictionary<int, int> value = new Dictionary<int, int>();
            value.Add(2003, 89);
            value.Add(2004, 122);
            value.Add(2005, 153);
            value.Add(2006, 154);
            value.Add(2007, 153);
            value.Add(2008, 154);
            value.Add(2009, 154);
            value.Add(2010, 153);
            value.Add(2011, 153);
            value.Add(2012, 154);
            value.Add(2013, 154);
            value.Add(2014, 152);
            value.Add(2015, 154);
            value.Add(2016, 153);
            value.Add(2017, 154);
            value.Add(2018, 153);
            value.Add(2019, 151);
            value.Add(2020, 78);
            return value;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ViewBind();
        }
        private void ViewBind()
        {
            var db = new Model();
            var table = db.T_DrawPrize.Select(x => new
            {
                期数 = x.Lssue,
                号码1 = x.number1,
                号码2 = x.number2,
                号码3 = x.number3,
                号码4 = x.number4,
                号码5 = x.number5,
                号码6 = x.number6,
                号码7 = x.number7,
            }).ToList();
            dataGridView1.DataSource = table;
        }

        private void 统计概率ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var db = new Model();
            int[] item = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33 };
            int[] item1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            float all = db.T_DrawPrize.Count();
            Dictionary<int, float> gailv = new Dictionary<int, float>();
            Dictionary<int, float> gailv1 = new Dictionary<int, float>();
            var str = "";
            foreach (int it in item)
            {
                //计算概率
                float count = db.T_DrawPrize.Count(x => x.number1 == it || x.number2 == it || x.number3 == it || x.number4 == it || x.number5 == it || x.number6 == it);
                float pj = count / all * 100;
                gailv.Add(it, pj);
            }

            foreach (int it in item1)
            {
                //计算概率
                float count = db.T_DrawPrize.Count(x => x.number7 == it);
                float pj = count / all * 100;
                gailv1.Add(it, pj);
            }
            var zuida = gailv.OrderByDescending(x => x.Value);
            var zuida1 = gailv1.OrderByDescending(x => x.Value);
            foreach (var ik in zuida)
            {
                str += "编号 " + ik.Key + " 出现概率为:" + ik.Value + "% \r\n";
            }
            str += " 其中篮球概率为: ";

            foreach (var ik in zuida1)
            {
                str += "篮球 " + ik.Key + " 出现概率为:" + ik.Value + "% \r\n";
            }
            this.textBox2.Text = "";
            this.textBox2.AppendText(str);
            Toyers();
        }
        private void Toyers()
        {
            var date = DateTime.Now.Year.ToString();//今年
            var update = DateTime.Now.AddYears(-1).Year.ToString();//去年
            var db = new Model();
            int[] item = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33 };//红球
            int[] item1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };//篮球

            float all = db.T_DrawPrize.Count(x => x.Lssue.Substring(0, 4) == date);//今年
            float upall = db.T_DrawPrize.Count(x => x.Lssue.Substring(0, 4) == update);//去年

            List<gailvduibi> gailv = new List<gailvduibi>();//红球
            List<gailvduibi> gailv1 = new List<gailvduibi>();//篮球
            var str = "";
            foreach (int it in item)
            {
                //计算概率
                float count = db.T_DrawPrize.Where(x => x.Lssue.Substring(0, 4) == date).Count(x => x.number1 == it || x.number2 == it || x.number3 == it || x.number4 == it || x.number5 == it || x.number6 == it);
                float pj = count / all * 100;

                //计算概率
                float upcount = db.T_DrawPrize.Where(x => x.Lssue.Substring(0, 4) == update).Count(x => x.number1 == it || x.number2 == it || x.number3 == it || x.number4 == it || x.number5 == it || x.number6 == it);
                float uppj = upcount / upall * 100;


                gailv.Add(new gailvduibi()
                {
                    number = it,
                    newyers = pj,
                    upyers = uppj,
                    size= pj > uppj?1:-1
                });
            }//今年

            foreach (int it in item1)
            {
                //计算概率
                float count = db.T_DrawPrize.Where(x => x.Lssue.Substring(0, 4) == date).Count(x => x.number7 == it);
                float pj = count / all * 100;

                //计算概率
                float upcount = db.T_DrawPrize.Where(x => x.Lssue.Substring(0, 4) == update).Count(x => x.number7 == it);
                float uppj = upcount / upall * 100;

                gailv1.Add(new gailvduibi()
                {
                    number = it,
                    newyers = pj,
                    upyers = uppj,
                    size = pj > uppj ? 1 : -1
                });
            }
            var zuida = gailv.OrderByDescending(x => x.newyers);
            var zuida1 = gailv1.OrderByDescending(x => x.newyers);
            foreach (var ik in zuida)
            {
                str += "编号 " + ik.number + " 出现概率为:" + ik.newyers + "% 教去年"+(ik.size==1?"多出"+ (ik.newyers-ik.upyers):"少出" + (ik.upyers - ik.newyers)) +"\r\n";
            }
            str += " 其中篮球概率为: ";

            foreach (var ik in zuida1)
            {
                str += "篮球 " + ik.number + " 出现概率为:" + ik.newyers + "% 教去年" + (ik.size == 1 ? "多出" + (ik.newyers - ik.upyers) : "少出" + (ik.upyers - ik.newyers)) + "\r\n";
            }
            this.textBox3.Text = "";
            this.textBox3.AppendText(str);
        }
    }
}
