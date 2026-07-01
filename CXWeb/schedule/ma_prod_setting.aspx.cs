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
    public partial class ma_prod_setting : System.Web.UI.Page
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


            //strsql = "select machine_no,productivity from schedule_ma_prod_setting where machine_no ='" + machine_no.Text + "' order by machine_no";
            strsql = "select machine_no,productivity, Sta_No from schedule_ma_prod_setting  order by Sta_No, machine_no";

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
            string machine_no = ((Label)GridView1.Rows[e.RowIndex].FindControl("lbl_machine_no")).Text;
            string sta_No = ((Label)GridView1.Rows[e.RowIndex].FindControl("txt_Sta_No")).Text;

            connectEIP myconn = new connectEIP();

            myconn.myopen();

            myconn.mySqlExecute("delete from  schedule_ma_prod_setting where machine_no=N'" + machine_no + "' and Sta_No ='" + sta_No + "'");


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
            string machine_no = ((Label)GridView1.Rows[e.RowIndex].FindControl("lbl_machine_no")).Text;
            string productivity = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txt_productivity")).Text;
            string sta_No = "";// ((Label)GridView1.Rows[e.RowIndex].FindControl("txt_Sta_No")).Text;

            try
            {
                Convert.ToInt32(productivity);
            }
            catch
            {
                hreport_msg.Value = "機台產能錯誤/Công suất máy là sai!!";
                return;
            }

            connectEIP myconn = new connectEIP();

            myconn.myopen();
            string sqlUpdate = "update  schedule_ma_prod_setting set productivity='" + productivity + "' where machine_no=N'" + machine_no + "' and Sta_No ='" + sta_No + "'";
            myconn.mySqlExecute("update  schedule_ma_prod_setting set productivity='"+ productivity + "' where machine_no=N'"+ machine_no + "'");


            myconn.myclose();

            GridView1.EditIndex = -1;

            query();

        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
        }
        protected void btnIns_Click(object sender, EventArgs e)
        {
            string machine_no = ((TextBox)GridView1.FooterRow.FindControl("txtins_machine_no")).Text;
            string productivity = ((TextBox)GridView1.FooterRow.FindControl("txtins_productivity")).Text;
            string sta_No = "";// ((TextBox)GridView1.FooterRow.FindControl("txt_Sta_No")).Text;

            DataTable machine_data = new DataTable();
            string sql = "";

            connectEIP myconn = new connectEIP();

            myconn.myopen();

            sql = "  select a.* " +
              " FROM OPENQUERY(TIPTOP, " +
              " 'select * " +
              " from eci_file t " +
              " where t.eciacti=''Y'' " +
              " and t.eci01 = ''" + machine_no + "'''   ) a";
            machine_data = myconn.mysearch(sql);
            if (machine_data.Rows.Count == 0)
            {
                hreport_msg.Value = "機台編號錯誤/Số máy sai！";
                //showMessage(report_msg);
                return;
            }

            try
            {
                Convert.ToInt32(productivity);
            }
            catch
            {
                hreport_msg.Value = "機台產能錯誤/Năng suất nhập sai！";
                return;
            }

            myconn.mySqlExecute("delete from  schedule_ma_prod_setting where machine_no=N'" + machine_no + "'");
            myconn.mySqlExecute("insert into  schedule_ma_prod_setting (machine_no,productivity) values ('" + machine_no + "','" + productivity + "') ");


            myconn.myclose();


            query();
        }
        protected void btnXem_Click(object sender, EventArgs e)
        {
            query();
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
            Response.Redirect("/schedule/main.aspx");
        }
    }
}