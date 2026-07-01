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
    public partial class report2 : System.Web.UI.Page
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
                    this.Session["report2_hdate1"] = date1.Text;
                    this.Session["report2_hdate2"] = date2.Text;
                }
                else
                {
                    vendor_name.Text = vd_code;
                    try
                    {
                        date1.Text = this.Session["report2_hdate1"].ToString();
                        date2.Text = this.Session["report2_hdate2"].ToString();
                    }
                    catch
                    {
                        this.date1.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        this.date2.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        this.Session["report2_hdate1"] = date1.Text;
                        this.Session["report2_hdate2"] = date2.Text;
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

                tbTempt = this.GetData("1", text1.ToString("yyyy/MM/dd"),text2.ToString("yyyy/MM/dd"));

                DataTable tb = new DataTable();
                tb.Columns.Add("col1");
                tb.Columns.Add("col2");

                for(int i =0;i< tbTempt.Rows.Count;i++)
                {
                    tb.Rows.Add(tbTempt.Rows[i]["oga03"].ToString(), tbTempt.Rows[i]["oga54"].ToString());
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
                    Chart1.Series["Category"].Points[Chart1.Series["Category"].Points.Count - 1].Url = "report2.aspx?vd="+ row["col1"].ToString()+"#vendor";
                }
               
                //for(int i=0;i<=15;i++)
                //    Chart1.Series["Category"].Points[i].Color = System.Drawing.ColorTranslator.FromHtml(array_colour[i]);

                //name x,y
                Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                Chart1.ChartAreas["ChartArea1"].AxisX.Title = "Client";
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

                if (vd_code != null)
                {
                    tbTempt = this.GetData("2", text1.ToString("yyyy/MM/dd"), text2.ToString("yyyy/MM/dd"));

                    DataTable tb2 = new DataTable();
                    tb2.Columns.Add("col1");
                    tb2.Columns.Add("col2");

                    for (int i = 0; i < tbTempt.Rows.Count; i++)
                    {
                        tb2.Rows.Add(tbTempt.Rows[i]["ogb04"].ToString(), tbTempt.Rows[i]["ogb14t"].ToString());
                    }
                    DataTable tbTempt3 = new DataTable();
                    tbTempt3 = this.GetData("3", text1.ToString("yyyy/MM/dd"), text2.ToString("yyyy/MM/dd"));
                    int max_num;
                    if (tbTempt3.Rows.Count > 5)
                        max_num = 5;
                    else
                        max_num = tbTempt3.Rows.Count;
                    double orther_price = 0;
                    foreach (DataRow row in tb2.Rows)
                    {
                        if (Convert.ToDouble(row["col2"]) >= Convert.ToDouble(tbTempt3.Rows[max_num - 1]["ogb14t"]))
                        {
                            Chart2.Series["Category"].Points.Add(Convert.ToDouble(row["col2"]));
                            Chart2.Series["Category"].Points[Chart2.Series["Category"].Points.Count - 1].Label = row["col1"].ToString();
                        }
                        else
                            orther_price += Convert.ToDouble(row["col2"]);
                    }
                    if (orther_price != 0)
                    {
                        Chart2.Series["Category"].Points.Add(orther_price);
                        Chart2.Series["Category"].Points[Chart2.Series["Category"].Points.Count - 1].Label = "other_product";
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
                strsql = " select a.*,b.occ02 from ( select oga03,round(sum(oga54*oga24),2) as oga54 from OGA_FILE " +
                    " where  oga02>=TO_DATE('" + pDate1 + "','YYYY-MM-DD') and oga02<=TO_DATE('" + pDate2 + "','YYYY-MM-DD') and oga54 <>0  group by oga03 ) a,occ_file b " +
                    " where a.oga03 = b.occ01 ";
            else if (code == "2")
                strsql = " select a.ogb04,a.ogb06,round(sum(a.ogb14t*b.oga24),2) as ogb14t from ogb_file a,OGA_FILE b where a.ogb01 in( select oga01 from OGA_FILE where  oga02>=TO_DATE('" + pDate1 + "','YYYY-MM-DD') and oga02<=TO_DATE('" + pDate2 + "','YYYY-MM-DD')  and  oga03='" + vd_code + "') and a.ogb14t <>0 and  a.ogb01=b.oga01 group by a.ogb04,a.ogb06";
            else if (code == "3")
                strsql = " select a.ogb04,a.ogb06,round(sum(a.ogb14t*b.oga24),2) as ogb14t from ogb_file a,OGA_FILE b where a.ogb01 in( select oga01 from OGA_FILE where  oga02>=TO_DATE('" + pDate1 + "','YYYY-MM-DD') and oga02<=TO_DATE('" + pDate2 + "','YYYY-MM-DD')  and  oga03='" + vd_code + "') and a.ogb14t <>0 and  a.ogb01=b.oga01 group by a.ogb04,a.ogb06  order by ogb14t desc";
            else
                strsql = "";
            dataByTime = myconn.mysearch(strsql);
            myconn.myclose();
            return dataByTime;
        }
        protected void btnXem_Click(object sender, EventArgs e)
        {
            this.Session["report2_hdate1"] = date1.Text;
            this.Session["report2_hdate2"] = date2.Text;
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