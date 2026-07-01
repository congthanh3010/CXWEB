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
    public partial class report28 : System.Web.UI.Page
    {
        connectTT myconn = new connectTT();
        protected static int col_num=12;
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
                textdate1.Text = DateTime.Now.AddMonths(-1).ToString("yy/MM/dd");
                textdate2.Text = DateTime.Now.ToString("yy/MM/dd");


                code_hide_table = "hidden ='true'";
                report_msg = "";
            }
           

        }
      
        private void LoadChart()
        {
            try
            {

               

                tbTempt = new DataTable();                
                tbTempt3 = new DataTable();
               
                for (int i = 0; i < col_num; i++)
                {
                    tbTempt3.Columns.Add(i.ToString());
                }
                               

               tbTempt = this.GetData("1");               

                tbTempt.Columns[0].ColumnName = "維修單號\n MS đơn bảo dưỡng";
                tbTempt.Columns[1].ColumnName = "日期\n Ngày";
                tbTempt.Columns[2].ColumnName = "機台編號\n MS máy";
                tbTempt.Columns[3].ColumnName = "機種\n Chủng loại";
                tbTempt.Columns[4].ColumnName = "模套編號\n MS khuôn";
                tbTempt.Columns[5].ColumnName = "模具編號\n MS linh kiện";
                tbTempt.Columns[6].ColumnName = "模具名稱\n Tên linh kiện";
                tbTempt.Columns[7].ColumnName = "修模大類\n Nguyên nhân chính";
                tbTempt.Columns[8].ColumnName = "大類描述\n Diễn giải nguyên nhân chính";
                tbTempt.Columns[9].ColumnName = "修模小類\n Nguyên nhân chi tiết";
                tbTempt.Columns[10].ColumnName = "小類描述\n Diễn giải nguyên nhân chi tiết";
                tbTempt.Columns[11].ColumnName = "修模耗時(時)\n Thời gian sửa khuôn(giờ)";
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
        private DataTable GetData(string code)
        {
            string strsql;    
            DataTable dataByTime = new DataTable();
            myconn.myopen();
            if (code == "1")
                strsql = "select moezz.tc_moe01 as tc_moe01, to_char(moezz.tc_moe02,'yy/MM/dd') as tc_moe02,moezz.tc_moe11,moezz.tc_moe10,moebzz.tc_moeb02,moebzz.tc_moeb03,moa.tc_moa02,moebzz.tc_moeb05 as tc_moeb05, tc_mod02,moebzz.tc_moeb06,tc_mod04,moecc.pjsj as fix_time" +
                        " from tc_moeb_file moebzz," +
                        " tc_moe_file moezz," +
                        " tc_mod_file  mod," +
                        " (select moe.tc_moe01 as tc_moe01,round(moe.tc_moe09 / 60 / moeaa.chishu, 2) as pjsj" +
                        " from tc_moe_file moe," +
                        " (select tc_moe01, count(tc_moeb01) as chishu" +
                        " from tc_moeb_file, tc_moe_file" +
                        " where tc_moeb01 = tc_moe01 and tc_moe03 is not null" +
                        " and tc_moeconf = 'Y'  and tc_moeb05 not in('LK01','TM01')" +
                        " group by tc_moe01) moeaa" +
                        " where moe.tc_moe01 = moeaa.tc_moe01) moecc" +
                        " ,tc_moa_file moa "+
                        " where moebzz.tc_moeb01 = moezz.tc_moe01 and moa.tc_moa01= moebzz.tc_moeb03" +
                        " and moezz.tc_moeconf = 'Y'" +
                        " and moebzz.tc_moeb01 = moecc.tc_moe01" +
                        " and moebzz.tc_moeb05 = mod.tc_mod01 and moebzz.tc_moeb06 = mod.tc_mod03" +
                        " and moebzz.tc_moeb05 not in('LK01','TM01')" +
                        " and moezz.tc_moe03 is not null" +
                        " and to_char(moezz.tc_moe02, 'yy/MM/dd')>= '" + textdate1.Text + "'  and to_char(moezz.tc_moe02, 'yy/MM/dd')<= '" + textdate2.Text + "'" +
                        " and moezz.tc_moe10 like '"+textprod.Text+"%'"+
                        " order by tc_moeb05,tc_moe02 asc";
            else
                strsql = " ";
            dataByTime = myconn.mysearch(strsql);
            myconn.myclose();
            return dataByTime;
        }
        protected void btnXem_Click(object sender, EventArgs e)
        {
            report_msg = ""; 
           
            LoadChart();
            code_hide_table = "";
        }
       
        protected void btnExcel_Click(object sender, EventArgs e)
        {
         

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