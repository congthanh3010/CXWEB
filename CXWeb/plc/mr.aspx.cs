using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

namespace CXWeb.plc
{
    public partial class mr : System.Web.UI.Page
    {
        protected static DateTime dateNow;
        protected static string[] arrHour;
        protected static string plc_title;
        protected static string shift;
        protected static DateTime dStart, dMid, dEnd;

        private static connectDB conn;
        private static DataTable data, dataraw;
        //private static string dStart, dMid, dEnd;

        protected void Page_Load(object sender, EventArgs e)
        {
            conn = new connectDB();
            dateNow = DateTime.Now;
            //dateNow = new DateTime(2022, 3, 2, 5, 59, 59);
            loadData();
            if (!IsPostBack)
                ddlDep_SelectedIndexChanged(null, null);
        }

        private void loadData()
        {
            if (dateNow.Hour >= 6 && dateNow.Hour < 18)
            {
                shift = "CA1";
                arrHour = new string[12] {"06", "07", "08", "09", "10", "11",
                                          "12", "13", "14", "15", "16", "17" };
                dStart = Convert.ToDateTime(dateNow.ToString("yyyy-MM-dd 06:00:00"));
                dEnd = Convert.ToDateTime(dateNow.ToString("yyyy-MM-dd 18:00:00"));
            }
            else
            {
                shift = "CA2";
                arrHour = new string[12] { "18", "19", "20", "21", "22", "23",
                                           "00", "01", "02", "03", "04", "05" };
                if (dateNow.Hour >= 18)
                {
                    dStart = Convert.ToDateTime(dateNow.ToString("yyyy-MM-dd 18:00:00"));
                    dEnd = Convert.ToDateTime(dateNow.AddDays(1).ToString("yyyy-MM-dd 06:00:00"));
                }
                else
                {
                    dStart = Convert.ToDateTime(dateNow.AddDays(-1).ToString("yyyy-MM-dd 18:00:00"));
                    dEnd = Convert.ToDateTime(dateNow.ToString("yyyy-MM-dd 06:00:00"));
                }
            }
            ListView1.DataSource = arrHour;
            ListView1.DataBind();

            //data = new DataTable();
            //dataraw = new DataTable();
        }

        protected void ddlDep_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn.myopen();
            string strQuery = "DECLARE @rank TABLE (ordering INT IDENTITY(1,1), id VARCHAR(20))\n";
            if (ddlDep.SelectedValue.Trim() == "")
            {
                foreach (ListItem item in ddlDep.Items)
                {
                    if (item.Text.Trim() != "")
                    {
                        var arrDep = item.Value.Split(';');
                        strQuery += string.Join("\n", arrDep.Select(dep => string.Format("INSERT INTO @rank VALUES ('{0}')", dep)));
                    }
                }
            }
            else
            {
                var arrDep = ddlDep.SelectedValue.Split(';');
                strQuery += string.Join("\n", arrDep.Select(dep => string.Format("INSERT INTO @rank VALUES ('{0}')", dep)));
            }
            strQuery += "SELECT pml.DepId, pml.DepName\n"
                + "FROM PLC_MachineList pml\n"
                + "RIGHT JOIN @rank r ON r.id = pml.DepId\n"
                + "GROUP BY r.ordering, pml.DepId, pml.DepName\n"
                + "ORDER BY r.ordering";
            DataTable dt = conn.mysearch(strQuery);
            conn.myclose();

            ddlArea.DataSource = dt;
            ddlArea.DataTextField = "DepName";
            ddlArea.DataValueField = "DepId";
            ddlArea.DataBind();
            ddlArea_SelectedIndexChanged(sender, e);
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            if (chkAuto.Checked)
            {
                if (ddlArea.SelectedIndex < (ddlArea.Items.Count - 1))
                    ddlArea.SelectedIndex++;
                else
                    ddlArea.SelectedIndex = 0;
            }
            ddlArea_SelectedIndexChanged(sender, e);
        }

        protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            Timer1.Enabled = false;
            plc_title = string.Format("PLC數據統計分析{0}", ddlArea.SelectedItem.Text);
            showReport();
            Timer1.Enabled = true;
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string mid = (e.Item.FindControl("fieldId") as HiddenField).Value;
                var lstData = dataraw.Select().Where(x => x["MACHINE_CODE"].ToString() == mid).ToList();
                var result = new List<DataRow>();
                if (lstData.Count == 0 || (Convert.ToDateTime(lstData[0]["TTime"]).ToString("HH:mm") != "06:00" && shift == "CA2")
                    || (Convert.ToDateTime(lstData[0]["TTime"]).ToString("HH:mm") != "18:00" && shift == "CA1"))
                {
                    lstData.Insert(0, newRow("min min-black", Convert.ToDateTime(dStart)));
                }

