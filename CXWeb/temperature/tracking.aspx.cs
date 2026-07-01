using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace CXWeb.temperature
{
    public partial class tracking : System.Web.UI.Page
    {
        private static connectDB conn;

        protected void Page_Load(object sender, EventArgs e)
        {
            txtDateFrom.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            txtDateTo.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            conn = new connectDB();
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            DataTable fields = LoadFields(ddlMachine.SelectedItem.Value);
            
        }

        protected DataTable LoadFields(string machine)
        {
            DataTable result = new DataTable();
            string strQuery = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS \n"
                + "WHERE TABLE_NAME = 'DAQ_Table1' \n"
                + $"(COLUMN_NAME = 'Time1' OR COLUMN_NAME LIKE '{machine}%') \n"
                + "ORDER BY ORDINAL_POSITION";
            conn.myopen();
            result = conn.mysearch(strQuery);
            conn.myclose();
            return result;
        }

        protected DataTable LoadData(string fromTime, string toTime, DataTable fields)
        {
            DataTable result = new DataTable();
            string strQuery = "";
            conn.myopen();
            result = conn.mysearch(strQuery);
            conn.myclose();
            return result;
        }
    }
}