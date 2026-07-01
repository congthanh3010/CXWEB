using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace CXWeb.schedule
{
    public partial class xi_ma_report : System.Web.UI.Page
    {
        private static connectEIP conn;
        private static DataTable fields;
        public static DataTable data;

        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDFrom.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtDTo.Text = DateTime.Now.ToString("yyyy-MM-dd");
                data = new DataTable();
                fields = new DataTable();
                conn = new connectEIP();
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            if (txtDFrom.Text.Trim() == "")
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Chưa nhập ngày bắt đầu";
                return;
            }
            if (txtDTo.Text.Trim() == "")
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Chưa nhập ngày kết thúc";
                return;
            }
            data = GetData(txtDFrom.Text, txtDTo.Text);
            fields = GetFields(ddlReport.SelectedValue, string.Concat(char.ToUpper(ddlReport.SelectedValue[0]), ddlReport.SelectedValue.Substring(1)));
            grvData.DataSource = data;
            grvData.DataBind();
            lblMessage.Text = "";

            if (grvData.Rows.Count > 0)
                btnExcel.Enabled = true;
            else
                btnExcel.Enabled = false;
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            //var report = CreateReport();
            //// Export Excel
            //string file_name = string.Format("滾電電腦資料 Ghi chép sản xuất xi mạ {0}.xls", DateTime.Now.ToString("yyyy-MM-dd"));
            //using (var export_data = new MemoryStream())
            //{
            //    Response.Clear();
            //    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //    Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", file_name));
            //    Response.BinaryWrite(report);
            //    Response.Flush();
            //    Response.End();
            //}

            string file_name = string.Format("滾電電腦資料 Ghi chép sản xuất xi mạ {0}.xls", DateTime.Now.ToString("yyyy-MM-dd"));
            string attachment = $"attachment; filename={file_name}";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grvData.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }

        protected void grvData_DataBinding(object sender, EventArgs e)
        {
            var grid = sender as GridView;
            grid.Columns.Clear();
            foreach (DataRow field in fields.Rows)
            {
                BoundField bound = new BoundField();
                bound.DataField = field["FieldName"].ToString();
                string format = "{0:" + field["ValueFormat"].ToString() + "}" + field["Unit"].ToString();
                bound.DataFormatString = format;
                grid.Columns.Add(bound);
            }
            string[] title = ddlReport.SelectedItem.Text.Split('-');
            lblTiltle.Text = string.Format("{0}電腦資料<br/>GHI CHÉP THEO DÕI SẢN XUẤT {1}", title[0].Trim(), title[1].Trim().ToUpper());
        }

        protected void grvData_RowCreated(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < fields.Rows.Count; i++)
            {
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Center;
                if (i == 0 || i == 1)
                    e.Row.Cells[i].Width = new Unit("100px");
                else if (fields.Rows[i]["DataType"].ToString() == typeof(Int32).FullName
                    || fields.Rows[i]["DataType"].ToString() == typeof(Double).FullName)
                {
                    e.Row.Cells[i].Width = new Unit("30px");
                }
                else if (fields.Rows[i]["DataType"].ToString() == typeof(DateTime).FullName)
                    e.Row.Cells[i].Width = new Unit("60px");
                else
                    e.Row.Cells[i].Width = new Unit("80px");
            }
        }

        protected void grvData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridViewRow cn_header = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                GridViewRow vn_header = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Normal);

                for (int i = 0; i < fields.Rows.Count; i++)
                {
                    TableHeaderCell cell = new TableHeaderCell();
                    cell.HorizontalAlign = HorizontalAlign.Center;
                    cell.Wrap = true;
                    cell.CssClass = "grvHeader";
                    cell.Text = fields.Rows[i]["CNTitle"].ToString();
                    cn_header.Controls.Add(cell);

                    cell = new TableHeaderCell();
                    cell.HorizontalAlign = HorizontalAlign.Center;
                    cell.Wrap = true;
                    cell.CssClass = "grvHeader";
                    cell.Text = fields.Rows[i]["VNTitle"].ToString();
                    vn_header.Controls.Add(cell);
                }

                grvData.Controls[0].Controls.AddAt(0, cn_header);
                grvData.Controls[0].Controls.AddAt(1, vn_header);
            }
        }

        protected void ddlReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((sender as DropDownList).SelectedItem.Value == "xi_lan_log")
            {
                ddlMachine.Enabled = true;
                data.Clear();
            }
            else
            {
                ddlMachine.Enabled = false;
                data.Clear();
            }
        }

        protected DataTable GetData(string dateFrom, string dateTo)
        {
            DataTable result = new DataTable();
            string strQuery = $"SELECT * FROM {ddlReport.SelectedValue} \n"
                + "WHERE xml1 >= '" + dateFrom + " 00:00:00' \n"
                + "AND (xml2 <= '" + dateTo + " 23:59:59' OR xml3 <= '" + dateTo + " 23:59:59') \n";
            if (ddlReport.SelectedItem.Value == "xi_lan_log")
                strQuery += $"AND MachineId = '{ddlMachine.SelectedItem.Value}' \n";
            strQuery += "ORDER BY xml1";
            conn.myopen();
            result = conn.mysearch(strQuery);
            return result;
        }

        protected DataTable GetFields(string pageName, string tableName)
        {
            DataTable result = new DataTable();
            string strQuery = "SELECT FieldName, VNTitle, CNTitle, ExcelColumn, DataType, ValueFormat, Unit \n"
                + "FROM ProgramField \n"
                + "WHERE ProgramCode='" + pageName + "' AND TableName='" + tableName + "' \n"
                + "AND FieldName NOT IN ('Id', 'MachineId', 'UploadTime') \n"
                + "ORDER BY ExcelColumn";
            conn.myopen();
            result = conn.mysearch(strQuery);
            conn.myclose();
            return result;
        }

        protected byte[] CreateReport()
        {
            HSSFWorkbook workbook;
            using (FileStream fstream = new FileStream(HttpContext.Current.Server.MapPath("\\image\\Xi_lan_log.xls"), FileMode.Open, FileAccess.Write))
            {
                workbook = new HSSFWorkbook(fstream);
                ISheet sheet = workbook.GetSheet("Sheet1");
                fstream.Close();
            }

            MemoryStream mstream = new MemoryStream();
            workbook.Write(mstream);
            return mstream.GetBuffer();
        }
    }
}