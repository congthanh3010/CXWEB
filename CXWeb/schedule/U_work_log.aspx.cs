using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Data;

namespace CXWeb.schedule
{
    public partial class U_work_log : System.Web.UI.Page
    {
        private static connectEIP connEIP;

        private static string pcname;
        private static string ip;
        private static DateTime wdate;
        private static string shift;
        private static DataTable wlog;
        private static string id;
        private static int index = -1;
        private static string session;

        protected void Page_Load(object sender, EventArgs e)
        {
            checkCookie();
            if (!IsPostBack)
                load();
        }

        private void load()
        {
            getWorkDate();

            //IPHostEntry host = Dns.GetHostEntry(Request.ServerVariables["REMOTE_HOST"]);
            //pcname = host.HostName;
            //foreach (IPAddress addr in host.AddressList)
            //{
            //    if (addr.AddressFamily.ToString() == "InterNetwork")
            //        ip = addr.ToString();
            //}
            ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!string.IsNullOrEmpty(ip))
            {
                string[] addresses = ip.Split(',');
                if (addresses.Length != 0)
                    ip = addresses[0];
            }
            else
                ip = Request.ServerVariables["REMOTE_ADDR"];
            pcname = Request.ServerVariables["REMOTE_HOST"];

            connEIP = new connectEIP();
            wlog = new DataTable();
            index = 0;
            loadData();
        }

        private void loadData()
        {
            wlog = getLogInput();
            grvWLog.DataSource = null;
            grvWLog.DataSource = wlog;
            grvWLog.DataBind();
            closeEdit();
            clear();
            if (wlog.Rows.Count > 0)
            {
                grvWLog.SelectedIndex = index;
                loadLog(grvWLog.SelectedDataKey.Value.ToString());
            }
        }

        private void loadLog(string Id)
        {
            var log = wlog.Select().FirstOrDefault(x => x["Id"].ToString() == Id);
            if (log != null)
            {
                id = log["Id"].ToString();
                txtDate.Text = Convert.ToDateTime(log["WorkDate"]).ToString("yyyy-MM-dd");
                ddlShift.SelectedValue = log["WorkShift"].ToString();
                ddlCode.SelectedValue = log["Employee"].ToString().Substring(0, 2);
                txtEmployee.Text = log["Employee"].ToString().Substring(2, log["Employee"].ToString().Length - 2);
                ddlMachine.SelectedValue = log["MachineId"].ToString();
                txtRunCard.Text = log["RuncardNo"].ToString();
                txtRecordNo.Text = log["RecordNo"].ToString();
                txtItem.Text = log["ItemNo"].ToString();
                txtStep.Text = log["StepNo"].ToString();
                txtProWeight.Text = log["ProWeight"].ToString();
                txtRealWeight.Text = log["RealWeight"].ToString();
                txtQty.Text = log["Qty"].ToString();
                txtImpTime.Text = Convert.ToDateTime(log["ImportTime"]).ToString("HH:mm");
                txtExpTime.Text = Convert.ToDateTime(log["ExportTime"]).ToString("HH:mm");
                hdfSpan.Value = log["TotalTime"].ToString() != "" ? Convert.ToInt16(log["TotalTime"]).ToString() : string.Empty;
                txtSpan.Text = Convert.ToInt16(log["TotalTime"]).ToString();
                txtTempe1.Text = log["Tempe1"].ToString() != "" ? Convert.ToInt16(log["Tempe1"]).ToString() : string.Empty;
                txtTempe2.Text = log["Tempe2"].ToString() != "" ? Convert.ToInt16(log["Tempe2"]).ToString() : string.Empty;
                txtTempe3.Text = log["Tempe3"].ToString() != "" ? Convert.ToInt16(log["Tempe3"]).ToString() : string.Empty;
                txtRemark.Text = log["Remark"].ToString();
            }
            else
            {
                clear();
            }
        }

        private DataTable getLogInput()
        {
            DataTable result = new DataTable();
            try
            {
                string strQuery = "SELECT * FROM U_work_log\n"
                    + "WHERE MachineId IS NOT NULL AND SessionId='" + session + "'\n"
                    + "AND (WorkDate='" + wdate.ToString("yyyy-MM-dd") + "' OR CONVERT(VARCHAR,CreateDate,111)='" + DateTime.Now.ToString("yyyy/MM/dd") + "')\n"
                    + "ORDER BY WorkDate DESC, MachineId, WorkShift, CreateDate DESC";
                connEIP.myopen();
                result = connEIP.mysearch(strQuery);
                connEIP.myclose();
            }
            catch { }
            return result;
        }

