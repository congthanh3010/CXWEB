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
    public partial class mcReport : System.Web.UI.Page
    {
        connectDB myconn = new connectDB();       
        protected string myDate = "2017-11-02";
        protected string date_from, date_to;
        protected DateTime pDate;
        protected string mc_code;
        protected void Page_Load(object sender, EventArgs e)
        {
            Chart1.Series[0].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Pie");
            Chart2.Series[0].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Pie");
            myconn.myopen();

            string strsql = "  select distinct clocate from [PLC_PLCM_CX] where P_Kind='A'";

            this.Repeater1.DataSource = myconn.mysearch(strsql);
            this.Repeater1.DataBind();

            myconn.myclose();

            DateTime pDate = new DateTime(DateTime.Now.AddDays(-1).Year, DateTime.Now.AddDays(-1).Month, DateTime.Now.AddDays(-1).Day, 6, 0, 0);
            DateTime pDate2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 6, 0, 0);
          
            if (myDate == "")
            {
                date_from = pDate.ToString("yyyy-MM-dd HH:mm");
                date_to = pDate2.ToString("yyyy-MM-dd HH:mm");
            }
            else
            {
                date_from = myDate+" 06:00";
                date_to =Convert.ToDateTime(myDate).AddDays(1).ToString("yyyy - MM - dd")+ " 05:59";
            }


            //myconn.myopen();

            //string strsql = "  select * from [PLC_PLCM_CX] order by p_kind asc, clocate asc, cmachine asc ";

            //mcTable = myconn.mysearch(strsql);         

            //myconn.myclose();

            mc_code = base.Request.QueryString["mc"];
          
            this.mamay.Text = mc_code;
           

            pDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            if (DateTime.Now.Hour >= 18)
            {
                pDate = pDate.AddHours(18.0);
            }
            else if (DateTime.Now.Hour < 6)
            {
                pDate = pDate.AddHours(-6.0);
            }
            else
            {
                pDate = pDate.AddHours(6.0);
            }


           
        

            if (!IsPostBack && mc_code != null)
            {
               

                hmamay.Value = mc_code;
                this.date1.Text = pDate.ToString("dd/MM/yyyy HH:mm");
                this.date2.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

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
                    text1 = new DateTime(Convert.ToInt32(date1.Text.Substring(6, 4)), Convert.ToInt32(date1.Text.Substring(3, 2)), Convert.ToInt32(date1.Text.Substring(0, 2)), Convert.ToInt32(date1.Text.Substring(11, 2)), Convert.ToInt32(date1.Text.Substring(14, 2)), 0);
                    text2 = new DateTime(Convert.ToInt32(date2.Text.Substring(6, 4)), Convert.ToInt32(date2.Text.Substring(3, 2)), Convert.ToInt32(date2.Text.Substring(0, 2)), Convert.ToInt32(date2.Text.Substring(11, 2)), Convert.ToInt32(date2.Text.Substring(14, 2)), 0);
                }
                catch
                {
                    showMessage("日期格式不对/Kiểu thời gian không đúng");
                    this.date1.Text = pDate.ToString("dd/MM/yyyy HH:mm");
                    this.date2.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                    text1 = new DateTime(Convert.ToInt32(date1.Text.Substring(6, 4)), Convert.ToInt32(date1.Text.Substring(3, 2)), Convert.ToInt32(date1.Text.Substring(0, 2)), Convert.ToInt32(date1.Text.Substring(11, 2)), Convert.ToInt32(date1.Text.Substring(14, 2)), 0);
                    text2 = new DateTime(Convert.ToInt32(date2.Text.Substring(6, 4)), Convert.ToInt32(date2.Text.Substring(3, 2)), Convert.ToInt32(date2.Text.Substring(0, 2)), Convert.ToInt32(date2.Text.Substring(11, 2)), Convert.ToInt32(date2.Text.Substring(14, 2)), 0);
                }

                this.time.Text = date1.Text+" - "+date2.Text;

                DataTable tbTempt = new DataTable(); 
                       
                tbTempt = this.GetData(this.hmamay.Value, text1.ToString("yyyy-MM-dd HH:mm"), text2.ToString("yyyy-MM-dd HH:mm"));

                DataTable tb = new DataTable();
                tb.Columns.Add("col1");
                tb.Columns.Add("col2");

                DataTable tb2 = new DataTable();
                tb2.Columns.Add("col1");
                tb2.Columns.Add("col2");



                double count = 0;
                double countA01 = 0;
                double countB01 = 0;
                double countC01 = 0;
                double countC02 = 0;
                double countC03 = 0;
                double countC04 = 0;
                double countC05 = 0;
                double countC06 = 0;
                double countC07 = 0;
                double countD01 = 0;
                double countD02 = 0;
                double countD03 = 0;
                double countD04 = 0;
                double countD05 = 0;
                double countD06 = 0;
                double count_total = 0;

                double countState1 = 0;
                double countState2 = 0;
                double countState3 = 0;
                Session["a"] = " ";
                for (int i = 0; i < tbTempt.Rows.Count; i++)
                {
                    if (tbTempt.Rows[i]["cStatus"].ToString().Trim() =="A01")
                    {
                        countA01++;
                    }
                    else if (tbTempt.Rows[i]["cStatus"].ToString().Trim() == "B01")
                    {
                        countB01++;
                    }
                    else if (tbTempt.Rows[i]["cStatus"].ToString().Trim() == "C01")
                    {
                        countC01++;
                    }
                    else if (tbTempt.Rows[i]["cStatus"].ToString().Trim() == "C02")
                    {
                        countC02++;
                    }
                    else if (tbTempt.Rows[i]["cStatus"].ToString().Trim() == "C03")
                    {
                        countC03++;
                    }
                    else if (tbTempt.Rows[i]["cStatus"].ToString().Trim() == "C04")
                    {
                        countC04++;
                    }
                    else if (tbTempt.Rows[i]["cStatus"].ToString().Trim() == "C05")
                    {
                        countC05++;
                    }
                    else if (tbTempt.Rows[i]["cStatus"].ToString().Trim() == "C06")
                    {
                        countC06++;
                    }
                    else if (tbTempt.Rows[i]["cStatus"].ToString().Trim() == "C07")
                    {
                        countC07++;
                    }
                    else if (tbTempt.Rows[i]["cStatus"].ToString().Trim() == "D01")
                    {
                        countD01++;
                    }
                    else if (tbTempt.Rows[i]["cStatus"].ToString().Trim() == "D02")
                    {
                        countD02++;
                    }
                    else if (tbTempt.Rows[i]["cStatus"].ToString().Trim() == "D03")
                    {
                        countD03++;
                    }
                    else if (tbTempt.Rows[i]["cStatus"].ToString().Trim() == "D04")
                    {
                        countD04++;
                    }
                    else if (tbTempt.Rows[i]["cStatus"].ToString().Trim() == "D05")
                    {
                        countD05++;
                    }
                    else if (tbTempt.Rows[i]["cStatus"].ToString().Trim() == "D06")
                    {
                        countD06++;
                    }
                    else
                    {
                        count++;
                    }

                    if (tbTempt.Rows[i]["State"].ToString().Trim() == "1")
                    {
                        countState1++;
                    }
                    else if(tbTempt.Rows[i]["State"].ToString().Trim() == "2")
                    {
                        countState2++;
                    }
                    else if(tbTempt.Rows[i]["State"].ToString().Trim() == "3")
                    {
                        countState3++;
                    }
                   
                }
                count_total = tbTempt.Rows.Count;

                //if (countA01 != 0)
                //    tb.Rows.Add("A01:" + (Math.Round(countA01 * 100 / count_total, 2)).ToString() + "%", countA01);
                //else
                //    tb.Rows.Add("", 0);
                //if (countB01 != 0)
                //    tb.Rows.Add("B01:" + (Math.Round(countB01 * 100 / count_total, 2)).ToString() + "%", countB01);
                //else
                //    tb.Rows.Add("", 0);
                //if (countC01 != 0)
                //    tb.Rows.Add("C01:" + (Math.Round(countC01 * 100 / count_total, 2)).ToString() + "%", countC01);
                //else
                //    tb.Rows.Add("", 0);
                //if (countC02 != 0)
                //    tb.Rows.Add("C02:" + (Math.Round(countC02 * 100 / count_total, 2)).ToString() + "%", countC02);
                //else
                //    tb.Rows.Add("", 0);
                //if (countC03 != 0)
                //    tb.Rows.Add("C03:" + (Math.Round(countC03 * 100 / count_total, 2)).ToString() + "%", countC03);
                //else
                //    tb.Rows.Add("", 0);
                //if (countC04 != 0)
                //    tb.Rows.Add("C04:" + (Math.Round(countC04 * 100 / count_total, 2)).ToString() + "%", countC04);
                //else
                //    tb.Rows.Add("", 0);
                //if (countC05 != 0)
                //    tb.Rows.Add("C05:" + (Math.Round(countC05 * 100 / count_total, 2)).ToString() + "%", countC05);
                //else
                //    tb.Rows.Add("", 0);
                //if (countC06 != 0)
                //    tb.Rows.Add("C06:" + (Math.Round(countC06 * 100 / count_total, 2)).ToString() + "%", countC06);
                //else
                //    tb.Rows.Add("", 0);
                //if (countC07 != 0)
                //    tb.Rows.Add("C07:" + (Math.Round(countC07 * 100 / count_total, 2)).ToString() + "%", countC07);
                //else
                //    tb.Rows.Add("", 0);
                //if (countD01 != 0)
                //    tb.Rows.Add("D01:" + (Math.Round(countD01 * 100 / count_total, 2)).ToString() + "%", countD01);
                //else
                //    tb.Rows.Add("", 0);
                //if (countD02 != 0)
                //    tb.Rows.Add("D02:" + (Math.Round(countD02 * 100 / count_total, 2)).ToString() + "%", countD02);
                //else
                //    tb.Rows.Add("", 0);
                //if (countD03 != 0)
                //    tb.Rows.Add("D03:" + (Math.Round(countD03 * 100 / count_total, 2)).ToString() + "%", countD03);
                //else
                //    tb.Rows.Add("", 0);
                //if (countD04 != 0)
                //    tb.Rows.Add("D04:" + (Math.Round(countD04 * 100 / count_total, 2)).ToString() + "%", countD04);
                //else
                //    tb.Rows.Add("", 0);
                //if (countD05 != 0)
                //    tb.Rows.Add("D05:" + (Math.Round(countD05 * 100 / count_total, 2)).ToString() + "%", countD05);
                //else
                //    tb.Rows.Add("", 0);
                //if (countD06 != 0)
                //    tb.Rows.Add("D06:" + (Math.Round(countD06 * 100 / count_total, 2)).ToString() + "%", countD06);
                //else
                //    tb.Rows.Add("", 0);
                //if (count != 0)
                //    tb.Rows.Add("A00:" + (Math.Round(count * 100 / count_total, 2)).ToString() + "%", count);
                //else
                //    tb.Rows.Add("",0);
                int min_size = 2;

                tb.Rows.Add(Math.Round(countA01 * 100 / count_total, 2)> min_size?"A01":"", countA01);
                tb.Rows.Add(Math.Round(countB01 * 100 / count_total, 2) > min_size ? "B01" : "", countB01);
                tb.Rows.Add(Math.Round(countC01 * 100 / count_total, 2) > min_size ? "C01" : "", countC01);
                tb.Rows.Add(Math.Round(countC02 * 100 / count_total, 2) > min_size ? "C02" : "", countC02);
                tb.Rows.Add(Math.Round(countC03 * 100 / count_total, 2) > min_size ? "C03" : "", countC03);
                tb.Rows.Add(Math.Round(countC04 * 100 / count_total, 2) > min_size ? "C04" : "", countC04);
                tb.Rows.Add(Math.Round(countC05 * 100 / count_total, 2) > min_size ? "C05" : "", countC05);
                tb.Rows.Add(Math.Round(countC06 * 100 / count_total, 2) > min_size ? "C06" : "", countC06);
                tb.Rows.Add(Math.Round(countC07 * 100 / count_total, 2) > min_size ? "C07" : "", countC07);
                tb.Rows.Add(Math.Round(countD01 * 100 / count_total, 2) > min_size ? "D01" : "", countD01);
                tb.Rows.Add(Math.Round(countD02 * 100 / count_total, 2) > min_size ? "D02" : "", countD02);
                tb.Rows.Add(Math.Round(countD03 * 100 / count_total, 2) > min_size ? "D03" : "", countD03);
                tb.Rows.Add(Math.Round(countD04 * 100 / count_total, 2) > min_size ? "D04" : "", countD04);
                tb.Rows.Add(Math.Round(countD05 * 100 / count_total, 2) > min_size ? "D05" : "", countD05);
                tb.Rows.Add(Math.Round(countD06 * 100 / count_total, 2) > min_size ? "D06" : "", countD06);
                tb.Rows.Add(Math.Round(count * 100 / count_total, 2) > min_size ? "A00" : "", count);

                //tb.Rows.Add("", 1);
                //tb.Rows.Add("", 1);
                //tb.Rows.Add("", 1);
                //tb.Rows.Add("", 1);
                //tb.Rows.Add("", 1);
                //tb.Rows.Add("", 1);
                //tb.Rows.Add("", 1);
                //tb.Rows.Add("", 1);
                //tb.Rows.Add("", 1);
                //tb.Rows.Add("", 1);
                //tb.Rows.Add("", 1);
                //tb.Rows.Add("", 1);
                //tb.Rows.Add("", 1);
                //tb.Rows.Add("", 1);
                //tb.Rows.Add("", 1);
                //tb.Rows.Add("", 1);

                if (countState1 != 0)
                    tb2.Rows.Add((Math.Round(countState1 * 100 / (countState1 + countState2 + countState3), 2)).ToString()+"%", countState1);
                else
                    tb.Rows.Add("", 0);
                if (countState2 != 0)
                    tb2.Rows.Add((Math.Round(countState2 * 100 / (countState1 + countState2 + countState3), 2)).ToString() + "%", countState2);
                else
                    tb.Rows.Add("", 0);
                if (countState3 != 0)
                    tb2.Rows.Add((Math.Round(countState3 * 100 / (countState1 + countState2 + countState3), 2)).ToString() + "%", countState3);
                else
                    tb.Rows.Add("", 0);

                string[] array_colour = new string[]
               {
                   "#008000",
                   "#006680",
                   "#330080",
                   "#800033",
                   "#806600",
                   "#00FF00",
                   "#00CCFF",
                   "#6600FF",
                   "#FF0066",
                   "#FFCC00",
                   "#80FF80",
                   "#80E5FF",
                   "#B380FF",
                   "#FF80B3",
                   "#FFE680",
                   "#BFBFBF"
               };

                //查询回报代码
                DataTable dtReport = new DataTable();
                myconn.myopen();

                string strsql2 = " select cCode,cDesc from COD_CODE ";

                dtReport = myconn.mysearch(strsql2);
                dtReport.Rows.Add("A00", "無機台回報資訊(Không có thông tin báo cáo)");   

              
                myconn.myclose();

                dtReport.Columns.Add("code");
                dtReport.Columns.Add("percent");

                dtReport.Rows[0]["percent"] = (Math.Round(countA01 * 100 / count_total, 2)).ToString() + "%";
                dtReport.Rows[1]["percent"] = (Math.Round(countB01 * 100 / count_total, 2)).ToString() + "%";
                dtReport.Rows[2]["percent"] = (Math.Round(countC01 * 100 / count_total, 2)).ToString() + "%";
                dtReport.Rows[3]["percent"] = (Math.Round(countC02 * 100 / count_total, 2)).ToString() + "%";
                dtReport.Rows[4]["percent"] = (Math.Round(countC03 * 100 / count_total, 2)).ToString() + "%";
                dtReport.Rows[5]["percent"] = (Math.Round(countC04 * 100 / count_total, 2)).ToString() + "%";
                dtReport.Rows[6]["percent"] = (Math.Round(countC05 * 100 / count_total, 2)).ToString() + "%";
                dtReport.Rows[7]["percent"] = (Math.Round(countC06 * 100 / count_total, 2)).ToString() + "%";
                dtReport.Rows[8]["percent"] = (Math.Round(countC07 * 100 / count_total, 2)).ToString() + "%";
                dtReport.Rows[9]["percent"] = (Math.Round(countD01 * 100 / count_total, 2)).ToString() + "%";
                dtReport.Rows[10]["percent"] = (Math.Round(countD02 * 100 / count_total, 2)).ToString() + "%";
                dtReport.Rows[11]["percent"] = (Math.Round(countD03 * 100 / count_total, 2)).ToString() + "%";
                dtReport.Rows[12]["percent"] = (Math.Round(countD04 * 100 / count_total, 2)).ToString() + "%";
                dtReport.Rows[13]["percent"] = (Math.Round(countD05 * 100 / count_total, 2)).ToString() + "%";
                dtReport.Rows[14]["percent"] = (Math.Round(countD06 * 100 / count_total, 2)).ToString() + "%";
                dtReport.Rows[15]["percent"] = (Math.Round(count * 100 / count_total, 2)).ToString() + "%";

                for(int i=0;i< dtReport.Rows.Count;i++)
                {
                    dtReport.Rows[i]["code"] = "style='background:"+ array_colour[i] + ";'";
                }

                this.Repeater2.DataSource = dtReport;
                this.Repeater2.DataBind();

                //Set chart data source
                //Chart1.DataSource = tb;

                ////Set series members names for the X and Y values
                //Chart1.Series["Category"].XValueMember = "col1";
                //Chart1.Series["Category"].YValueMembers = "col2";

                ////Data bind to the selected data source
                //Chart1.DataBind();

                foreach (DataRow row in tb.Rows)
                {
                    Chart1.Series["Category"].Points.Add(Convert.ToInt32(row["col2"]));
                    Chart1.Series["Category"].Points[Chart1.Series["Category"].Points.Count - 1].Label = row["col1"].ToString();

                }

                for(int i=0;i<=15;i++)
                    Chart1.Series["Category"].Points[i].Color = System.Drawing.ColorTranslator.FromHtml(array_colour[i]);
               
                //name x,y
                Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                Chart1.ChartAreas["ChartArea1"].AxisX.Title = "Status";
                Chart1.ChartAreas["ChartArea1"].AxisY.Title = "Time(min)";

                //Chart2.DataSource = tb2;

                ////Set series members names for the X and Y values
                //Chart2.Series["Category"].XValueMember = "col1";
                //Chart2.Series["Category"].YValueMembers = "col2";

                //Chart2.Series["Category"].Points[Chart2.Series["Category"].Points.Count-1].Color = System.Drawing.Color.Green;
                ////Chart2.Series["Category"].Points[1].Color = System.Drawing.Color.Yellow;
                ////Chart2.Series["Category"].Points[2].Color = System.Drawing.Color.Red;
                ////Data bind to the selected data source
                //Chart2.DataBind();

                foreach (DataRow row in tb2.Rows)
                {
                    Chart2.Series["Category"].Points.Add(Convert.ToInt32(row["col2"]));
                    Chart2.Series["Category"].Points[Chart2.Series["Category"].Points.Count - 1].Label = row["col1"].ToString();
                    Chart2.Series["Category"].Points[Chart2.Series["Category"].Points.Count - 1].Url = "www.google.com";

                }

                Chart2.Series["Category"].Points[0].Color = System.Drawing.Color.Green;
                Chart2.Series["Category"].Points[1].Color = System.Drawing.Color.Yellow;
                Chart2.Series["Category"].Points[2].Color = System.Drawing.Color.Red;

                //name x,y
                
                Chart2.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                Chart2.ChartAreas["ChartArea1"].AxisX.Title = "Status";
                Chart2.ChartAreas["ChartArea1"].AxisY.Title = "Time(min)";
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
        private DataTable GetData(string pMaMay, string pDate1, string pDate2)
        {
            string strsql;
           

            DataTable dataByTime = new DataTable();
           


            myconn.myopen();
            //strsql = "select * from v_dtl_machine where cMachine = '" + pMaMay + "' and dTime > '" + pDate1 + "' and dTime< '" + pDate2 + "' order by dtime desc";
            strsql = " select *,a.[Count] ^ 66 as iCnt from PLC_VL a " +
                     " left join MST_STS b  on b.dStTime = (select top 1 dStTime from MST_STS where dStTime <= a.TTime and cMachine = a.MACHINE_CODE order by dStTime desc) " +
                     " and b.cMachine = a.MACHINE_CODE " +
                     " left join  COD_CODE c on b.cMachine = a.MACHINE_CODE and b.cStatus = c.cCode and c.cCat = 'MachineStatus' " +
                     " where a.MACHINE_CODE = '" + pMaMay + "' " +
                     " and a.TTime >= '" + pDate1 + "' and a.TTime <= '" + pDate2 + "' " +
                     " order by a.TTime desc";
            dataByTime = myconn.mysearch(strsql);

           

            myconn.myclose();

            return dataByTime;
        }
        protected void btnXem_Click(object sender, EventArgs e)
        {

          
            this.mamay.Text = hmamay.Value;

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