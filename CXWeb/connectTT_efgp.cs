using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
using System.Data;
//using Oracle.DataAccess.Client;
//using Devart.Data;
//using Devart.Data.Oracle;

namespace CXWeb
{
    public class connectTT_efgp
    {
        //private SqlConnection conn;
        private static OracleConnection conn;
        public void myopen()
        {
            string strconn = @"Data Source=CXVNTP;Persist Security Info=True;User ID=efgp;Password=efgp";
            conn = new OracleConnection(strconn);
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
            OracleCommand cmd = new OracleCommand(strsql, conn);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }
    }
}