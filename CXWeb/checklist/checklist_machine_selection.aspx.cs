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

namespace CXWeb.checklist
{
    public partial class checklist_machine_selection : System.Web.UI.Page
    {
        connectEIP myconn = new connectEIP(); 
        protected void Page_Load(object sender, EventArgs e)
        {
            string sub_unit_code = base.Request.QueryString["su"];
            

            if (!IsPostBack && sub_unit_code != null)
            {
                if (Session["LoginUserInfo"] == null)
                {
                    Response.Redirect("login.aspx");
                }

                hsub_unit.Value = sub_unit_code;

                if (DateTime.Now.Hour < 6)
                    textDate.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");
                else
                    textDate.Text = DateTime.Now.ToString("yyyy/MM/dd");

                

                DataTable mydata = new DataTable();
                myconn.myopen();
                mydata = myconn.mysearch("select office_check_person from checklist_unit_setting where office_check_person like '" + Session["LoginUserInfo"].ToString() + "%'");
                myconn.myclose();

                if (mydata.Rows.Count == 0)
                {
                    textDate.Enabled = false;
                 
                }



                query(sub_unit_code);

            }

        }
        void query(string sub_unit)
        {
            string strsql = "";
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
            //-------------------------------------------------------------

            DataTable mydata = new DataTable();
            myconn.myopen();


            //strsql = "select * from checklist_unit_setting where sub_unit ='" + sub_unit + "' order by number";
            strsql = "select a.*,b.sum_point,isnull(c.count_prob,0) as count_prob from checklist_unit_setting a" +
                     " left outer join checklist_table b on a.machine_no = b.ten_may and"+
                     " b.don_vi = '"+ sub_unit+"' and b.thoi_gian = '"+ textDate.Text + "'" +
                     " left outer join"+
                     " (select machine_no, count(*) as count_prob from checklist_machine_problem where status = 'problem'"+
                     " group by machine_no) c"+
                     " on c.machine_no=a.machine_no "+
                     " where a.sub_unit = '" + sub_unit+"' order by a.number";
             mydata = myconn.mysearch(strsql);

            mydata.Columns.Add("link");
            mydata.Columns.Add("link2");
            for (int i = 0; i < mydata.Rows.Count; i++)
            {
                mydata.Rows[i]["link"] = "checklist_machine.aspx?su=" + sub_unit + "&mc=" + mydata.Rows[i]["machine_no"]+"&da="+ textDate.Text;
                mydata.Rows[i]["link2"] = "checklist_machine.aspx?su=" + sub_unit + "&mg=" + mydata.Rows[i]["machine_group"] + "&da=" + textDate.Text;
            }


            GridView1.DataSource = mydata;
           
            GridView1.DataBind();

         
            int colspan = 1;
           
            for (int i = 0; i < mydata.Rows.Count; i++)
            {
                // xu ly mau
                if (mydata.Rows[i]["sum_point"].ToString().Trim() != "")
                {
                    GridView1.Rows[i].BackColor = System.Drawing.Color.Yellow;
                    GridView1.Rows[i].ForeColor = System.Drawing.Color.Red;
                    ((HyperLink)GridView1.Rows[i].FindControl("HyperLink1")).ForeColor = System.Drawing.Color.Red;
                    ((HyperLink)GridView1.Rows[i].FindControl("HyperLink2")).ForeColor = System.Drawing.Color.Red;
                }


              

            }
            for(int i= mydata.Rows.Count-1;i>=0;i--)
            {
                // xu ly phan group rowspan
                if (i != 0)
                {
                    if (mydata.Rows[i]["machine_group"].ToString() == mydata.Rows[i - 1]["machine_group"].ToString())
                    {
                        colspan++;
                        GridView1.Rows[i].Cells[0].Visible = false;
                    }
                    else
                    {
                        GridView1.Rows[i].Cells[0].Attributes.Add("rowspan", colspan.ToString());
                        colspan = 1;
                    }
                }
                else
                {
                    GridView1.Rows[i].Cells[0].Attributes.Add("rowspan", colspan.ToString());
                    colspan = 1;
                }
            }
            myconn.myclose();
        }
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            query(hsub_unit.Value);
        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            query(hsub_unit.Value);
        }
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

           


        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/checklist/checklist_unit_selection.aspx");
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //try
            //{
            //    if (e.Row.RowType == DataControlRowType.DataRow)
            //    {
            //        string lblsum_point = ((Label)GridView1.Rows[e.Row.RowIndex].FindControl("lblsum_point")).Text;
            //        if (lblsum_point.Trim() != "") //Select the row 
            //        {
            //            e.Row.BackColor = System.Drawing.Color.Green;
            //            e.Row.ForeColor = System.Drawing.Color.Red;

            //            //or you can select the color 
            //            //e.Row.BackColor = System.Drawing.Color.FromArgb(255, 0, 0); 
            //        }
            //    }
            //}
            //catch (Exception ms)
            //{

            //}
        }
        //protected void btnXem_Click(object sender, EventArgs e)
        //{
        //    query(hsub_unit.Value);
        //}

        protected void textDate_TextChanged(object sender, EventArgs e)
        {
            query(hsub_unit.Value);
        }
    }
}