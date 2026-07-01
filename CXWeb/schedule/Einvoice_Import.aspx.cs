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
using System.Windows.Forms;

namespace CXWeb.schedule
{
    public partial class Einvoice_Import : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                confirm_date.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");
                End_Date.Text = DateTime.Now.ToString("yyyy/MM/dd");
                txtWeek.Text = "0";
                //query();
            }
        }
        
        void query()
        {


            connectEinvoice myconn_SQL = new connectEinvoice();


            //-------------------------------------------------------------

            DataSet mydata = new DataSet();
            

            System.Collections.Hashtable htPara = new System.Collections.Hashtable();
            
            htPara["BEGIN_DATE"] = confirm_date.Text;
            htPara["END_DATE"] = End_Date.Text;
            htPara["TY_GIA"] = txtWeek.Text;


            mydata = myconn_SQL.ExecuteReturnDs("sp_GetEinvoice", htPara, CommandType.StoredProcedure);

            myconn_SQL.myclose();

            if (mydata.Tables[0].Rows.Count > 0)
                hreport_msg.Value = "Xem Thành công !!!";
            else
                hreport_msg.Value = "Xem không thành công !!!";

            GridView2.DataSource = mydata.Tables[0];
            GridView2.DataBind();

            GridView1.DataSource = mydata.Tables[1];
            GridView1.DataBind();
        }

        void query_Xem()
        {


            connectEinvoice myconn_SQL = new connectEinvoice();


            //-------------------------------------------------------------

            DataSet mydata = new DataSet();


            System.Collections.Hashtable htPara = new System.Collections.Hashtable();

            htPara["BEGIN_DATE"] = confirm_date.Text;
            htPara["END_DATE"] = End_Date.Text;
            htPara["TY_GIA"] = txtWeek.Text;


            mydata = myconn_SQL.ExecuteReturnDs("sp_GetEinvoice_View", htPara, CommandType.StoredProcedure);

            myconn_SQL.myclose();

            if (mydata.Tables[0].Rows.Count > 0)
                hreport_msg.Value = "Import Thành công !!!";
            else
                hreport_msg.Value = "Import không thành công !!!";

            GridView2.DataSource = mydata.Tables[0];
            GridView2.DataBind();

            GridView1.DataSource = mydata.Tables[1];
            GridView1.DataBind();
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
            

        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
        }
        protected void btnIns_Click(object sender, EventArgs e)
        {
            
        }
        protected void btnXem_Click(object sender, EventArgs e)
        {
            if (txtWeek.Text == "0")
                hreport_msg.Value = "Tỷ giá = 0, Vui lòng nhập tỷ giá!!!!";
            else
                query();
        }
        protected void btnView_Click(object sender, EventArgs e)
        {
            if (txtWeek.Text == "0")
                hreport_msg.Value = "Tỷ giá = 0, Vui lòng nhập tỷ giá!!!!";
            else
                query_Xem();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            DataTable mydata = new DataTable();
            DataTable machine_data = new DataTable();
            string sql = "";
            connectEIP myconn = new connectEIP();

            myconn.myopen();

           

            
            myconn.myclose();

            hreport_msg.Value = " ";

            query();
        }
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            //Response.Redirect("/schedule/main.aspx");
        }

        protected void btnOrder_Merge_Click(object sender, EventArgs e)
        {
            

        }
        
    }
}