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

namespace CXWeb.tracking
{
    public partial class tracking_product_selection : System.Web.UI.Page
    {
        connectTT myconn = new connectTT(); 
        protected void Page_Load(object sender, EventArgs e)
        {
             

            if (!IsPostBack )
            {
                query();

            }

        }
        void query()
        {
            string strsql = "";
          
            //-------------------------------------------------------------

            DataTable mydata = new DataTable();
            myconn.myopen();

            if (invoice_text.Text.Trim() == "" && product_no.Text.Trim() == "" && lot_number.Text.Trim() == "")
                return;
            //strsql = "select * from checklist_unit_setting where sub_unit ='" + sub_unit + "' order by number";
            strsql = "select distinct b.ogb04,b.ogb092 from oga_file a, ogb_file b "+
                     " where oga01 = ogb01 and oga27 like '"+ invoice_text .Text.Trim()+ "%' and ogb04 like '" + product_no.Text.Trim() + "%' and ogb092 like '" + lot_number.Text.Trim() + "%' and oga09 in('2','3','4')" +
                     " order by b.ogb04";
             mydata = myconn.mysearch(strsql);

            mydata.Columns.Add("link");
           
            for (int i = 0; i < mydata.Rows.Count; i++)
            {
                mydata.Rows[i]["link"] = "tracking_table.aspx?pd=" + mydata.Rows[i]["ogb04"] + "&lot=" + mydata.Rows[i]["ogb092"];
               
            }


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
        //protected void btnBack_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("/checklist/checklist_unit_selection.aspx");
        //}

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
        protected void btnXem_Click(object sender, EventArgs e)
        {
            query();
        }
    }
}