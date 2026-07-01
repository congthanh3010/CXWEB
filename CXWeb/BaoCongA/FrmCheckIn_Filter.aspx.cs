//using AForge.Video;
//using AForge.Video.DirectShow;
using System;
using System.Data;
using System.Drawing;
using System.Media;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using ZXing;


namespace CXWeb.BaoCong
{
    public partial class FrmCheckIn_Filter : System.Web.UI.Page
    {
        private static connectEIP connEIP;
        private static connectDB connPLC;
        private static string strMode;
        private static string pcname;
        private static string ip;
        private static DateTime wdate;
        private static string shift;
        private static DataTable wlog;
        private static string id;
        private static int index = -1;
        private static string session;
        private static string strmsg;
        private static string Ma_Id = "1";
        private static DateTime impTime;
        private static DateTime expTime;

        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
                load();
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            OpenEdit();
            txtRunCard.Focus();
            clear();
            state.Value = "insert";
            strMode = "N";
            btnInsert.Enabled = false;
            btnModify.Enabled = false;
            btnDelete.Enabled = false;

            txtBeginDate.Text = wdate.ToString("yyyy-MM-dd");
            txtEndDate.Text = wdate.ToString("yyyy-MM-dd");
            txtEndGio.Text = "00:00";
            txtBeginGio.Text = "00:00";
        }

