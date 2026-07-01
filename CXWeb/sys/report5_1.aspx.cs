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

namespace CXWeb.sys
{
    public partial class report5_1 : System.Web.UI.Page
    {
        connectEIP myconn = new connectEIP();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                remark2.Text = "依據每次提出的規格報價";
                BindData();
            }
        }

        private void BindData()
        {
            DataTable mydata = new DataTable();
            myconn.myopen();
            mydata = myconn.mysearch("select * from COD_CODE where class = 'report5' order by code");
            gvData.DataSource = mydata;
            gvData.DataBind();
            myconn.myclose();
        }

        protected void gvData_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string str = gvData.DataKeys[e.RowIndex].Value.ToString();
            myconn.myopen();
            myconn.mySqlExecute("delete from COD_CODE where class ='report5' and code = '"+ str + "' ");
            myconn.myclose();
            BindData();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            myconn.myopen();
            myconn.mySqlExecute("insert into COD_CODE (class,code,description,description2) values ('report5',N'" + mat_no.Text+ "',N'" + remark.Text + "',N'" + remark2.Text + "') ");
            myconn.myclose();
            BindData();
        }
    }
}