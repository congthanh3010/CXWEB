using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Configuration;
using System.Web.UI.DataVisualization.Charting;
using System.Threading;
using System.Windows.Forms;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;

namespace CXWeb.schedule
{
    public partial class order_list : System.Web.UI.Page
    {
        private static DataSet mydata;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                confirm_date.Text = DateTime.Now.AddDays(0).ToString("yyyy/MM/dd");
                End_Date.Text = "2020/07/31";//DateTime.Now.AddDays(0).ToString("yyyy/MM/dd");
                txtQty_Merge.Text = "6000";
                txtWeek.Text = "8";
                query();
                hreport_msg.Value = "Chạy Thành Công";
            }
        }
        void query_Oracle()
        {
            connectTT myconn = new connectTT();

            string strsql = "";
           
            //-------------------------------------------------------------

            DataTable mydata = new DataTable();
            DataTable mydata2 = new DataTable();
            myconn.myopen();


            //strsql = "select machine_no,productivity from schedule_ma_prod_setting where machine_no ='" + machine_no.Text + "' order by machine_no";
            strsql = "select oea01,oea03,oea032,oea04,occ02,oeb12,to_char(oeb15,'yyyy/MM/dd') as oeb15,oeb04     from  v_2652_oea_q" +
                     " WHERE to_char(oea72,'yyyy/MM/dd')= '"+ confirm_date.Text+ "'" +
                     " order by oeb15 ";

            mydata = myconn.mysearch(strsql);

            mydata.Columns.Add("link1");
            for (int i=0;i< mydata.Rows.Count;i++)
            {
                //mydata.Rows[i]["link1"] = "/schedule/product_required_quantity.aspx?pr=" + mydata.Rows[i]["oeb04"].ToString();
                mydata.Rows[i]["link1"] = "/schedule/order_list_filter.aspx?pr=" + mydata.Rows[i]["oeb04"].ToString() + "&d=" + confirm_date.Text;
            }

            GridView1.DataSource = mydata;
            GridView1.DataBind();

          


            myconn.myclose();
        }
        void query()
        {

            connectTT myconn = new connectTT();
            connectEIP myconn_SQL = new connectEIP();


            //-------------------------------------------------------------

            mydata = new DataSet();
            

            System.Collections.Hashtable htPara = new System.Collections.Hashtable();
            
            htPara["BEGIN_DATE"] = confirm_date.Text;
            htPara["END_DATE"] = End_Date.Text;
            htPara["PERIOD"] = Convert.ToInt32(txtWeek.Text);


            mydata = myconn_SQL.ExecuteReturnDs("sp_FilterMain_V3", htPara, CommandType.StoredProcedure);
            //sp_FilterMain_V3 sp_FilterMain_test
            myconn_SQL.myclose();

            GridView1.DataSource = mydata.Tables[0];
            GridView1.DataBind();

            GridView2.DataSource = mydata.Tables[1];
            GridView2.DataBind();
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
            DataTable mydata2 = new DataTable();
            connectEIP myconn_SQL = new connectEIP();

            //Delete du lieu cu
            string strSQL = "schedule_Delete_Order";
            myconn_SQL.myopen();

            System.Collections.Hashtable htPara1 = new System.Collections.Hashtable();
            htPara1["CONFIRM_ID"] = confirm_date.Text;

            myconn_SQL.mySqlExecute(strSQL, htPara1, CommandType.StoredProcedure);
            myconn_SQL.myclose();

            bool is_Check = false;

            string strOrder_ID = "";

            string strOrder_No = "";
            string strItem_No = "";
            string strCus_No = "";
            string strCus_Name = "";
            string strDate = "";
            string strConfirm_Date = confirm_date.Text;
            double dbQty = 0;
            double dbQty_To_Pro = 0;
            DateTime dteNgay;
            int int_Count_Check = 0;
            myconn_SQL.myopen();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                
                strOrder_ID = "";
                strItem_No = ((System.Web.UI.WebControls.Label)GridView1.Rows[i].FindControl("lbl_Item_No")).Text;
                strOrder_No = ((System.Web.UI.WebControls.Label)GridView1.Rows[i].FindControl("lbl_Order_No")).Text;
                strCus_No = ((System.Web.UI.WebControls.Label)GridView1.Rows[i].FindControl("lbl_Cus_ID")).Text;
                strCus_Name = ((System.Web.UI.WebControls.Label)GridView1.Rows[i].FindControl("lbl_Cus_Name")).Text;
                strDate = ((System.Web.UI.WebControls.Label)GridView1.Rows[i].FindControl("lbl_Delivery_Date_Convert")).Text;
                dbQty = Convert.ToDouble(((System.Web.UI.WebControls.Label)GridView1.Rows[i].FindControl("lbl_Qty_Convert")).Text);
                dbQty_To_Pro = Convert.ToDouble(((System.Web.UI.WebControls.Label)GridView1.Rows[i].FindControl("lbl_Qty_To_Product")).Text);
                if (strDate != "")
                    dteNgay = Convert.ToDateTime(strDate);
                else
                    dteNgay = DateTime.Now.AddDays(10);


                if (dbQty_To_Pro <= 0)
                    continue;

                System.Collections.Hashtable htPara = new System.Collections.Hashtable();
                htPara["ORDER_ID"] = strOrder_ID;
                htPara["ORDER_NO"] = strOrder_No;
                htPara["ITEM_NO"] = strItem_No;
                htPara["CUS_NO"] = strCus_No;
                htPara["DELIVERY_DATE"] = dteNgay;
                htPara["CONFIRM_DATE"] = strConfirm_Date;
                htPara["CUS_NAME"] = strCus_Name;
                htPara["ORDER_QTY"] = dbQty;
                htPara["QTY_TO_PRODUCE"] = dbQty_To_Pro;

                mydata2 = myconn_SQL.ExecuteReturnDt("sp_Save_Order_Merging", htPara, CommandType.StoredProcedure);
                int_Count_Check += 1;
                
            }
            myconn_SQL.myclose();
            // MessageBox.Show("Tạo thành công!!!");
            if (int_Count_Check >= 1)
            {
                Response.Redirect("/schedule/order_list_Filter_V2.aspx?pr=" + strConfirm_Date + "&oqty=" + txtQty_Merge.Text);
            }

        }

        protected void btnExcel_Click1(object sender, EventArgs e)
        {
            hreport_msg.Value = " Đang xuất Excel......";
            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet1 = workbook.CreateSheet("Order List");
            IRow title = sheet1.CreateRow(0);
            IRow header = sheet1.CreateRow(1);
            int index;
            //DataSet mydata = new DataSet();
            // Title and Header
            List<string[]> list_orders = new List<string[]>()
            {
                new string[] {"Item No", "Item_No", "text"},
                new string[] {"Order No", "Order_No", "text" },
                new string[] {"Old Delivery Date", "Delivery_Date_Old", "M/dd/yyyy"},
                new string[] {"Delivery Date", "Delivery_Date", "M/dd/yyyy"},
                new string[] {"Qty Order", "Quantity", "#,###"},
                new string[] {"Inventory", "Qty_Inventory", "#,###"},
                new string[] {"Qty Produce", "Qty_Cacl", "#,###"}
            };

            List<string[]> summary_orders = new List<string[]>()
            {
                new string[] {"Item No", "Item_No", "text"},
                new string[] {"Delivery Date", "Delivery_Date", "M/dd/yyyy"},
                new string[] {"Qty Order", "Qty", "#,###"},
                new string[] {"Inventory", "Qty_Inventory", "#,###"},
                new string[] {"Wip", "Qty_WIP", "#,###"},
                new string[] {"Qty to Produce", "Qty_To_Produce", "#,###"}
            };
            ICell title_cell_1 = title.CreateCell(0);
            title_cell_1.SetCellValue("Order List (Bảng kê chi tiết đơn hàng)");
            var merge1 = new NPOI.SS.Util.CellRangeAddress(0, 0, 0, list_orders.Count - 1);
            sheet1.AddMergedRegion(merge1);

            int col_num = list_orders.Count;
            ICell title_cell_2 = title.CreateCell(col_num + 2);
            title_cell_2.SetCellValue("Summary Order (Bảng tổng hợp mặt hàng)");
            var merge2 = new NPOI.SS.Util.CellRangeAddress(0, 0, col_num + 2, col_num + 1 + summary_orders.Count);
            sheet1.AddMergedRegion(merge2);

            index = 0;
            foreach (string[] str in list_orders)
            {
                ICell cell = header.CreateCell(index);
                cell.SetCellValue(str[0]);
                index++;
            }
            index = col_num + 2;
            foreach (string[] str in summary_orders)
            {
                ICell cell = header.CreateCell(index);
                cell.SetCellValue(str[0]);
                index++;
            }
            string strCount;
            // Data
            for (int i = 0; i < mydata.Tables[0].Rows.Count; i++)
            {
                strCount = "Running Order List - " + i.ToString() + "/ " + mydata.Tables[0].Rows.Count.ToString();
                //AutoClosingMessageBox.Show(strCount,"Caption",1000);

                hreport_msg.Value = "Running Order List - " + i.ToString();
                IRow row = sheet1.CreateRow(i + 2);
                for (int j = 0; j < list_orders.Count; j++)
                {
                    ICell cell = row.CreateCell(j);
                    object value = mydata.Tables[0].Rows[i][list_orders[j][1]];
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
                    style.DataFormat = workbook.CreateDataFormat().GetFormat(list_orders[j][2]);
                    cell.CellStyle = style;
                    sheet1.AutoSizeColumn(j);
                }

                if (i < mydata.Tables[1].Rows.Count)
                {
                     strCount = "Running Order summary - " + i.ToString() + "/ " + mydata.Tables[0].Rows.Count.ToString();
                    //AutoClosingMessageBox.Show(strCount, "Caption", 1000);

                    for (int j = 0; j < summary_orders.Count; j++)
                    {
                        ICell cell = row.CreateCell(col_num + 2 + j);
                        object value = mydata.Tables[1].Rows[i][summary_orders[j][1]];
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
                        style.DataFormat = workbook.CreateDataFormat().GetFormat(summary_orders[j][2]);
                        cell.CellStyle = style;
                        sheet1.AutoSizeColumn(col_num + 2 + j);
                    }
                }
            }

            // Export Excel
            string file_name = string.Format("Order_List-{0}.xlsx", DateTime.Now.ToString("yyyyMMdd"));
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
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (FileUpload1.PostedFile != null)
            {
                // Upload and save files
                string path = string.Concat(Server.MapPath("~/image/" + FileUpload1.PostedFile.FileName));
                FileUpload1.SaveAs(path);

                // Connection string to Workbook
                string strConnExl = "";
                string extension = Path.GetExtension(path);
                if (extension == ".xlsx")
                    strConnExl = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1\"";
                else
                    strConnExl = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";

                using (OleDbConnection ExlConn = new OleDbConnection(strConnExl))
                {
                    ExlConn.Open();
                    try
                    {
                        DataTable dt = ExlConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                        foreach (DataRow tab in dt.Rows)
                        {
                            string sheet = tab["TABLE_NAME"].ToString();
                            OleDbCommand cmd = new OleDbCommand("SELECT * FROM [" + sheet + "]", ExlConn);
                            cmd.CommandType = CommandType.Text;

                            // Fill data to temp datatable
                            DataTable output = new DataTable(sheet);
                            new OleDbDataAdapter(cmd).Fill(output);

                            string day_code = DateTime.Now.ToString("yyyyMMdd");
                            if (output.Rows.Count > 0)
                            {
                                // Modify temp table
                                output.Columns.Add("Order ID (Order_ID)", typeof(string));
                                output.Columns.Add("Order No (Order_No)", typeof(string));
                                output.Columns.Add("Confirm Date (Confirm_Date)", typeof(DateTime));
                                output.Columns.Add("Confirm ID (Confirm_ID)", typeof(string));
                                output.Columns.Add("Delivery Date (Delivery_Date)", typeof(DateTime));
                                output.Columns.Add("Order Qty (Order_Qty)", typeof(double));

                                foreach (DataRow row in output.Rows)
                                {
                                    row["Order ID (Order_ID)"] = day_code + "-" + row["Item No (Item_No)"].ToString();
                                    row["Order No (Order_No)"] = day_code + "-" + row["Item No (Item_No)"].ToString();
                                    row["Confirm ID (Confirm_ID)"] = day_code + "-" + row["Item No (Item_No)"].ToString();
                                    row["Confirm Date (Confirm_Date)"] = DateTime.Now;
                                    row["Delivery Date (Delivery_Date)"] = DateTime.Now;
                                    row["Order Qty (Order_Qty)"] = row["Qty Produce (QTY_TO_PRODUCE)"];
                                }

                                // Delete data already exists
                                connectEIP SqlConn = new connectEIP();
                                SqlConn.myopen();
                                string query = "DELETE FROM Schedule_Order_Merging WHERE Order_ID LIKE '" + day_code + "%'";
                                SqlConn.mySqlExecute(query);

                                // Bulk Copy to SQL Server
                                if (SqlConn.BulkCopy(output, "Schedule_Order_Merging") != "")
                                {
                                    lblMessage.Text = "Have error when import data";
                                    lblMessage.ForeColor = System.Drawing.Color.Red;
                                    SqlConn.myclose();
                                    ExlConn.Close();
                                    return;
                                }
                                SqlConn.myclose();
                            }
                        }
                        lblMessage.Text = "Import file successfull";
                        lblMessage.ForeColor = System.Drawing.Color.Blue;
                    }
                    catch
                    {
                        lblMessage.Text = "File can not uploaded";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                    ExlConn.Close();
                }
            }
        }

        //public class AutoClosingMessageBox
        //{
        //    System.Threading.Timer _timeoutTimer;
        //    string _caption;
        //    AutoClosingMessageBox(string text, string caption, int timeout)
        //    {
        //        _caption = caption;
        //        _timeoutTimer = new System.Threading.Timer(OnTimerElapsed,
        //            null, timeout, System.Threading.Timeout.Infinite);
        //        using (_timeoutTimer)
        //              MsgBox(text);
        //        //MessageBox.Show(text, caption);
        //    }
        //    public static void Show(string text, string caption, int timeout)
        //    {
        //        new AutoClosingMessageBox(text, caption, timeout);
        //    }
        //    void OnTimerElapsed(object state)
        //    {
        //        IntPtr mbWnd = FindWindow("#32770", _caption); // lpClassName is #32770 for MessageBox
        //        if (mbWnd != IntPtr.Zero)
        //            SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        //        _timeoutTimer.Dispose();
        //    }
        //    private void MsgBox(string sMessage)
        //    {
        //        string msg = "&lt;script language=\"javascript\"&gt;";
        //        msg += "alert('" + sMessage + "');";
        //        msg += "&lt;/script>";
        //        Response.Write(msg);
        //    }
        //    const int WM_CLOSE = 0x0010;
        //    private object Response;

        //    [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        //    static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        //    [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        //    static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
        //}


    }
}