        private DataTable getRuncard(string runcard, string machine)
        {
            DataTable result = new DataTable();
            try
            {
                //string strQuery = "SELECT sf.sgm01 AS RuncardNo, sf.sgm02 AS RecordNo, sf.sgm03_par AS ItemNo,\n"
                //    + "       sf.sgm04 AS StepNo, f_1104_bmb06(sf.sgm03_par) * 1000 AS ProWeight\n"
                //    + "FROM sgm_file sf\n"
                //    + "WHERE sf.sgm01 LIKE '%" + runcard + "%'\n"
                //    + "AND EXISTS (SELECT 1 FROM eci_file ef WHERE ef.eci03 = sf.sgm06 AND ef.eci01 = '" + machine + "')";
                string strQuery = "SELECT *\n"
                    + "FROM OPENQUERY(TIPTOP, 'SELECT sf.sgm01 AS RuncardNo, sf.sgm02 AS RecordNo, sf.sgm03_par AS ItemNo,\n"
                    + "       sf.sgm04 AS StepNo, f_1104_bmb06(sf.sgm03_par) * 1000 AS ProWeight\n"
                    + "FROM sgm_file sf\n"
                    + "WHERE sf.sgm01 LIKE ''%" + runcard + "%''\n"
                    + "AND EXISTS (SELECT 1 FROM eci_file ef WHERE ef.eci03 = sf.sgm06 AND ef.eci01 LIKE ''%LU%'')')";
                connEIP.myopen();
                result = connEIP.mysearch(strQuery);
                connEIP.myclose();
            }
            catch { }
            return result;
        }

        private void getWorkDate()
        {
            DateTime date = DateTime.Now;
            if (date.Hour < 6)
            {
                wdate = date.AddDays(-1);
                shift = "CA2";
            }
            else
            {
                wdate = date;
                if (date.Hour >= 6 && date.Hour < 18)
                    shift = "CA1";
                else
                    shift = "CA2";
            }
        }

        private void clear()
        {
            txtDate.Text = "";
            ddlShift.SelectedValue = "CA1";
            ddlCode.SelectedIndex = 0;
            txtEmployee.Text = "";
            ddlMachine.SelectedIndex = 0;
            txtRunCard.Text = "";
            txtRecordNo.Text = "";
            txtItem.Text = "";
            txtStep.Text = "";
            txtProWeight.Text = "";
            txtRealWeight.Text = "";
            txtQty.Text = "";
            txtImpTime.Text = "";
            txtExpTime.Text = "";
            hdfSpan.Value = "";
            txtSpan.Text = "";
            txtTempe1.Text = "";
            txtTempe2.Text = "";
            txtTempe3.Text = "";
            txtRemark.Text = "";
        }

        private void openEdit()
        {
            txtDate.Enabled = true;
            ddlShift.Enabled = true;
            ddlCode.Enabled = true;
            txtEmployee.Enabled = true;
            ddlMachine.Enabled = true;
            txtRecordNo.Enabled = true;
            txtRunCard.Enabled = true;
            txtItem.Enabled = true;
            txtStep.Enabled = true;
            txtProWeight.Enabled = true;
            txtRealWeight.Enabled = true;
            txtQty.Enabled = true;
            txtBarrelWeight.Enabled = true;
            txtImpTime.Enabled = true;
            txtExpTime.Enabled = true;
            txtSpan.Enabled = true;
            txtTempe1.Enabled = true;
            txtTempe2.Enabled = true;
            txtTempe3.Enabled = true;
            txtRemark.Enabled = true;

            btnSave.Enabled = true;
            btnCancel.Enabled = true;

            lblMessage.Text = "";
        }

