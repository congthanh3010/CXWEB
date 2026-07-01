using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace CXWeb.schedule
{
    public partial class Einvoice_Buyer : System.Web.UI.Page
    {
        private static DataTable dt = new DataTable();
        private connectEIP conn = new connectEIP();
        private static int index = -1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                index = 0;
                query();
            }
        }

        private void query()
        {
            CloseEdit();
            conn.myopen();
            string strsql = "";
            strsql += "select ma_don_vi, ho_ten_nguoi_mua_hang, ten_don_vi, dia_chi,ma_so_thue, email, hinh_thuc_thanh_toan, ten_dich_vu,\n"
                + "don_vi_tinh_HD, don_vi_tinh_HT, don_vi_tien_te_don_gia, don_vi_tien_te_thanh_tien, don_vi_tien_te_HT,\n"
                + "ngay_hd_lech,loai_ti_suat\n"
                + "from einvoice_client_info\n"
                + "order by ma_don_vi";
            dt = conn.mysearch(strsql);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            GridView1.SelectedIndex = index;
            LoadBuyer(GridView1.SelectedDataKey.Value.ToString());

            try
            {
                conn.myclose();
            }
            catch
            {
                return;
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" + e.Row.RowIndex);
                //e.Row.Attributes.Add("onmouseover", "MouseEvents(this, event)");
                //e.Row.Attributes.Add("onmouseout", "MouseEvents(this, event)");
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            if (GridView1.SelectedIndex >= 0)
            {
                index = GridView1.SelectedIndex;
                LoadBuyer(GridView1.SelectedDataKey.Value.ToString());
            }
        }

        private void LoadBuyer(string Id)
        {
            var buyer = dt.Select($"ma_don_vi='{Id}'").Count() > 0 ? dt.Select($"ma_don_vi='{Id}'")[0] : null;
            if (buyer != null)
            {
                txtHoTen.Text = buyer["ho_ten_nguoi_mua_hang"].ToString();
                txtDonVi.Text = buyer["ten_don_vi"].ToString();
                lblBuyer.Text = buyer["ten_don_vi"].ToString();
                txtDiaChi.Text = buyer["dia_chi"].ToString();
                txtMaSo.Text = buyer["ma_so_thue"].ToString();
                txtEmail.Text = buyer["email"].ToString();
                txtDichVu.Text = buyer["ten_dich_vu"].ToString();
                txtThanhToan.Text = buyer["hinh_thuc_thanh_toan"].ToString();
                txtTinhHD.Text = buyer["don_vi_tinh_HD"].ToString();
                txtTinhHT.Text = buyer["don_vi_tinh_HT"].ToString();
                txtTienTeDG.Text = buyer["don_vi_tien_te_don_gia"].ToString();
                txtTienTeTT.Text = buyer["don_vi_tien_te_thanh_tien"].ToString();
                txtTienTeHT.Text = buyer["don_vi_tien_te_HT"].ToString();
                txtTiSuat.Text = buyer["loai_ti_suat"].ToString();
            }
            else
            {
                txtHoTen.Text = "";
                txtDonVi.Text = "";
                lblBuyer.Text = "";
                txtDiaChi.Text = "";
                txtMaSo.Text = "";
                txtEmail.Text = "";
                txtDichVu.Text = "";
                txtThanhToan.Text = "";
                txtTinhHD.Text = "";
                txtTinhHT.Text = "";
                txtTienTeDG.Text = "";
                txtTienTeTT.Text = "";
                txtTienTeHT.Text = "";
                txtTiSuat.Text = "";
            }
        }

        private void OpenEdit()
        {
            txtHoTen.Enabled = true;
            txtDonVi.Enabled = true;
            txtDiaChi.Enabled = true;
            txtMaSo.Enabled = true;
            txtEmail.Enabled = true;
            txtDichVu.Enabled = true;
            txtThanhToan.Enabled = true;
            txtTinhHD.Enabled = true;
            txtTinhHT.Enabled = true;
            txtTienTeDG.Enabled = true;
            txtTienTeTT.Enabled = true;
            txtTienTeHT.Enabled = true;
            txtTiSuat.Enabled = true;

            btnSave.Enabled = true;
            btnCancel.Enabled = true;
        }

        private void CloseEdit()
        {
            txtHoTen.Enabled = false;
            txtDonVi.Enabled = false;
            txtDiaChi.Enabled = false;
            txtMaSo.Enabled = false;
            txtEmail.Enabled = false;
            txtDichVu.Enabled = false;
            txtThanhToan.Enabled = false;
            txtTinhHD.Enabled = false;
            txtTinhHT.Enabled = false;
            txtTienTeDG.Enabled = false;
            txtTienTeTT.Enabled = false;
            txtTienTeHT.Enabled = false;
            txtTiSuat.Enabled = false;

            btnSave.Enabled = false;
            btnCancel.Enabled = false;
        }

        protected void btnModify_Click(object sender, EventArgs e)
        {
            OpenEdit();
            txtHoTen.Focus();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            CloseEdit();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            conn.myopen();
            string id = GridView1.SelectedDataKey.Value.ToString();
            string strsql = "update einvoice_client_info set\n"
                + $"    ho_ten_nguoi_mua_hang = N'{txtHoTen.Text}',\n"
                + $"    ten_don_vi = N'{txtDonVi.Text}',\n"
                + $"    dia_chi = N'{txtDiaChi.Text}',\n"
                + $"    ma_so_thue = N'{txtMaSo.Text}',\n"
                + $"    email = N'{txtEmail.Text}',\n"
                + $"    hinh_thuc_thanh_toan = N'{txtThanhToan.Text}',\n"
                + $"    ten_dich_vu = N'{txtDichVu.Text}',\n"
                + $"    don_vi_tinh_HD = N'{txtTinhHD.Text}',\n"
                + $"    don_vi_tinh_HT = N'{txtTinhHT.Text}',\n"
                + $"    don_vi_tien_te_don_gia = N'{txtTienTeDG.Text}',\n"
                + $"    don_vi_tien_te_thanh_tien = N'{txtTienTeTT.Text}',\n"
                + $"    don_vi_tien_te_HT = N'{txtTienTeHT.Text}',\n"
                + $"    loai_ti_suat = N'{txtTiSuat.Text}'\n"
                + $"where ma_don_vi = '{id}'";
            conn.mySqlExecute(strsql);
            lblMessage.Text = "Updated successfully";
            lblMessage.ForeColor = System.Drawing.Color.Green;
            query();
        }
    }
}