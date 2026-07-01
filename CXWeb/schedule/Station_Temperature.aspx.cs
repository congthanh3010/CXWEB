using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;

namespace CXWeb.schedule
{
    public partial class Station_Temperature : System.Web.UI.Page
    {
        private static connectEIP conn;
        private static string filePath;
        private static bool isHasFile = false;
        private static IWorkbook workbook;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                load();
            }
        }

        private void load()
        {
            report_date.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (txtUser.Text.Trim() == "")
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Chưa nhập người tải lên";
            }
            else if (!isHasFile)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Chưa chọn file tải lên";
            }
            else if (KiemtraCactruongSo(txtCos_Int.Text) == false)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Cos I không phải là dạng số !!! Yêu cầu kiểm tra lại";
                
            }                
            else
            {
                try
                {
                    // Create table data
                    DataTable data = new DataTable();
                    data.Columns.Add(new DataColumn("0 (WSID)", typeof(string)));
                    data.Columns.Add(new DataColumn("1 (Stt_ID)", typeof(int)));

                    data.Columns.Add(new DataColumn("2 (TD_Bien_The)", typeof(string)));
                    data.Columns.Add(new DataColumn("3 (TD_Dao_Cat)", typeof(string)));
                    data.Columns.Add(new DataColumn("4 (TD_CB)", typeof(int)));
                    data.Columns.Add(new DataColumn("5 (TD_VT_DD)", typeof(string)));
                    data.Columns.Add(new DataColumn("6 (TD_Cuong_Do)", typeof(int)));
                    data.Columns.Add(new DataColumn("7 (TD_QC)", typeof(string)));
                    data.Columns.Add(new DataColumn("8 (TD_Nhiet_Do)", typeof(int)));

                    data.Columns.Add(new DataColumn("9 (TP_Tu_PP)", typeof(string)));
                    data.Columns.Add(new DataColumn("10 (TP_Lap_Dat)", typeof(string)));
                    data.Columns.Add(new DataColumn("11 (TP_Cuong_Do)", typeof(int)));
                    data.Columns.Add(new DataColumn("12 (TP_Cuong_Do_TT)", typeof(int)));
                    data.Columns.Add(new DataColumn("13 (TP_Nhiet_Do)", typeof(int)));

                    data.Columns.Add(new DataColumn("14 (TB_MSM)", typeof(string)));
                    data.Columns.Add(new DataColumn("15 (TB_Lap_Dat)", typeof(string)));
                    data.Columns.Add(new DataColumn("16 (TB_Nhiet_Do)", typeof(int)));
                    data.Columns.Add(new DataColumn("17 (TB_CB)", typeof(int)));
                    data.Columns.Add(new DataColumn("18 (TB_AP_MAX)", typeof(int)));
                    data.Columns.Add(new DataColumn("19 (TB_Cuong_Do_TT)", typeof(int)));
                    data.Columns.Add(new DataColumn("20 (TB_Ghi_Chu)", typeof(string)));

                    data.Columns.Add(new DataColumn("21 (UploadUser)", typeof(string)));
                    data.Columns.Add(new DataColumn("22 (Station_No)", typeof(string)));
                    data.Columns.Add(new DataColumn("23 (Cos_Int)", typeof(int)));
                    data.Columns.Add(new DataColumn("24 (Ngay_Kiem_tra_2)", typeof(string)));
                    

                    ISheet sheet = workbook.GetSheetAt(Convert.ToInt32(ddlSheet.SelectedValue));
                    string user = ddlCode.Text + txtUser.Text.Trim();

                    int index = 1;
                    int rowCount = sheet.PhysicalNumberOfRows;
                    int colCount = sheet.GetRow(0).PhysicalNumberOfCells;

                    DataFormatter format = new DataFormatter();
                    int rowNum = 5;
                    string val = sheet.GetRow(rowNum).Cells[0].NumericCellValue.ToString().Trim();
                    while (val != "" || val.ToUpper().ToString() == "TOTAL")
                    {
                        if (val.ToString().ToUpper() == "TOTAL")
                            break;

                        IRow row = sheet.GetRow(rowNum);
                        var newRow = data.NewRow();
                        string stt = "00" + index;
                        newRow[0] = stt.Substring(stt.Length - 2, 2);
                        //newRow[1] = row.GetCell(1).StringCellValue.Trim(); //row.GetCell(2).StringCellValue.Trim();
                        //newRow[2] = row.GetCell(3).;
                        for (int i = 1; i <= 17; i++)
                        {
                            ICell cell = row.GetCell(i-1);
                            switch (cell.CellType)
                            {
                                case CellType.String:
                                    newRow[i] = cell.StringCellValue.Trim();
                                    break;
                                case CellType.Numeric:
                                    newRow[i] = (Int32)cell.NumericCellValue;
                                    break;
                            }
                        }
                        newRow[21] = user;
                        newRow[22] = ddlQua_Trinh.Text;
                        newRow[23] = txtCos_Int.Text;
                        newRow[24] = report_date.Text;


                        data.Rows.Add(newRow);
                        index++;
                        rowNum++;
                        try
                        {
                            val = sheet.GetRow(rowNum).Cells[0].NumericCellValue.ToString().Trim();
                        }
                        catch (Exception)
                        {

                            val = sheet.GetRow(rowNum).Cells[0].StringCellValue.ToString().Trim();
                        }                       

                    }
                    conn = new connectEIP();
                    conn.myopen();

                    // Delete data already exists
                    string strQuery = "DELETE FROM Station_Temperature WHERE Ngay_Kiem_tra_2 = '" + report_date.Text + "' and Station_No = '" + ddlQua_Trinh.Text + "'";
                    conn.mySqlExecute(strQuery);

                    // Bulk Copy to SQL Server
                    string result = conn.BulkCopy(data, "Station_Temperature");
                    if (result != "")
                    {
                        lblMessage.Text = result;
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        conn.myclose();
                        return;
                    }

                    // Load data
                    strQuery = "SELECT ROW_NUMBER() OVER (ORDER BY WSID) AS NumOrd,* FROM Station_Temperature WHERE Ngay_Kiem_tra_2 = '" + report_date.Text + "' and Station_No = '" + ddlQua_Trinh.Text + "'";
                    DataTable log = conn.mysearch(strQuery);
                    grvWLog.DataSource = log;
                    grvWLog.DataBind();

                    lblMessage.ForeColor = System.Drawing.Color.Blue;
                    lblMessage.Text = "Tải file lên thành công lúc " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                    conn.myclose();
                    File.Delete(filePath);


                }
                catch (Exception ex)
                {

                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = ex.Message;
                }

            }
            
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            if (!FileUpload1.HasFile)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Chưa chọn file";
            }
            else
            {
                try
                {
                    // Upload excel file to server
                    string strFolderPath = Server.MapPath("~/App_Data");
                    HttpPostedFile file = FileUpload1.PostedFile;
                    string fileName = file.FileName;
                    filePath = strFolderPath + "/" + fileName;
                    FileUpload1.SaveAs(filePath);

                    if (File.Exists(filePath))
                    {
                        isHasFile = true;
                        using (FileStream fstream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                        {
                            
                            string extension = Path.GetExtension(filePath);
                            if (extension.ToLower() == ".xls")
                                workbook = new HSSFWorkbook(fstream);
                            else
                                workbook = new XSSFWorkbook(fstream);
                        }
                        ddlSheet.Items.Clear();

                        for (int i = 0; i < workbook.NumberOfSheets; i++)
                            ddlSheet.Items.Add(new ListItem(workbook.GetSheetName(i), i.ToString()));

                        grvWLog.DataSource = new DataTable();
                        grvWLog.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = ex.Message;
                }
            }
        }
        protected void grvWLog_RowDataBound(object sender, GridViewRowEventArgs e)
        {
              GridViewRow row = e.Row;
            if (row.RowType == DataControlRowType.Header)
            {
                GridViewRow newRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                GridViewRow newRow2 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Normal);
                GridViewRow newRow3 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Normal);

                // Row header 1
                TableHeaderCell cell = new TableHeaderCell();
                cell.RowSpan = 2;
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "TT";
                newRow1.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.RowSpan = 2;
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Trạm 變電站 ";
                newRow1.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.ColumnSpan = 6;
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "TRẠM ĐIỆN 變電站";
                cell.CssClass = "grvHeader";
                newRow1.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.ColumnSpan = 4;
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "TỦ PHÂN PHỐI 分配電箱";
                cell.CssClass = "grvHeader";
                newRow1.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.ColumnSpan = 6;
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "THIẾT BỊ SỬ DỤNG 設備使用";
                cell.CssClass = "grvHeader";
                newRow1.Controls.Add(cell);

                // Row header 2
                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "BIẾN THẾ<br/>變壓機(KVA)";
                newRow2.Controls.Add(cell);

                // Row header 2
                // Trạm Điện
                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "DAO CẮT <br/>總開關 （A)";
                newRow2.Controls.Add(cell);
                
                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "CB <br/>開關 ";
                newRow2.Controls.Add(cell);
                
                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "VỊ TRÍ DD<br/>量測電線區域 ";
                newRow2.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Cường độ dòng điện  <br/>電流(A)";
                newRow2.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Qui Cách dây dẫn <br/> 電纜規格";
                newRow2.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "NHIỆT ĐỘ DÂY DẪN  <br/>電線溫度";
                newRow2.Controls.Add(cell);

                // Tủ Phân phối
                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "TỦ PHÂN PHỐI <br/>分配電箱";
                newRow2.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Năm lắp đặt dây điện <br/> 電線年限";
                newRow2.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Cường độ dòng điện CB <br/>電流(A)";
                newRow2.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Cường độ dòng điện Thục tế <br/>實際使用電流(A)";
                newRow2.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "NHIỆT ĐỘ TỦ PHÂN PHỐI <br/>分配電箱溫度";
                newRow2.Controls.Add(cell);

                //Thiết bị sử dụng
                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "設編號 <br/>MSM";
                newRow2.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Năm lắp đặt dây điện  電線年限";
                newRow2.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "NHIỆT ĐỘ <br/>温度 ";
                newRow2.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "CB <br/>開關 ";
                newRow2.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "AP MAX";
                newRow2.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Cường độ dòng điện Thục tế  <br/>實際使用電流";
                newRow2.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "GHI CHÚ <br/>備註";
                newRow2.Controls.Add(cell);

                newRow1.CssClass = "grvHeader";
                newRow2.CssClass = "grvHeader";

                grvWLog.Controls[0].Controls.AddAt(0, newRow1);
                grvWLog.Controls[0].Controls.AddAt(1, newRow2);

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

        
    }
}