        private void closeEdit()
        {
            txtDate.Enabled = false;
            ddlShift.Enabled = false;
            ddlCode.Enabled = false;
            txtEmployee.Enabled = false;
            ddlMachine.Enabled = false;
            txtRecordNo.Enabled = false;
            txtRunCard.Enabled = false;
            txtItem.Enabled = false;
            txtStep.Enabled = false;
            txtProWeight.Enabled = false;
            txtRealWeight.Enabled = false;
            txtQty.Enabled = false;
            txtBarrelWeight.Enabled = false;
            txtImpTime.Enabled = false;
            txtExpTime.Enabled = false;
            txtSpan.Enabled = false;
            txtTempe1.Enabled = false;
            txtTempe2.Enabled = false;
            txtTempe3.Enabled = false;
            txtRemark.Enabled = false;

            btnInsert.Enabled = true;
            if (wlog.Rows.Count > 0)
            {
                btnModify.Enabled = true;
                btnDelete.Enabled = true;
            }
            else
            {
                btnModify.Enabled = false;
                btnDelete.Enabled = false;
            }
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            openEdit();
            clear();
            state.Value = "insert";

            // Create new record and get Id
            string strQuery = "EXEC sp_Edit_U_Work 'ins', NULL, NULL, NULL, NULL, NULL, NULL,\n"
                + "     NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,\n"
                + "     NULL, NULL, NULL, NULL, '" + ip + "', '" + pcname + "', '" + session + "'";
            connEIP.myopen();
            DataTable dt = connEIP.mysearch(strQuery);
            id = dt.Rows[0]["Id"].ToString();
            connEIP.myclose();

            btnInsert.Enabled = false;
            btnModify.Enabled = false;
            btnDelete.Enabled = false;

            getWorkDate();
            txtDate.Text = wdate.ToString("yyyy-MM-dd");
            ddlShift.SelectedValue = shift;
            txtEmployee.Focus();
        }

