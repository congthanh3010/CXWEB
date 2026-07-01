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
    public partial class prod_attrition_rate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
             
                query();
            }
        }
        void query()
        {
            connectEIP myconn = new connectEIP();

            string strsql = "";
           
            //-------------------------------------------------------------

            DataTable mydata = new DataTable();
            DataTable mydata2 = new DataTable();
            myconn.myopen();


            //strsql = "select Item_No,productivity from schedule_ma_prod_setting where Item_No ='" + Item_No.Text + "' order by Item_No";
            if(txtItem_No.Text == "")
                strsql = "select * from schedule_Loss_Rate  order by Item_No";
            else
                strsql = "select * from schedule_Loss_Rate where Item_No like '%" + txtItem_No.Text + "%'  order by Item_No";

            mydata = myconn.mysearch(strsql);   

            GridView1.DataSource = mydata;
            GridView1.DataBind();
            myconn.myclose();
        }
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            query();
        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string Item_No = ((Label)GridView1.Rows[e.RowIndex].FindControl("lbl_Item_No")).Text;
            

            connectEIP myconn = new connectEIP();

            myconn.myopen();

            myconn.mySqlExecute("delete from  schedule_Loss_Rate where Item_No=N'" + Item_No + "'");


            myconn.myclose();

          

            query();
        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            query();
        }
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string Item_No = ((Label)GridView1.Rows[e.RowIndex].FindControl("lbl_Item_No")).Text;
            string Rate_1 = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txt_Rate_1")).Text;
            string Rate_2 = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txt_Rate_2")).Text;
            string Rate_3 = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txt_Rate_3")).Text;

            try
            {
                Convert.ToInt32(Rate_1);
            }
            catch
            {
                hreport_msg.Value = "損耗率-Attrition Rate is wrong !!";
                return;
            }

            connectEIP myconn = new connectEIP();

            myconn.myopen();

            myconn.mySqlExecute("update  schedule_Loss_Rate set Rate_1='" + Rate_1 + "', Rate_2='" + Rate_2 + "', Rate_3='" + Rate_3 + "' where Item_No=N'" + Item_No + "'");


            myconn.myclose();

            GridView1.EditIndex = -1;

            query();

        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
        }
        protected void btnIns_Click(object sender, EventArgs e)
        {
            //string Item_No = ((TextBox)GridView1.FooterRow.FindControl("txtins_Item_No")).Text;
            //string Rate_1 = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txt_Rate_1")).Text;
            //string Rate_2 = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txt_Rate_2")).Text;
            //string Rate_3 = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txt_Rate_3")).Text;

            //DataTable machine_data = new DataTable();
            //string sql = "";

            //connectEIP myconn = new connectEIP();

            //myconn.myopen();

            //sql = "  select a.* " +
            //  " FROM OPENQUERY(TIPTOP, " +
            //  " 'select * " +
            //  " from eci_file t " +
            //  " where t.eciacti=''Y'' " +
            //  " and t.eci01 = ''" + Item_No + "'''   ) a";
            //machine_data = myconn.mysearch(sql);
            //if (machine_data.Rows.Count == 0)
            //{
            //    hreport_msg.Value = "機台編號錯誤/Số máy sai！";
            //    //showMessage(report_msg);
            //    return;
            //}

            //try
            //{
            //    Convert.ToInt32(productivity);
            //}
            //catch
            //{
            //    hreport_msg.Value = "機台產能錯誤/Năng suất nhập sai！";
            //    return;
            //}

            //myconn.mySqlExecute("delete from  schedule_ma_prod_setting where Item_No=N'" + Item_No + "' and Sta_No ='" + sta_No + "'");
            //myconn.mySqlExecute("insert into  schedule_ma_prod_setting (Item_No,productivity, Sta_No) values ('" + Item_No + "','" + productivity + "','" + sta_No + "') ");


            //myconn.myclose();


            //query();
        }
        protected void btnXem_Click(object sender, EventArgs e)
        {
            query();
        }

            
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //DataTable mydata = new DataTable();
            //DataTable machine_data = new DataTable();
            //string sql = "";
            //connectEIP myconn = new connectEIP();

            //myconn.myopen();

           

            
            //myconn.myclose();

            //hreport_msg.Value = " ";

            //query();
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("/schedule/main.aspx");
        }
    }
}