        protected void btnModify_Click(object sender, EventArgs e)
        {
            if(txtRunCard.Text != "")
            {
                OpenEdit();
                state.Value = "modify";
                strMode = "E";
                btnInsert.Enabled = false;
                btnModify.Enabled = false;
                btnDelete.Enabled = false;
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
            }
           
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //Edit_Datat
            if(strMode == "M")
            {
                try
                {
                    impTime = Convert.ToDateTime(txtBeginDate.Text.Trim() + " " + txtBeginGio.Text.Trim());
                    expTime = Convert.ToDateTime(txtEndDate.Text.Trim() + " " + txtEndGio.Text.Trim());
                }
                catch
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "Thời gian nhập sai, vui lòng kiểm tra !!!";
                    return;
                }

                string strImpTime = impTime.ToString("yyyy-MM-dd HH:mm:ss");
                string strExpTime = expTime.ToString("yyyy-MM-dd HH:mm:ss");

                strMode = "D";
                if (strMode == null)
                    strMode = "N";
                bool result = Edit_Datat(strMode, strImpTime, strExpTime);
                if (result == true)
                {
                    lblMessage.Text = "Xóa thành công !!!";
                    strMode = "";
                    state.Value = "saved";
                    loadData();
                }
                else
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "Xảy ra lỗi khi xóa ";
                    return;
                }
            }
            else
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Không có dữ liệu để xóa !!!";
                return;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string msg = "";
            if (txtEmployee.Text.Trim() == "")
                msg = "Chưa nhập Mã thao tác viên";
            else if (txtRunCard.Text.Trim() == "")
                msg = "Chưa nhập Mã Runcard";
            else if (txtProcess.Text.Trim() == "")
                msg = "Chưa nhập Mã cộng đoạn";
            else if (txtMachineId.Text.Trim() == "")
                msg = "Chưa nhập Mã máy";
            else if (txtBeginDate.Text.Trim() == "")
                msg = "Chưa nhập Thời gian bắt đầu";
            else if (txtEndDate.Text.Trim() == "")
                msg = "Chưa nhập Thời gian kết thúc";


            //Kiem tra mã runcard
            DataTable dtRunCard = getRuncard(txtRunCard.Text.Trim(), txtMachineId.Text);
            if (dtRunCard.Rows.Count < 1)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Vui lòng kiểm tra lại mã Runcard";
                txtRunCard.Focus();
                return;
            }

            // Kiem tra ma may
            if (txtMachineId.Text.Trim() != "")
            {
                DataTable dt = new DataTable();
                dt = getMachine(txtMachineId.Text);

                if (dt.Rows.Count < 1)
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "Mã máy này không tồn tại !!!";
                    return;
                }

            }

            // Kiem tra cong doan
            if (txtProcess.Text.Trim() != "")
            {
                DataTable dt = getProcess(txtProcess.Text.ToUpper());
                if (dt.Rows.Count < 1)
                {
                    lblMessage.Text = "Vui lòng kiểm tra lại mã Công đoạn";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    txtProcess.Focus();
                    return;
                }
             }

            if (msg != "")
            {
              
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = msg;
            }
            else
            {
                try
                {
                    
                    try
                    {
                        impTime = Convert.ToDateTime(txtBeginDate.Text.Trim() + " " + txtBeginGio.Text.Trim());
                        expTime = Convert.ToDateTime(txtEndDate.Text.Trim() + " " + txtEndGio.Text.Trim());
                    }
                    catch
                    {
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        lblMessage.Text = "Thời gian nhập sai, vui lòng kiểm tra !!!";
                        return;
                    }
                    
                    string strImpTime = impTime.ToString("yyyy-MM-dd HH:mm:ss");
                    string strExpTime = expTime.ToString("yyyy-MM-dd HH:mm:ss");

                    if (impTime > expTime)
                    {
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        lblMessage.Text = "Thời Gian bắt đầu phải nhỏ hơn thời gian kết thúc !!!";
                        return;
                    }

                    if(expTime > DateTime.Now)
                    {
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        lblMessage.Text = "Thời Gian kết thúc lớn hơn thời gian hiện tại. Vui lòng kiểm tra lại !!!";
                        return;
                    }
                    // Kiểm tra thời gian lưu
                    //bool bKiemtra = KiemtraBeginDate(strImpTime, txtMachineId.Text);
                    //if (bKiemtra == false)
                    //{
                        
                    //    if (System.Windows.Forms.MessageBox.Show(strmsg, "Go Home",
                    //           System.Windows.Forms.MessageBoxButtons.YesNo,
                    //           System.Windows.Forms.MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.No)
                    //    {
                    //        lblMessage.ForeColor = System.Drawing.Color.Red;
                    //        return;
                    //    } 
                    //    else
                    //    {
                    //        lblMessage.Text = "";
                    //    } 
                                             
                    //}

                    if (strMode == null)
                        strMode = "N";
                    bool result = Edit_Datat(strMode, strImpTime, strExpTime);
                    if(result == true)
                    {
                        lblMessage.Text = "Lưu thành công !!!";
                        strMode = "";
                        state.Value = "saved";
                    }
                    else
                    {
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        lblMessage.Text = "Xảy ra lỗi khi lưu ghi chép";
                        return;
                    }

                        
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
            clear();
            btnInsert.Enabled = true;
            btnSave.Enabled = false;
            btnModify.Enabled = false;
            btnCancel.Enabled = false;
            loadData();
            strMode = "";
            lblMessage.Text = "";
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {

        }
        protected void grvWLog_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grvWLog, "Select$" + e.Row.RowIndex);
        }
        protected void grvWLog_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grvWLog.SelectedIndex >= 0 && (strMode != "E" && strMode != "N"))
            {
                index = grvWLog.SelectedIndex;
                loadLog(grvWLog.SelectedDataKey.Value.ToString());
                strMode = "M"; 
            }
        }
        #region Sự kiện
        private void clear()
        {
            txtRunCard.Text = "";
            txtProcess.Text = "";
            txtEmployee.Text = "";
            txtMachineId.Text = "";
            txtBeginDate.Text = "";
            txtBeginGio.Text = "";
            txtEndDate.Text = "";
            txtEndGio.Text = "";


        }
        private void OpenEdit()
        {
            txtRunCard.Enabled = true;
            txtProcess.Enabled = true;
            txtEmployee.Enabled = true;
            txtMachineId.Enabled = true;
            txtBeginDate.Enabled = true;
            txtBeginGio.Enabled = true;
            txtEndDate.Enabled = true;
            txtEndGio.Enabled = true;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            
            lblMessage.Text = "";
        }
        private void CloseEdit()
        {
            txtRunCard.Enabled = false;
            txtProcess.Enabled = false;
            txtEmployee.Enabled = false;
            txtMachineId.Enabled = false;
            txtBeginDate.Enabled = false;
            txtBeginGio.Enabled = false;
            txtEndDate.Enabled = false;
            txtEndGio.Enabled = false;

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
        private void loadData()
        {
            wlog = getLogInput();
            grvWLog.DataSource = null;
            grvWLog.DataSource = wlog;
            grvWLog.DataBind();
            CloseEdit();
            clear();            
        }
        private DataTable getLogInput()
        {
            DataTable result = new DataTable();
            try
            {
                string strQuery = "SELECT * FROM BaoCong_CheckIn where   Ngay_Nhap >= DATEADD(DD,-3,'" + wdate.ToString("yyyy-MM-dd") + "') and Ngay_Nhap <= '" + wdate.ToString("yyyy-MM-dd") + "' ORDER BY Ngay_Nhap desc, Ident00 desc";
                connPLC.myopen();
                result = connPLC.mysearch(strQuery);
                connPLC.myclose();
            }
            catch { }
            return result;
        }
        private void loadLog(string Id)
        {
            var log = wlog.Select().FirstOrDefault(x => x["Ident00"].ToString() == Id);
            if (log != null)
            {
                id = log["Ident00"].ToString();
                Ma_Id = id;
                txtRunCard.Text = log["Rundcard_ID"].ToString();
                txtProcess.Text = log["Process"].ToString();
                txtMachineId.Text = log["Machine_ID"].ToString();
                ddlCode.SelectedValue = log["Emp_ID"].ToString().Substring(0, 2);
                txtEmployee.Text = log["Emp_ID"].ToString().Substring(2, log["Emp_ID"].ToString().Length - 2);
                txtBeginDate.Text = Convert.ToDateTime(log["BeginDate"]).ToString("yyyy-MM-dd");
                txtBeginGio.Text = Convert.ToDateTime(log["BeginDate"]).ToString("HH:mm");
                txtEndDate.Text = Convert.ToDateTime(log["EndDate"]).ToString("yyyy-MM-dd");
                txtEndGio.Text = Convert.ToDateTime(log["EndDate"]).ToString("HH:mm");
                                
            }
            else
            {
                clear();
            }
        }
        private void load()
        {
            wdate = DateTime.Now;

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
            connPLC = new connectDB();
            wlog = new DataTable();
            index = 0;
            loadData();
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
                    + "')";
                connEIP.myopen();
                result = connEIP.mysearch(strQuery);
                connEIP.myclose();
            }
            catch { }
            return result;
        }

        private DataTable getProcess(string Process_id)
        {
            DataTable result = new DataTable();
            try
            {
               
                string strQuery = "SELECT *\n"
                    + "FROM OPENQUERY(TIPTOP, 'select * from ecd_file\n"
                    + "WHERE ecd01 = ''" + Process_id + "''\n"
                    + "')";
                connEIP.myopen();
                result = connEIP.mysearch(strQuery);
                connEIP.myclose();
            }
            catch { }
            return result;
        }

        private DataTable getMachine(string machine)
        {
            DataTable result = new DataTable();
            try
            {
                connPLC.myopen();
                result = connPLC.mysearch("select * from PLC_MachineList where MachineId = '" + machine + "'");
                connPLC.myclose();
            }
            catch { }
            return result;
        }
        private bool Edit_Datat(string strModeA, string strImpTime, string strExpTime)
        {
            bool result = false;
            if(strModeA.ToString() != "" || strModeA != null)
            {
                System.Collections.Hashtable htPara = new System.Collections.Hashtable();
                htPara["MODE"] = strMode;
                htPara["RUNDCARD_ID"] = txtRunCard.Text.ToUpper();
                htPara["IDENT"] = Ma_Id;
                htPara["MACHINE_ID"] = txtMachineId.Text.ToUpper();
                htPara["PROCESS"] = txtProcess.Text.ToUpper();
                htPara["EMP_ID"] = ddlCode.SelectedValue + txtEmployee.Text.Trim();
                htPara["BEGINDATE"] = strImpTime;
                htPara["ENDDATE"] = strExpTime;
                htPara["NGAY_NHAP"] = wdate.ToString("yyyy-MM-dd"); ;

                connPLC.myopen();

                connPLC.mySqlExecute("sp_Edit_BaocongCheckIn", htPara, CommandType.StoredProcedure);
                connPLC.myclose();
                result = true;
            }
            return result;


        }
        private bool KiemtraBeginDate(string strGioBD, string strMachineId)
        {
            bool result = true;
            //Kiểm tra Giờ bắt đầu
            string strQuery = "select enddate from BaoCong_CheckIn where enddate >= '" + strGioBD + "' and Machine_ID = '" + strMachineId + "' ";
            if(strMode == "E")
                strQuery = "select enddate from BaoCong_CheckIn where enddate >= '" + strGioBD + "' and Machine_ID = '" + strMachineId + "' and IDENT00 <> " + Ma_Id;
            strQuery = strQuery + " ORDER BY enddate desc";
            DataTable dtKiemTra = new DataTable();
            connPLC.myopen();
            dtKiemTra = connPLC.mysearch(strQuery);
            connPLC.myclose();

            if (dtKiemTra.Rows.Count >= 1)
            {
                string strTime = dtKiemTra.Rows[0][0].ToString();
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Thời Gian bắt đầu trùng lập với thời gian trước đó " + strTime + " , Vui lòng kiểm tra !!!";
                strmsg = "Thời Gian bắt đầu trùng lập với thời gian trước đó " + strTime + " , Chọn Yes để tiếp hay No để nhập lại !!!";
                result = false;
            }
            else
                lblMessage.Text = "";

            return result;
        }
        #endregion

        

        protected void btnClose_Click(object sender, EventArgs e)
        {
            //string strMachineid = result.Text;
            //if (strMachineid != null || strMachineid != "")
            //{
            //    txtMachineId.Text = strMachineid;
            //}
        }

       
    }
}