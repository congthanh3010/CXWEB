using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Text.RegularExpressions;

namespace CXWeb
{
    public class connectDB
    {
        private SqlConnection conn;
        string strconn = @"Data Source=10.10.20.14;Initial Catalog=PLC_REPORT;Persist Security Info=True;User ID=sa;Password=@PLC123";

        public void myopen()
        {
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
            cmd.CommandTimeout = 3600;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }

        public void mySqlExecute(string strsql)
        {
            SqlCommand cmd = new SqlCommand(strsql, conn);
            cmd.CommandTimeout = 3600;
            cmd.ExecuteNonQuery();
        }
        public void mySqlExecute(string strsql, Hashtable htSQLPara, CommandType cmdType)
        {
            //SqlCommand cmd = new SqlCommand(strsql, conn);
            //cmd.ExecuteNonQuery();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = strsql;
            sqlCommand.CommandType = cmdType;
            sqlCommand.CommandTimeout = 0;
            if (htSQLPara.Count > 0)
            {
                if (sqlCommand.CommandType == CommandType.StoredProcedure)
                {

                    DataTable dt = mysearch("Select UPPER(Name) as Name FROM Sys.Parameters where Object_id = Object_id('" + strsql + "')");

                    foreach (DataRow row in dt.Rows)
                    {
                        string upper = ((string)row["Name"]).Replace("@", "").ToUpper();
                        if (htSQLPara.Contains((object)upper))
                            sqlCommand.Parameters.AddWithValue("@" + upper, htSQLPara[(object)upper]);
                    }
                }
            }
            sqlCommand.Connection = conn;
            sqlCommand.ExecuteNonQuery();

        }
        //public DataTable execReturnData(string strExec, Hashtable para, CommandType cmdType)
        //{
        //    DataTable data = new DataTable();
        //    using (var cmd = new SqlCommand(strExec, conn))
        //    {
        //        using (var da = new SqlDataAdapter(cmd))
        //        {
        //            cmd.CommandType = cmdType;
        //            cmd.CommandTimeout = 3600;
        //            foreach (var key in para.Keys)
        //                cmd.Parameters.AddWithValue(key.ToString(), para[key]);
        //            da.Fill(data);
        //        }
        //    }
        //    return data;
        //}
        public DataTable execReturnData(string strExec, Hashtable para, CommandType cmdType)
        {
            DataTable data = new DataTable();
            using (var conn = new SqlConnection(strconn))
            using (var cmd = new SqlCommand(strExec, conn))
            {
                cmd.CommandType = cmdType;
                cmd.CommandTimeout = 3600;

                if (para != null)
                {
                    foreach (var key in para.Keys)
                    {
                        var paramName = key.ToString();
                        cmd.Parameters.AddWithValue(paramName, para[key] ?? DBNull.Value);
                    }
                }

                using (var da = new SqlDataAdapter(cmd))
                {
                    conn.Open();
                    da.Fill(data);
                }
            }

            return data;
        }


        public string BulkCopy(DataTable dt, string tableName)
        {
            try
            {
                // Bulk Copy to SQL Server
                SqlBulkCopy bulk = new SqlBulkCopy(strconn);
                bulk.BulkCopyTimeout = 3600;
                bulk.DestinationTableName = tableName;
                foreach (DataColumn col in dt.Columns)
                {
                    Regex reg = new Regex(@"\((?<field>\w+)\)");
                    string field_name = reg.Matches(col.ColumnName)[0].Groups["field"].ToString();
                    bulk.ColumnMappings.Add(col.ColumnName, field_name);
                }
                bulk.WriteToServer(dt);
            }
            catch (Exception ex) { return "Error: " + ex; }
            return "";
        }
    }
}