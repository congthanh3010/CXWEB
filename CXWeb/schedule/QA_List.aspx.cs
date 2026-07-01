using System;
using System.Data;
using System.Drawing;
using System.Media;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CXWeb.schedule
{
    public partial class QA_List : System.Web.UI.Page
    {
        private static connectEIP connEIP;
        private static connectDB connPLC;
        private static string strMode;
        private static DateTime wdate;
        private static DataTable wlog;
        private static int index = -1;
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
                load();
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
        protected void grvWLog_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grvWLog, "Select$" + e.Row.RowIndex);
        }
        #region Sự kiện
        private void load()
        {
            connEIP = new connectEIP();
            connPLC = new connectDB();
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
            CloseEdit();
            clear();
        }
        private void loadLog(string Id)
        {
            var log = wlog.Select().FirstOrDefault(x => x["Code_Id"].ToString() == Id);
            if (log != null)
            {
                txtCode_Id.Text = log["Code_Id"].ToString();
                txtCode_Name.Text = log["Code_Name"].ToString();
                txtDon_Vi.Text = log["Don_Vi"].ToString();
                txtSo_Cot.Text = log["So_Cot"].ToString();
                txtTieu_Chuan.Text = log["Tieu_Chuan"].ToString();
                txtValue_Max.Text = log["Value_Max"].ToString();
                txtValue_Min.Text = log["Value_Min"].ToString();
                ddlMachine.SelectedValue = log["Machine_ID"].ToString();
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
                string strQuery = "select * from QA_List order by Machine_ID, Code_Id";
                connEIP.myopen();
                result = connEIP.mysearch(strQuery);
                connPLC.myclose();
            }
            catch { }
            return result;
        }
        private void OpenEdit()
        {
            txtCode_Id.Enabled = true;
            txtCode_Name.Enabled = true;
            txtDon_Vi.Enabled = true;
            txtSo_Cot.Enabled = true;
            txtTieu_Chuan.Enabled = true;
            txtValue_Max.Enabled = true;
            txtValue_Min.Enabled = true;
            ddlMachine.Enabled = true;
        }
        private void CloseEdit()
        {
            txtCode_Id.Enabled = false;
            txtCode_Name.Enabled = false;
            txtDon_Vi.Enabled = false;
            txtSo_Cot.Enabled = false;
            txtTieu_Chuan.Enabled = false;
            txtValue_Max.Enabled = false;
            txtValue_Min.Enabled = false;
            ddlMachine.Enabled = false;

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
        private void clear()
        {
            txtCode_Id.Text = "";
            txtCode_Name.Text = "";
            txtDon_Vi.Text = "";
            txtSo_Cot.Text = "";
            txtTieu_Chuan.Text = "";
            txtValue_Max.Text = "";
            txtValue_Min.Text = "";
            ddlMachine.SelectedIndex= 0;


        }
        private DataTable getRuncard(string Code_Id)
        {
            DataTable result = new DataTable();
            try
            {
                //string strQuery = "SELECT sf.sgm01 AS RuncardNo, sf.sgm02 AS RecordNo, sf.sgm03_par AS ItemNo,\n"
                //    + "       sf.sgm04 AS StepNo, f_1104_bmb06(sf.sgm03_par) * 1000 AS ProWeight\n"
                //    + "FROM sgm_file sf\n"
                //    + "WHERE sf.sgm01 LIKE '%" + runcard + "%'\n"
                //    + "AND EXISTS (SELECT 1 FROM eci_file ef WHERE ef.eci03 = sf.sgm06 AND ef.eci01 = '" + machine + "')";
                string strQuery = "SELECT * FROM QA_LIST where Code_Id = '" + Code_Id + "'";
                connEIP.myopen();
                result = connEIP.mysearch(strQuery);
                connEIP.myclose();
            }
            catch { }
            return result;
        }
        private bool Edit_Datat(string strModeA)
        {
            bool result = false;
            if (strModeA.ToString() != "" || strModeA != null)
            {
                System.Collections.Hashtable htPara = new System.Collections.Hashtable();
                htPara["MODE"] = strMode;
                htPara["CODE_ID"] = txtCode_Id.Text.ToUpper();
                htPara["CODE_NAME"] = txtCode_Name.Text;
                htPara["MACHINE"] = ddlMachine.SelectedValue.ToString();
                htPara["DON_VI"] = txtDon_Vi.Text.ToUpper();
                htPara["SO_COT"] = txtValue_Max.Text.Trim(); 
                htPara["TIEU_CHUAN"] = txtTieu_Chuan.Text.Trim();
                htPara["VALUE_MAX"] = txtValue_Max.Text.Trim();
                htPara["VALUE_MIN"] = txtValue_Min.Text.Trim();

                connEIP.myopen();

                connEIP.mySqlExecute("sp_Edit_QA_List", htPara, CommandType.StoredProcedure);
                connEIP.myclose();
                result = true;
            }
            return result;


        }
        bool KiemtraCactruongSo(string So)
        {
            try
            {
                Convert.ToInt32(So);
                return true;
            }
            catch { return false; }
        }

        #endregion

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string msg = "";
            if (txtCode_Id.Text.Trim() == "")
                msg = "Chưa nhập Mã Hóa Chất";
            else if (txtCode_Name.Text.Trim() == "")
                msg = "Chưa nhập tên Hóa Chất";
            else if (txtDon_Vi.Text.Trim() == "")
                msg = "Chưa nhập Đơn vị tính";
            else if (txtTieu_Chuan.Text.Trim() == "")
                msg = "Chưa nhập tiêu chuẩn";
            else if (txtValue_Min.Text.Trim() == "")
                msg = "Chưa nhập giá trị Min";
            else if (txtValue_Max.Text.Trim() == "")
                msg = "Chưa nhập giá trị max";
            else if (KiemtraCactruongSo(txtValue_Max.Text) == false)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Value Max không phải là dạng số !!! Yêu cầu kiểm tra lại";

            }
            else if (KiemtraCactruongSo(txtValue_Min.Text) == false)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Value Min không phải là dạng số !!! Yêu cầu kiểm tra lại";

            }
            else if (KiemtraCactruongSo(txtSo_Cot.Text) == false)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Cột Mốc không phải là dạng số !!! Yêu cầu kiểm tra lại";

            }

            if (strMode == null)
                strMode = "N";

            if (strMode == "")
                strMode = "N";

            //Kiem tra mã runcard
            DataTable dtRunCard = getRuncard(txtCode_Id.Text);
            if (dtRunCard.Rows.Count > 0 && strMode == "N")
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Mã Hóa chất đã tồn tại, vui lòng kiểm tra lại";
                txtCode_Id.Focus();
                return;
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

                    bool result = Edit_Datat(strMode);
                    if (result == true)
                    {

                        lblMessage.ForeColor = System.Drawing.Color.Blue;
                        lblMessage.Text = "Đã lưu ghi chép lúc " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                        loadData();
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
                catch(Exception ex)
                {

                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "Xảy ra lỗi khi lưu ghi chép" + ex.ToString();
                    return;
                }

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

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            OpenEdit();
            txtCode_Id.Focus();
            clear();
            state.Value = "insert";
            strMode = "N";
            btnInsert.Enabled = false;
            btnModify.Enabled = false;
            btnDelete.Enabled = false;
        }

        protected void btnModify_Click(object sender, EventArgs e)
        {
            if (txtCode_Id.Text != "")
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
            strMode = "D";
            if (strMode == null)
                strMode = "N";
            bool result = Edit_Datat(strMode);
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
    }
}