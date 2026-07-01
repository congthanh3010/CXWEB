using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using System.Web.UI.DataVisualization.Charting;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.Drawing.Drawing2D;
using System.Globalization;

namespace CXWeb.schedule
{
    public partial class Ghi_Chep_Khuon : System.Web.UI.Page
    {
        private static connectEIP connEIP;

        private static string pcname;
        private static string ip;
        private static int index = -1;
        private static string session;
        private static DateTime wdate;
        private static string strMode = "N";

        private static DataTable dt = new DataTable();

        string folderPath= "";
        string fileName = "";
        string fileName_ThuTe = "";
        int Ma_Id = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            wdate = DateTime.Now;

            if (!IsPostBack)
                load();

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            
            string msg = "";
            if (txtChung_Loai.Text == "")
                msg = "Phải nhập thông tin chủng loại !!!";
            //else if(txtMa_So_Khuon.Text == "")
            //    msg = "Phải nhập thông tin Mã số khuôn !!!";
            
            if(FileUpload1.FileName != "" && CheckFileType(FileUpload1.FileName) == false)
                msg = "File Upload Hình Ảnh không đúng như quy định !!! Phải là Hình hay PDF.";

            if (File_Thuc_Te.FileName != "" && CheckFileType(File_Thuc_Te.FileName) == false)
                msg = "File Upload Thực Tế Đo không đúng như quy định !!! Phải là Hình hay PDF.";

            if (KiemtraCactruongSo(txtSo_Luong_Kiem_Tra.Text) == false)
                msg = "Số Lượng kiểm tra không phải là dạng số !!! Yêu cầu kiểm tra lại.";

            if (KiemtraCactruongSo(txtKich_Thuoc_NG.Text) == false)
                msg = "Kích thước NG không phải là dạng số !!! Yêu cầu kiểm tra lại.";

            if (KiemtraCactruongSo(txtKich_Thuoc_OK.Text) == false)
                msg = "Kích thước OK không phải là dạng số !!! Yêu cầu kiểm tra lại.";

