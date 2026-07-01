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
    public partial class report16 : System.Web.UI.Page
    {
        connectTT myconn = new connectTT();
        protected static int col_num = 9;
        protected static string[] col_name = new string[col_num];
        protected static int table_width = col_num * 100;
        protected static string code_hide_table = "hidden ='true'";
        protected DataTable tbTempt;
        protected static DataTable tbTempt3;
        protected static string report_msg = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                textyear1.Text = DateTime.Now.AddYears(-1).Year.ToString();
                textyear2.Text = DateTime.Now.Year.ToString();
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
                tbTempt.Columns[0].ColumnName = "料號";
                tbTempt.Columns[1].ColumnName = "供應商代號";
                tbTempt.Columns[2].ColumnName = "供應商簡稱";               
                tbTempt.Columns[3].ColumnName = "最後一次採購單價";
                tbTempt.Columns[4].ColumnName = "度採購縂數量";
                tbTempt.Columns[5].ColumnName = "採購縂金額";
                tbTempt.Columns[6].ColumnName = "價格本/核價單";
                tbTempt.Columns[7].ColumnName = "價格";
                tbTempt.Columns[8].ColumnName = "幣別";
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
                strsql = "select pmn04, pmm09, pmc03, pmn31t, pmn20,pmn88t,(case when pmi01a is null then pmi01b else pmi01a end ) as pmi01," +
      " (case when pmj07t1 is null then pmj07t2 else pmj07t1 end) as pmj07t," +
            " (case when pmj05a is null then pmj05b else pmj05a end ) as pmj05" +
      " from(select pmm09, pmc03, pmn04, pmn20, pmn88t, pmn31t, pmi01a, pmj07t1, pmi01b, pmj07t2, pmj05a, pmj05b" +
      " from(select pmm09, pmc03, pmn04, sum(pmn20) as pmn20, sum(pmn88t) as pmn88t, max(pmn31t) as pmn31t" +
      " from pmn_file, pmm_file, pmc_file" +
            " where pmn01 = pmm01 and pmm09 = pmc01 and pmn01 = pmm01 and pmm18 = 'Y' and(pmm25 = '1' or pmm25 = '2' or pmm25 = '6')" +
            " and to_char(pmm04, 'yyyy') = '"+textyear1.Text+"'" +
            " group by pmm09, pmc03, pmn04) pa" +
      " left outer join" +
            " (select pmi03, pmj03, pmi01 as pmi01a, pmj07t as pmj07t1, pmj05 as pmj05a from pmj_file a, pmi_file b" +
            " where a.pmj01 = b.pmi01 and b.pmiconf = 'Y' and b.pmi06 = '1'" +
            " and to_char(pmi02, 'yyyy') = '" + textyear2.Text + "'" +
            " and to_char(a.ta_pmj01, 'yyyy-mm-dd') > to_char(sysdate, 'yyyy-mm-dd') and b.ta_pmi04 = '1'" +
            " and(a.pmj03, b.pmi03, a.pmj09) in (select pmj03, pmi03, max(pmj09) as pmj09 from pmj_file, pmi_file where pmj01 = pmi01 and ta_pmi04 = '1'" +
            " group by pmj03, pmi03))pb" +
            " on pa.pmm09 = pb.pmi03 and pa.pmn04 = pb.pmj03" +
      " left outer join" +
            " (select pmi03, pmj03, pmi01 as pmi01b, pmj07t as pmj07t2, pmj05 as pmj05b from pmj_file a, pmi_file b" +
            " where a.pmj01 = b.pmi01 and b.pmiconf = 'Y' and b.pmi06 = '1'" +
            " and to_char(pmi02, 'yyyy') = '" + textyear2.Text + "'" +
            " and to_char(a.ta_pmj01, 'yyyy-mm-dd') > to_char(sysdate, 'yyyy-mm-dd') and b.ta_pmi04 = '3'" +
            " and(a.pmj03, b.pmi03, a.pmj09) in (select pmj03, pmi03, max(pmj09) as pmj09" +
            " from pmj_file, pmi_file where pmj01 = pmi01 and ta_pmi04 = '3'" +
            " group by pmj03, pmi03))pc on pa.pmm09 = pc.pmi03 and pa.pmn04 = pc.pmj03) order by pmn04";
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
            base.Response.BinaryWrite(Excel.Report(col_name, tbTempt3, " ", col_num));
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