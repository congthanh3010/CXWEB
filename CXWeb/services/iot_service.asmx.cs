using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.Net;
using System.Globalization;
using System.Data;

namespace CXWeb.services
{
    public class ResponseContent
    {
        public int quantity { get; set; }
    }

    /// <summary>
    /// Summary description for iot_service
    /// </summary>
    [WebService(Namespace = "http://eip.cxtechnology.vn/services/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class iot_service : System.Web.Services.WebService
    {
        private static connectDB conn = new connectDB();

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetIotQuantityJson(string machine_id, string begin_date, string begin_time, string end_date, string end_time)
        {
            string message = "";
            if (!isHaveMachine(machine_id))
                message = "Machine not found.";
            else if (!isDateFormat(begin_date))
                message = "Begin date format not correct.";
            else if (!isDateFormat(end_date))
                message = "End date format not correct.";
            else if (!isTimeFormat(begin_time))
                message = "Begin time format not correct.";
            else if (!isTimeFormat(end_time))
                message = "End time format not correct.";

            ResponseContent response = new ResponseContent();
            if (string.IsNullOrWhiteSpace(message))
                response.quantity = IotQuantity(machine_id, begin_date, begin_time, end_date, end_time);

            JavaScriptSerializer js = new JavaScriptSerializer();
            string responseString = js.Serialize(response);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.AddHeader("content-length", responseString.Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(responseString);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        [WebMethod]
        public int GetIotQuantityXML(string machine_id, string begin_date, string begin_time, string end_date, string end_time)
        {
            if (isHaveMachine(machine_id) && isDateFormat(begin_date) && isDateFormat(end_date)
                && isTimeFormat(begin_time) && isTimeFormat(end_time))
                return IotQuantity(machine_id, begin_date, begin_time, end_date, end_time);
            return 0;
        }

        [WebMethod]
        public void InsertToSQL(DataSet dataSet)
        {
            try
            {
                insertToSQL(dataSet);
                HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.OK;
                HttpContext.Current.Response.Write("Done.");
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.NotModified;
                HttpContext.Current.Response.Write("Fail: " + ex.Message);
            }
        }

        [WebMethod]
        public DataTable GetDataSQL(string key)
        {
            return getData(key);
        }

        private int IotQuantity(string machine_id, string begin_date, string begin_time, string end_date, string end_time)
        {
            string bdate = DateTime.ParseExact(begin_date, "yyyyMMdd", null).ToString("yyyy-MM-dd");
            string edate = DateTime.ParseExact(end_date, "yyyyMMdd", null).ToString("yyyy-MM-dd");
            string btime = DateTime.ParseExact(begin_time, "HHmmss", null).ToString("HH:mm:ss");
            string etime = DateTime.ParseExact(end_time, "HHmmss", null).ToString("HH:mm:ss");
            string strQuery = "SELECT ISNULL(SUM([Count] - 66), 0) * ISNULL((SELECT DISTINCT CountBase FROM PLC_MachineList WHERE MachineId = MACHINE_CODE), 1) \n"
                + "FROM PLC_VL \n"
                + $"WHERE MACHINE_CODE = '{machine_id.ToUpper()}' \n"
                + $"AND TTime BETWEEN '{bdate} {btime}' AND '{edate} {etime}' \n"
                + "GROUP BY MACHINE_CODE";
            conn.myopen();
            DataTable data = conn.mysearch(strQuery);
            conn.myclose();

            return Convert.ToInt32(data.Rows[0][0]);
        }

        private bool isHaveMachine(string machine_id)
        {
            string strQuery = $"SELECT 1 FROM PLC_MachineList WHERE MachineId = '{machine_id.ToUpper()}'";
            conn.myopen();
            DataTable data = conn.mysearch(strQuery);
            conn.myclose();
            return data.Rows.Count > 0;
        }

        private bool isDateFormat(string date)
        {
            DateTime result;
            return DateTime.TryParseExact(date, "yyyyMMdd", null, DateTimeStyles.None, out result);
        }

        private bool isTimeFormat(string time)
        {
            DateTime result;
            return DateTime.TryParseExact(time, "HHmmss", null, DateTimeStyles.None, out result);
        }

        private void insertToSQL(DataSet dataSet)
        {
            if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                conn.myopen();
                string strQuery;
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    strQuery = "INSERT INTO Test_WebService (Column1, Column2, Column3) " 
                        + $"VALUES ('{row[0]}', {row[1]}, {row[2]})";
                    conn.mySqlExecute(strQuery);
                }
                conn.myclose();
            }
        }

        private DataTable getData(string key)
        {
            string strQuery = $"SELECT * FROM Test_WebService WHERE column1 = '{key}'";
            conn.myopen();
            DataTable data = conn.mysearch(strQuery);
            conn.myclose();
            return data;
        }
    }
}
