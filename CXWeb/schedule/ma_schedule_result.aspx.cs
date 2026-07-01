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
    public partial class ma_schedule_result : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                machine_no.Text = base.Request.QueryString["ma"];
                date_start.Text = DateTime.Now.ToString("yyyy/MM/dd");
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
            strsql = "select id,schedule_date,machine_no,process,product,schedule_quantity from schedule_produce_plan where machine_no='"+ machine_no.Text+ "' and schedule_date>='"+ date_start.Text.Replace("/","-") + "'  order by schedule_date";

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

            connectEIP myconn = new connectEIP();

            myconn.myopen();

            myconn.mySqlExecute("delete from  schedule_ma_prod_setting where machine_no=N'" + machine_no + "'");


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

            try
            {
                Convert.ToInt32(productivity);
            }
            catch
            {
                hreport_msg.Value = "機台產能錯誤！";
                return;
            }

            connectEIP myconn = new connectEIP();

            myconn.myopen();

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
                hreport_msg.Value = "機台編號錯誤！";
                //showMessage(report_msg);
                return;
            }

            try
            {
                Convert.ToInt32(productivity);
            }
            catch
            {
                hreport_msg.Value = "機台產能錯誤！";
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
        //protected void btnReturn_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("/schedule/order_list.aspx");
        //}
    }
}