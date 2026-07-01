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



namespace CXWeb.checklist
{
    public partial class checklist_page : System.Web.UI.Page
    {
        connectEIP myconn = new connectEIP();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                
                DataTable mydata = new DataTable();
                string strsql = "select m.col_name,n.description from eped54 m,cod_code n"+
                    " where m.table_code = 'checklist' and n.class='checklist_table' and m.col_name=n.code"+
                    " order by m.col_no";
                myconn.myopen();
                mydata = myconn.mysearch(strsql);
                myconn.myclose();

                for(int i=0;i< mydata.Rows.Count;i++)
                {
                    GridView1.Columns[i].HeaderText = mydata.Rows[i]["description"].ToString();
                }
              
                query();
            }
           
        }
       
        private void BindData()
        {
            string unit = "CT1";
            string date;
            string shift;

            if (DateTime.Now.Hour >= 18)
            {
                shift = "CA2";
                date = DateTime.Now.ToString("yyyy/MM/dd");
            }
            else if (DateTime.Now.Hour < 6)
            {
                shift = "CA2";
                date = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");
            }
            else
            {
                shift = "CA1";
                date = DateTime.Now.ToString("yyyy/MM/dd");
            }
            // tim du lieu trong checklist
            DataTable mydata = new DataTable();
            
            string strsql;
            strsql = "select * from checklist_table where don_vi='"+ unit + "' and thoi_gian ='"+ date+"' and ca='"+ shift+"'"+
                " order by ten_may";
            myconn.myopen();
            mydata = myconn.mysearch(strsql);            
           


            if(mydata.Rows.Count==0)
            {
               
                string strsql2 = "select * from checklist_unit_setting where unit ='"+ unit + "' order by number ";
                string inssql = "";
                DataTable unit_setting_data = new DataTable();
                unit_setting_data= myconn.mysearch(strsql2);
                for(int i = 0;i< unit_setting_data.Rows.Count;i++)
                {
                    if(shift=="CA1")
                        inssql = inssql + "insert into checklist_table (don_vi,thoi_gian,ca,ten_may,thao_tac_vien) values" +
                        " ('"+ unit + "','"+ date + "','"+ shift + "','"+ unit_setting_data.Rows[i]["machine_no"].ToString()+ "','" + unit_setting_data.Rows[i]["ca1_msnv"].ToString() +"');";
                    else
                        inssql = inssql + "insert into checklist_table (don_vi,thoi_gian,ca,ten_may,thao_tac_vien) values" +
                      " ('" + unit + "','" + date + "','" + shift + "','" + unit_setting_data.Rows[i]["machine_no"].ToString() + "','" + unit_setting_data.Rows[i]["ca2_msnv"].ToString() + "');";
                }
                myconn.mySqlExecute(inssql);
                mydata = myconn.mysearch(strsql);
            }           

            

            myconn.myclose();

            GridView1.DataSource = mydata;
            GridView1.DataBind();
        }
        private void query()
        {
            BindData();
        }
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            query();
        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            query();
        }
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string unit = "CT1";
            string date;
            string shift;

            if (DateTime.Now.Hour >= 18)
            {
                shift = "CA2";
                date = DateTime.Now.ToString("yyyy/MM/dd");
            }
            else if (DateTime.Now.Hour < 6)
            {
                shift = "CA2";
                date = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");
            }
            else
            {
                shift = "CA1";
                date = DateTime.Now.ToString("yyyy/MM/dd");
            }

            string ten_may = ((Label)GridView1.Rows[e.RowIndex].FindControl("lblten_may")).Text;
            string quality01 = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtquality01")).Text;
            string quality02 = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtquality02")).Text;
            string quality03 = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtquality03")).Text;
            string quality04 = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtquality04")).Text;
            string s01 = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txts01")).Text;
            string s02 = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txts02")).Text;
            string s03 = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txts03")).Text;
            string s04 = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txts04")).Text;
            string s05 = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txts05")).Text;
            string s06 = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txts06")).Text;
            string s07 = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txts07")).Text;
            string s08 = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txts08")).Text;
            string security01 = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtsecurity01")).Text;
            string security02 = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtsecurity02")).Text;
            string security03 = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtsecurity03")).Text;
            string other01 = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtother01")).Text;

            if (quality01.Trim() == "") quality01 = "0";
            if (quality02.Trim() == "") quality02 = "0";
            if (quality03.Trim() == "") quality03 = "0";
            if (quality04.Trim() == "") quality04 = "0";
            if (s01.Trim() == "") s01 = "0";
            if (s02.Trim() == "") s02 = "0";
            if (s03.Trim() == "") s03 = "0";
            if (s04.Trim() == "") s04 = "0";
            if (s05.Trim() == "") s05 = "0";
            if (s06.Trim() == "") s06 = "0";
            if (s07.Trim() == "") s07 = "0";
            if (s08.Trim() == "") s08 = "0";
            if (security01.Trim() == "") security01 = "0";
            if (security02.Trim() == "") security02 = "0";
            if (security03.Trim() == "") security03 = "0";
            if (other01.Trim() == "") other01 = "0";
            Decimal sum_point = 0;
            try
            {
                sum_point = Convert.ToDecimal(quality01) + Convert.ToDecimal(quality02) + Convert.ToDecimal(quality03)
                    + Convert.ToDecimal(quality04) + Convert.ToDecimal(s01) + Convert.ToDecimal(s02)
                    + Convert.ToDecimal(s03) + Convert.ToDecimal(s04) + Convert.ToDecimal(s05)
                    + Convert.ToDecimal(s06) + Convert.ToDecimal(s07) + Convert.ToDecimal(s08)
                    + Convert.ToDecimal(security01) + Convert.ToDecimal(security02) + Convert.ToDecimal(security03)
                     + Convert.ToDecimal(other01);
            }
            catch
            {
                showMessage("Nhập điểm sai !");
                return;
            }


            string strsql = "update checklist_table set quality01 ='" + quality01 + "',quality02 ='"
                + quality02 + "',quality03 ='" + quality03 + "',quality04 ='" + quality04 
                + "',s01='"+ s01+"',s02='"+ s02+"',s03='"+ s03+"',s04='"+ s04+"',s05='"+ s05+"',s06='"+ s06
                +"',s07='"+ s07+"',s08='"+ s08+"',security01='"+ security01+"',security02='"+ security02
                +"',security03='"+ security03+"',other01='"+ other01+"',sum_point='"+ sum_point
                +"' where don_vi='"+unit+"' and thoi_gian='"+date+"' and ca='"+shift+"' and ten_may='"+ ten_may+"'   ";
            myconn.myopen();
            myconn.mySqlExecute(strsql);

            myconn.myclose();

            GridView1.EditIndex = -1;
            query();


        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }
        public void showMessage(string mess)
        {
            string strBuilder = "<script language='javascript'>alert('" + mess + "')</script>";
            Response.Write(strBuilder);
        }
    }
}