                if (Convert.ToDateTime(lstData[0]["TTime"]).ToString("HH:mm") != "05:58"
                    && Convert.ToDateTime(lstData[0]["TTime"]).ToString("HH:mm") != "17:58")
                {
                    lstData.Add(newRow("", Convert.ToDateTime(dEnd).AddMinutes(-2)));
                }
                lstData.Add(newRow("min-black", Convert.ToDateTime(dEnd)));
                lstData.Add(newRow("min-black", Convert.ToDateTime(dEnd).AddMinutes(2)));

                result.Add(lstData[0]);
                for (int i = 1; i < lstData.Count - 1; i++)
                {
                    if (Convert.ToDateTime(lstData[i]["TTime"]).ToString("HH:mm") != Convert.ToDateTime(lstData[i - 1]["TTime"]).ToString("HH:mm"))
                    {
                        var date = Convert.ToDateTime(result[result.Count - 1]["TTime"]);
                        while (Convert.ToDateTime(lstData[i]["TTime"]).ToString("HH:mm") != date.AddMinutes(2).ToString("HH:mm"))
                        {
                            date = date.AddMinutes(2);
                            var css = date.Minute == 0 ? "min-black" : "";
                            result.Add(newRow(css, date));
                        }
                        result.Add(lstData[i]);
                    }
                }

