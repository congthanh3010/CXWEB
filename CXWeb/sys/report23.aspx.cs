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
    public partial class report23 : System.Web.UI.Page
    {
        connectTT myconn = new connectTT();
        protected static int col_num=5;
        protected static string[] col_name = new string[col_num];
        protected static int table_width = col_num*100;
        protected static string code_hide_table = "hidden ='true'";
        protected DataTable tbTempt;
        protected static DataTable tbTempt3;
        protected static string report_msg="";
        protected void Page_Load(object sender, EventArgs e)
        {           
            
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
               tbTempt.Columns[0].ColumnName = "機種";
               tbTempt.Columns[1].ColumnName = "機台編號";
               tbTempt.Columns[2].ColumnName = "修模次數";
               tbTempt.Columns[3].ColumnName = "生產數量";
               tbTempt.Columns[4].ColumnName = "每次修模生產數量";
                for (int i = 0; i < col_num; i++)
                {
                    col_name[i] = tbTempt.Columns[i].ColumnName;
                }
               
               
                for (int i = 0; i < tbTempt.Rows.Count; i++)
                {
                    tbTempt3.Rows.Add();
                    for (int j = 0; j < col_num; j++)
                    {
                        tbTempt3.Rows[i][j] = tbTempt.Rows[i][j].ToString();
                    }
                    
                }

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
                strsql = "select tc_moe10,tc_moe11,mjtime,shb111,round(shb111/mjtime,0) as qty" +
                        " from" +
                        " (" +
                        " select to_char(tc_moe02, 'yyyymm') as tc_moe02, tc_moe10, tc_moe11, count(tc_moe01) as mjtime" +
                        " from tc_moe_file" +
                        " where tc_moeconf = 'Y' and tc_moe03 is not null" +
                        " group by to_char(tc_moe02, 'yyyymm'), tc_moe10, tc_moe11) moe" +
                        " inner join(" +
                        " select to_char(shb03, 'yyyymm') as shb03, shb10, shb09, sum(shb111) as shb111 from" +
                        " (" +
                        " select(case when(ecg05 < ecg06 or(ecg05 > ecg06 and shb031 > '12:00')) then shb03 else shb03 - 1 end) as shb03, shb10, shb09, (shb111 + shb112 + shb115) as shb111" +
                        " from shb_file, ecg_file" +
                        " where shb08 = ecg01(+) and shbconf = 'Y')" +
                        " group by to_char(shb03, 'yyyymm'), shb10, shb09) shb" +
                        " on shb.shb10 = moe.tc_moe10 and shb.shb09 = moe.tc_moe11 and shb.shb03 = moe.tc_moe02" +
                        " where moe.tc_moe02 = '" + query_time + "'" +
                        " order by mjtime desc";
            
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

           
            LoadChart();
            code_hide_table = "";
        }
       
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            if (textmonth.Text.Length == 1)
                textmonth.Text = "0" + textmonth.Text;

          

            if (code_hide_table.Trim() != "")
            {
                showMessage("请先查询！");
                return;
            }
            string arg = "report " + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
            base.Response.Clear();
            //base.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            base.Response.ContentType= "application/ms-excel";
            base.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", arg));
            base.Response.BinaryWrite(Excel.Report(col_name,tbTempt3, "", col_num));
            base.Response.Flush();
            base.Response.End();
        }

        public void showMessage(string mess)
        {
            string strBuilder = "<script language='javascript'>alert('" + mess + "')</script>";
            Response.Write(strBuilder);
        }
    }
}