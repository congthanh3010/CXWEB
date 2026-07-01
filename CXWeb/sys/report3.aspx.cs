using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.DataVisualization.Charting;


namespace CXWeb.sys
{
    public partial class report3 : System.Web.UI.Page
    {
        connectTT myconn = new connectTT();       
        protected string myDate = "2017-11-02";
        protected string date_from, date_to;
        protected DateTime pDate;
        protected string tg_code="";

        //protected void Timer1_Tick(object sender, EventArgs e)
        //{
        //    this.date1.Text = DateTime.Now.ToString("dd/MM/yyyy");
        //    this.date2.Text = DateTime.Now.ToString("dd/MM/yyyy");
        //    this.Session["hdate1"] = date1.Text;
        //    this.Session["hdate2"] = date2.Text;
        //    LoadChartType();
        //    LoadChart();
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
           

            Chart1.Series[0].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Column");
            Chart2.Series[0].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Pie");
           

            DateTime pDate = new DateTime(DateTime.Now.AddDays(-1).Year, DateTime.Now.AddDays(-1).Month, DateTime.Now.AddDays(-1).Day, 6, 0, 0);
            DateTime pDate2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 6, 0, 0);      
       


            //myconn.myopen();

            //string strsql = "  select * from [PLC_PLCM_CX] order by p_kind asc, clocate asc, cmachine asc ";

            //mcTable = myconn.mysearch(strsql);         

            //myconn.myclose();

            tg_code = base.Request.QueryString["tg"];