        protected void btnModify_Click(object sender, EventArgs e)
        {
            openEdit();
            state.Value = "modify";

            btnInsert.Enabled = false;
            btnModify.Enabled = false;
            btnDelete.Enabled = false;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string strQuery = "EXEC sp_Edit_U_Work 'upd', '" + id + "', NULL, NULL, NULL, NULL, NULL,\n"
                    + "     NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,\n"
                    + "     NULL, NULL, NULL, NULL, '" + ip + "', '" + pcname + "', '" + session + "'";
            connEIP.myopen();
            connEIP.mySqlExecute(strQuery);
            connEIP.myclose();
            lblMessage.Text = "";
            index = 0;
            loadData();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string msg = "";
            if (txtEmployee.Text.Trim() == "")
                msg = "Chưa nhập Mã thao tác viên";
            else if (txtRecordNo.Text.Trim() == "")
                msg = "Chưa nhập Mã công đơn";
            else if (txtRealWeight.Text.Trim() == "")
                msg = "Chưa nhập Trọng lượng vô lò";
            else if (txtQty.Text.Trim() == "")
                msg = "Chưa nhập Số lượng thực tế";
            else if (txtImpTime.Text.Trim() == "")
                msg = "Chưa nhập Thời gian vô lò";
            else if (txtExpTime.Text.Trim() == "")
                msg = "Chưa nhập Thời gian ra lò";
            else if (txtTempe1.Text.Trim() == "" && txtTempe2.Text.Trim() == "" && txtTempe3.Text.Trim() == "")
                msg = "Chưa nhập Nhiệt độ";
            if (msg != "")
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = msg;
            }
            else
            {
                try
                {
                    DateTime impTime = Convert.ToDateTime(txtDate.Text.Trim() + " " + txtImpTime.Text.Trim());
                    DateTime expTime = Convert.ToDateTime(txtDate.Text.Trim() + " " + txtExpTime.Text.Trim());
                    if (ddlShift.SelectedValue == "CA2")
                    {
                        if (impTime.Hour < 18)
                            impTime = impTime.AddDays(1);
                        if (expTime.Hour < 18)
                            expTime = expTime.AddDays(1);
                    }
                    if (impTime > expTime)
                        expTime = expTime.AddDays(1);
                    string strImpTime = impTime.ToString("yyyy-MM-dd HH:mm:ss");
                    string strExpTime = expTime.ToString("yyyy-MM-dd HH:mm:ss");

                    string strQuery = "EXEC sp_Edit_U_Work\n"
                        + " 'upd',\n"
                        + " '" + id + "',\n"
                        + " '" + txtDate.Text.Trim() + "',\n"
                        + " '" + ddlShift.SelectedValue + "',\n"
                        + " '" + ddlCode.SelectedValue + txtEmployee.Text.Trim() + "',\n"
                        + " '" + ddlMachine.SelectedValue + "',\n"
                        + " '" + txtRecordNo.Text.Trim() + "',\n"
                        + " '" + txtRunCard.Text.Trim() + "',\n"
                        + " '" + txtItem.Text.Trim() + "',\n"
                        + " '" + txtStep.Text.Trim() + "',\n"
                        + " " + txtProWeight.Text.Trim() + ",\n"
                        + " " + txtRealWeight.Text.Trim() + ",\n"
                        + " " + txtQty.Text.Trim() + ",\n"
                        + " " + (txtBarrelWeight.Text.Trim() == "" ? "NULL" : txtBarrelWeight.Text.Trim()) + ",\n"
                        + " '" + strImpTime + "',\n"
                        + " '" + strExpTime + "',\n"
                        + " " + hdfSpan.Value + ",\n"
                        + " " + (txtTempe1.Text.Trim() == "" ? "NULL" : txtTempe1.Text.Trim()) + ",\n"
                        + " " + (txtTempe2.Text.Trim() == "" ? "NULL" : txtTempe2.Text.Trim()) + ",\n"
                        + " " + (txtTempe3.Text.Trim() == "" ? "NULL" : txtTempe3.Text.Trim()) + ",\n"
                        + " N'" + txtRemark.Text.Trim() + "',\n"
                        + " '" + ip + "',\n"
                        + " '" + pcname + "',\n"
                        + " '" + session + "'";
                    connEIP.myopen();
                    connEIP.mySqlExecute(strQuery);
                    connEIP.myclose();
                    state.Value = "saved";
                }
                catch
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "Xảy ra lỗi khi lưu ghi chép";
                    return;
                }
                lblMessage.ForeColor = System.Drawing.Color.Blue;
                lblMessage.Text = "Đã lưu ghi chép lúc " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                loadData();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (state.Value == "insert")
            {
                string strQuery = "EXEC sp_Edit_U_Work 'upd', '" + id + "', NULL, NULL, NULL, NULL, NULL,\n"
                    + "     NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,\n"
                    + "     NULL, NULL, NULL, NULL, '" + ip + "', '" + pcname + "', '" + session + "'";
                connEIP.myopen();
                connEIP.mySqlExecute(strQuery);
                connEIP.myclose();
            }
            state.Value = "cancel";
            loadData();
        }

        protected void txtRunCard_TextChanged(object sender, EventArgs e)
        {
            if (txtRunCard.Text.Trim() != "")
            {
                DataTable dt = getRuncard(txtRunCard.Text.Trim(), ddlMachine.Text);
                if (dt.Rows.Count > 0)
                {
                    txtRunCard.Text = dt.Rows[0]["RuncardNo"].ToString();
                    txtRecordNo.Text = dt.Rows[0]["RecordNo"].ToString();
                    txtItem.Text = dt.Rows[0]["ItemNo"].ToString();
                    txtStep.Text = dt.Rows[0]["StepNo"].ToString();
                    txtProWeight.Text = dt.Rows[0]["ProWeight"].ToString();
                    txtRealWeight.Focus();
                }
            }
        }

        protected void grvWLog_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grvWLog.SelectedIndex >= 0)
            {
                index = grvWLog.SelectedIndex;
                loadLog(grvWLog.SelectedDataKey.Value.ToString());
            }
        }

        protected void grvWLog_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grvWLog, "Select$" + e.Row.RowIndex);
        }

        private string randomString(int length)
        {
            const string chars = "1234567890qwertyuiopasdfghjklzxcvbnm";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void checkCookie()
        {
            if (Request.Cookies["U_work"] != null)
            {
                session = Request.Cookies["U_work"].Value;
            }
            else
            {
                string value = randomString(32);
                var end = Convert.ToDateTime(DateTime.Now.AddDays(30).ToString("yyyy-MM-dd 06:00:00"));
                HttpCookie cookie = new HttpCookie("U_work");
                cookie.Value = value;
                cookie.Expires = end;
                Response.Cookies.Add(cookie);
                session = value;
            }
        }
    }
}