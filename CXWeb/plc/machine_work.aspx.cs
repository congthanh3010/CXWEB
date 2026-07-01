using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Collections;
using System.Data;
using Newtonsoft.Json;

namespace CXWeb.plc
{
    public partial class machine_work : System.Web.UI.Page
    {
        protected connectDB connect;
        protected DataTable data;
        protected string[] mcolor = new string[] { "gray", "green", "yellow", "red" };
        protected int index;

        protected void Page_Load(object sender, EventArgs e)
        {
            string mid = base.Request.QueryString["mid"];
            if (mid != null)
            {
                connect = new connectDB();
                connect.myopen();
                string sdate;
                DateTime now = DateTime.Now;
                if (now.Hour >= 6 && now.Hour < 18)
                    sdate = now.AddDays(-1).ToString("yyyy-MM-dd") + " 18:00:00";
                else
                    sdate = now.ToString("yyyy-MM-dd") + " 06:00:00";
                string squery = string.Format(@"DECLARE @machine_id VARCHAR(15) = '{0}'
                    SELECT TOP 1 pml.MachineId, pv.TTime, pv.[State], tt.Process, tt.ProcessName, tt.item
                            , gen.gen01 + '-' + gen.gen02 AS dCreateUser, FLOOR(eip.productivity/24*10) pro10h, pmax.iCount
                            , ISNULL(ms.dStatus,'') dStatus, ISNULL(msc.cDesc,'') cDesc
                    FROM PLC_MachineList pml
                    LEFT JOIN (
                        SELECT MACHINE_CODE, MAX(CASE WHEN TTime BETWEEN DATEADD(mi,-2,GETDATE()) AND GETDATE() THEN TTime END) TTime, SUM([Count] - 66) iCount
                        FROM PLC_VL
                        WHERE MACHINE_CODE = @machine_id
                        AND TTime BETWEEN '{1}' AND GETDATE()
                        GROUP BY MACHINE_CODE) pmax ON pmax.MACHINE_CODE = pml.MachineId
                    LEFT JOIN PLC_VL pv ON pv.MACHINE_CODE = pmax.MACHINE_CODE AND pv.TTime = pmax.TTime
                    LEFT JOIN OPENQUERY(EIP, 'SELECT machine_no, productivity FROM schedule_ma_prod_setting') eip ON eip.machine_no = pml.MachineId
                    LEFT JOIN OPENQUERY(TIPTOP,
                        'select eci01 as MachineCode
                                , f_1444_ckinlst(''3'', eci01) as Process
                                , f_1444_ckinlst(''4'', eci01) as ProcessName
                                , substr(f_1444_ckinlst(''5'', eci01), 2, 7) as item
                        from eci_file
                        where f_1444_ckinlst(''1'', eci01) is not null
                        and eci01 = ''{0}''') tt ON tt.MachineCode = pml.MachineId
                    LEFT JOIN MACHINE_STATUS ms ON ms.dMachine = pml.MachineId AND ms.dStTimeStart >= '{1}'
                    LEFT JOIN MACHINE_STATUS_CODE msc ON msc.cCode = ms.dStatus
                    LEFT JOIN OPENQUERY(TIPTOP, 'SELECT gen01, gen02 FROM gen_file') gen ON gen.gen01 = ms.dCreateUser
                    WHERE pml.MachineId = @machine_id", mid, sdate);
                data = connect.mysearch(squery);
                index = 0;
                if (data.Rows.Count > 0)
                    index = data.Rows[0].IsNull("State") ? 0 : Convert.ToInt16(data.Rows[0]["State"]);
                rptDetail.DataSource = data;
                rptDetail.DataBind();
                connect.myclose();
            }
        }

        [WebMethod, ScriptMethod(UseHttpGet = false)]
        public static string GetMachineWork(string id)
        {
            connectDB connect = new connectDB();
            string strExec = "sp_load_machine_works";
            DateTime now = DateTime.Now;
            DateTime begin;
            if (now.Hour >= 6 && now.Hour < 18)
                begin = Convert.ToDateTime(now.AddDays(-1).ToString("yyyy-MM-dd") + " 18:00:00");
            else
                begin = Convert.ToDateTime(now.ToString("yyyy-MM-dd") + " 06:00:00");
            var para = new Hashtable();
            para["@machine_id"] = id;
            para["@begin"] = begin;


            connect.myopen();
            DataTable dt = new DataTable();
            dt = connect.execReturnData(strExec, para, CommandType.StoredProcedure);
            connect.myclose();

            var serializer = new JavaScriptSerializer();
            var rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                    row.Add(col.ColumnName, dr[col]);
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }
    }
}