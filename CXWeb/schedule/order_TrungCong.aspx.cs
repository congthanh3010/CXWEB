using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.DataVisualization.Charting;
using System.Threading;
using System.Text.RegularExpressions;

namespace CXWeb.schedule
{
    public partial class order_TrungCong : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!IsPostBack)
            {
                txtBegin_Date.Text = DateTime.Now.AddDays(-1).ToString("MM/dd/yyyy");
                txtEnd_Date.Text = DateTime.Now.AddDays(0).ToString("MM/dd/yyyy");
                CloseText();
                query();
            }
        }
        void query()
        {
            connectEIP myconn = new connectEIP();

            DataSet myDataset = new DataSet();
            DataTable myDatatable = new DataTable();

            string strSQL = "sp_MachineStopViewAll";

            System.Collections.Hashtable htPara = new System.Collections.Hashtable();
            htPara["TYPE_INPUT"] = "3";
            myconn.myopen();

            myDatatable = myconn.ExecuteReturnDt(strSQL, htPara, CommandType.StoredProcedure);

            myconn.myclose();

            GridView1.DataSource = myDatatable;
            GridView1.DataBind();

        }
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            query();
        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            query();
        }
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            query();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "New/Thêm mới")
            {
                hfContactID.Value = "";
                btnSave.Text = "Save/Lưu";
                Clear();
                OpenText();
                return;
            }
            else
            {
                btnSave.Text = "New/Thêm mới";
                CloseText();
            }
              

            if (IsNumber(txtTime_Date.Text) == false)
                return;
            string strTimeDate = txtTime_Date.Text;
            int intTime_Date = strTimeDate == "" ? 0 : Convert.ToInt32(strTimeDate);

            DateTime dteBegin_Date = Convert.ToDateTime(txtBegin_Date.Text);
            DateTime dteEnd_Date = Convert.ToDateTime(txtEnd_Date.Text);

            if (dteBegin_Date > dteEnd_Date)
            {
                lblSuccessMessage.Text = "Ngày bắt đầu lớn hàng ngày text, Vui lòng kiểm tra lại !!!";
                return;
            }
            Int32 intTotalDate = (dteEnd_Date - dteBegin_Date).Days;
            if (intTotalDate < 0)
                intTotalDate = 0;
            int intTotal_time = (intTotalDate + 1) * 24;

            if (Convert.ToInt64(txtTime_Date.Text) > intTotal_time)
            {
                lblSuccessMessage.Text = "Nhập sai số lượng giờ,Vui lòng kiểm tra lại !!! ";
                return;
            }

            connectEIP myconn = new connectEIP();
            int intId_Num = hfContactID.Value == "" ? 0 : Convert.ToInt32(hfContactID.Value);
            string strsql = "sp_AddOrUpdateMachine";
            myconn.myopen();

            System.Collections.Hashtable htPara = new System.Collections.Hashtable();
            htPara["ID_NUM"] = intId_Num;
            htPara["ORDER_ID"] = txtOrder_Id.Text;
            htPara["ITEM_NO"] = txtItem_No.Text;
            //htPara["CUS_NAME"] = txtCus_Name.Text;
            htPara["MACHINE_NO"] = txtMachine_No.Text;
            htPara["BEGIN_DATE"] = txtBegin_Date.Text;
            htPara["END_DATE"] = txtEnd_Date.Text;
            htPara["TIME_DATE"] = intTime_Date;
            htPara["TYPE_INPUT"] = "3";

            myconn.mySqlExecute(strsql, htPara, CommandType.StoredProcedure);
            myconn.myclose();
            Clear();
            if(intId_Num == 0)
                lblSuccessMessage.Text = "Saved Successfully";
            else
                lblSuccessMessage.Text = "Updated Successfully";

            query();            
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();

        }
        protected void lnk_OnClick(object sender, EventArgs e)
        {
            int intId_Num = Convert.ToInt32((sender as LinkButton).CommandArgument);

            connectEIP myconn = new connectEIP();

            string strsql = "";

            //-------------------------------------------------------------

            DataSet mydataset = new DataSet();
            DataTable mydata2 = new DataTable();
            myconn.myopen();

            System.Collections.Hashtable htPara = new System.Collections.Hashtable();
            htPara["ID_NUM"] = intId_Num;

            mydata2 = myconn.ExecuteReturnDt("sp_MachineStopViewByID", htPara, CommandType.StoredProcedure);

            myconn.myclose();
            if (mydata2.Rows.Count > 0)
            {
                txtBegin_Date.Text = mydata2.Rows[0]["Begin_Date_Convert"].ToString();
                txtEnd_Date.Text = mydata2.Rows[0]["End_Date_Convert"].ToString();
                txtOrder_Id.Text = mydata2.Rows[0]["Order_ID"].ToString();
                //txtCus_Name.Text = mydata2.Rows[0]["Cus_Name"].ToString();
                txtItem_No.Text = mydata2.Rows[0]["Item_No"].ToString();
                txtMachine_No.Text = mydata2.Rows[0]["Machine_No"].ToString();
                txtTime_Date.Text = mydata2.Rows[0]["Time_Date_Convert"].ToString();
                btnSave.Text = "Update/Cập nhật";
                hfContactID.Value = intId_Num.ToString();
                OpenText();
            }
          

        }
        protected void lnk_Delete(object sender, EventArgs e)
        {
            int intId_Num = Convert.ToInt32((sender as LinkButton).CommandArgument);

            connectEIP myconn = new connectEIP();
            
            string strsql = "sp_MachineStopDelete";
            myconn.myopen();

            System.Collections.Hashtable htPara = new System.Collections.Hashtable();
            htPara["ID_NUM"] = intId_Num;
            
            myconn.mySqlExecute(strsql, htPara, CommandType.StoredProcedure);
            myconn.myclose();
            Clear();
            if (intId_Num == 0)
                lblSuccessMessage.Text = "Delete Successfully";
            else
                lblSuccessMessage.Text = "Delete Successfully";

            Clear();

            query();

        }
        public bool IsNumber(string pText)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(pText);
        }
        private void CloseText()
        {
            txtMachine_No.ReadOnly = true;
            txtBegin_Date.ReadOnly = true;
            txtEnd_Date.ReadOnly = true;
            txtTime_Date.ReadOnly = true;
            txtOrder_Id.ReadOnly = true;
            txtItem_No.ReadOnly = true;
            //txtCus_Name.ReadOnly = true;

            txtMachine_No.Enabled = false;
            txtBegin_Date.Enabled = false;
            txtEnd_Date.Enabled = false;
            txtTime_Date.Enabled = false;
            txtOrder_Id.Enabled = false;
            txtItem_No.Enabled = false;
           // txtCus_Name.Enabled = false;
            btnCalc_Date.Enabled = false;

        }
        private void OpenText()
        {
            txtMachine_No.ReadOnly = false;
            txtBegin_Date.ReadOnly = false;
            txtEnd_Date.ReadOnly = false;
            txtTime_Date.ReadOnly = false;
            txtOrder_Id.ReadOnly = false;
            txtItem_No.ReadOnly = false;
            //txtCus_Name.ReadOnly = false;

            txtMachine_No.Enabled = true;
            txtBegin_Date.Enabled = true;
            txtEnd_Date.Enabled = true;
            txtTime_Date.Enabled = true;
            txtOrder_Id.Enabled = true;
            txtItem_No.Enabled = true;
            //txtCus_Name.Enabled = true;
            btnCalc_Date.Enabled = true;
        }
        public void Clear()
        {
            hfContactID.Value = "";
            txtMachine_No.Text = txtTime_Date.Text = "";
            lblSuccessMessage.Text = lblErrorMessage.Text = "";
            //txtCus_Name.Text = "";
            txtItem_No.Text = "";
            txtOrder_Id.Text = "";
            txtBegin_Date.Text = DateTime.Now.AddDays(-1).ToString("MM/dd/yyyy");
            txtEnd_Date.Text = DateTime.Now.AddDays(0).ToString("MM/dd/yyyy");
            //btnSave.Text = "Save/Lưu";
            
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("/schedule/main.aspx");
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void btnCalc_Date_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dteBegin_Date = Convert.ToDateTime(txtBegin_Date.Text);
                DateTime dteEnd_Date = Convert.ToDateTime(txtEnd_Date.Text);
                Int32 intTotalDate = (dteEnd_Date - dteBegin_Date).Days;
                if (intTotalDate < 0)
                    intTotalDate = 0;
                txtTime_Date.Text = Convert.ToString((intTotalDate + 1)*24);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}