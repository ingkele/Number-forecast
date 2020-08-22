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
            MessageBox.Show("已经整体同步过了，现在是自动同步哟！");
            return;
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
            this.timer1.Start();
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
                    size = pj > uppj ? 1 : -1
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
                str += "编号 " + ik.number + " 出现概率为:" + ik.newyers + "% 教去年" + (ik.size == 1 ? "多出" + (ik.newyers - ik.upyers) : "少出" + (ik.upyers - ik.newyers)) + "\r\n";
            }
            str += " 其中篮球概率为: ";

            foreach (var ik in zuida1)
            {
                str += "篮球 " + ik.number + " 出现概率为:" + ik.newyers + "% 教去年" + (ik.size == 1 ? "多出" + (ik.newyers - ik.upyers) : "少出" + (ik.upyers - ik.newyers)) + "\r\n";
            }
            this.textBox3.Text = "";
            this.textBox3.AppendText(str);
        }

        private void 次数统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> year = new List<string>();
            for (var i = 2003; i <= 2020; i++)
            {
                year.Add(i.ToString());
            }
            List<haoma> table = new List<haoma>();

            foreach (var item in year)
            {
                table.Add(new haoma
                {
                    年份 = item,
                    红一 = drawprize(item, 1),
                    红二 = drawprize(item, 2),
                    红三 = drawprize(item, 3),
                    红四 = drawprize(item, 4),
                    红五 = drawprize(item, 5),
                    红六 = drawprize(item, 6),
                    红七 = drawprize(item, 7),
                    红八 = drawprize(item, 8),
                    红九 = drawprize(item, 9),
                    红十 = drawprize(item, 10),
                    红十一 = drawprize(item, 11),
                    红十二 = drawprize(item, 12),
                    红十三 = drawprize(item, 13),
                    红十四 = drawprize(item, 14),
                    红十五 = drawprize(item, 15),
                    红十六 = drawprize(item, 16),
                    红十七 = drawprize(item, 17),
                    红十八 = drawprize(item, 18),
                    红十九 = drawprize(item, 19),
                    红二十 = drawprize(item, 20),
                    红二十一 = drawprize(item, 21),
                    红二十二 = drawprize(item, 22),
                    红二十三 = drawprize(item, 23),
                    红二十四 = drawprize(item, 24),
                    红二十五 = drawprize(item, 25),
                    红二十六 = drawprize(item, 26),
                    红二十七 = drawprize(item, 27),
                    红二十八 = drawprize(item, 28),
                    红二十九 = drawprize(item, 29),
                    红三十 = drawprize(item, 30),
                    红三十一 = drawprize(item, 31),
                    红三十二 = drawprize(item, 32),
                    红三十三 = drawprize(item, 33),
                    红三十四 = drawprize(item, 34),
                });
            }
            dataGridView2.DataSource = table;

            var hong1 = table.Where(x => x.年份 != "2003"&&x.年份!=DateTime.Now.Year.ToString()).Select(x => x.红一).Average(x => x);
            var hong2 = table.Where(x => x.年份 != "2003" && x.年份 != DateTime.Now.Year.ToString()).Select(x => x.红二).Average(x => x);
            var hong3 = table.Where(x => x.年份 != "2003" && x.年份 != DateTime.Now.Year.ToString()).Select(x => x.红三).Average(x => x);
            var hong4 = table.Where(x => x.年份 != "2003" && x.年份 != DateTime.Now.Year.ToString()).Select(x => x.红四).Average(x => x);
            var hong5 = table.Where(x => x.年份 != "2003" && x.年份 != DateTime.Now.Year.ToString()).Select(x => x.红五).Average(x => x);
            var hong6 = table.Where(x => x.年份 != "2003" && x.年份 != DateTime.Now.Year.ToString()).Select(x => x.红六).Average(x => x);
            var hong7 = table.Where(x => x.年份 != "2003" && x.年份 != DateTime.Now.Year.ToString()).Select(x => x.红七).Average(x => x);
            var hong8 = table.Where(x => x.年份 != "2003" && x.年份 != DateTime.Now.Year.ToString()).Select(x => x.红八).Average(x => x);
            var hong9 = table.Where(x => x.年份 != "2003" && x.年份 != DateTime.Now.Year.ToString()).Select(x => x.红九).Average(x => x);
            var hong10 = table.Where(x => x.年份 != "2003" && x.年份 != DateTime.Now.Year.ToString()).Select(x => x.红十).Average(x => x);
            var hong11= table.Where(x => x.年份 != "2003" && x.年份 != DateTime.Now.Year.ToString()).Select(x => x.红十一).Average(x => x);
            var hong12 = table.Where(x => x.年份 != "2003" && x.年份 != DateTime.Now.Year.ToString()).Select(x => x.红十二).Average(x => x);
            var hong13 = table.Where(x => x.年份 != "2003" && x.年份 != DateTime.Now.Year.ToString()).Select(x => x.红十三).Average(x => x);
            var hong14 = table.Where(x => x.年份 != "2003" && x.年份 != DateTime.Now.Year.ToString()).Select(x => x.红十四).Average(x => x);
            var hong15 = table.Where(x => x.年份 != "2003" && x.年份 != DateTime.Now.Year.ToString()).Select(x => x.红十五).Average(x => x);
            var hong16 = table.Where(x => x.年份 != "2003" && x.年份 != DateTime.Now.Year.ToString()).Select(x => x.红十六).Average(x => x);
            var hong17 = table.Where(x => x.年份 != "2003" && x.年份 != DateTime.Now.Year.ToString()).Select(x => x.红十七).Average(x => x);
            var hong18 = table.Where(x => x.年份 != "2003" && x.年份 != DateTime.Now.Year.ToString()).Select(x => x.红十八).Average(x => x);
            var hong19 = table.Where(x => x.年份 != "2003" && x.年份 != DateTime.Now.Year.ToString()).Select(x => x.红十九).Average(x => x);
            var hong20 = table.Where(x => x.年份 != "2003" && x.年份 != DateTime.Now.Year.ToString()).Select(x => x.红二十).Average(x => x);
            var hong21= table.Where(x => x.年份 != "2003" && x.年份 != DateTime.Now.Year.ToString()).Select(x => x.红二十一).Average(x => x);
            var hong22 = table.Where(x => x.年份 != "2003" && x.年份 != DateTime.Now.Year.ToString()).Select(x => x.红二十二).Average(x => x);
            var hong23 = table.Where(x => x.年份 != "2003" && x.年份 != DateTime.Now.Year.ToString()).Select(x => x.红二十三).Average(x => x);
            var hong24 = table.Where(x => x.年份 != "2003" && x.年份 != DateTime.Now.Year.ToString()).Select(x => x.红二十四).Average(x => x);
            var hong25 = table.Where(x => x.年份 != "2003" && x.年份 != DateTime.Now.Year.ToString()).Select(x => x.红二十五).Average(x => x);
            var hong26 = table.Where(x => x.年份 != "2003" && x.年份 != DateTime.Now.Year.ToString()).Select(x => x.红二十六).Average(x => x);
            var hong27 = table.Where(x => x.年份 != "2003" && x.年份 != DateTime.Now.Year.ToString()).Select(x => x.红二十七).Average(x => x);
            var hong28 = table.Where(x => x.年份 != "2003" && x.年份 != DateTime.Now.Year.ToString()).Select(x => x.红二十八).Average(x => x);
            var hong29 = table.Where(x => x.年份 != "2003" && x.年份 != DateTime.Now.Year.ToString()).Select(x => x.红二十九).Average(x => x);
            var hong30 = table.Where(x => x.年份 != "2003" && x.年份 != DateTime.Now.Year.ToString()).Select(x => x.红三十).Average(x => x);
            var hong31 = table.Where(x => x.年份 != "2003" && x.年份 != DateTime.Now.Year.ToString()).Select(x => x.红三十一).Average(x => x);
            var hong32 = table.Where(x => x.年份 != "2003" && x.年份 != DateTime.Now.Year.ToString()).Select(x => x.红三十二).Average(x => x);
            var hong33 = table.Where(x => x.年份 != "2003" && x.年份 != DateTime.Now.Year.ToString()).Select(x => x.红三十三).Average(x => x);
            //计算平均值

            this.textBox1.AppendText("红球1平均出场" + hong1 + " 次，今年出现：" + table.FirstOrDefault(x=>x.年份==DateTime.Now.Year.ToString()).红一+ " 次\r\n");
            this.textBox1.AppendText("红球2平均出场" + hong2 + " 次，今年出现：" + table.FirstOrDefault(x => x.年份 == DateTime.Now.Year.ToString()).红二 + " 次\r\n");
            this.textBox1.AppendText("红球3平均出场" + hong3 + " 次，今年出现：" + table.FirstOrDefault(x => x.年份 == DateTime.Now.Year.ToString()).红三 + " 次\r\n");
            this.textBox1.AppendText("红球4平均出场" + hong4 + " 次，今年出现：" + table.FirstOrDefault(x => x.年份 == DateTime.Now.Year.ToString()).红四 + " 次\r\n");
            this.textBox1.AppendText("红球5平均出场" + hong5 + " 次，今年出现：" + table.FirstOrDefault(x => x.年份 == DateTime.Now.Year.ToString()).红五 + " 次\r\n");
            this.textBox1.AppendText("红球6平均出场" + hong6 + " 次，今年出现：" + table.FirstOrDefault(x => x.年份 == DateTime.Now.Year.ToString()).红六 + " 次\r\n");
            this.textBox1.AppendText("红球7平均出场" + hong7 + " 次，今年出现：" + table.FirstOrDefault(x => x.年份 == DateTime.Now.Year.ToString()).红七 + " 次\r\n");
            this.textBox1.AppendText("红球8平均出场" + hong8 + " 次，今年出现：" + table.FirstOrDefault(x => x.年份 == DateTime.Now.Year.ToString()).红八 + " 次\r\n");
            this.textBox1.AppendText("红球9平均出场" + hong9 + " 次，今年出现：" + table.FirstOrDefault(x => x.年份 == DateTime.Now.Year.ToString()).红九 + " 次\r\n");
            this.textBox1.AppendText("红球10平均出场" + hong10 + " 次，今年出现：" + table.FirstOrDefault(x => x.年份 == DateTime.Now.Year.ToString()).红十 + " 次\r\n");
            this.textBox1.AppendText("红球11平均出场" + hong11 + " 次，今年出现：" + table.FirstOrDefault(x => x.年份 == DateTime.Now.Year.ToString()).红十一 + " 次\r\n");
            this.textBox1.AppendText("红球12平均出场" + hong12 + " 次，今年出现：" + table.FirstOrDefault(x => x.年份 == DateTime.Now.Year.ToString()).红十二 + " 次\r\n");
            this.textBox1.AppendText("红球13平均出场" + hong13 + " 次，今年出现：" + table.FirstOrDefault(x => x.年份 == DateTime.Now.Year.ToString()).红十三 + " 次\r\n");
            this.textBox1.AppendText("红球14平均出场" + hong14 + " 次，今年出现：" + table.FirstOrDefault(x => x.年份 == DateTime.Now.Year.ToString()).红十四 + " 次\r\n");
            this.textBox1.AppendText("红球15平均出场" + hong15 + " 次，今年出现：" + table.FirstOrDefault(x => x.年份 == DateTime.Now.Year.ToString()).红十五 + " 次\r\n");
            this.textBox1.AppendText("红球16平均出场" + hong16 + " 次，今年出现：" + table.FirstOrDefault(x => x.年份 == DateTime.Now.Year.ToString()).红十六 + " 次\r\n");
            this.textBox1.AppendText("红球17平均出场" + hong17 + " 次，今年出现：" + table.FirstOrDefault(x => x.年份 == DateTime.Now.Year.ToString()).红十七 + " 次\r\n");
            this.textBox1.AppendText("红球18平均出场" + hong18 + " 次，今年出现：" + table.FirstOrDefault(x => x.年份 == DateTime.Now.Year.ToString()).红十八 + " 次\r\n");
            this.textBox1.AppendText("红球19平均出场" + hong19 + " 次，今年出现：" + table.FirstOrDefault(x => x.年份 == DateTime.Now.Year.ToString()).红十九 + " 次\r\n");
            this.textBox1.AppendText("红球20平均出场" + hong20 + " 次，今年出现：" + table.FirstOrDefault(x => x.年份 == DateTime.Now.Year.ToString()).红二十 + " 次\r\n");
            this.textBox1.AppendText("红球21平均出场" + hong21 + " 次，今年出现：" + table.FirstOrDefault(x => x.年份 == DateTime.Now.Year.ToString()).红二十一 + " 次\r\n");
            this.textBox1.AppendText("红球22平均出场" + hong22 + " 次，今年出现：" + table.FirstOrDefault(x => x.年份 == DateTime.Now.Year.ToString()).红二十二 + " 次\r\n");
            this.textBox1.AppendText("红球23平均出场" + hong23 + " 次，今年出现：" + table.FirstOrDefault(x => x.年份 == DateTime.Now.Year.ToString()).红二十三 + " 次\r\n");
            this.textBox1.AppendText("红球24平均出场" + hong24 + " 次，今年出现：" + table.FirstOrDefault(x => x.年份 == DateTime.Now.Year.ToString()).红二十四 + " 次\r\n");
            this.textBox1.AppendText("红球25平均出场" + hong25 + " 次，今年出现：" + table.FirstOrDefault(x => x.年份 == DateTime.Now.Year.ToString()).红二十五 + " 次\r\n");
            this.textBox1.AppendText("红球26平均出场" + hong26 + " 次，今年出现：" + table.FirstOrDefault(x => x.年份 == DateTime.Now.Year.ToString()).红二十六 + " 次\r\n");
            this.textBox1.AppendText("红球27平均出场" + hong27 + " 次，今年出现：" + table.FirstOrDefault(x => x.年份 == DateTime.Now.Year.ToString()).红二十七 + " 次\r\n");
            this.textBox1.AppendText("红球28平均出场" + hong28 + " 次，今年出现：" + table.FirstOrDefault(x => x.年份 == DateTime.Now.Year.ToString()).红二十八 + " 次\r\n");
            this.textBox1.AppendText("红球29平均出场" + hong29 + " 次，今年出现：" + table.FirstOrDefault(x => x.年份 == DateTime.Now.Year.ToString()).红二十九 + " 次\r\n");
            this.textBox1.AppendText("红球30平均出场" + hong30 + " 次，今年出现：" + table.FirstOrDefault(x => x.年份 == DateTime.Now.Year.ToString()).红三十 + " 次\r\n");
            this.textBox1.AppendText("红球31平均出场" + hong31 + " 次，今年出现：" + table.FirstOrDefault(x => x.年份 == DateTime.Now.Year.ToString()).红三十一 + " 次\r\n");
            this.textBox1.AppendText("红球32平均出场" + hong32 + " 次，今年出现：" + table.FirstOrDefault(x => x.年份 == DateTime.Now.Year.ToString()).红三十二 + " 次\r\n");
            this.textBox1.AppendText("红球33平均出场" + hong33 + " 次，今年出现：" + table.FirstOrDefault(x => x.年份 == DateTime.Now.Year.ToString()).红三十三 + " 次\r\n");
        }
        private int drawprize(string year, int i)
        {
            var db = new Model();
            return db.T_DrawPrize.Where(x => x.Lssue.Substring(0, 4) == year).Count(x => x.number1 == i || x.number2 == i || x.number3 == i || x.number4 == i || x.number5 == i || x.number6 == i);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var ucc = "";
            httpservice httpservice = new httpservice();
            string url = "http://www.17500.cn/widget/ssq/surveyload.html?issue=";
            var db = new Model();
            //取数据库最新的一条
            var ite = db.T_DrawPrize.OrderByDescending(x => x.Lssue).FirstOrDefault();
            //判断是否今年最后一期
            if (ite.Lssue.Substring(0, 4) == DateTime.Now.Year.ToString())
            {
                ucc = (int.Parse(ite.Lssue) + 1).ToString();
            }
            else
            {
                ucc = DateTime.Now.Year.ToString() + "001";
            }

            HttpItem HttpItem = new HttpItem()
            {
                URL = url + ucc
            };
            var ss = httpservice.GetHtml(HttpItem);

            JObject json = (JObject)JsonConvert.DeserializeObject(ss.Html);
            var str = json["award"].ToString().Split(',');

            if (str[0]=="-")
            {
                return;
            }
            db.T_DrawPrize.Add(new T_DrawPrize()
            {
                Lssue = ucc,
                number1 = int.Parse(str[0]),
                number2 = int.Parse(str[1]),
                number3 = int.Parse(str[2]),
                number4 = int.Parse(str[3]),
                number5 = int.Parse(str[4]),
                number6 = int.Parse(str[5]),
                number7 = int.Parse(str[6]),
            });
            this.textBox1.AppendText("正在同步第 " + ucc + " 期" + json["award"].ToString() + "\r\n");


            db.SaveChanges();
            ViewBind();
        }
    }
}
