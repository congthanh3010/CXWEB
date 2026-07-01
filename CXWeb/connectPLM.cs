using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace CXWeb
{
    public class connectPLM
    {
        private SqlConnection conn;

        public void myopen()
        {
            string strconn = @"Data Source=10.10.20.9;Initial Catalog=CX_official_1;Persist Security Info=True;User ID=sa;Password=sql#DSC";
            conn = new SqlConnection(strconn);
            conn.Open();

        }

        public void myclose()
        {
            conn.Close();
        }

        public DataTable mysearch(string strsql)
        {
            DataTable dt = new DataTable();
            dt.TableName = "dataByTime";
            SqlCommand cmd = new SqlCommand(strsql, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }

        public void mySqlExecute(string strsql)
        {
            SqlCommand cmd = new SqlCommand(strsql, conn);
            cmd.ExecuteNonQuery();
        }

    }
}