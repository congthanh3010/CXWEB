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
    public partial class report8 : System.Web.UI.Page
    {
        connectTT myconn = new connectTT();
        protected static int max_count_col = 1,col_num=0;
        protected static int colspan = 0;
        protected string myDate = "2017-11-02";
        protected static string strsql;
        protected string date_from, date_to;
        protected static string query_time;
        protected static string[] code_hiden = new string[40];
        protected static string[] col_name = new string[40];
        protected static string code_hide_table = "hidden ='true'";
        protected static int table_width = 0;
        protected DateTime pDate;
        protected string tg_code="";
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

            //tg_code = base.Request.QueryString["vd"];

            if (!IsPostBack)
            {
                code_hide_table = "hidden ='true'";
                this.date1.Text = DateTime.Now.AddDays(-60).ToString("dd/MM/yyyy");
                this.date2.Text = DateTime.Now.ToString("dd/MM/yyyy");

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
                query_time = textyear.Text.Substring(2, 2) + textmonth.Text;

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

               

                tbTempt = new DataTable();
                
                tbTempt3 = new DataTable();

                tbTempt3.Columns.Add("工作站编号");
                tbTempt3.Columns.Add("部門/單位");
                tbTempt3.Columns.Add("机台分类");
                tbTempt3.Columns.Add("机台吨数");
                tbTempt3.Columns.Add("机台编号");
                tbTempt3.Columns.Add("01");

              

                tbTempt = this.GetData("4", text1.ToString("yyyy-MM-dd"),text2.ToString("yyyy-MM-dd"));

                
                int index = 0;
                double[] avg = new double[400]; 
                for (int i = 0; i < tbTempt.Rows.Count; i++)
                {
                    //Response.Write(tbTempt.Rows[i]["shb03"].ToString()); Response.Write("<br/>");
                    if (tbTempt.Rows[i]["shb03"].ToString()=="01")
                    {
                      
                        tbTempt3.Rows.Add();
                        tbTempt3.Rows[i]["工作站编号"] = tbTempt.Rows[i]["ta_eca004"].ToString();
                        tbTempt3.Rows[i]["部門/單位"] = tbTempt.Rows[i]["azf03"].ToString();
                        tbTempt3.Rows[i]["机台分类"] = tbTempt.Rows[i]["ta_eci009"].ToString();
                        tbTempt3.Rows[i]["机台吨数"] = tbTempt.Rows[i]["ta_eci010"].ToString();
                        tbTempt3.Rows[i]["机台编号"] = tbTempt.Rows[i]["shb09"].ToString();
                        tbTempt3.Rows[i]["01"] = tbTempt.Rows[i]["AA"].ToString();
                        avg[i] = Convert.ToDouble(tbTempt3.Rows[i]["01"]);

                        //按生管要求修正信息
                        if (tbTempt3.Rows[i]["机台编号"].ToString()== "LU-DIEN#10"
                            || tbTempt3.Rows[i]["机台编号"].ToString() == "LU-DIEN#2"
                            || tbTempt3.Rows[i]["机台编号"].ToString() == "LU-DIEN#3"
                            || tbTempt3.Rows[i]["机台编号"].ToString() == "LU-DIEN#4"
                            || tbTempt3.Rows[i]["机台编号"].ToString() == "LU-DIEN#5"
                            || tbTempt3.Rows[i]["机台编号"].ToString() == "LU-DIEN#8"
                            || tbTempt3.Rows[i]["机台编号"].ToString() == "LU-DIEN#9")
                        {
                            tbTempt3.Rows[i]["部門/單位"] = "製二";
                            tbTempt3.Rows[i]["机台分类"] = "退火";
                            tbTempt3.Rows[i]["机台吨数"] = "電爐";
                        }
                        else if (tbTempt3.Rows[i]["机台编号"].ToString() == "LU-GASA"
                            || tbTempt3.Rows[i]["机台编号"].ToString() == "LU-GASB"
                            || tbTempt3.Rows[i]["机台编号"].ToString() == "LU-GASC")
                        {
                            tbTempt3.Rows[i]["部門/單位"] = "製二";
                            tbTempt3.Rows[i]["机台分类"] = "退火";
                            tbTempt3.Rows[i]["机台吨数"] = "瓦斯爐";
                        }
                        else if (tbTempt3.Rows[i]["机台编号"].ToString() == "LU-DIEN#7")
                        {
                            tbTempt3.Rows[i]["部門/單位"] = "製二";
                            tbTempt3.Rows[i]["机台分类"] = "退火";
                            tbTempt3.Rows[i]["机台吨数"] = "球化電爐";
                        }
                        else if (tbTempt3.Rows[i]["机台编号"].ToString() == "LU-GASPIT")
                        {
                            tbTempt3.Rows[i]["部門/單位"] = "製二";
                            tbTempt3.Rows[i]["机台分类"] = "退火";
                            tbTempt3.Rows[i]["机台吨数"] = "球化爐（瓦斯）";
                        }
                        else if (tbTempt3.Rows[i]["机台编号"].ToString() == "PC-P001"
                           || tbTempt3.Rows[i]["机台编号"].ToString() == "PC-P002"
                            || tbTempt3.Rows[i]["机台编号"].ToString() == "PC-P003"
                           || tbTempt3.Rows[i]["机台编号"].ToString() == "PC-P004")
                        {
                            tbTempt3.Rows[i]["部門/單位"] = "加工";
                            tbTempt3.Rows[i]["机台分类"] = "噴砂";
                            tbTempt3.Rows[i]["机台吨数"] = "成品噴砂";
                        }
                        else if (tbTempt3.Rows[i]["机台编号"].ToString() == "PC-SP01"
                           || tbTempt3.Rows[i]["机台编号"].ToString() == "PC-SP02"
                           || tbTempt3.Rows[i]["机台编号"].ToString() == "PC-SP03"
                            || tbTempt3.Rows[i]["机台编号"].ToString() == "PC-SP04"
                           || tbTempt3.Rows[i]["机台编号"].ToString() == "PC-SP05")
                        {
                            tbTempt3.Rows[i]["部門/單位"] = "製二";
                            tbTempt3.Rows[i]["机台分类"] = "噴砂";
                            tbTempt3.Rows[i]["机台吨数"] = "半成品噴砂";
                        }
                        else if (tbTempt3.Rows[i]["机台编号"].ToString() == "PC-0007")
                        {
                            tbTempt3.Rows[i]["部門/單位"] = "製二";
                            tbTempt3.Rows[i]["机台分类"] = "噴砂";
                            tbTempt3.Rows[i]["机台吨数"] = "平台噴砂";
                        }
                        else if (tbTempt3.Rows[i]["机台编号"].ToString() == "CR-000C")
                        {
                            tbTempt3.Rows[i]["部門/單位"] = "製二";
                            tbTempt3.Rows[i]["机台分类"] = "洗料";
                            tbTempt3.Rows[i]["机台吨数"] = "洗料C線";
                        }
                        else if (tbTempt3.Rows[i]["机台编号"].ToString() == "CR-000D")
                        {
                            tbTempt3.Rows[i]["部門/單位"] = "製二";
                            tbTempt3.Rows[i]["机台分类"] = "洗料";
                            tbTempt3.Rows[i]["机台吨数"] = "洗料D線";
                        }
                        else if (tbTempt3.Rows[i]["机台编号"].ToString() == "CR-001B")
                        {
                            tbTempt3.Rows[i]["部門/單位"] = "製二";
                            tbTempt3.Rows[i]["机台分类"] = "洗料";
                            tbTempt3.Rows[i]["机台吨数"] = "洗料B 線";
                        }
                        else if (tbTempt3.Rows[i]["机台编号"].ToString() == "LH-0001")
                        {
                            tbTempt3.Rows[i]["部門/單位"] = "製三";
                            tbTempt3.Rows[i]["机台分类"] = "連續沖壓";
                            tbTempt3.Rows[i]["机台吨数"] = "400噸";
                        }
                        else if (tbTempt3.Rows[i]["机台编号"].ToString() == "XL-001B")
                        {
                            tbTempt3.Rows[i]["部門/單位"] = "滾電";
                            tbTempt3.Rows[i]["机台分类"] = "滾電B 線";
                            tbTempt3.Rows[i]["机台吨数"] = "滾電B 線";
                        }
                        else if (tbTempt3.Rows[i]["机台编号"].ToString() == "XL-001C")
                        {
                            tbTempt3.Rows[i]["部門/單位"] = "滾電";
                            tbTempt3.Rows[i]["机台分类"] = "滾電C 線";
                            tbTempt3.Rows[i]["机台吨数"] = "滾電C 線";
                        }
                        else if (tbTempt3.Rows[i]["机台编号"].ToString() == "XL-001D")
                        {
                            tbTempt3.Rows[i]["部門/單位"] = "滾電";
                            tbTempt3.Rows[i]["机台分类"] = "滾電D 線";
                            tbTempt3.Rows[i]["机台吨数"] = "滾電D 線";
                        }
                        else if (tbTempt3.Rows[i]["机台编号"].ToString() == "XT-000C")
                        {
                            tbTempt3.Rows[i]["部門/單位"] = "吊電";
                            tbTempt3.Rows[i]["机台分类"] = "吊電";
                            tbTempt3.Rows[i]["机台吨数"] = "吊電";
                        }
                        else if (tbTempt3.Rows[i]["机台编号"].ToString() == "DC-000A")
                        {
                            tbTempt3.Rows[i]["部門/單位"] = "鋁制組";
                            tbTempt3.Rows[i]["机台分类"] = "陽極處理";
                           
                        }
                        else if (tbTempt3.Rows[i]["机台编号"].ToString() == "DE-B001"
                            || tbTempt3.Rows[i]["机台编号"].ToString() == "DE-B002"
                            || tbTempt3.Rows[i]["机台编号"].ToString() == "DE-B003")
                        {
                            tbTempt3.Rows[i]["部門/單位"] = "鋁制組";
                            tbTempt3.Rows[i]["机台分类"] = "沖孔";

                        }
                        else if (tbTempt3.Rows[i]["机台编号"].ToString() == "DE-B005")
                        {
                            tbTempt3.Rows[i]["部門/單位"] = "鋁制組";
                            tbTempt3.Rows[i]["机台分类"] = "沖孔,切邊";
                        }
                        else if (tbTempt3.Rows[i]["机台编号"].ToString() == "DE-B007"
                            || tbTempt3.Rows[i]["机台编号"].ToString() == "TH-0602")
                        {
                            tbTempt3.Rows[i]["部門/單位"] = "鋁制組";
                            tbTempt3.Rows[i]["机台分类"] = "切邊";
                        }
                        else if (tbTempt3.Rows[i]["机台编号"].ToString() == "DK-0001"
                            || tbTempt3.Rows[i]["机台编号"].ToString() == "DK-0005")
                        {
                            tbTempt3.Rows[i]["部門/單位"] = "鋁制組";
                            tbTempt3.Rows[i]["机台分类"] = "貼膠";
                        }
                        else if (tbTempt3.Rows[i]["机台编号"].ToString() == "DK-0003"
                            || tbTempt3.Rows[i]["机台编号"].ToString() == "DK-0006"
                            || tbTempt3.Rows[i]["机台编号"].ToString() == "DK-0012")
                        {
                            tbTempt3.Rows[i]["部門/單位"] = "鋁制組";
                            tbTempt3.Rows[i]["机台分类"] = "鉚合";
                        }
                        else if (tbTempt3.Rows[i]["机台编号"].ToString() == "DK-0004"
                            || tbTempt3.Rows[i]["机台编号"].ToString() == "DK-0010"
                            || tbTempt3.Rows[i]["机台编号"].ToString() == "DK-0011")
                        {
                            tbTempt3.Rows[i]["部門/單位"] = "鋁制組";
                            tbTempt3.Rows[i]["机台分类"] = "噴膠";
                        }
                        else if (tbTempt3.Rows[i]["机台编号"].ToString() == "GC-ML001")
                        {
                            tbTempt3.Rows[i]["部門/單位"] = "鋁制組";
                            tbTempt3.Rows[i]["机台分类"] = "倒角";
                        }
                        else if (tbTempt3.Rows[i]["机台编号"].ToString() == "PC-AL")
                        {
                            tbTempt3.Rows[i]["部門/單位"] = "鋁制組";
                            tbTempt3.Rows[i]["机台分类"] = "以油墨印字";
                        }
                        else if (tbTempt3.Rows[i]["机台编号"].ToString() == "TH-0251"
                            || tbTempt3.Rows[i]["机台编号"].ToString() == "TH-0601")
                        {
                            tbTempt3.Rows[i]["部門/單位"] = "鋁制組";
                            tbTempt3.Rows[i]["机台分类"] = "(成型,切邊）";
                        }
                        else if (tbTempt3.Rows[i]["机台编号"].ToString() == "TH-0351"
                            || tbTempt3.Rows[i]["机台编号"].ToString() == "TH-0451")
                        {
                            tbTempt3.Rows[i]["部門/單位"] = "鋁制組";
                            tbTempt3.Rows[i]["机台分类"] = "成型";
                        }
                       
                    }
                    else
                    {
                        if (tbTempt.Rows[i]["shb03"].ToString() != tbTempt.Rows[i - 1]["shb03"].ToString())
                        {
                            tbTempt3.Columns.Add(tbTempt.Rows[i]["shb03"].ToString());
                            index = 0;
                        }
                        if (tbTempt3.Rows[index]["机台编号"].ToString() == tbTempt.Rows[i]["shb09"].ToString())
                        {
                            tbTempt3.Rows[index][tbTempt.Rows[i]["shb03"].ToString()] = tbTempt.Rows[i]["AA"].ToString();
                            avg[index] += Convert.ToDouble(tbTempt3.Rows[index][tbTempt.Rows[i]["shb03"].ToString()]);
                            index++;
                        }
                      
                    }                
                                  
                }
                
                tbTempt3.Columns.Add("AVG");

                for (int i = 0; i < tbTempt3.Rows.Count; i++)
                {
                    tbTempt3.Rows[i]["AVG"] = Math.Round(avg[i] /(tbTempt3.Columns.Count - 6),2).ToString();
                }

                for (int i = 0; i < tbTempt3.Columns.Count; i++)
                {
                    col_name[i] = tbTempt3.Columns[i].ColumnName;
                  
                }
                
                col_num = tbTempt3.Columns.Count;

                for (int i = 0; i < col_num; i++)
                {
                    code_hiden[i] = null;
                }
                for (int i = col_num; i < code_hiden.Length; i++)
                {
                    code_hiden[i] = "hidden ='true'";
                }
                table_width = col_num * 100+300;

                //调整列名和列数，为了前台编程方便
                for (int i = tbTempt3.Columns.Count - 1; i >= 0; i--)
                {
                    tbTempt3.Columns[i].ColumnName = i.ToString();
                }
                for (int i = col_num; i <= 36; i++)// 一个月最多31 天 + 6 列
                {
                    tbTempt3.Columns.Add(i.ToString());
                }

                this.Repeater1.DataSource = tbTempt3;
                this.Repeater1.DataBind();


            }
            catch (Exception ms)
            {
                //Response.Write(strsql);
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
          
            DataTable dataByTime = new DataTable();
            myconn.myopen();
            if (code == "4")
                strsql = "select t.yymm,TO_CHAR (t.shb03, 'DD') as shb03,t.ta_eca004,t.azf03,b.ta_eci009,b.ta_eci010,t.shb09,round(sum(t.D2)*100/sum(t.A),1) as AA" +
                        " from V_2789_JDL_CZX t,eci_file b where t.shb09 = b.eci01" +
                        " and t.yymm = '" + query_time+"' and t.TA_ECA004 in ('10','20','21','22','23','30','41','42','51','52','A0')" +
                        " group by  t.yymm,t.shb03,t.ta_eca004,t.azf03,b.ta_eci009,b.ta_eci010,t.shb09" +
                        " order by t.shb03,t.ta_eca004,b.ta_eci009,b.ta_eci010,t.shb09";
            else
                strsql = " ";
            string v = strsql;
            dataByTime = myconn.mysearch(v);
           
            myconn.myclose();
           
            return dataByTime;
        }
        protected void btnXem_Click(object sender, EventArgs e)
        {

            
            try
            {
                if (Convert.ToInt32(textmonth.Text) < 1 || Convert.ToInt32(textmonth.Text) > 12
                    || Convert.ToInt32(textyear.Text) < 0000 || Convert.ToInt32(textyear.Text) > 9999)
                {
                    showMessage("日期格式不对");
                    return;

                }
            }
            catch
            {
                showMessage("日期格式不对");
                return;
            }
            query_time = textyear.Text.Substring(2, 2) + textmonth.Text;

            LoadChartType();
            LoadChart();
            code_hide_table = "";
        }
        protected void btnExcel_Click(object sender, EventArgs e)
        {
           if(code_hide_table.Trim()!="")
            {
                showMessage("请先查询！");
                return;
            }

            string arg = "report " + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
            base.Response.Clear();
            //base.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            base.Response.ContentType= "application/ms-excel";
            base.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", arg));
            base.Response.BinaryWrite(Excel.Report8(col_name,tbTempt3, query_time,col_num));
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