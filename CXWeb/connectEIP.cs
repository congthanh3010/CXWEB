using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace CXWeb
{
    public class connectEIP
    {
        public SqlConnection conn;
        string strconn = @"Data Source=10.10.20.11;Initial Catalog=EIP;Persist Security Info=True;User ID=sa;Password=sa@WEB";
      
        public void myopen()
        {
            //string strconn = @"Data Source=INTRAWEB;Initial Catalog=EIP;Persist Security Info=True;User ID=sa;Password=sa@WEB";
            conn = new SqlConnection(strconn);
            
            //using (conn = new SqlConnection(strconn))
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

        public  DataTable mysearch(string strsql)
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
        public  DataTable ExecuteReturnDt(string strSQLExec, Hashtable htSQLPara, CommandType cmdType)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = strSQLExec;
            sqlCommand.CommandType = cmdType;
            sqlCommand.CommandTimeout = 0;
            if (htSQLPara.Count > 0)
            {
                if (sqlCommand.CommandType == CommandType.StoredProcedure)
                {
                    myopen();
                    DataTable dt = mysearch("Select UPPER(Name) as Name FROM Sys.Parameters where Object_id = Object_id('" + strSQLExec + "')");

                    foreach (DataRow row in dt.Rows)
                    {
                        string upper = ((string)row["Name"]).Replace("@", "").ToUpper();
                        if (htSQLPara.Contains((object)upper))
                            sqlCommand.Parameters.AddWithValue("@" + upper, htSQLPara[(object)upper]);
                    }
                }
            }
            sqlCommand.Connection = conn;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
           
            DataTable dataTable = new DataTable();
            try
            {
                sqlDataAdapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
               
                return (DataTable)null;
            }
            myclose();
            return dataTable;
        }

        public  DataSet ExecuteReturnDs(string strSQLExec, Hashtable htSQLPara, CommandType cmdType)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = strSQLExec;
            sqlCommand.CommandType = cmdType;
            if (htSQLPara.Count > 0)
            {
                if (sqlCommand.CommandType == CommandType.StoredProcedure)
                {
                    myopen();
                    DataTable dt = mysearch("Select UPPER(Name) as Name FROM Sys.Parameters where Object_id = Object_id('" + strSQLExec + "')");

                    foreach (DataRow row in dt.Rows)
                    {
                        string upper = ((string)row["Name"]).Replace("@", "").ToUpper();
                        if (htSQLPara.Contains((object)upper))
                            sqlCommand.Parameters.AddWithValue("@" + upper, htSQLPara[(object)upper]);
                    }
                }
               
            }
            sqlCommand.Connection = conn;
            sqlCommand.CommandTimeout = 0;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataSet dataSet = new DataSet();
            try
            {
                sqlDataAdapter.Fill(dataSet);
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show("Error :" + ex.Message);
                string str = string.Empty;
                for (int index = 0; index < sqlCommand.Parameters.Count - 1; ++index)
                    str = str + (str == string.Empty ? " " : ",") + (sqlCommand.Parameters[index].Value.GetType().Name == "String" ? "'" : "") + sqlCommand.Parameters[index].Value.ToString() + (sqlCommand.Parameters[index].Value.GetType().Name == "String" ? "'" : "");
                Clipboard.SetText(sqlCommand.CommandText + " " + str);
                return (DataSet)null;
            }
          
            myclose();
            return dataSet;
        }

        public  DataTable ExecuteReturnDt(string strSQLExec, CommandType cmdType)
        {
            return ExecuteReturnDt(strSQLExec, new Hashtable(), cmdType);
        }

        public string BulkCopy(DataTable dt, string tabName)
        {
            try
            {
                // Bulk Copy to SQL Server
                SqlBulkCopy bulk = new SqlBulkCopy(strconn);
                bulk.DestinationTableName = tabName;
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