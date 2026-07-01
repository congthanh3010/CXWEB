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
    public partial class DBR : System.Web.UI.Page
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
            connectTT myconn = new connectTT();

            string strsql = "";
            string product_no = base.Request.QueryString["pr"];
            string order_quantity = base.Request.QueryString["oqty"];
            string wip_quantity = base.Request.QueryString["wqty"];
            string finish_date = base.Request.QueryString["d"];
            string stage = base.Request.QueryString["st"];

            //-------------------------------------------------------------

            DataTable mydata = new DataTable();
            DataTable mydata2 = new DataTable();
            myconn.myopen();


            //strsql = "select machine_no,productivity from schedule_ma_prod_setting where machine_no ='" + machine_no.Text + "' order by machine_no";
            strsql = "select ecb01, ecb03, ecb06, ecb17, ecb08, eca02,'' as produce_date,'0' as produce_qty ,eci01 as ecb07" +
                     " from(select ecu01, ecu10, max(ecu02) as ecu02 from ecu_file where ecu10 = 'Y' group by ecu01, ecu10) u," +
                     " ecb_file" +
                     " left outer join eca_file on eca01 = ecb08" +
                     " left outer join (select max(eci01) as eci01, eci03 from eci_file where eciacti='Y' group by eci03) on eci03=ecb08"+
                     " where ecb01 = u.ecu01 and ecb02 = u.ecu02 and u.ecu10 = 'Y'" +
                     " and ecb01 in(" +
                     " select '"+product_no+"' as bmb03 from dual" +
                     " union" +
                     " select bmb03 from bmb_file where bmb01 = '"+product_no+"' and bmb03 like  'H%' and bmb05 is null" +
                     " union" +
                     " select bmb03 from bmb_file" +
                     " where bmb01 in (select bmb03 from bmb_file where bmb01 = '"+product_no+"' and bmb03 like  'H%' and bmb05 is  null)" +
                     " and bmb03 like  'H%' and bmb05 is  null" +
                     " union" +
                     " select bmb03 from bmb_file" +
                     " where bmb01 in (select bmb03 from bmb_file" +
                     " where bmb01 in (select bmb03 from bmb_file where bmb01 = '"+product_no+"' and bmb03 like  'H%' and bmb05 is  null) " +
                     " and bmb03 like  'H%' and bmb05 is  null)" +
                     " and bmb03 like  'H%' and bmb05 is  null" +
                     " union" +
                     " select bmb03 from bmb_file" +
                     " where bmb01 in (select bmb03 from bmb_file" +
                     " where bmb01 in (select bmb03 from bmb_file" +
                     " where bmb01 in (select bmb03 from bmb_file where bmb01 = '"+product_no+"' and bmb03 like  'H%' and bmb05 is  null)  " +
                     " and bmb03 like  'H%' and bmb05 is  null) " +
                     " and bmb03 like  'H%' and bmb05 is  null)" +
                     " and bmb03 like  'H%' and bmb05 is  null" +
                     " ) and substr(ecb01,14,2)='"+ stage + "'" +
                     " order by ecb01, ecb03 desc";

            mydata = myconn.mysearch(strsql);


            mydata.Columns.Add("link1");

            int skip_time_day = 0;
            double bad_rate = 0.025;
          
            for(int i =0;i< mydata.Rows.Count;i++)
            {
                mydata.Rows[i]["link1"] = "/schedule/ma_schedule_result.aspx?ma=" + mydata.Rows[i]["ecb07"].ToString();


                skip_time_day = 0;
                bad_rate = 0.025;

                if (stage == "D0") skip_time_day += 1;
                else if (stage == "C0") skip_time_day += 1;
                else if(stage == "B0") skip_time_day += 1;
                else if(stage == "A0") skip_time_day += 1;

                mydata.Rows[i]["produce_date"] = Convert.ToDateTime(finish_date).AddDays(-skip_time_day).ToString("yyyy/MM/dd");
                mydata.Rows[i]["produce_qty"] = Math.Round((Convert.ToDouble(order_quantity) - Convert.ToDouble(wip_quantity))*(1+ bad_rate * Convert.ToDouble(i)),0);
            }


            GridView1.DataSource = mydata;
            GridView1.DataBind();

          


            myconn.myclose();
        }
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;

            DataTable mygrid_data = new DataTable();
            mygrid_data = get_grid_data(e.RowIndex);
            GridView1.DataSource = mygrid_data;
            GridView1.DataBind();

         
        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
           
        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;

            DataTable mygrid_data = new DataTable();
            mygrid_data = get_grid_data(-1);
            GridView1.DataSource = mygrid_data;
            GridView1.DataBind();
           
        }
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                //tam luu du lieu grid
                DataTable mydata = new DataTable();

                mydata = get_grid_data(e.RowIndex);
               




                GridView1.EditIndex = -1;

                GridView1.DataSource = mydata;
                GridView1.DataBind();

            }
            catch (Exception ms)
            {
                showMessage(ms.Message);
                Response.Write(ms.Message);
            }

      
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
        }
        protected void btnIns_Click(object sender, EventArgs e)
        {
         
        }
        //protected void btnXem_Click(object sender, EventArgs e)
        //{
 
        //}
        protected void btnSchedule_Click(object sender, EventArgs e)
        {
            try
            {
                
                int machine_productivity ;
                int produce_qty = 0;
                string produce_date = "";
                string machine_no="";
                string product_no = "";
                string process = "";



                string strsql = "";
                string strsql_ins = "";
                DataTable mygrid_data = new DataTable();
                mygrid_data = get_grid_data(-1);
                connectEIP myconn = new connectEIP();
                myconn.myopen();

                DataTable ma_prod_setting = new DataTable();
                for (int i = 0; i < mygrid_data.Rows.Count; i++)
                {
                    machine_productivity = 30000;
                    produce_qty = 0;
                    produce_date = "";
                    machine_no = "";
                    product_no = "";
                    process = "";


                    produce_qty = Convert.ToInt32(mygrid_data.Rows[i]["produce_qty"].ToString());
                    produce_date = mygrid_data.Rows[i]["produce_date"].ToString();
                    machine_no = mygrid_data.Rows[i]["ecb07"].ToString();
                    product_no = mygrid_data.Rows[i]["ecb01"].ToString();
                    process = mygrid_data.Rows[i]["ecb06"].ToString();

                    //lấy sản lượng máy mỗi ngày
                    strsql = "select machine_no,productivity from schedule_ma_prod_setting where machine_no='" + machine_no + "'";
                    ma_prod_setting = myconn.mysearch(strsql);
                    if (ma_prod_setting.Rows.Count != 0)
                        machine_productivity = Convert.ToInt32(ma_prod_setting.Rows[0]["productivity"].ToString());

                    // điền dữ liệu xếp lịch vào schedule_produce_plan
                    if (produce_qty!=0 && machine_no!="" && produce_date!="")
                    {
                        strsql_ins = "insert into  schedule_produce_plan " +
                                     " (id,schedule_date,machine_no,process,product,schedule_quantity)  " +
                                     " values ('"+DateTime.Now.ToString("yyMMdd_HHmmss_fff")+"','"+ produce_date+"','" + machine_no + "','" + process + "','" + product_no + "','" + produce_qty + "') ";
                        myconn.mySqlExecute(strsql_ins);
                    }


                }

                



                myconn.myclose();

                showMessage("排程成功。");

            }
            catch (Exception ms)
            {
                showMessage(ms.Message);
                Response.Write(ms.Message);
            }

        }
        DataTable get_grid_data(int edit_row_index)
        {
            DataTable mydata = new DataTable();

            mydata.Columns.Add("ecb01");
            mydata.Columns.Add("ecb03");
            mydata.Columns.Add("ecb06");
            mydata.Columns.Add("produce_date");
            mydata.Columns.Add("produce_qty");
            mydata.Columns.Add("ecb07");
            mydata.Columns.Add("link1");

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (i == edit_row_index)
                    mydata.Rows.Add(((Label)GridView1.Rows[i].FindControl("lbl01")).Text,
                    ((TextBox)GridView1.Rows[i].FindControl("txt02")).Text,
                    ((TextBox)GridView1.Rows[i].FindControl("txt03")).Text,
                    ((TextBox)GridView1.Rows[i].FindControl("txt04")).Text,
                    ((TextBox)GridView1.Rows[i].FindControl("txt05")).Text,
                    ((TextBox)GridView1.Rows[i].FindControl("txt06")).Text,
                    "/schedule/ma_schedule_result.aspx?ma=" + ((TextBox)GridView1.Rows[i].FindControl("txt06")).Text);
                else
                    mydata.Rows.Add(((Label)GridView1.Rows[i].FindControl("lbl01")).Text,
                   ((Label)GridView1.Rows[i].FindControl("lbl02")).Text,
                   ((Label)GridView1.Rows[i].FindControl("lbl03")).Text,
                   ((Label)GridView1.Rows[i].FindControl("lbl04")).Text,
                   ((Label)GridView1.Rows[i].FindControl("lbl05")).Text,
                   ((HyperLink)GridView1.Rows[i].FindControl("HyperLink1")).Text,
                   "/schedule/ma_schedule_result.aspx?ma=" + ((HyperLink)GridView1.Rows[i].FindControl("HyperLink1")).Text);

            }
            return mydata;

        }
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("/schedule/product_required_quantity.aspx?pr="+base.Request.QueryString["pr"]);
        }
        public void showMessage(string mess)
        {
            //string strBuilder = "<script language='javascript'>alert('" + mess + "')</script>";
            //Response.Write(strBuilder);
            string myStringVariable = string.Empty;
            myStringVariable = mess;
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + myStringVariable + "');", true);
        }
    }
}