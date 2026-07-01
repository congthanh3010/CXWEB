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
    public partial class report1 : System.Web.UI.Page
    {
        connectTT myconn = new connectTT();       
        protected string myDate = "2017-11-02";
        protected string date_from, date_to;
        protected DateTime pDate;
        protected string vd_code="";
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

            vd_code = base.Request.QueryString["vd"];

            if (!IsPostBack)
            {
             
                if(vd_code==null)
                {
                    this.date1.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    this.date2.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    this.Session["report1_hdate1"] = date1.Text;
                    this.Session["report1_hdate2"] = date2.Text;
                }
                else
                {
                    vender_name.Text = vd_code;
                    try
                    {
                        date1.Text = this.Session["report1_hdate1"].ToString();
                        date2.Text = this.Session["report1_hdate2"].ToString();
                    }
                    catch
                    {
                        this.date1.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        this.date2.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        this.Session["report1_hdate1"] = date1.Text;
                        this.Session["report1_hdate2"] = date2.Text;
                    }
                }
              

                LoadChartType();

                LoadChart();
            }


        }
        private void LoadChartType()
        {
            string[] chartType = Enum.GetNames(typeof(SeriesChartType));
            drTypeChart.DataSource = chartType;
            drTypeChart.DataBind();
            drTypeChart.Items.Insert(0, new ListItem("--Option--", "Column"));

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

                tbTempt = this.GetData("1", text1.ToString("yyyy/MM/dd"),text2.ToString("yyyy/MM/dd"));

                DataTable tb = new DataTable();
                tb.Columns.Add("col1");
                tb.Columns.Add("col2");

                for(int i =0;i< tbTempt.Rows.Count;i++)
                {
                    tb.Rows.Add(tbTempt.Rows[i]["pmm09"].ToString(), tbTempt.Rows[i]["pmm40t"].ToString());
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

                foreach (DataRow row in tb.Rows)
                {
                    //Chart1.Series["Category"].Points.Add(Convert.ToDouble(row["col2"]));
                    //Chart1.Series["Category"].Points[Chart1.Series["Category"].Points.Count - 1].Label = row["col1"].ToString();
                    Chart1.Series["Category"].Points.Add();                 
                    Chart1.Series["Category"].Points[Chart1.Series["Category"].Points.Count - 1].SetValueXY(row["col1"].ToString(), Convert.ToDouble(row["col2"]));
                    Chart1.Series["Category"].Points[Chart1.Series["Category"].Points.Count - 1].Url = "report1.aspx?vd="+ row["col1"].ToString()+"#vender";
                }
               
                //for(int i=0;i<=15;i++)
                //    Chart1.Series["Category"].Points[i].Color = System.Drawing.ColorTranslator.FromHtml(array_colour[i]);

                //name x,y
                Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                Chart1.ChartAreas["ChartArea1"].AxisX.Title = "Vender";
                Chart1.ChartAreas["ChartArea1"].AxisY.Title = "Price(USD)";

                //Chart2.DataSource = tb2;

                ////Set series members names for the X and Y values
                //Chart2.Series["Category"].XValueMember = "col1";
                //Chart2.Series["Category"].YValueMembers = "col2";

                //Chart2.Series["Category"].Points[Chart2.Series["Category"].Points.Count-1].Color = System.Drawing.Color.Green;
                ////Chart2.Series["Category"].Points[1].Color = System.Drawing.Color.Yellow;
                ////Chart2.Series["Category"].Points[2].Color = System.Drawing.Color.Red;
                ////Data bind to the selected data source
                //Chart2.DataBind();

                if (vd_code!=null)
                {
                    tbTempt = this.GetData("2", text1.ToString("yyyy/MM/dd"), text2.ToString("yyyy/MM/dd"));

                    DataTable tb2 = new DataTable();
                    tb2.Columns.Add("col1");
                    tb2.Columns.Add("col2");

                    for (int i = 0; i < tbTempt.Rows.Count; i++)
                    {
                        tb2.Rows.Add(tbTempt.Rows[i]["pmn04"].ToString(), tbTempt.Rows[i]["pmn88t"].ToString());
                    }

                    foreach (DataRow row in tb2.Rows)
                    {
                        Chart2.Series["Category"].Points.Add(Convert.ToDouble(row["col2"]));
                        Chart2.Series["Category"].Points[Chart2.Series["Category"].Points.Count - 1].Label = row["col1"].ToString();
                    }

                    //Chart2.Series["Category"].Points[0].Color = System.Drawing.Color.Green;
                    //Chart2.Series["Category"].Points[1].Color = System.Drawing.Color.Yellow;
                    //Chart2.Series["Category"].Points[2].Color = System.Drawing.Color.Red;

                    //name x,y

                    Chart2.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                    Chart2.ChartAreas["ChartArea1"].AxisX.Title = "Product";
                    Chart2.ChartAreas["ChartArea1"].AxisY.Title = "Price";

                    this.Repeater3.DataSource = tbTempt;
                    this.Repeater3.DataBind();

                }
            }
            catch (Exception ms)
            {
                Response.Write(ms.Message);
            }
        }



        #region action
        protected void drTypeChart_SelectedIndexChanged(object sender, EventArgs e)
        {
            Chart1.Series[0].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), drTypeChart.SelectedValue);
            LoadChart();
        }
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
            if(code=="1")
                strsql = " select a.*,b.pmc03 from ( " +
                     " select pmm09, round(sum(pmm40t*pmm42),2) as pmm40t from PMM_FILE  where  pmm04>=TO_DATE('" + pDate1 + "','YYYY-MM-DD') and pmm04<=TO_DATE('" + pDate2 + "','YYYY-MM-DD')  " +
                     " and pmm40t<>0 group by pmm09) a,pmc_file b where a.pmm09 = b.pmc01";
            else
                strsql = " select a.pmn04,a.pmn041,round(sum(a.pmn88t*b.pmm42),2) as pmn88t from pmn_file a,PMM_FILE b where " +
                         " a.pmn01 in (select pmm01 from PMM_FILE where pmm09 = '"+ vd_code + "' and  pmm04>=TO_DATE('" + pDate1 + "','YYYY-MM-DD') and pmm04<=TO_DATE('" + pDate2 + "','YYYY-MM-DD')) " +
                         " and a.pmn88t <>0 and a.pmn01=b.pmm01 group by a.pmn04,a.pmn041";
            dataByTime = myconn.mysearch(strsql);
            myconn.myclose();
            return dataByTime;
        }
        protected void btnXem_Click(object sender, EventArgs e)
        {
            this.Session["report1_hdate1"] = date1.Text;
            this.Session["report1_hdate2"] = date2.Text;
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