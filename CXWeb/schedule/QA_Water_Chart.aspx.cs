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
using System.Web.UI.DataVisualization.Charting;

namespace CXWeb.schedule
{
    public partial class QA_Water_Chart : System.Web.UI.Page
    {
        private static connectEIP conn;
        private static string filePath;
        private static bool isHasFile = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindchartXiDen();
                BindchartXitreo();
                BindchartXilan();
            }
        }
        private void BindchartXiDen()
        {
            conn = new connectEIP();
            conn.myopen();

            DataTable ChartData = new DataTable();
            string strQuery = "sp_Get_QA_Water";
            System.Collections.Hashtable htPara = new System.Collections.Hashtable();
            htPara["LOAI_XI"] = "XIDEN";

            ChartData = conn.ExecuteReturnDt(strQuery, htPara, CommandType.StoredProcedure);

            conn.myclose();

            //storing total rows count to loop on each Record
            string[] XPointMember = new string[ChartData.Rows.Count];
            int[] YPointMember = new int[ChartData.Rows.Count];
            string strIsMax = "";
            for (int count = 0; count < ChartData.Rows.Count; count++)
            {
                //storing Values for X axis
                // XPointMember[count] = ChartData.Rows[count]["Ngay"].ToString();

                //storing values for Y Axis
                //   YPointMember[count] = Convert.ToInt32(ChartData.Rows[count]["XiDen_Time1"]);
                strIsMax = ChartData.Rows[count]["Is_Max"].ToString();

                Chart1.Series["Series_XiDen"].Points.Add();
                Chart1.Series["s2"].Points.Add();
                if (strIsMax == "N")
                {
                    Chart1.Series["Series_XiDen"].Points[Chart1.Series["Series_XiDen"].Points.Count - 1].Label = (ChartData.Rows[count]["Gia_Tri"].ToString().Substring(0, (ChartData.Rows[count]["Gia_Tri"].ToString().Length - 3)));
                    Chart1.Series["Series_XiDen"].Points[Chart1.Series["Series_XiDen"].Points.Count - 1].LabelBorderColor = System.Drawing.Color.Blue;
                    
                    //Chart1.Series["Series_XiDen"].Points[Chart1.Series["Series_XiDen"].Points.Count - 1].SetValueXY(ChartData.Rows[count]["Ngay"].ToString(), Convert.ToInt32(ChartData.Rows[count]["Gia_Tri"]));
                }
                //Chart1.Series["Series1"].Points[Chart1.Series["Series1"].Points.Count - 1].a
                Chart1.Series["Series_XiDen"].Points[Chart1.Series["Series_XiDen"].Points.Count - 1].SetValueXY(ChartData.Rows[count]["Ngay"].ToString(), Convert.ToInt32(ChartData.Rows[count]["Gia_Tri"]));
                Chart1.Series["s2"].Points[Chart1.Series["s2"].Points.Count - 1].SetValueXY(ChartData.Rows[count]["Ngay"].ToString(), 10);
                
            }
            //binding chart control
           // Chart1.Series[0].Points.DataBindXY(XPointMember, YPointMember);

            //Setting width of line
            Chart1.Series[0].BorderWidth = 5;

            Chart1.ChartAreas["ChartArea_XiDen"].AxisX.MajorGrid.Enabled = false;
            Chart1.ChartAreas["ChartArea_XiDen"].AxisY.MajorGrid.Enabled = true;
            Chart1.ChartAreas["ChartArea_XiDen"].AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;
            Chart1.ChartAreas["ChartArea_XiDen"].AxisX.Interval = 1;
            Chart1.ChartAreas["ChartArea_XiDen"].AxisX.TitleForeColor = System.Drawing.Color.Blue;
            Chart1.ChartAreas["ChartArea_XiDen"].AxisY.TitleForeColor = System.Drawing.Color.Blue;
            Chart1.ChartAreas["ChartArea_XiDen"].AxisY.MajorGrid.LineColor = System.Drawing.Color.Green;


            foreach (DataPoint pt in Chart1.Series["Series_XiDen"].Points)
            {
                if (pt.YValues[0] < 10)
                {
                    pt.Color = System.Drawing.Color.Red;
                }
                else if (pt.YValues[0] >= 10)
                {
                    pt.Color = System.Drawing.Color.ForestGreen;
                }
                if (pt.YValues[0] >= 1)
                {
                    pt.MarkerStyle = MarkerStyle.Circle;
                    pt.MarkerSize = 5;
                    pt.MarkerColor = System.Drawing.Color.Transparent;
                    pt.MarkerBorderColor = System.Drawing.Color.Orange;
                    pt.MarkerBorderWidth = 2;
                }
                    
            }
            // set Y axix range
            //Chart1.ChartAreas["ChartArea_XiDen"].AxisY.Minimum = 15;
            //Chart1.ChartAreas["ChartArea_XiDen"].AxisY.Maximum = 40;
            //Chart1.ChartAreas["ChartArea_XiDen"].AxisY.Minimum = 0;
            //Chart1.ChartAreas["ChartArea_XiDen"].AxisY.Maximum = 30;

            StripLine stripLine1 = new StripLine();
            stripLine1.StripWidth = 0;
            
            stripLine1.BorderColor = System.Drawing.Color.Transparent;
            stripLine1.BorderWidth = 3;
            stripLine1.Interval = 3;
            stripLine1.BackColor = System.Drawing.Color.RosyBrown;
            stripLine1.IntervalOffset = 20;
            stripLine1.StripWidth = 10;
            Chart1.ChartAreas["ChartArea_XiDen"].AxisX.LabelStyle.Angle = -90;

            stripLine1.BackSecondaryColor = System.Drawing.Color.Purple;
            stripLine1.BackGradientStyle = GradientStyle.LeftRight;

            Chart1.ChartAreas[0].AxisY.StripLines.Add(stripLine1);
            
            //Chart1.ChartAreas[0].AxisY.StripLines.
        }
        private void BindchartXitreo()
        {
            conn = new connectEIP();
            conn.myopen();

            DataTable ChartData = new DataTable();
            string strQuery = "sp_Get_QA_Water";
            System.Collections.Hashtable htPara = new System.Collections.Hashtable();
            htPara["LOAI_XI"] = "XITREO";

            ChartData = conn.ExecuteReturnDt(strQuery, htPara, CommandType.StoredProcedure);

            conn.myclose();

            //storing total rows count to loop on each Record
            string[] XPointMember = new string[ChartData.Rows.Count];
            int[] YPointMember = new int[ChartData.Rows.Count];
            string strIsMax = "";
            for (int count = 0; count < ChartData.Rows.Count; count++)
            {
                
                strIsMax = ChartData.Rows[count]["Is_Max"].ToString();

                Chart_XiTreo.Series["Series_XiTreo"].Points.Add();

                if (strIsMax == "N")
                {
                    Chart_XiTreo.Series["Series_XiTreo"].Points[Chart_XiTreo.Series["Series_XiTreo"].Points.Count - 1].Label = (ChartData.Rows[count]["Gia_Tri"].ToString().Substring(0, (ChartData.Rows[count]["Gia_Tri"].ToString().Length - 3)));
                    //Chart_XiTreo.Series["Series_XiTreo"].Points[Chart_XiTreo.Series["Series_XiTreo"].Points.Count - 1].LabelBorderColor = System.Drawing.Color.Blue;
                }
                //Chart_XiTreo.Series["Series1"].Points[Chart_XiTreo.Series["Series1"].Points.Count - 1].a
                Chart_XiTreo.Series["Series_XiTreo"].Points[Chart_XiTreo.Series["Series_XiTreo"].Points.Count - 1].SetValueXY(ChartData.Rows[count]["Ngay"].ToString(), Convert.ToInt32(ChartData.Rows[count]["Gia_Tri"]));
            }


            //Setting width of line
            Chart_XiTreo.Series[0].BorderWidth = 5;

            Chart_XiTreo.ChartAreas["ChartArea_XiTreo"].AxisX.MajorGrid.Enabled = false;
            Chart_XiTreo.ChartAreas["ChartArea_XiTreo"].AxisY.MajorGrid.Enabled = false;

            Chart_XiTreo.ChartAreas["ChartArea_XiTreo"].AxisX.Interval = 5;
           

        }

        private void BindchartXilan()
        {
            conn = new connectEIP();
            conn.myopen();

            DataTable ChartData = new DataTable();
            string strQuery = "sp_Get_QA_Water";
            System.Collections.Hashtable htPara = new System.Collections.Hashtable();
            htPara["LOAI_XI"] = "XILAN";

            ChartData = conn.ExecuteReturnDt(strQuery, htPara, CommandType.StoredProcedure);

            conn.myclose();

            //storing total rows count to loop on each Record
            string[] XPointMember = new string[ChartData.Rows.Count];
            int[] YPointMember = new int[ChartData.Rows.Count];
            string strIsMax = "";
            for (int count = 0; count < ChartData.Rows.Count; count++)
            {

                strIsMax = ChartData.Rows[count]["Is_Max"].ToString();

                Chart_XiLan.Series["Series_XiLan"].Points.Add();

                if (strIsMax == "N")
                {
                    Chart_XiLan.Series["Series_XiLan"].Points[Chart_XiLan.Series["Series_XiLan"].Points.Count - 1].Label = (ChartData.Rows[count]["Gia_Tri"].ToString().Substring(0, (ChartData.Rows[count]["Gia_Tri"].ToString().Length - 3)));
                    //Chart_XiLan.Series["Series_XiLan"].Points[Chart_XiLan.Series["Series_XiLan"].Points.Count - 1].LabelBorderColor = System.Drawing.Color.Blue;
                }
                //Chart_XiLan.Series["Series1"].Points[Chart_XiLan.Series["Series1"].Points.Count - 1].a
                Chart_XiLan.Series["Series_XiLan"].Points[Chart_XiLan.Series["Series_XiLan"].Points.Count - 1].SetValueXY(ChartData.Rows[count]["Ngay"].ToString(), Convert.ToInt32(ChartData.Rows[count]["Gia_Tri"]));
            }


            //Setting width of line
            Chart_XiLan.Series[0].BorderWidth = 5;

            Chart_XiLan.ChartAreas["ChartArea_XiLan"].AxisX.MajorGrid.Enabled = false;
            Chart_XiLan.ChartAreas["ChartArea_XiLan"].AxisY.MajorGrid.Enabled = false;

            Chart_XiLan.ChartAreas["ChartArea_XiLan"].AxisX.Interval = 1;

        }

    }
}