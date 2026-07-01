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
using System.Threading;


namespace CXWeb.sys
{
    public partial class report14 : System.Web.UI.Page
    {
        connectTT myconn = new connectTT();
        protected static int col_num=3;
        protected static string[] col_name = new string[col_num];
        protected static string code_hide_table = "hidden ='true'";
        protected DataTable tbTempt;
        protected static DataTable tbTempt3;
        protected static string report_msg="";
        protected void Page_Load(object sender, EventArgs e)
        {
            Chart1.Series[0].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Column");
            if (!IsPostBack)
            {
                textmonth.Text = DateTime.Now.AddMonths(-1).Month.ToString();
                if (textmonth.Text.Length == 1)
                    textmonth.Text = "0" + textmonth.Text;
                if (textmonth.Text == "12")
                {
                    textyear.Text = DateTime.Now.AddYears(-1).Year.ToString();
                }
                else
                {
                    textyear.Text = DateTime.Now.Year.ToString();
                }
               
                code_hide_table = "hidden ='true'";
                report_msg = "";
            }
           

        }
      
        private void LoadChart()
        {
            try
            {

                string query_time = textyear.Text + textmonth.Text;


                tbTempt = new DataTable();                
                tbTempt3 = new DataTable();
               
                for (int i = 0; i < col_num; i++)
                {
                    tbTempt3.Columns.Add(i.ToString());
                }

               
                tbTempt = this.GetData("1", query_time);
                
                tbTempt3.Columns[0].ColumnName = "機台類型";
                tbTempt3.Columns[1].ColumnName = "修模次數";
                tbTempt3.Columns[2].ColumnName = "百分比";

                for (int i = 0; i < col_num; i++)
                {
                    col_name[i] = tbTempt3.Columns[i].ColumnName;
                }

                int sum_times = 0;
                for (int i = 0; i < tbTempt.Rows.Count; i++)
                {
                    tbTempt3.Rows.Add(tbTempt.Rows[i]["machine_type"].ToString(), tbTempt.Rows[i]["times"].ToString(),0);

                    sum_times += Convert.ToInt32(tbTempt3.Rows[i]["修模次數"]);

                }
                for (int i = 0; i < tbTempt.Rows.Count; i++)
                {
                    tbTempt3.Rows[i]["百分比"] = Math.Round(Convert.ToDouble(tbTempt3.Rows[i]["修模次數"])/ sum_times, 2);
                }
               
                    //-----------------------------------------------------

                    DataTable tb = new DataTable();
                tb.Columns.Add("col1");
                tb.Columns.Add("col2");

                foreach (DataRow row in tbTempt3.Rows)
                {
                    Chart1.Series["Category"].Points.Add();
                    Chart1.Series["Category"].Points[Chart1.Series["Category"].Points.Count - 1].Label = row["百分比"].ToString();
                    Chart1.Series["Category"].Points[Chart1.Series["Category"].Points.Count - 1].SetValueXY(row["機台類型"].ToString(), Convert.ToDouble(row["百分比"]));
                   
                }
                Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                //-----------------------------------------------------

                tbTempt3.Rows.Add("總計", sum_times, " ");

                tbTempt3.Columns[0].ColumnName = "0";
                tbTempt3.Columns[1].ColumnName = "1";
                tbTempt3.Columns[2].ColumnName = "2";

                this.Repeater1.DataSource = tbTempt3;
                this.Repeater1.DataBind();

            }
            catch (Exception ms)
            {
                showMessage(ms.Message);
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
        private DataTable GetData(string code,string query_time)
        {
            string strsql;    
            DataTable dataByTime = new DataTable();
            myconn.myopen();
            if (code == "1")
                strsql = "select ta_eci009 as machine_type,count(tc_moe01) as times"+
                         " from eci_file, tc_moe_file" +
                         " where eci01 = tc_moe11 and tc_moeconf = 'Y'" +
                         " and ta_eci009 in ('連續鍛打','單打') "+ 
                         " and to_char(tc_moe02,'yyyymm')= '" + query_time + "'" +
                         " group by ta_eci009";
            else
                strsql = " ";
          
            dataByTime = myconn.mysearch(strsql);
            myconn.myclose();
           
            return dataByTime;
        }
        protected void btnXem_Click(object sender, EventArgs e)
        {
            report_msg = ""; 
            try
            {
                if (Convert.ToInt32(textmonth.Text) < 1 || Convert.ToInt32(textmonth.Text) > 12
                    || Convert.ToInt32(textyear.Text) < 0000 || Convert.ToInt32(textyear.Text) > 9999)
                {
                    this.Repeater1.DataSource = null;
                    this.Repeater1.DataBind();
                    code_hide_table = "hidden ='true'";
                    report_msg = "日期格式不对";

                    return;

                }
            }
            catch
            {
                this.Repeater1.DataSource = null;
                this.Repeater1.DataBind();
                code_hide_table = "hidden ='true'";
                report_msg = "日期格式不对";

                return;
            }

            if (textmonth.Text.Length == 1)
                textmonth.Text = "0" + textmonth.Text;

            string query_time = textyear.Text + textmonth.Text;
            LoadChart();
            code_hide_table = "";
        }
       
        //protected void btnExcel_Click(object sender, EventArgs e)
        //{
        //    if (textmonth.Text.Length == 1)
        //        textmonth.Text = "0" + textmonth.Text;

        //    string query_time = textyear.Text.Substring(2, 2) + "/" + textmonth.Text;

        //    if (code_hide_table.Trim() != "")
        //    {
        //        showMessage("请先查询！");
        //        return;
        //    }
        //    string arg = "機台之換模耗時 " + DateTime.Now.ToString("yyyy/MM/dd") + ".xlsx";
        //    base.Response.Clear();
        //    base.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //    base.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", arg));
        //    base.Response.BinaryWrite(Excel.Report(col_name,tbTempt3, query_time, col_num));
        //    base.Response.Flush();
        //    base.Response.End();
        //}

        public void showMessage(string mess)
        {
            string strBuilder = "<script language='javascript'>alert('" + mess + "')</script>";
            Response.Write(strBuilder);
        }
    }
}