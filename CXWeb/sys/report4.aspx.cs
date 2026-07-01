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
    public partial class report4 : System.Web.UI.Page
    {
        connectTT myconn = new connectTT();       
        protected string myDate = "2017-11-02";
        protected string date_from, date_to;
        protected DateTime pDate;
        protected string tg_code="";
        protected string list_of_fix_mold_order;
        protected string rent_mold_order;
        protected void Page_Load(object sender, EventArgs e)
        {
            Chart1.Series[0].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Pie");
            Chart2.Series[0].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Pie");
           

            DateTime pDate = new DateTime(DateTime.Now.AddDays(-1).Year, DateTime.Now.AddDays(-1).Month, DateTime.Now.AddDays(-1).Day, 6, 0, 0);
            DateTime pDate2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 6, 0, 0);      
       


            //myconn.myopen();

            //string strsql = "  select * from [PLC_PLCM_CX] order by p_kind asc, clocate asc, cmachine asc ";

            //mcTable = myconn.mysearch(strsql);         

            //myconn.myclose();

            tg_code = base.Request.QueryString["tg"];

            if (!IsPostBack)
            {
             
                if(tg_code==null)
                {
                    this.date1.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    this.date2.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    this.Session["report1_hdate1"] = date1.Text;
                    this.Session["report1_hdate2"] = date2.Text;
                }
                else
                {
                    vender_name.Text = tg_code;
                    try
                    {
                        date1.Text = this.Session["report1_hdate1"].ToString();
                        date2.Text = this.Session["report1_hdate2"].ToString();
                    }
                    catch
                    {
                        this.date1.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        this.date2.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        this.Session["report1_hdate1"] = date1.Text;
                        this.Session["report1_hdate2"] = date2.Text;
                    }
                }
              

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

                this.time.Text = text1.ToString("yyyy/MM/dd") + " - "+ text2.ToString("yyyy/MM/dd");

                //DataTable tbTempt = new DataTable();

                //tbTempt = this.GetData("1", text1.ToString("yyyy/MM/dd"),text2.ToString("yyyy/MM/dd"));
                DataTable tbTempt11 = new DataTable();

                tbTempt11 = this.GetData("11", text1.ToString("yyyy/MM/dd"), text2.ToString("yyyy/MM/dd"));
                DataTable tbTempt12 = new DataTable();

                tbTempt12 = this.GetData("12", text1.ToString("yyyy/MM/dd"), text2.ToString("yyyy/MM/dd"));

                DataTable tb = new DataTable();
                tb.Columns.Add("col1");
                tb.Columns.Add("col2");
                tb.Columns.Add("code");
                tb.Columns.Add("percent");

                double good_product_wt = 0;
                double badMo_product_wt = 0;
                double bad_product_wt = 0;


                good_product_wt = Convert.ToDouble(tbTempt11.Rows[0]["ta_shb001"]);
                badMo_product_wt = Convert.ToDouble(tbTempt12.Rows[0]["shbud07"]);
                bad_product_wt = Convert.ToDouble(tbTempt11.Rows[0]["shbud07"])- badMo_product_wt;
                //for (int i =0;i< tbTempt.Rows.Count;i++)
                //{
                //    good_product_wt += Convert.ToDouble(tbTempt.Rows[i]["ta_shb001"]);
                //    if(tbTempt.Rows[i]["tc_moe01"].ToString().Trim()=="2")
                //    {
                //        badMo_product_wt+= Convert.ToDouble(tbTempt.Rows[i]["shbud07"]);

                //    }
                //    else
                //    {
                //        bad_product_wt += Convert.ToDouble(tbTempt.Rows[i]["shbud07"]);
                //    }                   
                //}


                tb.Rows.Add("良品重量(Kg)", good_product_wt, "style='background:Green;'",Math.Round(good_product_wt*100/(good_product_wt+ badMo_product_wt+ bad_product_wt),2)+"%");
                tb.Rows.Add("其它原因报废(Kg)", bad_product_wt , "style='background:Yellow;'", Math.Round(bad_product_wt * 100 / (good_product_wt + badMo_product_wt + bad_product_wt),2) + "%");
                tb.Rows.Add("模具不良报废(Kg)", badMo_product_wt , "style='background:Red;'", Math.Round(badMo_product_wt * 100 / (good_product_wt + badMo_product_wt + bad_product_wt),2) + "%");
               

                this.Repeater2.DataSource = tb;
                this.Repeater2.DataBind();

            

                foreach (DataRow row in tb.Rows)
                {
                    //Chart1.Series["Category"].Points.Add(Convert.ToDouble(row["col2"]));
                    //Chart1.Series["Category"].Points[Chart1.Series["Category"].Points.Count - 1].Label = row["col1"].ToString();
                    Chart1.Series["Category"].Points.Add();                 
                    Chart1.Series["Category"].Points[Chart1.Series["Category"].Points.Count - 1].SetValueXY("", Convert.ToDouble(row["col2"]));
                    //Chart1.Series["Category"].Points[Chart1.Series["Category"].Points.Count - 1].Url = "report1.aspx?vd="+ row["col1"].ToString()+"#vender";
                }
                Chart1.Series["Category"].Points[0].Color = System.Drawing.Color.Green;
                Chart1.Series["Category"].Points[1].Color = System.Drawing.Color.Yellow;
                Chart1.Series["Category"].Points[2].Color = System.Drawing.Color.Red;

                //for(int i=0;i<=15;i++)
                //    Chart1.Series["Category"].Points[i].Color = System.Drawing.ColorTranslator.FromHtml(array_colour[i]);

                //name x,y
                Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                Chart1.ChartAreas["ChartArea1"].AxisX.Title = "";
                Chart1.ChartAreas["ChartArea1"].AxisY.Title = "";

                DataTable tbTempt2 = new DataTable();

                tbTempt2 = this.GetData("21", text1.ToString("yyyy/MM/dd"), text2.ToString("yyyy/MM/dd"));

                DataTable tb2 = new DataTable();
                tb2.Columns.Add("col1");
                tb2.Columns.Add("col2");

                DataTable tb21 = new DataTable();
                tb21.Columns.Add("col1");
                tb21.Columns.Add("col2");
                tb21.Columns.Add("url");

                double tol_wt2 = 0;
                double other_wt2 = 0;
                int show_num = 10;
                for (int i = 0; i < tbTempt2.Rows.Count; i++)
                {
                    if(i< show_num)
                        tb2.Rows.Add(tbTempt2.Rows[i]["shb05"].ToString(), tbTempt2.Rows[i]["shbud07"].ToString());                    
                    else
                        other_wt2 += Convert.ToDouble(tbTempt2.Rows[i]["shbud07"]);
                    tol_wt2 += Convert.ToDouble(tbTempt2.Rows[i]["shbud07"]);

                    tb21.Rows.Add(tbTempt2.Rows[i]["shb05"].ToString(), tbTempt2.Rows[i]["shbud07"].ToString(), "report4.aspx?tg=" + tbTempt2.Rows[i]["shb05"].ToString() + "#target");
                 

                }



                tb21.Rows.Add("Total", tol_wt2, "report4.aspx");
                tb2.Rows.Add("其它工单", other_wt2);
                foreach (DataRow row in tb2.Rows)
                {
                    Chart2.Series["Category"].Points.Add();

                    //if(Convert.ToDouble(row["col2"])>= tol_wt2/20)
                    //{
                    //    Chart2.Series["Category"].Points[Chart2.Series["Category"].Points.Count - 1].SetValueXY(row["col2"], Convert.ToDouble(row["col2"]));
                    //}
                    //else
                    //{
                    //    Chart2.Series["Category"].Points[Chart2.Series["Category"].Points.Count - 1].SetValueXY("", Convert.ToDouble(row["col2"]));
                    //}
                    if (Chart2.Series["Category"].Points.Count <= show_num)
                    {
                        if (Convert.ToDouble(row["col2"]) >= tol_wt2 / 30)
                        {
                            Chart2.Series["Category"].Points[Chart2.Series["Category"].Points.Count - 1].SetValueXY(row["col2"], Convert.ToDouble(row["col2"]));
                        }
                        else
                        {
                            Chart2.Series["Category"].Points[Chart2.Series["Category"].Points.Count - 1].SetValueXY("", Convert.ToDouble(row["col2"]));
                        }
                        Chart2.Series["Category"].Points[Chart2.Series["Category"].Points.Count - 1].Url = "report4.aspx?tg=" + row["col1"].ToString() + "#target";
                    }
                    else
                    {
                        Chart2.Series["Category"].Points[Chart2.Series["Category"].Points.Count - 1].SetValueXY(row["col1"]+":"+ row["col2"], Convert.ToDouble(row["col2"]));
                    }
                }

                //Chart2.Series["Category"].Points[0].Color = System.Drawing.Color.Green;
                //Chart2.Series["Category"].Points[1].Color = System.Drawing.Color.Yellow;
                //Chart2.Series["Category"].Points[2].Color = System.Drawing.Color.Red;

                //name x,y

                Chart2.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                Chart2.ChartAreas["ChartArea1"].AxisX.Title = "";
                Chart2.ChartAreas["ChartArea1"].AxisY.Title = "";

                this.Repeater3.DataSource = tb21;
                this.Repeater3.DataBind();

                if (tg_code != null)
                {
                 
                    DataTable tbTempt3 = new DataTable();

                    tbTempt3 = this.GetData("41", text1.ToString("yyyy/MM/dd"), text2.ToString("yyyy/MM/dd"));

                    //DataTable tb3 = new DataTable();
                    //tb3.Columns.Add("col1");
                    //tb3.Columns.Add("col2");

                    //for (int i = 0; i < tbTempt3.Rows.Count; i++)
                    //{
                       
                    //   tb3.Rows.Add(tbTempt3.Rows[i]["tc_moe01"].ToString(), tbTempt3.Rows[i]["shbud07"].ToString());
                       
                    //}



                    //foreach (DataRow row in tb3.Rows)
                    //{
                    //    Chart3.Series["Category"].Points.Add();

                    //    Chart3.Series["Category"].Points[Chart3.Series["Category"].Points.Count - 1].SetValueXY(row["col1"].ToString(), Convert.ToDouble(row["col2"]));
                        
                    //}

              


                    this.Repeater4.DataSource = tbTempt3;
                    this.Repeater4.DataBind();
                }


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
        private DataTable GetData(string code,string pDate1,string pDate2)
        {
            string strsql;    
            DataTable dataByTime = new DataTable();
            myconn.myopen();
            if (code == "1")
                strsql = "  select sum(ta_shb001) as ta_shb001,sum(shbud07) as shbud07,tc_moe01 " +
                         " from(select a.ta_shb001, a.shbud07, decode(b.tc_moe01, null, '1', '2') as tc_moe01 from shb_file a " +
                         " left join tc_moe_file b on b.tc_moe06 = a.shbud02 and b.tc_moeconf = 'Y'  and b.tc_moe01 in "+
                         " (select tc_moe01 from tc_moe_file where tc_moe06 = a.shbud02 and tc_moeconf = 'Y' and rownum = 1)"+
                         " where a.shb02>=TO_DATE('" + pDate1 + "','YYYY-MM-DD') and a.shb02<=TO_DATE('" + pDate2 + "','YYYY-MM-DD') and a.shbconf = 'Y' and a.shbud02 <> 'null' and a.shbud07 <> 0 )group by tc_moe01 ";
            else if (code == "2")
                strsql = "  select tc_moe03,sum(shbud07) as shbud07 from ( select a.shb01,a.shb05,a.shbud02,a.ta_shb001,a.shbud07,b.tc_moe01,b.tc_moe03 from shb_file a " +
                          " left join tc_moe_file b on b.tc_moe06 = a.shbud02 and b.tc_moeconf = 'Y' " +
                          " where a.shb02>=TO_DATE('" + pDate1 + "','YYYY-MM-DD') and a.shb02<=TO_DATE('" + pDate2 + "','YYYY-MM-DD') and a.shbconf = 'Y' and a.shbud02 <> 'null' and a.shbud07 <> 0 order by a.shb05)"+
                          " where tc_moe03<> 'null' group by tc_moe03  order by tc_moe03";
            else if (code == "3")
                strsql = " select a.shb01,a.shb05,a.shbud02,a.ta_shb001,a.shbud07,b.tc_moe01,b.tc_moe03 from shb_file a" +
                         " left join tc_moe_file b on b.tc_moe06 = a.shbud02 and b.tc_moeconf = 'Y'" +
                         " where  a.shb02>=TO_DATE('" + pDate1 + "','YYYY-MM-DD') and a.shb02<=TO_DATE('" + pDate2 + "','YYYY-MM-DD')  and a.shbconf = 'Y' and a.shbud02 <> 'null' and a.shbud07 <> 0" +
                         " and a.shbud02 = '" + tg_code + "'";

            else if (code == "4")
                strsql = "  select n.tc_moeb01,n.tc_moeb02,n.tc_moeb03,u.tc_moa02,u.tc_moa03,n.tc_moeb05,v.tc_mod02,n.tc_moeb06,v.tc_mod04 from tc_moeb_file n,tc_moa_file u,tc_mod_file v" +
                         " where n.tc_moeb03 = u.tc_moa01 and n.tc_moeb05 = v.tc_mod01 and n.tc_moeb06 = v.tc_mod03 "+
                         " and n.tc_moeb01 in  "+
                         " (select b.tc_moe01 from shb_file a "+
                         " left join tc_moe_file b on b.tc_moe06 = a.shbud02 and b.tc_moeconf = 'Y'"+
                         " where a.shb02>=TO_DATE('" + pDate1 + "','YYYY-MM-DD') and a.shb02<=TO_DATE('" + pDate2 + "','YYYY-MM-DD') and a.shbconf = 'Y' and a.shbud02 <> 'null' and a.shbud07 <> 0  and b.tc_moe01 <> 'null'" +
                         " and a.shbud02 = '" + tg_code + "')";
            else if (code == "11")
                strsql = " select  sum(ta_shb001) as ta_shb001,sum(shbud07) as shbud07 from shb_file a "+
                         " where a.shb02>=TO_DATE('" + pDate1 + "','YYYY-MM-DD') and a.shb02<=TO_DATE('" + pDate2 + "','YYYY-MM-DD')  and a.shbconf = 'Y' and a.shbud02 <> 'null' and a.shbud07 <> 0 ";
            else if (code == "12")
                strsql = "  select sum(a.shbud07) as shbud07 from shb_file a " +
                         " ,tc_moe_file b where b.tc_moe06 = a.shbud02 and b.tc_moeconf = 'Y' and "+
                         " a.shb02>=TO_DATE('" + pDate1 + "','YYYY-MM-DD') and a.shb02<=TO_DATE('" + pDate2 + "','YYYY-MM-DD')  and a.shbconf = 'Y' and a.shbud02 <> 'null' and a.shbud07 <> 0" +
                         " and b.tc_moe01 in (select tc_moe01 from tc_moe_file where tc_moe06 = a.shbud02 and tc_moeconf = 'Y' and rownum = 1)";
            else if (code == "21")
                strsql = "  select a.shb05,sum(a.shbud07) as shbud07 from shb_file a " +
                         " ,tc_moe_file b where b.tc_moe06 = a.shbud02 and b.tc_moeconf = 'Y' and"+
                         " a.shb02>=TO_DATE('" + pDate1 + "','YYYY-MM-DD') and a.shb02<=TO_DATE('" + pDate2 + "','YYYY-MM-DD') and a.shbconf = 'Y' and a.shbud02 <> 'null' and a.shbud07 <> 0" +
                         " and b.tc_moe01 in (select tc_moe01 from tc_moe_file where tc_moe06 = a.shbud02 and tc_moeconf = 'Y' and rownum = 1)"+
                         " group by a.shb05 order by shbud07 desc ";
            else if (code == "41")
                strsql = "   select n.tc_moeb01,n.tc_moeb02,n.tc_moeb03,u.tc_moa02,u.tc_moa03,n.tc_moeb05,v.tc_mod02,n.tc_moeb06,v.tc_mod04 "+
                         " from tc_moe_file b, tc_moeb_file n, tc_moa_file u,tc_mod_file v "+
                         " where n.tc_moeb03 = u.tc_moa01 and n.tc_moeb05 = v.tc_mod01 and n.tc_moeb06 = v.tc_mod03 and b.tc_moe06 in "+
                         " (select distinct(a.shbud02) from shb_file a where  a.shb02>=TO_DATE('" + pDate1 + "','YYYY-MM-DD') and a.shb02<=TO_DATE('" + pDate2 + "','YYYY-MM-DD') and a.shbconf = 'Y' and a.shbud02 <> 'null' and a.shbud07 <> 0 and a.shb05 = '" + tg_code + "') " +
                         " and b.tc_moe01 = n.tc_moeb01 "+
                         " and n.tc_moeb02 <> 'null' and b.tc_moeconf = 'Y'  and b.tc_moe01 <> 'null'"+
                         " order by n.tc_moeb01 ";
            else strsql = "";
            dataByTime = myconn.mysearch(strsql);
            myconn.myclose();
            return dataByTime;
        }
        protected void btnXem_Click(object sender, EventArgs e)
        {
            this.Session["report1_hdate1"] = date1.Text;
            this.Session["report1_hdate2"] = date2.Text;
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