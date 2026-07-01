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
    public partial class report15 : System.Web.UI.Page
    {
        connectTT myconn = new connectTT();
        protected static int col_num = 3;
        protected static string[] col_name = new string[col_num];
        protected static string code_hide_table = "hidden ='true'";
        protected DataTable tbTempt;
        protected static DataTable tbTempt3;
        protected static string report_msg = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Chart1.Series[0].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Pie");
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

                string query_time = textyear.Text+ textmonth.Text;


                tbTempt = new DataTable();
                tbTempt3 = new DataTable();

                for (int i = 0; i < col_num; i++)
                {
                    tbTempt3.Columns.Add(i.ToString());
                }


                tbTempt = this.GetData("1", query_time);
                tbTempt.Columns[0].ColumnName = "大類描述";
                tbTempt.Columns[1].ColumnName = "修模耗時";
                tbTempt.Columns.Add("百分比");
                int fix_time_total = 0;
                for (int i = 0; i < tbTempt.Rows.Count; i++)
                {
                    fix_time_total+= Convert.ToInt32(tbTempt.Rows[i]["修模耗時"]);
                }
                for (int i = 0; i < tbTempt.Rows.Count; i++)
                {
                    if (tbTempt.Rows[i]["大類描述"].ToString() == "KHAC")
                        tbTempt.Rows[i]["大類描述"] = "KHÁC其他";
                    else if (tbTempt.Rows[i]["大類描述"].ToString() == "MÁY NG")
                        tbTempt.Rows[i]["大類描述"] = "MÁY NG機台因素導致模具損壞";
                    else if (tbTempt.Rows[i]["大類描述"].ToString() == "báo phế")
                        tbTempt.Rows[i]["大類描述"] = "báo phế報廢";
                    else if (tbTempt.Rows[i]["大類描述"].ToString() == "thêm mới MS khuôn, sửa MS")
                        tbTempt.Rows[i]["大類描述"] = "thêm mới MS khuôn, sửa MS模具零配件損壞，需重新作新配件";
                    tbTempt.Rows[i]["百分比"] = Math.Round(Convert.ToDouble(tbTempt.Rows[i]["修模耗時"])*100  / fix_time_total, 2);
                }
                //-----------------------------------------------------

                DataTable tb = new DataTable();
                tb.Columns.Add("col1");
                tb.Columns.Add("col2");

                foreach (DataRow row in tbTempt.Rows)
                {

                    Chart1.Series["Category"].Points.Add();
                    //Chart1.Series["Category"].Points[Chart1.Series["Category"].Points.Count - 1].Label = row["大類描述"].ToString();
                    if (Convert.ToDouble(row["百分比"]) > 0.01)
                        Chart1.Series["Category"].Points[Chart1.Series["Category"].Points.Count - 1].SetValueXY(row["大類描述"].ToString()+ (row["百分比"]).ToString()+"%", Convert.ToDouble(row["百分比"]));
                    else
                        Chart1.Series["Category"].Points[Chart1.Series["Category"].Points.Count - 1].SetValueY(Convert.ToDouble(row["百分比"]));
                }
                Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                //-----------------------------------------------------

                for (int i = 0; i < col_num; i++)
                {
                    col_name[i] = tbTempt.Columns[i].ColumnName;
                }
                for (int i = 0; i < tbTempt.Rows.Count; i++)
                {
                    tbTempt3.Rows.Add(tbTempt.Rows[i]["大類描述"].ToString(), tbTempt.Rows[i]["修模耗時"].ToString(), tbTempt.Rows[i]["百分比"].ToString());
                }

                tbTempt3.Rows.Add("總計", fix_time_total," ");
                this.Repeater1.DataSource = tbTempt3;
                this.Repeater1.DataBind();

            }
            catch (Exception ms)
            {
                showMessage(ms.Message);
                Response.Write(ms.Message);
            }
        }




        private DataTable GetData(string code, string query_time)
        {
            string strsql;
            DataTable dataByTime = new DataTable();
            myconn.myopen();
            if (code == "1")
                strsql = "select tc_mod02 as fix_type,sum(pjsj) as fix_time"+
                         " from" +
                               " (select moezz.tc_moe01 as tc_moe01, moezz.tc_moe02 as tc_moe02, moebzz.tc_moeb05 as tc_moeb05, tc_mod02, moecc.pjsj as pjsj" +
                                " from tc_moeb_file moebzz, tc_moe_file moezz," +
                                     " (select distinct tc_mod01, tc_mod02 from tc_mod_file where tc_mod01 in (select distinct tc_mod01 from tc_mod_file))  mod," +
                                             " (select moe.tc_moe01 as tc_moe01,round(moe.tc_moe09/60 / moeaa.chishu, 2) as pjsj" +
                                             " from tc_moe_file moe," +
                                                  " (select tc_moe01, count(tc_moeb01) as chishu" +
                                                  " from tc_moeb_file, tc_moe_file" +
                                                  " where tc_moeb01=tc_moe01 and tc_moe03 is not null" +
                                                  " and tc_moeconf = 'Y' and tc_moeb05 not in('LK01','BD05','TM01')" +
                                                  " group by tc_moe01) moeaa" +
                                              " where moe.tc_moe01 = moeaa.tc_moe01" +
                                              " ) moecc" +
                                 "  where moebzz.tc_moeb01 = moezz.tc_moe01" +
                                  " and moezz.tc_moeconf = 'Y'" +
                                  " and moebzz.tc_moeb01 = moecc.tc_moe01" +
                                  " and moebzz.tc_moeb05 = mod.tc_mod01" +
                                  " and moebzz.tc_moeb05 not in('LK01','BD05','TM01')" +
                                  " and moezz.tc_moe03 is not null" +
                              "  )" +
                          " where to_char(tc_moe02,'yyyymm')= '"+ query_time+"'" +
                          " group by tc_mod02" +
                          " order by fix_time desc "; 
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