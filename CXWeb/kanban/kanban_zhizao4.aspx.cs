using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace CXWeb.kanban
{
    public partial class kanban_zhizao4 : System.Web.UI.Page
    {
        private static DataTable data = new DataTable();
        private static int page_index = 1;
        private static string unit = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            unit = base.Request.QueryString["u"];
            if (!IsPostBack)
            {
                query();
            }
        }

        private void query()
        {
            connectTT conn = new connectTT();
            conn.myopen();

            string strsql = "";
            if (page_index == 1)
            {
                //strsql = "select s.b, s.a "
                //    + "from "
                //    + "(select t.* , "
                //    + "(SELECT DISTINCT gen02 FROM v_2684_shb "
                //    + "             WHERE shb10 = t.shb10 AND shb081 = t.shb081 AND shb09 = t.tc_jhl05 "
                //    + "             AND shb03 = t.tc_jhl02 AND shb08 = t.tc_jhl01 and rownum = 1) b, "
                //    + "case when round(t.shb111 / tc_jhl06 * 100, 2) >= 100 then 100 else round(t.shb111 / tc_jhl06 * 100, 2) end a "
                //    + "from v_2652_shb_jhl2 t "
                //    + "where to_char(tc_jhl02, 'yyyy/MM/dd') = to_char(sysdate - 1, 'yyyy/MM/dd') "
                //    + $"and tc_jhl06 > 0 and shb10 is not null and tc_jhl01 = 'CA1' and gem01='{unit}' "
                //    + "order by A desc "
                //    + ") s "
                //    + "where rownum <= 5";
                strsql = "select s.tc_jhl05 c,(SELECT DISTINCT gen02 FROM v_2684_shb "
                    + "             WHERE shb10 = s.shb10 AND shb081 = s.shb081 AND shb09 = s.tc_jhl05 "
                    + "             AND shb03 = s.tc_jhl02 AND shb08 = s.tc_jhl01 and rownum = 1 ) b,s.a "
                    + "from "
                    + " (select t.* ,"
                    + "case when round(t.shb111 / tc_jhl06 * 100, 2) >= 100 then 100 else round(t.shb111 / tc_jhl06 * 100, 2) end a "
                    + "from v_2652_shb_jhl2 t "
                    + "where to_char(tc_jhl02, 'yyyy/MM/dd') = to_char(sysdate - 1, 'yyyy/MM/dd') "
                    + $"and tc_jhl06 > 0 and shb10 is not null and tc_jhl01 = 'CA1' and gem01 = '{unit}' "
                    + "order by A desc "
                    + ") s "
                    + "where rownum <= 5";
                GridView1.Columns[0].Visible = true;
                GridView1.Columns[0].HeaderText = "機台代號</br>Mã số máy";
                GridView1.Columns[1].HeaderText = "操作人員姓名</br>Họ tên thao tác viên";
                GridView1.Columns[2].HeaderText = "計畫達成率</br>Tỷ lệ hoàn thành kế hoạch";
                lblHead.Text = "日計畫達成率前5名</br>Top 5 tỷ lệ hoàn thành kế hoạch cao nhất trong ngày";

            }
            else if (page_index == 2)
            {
                //strsql = "select s.b, s.a "
                //    + "from "
                //    + "(select t.* , "
                //    + "(SELECT DISTINCT gen02 FROM v_2684_shb "
                //    + "             WHERE shb10 = t.shb10 AND shb081 = t.shb081 AND shb09 = t.tc_jhl05 "
                //    + "             AND shb03 = t.tc_jhl02 AND shb08 = t.tc_jhl01 and rownum = 1) b, "
                //    + "case when round(t.shb111 / tc_jhl06 * 100, 2) >= 100 then 100 else round(t.shb111 / tc_jhl06 * 100, 2) end a "
                //    + "from v_2652_shb_jhl2 t "
                //    + "where to_char(tc_jhl02, 'yyyy/MM/dd') = to_char(sysdate - 1, 'yyyy/MM/dd') "
                //    + $"and tc_jhl06 > 0 and shb10 is not null and tc_jhl01 = 'CA1' and gem01='{unit}' "
                //    + "order by A "
                //    + ") s "
                //    + "where rownum <= 5";
                strsql = "select s.tc_jhl05 c,(SELECT DISTINCT gen02 FROM v_2684_shb "
                    + "             WHERE shb10 = s.shb10 AND shb081 = s.shb081 AND shb09 = s.tc_jhl05 "
                    + "             AND shb03 = s.tc_jhl02 AND shb08 = s.tc_jhl01 and rownum = 1 ) b,s.a "
                    + "from "
                    + "(select t.* , "
                    + "case when round(t.shb111 / tc_jhl06 * 100, 2) >= 100 then 100 else round(t.shb111 / tc_jhl06 * 100, 2) end a "
                    + "from v_2652_shb_jhl2 t "
                    + "where to_char(tc_jhl02, 'yyyy/MM/dd') = to_char(sysdate - 1, 'yyyy/MM/dd') "
                    + $"and tc_jhl06 > 0 and shb10 is not null and tc_jhl01 = 'CA1' and gem01 = '{unit}' "
                    + "order by A "
                    + ") s "
                    + "where rownum <= 5";
                GridView1.Columns[0].Visible = true;
                GridView1.Columns[0].HeaderText = "機台代號</br>Mã số máy";
                GridView1.Columns[1].HeaderText = "操作人員姓名</br>Họ tên thao tác viên";
                GridView1.Columns[2].HeaderText = "計畫達成率</br>Tỷ lệ hoàn thanh kế hoạch";
                lblHead.Text = "日計畫達成率倒數5名</br>Top 5 tỷ lệ hoàn thành kế hoạch thấp nhất trong ngày";
            }
            else if (page_index == 3)
            {
                strsql = "select '' c,s.tc_jhl05 b, s.a  "
                    + "from "
                    + "(select t.* , "
                    + "case when round(t.shb111 / tc_jhl06 * 100, 2) >= 100 then 100 else round(t.shb111 / tc_jhl06 * 100, 2) end a "
                    + "from v_2652_shb_jhl2 t "
                    + "where to_char(tc_jhl02, 'yyyy/MM/dd') = to_char(sysdate - 1, 'yyyy/MM/dd') "
                    + $"and tc_jhl06 > 0 and shb10 is not null and tc_jhl01 = 'CA1' and gem01='{unit}' "
                    + "order by A desc "
                    + ") s "
                    + "where rownum <= 5";
                GridView1.Columns[0].Visible = false;
                GridView1.Columns[1].HeaderText = "機台編號</br>Mã số máy";
                GridView1.Columns[2].HeaderText = "計畫達成率</br>Tỷ lệ hoàn thành kế hoạch";
                lblHead.Text = "日計畫達成率機台前5名</br>Top 5 máy có tỷ lệ hoàn thành kế hoạch cao nhất trong ngày";
            }
            else if (page_index == 4)
            {
                strsql = "select '' c,s.tc_jhl05 b, s.a "
                    + "from "
                    + "(select t.* , "
                    + "case when round(t.shb111 / tc_jhl06 * 100, 2) >= 100 then 100 else round(t.shb111 / tc_jhl06 * 100, 2) end a "
                    + "from v_2652_shb_jhl2 t "
                    + "where to_char(tc_jhl02, 'yyyy/MM/dd') = to_char(sysdate - 1, 'yyyy/MM/dd') "
                    + $"and tc_jhl06 > 0 and shb10 is not null and tc_jhl01 = 'CA1' and gem01='{unit}' "
                    + "order by A "
                    + ") s "
                    + "where rownum <= 5";
                GridView1.Columns[0].Visible = false;
                GridView1.Columns[1].HeaderText = "機台編號</br>Mã số máy";
                GridView1.Columns[2].HeaderText = "計畫達成率</br>Tỷ lệ hoàn thành kế hoạch";
                lblHead.Text = "日計畫達成率機台倒數5名</br>Top 5 máy có tỷ lệ hoàn thành kế hoạch thấp nhất trong ngày";
            }
            else if (page_index == 5)
            {
                //strsql = "select s.b, s.a "
                //    + "from "
                //    + "(select t.* , "
                //    + "(SELECT DISTINCT gen02 FROM v_2684_shb "
                //    + "             WHERE shb10 = t.shb10 AND shb081 = t.shb081 AND shb09 = t.tc_jhl05"
                //    + "             AND shb03 = t.tc_jhl02 AND shb08 = t.tc_jhl01 and rownum = 1) b, "
                //    + "case when round(t.shb111 / tc_jhl06 * 100, 2) >= 100 then 100 else round(t.shb111 / tc_jhl06 * 100, 2) end a "
                //    + "from v_2652_shb_jhl2 t "
                //    + "where to_char(tc_jhl02, 'yyyy/MM/dd') between to_char(sysdate - 8, 'yyyy/MM/dd') and to_char(sysdate - 1, 'yyyy/MM/dd') "
                //    + $"and tc_jhl06 > 0 and shb10 is not null and tc_jhl01 = 'CA1' and gem01='{unit}' "
                //    + "order by A desc "
                //    + ") s "
                //    + "where rownum <= 5";
                strsql = "select s.tc_jhl05 c,(SELECT DISTINCT gen02 FROM v_2684_shb "
                    + "             WHERE shb10 = s.shb10 AND shb081 = s.shb081 AND shb09 = s.tc_jhl05 "
                    + "             AND shb03 = s.tc_jhl02 AND shb08 = s.tc_jhl01 and rownum = 1 ) b,s.a "
                    + "from "
                    + "(select t.* , "
                    + "case when round(t.shb111 / tc_jhl06 * 100, 2) >= 100 then 100 else round(t.shb111 / tc_jhl06 * 100, 2) end a "
                    + "from v_2652_shb_jhl2 t "
                    + "where to_char(tc_jhl02, 'yyyy/MM/dd') between to_char(sysdate - 8, 'yyyy/MM/dd') and to_char(sysdate - 1, 'yyyy/MM/dd') "
                    + $"and tc_jhl06 > 0 and shb10 is not null and tc_jhl01 = 'CA1' and gem01 = '{unit}' "
                    + "order by A desc "
                    + ") s "
                    + "where rownum <= 5";
                GridView1.Columns[0].Visible = true;
                GridView1.Columns[0].HeaderText = "機台代號</br>Mã số máy";
                GridView1.Columns[1].HeaderText = "操作人員姓名</br>Họ tên thao tác viên";
                GridView1.Columns[2].HeaderText = "計畫達成率</br>Tỷ lệ hoàn thành kế hoạch";
                lblHead.Text = "周計畫達成率前5名</br>Top 5 tỷ lệ hoàn thành kế hoạch cao nhất trong tuần";
            }
            else if (page_index == 6)
            {
                //strsql = "select s.b, s.a "
                //    + "from "
                //    + "(select t.* , "
                //    + "(SELECT DISTINCT gen02 FROM v_2684_shb "
                //    + "             WHERE shb10 = t.shb10 AND shb081 = t.shb081 AND shb09 = t.tc_jhl05 "
                //    + "             AND shb03 = t.tc_jhl02 AND shb08 = t.tc_jhl01 and rownum = 1) b, "
                //    + "case when round(t.shb111 / tc_jhl06 * 100, 2) >= 100 then 100 else round(t.shb111 / tc_jhl06 * 100, 2) end a "
                //    + "from v_2652_shb_jhl2 t "
                //    + "where to_char(tc_jhl02, 'yyyy/MM/dd') between to_char(sysdate - 8, 'yyyy/MM/dd') and to_char(sysdate - 1, 'yyyy/MM/dd') "
                //    + $"and tc_jhl06 > 0 and shb10 is not null and tc_jhl01 = 'CA1' and gem01='{unit}' "
                //    + " order by A "
                //    + ") s "
                //    + "where rownum <= 5";
                strsql = "select s.tc_jhl05 c,(SELECT DISTINCT gen02 FROM v_2684_shb "
                    + "             WHERE shb10 = s.shb10 AND shb081 = s.shb081 AND shb09 = s.tc_jhl05 "
                    + "             AND shb03 = s.tc_jhl02 AND shb08 = s.tc_jhl01 and rownum = 1 ) b,s.a "
                    + "from "
                    + "(select t.* , "
                    + "case when round(t.shb111 / tc_jhl06 * 100, 2) >= 100 then 100 else round(t.shb111 / tc_jhl06 * 100, 2) end a "
                    + "from v_2652_shb_jhl2 t "
                    + "where to_char(tc_jhl02, 'yyyy/MM/dd') between to_char(sysdate - 8, 'yyyy/MM/dd') and to_char(sysdate - 1, 'yyyy/MM/dd') "
                    + $"and tc_jhl06 > 0 and shb10 is not null and tc_jhl01 = 'CA1' and gem01 = '{unit}' "
                    + "order by A "
                    + ") s "
                    + "where rownum <= 5";
                GridView1.Columns[0].Visible = true;
                GridView1.Columns[0].HeaderText = "機台代號</br>Mã số máy";
                GridView1.Columns[1].HeaderText = "操作人員姓名</br>Họ tên thao tác viên";
                GridView1.Columns[2].HeaderText = "計畫達成率</br>Tỷ lệ hoàn thành kế hoạch";
                lblHead.Text = "周計畫達成率倒數5名</br>Top 5 tỷ lệ hoàn thành kế hoạch thấp nhất trong tuần";
            }
            else if (page_index == 7)
            {
                strsql = "select '' c,s.oper_name b,s.a "
                    + "from "
                    + "(select oper_no, oper_name, oper_deptno, oper_dept, sum(shb111) as shb111a, sum(shc05) as shb112a, "
                    + "case when sum(shb111) + sum(shc05) = 0 then 0 else round(sum(shc05) / (sum(shb111) + sum(shc05)) * 100, 2) end a "
                    + "  from v_1805_shb_dtl "
                    + $"    WHERE to_char(shb03, 'yyyy/mm/dd') = to_char(sysdate - 1, 'yyyy/MM/dd') and oper_deptno='{unit}' "
                    + "  group by oper_no, oper_name, oper_deptno, oper_dept "
                    + "    order by a desc "
                    + "    )s "
                    + " where rownum <= 5";
                GridView1.Columns[0].Visible = false;
                GridView1.Columns[1].HeaderText = "操作人員姓名</br>Họ tên thao tác viên";
                GridView1.Columns[2].HeaderText = "報廢比例</br>Tỷ lệ báo phế";
                lblHead.Text = "日報廢率最高5名</br>Top 5 tỷ lệ báo phế cao nhất trong ngày";
            }
            else if (page_index == 8)
            {
                strsql = "select '' c,s.oper_name b,s.a "
                    + "from "
                    + "(select oper_no, oper_name, oper_deptno, oper_dept, sum(shb111) as shb111a, sum(shc05) as shb112a, "
                    + "case when sum(shb111) + sum(shc05) = 0 then 0 else round(sum(shc05) / (sum(shb111) + sum(shc05)) * 100, 2) end a "
                    + "  from v_1805_shb_dtl "
                    + $"    WHERE to_char(shb03, 'yyyy/mm/dd') = to_char(sysdate - 1, 'yyyy/MM/dd') and oper_deptno='{unit}' "
                    + "  group by oper_no, oper_name, oper_deptno, oper_dept "
                    + "    order by a "
                    + "    )s "
                    + " where rownum <= 5";
                GridView1.Columns[0].Visible = false;
                GridView1.Columns[1].HeaderText = "操作人員姓名</br>Họ tên thao tác viên";
                GridView1.Columns[2].HeaderText = "報廢比例</br>Tỷ lệ báo phế";
                lblHead.Text = "日報廢率最低5名</br>Top 5 tỷ lệ báo phế thấp nhất trong ngày";
            }
            if (strsql != "")
            {
                data = conn.mysearch(strsql);
                GridView1.DataSource = data;
                GridView1.DataBind();
            }
            Timer1.Enabled = true;
            try { conn.myclose(); }
            catch { return; }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Timer1.Enabled = false;
            if (page_index < 8)
            {
                page_index++;
                query();
            }
            else
            {
                page_index = 1;
                Response.Redirect($"/kanban/kanban_zhizao3.aspx?page=kanban_zhizao&u={unit}");
            }
        }
    }
}