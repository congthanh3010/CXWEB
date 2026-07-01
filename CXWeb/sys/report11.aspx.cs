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
    public partial class report11 : System.Web.UI.Page
    {
        connectTT myconn = new connectTT();
        protected static int col_num=4;
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

                string query_time = textyear.Text.Substring(2, 2) +"/"+ textmonth.Text;


                tbTempt = new DataTable();                
                tbTempt3 = new DataTable();
               
                for (int i = 0; i < col_num; i++)
                {
                    tbTempt3.Columns.Add(i.ToString());
                }
                               

               tbTempt = this.GetData("1", query_time);
                tbTempt.Columns[0].ColumnName = "單位部門";
                tbTempt.Columns[1].ColumnName = "技術員工號";
                tbTempt.Columns[2].ColumnName = "技術員姓名";
                tbTempt.Columns[3].ColumnName = "換模耗時";
                //-----------------------------------------------------

                DataTable tb = new DataTable();
                tb.Columns.Add("col1");
                tb.Columns.Add("col2");

                foreach (DataRow row in tbTempt.Rows)
                {
                    if (Chart1.Series["Category"].Points.Count < 20)
                    {
                        Chart1.Series["Category"].Points.Add();
                        Chart1.Series["Category"].Points[Chart1.Series["Category"].Points.Count - 1].Label = row["換模耗時"].ToString();
                        Chart1.Series["Category"].Points[Chart1.Series["Category"].Points.Count - 1].SetValueXY(row["技術員工號"].ToString(), Convert.ToDouble(row["換模耗時"]));
                    }
                }
                Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                //-----------------------------------------------------

                for (int i = 0; i < col_num; i++)
                {
                    col_name[i] = tbTempt.Columns[i].ColumnName;
                }
                for (int i = 0; i < tbTempt.Rows.Count; i++)
                {
                    tbTempt3.Rows.Add(tbTempt.Rows[i]["單位部門"].ToString(), tbTempt.Rows[i]["技術員工號"].ToString(), tbTempt.Rows[i]["技術員姓名"].ToString(), tbTempt.Rows[i]["換模耗時"].ToString());
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



       
        private DataTable GetData(string code,string query_time)
        {
            try
            {
                string strsql;
                DataTable dataByTime = new DataTable();
                myconn.myopen();
                if (code == "1")
                    strsql = "select gem02,shb04,gen02," +
                            " round((tc_chma18 + tc_chma10) / 60, 2) as avg_time" +
                            " from" +
                            " (select  substr(tc_chma01, 0, 5) as tc_chma01,tc_chma001, shb04, sum(tc_chma18) as tc_chma18, sum(tc_chma10) as tc_chma10, sum(tc_chma16) as tc_chma16" +
                            " from tc_chma_file" +
                            " inner join" +
                            " (select distinct to_char(shb03, 'yy/mm/dd') as shb03, shb08, shb09, shb04 from" +
                            " (select(case when(ecg05 < ecg06 or(ecg05 > ecg06 and shb031 > '12:00')) then shb03 else shb03 - 1 end) as shb03, shb04, shb09, shb08" +
                            " from shb_file, ecg_file" +
                            " where shb08 = ecg01(+) and shbconf = 'Y' and shbud02 is not null))" +
                            " on shb03 = tc_chma01 and shb09 = tc_chma03 and shb08 = tc_chma02" +
                            " group by substr(tc_chma01, 0, 5),shb04,tc_chma001) " +
                            " inner join gen_file on gen01 = shb04" +
                            " inner join gem_file on gem01=tc_chma001" +
                            " where tc_chma01 = '" + query_time + "' and tc_chma18+tc_chma10 <> 0" +
                            " order by gem02,avg_time desc";
                else
                    strsql = " ";
                dataByTime = myconn.mysearch(strsql);
                myconn.myclose();
                return dataByTime;
            }
            catch (Exception ms)
            {
                showMessage(ms.Message);
                Response.Write(ms.Message);
                report_msg = ms.Message;
                DataTable dataByTime = new DataTable();
                return dataByTime;
            }
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