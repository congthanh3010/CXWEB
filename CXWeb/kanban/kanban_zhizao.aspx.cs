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

namespace CXWeb.kanban
{
    public partial class kanban_zhizao : System.Web.UI.Page
    {
        
        private static DataTable mydataCT1 = new DataTable();
        private static DataTable mydataCT2 = new DataTable();
        private static DataTable mydataCT3 = new DataTable();
        private static DataTable mydataCT1_quanjian = new DataTable();
        private static DataTable mydataCT2_quanjian = new DataTable();
        private static DataTable mydataCT3_quanjian = new DataTable();

        private static DataTable mydata2CT1 = new DataTable();
        private static DataTable mydata2CT2 = new DataTable();
        private static DataTable mydata2CT3 = new DataTable();

        private static DataTable mydata2CT1_quanjian = new DataTable();
        private static DataTable mydata2CT2_quanjian = new DataTable();
        private static DataTable mydata2CT3_quanjian = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            string unit_code = base.Request.QueryString["u"];

            if (!IsPostBack && unit_code != null)
            {

                h_unit.Value = unit_code;

                if (h_unit.Value == "2FN201") head_label.Text = "2FN201 製一組-T.C.T.1 (2FN201 Chế Tạo 1-T.C.T.1)";
                else if (h_unit.Value == "2FN202") head_label.Text = "2FN202 製二組-T.C.T.2 (2FN202 Chế Tạo 2-T.C.T.2)";
                else if (h_unit.Value == "2FN501") head_label.Text = "2FN501 製三組-T.C.T.3 (2FN501 Chế Tạo 3-T.C.T.3)";

                query();
                //Timer1.Enabled = true;


            }

        }
        void query()
        {
           

            string strsql = "";
            string strsql2 = "";
          

            DataTable mydata_setting = new DataTable();

            DataTable mydata = new DataTable();

            DataTable mydata2 = new DataTable();

            DataTable mydata3 = new DataTable();

            connectTT myconn = new connectTT();
            connectEIP myconn2 = new connectEIP();
            myconn.myopen();

            //查設定天數
            myconn2.myopen();
            mydata_setting = myconn2.mysearch("select * from kanban_setting_zhizao order by code");
            myconn2.myclose();
            string[] day_num = new string[mydata_setting.Rows.Count];

            for (int i=0;i< mydata_setting.Rows.Count;i++)
            {
                day_num[i] = mydata_setting.Rows[i]["day_number"].ToString();
            }

            GridView1.Columns[1].HeaderText = day_num[0] + "天以下</br>Dưới" + day_num[0] + "ngày";
            GridView1.Columns[2].HeaderText = day_num[1] + "天以下</br>Dưới" + day_num[1] + "ngày";
            GridView1.Columns[3].HeaderText = day_num[2] + "天以下</br>Dưới" + day_num[2] + "ngày";
            GridView1.Columns[4].HeaderText = day_num[3] + "天以下</br>Dưới" + day_num[3] + "ngày";
            GridView1.Columns[5].HeaderText = day_num[4] + "天以下</br>Dưới" + day_num[4] + "ngày";
            GridView1.Columns[6].HeaderText = day_num[4] + "天以上</br>Trên" + day_num[4] + "ngày";
            //第一段全部查詢

            strsql = "select shm_type,count(*) s ,to_char(sum(wipqty),'999,999,999') sl,to_char(round(sum(wipqty*ima18),0),'999,999,999') tl" +
                    " from(" +
                    " select v.shm012," +
                    " case when sl_day < "+ day_num[0] + " or sl_day is null then 1" +
                    " when sl_day >= " + day_num[0] + " and sl_day < " + day_num[1] + "  then 2" +
                    " when sl_day >= " + day_num[1] + " and sl_day < " + day_num[2] + "  then 3" +
                    " when sl_day >= " + day_num[2] + " and sl_day < " + day_num[3] + "  then 4" +
                    " when sl_day >= " + day_num[3] + " and sl_day < " + day_num[4] + "  then 5 " +
                    " else 6" +
                    " end shm_type," +
                    " wipqty, ima18" +
                    " from v_1104_dayclr_wo10 v" +
                    " left outer join ima_file on ima01 = shm05 and imaacti = 'Y'" +
                    " where gem01 = '"+ h_unit.Value + "')" +
                    " group by shm_type" +
                    " order by shm_type";
            mydata = myconn.mysearch(strsql);


            for(int i=0;i<7;i++)
            {
                mydata2.Columns.Add(i.ToString());
            }
            mydata2.Rows.Add("工單件數 Số kiện công đơn","","","","","","");
            mydata2.Rows.Add("數量 Số lượng", "", "", "", "", "", "");
            mydata2.Rows.Add("重量(KG) Trọng lượng(KG)", "", "", "", "", "", "");

            for (int i=0;i< mydata.Rows.Count;i++)
            {
                mydata2.Rows[0][mydata.Rows[i]["SHM_TYPE"].ToString()] = mydata.Rows[i]["S"].ToString();
                mydata2.Rows[1][mydata.Rows[i]["SHM_TYPE"].ToString()] = mydata.Rows[i]["SL"].ToString();
                mydata2.Rows[2][mydata.Rows[i]["SHM_TYPE"].ToString()] = mydata.Rows[i]["TL"].ToString();
            }


            if (h_unit.Value == "2FN201")
            {
                mydata2CT1.Reset();
                mydata2CT1 = mydata2.Copy();
                GridView1.DataSource = mydata2CT1;
            }
            if (h_unit.Value == "2FN202")
            {
                mydata2CT2.Reset();
                mydata2CT2 = mydata2.Copy();
                GridView1.DataSource = mydata2CT2;
            }
            if (h_unit.Value == "2FN501")
            {
                mydata2CT3.Reset();
                mydata2CT3 = mydata2.Copy();
                GridView1.DataSource = mydata2CT3;
            }

            //GridView1.DataSource = mydata2;
            GridView1.DataBind();

            //GridViewRow row = new GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal);            
            //TableCell cell = new TableHeaderCell();
            //cell.ColumnSpan = 7;
            //if (unit == "2FN201") cell.Text = "2FN201 製一組-T.C.T.1";
            //else if (unit == "2FN202") cell.Text = "2FN202 製二組-T.C.T.2";
            //else if (unit == "2FN501") cell.Text = "2FN501 製三組T.C.T.3";
            //row.Cells.Add(cell);
            //((Table)GridView1.Controls[0]).Rows.AddAt(0, row);


            //第一段查詢全檢

            mydata.Reset();

            strsql = "select shm_type,count(*) s ,to_char(sum(wipqty),'999,999,999') sl,to_char(round(sum(wipqty*ima18),0),'999,999,999') tl" +
               " from(" +
               " select v.shm012," +
               " case when sl_day < " + day_num[0] + " or sl_day is null then 1" +
               " when sl_day >= " + day_num[0] + " and sl_day < " + day_num[1] + "  then 2" +
               " when sl_day >= " + day_num[1] + " and sl_day < " + day_num[2] + "  then 3" +
               " when sl_day >= " + day_num[2] + " and sl_day < " + day_num[3] + "  then 4" +
               " when sl_day >= " + day_num[3] + " and sl_day < " + day_num[4] + "  then 5 " +
               " else 6" +
               " end shm_type," +
               " wipqty, ima18" +
               " from v_1104_dayclr_wo10 v" +
               " left outer join ima_file on ima01 = shm05 and imaacti = 'Y'" +
               " where gem01 = '" + h_unit.Value + "' and sgm04 like 'DD%')" +
               " group by shm_type" +
               " order by shm_type";
            mydata = myconn.mysearch(strsql);

            mydata2.Reset();
            for (int i = 0; i < 7; i++)
            {
                mydata2.Columns.Add(i.ToString());
            }
            mydata2.Rows.Add("工單件數 Số kiện công đơn", "", "", "", "", "", "");
            mydata2.Rows.Add("數量 Số lượng", "", "", "", "", "", "");
            mydata2.Rows.Add("重量(KG) Trọng lượng(KG)", "", "", "", "", "", "");

            for (int i = 0; i < mydata.Rows.Count; i++)
            {
                mydata2.Rows[0][mydata.Rows[i]["SHM_TYPE"].ToString()] = mydata.Rows[i]["S"].ToString();
                mydata2.Rows[1][mydata.Rows[i]["SHM_TYPE"].ToString()] = mydata.Rows[i]["SL"].ToString();
                mydata2.Rows[2][mydata.Rows[i]["SHM_TYPE"].ToString()] = mydata.Rows[i]["TL"].ToString();
            }


            if (h_unit.Value == "2FN201")
            {
                mydata2CT1_quanjian.Reset();
                mydata2CT1_quanjian = mydata2.Copy();
             
            }
            if (h_unit.Value == "2FN202")
            {
                mydata2CT2_quanjian.Reset();
                mydata2CT2_quanjian = mydata2.Copy();
               
            }
            if (h_unit.Value == "2FN501")
            {
                mydata2CT3_quanjian.Reset();
                mydata2CT3_quanjian = mydata2.Copy();
               
            }

            //第二段-------------------------------------------------------------------

            strsql2 = "select distinct v.shm012,shm05,sgm03,sgm04,sgm45,sgm06,to_char(shb03,'yyyy/MM/dd') shb03,to_char(wipqty,'999,999,999') wipqty,NVL(sl_day,0) sl_day,to_char(sgm301,'999,999,999') sgm301,to_char(round(sgm311,0),'999,999,999') sgm311,to_char(round(sgm313,0),'999,999,999') sgm313, " +
                    " case when u.tc_jhd01 is null then 'N' " +
                    " else 'Y' end is_plan " +
                    " from v_1104_dayclr_wo10 v " +
                    " left outer join " +
                    " (select * from tc_jhd_file where tc_jhd01 in (select max(tc_jhd01) from tc_jhd_file)) u " +
                    " on tc_jhd04 = shm05 and tc_jhd05 = sgm04 " +
                    " where gem01 = '"+ h_unit.Value + "'"+
                    " order by sl_day desc";

            mydata3 = myconn.mysearch(strsql2);
            if (h_unit.Value == "2FN201")
            {               
               
                mydataCT1.Reset();
                mydataCT1 = mydata3.Copy();
                GridView2.DataSource = mydataCT1;

                mydataCT1_quanjian.Reset();
                mydataCT1_quanjian = mydataCT1.Clone();
                foreach (DataRow row in mydataCT1.Rows)
                {
                    if (row["SGM04"].ToString().Substring(0,2) == "DD")
                    {
                        mydataCT1_quanjian.ImportRow(row);
                    }
                }

            }
            if (h_unit.Value == "2FN202")
            {
                mydataCT2.Reset();
                mydataCT2 = mydata3.Copy();
                GridView2.DataSource = mydataCT2;

                mydataCT2_quanjian.Reset();
                mydataCT2_quanjian = mydataCT2.Clone();
                foreach (DataRow row in mydataCT2.Rows)
                {
                    if (row["SGM04"].ToString().Substring(0, 2) == "DD")
                    {
                        mydataCT2_quanjian.ImportRow(row);
                    }
                }
            }
            if (h_unit.Value == "2FN501")
            {
                mydataCT3.Reset();
                mydataCT3 = mydata3.Copy();
                GridView2.DataSource = mydataCT3;

                mydataCT3_quanjian.Reset();
                mydataCT3_quanjian = mydataCT3.Clone();
                foreach (DataRow row in mydataCT3.Rows)
                {
                    if (row["SGM04"].ToString().Substring(0, 2) == "DD")
                    {
                        mydataCT3_quanjian.ImportRow(row);
                    }
                }
            }





            GridView2.DataBind();

            Timer1.Enabled = true;

            try
            {
                myconn.myclose();
            }
            catch
            {
                return;
            }

            
        }

        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {

       

        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            if (GridView2.PageIndex == GridView2.PageCount - 1)
            {
                //GridView2.PageIndex = 0;
                Response.Redirect($"/kanban/kanban_zhizao3.aspx?page=kanban_zhizao&u={h_unit.Value}");
            }
            else
            {
                GridView2.PageIndex = GridView2.PageIndex + 1;
                if (h_unit.Value == "2FN201")
                {
                    if (rdoButton1.Checked)
                    {
                        GridView1.DataSource = mydata2CT1;
                        GridView2.DataSource = mydataCT1;
                    }
                    if (rdoButton2.Checked)
                    {
                        GridView1.DataSource = mydata2CT1_quanjian;
                        GridView2.DataSource = mydataCT1_quanjian;
                    }


                }
                if (h_unit.Value == "2FN202")
                {
                    if (rdoButton1.Checked)
                    {
                        GridView1.DataSource = mydata2CT2;
                        GridView2.DataSource = mydataCT2;
                    }
                    if (rdoButton2.Checked)
                    {
                        GridView1.DataSource = mydata2CT2_quanjian;
                        GridView2.DataSource = mydataCT2_quanjian;

                    }
                }
                if (h_unit.Value == "2FN501")
                {
                    if (rdoButton1.Checked)
                    {
                        GridView1.DataSource = mydata2CT3;
                        GridView2.DataSource = mydataCT3;
                    }
                    if (rdoButton2.Checked)
                    {
                        GridView1.DataSource = mydata2CT3_quanjian;
                        GridView2.DataSource = mydataCT3_quanjian;
                    }
                }
                GridView1.DataBind();
                GridView2.DataBind();
            }

            //GridViewRow row = new GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal);
            //TableCell cell = new TableHeaderCell();
            //cell.ColumnSpan = 7;
            //if (h_unit.Value == "2FN201") cell.Text = "2FN201 製一組-T.C.T.1";
            //else if (h_unit.Value == "2FN202") cell.Text = "2FN202 製二組-T.C.T.2";
            //else if (h_unit.Value == "2FN501") cell.Text = "2FN501 製三組T.C.T.3";
            //row.Cells.Add(cell);
            //((Table)GridView1.Controls[0]).Rows.AddAt(0, row);
        }

