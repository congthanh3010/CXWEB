using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Windows.Forms;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.DataVisualization.Charting;
using System.Threading;


namespace CXWeb.schedule
{
    public partial class order_DBR : System.Web.UI.Page
    {
        BindingSource bdsInheritVoucher = new BindingSource();
        DataTable mydata;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                confirm_Order_ID.Text = base.Request.QueryString["pr"];
                query();

            }
        }

        void query()
        {
            connectEIP myconn_SQL = new connectEIP();


            //-------------------------------------------------------------


            DataSet mydataset = new DataSet();

            myconn_SQL.myopen();

            System.Collections.Hashtable htPara = new System.Collections.Hashtable();
            htPara["ORDER_ID"] = confirm_Order_ID.Text;

            mydataset = myconn_SQL.ExecuteReturnDs("sp_GetBom_V2", htPara, CommandType.StoredProcedure);

            myconn_SQL.myclose();

            GridView1.DataSource = mydataset.Tables[2];
            GridView1.DataBind();

            GridView2.DataSource = mydataset.Tables[4];
            GridView2.DataBind();

            GridView3.DataSource = mydataset.Tables[3];
            GridView3.DataBind();

        }

        void Get_Order_Info()
        {
            connectEIP myconn_SQL = new connectEIP();
            DataTable dt = new DataTable();

            myconn_SQL.myopen();

            dt = myconn_SQL.mysearch("SELECT Order_ID, sum(Order_Qty) AS Order_Qty, MIN(Delivery_Date) AS Delivery_Date FROM Schedule_Order_Merging WHERE Order_ID = '" + confirm_Order_ID.Text + "' GROUP BY Order_ID ");

            if (dt.Rows.Count > 0)
            {
                numOrder_Qty.Text = dt.Rows[0][1].ToString();
                dteDeliver_Date.Text = dt.Rows[0][2].ToString();
            }

        }
        void queryMPS()
        {
            connectEIP myconn_SQL = new connectEIP();


            //-------------------------------------------------------------

           
            DataSet mydataset = new DataSet();

            myconn_SQL.myopen();

            System.Collections.Hashtable htPara = new System.Collections.Hashtable();
            htPara["ORDER_ID"] = confirm_Order_ID.Text;

            mydataset = myconn_SQL.ExecuteReturnDs("sp_GetBom", htPara, CommandType.StoredProcedure);

            myconn_SQL.myclose();

            GridView2.DataSource = mydataset.Tables[0];
            GridView2.DataBind();
            
        }
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("/schedule/order_list.aspx");
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
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
        }

        protected void btMPS_Click(object sender, EventArgs e)
        {
            //queryMPS();
        }

        protected void btnXem_Click(object sender, EventArgs e)
        {
            query();
            Get_Order_Info();
        }

       
    }
}