                var rpt1 = (e.Item.FindControl("Repeater2") as Repeater);
                rpt1.DataSource = result.CopyToDataTable();
                rpt1.DataBind();
            }
        }

        protected void Repeater1_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            ScriptManager scriptMan = ScriptManager.GetCurrent(this);
            var lbt = e.Item.FindControl("lbtMachine") as LinkButton;
            if (lbt != null)
                scriptMan.RegisterAsyncPostBackControl(lbt);
        }

        protected void lbtMachine_Click(object sender, EventArgs e)
        {
            var lbt = sender as LinkButton;
            pnlDetail_Init();
            var row = data.Select().FirstOrDefault(x => x["MachineId"].ToString() == lbt.Text);
            if (row != null)
            {
                txtMachine.Text = row["MachineId"].ToString();
                txtRuncard.Text = row["RuncardId"].ToString();
                txtItem.Text = row["Item"].ToString();
                txtOperator.Text = row["OperatorList"].ToString().Replace(";", "\n");
                txtScheduler.Text = row["Scheduler"].ToString();

                var planQty = Convert.ToInt32(row["RuncardQty"]);
                txtPlanQty.Text = planQty > 0 ? planQty.ToString("N0") : txtPlanQty.Text;
                var iotQty = Convert.ToInt32(row["TotalQty"]);
                txtIoTQty.Text = iotQty > 0 ? iotQty.ToString("N0") : txtIoTQty.Text;
                var rptQty = Convert.ToInt32(row["ChkOutQty"]);
                txtReportQty.Text = rptQty > 0 ? rptQty.ToString("N0") : txtReportQty.Text;
                var rate = planQty != 0 ? iotQty * 100 / planQty : (iotQty != 0 ? 100 : 0);
                txtSuccessRate.Text = planQty == 0 && iotQty == 0 ? txtSuccessRate.Text : rate.ToString("N0");

                var spmRate = Convert.ToInt32(row["TotalSpm"]);
                if (spmRate >= 60 && spmRate < 80)
                    txtSpmRate.BackColor = System.Drawing.Color.DeepPink;
                else if (spmRate < 60)
                    txtSpmRate.BackColor = System.Drawing.Color.Red;
                else
                    txtSpmRate.BackColor = System.Drawing.Color.White;
                txtSpmRate.Text = spmRate > 0 ? spmRate.ToString("N0") : txtSpmRate.Text;
            }
            mpeDetail.Show();
        }

        private void pnlDetail_Init()
        {
            txtMachine.Text = "";
            txtRuncard.Text = "";
            txtItem.Text = "";
            txtPlanQty.Text = "-";
            txtIoTQty.Text = "-";
            txtReportQty.Text = "-";
            txtSuccessRate.Text = "-";
            txtSpmRate.Text = "-";
            txtSuccessRate.BackColor = System.Drawing.Color.White;
            txtOperator.Text = "";
            txtScheduler.Text = "";
        }

        private DataRow newRow(object css, object time)
        {
            var row = dataraw.NewRow();
            row["css"] = css;
            row["TTime"] = time;
            return row;
        }

        private void showReport()
        {
            conn.myopen();
            string strQuery = "SELECT MACHINE_CODE, TTime, [State], [Count] - 66 AS iCount,\n"
                + "       CASE WHEN DATEPART(mi,TTime) = 0 THEN 'min-black'\n"
                + "			WHEN [Count] > 66 OR ([State] = 2 AND MACHINE_CODE LIKE 'CN-C%') OR [State] = 1 THEN 'min-green'\n"
                + "			WHEN [State] = 2 AND [Count] = 66 THEN 'min-yellow'\n"
                + "			WHEN [State] = 3 THEN 'min-red' END AS css\n"
                + "FROM PLC_VL\n"
                + "WHERE TTime >= '" + dStart.ToString("yyyy-MM-dd HH:mm:ss") + "' AND TTime < '" + dEnd.ToString("yyyy-MM-dd HH:mm:ss") + "'\n"
                + "AND EXISTS (SELECT 1 FROM PLC_MachineList WHERE MachineId = MACHINE_CODE AND DepId = '" + ddlArea.SelectedValue.ToString() + "')\n"
                + "AND DATEPART(mi,TTime) % 2 = 0\n"
                + "ORDER BY MACHINE_CODE, TTime";
            dataraw = new DataTable();
            dataraw = conn.mysearch(strQuery);

            //strQuery = "SELECT pml.MachineId, ISNULL(plc.iSumS1, 0) AS iSumS1, ISNULL(plc.iSumS2, 0) AS iSumS2, plc.iSum,\n"
            //    + "       CONVERT(NUMERIC(10,2),(1.0*ISNULL(plc.iCountWS1,0)/720)*100) AS perS1,\n"
            //    + "       CONVERT(NUMERIC(10,2),(1.0*ISNULL(plc.iCountWS2,0)/DATEDIFF(mi,'" + dMid.ToString("yyyy-MM-dd HH:mm:ss") + "','" + dateNow.ToString("yyyy-MM-dd HH:mm:ss") + "'))*100) AS perS2,\n"
            //    + "       tcs.Item, tcs.Process, SUBSTRING(CONVERT(nvarchar,ms.dStTimeStart,120),12,5) AS dStTimeStart, msc.cDesc, pag1.GroupName AS group1, pag2.GroupName AS group2,\n"
            //    + "       CASE WHEN ISNULL(DATEDIFF(mi,plc.lastW,'" + dateNow.ToString("yyyy-MM-dd HH:mm:ss") + "'),60) >= 60 THEN 'min-red'\n"
            //    + "			WHEN ISNULL(DATEDIFF(mi,plc.lastW,'" + dateNow.ToString("yyyy-MM-dd HH:mm:ss") + "'),60) >= 30 THEN 'min-yellow' ELSE '' END AS cssLine1,\n"
            //    + "       CASE WHEN ISNULL(DATEDIFF(mi,plc.lastW,'" + dateNow.ToString("yyyy-MM-dd HH:mm:ss") + "'),60) >= 60 THEN 'min-red' ELSE '' END AS cssLine2,\n"
            //    + "       CASE WHEN pv.[State] = 1 THEN 'min-green'\n"
            //    + "			WHEN pv.[State] = 2 THEN 'min-yellow'\n"
            //    + "       ELSE 'min-red' END cssStatus, msc.cCode, plc.lastW\n"
            //    + "FROM PLC_MachineList pml\n"
            //    + "LEFT JOIN tc_5836_chkin_status tcs ON tcs.MachineCode = pml.MachineId\n"
            //    + "LEFT JOIN (\n"
            //    + "	SELECT dMachine, MAX(dStTimeStart) dStTimeStart\n"
            //    + "	FROM MACHINE_STATUS\n"
            //    + "	GROUP BY dMachine\n"
            //    + ") tmax ON tmax.dMachine = pml.MachineId\n"
            //    + "LEFT JOIN MACHINE_STATUS ms ON ms.dMachine = tmax.dMachine AND ms.dStTimeStart = tmax.dStTimeStart\n"
            //    + "LEFT JOIN MACHINE_STATUS_CODE msc ON msc.cCode = ms.dStatus\n"
            //    + "LEFT JOIN PLC_ApiGroup pag1 ON pag1.DepId = pml.DepId AND pag1.ApiType = 'Line' AND pag1.Func='PLC_Notify_v2' AND pag1.NLevel = 1\n"
            //    + "LEFT JOIN PLC_ApiGroup pag2 ON pag2.DepId = pml.DepId AND pag2.ApiType = 'Line' AND pag2.Func='PLC_Notify_v2' AND pag2.NLevel = 2\n"
            //    + "LEFT JOIN (\n"
            //    + "	SELECT MACHINE_CODE, MAX(CASE WHEN [Count] > 66 THEN TTime END) lastW, MAX(TTime) lastT,\n"
            //    + "	       SUM(CASE WHEN TTime >= '" + dStart.ToString("yyyy-MM-dd HH:mm:ss") + "' AND TTime < '" + dMid.ToString("yyyy-MM-dd HH:mm:ss") + "' THEN [Count] - 66 END) AS iSumS1,\n"
            //    + "	       SUM(CASE WHEN TTime >= '" + dMid.ToString("yyyy-MM-dd HH:mm:ss") + "' AND TTime < '" + dEnd.ToString("yyyy-MM-dd HH:mm:ss") + "' THEN [Count] - 66 END) AS iSumS2,\n"
            //    + "	       SUM(CASE WHEN TTime >= '" + dStart.ToString("yyyy-MM-dd HH:mm:ss") + "' AND TTime < '" + dEnd.ToString("yyyy-MM-dd HH:mm:ss") + "' THEN [Count] - 66 END) AS iSum,\n"
            //    + "	       SUM(CASE WHEN TTime >= '" + dStart.ToString("yyyy-MM-dd HH:mm:ss") + "' AND TTime < '" + dMid.ToString("yyyy-MM-dd HH:mm:ss") + "' AND ([Count] > 66 OR [State] = 1) THEN 1 END) AS iCountWS1,\n"
            //    + "	       SUM(CASE WHEN TTime >= '" + dMid.ToString("yyyy-MM-dd HH:mm:ss") + "' AND TTime < '" + dEnd.ToString("yyyy-MM-dd HH:mm:ss") + "' AND ([Count] > 66 OR [State] = 1) THEN 1 END) AS iCountWS2\n"
            //    + "	FROM PLC_VL\n"
            //    + "	WHERE TTime >= '" + dStart.ToString("yyyy-MM-dd HH:mm:ss") + "' AND TTime < '" + dEnd.ToString("yyyy-MM-dd HH:mm:ss") + "'\n"
            //    + "	GROUP BY MACHINE_CODE\n"
            //    + ") plc ON plc.MACHINE_CODE = pml.MachineId\n"
            //    + "LEFT JOIN PLC_VL pv ON pv.MACHINE_CODE = plc.MACHINE_CODE AND pv.TTime = plc.lastT AND DATEDIFF(mi,pv.TTime,GETDATE()) <= 3\n"
            //    + "WHERE pml.DepId = '" + ddlArea.SelectedValue.ToString() + "' AND pml.MStatus = 'Show'\n"
            //    + "ORDER BY pml.Num";
            strQuery = "mr_GetData";
            Hashtable htPara = new Hashtable();
            htPara["date"] = dStart.ToString("yyyy-MM-dd");
            htPara["shift"] = shift;
            htPara["begin"] = dStart.ToString("yyyy-MM-dd HH:mm:ss");
            htPara["end"] = dEnd.ToString("yyyy-MM-dd HH:mm:ss");
            htPara["now"] = dateNow.ToString("yyyy-MM-dd HH:mm:ss");
            htPara["dep"] = ddlArea.SelectedValue.ToString();

            data = new DataTable();
            //data = conn.mysearch(strQuery);
            data = conn.execReturnData(strQuery, htPara, CommandType.StoredProcedure);
            conn.myclose();

            data.Columns.Add("cssSpm", typeof(string));
            foreach (DataRow row in data.Rows)
            {
                if (!row.IsNull("ShiftSpm"))
                {
                    var shift_spm = Convert.ToInt32(row["ShiftSpm"]);
                    if (shift_spm < 60 && shift_spm > 0)
                        row["cssSpm"] = "min-red";
                    else if (shift_spm >= 60 && shift_spm < 80)
                        row["cssSpm"] = "min-pink";
                    else
                        row["cssSpm"] = "";
                }
            }
            Repeater1.DataSource = data;
            Repeater1.DataBind();
        }
    }
}