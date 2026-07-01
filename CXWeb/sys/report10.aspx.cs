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
    public partial class report10 : System.Web.UI.Page
    {
        connectTT myconn = new connectTT();
        protected static int col_num=7;
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

                string query_time = textyear.Text.Substring(2, 2) +"/"+ textmonth.Text;


                tbTempt = new DataTable();                
                tbTempt3 = new DataTable();
               
                for (int i = 0; i < col_num; i++)
                {
                    tbTempt3.Columns.Add(i.ToString());
                }
                               

               tbTempt = this.GetData("1", query_time);
               tbTempt.Columns[0].ColumnName = "名次";
               tbTempt.Columns[1].ColumnName = "單位部門";
               tbTempt.Columns[2].ColumnName = "機台編號";
               tbTempt.Columns[3].ColumnName = "機種種類數";
               tbTempt.Columns[4].ColumnName = "換模時間";
               tbTempt.Columns[5].ColumnName = "換模次數";
               tbTempt.Columns[6].ColumnName = "每次換模耗時";
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
                    tbTempt3.Rows[i][0] = (i + 1);
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
                strsql = "select '' as idx,gem02,tc_chma03,shb10_qty,round(tc_chma18/60,2), tc_chma17," +
                         " (case when tc_chma17 <= 0 then 0 else round(tc_chma18 / 60 / tc_chma17, 2) end)" +
                         " from" +
                         " (select substr(tc_chma01, 0, 5) as tc_chma01, tc_chma03,tc_chma001, sum(tc_chma18) as tc_chma18, sum(tc_chma17) as tc_chma17 from tc_chma_file" +
                         " group by substr(tc_chma01, 0, 5), tc_chma03,tc_chma001)  chma" +
                         " inner join" +
                         " (select shb03, shb09, count(shb10) as shb10_qty from" +
                         " (select distinct substr(to_char(shb03, 'yyyy/mm'), 3, 5) as shb03, shb09, shb10 from" +
                         " (select(case when(ecg05 < ecg06 or(ecg05 > ecg06 and shb031 > '12:00')) then shb03 else shb03 - 1 end) as shb03, shb10, shb09" +
                         " from shb_file, ecg_file" +
                         " where shb08 = ecg01(+) and shbconf = 'Y' and shbud02 is not null))" +
                         " group by shb03, shb09) shb" +
                         " on shb03 = tc_chma01 and shb09 = tc_chma03" +
                         " inner join gem_file on gem01=tc_chma001"+
                         " where tc_chma01 = '" + query_time + "'" +
                         " order by gem02,round(tc_chma18 / 60, 2) desc";
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

            string query_time = textyear.Text.Substring(2, 2) + "/" + textmonth.Text;
            LoadChart();
            code_hide_table = "";
        }
       
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            if (textmonth.Text.Length == 1)
                textmonth.Text = "0" + textmonth.Text;

            string query_time = textyear.Text.Substring(2, 2) + "/" + textmonth.Text;

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
            base.Response.BinaryWrite(Excel.Report(col_name,tbTempt3, query_time, col_num));
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