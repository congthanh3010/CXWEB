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


namespace CXWeb.schedule
{
    public partial class ma_history : System.Web.UI.Page
    {
        DataTable mydata;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                Begin_date.Text = DateTime.Now.AddDays(-2).ToString("yyyy/MM/dd");
                End_date.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");
                confirm_Machine_ID.Text = "TD-0650012";
                //query();
            }
        }
        void query()
        {
            connectTT myconn = new connectTT();
            connectEIP myconn_SQL = new connectEIP();


            //-------------------------------------------------------------

            mydata = new DataTable();
            DataSet mydata2 = new DataSet();

            System.Collections.Hashtable htPara = new System.Collections.Hashtable();
            htPara["MACHINE_ID"] = confirm_Machine_ID.Text;
            htPara["BEGIN_DATE"] = Begin_date.Text;
            htPara["END_DATE"] = End_date.Text;


            mydata2 = myconn_SQL.ExecuteReturnDs("sp_GetMachine_Produce_History", htPara, CommandType.StoredProcedure);

            myconn_SQL.myclose();

         


            GridView1.DataSource = mydata2.Tables[0];
            GridView1.DataBind();

            GridView2.DataSource = mydata2.Tables[1];
            GridView2.DataBind();

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
            query();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
        }
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("/schedule/main.aspx");
        }
    }
}