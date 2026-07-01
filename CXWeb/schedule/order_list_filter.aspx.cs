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
    public partial class order_list_filter : System.Web.UI.Page
    {
        BindingSource bdsInheritVoucher = new BindingSource();
        DataSet mydata;
        string finish_date = "";
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                confirm_Item_ID.Text = base.Request.QueryString["pr"];
                finish_date = base.Request.QueryString["d"];
                txtWeek.Text = "6";
                query();
                
            }
        }
        void query()
        {
            connectTT myconn = new connectTT();
            connectEIP myconn_SQL = new connectEIP();


            //-------------------------------------------------------------

            mydata = new DataSet();
            finish_date = base.Request.QueryString["d"];

            System.Collections.Hashtable htPara = new System.Collections.Hashtable();
            htPara["ITEM_NO"] = confirm_Item_ID.Text;
            htPara["DATE"] = finish_date;
            htPara["PERIOD"] = Convert.ToInt32(txtWeek.Text);


            mydata = myconn_SQL.ExecuteReturnDs("sp_FilterOrder", htPara, CommandType.StoredProcedure);

            myconn_SQL.myclose();          

            GridView1.DataSource = mydata.Tables[0] ;
            GridView1.DataBind();

            GridView2.DataSource = mydata.Tables[2];
            GridView2.DataBind();

            GridView3.DataSource = mydata.Tables[3];
            GridView3.DataBind();

            int sum_order_quantity = 0;
            int sum_WIP = 0;

            for (int i = 0; i < mydata.Tables[0].Rows.Count; i++)
            {
                sum_order_quantity += Convert.ToInt32(mydata.Tables[0].Rows[i]["Order_Qty"]);
            }

            for (int i = 0; i < mydata.Tables[3].Rows.Count; i++)
            {
                sum_WIP += Convert.ToInt32(mydata.Tables[3].Rows[i]["Wip_Qty"]);
            }

            txtSum_Qty.Text = sum_order_quantity.ToString();
            txtSum_Wip.Text = sum_WIP.ToString();

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
       
        protected void btnXem_Click(object sender, EventArgs e)
        {
            query();
        }
       
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("/schedule/order_list.aspx");
        }

        protected void btnOrder_Merge_Click(object sender, EventArgs e)
        {
            DataTable mydata2 = new DataTable();

            bool is_Check = false;

            string strOrder_ID = Auto_Order_Id();
            connectEIP myconn_SQL = new connectEIP();

            string strOrder_No = "";
            string strItem_No = "";
            string strCus_No = "";
            string strCus_Name = "";
            string strDate = "";
            double dbQty = 0;
            DateTime dteNgay ;
            int int_Count_Check = 0;
            myconn_SQL.myopen();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                
                    strOrder_ID = ((System.Web.UI.WebControls.Label)GridView1.Rows[i].FindControl("lblOrder_ID")).Text;
                    strItem_No = ((System.Web.UI.WebControls.Label)GridView1.Rows[i].FindControl("lblItem_No")).Text;
                    strOrder_No = ((System.Web.UI.WebControls.Label)GridView1.Rows[i].FindControl("lblOrder_No")).Text;
                    strCus_No = ((System.Web.UI.WebControls.Label)GridView1.Rows[i].FindControl("lblCus_ID")).Text;
                    strCus_Name = ((System.Web.UI.WebControls.Label)GridView1.Rows[i].FindControl("lblCus_Name")).Text;
                    strDate = ((System.Web.UI.WebControls.Label)GridView1.Rows[i].FindControl("lblDelivery_Date")).Text;
                    dbQty = Convert.ToDouble(((System.Web.UI.WebControls.Label)GridView1.Rows[i].FindControl("lblQty")).Text);
                    if (strDate != "")
                        dteNgay = Convert.ToDateTime(strDate);
                    else
                        dteNgay = DateTime.Now.AddDays(10);


                if (dbQty <= 0)
                    continue;

                    System.Collections.Hashtable htPara = new System.Collections.Hashtable();
                    htPara["ORDER_ID"] = strOrder_ID;
                    htPara["ORDER_NO"] = strOrder_No;
                    htPara["ITEM_NO"] = strItem_No;
                    htPara["CUS_NO"] = strCus_No;
                    htPara["DELIVERY_DATE"] = dteNgay;
                    htPara["CUS_NAME"] = strCus_Name;
                    htPara["ORDER_QTY"] = dbQty;

                    mydata2 = myconn_SQL.ExecuteReturnDt("sp_Save_Order_Merging", htPara, CommandType.StoredProcedure);
                    int_Count_Check += 1;
                
            }
            myconn_SQL.myclose();
            // MessageBox.Show("Tạo thành công!!!");
            if (int_Count_Check >= 1)
            {
                Response.Redirect("/schedule/order_merging.aspx?pr=" + strOrder_ID);
            }

        }

        private string Auto_Order_Id()
        {
            DataTable dt = new DataTable();

            connectEIP myconn_SQL = new connectEIP();
            myconn_SQL.myopen();

            dt = myconn_SQL.ExecuteReturnDt("sp_Get_Order_ID_Merging", CommandType.StoredProcedure);
            string strOrder_Id = "";
            if (dt.Rows.Count > 0)
                strOrder_Id = dt.Rows[0][0].ToString();

            myconn_SQL.myclose();
            return strOrder_Id;
        }
    }
}