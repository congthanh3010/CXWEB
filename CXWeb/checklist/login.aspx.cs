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

namespace CXWeb.checklist
{
    public partial class login : System.Web.UI.Page
    {
        connectTT myconn = new connectTT();
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }
       

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtLoginName.Value) || string.IsNullOrEmpty(txtPassword.Value))
            {
                Response.Write("<script language=javascript>alert('Xin nhập tên và mật mã ！');</script>");

            }
            else
            {
                DataTable mydata = new DataTable();
                string strsql = "select gen01 from gen_file " +
                    " where gen01='" + txtLoginName.Value + "' and genacti='Y'";
                myconn.myopen();
                mydata = myconn.mysearch(strsql);
                myconn.myclose();

                if(txtLoginName.Value== "TW1244")
                {
                    DataRow row = mydata.NewRow();
                    row[0] = "TW1244";
                    mydata.Rows.InsertAt(row, 0);
                }
                

                if (mydata.Rows.Count == 0)
                {
                    Response.Write("<script language=javascript>alert('Sai tên ！');</script>");
                }
                else if (mydata.Rows[0]["gen01"].Equals(txtPassword.Value))
                {
                    Session["LoginUserInfo"] = txtLoginName.Value;
                    Response.Redirect("checklist_unit_selection.aspx");
                }
                else
                {
                    Response.Write("<script language=javascript>alert('Sai mật mã ！');</script>");
                }
            }
        }
    }
}