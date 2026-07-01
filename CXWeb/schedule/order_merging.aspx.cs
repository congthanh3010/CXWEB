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
    public partial class order_merging : System.Web.UI.Page
    {
        BindingSource bdsInheritVoucher = new BindingSource();
        DataTable mydata;
        string strConfirm_Date = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strConfirm_Date = base.Request.QueryString["pr"];
                confirm_date.Text = base.Request.QueryString["pr"];
                query();
               
            }
        }

        void query()
        {

            connectEIP myconn_SQL = new connectEIP();

            //-------------------------------------------------------------

            mydata = new DataTable();
            DataSet mydata2 = new DataSet();

            myconn_SQL.myopen();

            System.Collections.Hashtable htPara = new System.Collections.Hashtable();
            htPara["ORDER_ID"] = confirm_date.Text;
            htPara["_ITEM_NO"] = txtItem_no.Text;

            mydata2 = myconn_SQL.ExecuteReturnDs("sp_GetSchedule", htPara, CommandType.StoredProcedure);

            myconn_SQL.myclose();

            bdsInheritVoucher.DataSource = mydata2.Tables[0];


            GridView2.DataSource = bdsInheritVoucher;
            GridView2.DataBind();

            GridView3.DataSource = mydata2.Tables[1];
            GridView3.DataBind();

            GridView4.DataSource = mydata2.Tables[2];
            GridView4.DataBind();
        }

        void Get_Order_Info()
        {
            //connectEIP myconn_SQL = new connectEIP();
            //DataTable dt = new DataTable();

            //myconn_SQL.myopen();

            //dt = myconn_SQL.mysearch("SELECT Order_ID, sum(Qty_To_Produce) AS Order_Qty, CONVERT(varchar(50),MIN(Delivery_Date),101)  AS Delivery_Date FROM Schedule_Order_Merging WHERE Order_ID LIKE '" + confirm_Order_ID.Text + "%' GROUP BY Order_ID ");

            //if (dt.Rows.Count > 0)
            //{
            //    numOrder_Qty.Text = dt.Rows[0][1].ToString();
            //    dteDeliver_Date.Text = dt.Rows[0][2].ToString();
            //}

        }
        void queryMPS()
        {
            //connectEIP myconn_SQL = new connectEIP();


            ////-------------------------------------------------------------

           
            //DataSet mydataset = new DataSet();

            //myconn_SQL.myopen();

            //System.Collections.Hashtable htPara = new System.Collections.Hashtable();
            //htPara["ORDER_ID"] = confirm_Order_ID.Text;

            //mydataset = myconn_SQL.ExecuteReturnDs("sp_GetBom", htPara, CommandType.StoredProcedure);

            //myconn_SQL.myclose();

            //GridView2.DataSource = mydataset.Tables[0];
            //GridView2.DataBind();

            //GridView3.DataSource = mydataset.Tables[1];
            //GridView3.DataBind();

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
            queryMPS();
        }

        protected void btnXem_Click(object sender, EventArgs e)
        {
            query();
            Get_Order_Info();
        }

        protected void btDBR_Click(object sender, EventArgs e)
        {
           // Response.Redirect("/schedule/order_DBR.aspx?pr=" + confirm_Order_ID.Text);
        }
    }
}