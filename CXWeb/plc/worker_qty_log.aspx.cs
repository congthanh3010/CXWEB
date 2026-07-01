using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace CXWeb.plc
{
    public partial class worker_qty_log : System.Web.UI.Page
    {
        private static connectEIP conn;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                conn = new connectEIP();
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDate.Text))
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Chưa nhập ngày";
                return;
            }

            DataTable data = GetData(txtDate.Text.Trim());
            grvWLog.DataSource = data;
            grvWLog.DataBind();
            lblMessage.Text = string.Empty;
        }

        protected DataTable GetData(string date)
        {
            string strQuery = $"SELECT ROW_NUMBER() OVER (ORDER BY WSID) AS NumOrd,* FROM DepWorkerQty WHERE CreateDate='{date}' ORDER BY WSID";
            DataTable result = new DataTable();
            conn.myopen();
            result = conn.mysearch(strQuery);
            conn.myclose();
            return result;
        }

        protected void grvWLog_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;
            if (row.RowType == DataControlRowType.Header)
            {
                GridViewRow newRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);

                // Row header 1
                TableHeaderCell cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "TT";
                newRow.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.ColumnSpan = 2;
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Tên đơn vị";
                cell.CssClass = "grvHeader";
                newRow.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Số lượng nhân viên<br/>人數";
                cell.CssClass = "grvHeader";
                newRow.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Tổng số giờ<br/>總時數";
                cell.CssClass = "grvHeader";
                newRow.Controls.Add(cell);

                // Add header to gridview
                newRow.CssClass = "grvHeader";
                grvWLog.Controls[0].Controls.AddAt(0, newRow);
            }
        }
    }
}