        protected void Group1_CheckedChanged(Object sender, EventArgs e)
        {
            GridView2.PageIndex = 0;
            if (h_unit.Value == "2FN201")
            {
                if (rdoButton1.Checked)
                {
                    GridView1.DataSource = mydata2CT1;
                    GridView2.DataSource = mydataCT1;
                }
                if (rdoButton2.Checked)
                {
                    GridView1.DataSource = mydata2CT1_quanjian;
                    GridView2.DataSource = mydataCT1_quanjian;
                }


            }
            if (h_unit.Value == "2FN202")
            {
                if (rdoButton1.Checked)
                {
                    GridView1.DataSource = mydata2CT2;
                    GridView2.DataSource = mydataCT2;
                }
                if (rdoButton2.Checked)
                {
                    GridView1.DataSource = mydata2CT2_quanjian;
                    GridView2.DataSource = mydataCT2_quanjian;

                }
            }
            if (h_unit.Value == "2FN501")
            {
                if (rdoButton1.Checked)
                {
                    GridView1.DataSource = mydata2CT3;
                    GridView2.DataSource = mydataCT3;
                }
                if (rdoButton2.Checked)
                {
                    GridView1.DataSource = mydata2CT3_quanjian;
                    GridView2.DataSource = mydataCT3_quanjian;
                }
            }
            GridView1.DataBind();
            GridView2.DataBind();
            //Timer1.Enabled = true;


            //ThreadStart childthreat1 = new ThreadStart(timer_stop);
            //Thread child1 = new Thread(childthreat1);


            //ThreadStart childthreat2 = new ThreadStart(query);
            //Thread child2 = new Thread(childthreat2);

            //child1.Start();
            //child1.Join();


            //query();
            //child2.Start();



        }
        void timer_stop()
        {
            Timer1.Enabled = false;
        }

    }
   
}