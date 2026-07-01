using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
using System.Data;
//using Oracle.DataAccess.Client;
//using Devart.Data;
//using Devart.Data.Oracle;

using System.Text;
using System.Data.OleDb;

namespace CXWeb
{
    public class connectTT
    {
        //private SqlConnection conn;
        private OleDbConnection conn;
        public void myopen()
        {
            string strconn = @"Provider=oraoledb.oracle;Data Source=CXVNTP;Persist Security Info=True;User ID=cx_vn;Password=cx_vn168";
            conn = new OleDbConnection(strconn);
            //using (conn = new OleDbConnection(strconn))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
            }


        }

        public void myclose()
        {
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }

        }

        public DataTable mysearch(string strsql)
        {
            DataTable dt = new DataTable();
            dt.TableName = "dataByTime";
            OleDbCommand cmd = new OleDbCommand(strsql, conn);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }
    }
}