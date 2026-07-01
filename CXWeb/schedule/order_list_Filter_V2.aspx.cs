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
using System.Windows.Forms;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.Util;
using System.IO;

namespace CXWeb.schedule
{
    public partial class order_list_Filter_V2 : System.Web.UI.Page
    {
        private static DataSet mydata_Schedule;
        private static DataSet mydataset;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                confirm_date.Text = base.Request.QueryString["pr"];
                Start_Date.Text = base.Request.QueryString["pr"]; ;
                txtQty_Merge.Text = base.Request.QueryString["oqty"];
                query();
            }
        }
        void query_Oracle()
        {
           
        }
        void query()
        {

            connectEIP myconn = new connectEIP();

            string strsql = "";

            //-------------------------------------------------------------

            mydataset = new DataSet();
            DataTable mydata2 = new DataTable();
            myconn.myopen();
            if (txtQty_Merge.Text == "")
                return;

            System.Collections.Hashtable htPara = new System.Collections.Hashtable();
            htPara["CONFIRM_ID"] = confirm_date.Text;
            htPara["QTY_COLLECT"] = Convert.ToDouble(txtQty_Merge.Text);
            htPara["START_DATE"] = Start_Date.Text;
            

            mydataset = myconn.ExecuteReturnDs("schedule_Order_Collection", htPara, CommandType.StoredProcedure);

            myconn.myclose();

            GridView1.DataSource = mydataset.Tables[0];
            GridView1.DataBind();

            GridView2.DataSource = mydataset.Tables[1];
            GridView2.DataBind();

            myconn.myclose();
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
        protected void btnIns_Click(object sender, EventArgs e)
        {
            
        }
        protected void btnXem_Click(object sender, EventArgs e)
        {
            query();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            DataTable mydata = new DataTable();
            DataTable machine_data = new DataTable();
            string sql = "";
            connectEIP myconn = new connectEIP();

            myconn.myopen();

           

            
            myconn.myclose();

            hreport_msg.Value = " ";

            query();
        }
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("/schedule/main.aspx");
        }

        protected void btnOrder_Merge_Click(object sender, EventArgs e)
        {
            btnXem.Enabled = false;
            btnReturn.Enabled = false;
            Scheduling_Progress.Enabled = false;
            btnExcel.Enabled = false;

            //Response.Redirect("/schedule/order_merging.aspx?pr=" + confirm_date.Text);
            // Getdata BOM, Worstep
            connectTT myconn = new connectTT();
            connectEIP myconn_SQL = new connectEIP();

            mydata_Schedule = new DataSet();


            System.Collections.Hashtable htPara = new System.Collections.Hashtable();

            htPara["ORDER_ID"] = confirm_date.Text;
            htPara["_ITEM_NO"] = "";


            mydata_Schedule = myconn_SQL.ExecuteReturnDs("sp_GetSchedule", htPara, CommandType.StoredProcedure);
            //sp_FilterMain_V3 sp_FilterMain_test
            myconn_SQL.myclose();



            IWorkbook workbook = new XSSFWorkbook();

            ISheet sheet_BOM = workbook.CreateSheet("BOM");
            IRow title_BOM = sheet_BOM.CreateRow(0);
            IRow header_BOM = sheet_BOM.CreateRow(1);

            ISheet sheet_Workstep = workbook.CreateSheet("WORKSTEP");
            IRow title_Workstep = sheet_Workstep.CreateRow(0);
            IRow header_Workstep = sheet_Workstep.CreateRow(1);

            ISheet sheet_Machine1 = workbook.CreateSheet("MachineSchedule1");
            IRow title_Machine1 = sheet_Machine1.CreateRow(0);
            IRow header_Machine1 = sheet_Machine1.CreateRow(1);

            ISheet sheet_Machine2 = workbook.CreateSheet("MachineSchedule2");
            IRow title_Machine2 = sheet_Machine2.CreateRow(0);
            IRow header_Machine2 = sheet_Machine2.CreateRow(1);

            ISheet sheet_XL = workbook.CreateSheet("XepLich");
            IRow title_XL = sheet_XL.CreateRow(0);
            IRow header_XL = sheet_XL.CreateRow(1);

            int index;

            // Title and Header

            List<string[]> List_Bom = new List<string[]>()
            {               
                new string[] {"Order Merging ID", "Order_ID", "text"},
                new string[] {"Item No", "Item_No", "text"},
                new string[] {"Số lượng đơn hàng", "Qty", "#,###"},
                new string[] {"Tỷ lệ Hao hụt", "A_Rate", "#,###"},
                new string[] {"Số lượng sau HH", "Qty_Rate", "#,###"},
                new string[] {"Số lượng WIP", "Qty_WIP", "#,###"},
                new string[] {"Số lượng đã sử dụng", "Qty_WIP_Da_Su_Dung", "#,###"},
                new string[] {"Số lượng còn lại", "Qty_WIP_B", "#,###"},
                new string[] {"Số lượng để sản xuất", "Qty_Produce", "#,###"},
                new string[] {"Skip Time", "Is_Skip_Time", "text"},
                new string[] {"Thời gian", "Time_Skip", "#,###"},
                new string[] {"Bắt đầu", "Begin_Date", "M/dd/yyyy"},
                new string[] {"Kết thúc", "End_Date", "M/dd/yyyy"}
            };

            List<string[]> List_WorkStep = new List<string[]>()
            {
                new string[] {"Order Merging ID", "Order_ID", "text"},
                new string[] {"Item No", "Item_No", "text"},
                new string[] {"Số Trạm", "Step_Num", "text" },
                new string[] {"Mã Trạm", "Step_No", "text" },
                new string[] {"Tên Trạm", "Step_Name", "text" },
                new string[] {"Skip Time", "Is_Skip_Time", "text"},
                new string[] {"Thời gian", "Time_S", "#,###"},
                new string[] {"Số lượng cần sản xuất", "Qty_To_Produce", "#,###"},
                new string[] {"WIP", "Qty_WIP", "#,###"},
                new string[] {"WIP tổng", "Qty_Wip_Total", "#,###"},
                new string[] {"Số lượng đã sử dụng", "Qty_WIP_Da_Su_Dung", "#,###"},
                new string[] {"Số lượng còn lại", "Qty_WIP_B", "#,###"},
                new string[] {"Số lượng để xếp lịch", "Qty_To_Schedule", "#,###"},
                new string[] {"Số lượng để mở đơn", "Qty_To_Open", "#,###"},
                new string[] {"Tổng ngày", "Total_Day", "#,###"},
                new string[] {"Tổng giờ", "Total_Hour", "#,###"},

                new string[] {"Bắt đầu", "Begin_Date", "M/dd/yyyy"},
                new string[] {"Kết thúc", "End_Date", "M/dd/yyyy"},
                new string[] {"Bắt đầu 2", "Begin_Date_2", "M/dd/yyyy"},
                new string[] {"Kết thúc 2", "End_Date_2", "M/dd/yyyy"}
            };
            List<string[]> List_MachineShedule1 = new List<string[]>()
            {
                new string[] {"Order Merging ID", "Order_ID", "text"},
                new string[] {"Item No", "Item_No", "text"},
                new string[] {"Ngày SX", "Date_Input", "M/dd/yyyy"},
                new string[] {"Bắt đầu", "Begin_Date", "M/dd/yyyy"},
                new string[] {"Kết thúc", "End_Date", "M/dd/yyyy"},
                new string[] {"Số Trạm", "Step_Num", "text" },
                new string[] {"Mã Trạm", "Step_No", "text" },
                new string[] {"Tên Trạm", "Sta_No", "text" },
                new string[] {"Mã máy", "Machine_No", "text" },
                new string[] {"Số lượng để mở đơn", "Qty_0", "#,###"},
                new string[] {"Số lượng để mở đơn", "Qty", "#,###"},
            };
            List<string[]> List_MachineShedule2 = new List<string[]>()
            {
                new string[] {"Order Merging ID", "Order_ID", "text"},
                new string[] {"Item No", "Item_No", "text"},
                new string[] {"Ngày SX", "Date_Input", "M/dd/yyyy"},
                new string[] {"Bắt đầu", "Begin_Date", "M/dd/yyyy"},
                new string[] {"Kết thúc", "End_Date", "M/dd/yyyy"},
                new string[] {"Số Trạm", "Step_Num", "text" },
                new string[] {"Mã Trạm", "Step_No", "text" },
                new string[] {"Tên Trạm", "Sta_No", "text" },
                new string[] {"Mã Máy", "Machine_No", "text" },
                new string[] {"Số lượng để mở đơn", "Qty_0", "#,###"},
                new string[] {"Số lượng để mở đơn", "Qty", "#,###"},
            };

            List<string[]> List_XepLich = new List<string[]>()
            {
                 new string[] {"Mã máy", "Machine", "text"}
            };

            List<string[]> List_XepLich2 = new List<string[]>()
            {
                 new string[] {"Mã máy", "Machine", "text"}
            };
            string strTen_Ma = "";
            string strTen_Qty = "";
            string strTen_Tram = "";

            for (int i = 0; i < mydata_Schedule.Tables[6].Rows.Count; i++)
            {
                
                strTen_Ma = mydata_Schedule.Tables[6].Rows[i][1].ToString();
                strTen_Qty = mydata_Schedule.Tables[6].Rows[i][2].ToString();
                strTen_Tram = mydata_Schedule.Tables[6].Rows[i][3].ToString();
                List_XepLich2 = new List<string[]>()
                {
                    new string[] { strTen_Ma, strTen_Ma, "text" },
                    new string[] { strTen_Tram, strTen_Tram, "text" },
                    new string[] { strTen_Qty, strTen_Qty, "#,###" },
                };
                List_XepLich.AddRange(List_XepLich2);
            }

            //// BOM
            //ICell title_cell_BOM= title_BOM.CreateCell(0);
            //title_cell_BOM.SetCellValue("BOM FROM ORDER");
            //var merge_BOM = new NPOI.SS.Util.CellRangeAddress(0, 0, 0, List_Bom.Count - 1);
            //sheet_BOM.AddMergedRegion(merge_BOM);

            //index = 0;
            //foreach (string[] str in List_Bom)
            //{
            //    ICell cell = header_BOM.CreateCell(index);
            //    cell.SetCellValue(str[0]);
            //    index++;
            //}

            //// Data BOM
            //for (int i = 0; i < mydata_Schedule.Tables[0].Rows.Count; i++)
            //{
            //    IRow row = sheet_BOM.CreateRow(i + 2);
            //    for (int j = 0; j < List_Bom.Count; j++)
            //    {
            //        ICell cell = row.CreateCell(j);
            //        object value = mydata_Schedule.Tables[0].Rows[i][List_Bom[j][1]];
            //        string type = value.GetType().Name.ToLower();
            //        if (string.IsNullOrWhiteSpace(value.ToString()))
            //            cell.SetCellValue(string.Empty);
            //        else if (type == "string")
            //            cell.SetCellValue(value.ToString());
            //        else if (type == "int32" || type == "decimal")
            //            cell.SetCellValue(Convert.ToInt32(value));
            //        else if (type == "double")
            //            cell.SetCellValue(Convert.ToDouble(value));
            //        else if (type == "datetime")
            //            cell.SetCellValue(Convert.ToDateTime(value));
            //        else
            //            cell.SetCellValue(string.Empty);
            //        ICellStyle style = workbook.CreateCellStyle();
            //        style.DataFormat = workbook.CreateDataFormat().GetFormat(List_Bom[j][2]);
            //        cell.CellStyle = style;
            //        sheet_BOM.AutoSizeColumn(j);
            //    }
            //}
            //// workstep
            //ICell title_cell_Workstep = title_Workstep.CreateCell(0);
            //title_cell_Workstep.SetCellValue("WORK STEP");
            //var merge_Workstep = new NPOI.SS.Util.CellRangeAddress(0, 0, 0, List_WorkStep.Count - 1);
            //sheet_Workstep.AddMergedRegion(merge_Workstep);

            //index = 0;
            //foreach (string[] str in List_WorkStep)
            //{
            //    ICell cell = header_Workstep.CreateCell(index);
            //    cell.SetCellValue(str[0]);
            //    index++;
            //}

            //// Data workstep
            //for (int i = 0; i < mydata_Schedule.Tables[1].Rows.Count; i++)
            //{
            //    IRow row = sheet_Workstep.CreateRow(i + 2);
            //    for (int j = 0; j < List_WorkStep.Count; j++)
            //    {
            //        ICell cell = row.CreateCell(j);
            //        object value = mydata_Schedule.Tables[1].Rows[i][List_WorkStep[j][1]];
            //        string type = value.GetType().Name.ToLower();
            //        if (string.IsNullOrWhiteSpace(value.ToString()))
            //            cell.SetCellValue(string.Empty);
            //        else if (type == "string")
            //            cell.SetCellValue(value.ToString());
            //        else if (type == "int32" || type == "decimal")
            //            cell.SetCellValue(Convert.ToInt32(value));
            //        else if (type == "double")
            //            cell.SetCellValue(Convert.ToDouble(value));
            //        else if (type == "datetime")
            //            cell.SetCellValue(Convert.ToDateTime(value));
            //        else
            //            cell.SetCellValue(string.Empty);
            //        ICellStyle style = workbook.CreateCellStyle();
            //        style.DataFormat = workbook.CreateDataFormat().GetFormat(List_WorkStep[j][2]);
            //        cell.CellStyle = style;
            //        sheet_Workstep.AutoSizeColumn(j);
            //    }
            //}

            //// Machine Shedule 1
            //ICell title_cell_Machine1 = title_Machine1.CreateCell(0);
            //title_cell_Machine1.SetCellValue("MACHINE SHEDULE - LỊCH MÁY");
            //var merge_Machine1 = new NPOI.SS.Util.CellRangeAddress(0, 0, 0, List_MachineShedule1.Count - 1);
            //sheet_Machine1.AddMergedRegion(merge_Machine1);

            //index = 0;
            //foreach (string[] str in List_MachineShedule1)
            //{
            //    ICell cell = header_Machine1.CreateCell(index);
            //    cell.SetCellValue(str[0]);
            //    index++;
            //}

            //// Data Machine Schedule 1
            //for (int i = 0; i < mydata_Schedule.Tables[2].Rows.Count; i++)
            //{
            //    IRow row = sheet_Machine1.CreateRow(i + 2);
            //    for (int j = 0; j < List_MachineShedule1.Count; j++)
            //    {
            //        ICell cell = row.CreateCell(j);
            //        object value = mydata_Schedule.Tables[2].Rows[i][List_MachineShedule1[j][1]];
            //        string type = value.GetType().Name.ToLower();
            //        if (string.IsNullOrWhiteSpace(value.ToString()))
            //            cell.SetCellValue(string.Empty);
            //        else if (type == "string")
            //            cell.SetCellValue(value.ToString());
            //        else if (type == "int32" || type == "decimal")
            //            cell.SetCellValue(Convert.ToInt32(value));
            //        else if (type == "double")
            //            cell.SetCellValue(Convert.ToDouble(value));
            //        else if (type == "datetime")
            //            cell.SetCellValue(Convert.ToDateTime(value));
            //        else
            //            cell.SetCellValue(string.Empty);
            //        ICellStyle style = workbook.CreateCellStyle();
            //        style.DataFormat = workbook.CreateDataFormat().GetFormat(List_MachineShedule1[j][2]);
            //        cell.CellStyle = style;
            //        sheet_Machine1.AutoSizeColumn(j);
            //    }
            //}
            // Machine Schedule 2
            //ICell title_cell_Machine2 = title_Machine2.CreateCell(0);
            //title_cell_Machine2.SetCellValue("MACHINE SHEDULE - LỊCH MÁY");
            //var merge_Machine2 = new NPOI.SS.Util.CellRangeAddress(0, 0, 0, List_MachineShedule2.Count - 1);
            //sheet_Machine2.AddMergedRegion(merge_Machine2);

            //index = 0;
            //foreach (string[] str in List_MachineShedule2)
            //{
            //    ICell cell = header_Machine2.CreateCell(index);
            //    cell.SetCellValue(str[0]);
            //    index++;
            //}

            //// Data Machine Schedule 2
            //for (int i = 0; i < mydata_Schedule.Tables[3].Rows.Count; i++)
            //{
            //    IRow row = sheet_Machine2.CreateRow(i + 2);
            //    for (int j = 0; j < List_MachineShedule2.Count; j++)
            //    {
            //        ICell cell = row.CreateCell(j);
            //        object value = mydata_Schedule.Tables[3].Rows[i][List_MachineShedule2[j][1]];
            //        string type = value.GetType().Name.ToLower();
            //        if (string.IsNullOrWhiteSpace(value.ToString()))
            //            cell.SetCellValue(string.Empty);
            //        else if (type == "string")
            //            cell.SetCellValue(value.ToString());
            //        else if (type == "int32" || type == "decimal")
            //            cell.SetCellValue(Convert.ToInt32(value));
            //        else if (type == "double")
            //            cell.SetCellValue(Convert.ToDouble(value));
            //        else if (type == "datetime")
            //            cell.SetCellValue(Convert.ToDateTime(value));
            //        else
            //            cell.SetCellValue(string.Empty);
            //        ICellStyle style = workbook.CreateCellStyle();
            //        style.DataFormat = workbook.CreateDataFormat().GetFormat(List_MachineShedule2[j][2]);
            //        cell.CellStyle = style;
            //        sheet_Machine2.AutoSizeColumn(j);
            //    }
            //}

            //// Machine Schedule 2
            //ICell title_cell_XL = title_XL.CreateCell(0);
            //title_cell_XL.SetCellValue("XẾP LỊCH MÁY");
            //var merge_XL = new NPOI.SS.Util.CellRangeAddress(0, 0, 0, List_XepLich.Count - 1);
            //sheet_XL.AddMergedRegion(merge_XL);

            //index = 0;
            //foreach (string[] str in List_XepLich)
            //{
            //    ICell cell = header_XL.CreateCell(index);
            //    cell.SetCellValue(str[0]);
            //    index++;
            //}

            // Data Xep lich
            for (int i = 0; i < mydata_Schedule.Tables[5].Rows.Count; i++)
            {
                IRow row = sheet_XL.CreateRow(i + 2);
                for (int j = 0; j < List_XepLich.Count; j++)
                {
                    ICell cell = row.CreateCell(j);
                    object value = mydata_Schedule.Tables[5].Rows[i][List_XepLich[j][1]];
                    string type = value.GetType().Name.ToLower();
                    if (string.IsNullOrWhiteSpace(value.ToString()))
                        cell.SetCellValue(string.Empty);
                    else if (type == "string")
                        cell.SetCellValue(value.ToString());
                    else if (type == "int32" || type == "decimal")
                        cell.SetCellValue(Convert.ToInt32(value));
                    else if (type == "double")
                        cell.SetCellValue(Convert.ToDouble(value));
                    else if (type == "datetime")
                        cell.SetCellValue(Convert.ToDateTime(value));
                    else
                        cell.SetCellValue(string.Empty);
                    //ICellStyle style = workbook.CreateCellStyle();
                    //style.DataFormat = workbook.CreateDataFormat().GetFormat(List_XepLich[j][2]);
                    //cell.CellStyle = style;
                    //sheet_XL.AutoSizeColumn(j);
                }
            }

            for (int i = 0; i < List_XepLich.Count; i++)
            {
                ICellStyle style = workbook.CreateCellStyle();
                style.DataFormat = workbook.CreateDataFormat().GetFormat(List_XepLich[i][2]);
                sheet_XL.SetDefaultColumnStyle(i, style);
                sheet_XL.AutoSizeColumn(i);
            }

            // Export Excel
            string file_name = string.Format("Scheduling-Progress-{0}.xlsx", DateTime.Now.ToString("yyyyMMdd"));
            using (var export_data = new MemoryStream())
            {
                Response.Clear();
                workbook.Write(export_data);
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", file_name));
                Response.BinaryWrite(export_data.GetBuffer());
                Response.Flush();
                Response.End();
            }
            btnXem.Enabled = true;
            btnReturn.Enabled = true;
            Scheduling_Progress.Enabled = true;
            btnExcel.Enabled = true;
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            btnXem.Enabled = false;
            btnReturn.Enabled = false;
            Scheduling_Progress.Enabled = false;
            btnExcel.Enabled = false;
            IWorkbook workbook = new XSSFWorkbook();

            #region Sheet Order Collection
            ISheet sheet1 = workbook.CreateSheet("Order Collection");
            List<string[]> summary_headers = new List<string[]>()
            {
                new string[] {"Order Merging ID", "Order_ID", "text" },
                new string[] {"Item", "Item_No", "text" },
                new string[] {"Qty", "Order_Qty", "#,###" },
                new string[] {"Qty to Produce", "Quantity", "#,###" },
                new string[] {"Delivery Date", "Delivery_Date", "MM/dd/yyyy" }
            };
            //ExportExcel_Sheet(workbook, "Order Collection", "Order Collection (Bảng gộp đơn)", summary_headers, mydataset.Tables[1]);

            IRow title1 = sheet1.CreateRow(0);
            ICell title_cell1 = title1.CreateCell(0);
            title_cell1.SetCellValue("Order Collection (Bảng gộp đơn)");
            var title_merge = new NPOI.SS.Util.CellRangeAddress(0, 0, 0, summary_headers.Count - 1);
            sheet1.AddMergedRegion(title_merge);

            ICellStyle style1 = workbook.CreateCellStyle();
            style1.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            style1.VerticalAlignment = VerticalAlignment.Center;
            IFont font1 = workbook.CreateFont();
            //font1.FontName = "Calibri";
            //font1.FontHeight = 22;
            //font1.IsBold = true;
            //style1.SetFont(font1);
            title_cell1.CellStyle = style1;

            IRow header1 = sheet1.CreateRow(1);
            for (int i = 0; i < summary_headers.Count; i++)
            {
                ICell cell = header1.CreateCell(i);
                cell.SetCellValue(summary_headers[i][0]);

                ICellStyle style = workbook.CreateCellStyle();
                style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                style.VerticalAlignment = VerticalAlignment.Center;
                style.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium;
                style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium;
                style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium;
                style.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium;
                //style.FillBackgroundColor = HSSFColor.Yellow.Index;

                //IFont font = workbook.CreateFont();
                //font.FontName = "Calibri";
                //font.FontHeight = 11;
                //font.IsBold = true;

                //style.SetFont(font);
                cell.CellStyle = style;
            }

            for (int i = 0; i < mydataset.Tables[1].Rows.Count; i++)
            {
                IRow row = sheet1.CreateRow(i + 2);
                for (int j = 0; j < summary_headers.Count; j++)
                {
                    ICell cell = row.CreateCell(j);
                    object value = mydataset.Tables[1].Rows[i][summary_headers[j][1]];
                    string type = value.GetType().Name.ToLower();
                    if (string.IsNullOrWhiteSpace(value.ToString()))
                        cell.SetCellValue(string.Empty);
                    else if (type == "string")
                        cell.SetCellValue(value.ToString());
                    else if (type == "int32" || type == "decimal")
                        cell.SetCellValue(Convert.ToInt32(value));
                    else if (type == "double")
                        cell.SetCellValue(Convert.ToDouble(value));
                    else if (type == "datetime")
                        cell.SetCellValue(Convert.ToDateTime(value));
                    else
                        cell.SetCellValue(string.Empty);

                    ICellStyle style = workbook.CreateCellStyle();
                    style.DataFormat = workbook.CreateDataFormat().GetFormat(summary_headers[j][2]);
                    //style.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium;
                    //style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium;
                    //style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium;
                    //style.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium;
                    //IFont font = workbook.CreateFont();
                    //font.FontName = "Calibri";
                    //font.FontHeight = 11;

                    //style.SetFont(font);
                    cell.CellStyle = style;
                    sheet1.AutoSizeColumn(j);
                }
            }
            #endregion

            #region Sheet Order Collection Detail
            ISheet sheet2 = workbook.CreateSheet("Order Collection Detail");
            List<string[]> detail_headers = new List<string[]>()
            {
                new string[] {"Order Merging ID", "Order_ID", "text" },
                new string[] {"Order No", "Order_No", "text" },
                new string[] {"Item No", "Item_No", "text" },
                new string[] {"Delivery Date", "Delivery_Date", "MM/dd/yyyy" },
                new string[] {"Qty", "Order_Qty", "#,###" },
                new string[] {"Qty to Produce", "Qty_To_Produce", "#,###" }
            };
            //ExportExcel_Sheet(workbook, "Order Collection Detail", "", detail_headers, mydataset.Tables[0]);

            IRow header2 = sheet2.CreateRow(0);
            for (int i = 0; i < detail_headers.Count; i++)
            {
                ICell cell = header2.CreateCell(i);
                cell.SetCellValue(detail_headers[i][0]);

                ICellStyle style = workbook.CreateCellStyle();
                style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                style.VerticalAlignment = VerticalAlignment.Center;
                style.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium;
                style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium;
                style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium;
                style.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium;
                style.FillBackgroundColor = HSSFColor.Yellow.Index;

                //IFont font = workbook.CreateFont();
                //font.FontName = "Calibri";
                //font.FontHeight = 11;
                //font.IsBold = true;

                //style.SetFont(font);
                cell.CellStyle = style;
            }

            for (int i = 0; i < mydataset.Tables[0].Rows.Count; i++)
            {
                IRow row = sheet2.CreateRow(i + 1);
                for (int j = 0; j < detail_headers.Count; j++)
                {
                    ICell cell = row.CreateCell(j);
                    object value = mydataset.Tables[0].Rows[i][detail_headers[j][1]];
                    string type = value.GetType().Name.ToLower();
                    if (string.IsNullOrWhiteSpace(value.ToString()))
                        cell.SetCellValue(string.Empty);
                    else if (type == "string")
                        cell.SetCellValue(value.ToString());
                    else if (type == "int32" || type == "decimal")
                        cell.SetCellValue(Convert.ToInt32(value));
                    else if (type == "double")
                        cell.SetCellValue(Convert.ToDouble(value));
                    else if (type == "datetime")
                        cell.SetCellValue(Convert.ToDateTime(value));
                    else
                        cell.SetCellValue(string.Empty);

                    ICellStyle style = workbook.CreateCellStyle();
                    style.DataFormat = workbook.CreateDataFormat().GetFormat(detail_headers[j][2]);
                    //style.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium;
                    //style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium;
                    //style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium;
                    //style.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium;
                    //IFont font = workbook.CreateFont();
                    //font.FontName = "Calibri";
                    //font.FontHeight = 11;

                    //style.SetFont(font);
                    cell.CellStyle = style;
                    sheet2.AutoSizeColumn(j);
                }
            }
            #endregion            

            // Export Excel
            string file_name = string.Format("Order Collection-{0}.xlsx", DateTime.Now.ToString("yyyyMMdd"));
            using (var export_data = new MemoryStream())
            {
                Response.Clear();
                workbook.Write(export_data);
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", file_name));
                Response.BinaryWrite(export_data.GetBuffer());
                Response.Flush();
                Response.End();
            }
            btnXem.Enabled = true;
            btnReturn.Enabled = true;
            Scheduling_Progress.Enabled = true;
            btnExcel.Enabled = true;
        }


    }

}