            if (msg != "")
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = msg;
            }
            else
            {
                try
                {
                    if(strMode == "N")
                    { txtSo_Kiem_Tra.Text = GetID(); }                    

                    UploadHinhAnh();

                    System.Collections.Hashtable htPara = new System.Collections.Hashtable();

                    htPara["MODE"] = strMode;
                    htPara["SO_KIEM_TRA"] = txtSo_Kiem_Tra.Text;
                    htPara["SO_THU_TU"] = Ma_Id + 1;
                    htPara["THANH_TRA"] = txtThanh_Tra.Text;
                    htPara["QUA_TRINH"] = ddlQua_Trinh.Text;
                    htPara["SO_KIEM_TRA"] = txtSo_Kiem_Tra.Text;
                    htPara["NGAY_KIEM"] = txtNgay_Kiem.Text;
                    htPara["SO_LUONG_KIEM_TRA"] = Convert.ToInt32(txtSo_Luong_Kiem_Tra.Text);
                    htPara["KICH_THUOC_NG"] = Convert.ToInt32(txtKich_Thuoc_NG.Text);
                    htPara["CHUNG_LOAI"] = txtChung_Loai.Text;
                    htPara["MA_SO_KHUON"] = "";// txtMa_So_Khuon.Text;
                    htPara["KICH_THUOC_OK"] = Convert.ToInt32(txtKich_Thuoc_OK.Text);
                    htPara["LOAI_KHUON"] = ddlLoai_Khuon.Text;
                    htPara["SU_DUNG_DON_VI"] = ""; //txtSu_Dung_Don_Vi.Text;
                    htPara["HINH_ANH"] = fileName;
                    htPara["THUC_TE"] = fileName_ThuTe;

                    connEIP.myopen();
                   
                    connEIP.mySqlExecute("sp_Edit_ModLog", htPara, CommandType.StoredProcedure);
                    connEIP.myclose();

                    strMode = "";

                    lblMessage.Text = "Lưu thành công !!!";

                    LoadData();
                    CloseEdit();
                    btnInsert.Enabled = true;
                    btnModify.Enabled = true;
                    btnGetId.Enabled = false;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    btnDelete.Enabled = true;
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.ToString());
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = ex.ToString();// "Xảy ra lỗi khi lưu ghi chép";
                    return;
                }
            }
         }

        #region Thủ tục

        private void load()
        {
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
            index = 0;
            CloseEdit();
            
            txtNgay_Kiem.Text = wdate.ToString("yyyy-MM-dd");

            LoadData();
            btnModify.Enabled = false;
            btnCancel.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void LoadData()
        {
            dt = new DataTable();

            string strSQL = "select * from Mod_Log where Ngay_Kiem ='" + wdate.ToString("yyyy-MM-dd") + "'";

            connEIP.myopen();
            dt = connEIP.mysearch(strSQL);
            connEIP.myclose();

            dt.Columns.Add("code");
            dt.Columns.Add("Ten_file");
            dt.Columns.Add("Ten_hinh");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["code"] = "style = 'background-image: url(" + dt.Rows[i]["Hinh_Anh"].ToString() + ");'";
                if (dt.Rows[i]["Hinh_Anh"].ToString().Length >= 18)
                {
                    dt.Rows[i]["Ten_File"] = dt.Rows[i]["Thuc_Te"].ToString().Substring(18, dt.Rows[i]["Thuc_Te"].ToString().Length - 18);
                    dt.Rows[i]["Ten_hinh"] = dt.Rows[i]["Hinh_Anh"].ToString().Substring(18, dt.Rows[i]["Hinh_Anh"].ToString().Length - 18);
                }
                  
            }


            grvWLog.DataSource = null;
            grvWLog.DataSource = dt;
            grvWLog.DataBind();
        }

        private void UploadHinhAnh()
        {
            folderPath = Server.MapPath("~/images/checklist/");
            fileName = "";

            if (FileUpload1.HasFile && CheckFileType(FileUpload1.FileName) && CheckFileType(File_Thuc_Te.FileName))
            {
                //Check whether Directory (Folder) exists.
                if (!Directory.Exists(folderPath))
                {
                    //If Directory (Folder) does not exists. Create it.
                    Directory.CreateDirectory(folderPath);
                }
                string ten_Hinh_Anh = txtSo_Kiem_Tra.Text + "_HA" + Path.GetExtension(FileUpload1.FileName); 
                string ten_Thu_Te = txtSo_Kiem_Tra.Text + "_TT" + Path.GetExtension(File_Thuc_Te.FileName); ;

                fileName = "/images/checklist/" + ten_Hinh_Anh;
                fileName_ThuTe = "/images/checklist/" + ten_Thu_Te;
                //fileName_ThuTe = "/images/checklist/" + File_Thuc_Te.FileName;

                string a = folderPath + Path.GetFileName(FileUpload1.FileName);


                if (CheckFileHinh(FileUpload1.FileName) == true)
                {
                    
                    System.Drawing.Image Img = new System.Drawing.Bitmap(FileUpload1.PostedFile.InputStream);
                    {
                        Size ThumbNailSize = NewImageSize(Img.Height, Img.Width, 400);

                        using (System.Drawing.Image ImgThnail = new Bitmap(Img, ThumbNailSize.Width, ThumbNailSize.Height))
                        {
                            ImgThnail.Save(folderPath + ten_Hinh_Anh, Img.RawFormat);//folderPath + Path.GetFileName(FileUpload1.FileName)
                            ImgThnail.Dispose();
                        }
                        Img.Dispose();
                    }
                }
                else
                {
                    // Save the File to the Directory (Folder).
                    FileUpload1.SaveAs(folderPath + ten_Hinh_Anh);//Path.GetFileName(FileUpload1.FileName)
                }

                if (CheckFileHinh(File_Thuc_Te.FileName) == true)
                {

                    System.Drawing.Image Img = new System.Drawing.Bitmap(File_Thuc_Te.PostedFile.InputStream);
                    {
                        Size ThumbNailSize = NewImageSize(Img.Height, Img.Width, 400);

                        using (System.Drawing.Image ImgThnail = new Bitmap(Img, ThumbNailSize.Width, ThumbNailSize.Height))
                        {
                            ImgThnail.Save(folderPath + ten_Thu_Te, Img.RawFormat); // Path.GetFileName(File_Thuc_Te.FileName)
                            ImgThnail.Dispose();
                        }
                        Img.Dispose();
                    }
                }
                else
                {
                    // Save the File to the Directory (Folder).
                    File_Thuc_Te.SaveAs(folderPath + ten_Thu_Te);
                }

                //Save the File to the Directory (Folder).
                //FileUpload1.SaveAs(folderPath + Path.GetFileName(FileUpload1.FileName));

            }
            else if(strMode == "U")
            {
                fileName = lblHinh_Anh.Text;
                fileName_ThuTe = lblThuc_Te.Text;
            }
            else
            {
                lblMessage.Text = "File Upload Hình Ảnh không đúng như quy định !!! Phải là Hình hay PDF.";
            }                   

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
        bool CheckFileType(string fileName)
        {

            string ext = Path.GetExtension(fileName);
            switch (ext.ToLower())
            {
                case ".gif":
                    return true;
                case ".png":
                    return true;
                case ".jpg":
                    return true;
                case ".jpeg":
                    return true;
                case ".pdf":
                    return true;
                default:
                    return false;
            }
        }

        bool CheckFileHinh(string fileName)
        {

            string ext = Path.GetExtension(fileName);
            switch (ext.ToLower())
            {
                case ".gif":
                    return true;
                case ".png":
                    return true;
                case ".jpg":
                    return true;
                case ".jpeg":
                    return true;

                default:
                    return false;
            }
        }

        public Size NewImageSize(int OriginalHeight, int OriginalWidth, double FormatSize)
        {
            Size NewSize;
            double tempval;

            if (OriginalHeight > FormatSize && OriginalWidth > FormatSize)
            {
                if (OriginalHeight > OriginalWidth)
                    tempval = FormatSize / Convert.ToDouble(OriginalHeight);
                else
                    tempval = FormatSize / Convert.ToDouble(OriginalWidth);

                NewSize = new Size(Convert.ToInt32(tempval * OriginalWidth), Convert.ToInt32(tempval * OriginalHeight));
            }
            else
                NewSize = new Size(OriginalWidth, OriginalHeight); return NewSize;
        }

        private void OpenEdit()
        {
            //txtSo_Kiem_Tra.Enabled = true;
            txtThanh_Tra.Enabled = true;
            ddlQua_Trinh.Enabled = true;
            txtNgay_Kiem.Enabled = true;
            txtSo_Kiem_Tra.Enabled = true;
            txtKich_Thuoc_NG.Enabled = true;
            txtChung_Loai.Enabled = true;
           // txtMa_So_Khuon.Enabled = true;
            txtSo_Luong_Kiem_Tra.Enabled = true;
            txtKich_Thuoc_OK.Enabled = true;
            ddlLoai_Khuon.Enabled = true;
           // txtSu_Dung_Don_Vi.Enabled = true;
            FileUpload1.Enabled = true;
            btnGetId.Enabled = true;
            File_Thuc_Te.Enabled = true;
            lblMessage.Text = "";

        }

        private void CloseEdit()
        {
            //txtSo_Kiem_Tra.Enabled = false;
            txtThanh_Tra.Enabled = false;
            ddlQua_Trinh.Enabled = false;
            txtNgay_Kiem.Enabled = false;
            txtSo_Kiem_Tra.Enabled = false;
            txtKich_Thuoc_NG.Enabled = false;
            txtChung_Loai.Enabled = false;
          //  txtMa_So_Khuon.Enabled = false;
            txtSo_Luong_Kiem_Tra.Enabled = false;
            txtKich_Thuoc_OK.Enabled = false;
            btnGetId.Enabled = false;
            ddlLoai_Khuon.Enabled = false;
          //  txtSu_Dung_Don_Vi.Enabled = false;
            FileUpload1.Enabled = false;
            File_Thuc_Te.Enabled = false;
            lblMessage.Text = "";

            btnInsert.Enabled = true;
            //if (wlog.Rows.Count > 0)
            //{
            //    btnModify.Enabled = true;
            //    btnDelete.Enabled = true;
            //}
            //else
            //{
            //    btnModify.Enabled = false;
            //    btnDelete.Enabled = false;
            //}
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

        }
        private void Clear()
        {
            txtSo_Kiem_Tra.Text = "";
            txtThanh_Tra.Text = "";
            ddlQua_Trinh.SelectedIndex = 0;
            txtNgay_Kiem.Text = "";
            txtSo_Luong_Kiem_Tra.Text = "";
            txtSo_Kiem_Tra.Text = "";
            txtKich_Thuoc_NG.Text = "";
            txtChung_Loai.Text = "";
           // txtMa_So_Khuon.Text = "";
            txtKich_Thuoc_OK.Text = "";
            ddlLoai_Khuon.SelectedIndex = 0;
            //txtSu_Dung_Don_Vi.Text = "";
            lblHinh_Anh.Text = "";
            lblThuc_Te.Text = "";
            lblMessage.Text = "";
        }

        private  string GetID()
        {
            string strId = txtNgay_Kiem.Text;
            

            string strQuery = "Select ISNULL(MAX(right(So_Kiem_Tra,3)),0) as Id from Mod_Log where ngay_Kiem = CAST('" + strId + "' as date)";
            Ma_Id = 1;

            DataTable dt = new DataTable();
            connEIP.myopen();
            dt = connEIP.mysearch(strQuery);

            if(dt.Rows.Count > 0)
            {
                Ma_Id = Convert.ToInt16(dt.Rows[0]["Id"]) + 1;
            }
            if (Ma_Id == 0)
                strId = strId + "-001";
            else if (Ma_Id > 0 && Ma_Id < 10)
                strId = strId + "-00" + Ma_Id.ToString();
            else if (Ma_Id >= 10 && Ma_Id < 100)
                strId = strId + "-0" + Ma_Id.ToString();
            else if (Ma_Id >= 100 && Ma_Id < 1000)
                strId = strId + "-" + Ma_Id.ToString();


            return strId;
        }
        #endregion

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            OpenEdit();
            Clear();

            btnInsert.Enabled = false;
            btnModify.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            btnGetId.Enabled = true;
            strMode = "N";


            txtNgay_Kiem.Text = wdate.ToString("yyyy-MM-dd");
            txtThanh_Tra.Focus();
            txtSo_Kiem_Tra.Text = GetID();
        }

        protected void grvWLog_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string id = txtSo_Kiem_Tra.Text;

            System.Collections.Hashtable htPara = new System.Collections.Hashtable();

            htPara["MODE"] = "D";
            htPara["SO_KIEM_TRA"] = txtSo_Kiem_Tra.Text;
            htPara["SO_THU_TU"] = Ma_Id;
            htPara["THANH_TRA"] = txtThanh_Tra.Text;
            htPara["QUA_TRINH"] = ddlQua_Trinh.Text;
            htPara["SO_KIEM_TRA"] = txtSo_Kiem_Tra.Text;
            htPara["NGAY_KIEM"] = txtNgay_Kiem.Text;
            htPara["SO_LUONG_KIEM_TRA"] = Convert.ToInt32(txtSo_Luong_Kiem_Tra.Text);
            htPara["KICH_THUOC_NG"] = Convert.ToInt32(txtKich_Thuoc_NG.Text);
            htPara["CHUNG_LOAI"] = txtChung_Loai.Text;
            htPara["MA_SO_KHUON"] = "";// txtMa_So_Khuon.Text;
            htPara["KICH_THUOC_OK"] = Convert.ToInt32(txtKich_Thuoc_OK.Text);
            htPara["LOAI_KHUON"] = ddlLoai_Khuon.Text;
            htPara["SU_DUNG_DON_VI"] = "";// txtSu_Dung_Don_Vi.Text;
            htPara["HINH_ANH"] = fileName;
            htPara["THUC_TE"] = fileName_ThuTe;

            connEIP.myopen();

            connEIP.mySqlExecute("sp_Edit_ModLog", htPara, CommandType.StoredProcedure);
            connEIP.myclose();
            strMode = "";
            lblMessage.Text = "Lưu thành công !!!";
            Clear();
            btnInsert.Enabled = true;
            btnModify.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = false;
            btnGetId.Enabled = false;
            btnCancel.Enabled = false;
            LoadData();
        }

        protected void grvWLog_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grvWLog.SelectedIndex >= 0)
            {
                index = grvWLog.SelectedIndex;
                loadLog(grvWLog.SelectedDataKey.Value.ToString());
                btnInsert.Enabled = true;
                btnModify.Enabled = true;
                btnSave.Enabled = false;
                btnGetId.Enabled = false;
                btnCancel.Enabled = false;
                btnDelete.Enabled = true;
            }
        }

        private void loadLog(string Id)
        {
            var log = dt.Select($"So_Kiem_Tra='{Id}'").Count() > 0 ? dt.Select($"So_Kiem_Tra='{Id}'")[0] : null;
            if (log != null)
            {
                txtSo_Kiem_Tra.Text = log["So_Kiem_Tra"].ToString();
                txtNgay_Kiem.Text = Convert.ToDateTime(log["Ngay_Kiem"]).ToString("yyyy-MM-dd");
                Ma_Id =  Convert.ToInt32(log["So_Thu_Tu"]);
                txtThanh_Tra.Text = log["Thanh_Tra"].ToString();
                try
                {
                    ddlQua_Trinh.Text = log["Qua_trinh"].ToString().Substring(0, 3);
                }
                catch (Exception)
                {
                    ddlQua_Trinh.SelectedValue = "P00";
                   
                }
               
                txtSo_Luong_Kiem_Tra.Text = Convert.ToDouble(log["So_Luong_Kiem_Tra"]).ToString("#.##");
                txtKich_Thuoc_NG.Text = Convert.ToDouble(log["Kich_Thuoc_NG"]).ToString("#.##");  
                txtChung_Loai.Text = log["Chung_Loai"].ToString();
                //txtMa_So_Khuon.Text = log["Ma_So_Khuon"].ToString();
                try
                {
                   
                    ddlLoai_Khuon.SelectedValue = log["Loai_Khuon"].ToString().Substring(0, 2);
                }
                catch (Exception)
                {
                    ddlLoai_Khuon.SelectedValue = "K0";

                }

                txtKich_Thuoc_OK.Text = Convert.ToDouble(log["Kich_Thuoc_Ok"]).ToString("#.##");  
                
                //txtSu_Dung_Don_Vi.Text = log["Su_Dung_Don_Vi"].ToString();
                lblHinh_Anh.Text = log["Hinh_Anh"].ToString();
                lblThuc_Te.Text = log["Thuc_Te"].ToString();
                
            }
            else
            {
                Clear();
            }

        }

        protected void grvWLog_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grvWLog, "Select$" + e.Row.RowIndex);
                btnInsert.Enabled = true;
                btnModify.Enabled = true;
                btnSave.Enabled = false;
                btnCancel.Enabled = false;
                btnDelete.Enabled = true;               
                
            }
              
        }

        protected void btnModify_Click(object sender, EventArgs e)
        {
            OpenEdit();
            strMode = "U";

            btnInsert.Enabled = true;
            btnModify.Enabled = false;
            btnDelete.Enabled = false;
            btnGetId.Enabled = true;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            btnInsert.Enabled = true;
            btnModify.Enabled = false;
            btnSave.Enabled = false;
            btnGetId.Enabled = false;
            btnCancel.Enabled = false;
            btnDelete.Enabled = false;
            Clear();
            CloseEdit();
        }

        protected void txtNgay_Kiem_TextChanged(object sender, EventArgs e)
        {
            txtSo_Kiem_Tra.Text = GetID();
        }

        protected void txtNgay_Kiem_DataBinding(object sender, EventArgs e)
        {
            txtSo_Kiem_Tra.Text = GetID();
        }

        protected void btnGetId_Click(object sender, EventArgs e)
        {
          txtSo_Kiem_Tra.Text =  GetID();
        }
    }
}