            if (!IsPostBack)
            {
             
                if(tg_code==null)
                {
                    this.date1.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    this.date2.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    this.Session["report3_hdate1"] = date1.Text;
                    this.Session["report3_hdate2"] = date2.Text;
                }
                else
                {
                    target_name.Text = tg_code;
                    try
                    {
                        date1.Text = this.Session["report3_hdate1"].ToString();
                        date2.Text = this.Session["report3_hdate2"].ToString();
                    }
                    catch
                    {
                        this.date1.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        this.date2.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        this.Session["report3_hdate1"] = date1.Text;
                        this.Session["report3_hdate2"] = date2.Text;
                    }
                }
              

                LoadChartType();

                LoadChart();
            }


        }
        private void LoadChartType()
        {
            //string[] chartType = Enum.GetNames(typeof(SeriesChartType));
            //drTypeChart.DataSource = chartType;
            //drTypeChart.DataBind();
            //drTypeChart.Items.Insert(0, new ListItem("--Option--", "Column"));

            //drTypeChart2.DataSource = chartType;
            //drTypeChart2.DataBind();
            //drTypeChart2.Items.Insert(0, new ListItem("--Option--", "Column"));
        }
        private void LoadChart()
        {
            try
            {
                DateTime text1;
                DateTime text2;
                try
                {
                    text1 = new DateTime(Convert.ToInt32(date1.Text.Substring(6, 4)), Convert.ToInt32(date1.Text.Substring(3, 2)), Convert.ToInt32(date1.Text.Substring(0, 2)));
                    text2 = new DateTime(Convert.ToInt32(date2.Text.Substring(6, 4)), Convert.ToInt32(date2.Text.Substring(3, 2)), Convert.ToInt32(date2.Text.Substring(0, 2)));
                }
                catch
                {
                    showMessage("日期格式不对/Kiểu thời gian không đúng");
                    this.date1.Text = pDate.ToString("dd/MM/yyyy");
                    this.date2.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    text1 = new DateTime(Convert.ToInt32(date1.Text.Substring(6, 4)), Convert.ToInt32(date1.Text.Substring(3, 2)), Convert.ToInt32(date1.Text.Substring(0, 2)), Convert.ToInt32(date1.Text.Substring(11, 2)), Convert.ToInt32(date1.Text.Substring(14, 2)), 0);
                    text2 = new DateTime(Convert.ToInt32(date2.Text.Substring(6, 4)), Convert.ToInt32(date2.Text.Substring(3, 2)), Convert.ToInt32(date2.Text.Substring(0, 2)), Convert.ToInt32(date2.Text.Substring(11, 2)), Convert.ToInt32(date2.Text.Substring(14, 2)), 0);
                }

                this.time.Text = text1.ToString("yyyy/MM/dd") + " - "+ text2.ToString("yyyy/MM/dd");

                DataTable tbTempt = new DataTable();

                tbTempt = this.GetData("2", text1.ToString("yyyy/MM/dd"),text2.ToString("yyyy/MM/dd"));

                DataTable tb = new DataTable();
                tb.Columns.Add("col1");
                tb.Columns.Add("col2");

                for(int i =0;i< tbTempt.Rows.Count;i++)
                {
                    tb.Rows.Add(tbTempt.Rows[i]["sfb05"].ToString(), tbTempt.Rows[i]["sfb08"].ToString());
                }
             
                this.Repeater2.DataSource = tbTempt;
                this.Repeater2.DataBind();

                ////Set chart data source
                //Chart1.DataSource = tb;

                ////Set series members names for the X and Y values
                //Chart1.Series["Category"].XValueMember = "col1";
                //Chart1.Series["Category"].YValueMembers = "col2";

                ////Data bind to the selected data source
                //Chart1.DataBind();

                DataTable tbTempt3 = new DataTable();

                tbTempt3 = this.GetData("3", text1.ToString("yyyy/MM/dd"), text2.ToString("yyyy/MM/dd"));

                int max_num;
                if (tbTempt3.Rows.Count > 20)
                    max_num = 20;
                else
                    max_num = tbTempt3.Rows.Count;
                foreach (DataRow row in tb.Rows)
                {
                    //Chart1.Series["Category"].Points.Add(Convert.ToDouble(row["col2"]));
                    //Chart1.Series["Category"].Points[Chart1.Series["Category"].Points.Count - 1].Label = row["col1"].ToString();
                    if (Convert.ToDouble(row["col2"]) >= Convert.ToDouble(tbTempt3.Rows[max_num-1]["sfb08"]))
                    {
                        Chart1.Series["Category"].Points.Add();
                        Chart1.Series["Category"].Points[Chart1.Series["Category"].Points.Count - 1].SetValueXY(row["col1"].ToString(), Convert.ToDouble(row["col2"]));
                        Chart1.Series["Category"].Points[Chart1.Series["Category"].Points.Count - 1].Url = "report3.aspx?tg=" + row["col1"].ToString() + "#target";
                    }  
                }
               
                //for(int i=0;i<=15;i++)
                //    Chart1.Series["Category"].Points[i].Color = System.Drawing.ColorTranslator.FromHtml(array_colour[i]);

                //name x,y
                Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                //Chart1.ChartAreas["ChartArea1"].AxisX.Title = "Vendor";
                //Chart1.ChartAreas["ChartArea1"].AxisY.Title = "Price";

                //Chart2.DataSource = tb2;

                ////Set series members names for the X and Y values
                //Chart2.Series["Category"].XValueMember = "col1";
                //Chart2.Series["Category"].YValueMembers = "col2";

                //Chart2.Series["Category"].Points[Chart2.Series["Category"].Points.Count-1].Color = System.Drawing.Color.Green;
                ////Chart2.Series["Category"].Points[1].Color = System.Drawing.Color.Yellow;
                ////Chart2.Series["Category"].Points[2].Color = System.Drawing.Color.Red;
                ////Data bind to the selected data source
                //Chart2.DataBind();

                if (tg_code!=null)
                {
                    //tbTempt = this.GetData("2", text1.ToString("yyyy/MM/dd"), text2.ToString("yyyy/MM/dd"));

                    DataTable tb2 = new DataTable();
                    tb2.Columns.Add("col1");
                    tb2.Columns.Add("col2");

                    for (int i = 0; i < tbTempt.Rows.Count; i++)
                    {
                        if(tbTempt.Rows[i]["sfb05"].ToString()== tg_code)
                        {
                            //生产数量 sfb08
                            //已发数量 sfb081
                            //完工数量 sfb09
                            //报废数量 sfb12
                            tb2.Rows.Add("完工数量", tbTempt.Rows[i]["sfb09"].ToString());
                            tb2.Rows.Add("未生产数量", Convert.ToDouble(tbTempt.Rows[i]["sfb08"]) - Convert.ToDouble(tbTempt.Rows[i]["sfb09"]) - Convert.ToDouble(tbTempt.Rows[i]["sfb12"]));
                            //tb2.Rows.Add("未发数量", Convert.ToDouble(tbTempt.Rows[i]["sfb08"])- Convert.ToDouble(tbTempt.Rows[i]["sfb081"]));   
                            tb2.Rows.Add("报废数量", tbTempt.Rows[i]["sfb12"].ToString());
                        }
                      
                    }

                    

                    foreach (DataRow row in tb2.Rows)
                    {
                        Chart2.Series["Category"].Points.Add(Convert.ToDouble(row["col2"]));
                        if (Convert.ToDouble(row["col2"])!=0)
                        {                           
                            Chart2.Series["Category"].Points[Chart2.Series["Category"].Points.Count - 1].Label = row["col1"].ToString();
                        }
                    }

                    if(Chart2.Series["Category"].Points.Count==3)
                    {
                        Chart2.Series["Category"].Points[0].Color = System.Drawing.Color.Green;
                        Chart2.Series["Category"].Points[1].Color = System.Drawing.Color.Yellow;
                        Chart2.Series["Category"].Points[2].Color = System.Drawing.Color.Red;

                    }

                    //name x,y

                    //Chart2.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                    //Chart2.ChartAreas["ChartArea1"].AxisX.Title = "Product";
                    //Chart2.ChartAreas["ChartArea1"].AxisY.Title = "Price";


                    this.Repeater3.DataSource = tb2;
                    this.Repeater3.DataBind();
                }
            }
            catch (Exception ms)
            {
                Response.Write(ms.Message);
            }
        }



        #region action
        //protected void drTypeChart_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Chart1.Series[0].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), drTypeChart.SelectedValue);
        //    LoadChart();
        //}
        //protected void drTypeChart2_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Chart2.Series[0].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), drTypeChart2.SelectedValue);
        //    LoadChart();
        //}
        #endregion
        private DataTable GetData(string code,string pDate1,string pDate2)
        {
            string strsql;    
            DataTable dataByTime = new DataTable();
            myconn.myopen();
            //strsql = " select  pmm09,sum(pmm40t) as pmm40t from  PMM_FILE  where pmm04>=TO_DATE('" + pDate1 + "','YYYY-MM-DD') and pmm04<=TO_DATE('" + pDate2 + "','YYYY-MM-DD') group by pmm09";
            if (code == "1")
                strsql = " select sum(sfb08),sum(sfb081),sum(sfb09),sum(sfb12) from sfb_file " +
                         " where sfb81>=TO_DATE('" + pDate1 + "','YYYY-MM-DD') and sfb81<=TO_DATE('" + pDate2 + "','YYYY-MM-DD') and sfb87 = 'Y' and sfb43 in ('0','1')";
            else if (code == "2")
                strsql = " select a.*,b.ima02 from (select sfb05,sum(sfb08) as sfb08,sum(sfb081) as sfb081,sum(sfb09) as sfb09,sum(sfb12) as sfb12 from sfb_file " +
                         " where sfb81>=TO_DATE('" + pDate1 + "','YYYY-MM-DD') and sfb81<=TO_DATE('" + pDate2 + "','YYYY-MM-DD') and sfb87 = 'Y' and sfb43 in ('0','1')  group by sfb05) a,ima_file b where a.sfb05 = b.ima01";
            else if (code == "3")
                strsql = " select a.*,b.ima02 from (select sfb05,sum(sfb08) as sfb08,sum(sfb081) as sfb081,sum(sfb09) as sfb09,sum(sfb12) as sfb12 from sfb_file " +
                         " where sfb81>=TO_DATE('" + pDate1 + "','YYYY-MM-DD') and sfb81<=TO_DATE('" + pDate2 + "','YYYY-MM-DD') and sfb87 = 'Y' and sfb43 in ('0','1')  group by sfb05) a,ima_file b where a.sfb05 = b.ima01 order by sfb08 desc";
            else strsql = "";
           dataByTime = myconn.mysearch(strsql);
            myconn.myclose();
            return dataByTime;
        }
        protected void btnXem_Click(object sender, EventArgs e)
        {
            this.Session["report3_hdate1"] = date1.Text;
            this.Session["report3_hdate2"] = date2.Text;
            LoadChartType();
            LoadChart();
        }
        public void showMessage(string mess)
        {
            string strBuilder = "<script language='javascript'>alert('" + mess + "')</script>";
            Response.Write(strBuilder);
        }
    }
}