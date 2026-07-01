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
    public partial class report5 : System.Web.UI.Page
    {
        connectTT myconn = new connectTT();
        connectEIP myconn2 = new connectEIP();
        protected static int max_count_col = 1;
        protected static int colspan = 0;
        protected string date_from, date_to;
        protected string year1, year2;
        protected static string[] code_hiden = new string[100];
        protected static int table_width = 0;
        protected DateTime pDate;
        protected string tg_code = "";
        protected static string code_hide_table = "hidden ='true'";
        protected DataTable tbTempt;
        protected static DataTable tbTempt3;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Chart1.Series[0].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Column");
            //Chart2.Series[0].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Pie");


            DateTime pDate = new DateTime(DateTime.Now.AddDays(-1).Year, DateTime.Now.AddDays(-1).Month, DateTime.Now.AddDays(-1).Day, 6, 0, 0);
            DateTime pDate2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 6, 0, 0);


            //myconn.myopen();

            //string strsql = "  select * from [PLC_PLCM_CX] order by p_kind asc, clocate asc, cmachine asc ";

            //mcTable = myconn.mysearch(strsql);         

            //myconn.myclose();

            tg_code = base.Request.QueryString["vd"];

            this.date1.Text = DateTime.Now.ToString("dd/MM/yyyy");
            this.date2.Text = DateTime.Now.ToString("dd/MM/yyyy");

            if (!IsPostBack)
            {
                code_hide_table = "hidden ='true'";
                textyear1.Text = DateTime.Now.AddYears(-1).ToString("yyyy")+"/01/01";
                textyear2.Text = DateTime.Now.ToString("yyyy") + "/01/01";
                textyear1_to.Text = DateTime.Now.AddYears(-1).ToString("yyyy") + "/12/31";
                textyear2_to.Text = DateTime.Now.ToString("yyyy") + "/12/31";

                year1 = textyear1.Text.Substring(0,4);
                year2 = textyear2.Text.Substring(0, 4);
                Label1.Text = textyear1.Text.Substring(0, 4);
                Label2.Text = textyear1.Text.Substring(0, 4);
                Label3.Text = textyear2.Text.Substring(0, 4);
                Label4.Text = textyear2.Text.Substring(0, 4);
                Label5.Text = textyear1.Text.Substring(0, 4);
                Label6.Text = textyear2.Text.Substring(0, 4);

                //LoadChartType();

                //LoadChart();

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

                //this.time.Text = text1.ToString("yyyy/MM/dd") + " - "+ text2.ToString("yyyy/MM/dd");

                tbTempt = new DataTable();

                DataTable tbTempt2 = new DataTable();
                myconn2.myopen();
                tbTempt2 = myconn2.mysearch("select * from COD_CODE where class = 'report5' order by code");
                myconn2.myclose();


                tbTempt3 = new DataTable();
                tbTempt3.Columns.Add("pmn04");
                tbTempt3.Columns.Add("ima02");
                tbTempt3.Columns.Add("pmn07");
                tbTempt3.Columns.Add("pmn20_1");
                tbTempt3.Columns.Add("pmn31t_1");

                tbTempt3.Columns.Add("pmn20_2");
                tbTempt3.Columns.Add("pmn31t_2");

                tbTempt3.Columns.Add("price_diff");
                tbTempt3.Columns.Add("price_diff_percent");
                tbTempt3.Columns.Add("price_diff2");
                for (int i = 0; i < 100; i++)
                {
                    tbTempt3.Columns.Add(i.ToString());
                }
                tbTempt3.Columns.Add("sum_price_2");
                tbTempt3.Columns.Add("remark");
                tbTempt3.Columns.Add("remark2");


                tbTempt = this.GetData("4", text1.ToString("yyyy/MM/dd"), text2.ToString("yyyy/MM/dd"));

                int idx1 = 0, idx_year2 = 0, count_col = 0, tempt2_idx = 0;

                double sum_single_pmn20 = 0, pmn31t_1_total = 0, price_diff_inTotal = 0, price_diff2_inTotal = 0;
                double price1_total = 0, price2_total = 0;
                max_count_col = 1;
                for (int i = 0; i < tbTempt.Rows.Count; i++)
                {

                    if (tbTempt.Rows[i]["year"].ToString() == "year1")
                    {

                        idx_year2 = 0;
                        tbTempt3.Rows.Add();
                        idx1 = tbTempt3.Rows.Count - 1;
                        tbTempt3.Rows[idx1]["pmn04"] = tbTempt.Rows[i]["pmn04"].ToString();
                        tbTempt3.Rows[idx1]["ima02"] = tbTempt.Rows[i]["ima02"].ToString();
                        tbTempt3.Rows[idx1]["pmn07"] = tbTempt.Rows[i]["pmn07"].ToString();
                        tbTempt3.Rows[idx1]["pmn20_1"] = tbTempt.Rows[i]["pmn20"].ToString();
                        tbTempt3.Rows[idx1]["pmn31t_1"] = tbTempt.Rows[i]["pmn31t"].ToString();
                        price1_total += Math.Round(Convert.ToDouble(tbTempt3.Rows[idx1]["pmn31t_1"]) * Convert.ToDouble(tbTempt3.Rows[idx1]["pmn20_1"]), 2);
                        pmn31t_1_total += Math.Round(Convert.ToDouble(tbTempt3.Rows[idx1]["pmn31t_1"]), 6);

                        for (int j = tempt2_idx; j < tbTempt2.Rows.Count; j++)
                        {
                            if (tbTempt3.Rows[idx1]["pmn04"].ToString() == tbTempt2.Rows[j]["code"].ToString())
                            {
                                tbTempt3.Rows[idx1]["remark"] = tbTempt2.Rows[j]["description"].ToString();
                                tbTempt3.Rows[idx1]["remark2"] = tbTempt2.Rows[j]["description2"].ToString();
                                tempt2_idx = j + 1;
                                break;
                            }
                        }
                    }
                    else if (tbTempt.Rows[i]["year"].ToString() == "year2")
                    {

                        if (tbTempt.Rows[i - 1]["year"].ToString() == "year1")
                        {
                            count_col = 1;
                            sum_single_pmn20 = Convert.ToDouble(tbTempt.Rows[i]["pmn20"]);
                        }
                        else
                        {
                            if (tbTempt.Rows[i - 1]["pmn31t"].ToString() == tbTempt.Rows[i]["pmn31t"].ToString())
                            {
                                sum_single_pmn20 += Convert.ToDouble(tbTempt.Rows[i]["pmn20"]);
                            }
                            else
                            {
                                count_col++;
                                if (count_col > max_count_col) max_count_col = count_col;
                                sum_single_pmn20 = Convert.ToDouble(tbTempt.Rows[i]["pmn20"]);
                                idx_year2 += 2;
                            }
                        }
                        tbTempt3.Rows[idx1][idx_year2 + 10] = sum_single_pmn20;
                        tbTempt3.Rows[idx1][idx_year2 + 11] = tbTempt.Rows[i]["pmn31t"].ToString();

                    }
                    else if (tbTempt.Rows[i]["year"].ToString() == "year2_avg")
                    {
                        tbTempt3.Rows[idx1]["pmn20_2"] = tbTempt.Rows[i]["pmn20"].ToString();
                        tbTempt3.Rows[idx1]["pmn31t_2"] = tbTempt.Rows[i]["pmn31t"].ToString();
                        tbTempt3.Rows[idx1]["price_diff"] = Math.Round(Convert.ToDouble(tbTempt3.Rows[idx1]["pmn31t_2"]) - Convert.ToDouble(tbTempt3.Rows[idx1]["pmn31t_1"]), 6);
                        tbTempt3.Rows[idx1]["price_diff2"] = Math.Round(Convert.ToDouble(tbTempt3.Rows[idx1]["price_diff"]) * Convert.ToDouble(tbTempt3.Rows[idx1]["pmn20_2"]), 2);
                        if (Convert.ToDouble(tbTempt3.Rows[idx1]["pmn31t_1"]) != 0)
                            tbTempt3.Rows[idx1]["price_diff_percent"] = Math.Round(Convert.ToDouble(tbTempt3.Rows[idx1]["price_diff"]) * 100 / Convert.ToDouble(tbTempt3.Rows[idx1]["pmn31t_1"]), 2);
                        else
                        {
                            if (Convert.ToDouble(tbTempt3.Rows[idx1]["price_diff"]) == 0)
                            {
                                tbTempt3.Rows[idx1]["price_diff_percent"] = 0;
                            }
                        }

                        tbTempt3.Rows[idx1]["sum_price_2"] = Math.Round(Convert.ToDouble(tbTempt3.Rows[idx1]["pmn31t_2"]) * Convert.ToDouble(tbTempt3.Rows[idx1]["pmn20_2"]), 2);
                        price2_total += Math.Round(Convert.ToDouble(tbTempt3.Rows[idx1]["pmn31t_2"]) * Convert.ToDouble(tbTempt3.Rows[idx1]["pmn20_2"]), 2);
                        price_diff2_inTotal += Math.Round(Convert.ToDouble(tbTempt3.Rows[idx1]["price_diff2"]), 2);
                        price_diff_inTotal += Math.Round(Convert.ToDouble(tbTempt3.Rows[idx1]["price_diff"]), 6);

                    }
                }
                table_width = 1370 + 80 * max_count_col * 2;
                colspan = max_count_col * 2 + 2;
                for (int i = 0; i < code_hiden.Length; i++)
                {
                    code_hiden[i] = null;
                }
                for (int i = max_count_col; i < code_hiden.Length; i++)
                {
                    code_hiden[i] = "hidden ='true'";
                }
                tbTempt3.Rows.Add("", "", "", "", year1 + "总金额", "", year2 + "总金额", "", "总价差差异(%)", "总差异金额");
                tbTempt3.Rows.Add("", "", "", "", Math.Round(price1_total, 2), "", Math.Round(price2_total, 2), "", Math.Round(price_diff2_inTotal * 100 / (price2_total - price_diff2_inTotal), 2), Math.Round(price_diff2_inTotal, 2));
                this.Repeater1.DataSource = tbTempt3;
                this.Repeater1.DataBind();


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
        private DataTable GetData(string code, string pDate1, string pDate2)
        {
            string strsql;
            DataTable dataByTime = new DataTable();
            myconn.myopen();
            //strsql = " select  pmm09,sum(pmm40t) as pmm40t from  PMM_FILE  where pmm04>=TO_DATE('" + pDate1 + "','YYYY-MM-DD') and pmm04<=TO_DATE('" + pDate2 + "','YYYY-MM-DD') group by pmm09";
            if (code == "1")
                strsql = "select b.pmn04,b.pmn041,b.pmn07,sum(b.pmn20) as pmn20,round(sum(b.pmn20*b.pmn31t*a.pmm42)/sum(b.pmn20),2) as pmn31t from PMM_FILE a,PMN_FILE b where a.pmm01=b.pmn01 and a.pmm18='Y' and a.pmm25='2' " +
                        " and a.pmm04 between TO_DATE('" + year1 + "/1/1', 'YYYY-MM-DD') and TO_DATE('" + year1 + "/12/31', 'YYYY-MM-DD')" +
                        " group by b.pmn04,b.pmn041,b.pmn07 order by b.pmn04";
            else if (code == "2")
                strsql = " ";           
            else if (code == "4")
                strsql = " select  b.pmn04,TO_DATE('1/1/1', 'YYYY-MM-DD') as pmm04,c.ima02,b.pmn07,'year1' as year,sum(b.pmn20) as pmn20,round(sum(b.pmn20 * b.pmn31t * a.pmm42) / sum(b.pmn20), 6) as pmn31t" +
                         " from PMM_FILE a,PMN_FILE b, ima_file c" +
                         " where a.pmm01 = b.pmn01 and a.pmm18 = 'Y' and a.pmm25  in('2','6')  and b.pmn04 = c.ima01" +
                         " and a.pmm04 between TO_DATE('" + textyear1.Text + "', 'YYYY-MM-DD') and TO_DATE('" + textyear1_to.Text + "', 'YYYY-MM-DD') " +
                         " and b.pmn04 in " +
                         " (select  n.pmn04 from PMM_FILE m, PMN_FILE n where m.pmm01 = n.pmn01 and m.pmm18 = 'Y' and m.pmm25  in('2','6') " +
                         " and m.pmm04 between TO_DATE('" + textyear2.Text + "', 'YYYY-MM-DD') and TO_DATE('" + textyear2_to.Text + "', 'YYYY-MM-DD') and(n.pmn04 like 'F%' or n.pmn04 like 'W%' or n.pmn04 like 'M%') and n.pmn04 not like 'MISC%'"+
                         " and n.pmn04 not like 'MF2A%' and n.pmn04 not like 'MF2B%' and n.pmn04 not like 'MF3%' and n.pmn04 not like 'MF5%' and n.pmn04 not like 'MF7%' "+
                         " and n.pmn04 not like 'MH%' and n.pmn04 not like 'MK%' and n.pmn04 not like 'MP%' and n.pmn04 not like 'MR%' and n.pmn04 not like 'F5%')" +
                         " group by b.pmn04,c.ima02,b.pmn07" +
                         " union all " +
                         " select b.pmn04,TO_DATE('4000/1/1', 'YYYY-MM-DD') as pmm04,c.ima02,b.pmn07,'year2_avg' as year,sum(b.pmn20) as pmn20,round(sum(b.pmn20 * b.pmn31t * a.pmm42) / sum(b.pmn20), 6) as pmn31t" +
                         " from PMM_FILE a,PMN_FILE b, ima_file c" +
                         " where a.pmm01 = b.pmn01 and a.pmm18 = 'Y' and a.pmm25  in('2','6')  and b.pmn04 = c.ima01" +
                         " and a.pmm04 between TO_DATE('" + textyear2.Text + "', 'YYYY-MM-DD') and TO_DATE('" + textyear2_to.Text + "', 'YYYY-MM-DD') " +
                         " and b.pmn04 in " +
                         " (select  n.pmn04 from PMM_FILE m, PMN_FILE n where m.pmm01 = n.pmn01 and m.pmm18 = 'Y' and m.pmm25  in('2','6') " +
                         " and m.pmm04 between TO_DATE('" + textyear1.Text + "', 'YYYY-MM-DD') and TO_DATE('" + textyear1_to.Text + "', 'YYYY-MM-DD') and(n.pmn04 like 'F%' or n.pmn04 like 'W%' or n.pmn04 like 'M%') and n.pmn04 not like 'MISC%'" +
                         " and n.pmn04 not like 'MF2A%' and n.pmn04 not like 'MF2B%' and n.pmn04 not like 'MF3%' and n.pmn04 not like 'MF5%' and n.pmn04 not like 'MF7%' " +
                         " and n.pmn04 not like 'MH%' and n.pmn04 not like 'MK%' and n.pmn04 not like 'MP%' and n.pmn04 not like 'MR%' and n.pmn04 not like 'F5%')" +
                         " group by b.pmn04,c.ima02,b.pmn07" +
                         " union all " +
                         " select* from (select b.pmn04, a.pmm04, c.ima02, b.pmn07, 'year2' as year, (b.pmn20) as pmn20, round(b.pmn31t * a.pmm42, 6) as pmn31t" +
                         " from PMM_FILE a, PMN_FILE b, ima_file c" +
                         " where a.pmm01 = b.pmn01 and a.pmm18 = 'Y' and a.pmm25  in('2','6')  and b.pmn04 = c.ima01" +
                         " and a.pmm04 between TO_DATE('" + textyear2.Text + "', 'YYYY-MM-DD') and TO_DATE('" + textyear2_to.Text + "', 'YYYY-MM-DD')" +
                         " and b.pmn04 in " +
                         " (select  n.pmn04 from PMM_FILE m, PMN_FILE n where m.pmm01 = n.pmn01 and m.pmm18 = 'Y' and m.pmm25  in('2','6') " +
                         " and m.pmm04 between TO_DATE('" + textyear1.Text + "', 'YYYY-MM-DD') and TO_DATE('" + textyear1_to.Text + "', 'YYYY-MM-DD')and(n.pmn04 like 'F%' or n.pmn04 like 'W%' or n.pmn04 like 'M%') and n.pmn04 not like 'MISC%'" +
                         " and n.pmn04 not like 'MF2A%' and n.pmn04 not like 'MF2B%' and n.pmn04 not like 'MF3%' and n.pmn04 not like 'MF5%' and n.pmn04 not like 'MF7%' " +
                         " and n.pmn04 not like 'MH%' and n.pmn04 not like 'MK%' and n.pmn04 not like 'MP%' and n.pmn04 not like 'MR%' and n.pmn04 not like 'F5%')" +
                         " order by b.pmn04 asc, a.pmm04 asc)" +
                         " order by pmn04,pmm04,year";//不要改排序顺序
            else
                strsql = " ";
            dataByTime = myconn.mysearch(strsql);
            myconn.myclose();
            return dataByTime;
        }
        protected void btnXem_Click(object sender, EventArgs e)
        {

            year1 = textyear1.Text.Substring(0, 4);
            year2 = textyear2.Text.Substring(0, 4);
            Label1.Text = textyear1.Text.Substring(0, 4);
            Label2.Text = textyear1.Text.Substring(0, 4);
            Label3.Text = textyear2.Text.Substring(0, 4);
            Label4.Text = textyear2.Text.Substring(0, 4);
            Label5.Text = textyear1.Text.Substring(0, 4);
            Label6.Text = textyear2.Text.Substring(0, 4);

            LoadChartType();
            LoadChart();
            code_hide_table = "";
        }
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            year1 = textyear1.Text.Substring(0, 4);
            year2 = textyear2.Text.Substring(0, 4);
            Label1.Text = textyear1.Text.Substring(0, 4);
            Label2.Text = textyear1.Text.Substring(0, 4);
            Label3.Text = textyear2.Text.Substring(0, 4);
            Label4.Text = textyear2.Text.Substring(0, 4);
            Label5.Text = textyear1.Text.Substring(0, 4);
            Label6.Text = textyear2.Text.Substring(0, 4);
            if (code_hide_table.Trim() != "")
            {
                showMessage("请先查询！");
                return;
            }
            string arg = "report " + year1 + " - " + year2 + ".xls";
            base.Response.Clear();
            //base.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            base.Response.ContentType = "application/ms-excel";
            base.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", arg));
            base.Response.BinaryWrite(Excel.Report5(tbTempt3, year1, year2, max_count_col));
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