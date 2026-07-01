using System.Data;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using System.Web.UI.DataVisualization.Charting;
using System;

namespace CXWeb.schedule
{
    public partial class QA_HoaChat_Chart : System.Web.UI.Page
    {
        private static connectEIP conn;
        private static int intChec = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Load Chart 1
                ddMachine_Id.SelectedIndex = 0;
                LoadCode_Id(ddMachine_Id.SelectedValue.ToString());
                BindchartCR(ddCode_Id.SelectedValue.ToString());

                // Load Chart 2
                ddMachine2.SelectedIndex = 0;
                LoadCode_Id2(ddMachine2.SelectedValue.ToString());
                BindchartChart2(ddCode_Id2.SelectedValue.ToString());

                // Load Chart 3
                ddMachine_Id3.SelectedIndex = 0;
                LoadCode_Id3(ddMachine_Id3.SelectedValue.ToString());
                BindchartChart3(ddCode_Id3.SelectedValue.ToString());

                // Load Chart 4
                ddMachine_Id4.SelectedIndex = 0;
                LoadCode_Id4(ddMachine_Id4.SelectedValue.ToString());
                BindchartChart4(ddCode_Id4.SelectedValue.ToString());
                
            }

        }

        protected void dd_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCode_Id(ddMachine_Id.SelectedValue.ToString());
            BindchartCR(ddCode_Id.SelectedValue.ToString());
        }

        #region Chart1
        private void LoadCode_Id(string strMachine_ID)
        {
            conn = new connectEIP();
            conn.myopen();
            string strQuery = "SELECT Code_Id FROM QA_List WHERE Machine_Id = '" + strMachine_ID + "' ORDER BY Code_Id";
            DataTable dt = new DataTable();

            dt = conn.mysearch(strQuery);
            conn.myclose();

            if (dt.Rows.Count > 0 && dt != null)
            {
                ddCode_Id.DataSource = dt;
                ddCode_Id.DataTextField = "Code_Id";
                ddCode_Id.DataValueField = "Code_Id";
                ddCode_Id.DataBind();
                ddCode_Id.SelectedIndex = 0;
            }


        }
       
        private void BindchartCR(string Code_ID)
        {
            conn = new connectEIP();
            conn.myopen();
            //Code_ID = "1E-HCL1";
            DataSet ChartDataSet = new DataSet();
            string strQuery = "sp_Get_QA_Daily";
            System.Collections.Hashtable htPara = new System.Collections.Hashtable();
            htPara["CODE_ID"] = Code_ID;

            ChartDataSet = conn.ExecuteReturnDs(strQuery, htPara, CommandType.StoredProcedure);

            conn.myclose();

            DataTable dt_list = new DataTable();
            DataTable ChartData = new DataTable();

            ChartData = ChartDataSet.Tables[0];
            dt_list = ChartDataSet.Tables[1];

            string strTitle_X = "Date";
            string strTitle_Y = dt_list.Rows[0]["Code_Id"].ToString();
            string strTitale_Chart = dt_list.Rows[0]["Code_Id"].ToString() + " Chart";
            string strIsMax = "";
            double dbValue_Min = Convert.ToDouble(dt_list.Rows[0]["Value_Min"]);
            double dbValue_Max = Convert.ToDouble(dt_list.Rows[0]["Value_Max"]);
            double dbGia_Tri_Min = Convert.ToDouble(dt_list.Rows[0]["Gia_Tri_Min"]);
            double dbGia_Tri_Max = Convert.ToDouble(dt_list.Rows[0]["Gia_Tri_Max"]);

            for (int count = 0; count < ChartData.Rows.Count; count++)
            {
                
                strIsMax = ChartData.Rows[count]["Is_Max"].ToString();

                Chart_CR.Series["Series_CR"].Points.Add();
                
                if (strIsMax == "N")
                {
                    Chart_CR.Series["Series_CR"].Points[Chart_CR.Series["Series_CR"].Points.Count - 1].Label = (ChartData.Rows[count]["Gia_Tri"].ToString().Substring(0, (ChartData.Rows[count]["Gia_Tri"].ToString().Length - 2)));
                    Chart_CR.Series["Series_CR"].Points[Chart_CR.Series["Series_CR"].Points.Count - 1].LabelBorderColor = System.Drawing.Color.Blue;

                    //Chart_CR.Series["Series_CR"].Points[Chart_CR.Series["Series_CR"].Points.Count - 1].SetValueXY(ChartData.Rows[count]["Ngay"].ToString(), Convert.ToInt32(ChartData.Rows[count]["Gia_Tri"]));
                }
                //Chart_CR.Series["Series1"].Points[Chart_CR.Series["Series1"].Points.Count - 1].a
                Chart_CR.Series["Series_CR"].Points[Chart_CR.Series["Series_CR"].Points.Count - 1].SetValueXY(ChartData.Rows[count]["Ngay"].ToString(), Convert.ToDouble(ChartData.Rows[count]["Gia_Tri"]));
                

            }

            Chart_CR.Series[0].BorderWidth = 3;

            Chart_CR.ChartAreas["ChartArea_CR"].AxisX.Title = strTitle_X;
            Chart_CR.ChartAreas["ChartArea_CR"].AxisY.Title = strTitle_Y;
            Chart_CR.ChartAreas["ChartArea_CR"].AxisX.MajorGrid.Enabled = false;
            Chart_CR.ChartAreas["ChartArea_CR"].AxisY.MajorGrid.Enabled = true;
            Chart_CR.ChartAreas["ChartArea_CR"].AxisX.Interval = 1;
            Chart_CR.ChartAreas["ChartArea_CR"].AxisX.TitleForeColor = System.Drawing.Color.Blue;
            Chart_CR.ChartAreas["ChartArea_CR"].AxisY.TitleForeColor = System.Drawing.Color.Blue;
            Chart_CR.ChartAreas["ChartArea_CR"].AxisY.MajorGrid.LineColor = System.Drawing.Color.Green;

          foreach (DataPoint pt in Chart_CR.Series["Series_CR"].Points)
            {
                if (pt.YValues[0] < dbValue_Min)
                {
                    pt.Color = System.Drawing.Color.Red;
                }
                else if (pt.YValues[0] > dbValue_Max)
                {
                    pt.Color = System.Drawing.Color.Red;
                }
                if (pt.YValues[0] >= 1)
                {
                    pt.MarkerStyle = MarkerStyle.Circle;
                    pt.MarkerSize = 5;
                    pt.MarkerColor = System.Drawing.Color.Transparent;
                    pt.MarkerBorderColor = System.Drawing.Color.Orange;
                    pt.MarkerBorderWidth = 2;
                }
                if (pt.YValues[0] == 0)
                {
                    pt.Color = System.Drawing.Color.Transparent;
                }

            }

            if (dbGia_Tri_Min >= dbValue_Min)
            {
                Chart_CR.ChartAreas["ChartArea_CR"].AxisY.Minimum = Math.Round(dbValue_Min - dbValue_Min * 0.2, 0);
            }
            else
                Chart_CR.ChartAreas["ChartArea_CR"].AxisY.Minimum = dbGia_Tri_Min;

            if (dbGia_Tri_Max <= dbValue_Max)
            {
                Chart_CR.ChartAreas["ChartArea_CR"].AxisY.Maximum = Math.Round(dbValue_Max - dbValue_Max * 0.2, 0);
            }
            else
                Chart_CR.ChartAreas["ChartArea_CR"].AxisY.Maximum = dbGia_Tri_Max;



            //Tô màu
            StripLine stripLine1 = new StripLine();
            stripLine1.StripWidth = 0;

            stripLine1.BorderColor = System.Drawing.Color.Transparent;
            stripLine1.BorderWidth = 0;
            stripLine1.Interval = 0;
            stripLine1.BackColor = System.Drawing.Color.RosyBrown;
            stripLine1.IntervalOffset = dbValue_Min;
            stripLine1.StripWidth = dbValue_Max - dbValue_Min;

            Chart_CR.ChartAreas["ChartArea_CR"].AxisX.LabelStyle.Angle = -90;

            stripLine1.BackSecondaryColor = System.Drawing.Color.Purple;
            stripLine1.BackGradientStyle = GradientStyle.LeftRight;

            Chart_CR.ChartAreas[0].AxisY.StripLines.Add(stripLine1);

        }

        #endregion
        #region Chart2
        private void LoadCode_Id2(string strMachine_ID)
        {
            conn = new connectEIP();
            conn.myopen();
            string strQuery = "SELECT Code_Id FROM QA_List WHERE Machine_Id = '" + strMachine_ID + "' ORDER BY Code_Id";
            DataTable dt = new DataTable();

            dt = conn.mysearch(strQuery);
            conn.myclose();

            if (dt.Rows.Count > 0 && dt != null)
            {
                ddCode_Id2.DataSource = dt;
                ddCode_Id2.DataTextField = "Code_Id";
                ddCode_Id2.DataValueField = "Code_Id";
                ddCode_Id2.DataBind();
                ddCode_Id2.SelectedIndex = 0;
            }
        }
        private void BindchartChart2(string Code_ID)
        {
            conn = new connectEIP();
            conn.myopen();

            DataSet ChartDataSet = new DataSet();
            string strQuery = "sp_Get_QA_Daily";
            System.Collections.Hashtable htPara = new System.Collections.Hashtable();
            htPara["CODE_ID"] = Code_ID;

            ChartDataSet = conn.ExecuteReturnDs(strQuery, htPara, CommandType.StoredProcedure);

            conn.myclose();

            DataTable dt_list = new DataTable();
            DataTable ChartData = new DataTable();

            ChartData = ChartDataSet.Tables[0];
            dt_list = ChartDataSet.Tables[1];

            string strTitle_X = "Date";
            string strTitle_Y = dt_list.Rows[0]["Code_Id"].ToString();
            string strTitale_Chart = dt_list.Rows[0]["Code_Id"].ToString() + " Chart";
            string strIsMax = "";
            double dbValue_Min = Convert.ToDouble(dt_list.Rows[0]["Value_Min"]);
            double dbValue_Max = Convert.ToDouble(dt_list.Rows[0]["Value_Max"]);

            for (int count = 0; count < ChartData.Rows.Count; count++)
            {
                
                strIsMax = ChartData.Rows[count]["Is_Max"].ToString();

                Chart2.Series["Series2"].Points.Add();

                if (strIsMax == "N")
                {
                    Chart2.Series["Series2"].Points[Chart2.Series["Series2"].Points.Count - 1].Label = (ChartData.Rows[count]["Gia_Tri"].ToString().Substring(0, (ChartData.Rows[count]["Gia_Tri"].ToString().Length - 2)));
                    Chart2.Series["Series2"].Points[Chart2.Series["Series2"].Points.Count - 1].LabelBorderColor = System.Drawing.Color.Blue;

                    //Chart2.Series["Series2"].Points[Chart2.Series["Series2"].Points.Count - 1].SetValueXY(ChartData.Rows[count]["Ngay"].ToString(), Convert.ToInt32(ChartData.Rows[count]["Gia_Tri"]));
                }
                //Chart2.Series["Series1"].Points[Chart2.Series["Series1"].Points.Count - 1].a
                Chart2.Series["Series2"].Points[Chart2.Series["Series2"].Points.Count - 1].SetValueXY(ChartData.Rows[count]["Ngay"].ToString(), Convert.ToDouble(ChartData.Rows[count]["Gia_Tri"]));


            }

            Chart2.Series[0].BorderWidth = 3;

            Chart2.ChartAreas["ChartArea2"].AxisX.Title = strTitle_X;
            Chart2.ChartAreas["ChartArea2"].AxisY.Title = strTitle_Y;
            Chart2.ChartAreas["ChartArea2"].AxisX.MajorGrid.Enabled = false;
            Chart2.ChartAreas["ChartArea2"].AxisY.MajorGrid.Enabled = true;
            Chart2.ChartAreas["ChartArea2"].AxisX.Interval = 1;
            Chart2.ChartAreas["ChartArea2"].AxisX.TitleForeColor = System.Drawing.Color.Blue;
            Chart2.ChartAreas["ChartArea2"].AxisY.TitleForeColor = System.Drawing.Color.Blue;
            Chart2.ChartAreas["ChartArea2"].AxisY.MajorGrid.LineColor = System.Drawing.Color.Green;

            foreach (DataPoint pt in Chart2.Series["Series2"].Points)
            {
                if (pt.YValues[0] < dbValue_Min)
                {
                    pt.Color = System.Drawing.Color.Red;
                }
                else if (pt.YValues[0] > dbValue_Max)
                {
                    pt.Color = System.Drawing.Color.Red;
                }
                if (pt.YValues[0] >= 1)
                {
                    pt.MarkerStyle = MarkerStyle.Circle;
                    pt.MarkerSize = 5;
                    pt.MarkerColor = System.Drawing.Color.Transparent;
                    pt.MarkerBorderColor = System.Drawing.Color.Orange;
                    pt.MarkerBorderWidth = 2;
                }

                if (pt.YValues[0] == 0)
                {
                    pt.Color = System.Drawing.Color.Transparent;
                }

            }
            double dbGia_Tri_Min = Convert.ToDouble(dt_list.Rows[0]["Gia_Tri_Min"]);
            double dbGia_Tri_Max = Convert.ToDouble(dt_list.Rows[0]["Gia_Tri_Max"]);

            if (dbGia_Tri_Min >= dbValue_Min)
            {
                Chart2.ChartAreas["ChartArea2"].AxisY.Minimum = Math.Round(dbValue_Min - dbValue_Min * 0.2, 0);
            }
            else
                Chart2.ChartAreas["ChartArea2"].AxisY.Minimum = dbGia_Tri_Min;

            if (dbGia_Tri_Max <= dbValue_Max)
            {
                Chart2.ChartAreas["ChartArea2"].AxisY.Maximum = Math.Round(dbValue_Max - dbValue_Max * 0.2, 0);
            }
            else
                Chart2.ChartAreas["ChartArea2"].AxisY.Maximum = dbGia_Tri_Max;

            //Tô màu
            StripLine stripLine1 = new StripLine();
            stripLine1.StripWidth = 0;

            stripLine1.BorderColor = System.Drawing.Color.Transparent;
            stripLine1.BorderWidth = 0;
            stripLine1.Interval = 0;
            stripLine1.BackColor = System.Drawing.Color.RosyBrown;
            stripLine1.IntervalOffset = dbValue_Min;
            stripLine1.StripWidth = dbValue_Max - dbValue_Min;

            Chart2.ChartAreas["ChartArea2"].AxisX.LabelStyle.Angle = -90;

            stripLine1.BackSecondaryColor = System.Drawing.Color.Purple;
            stripLine1.BackGradientStyle = GradientStyle.LeftRight;

            Chart2.ChartAreas[0].AxisY.StripLines.Add(stripLine1);

        }

        #endregion
        #region Chart3
        private void LoadCode_Id3(string strMachine_ID)
        {
            conn = new connectEIP();
            conn.myopen();
            string strQuery = "SELECT Code_Id FROM QA_List WHERE Machine_Id = '" + strMachine_ID + "' ORDER BY Code_Id";
            DataTable dt = new DataTable();

            dt = conn.mysearch(strQuery);
            conn.myclose();

            if (dt.Rows.Count > 0 && dt != null)
            {
                ddCode_Id3.DataSource = dt;
                ddCode_Id3.DataTextField = "Code_Id";
                ddCode_Id3.DataValueField = "Code_Id";
                ddCode_Id3.DataBind();
                ddCode_Id3.SelectedIndex = 0;
            }
        }
        private void BindchartChart3(string Code_ID)
        {
            conn = new connectEIP();
            conn.myopen();

            DataSet ChartDataSet = new DataSet();
            string strQuery = "sp_Get_QA_Daily";
            System.Collections.Hashtable htPara = new System.Collections.Hashtable();
            htPara["CODE_ID"] = Code_ID;

            ChartDataSet = conn.ExecuteReturnDs(strQuery, htPara, CommandType.StoredProcedure);

            conn.myclose();

            DataTable dt_list = new DataTable();
            DataTable ChartData = new DataTable();

            ChartData = ChartDataSet.Tables[0];
            dt_list = ChartDataSet.Tables[1];

            string strTitle_X = "Date";
            string strTitle_Y = dt_list.Rows[0]["Code_Id"].ToString();
            string strTitale_Chart = dt_list.Rows[0]["Code_Id"].ToString() + " Chart";
            string strIsMax = "";
            double dbValue_Min = Convert.ToDouble(dt_list.Rows[0]["Value_Min"]);
            double dbValue_Max = Convert.ToDouble(dt_list.Rows[0]["Value_Max"]);

            for (int count = 0; count < ChartData.Rows.Count; count++)
            {

                strIsMax = ChartData.Rows[count]["Is_Max"].ToString();

                Chart3.Series["Series3"].Points.Add();

                if (strIsMax == "N")
                {
                    Chart3.Series["Series3"].Points[Chart3.Series["Series3"].Points.Count - 1].Label = (ChartData.Rows[count]["Gia_Tri"].ToString().Substring(0, (ChartData.Rows[count]["Gia_Tri"].ToString().Length - 2)));
                    Chart3.Series["Series3"].Points[Chart3.Series["Series3"].Points.Count - 1].LabelBorderColor = System.Drawing.Color.Blue;

                    //Chart3.Series["Series3"].Points[Chart3.Series["Series3"].Points.Count - 1].SetValueXY(ChartData.Rows[count]["Ngay"].ToString(), Convert.ToInt32(ChartData.Rows[count]["Gia_Tri"]));
                }
                //Chart3.Series["Series1"].Points[Chart3.Series["Series1"].Points.Count - 1].a
                Chart3.Series["Series3"].Points[Chart3.Series["Series3"].Points.Count - 1].SetValueXY(ChartData.Rows[count]["Ngay"].ToString(), Convert.ToDouble(ChartData.Rows[count]["Gia_Tri"]));


            }


            Chart3.Series[0].BorderWidth = 3;

            Chart3.ChartAreas["ChartArea3"].AxisX.Title = strTitle_X;
            Chart3.ChartAreas["ChartArea3"].AxisY.Title = strTitle_Y;
            Chart3.ChartAreas["ChartArea3"].AxisX.MajorGrid.Enabled = false;
            Chart3.ChartAreas["ChartArea3"].AxisY.MajorGrid.Enabled = true;
            Chart3.ChartAreas["ChartArea3"].AxisX.Interval = 1;
            Chart3.ChartAreas["ChartArea3"].AxisX.TitleForeColor = System.Drawing.Color.Blue;
            Chart3.ChartAreas["ChartArea3"].AxisY.TitleForeColor = System.Drawing.Color.Blue;
            Chart3.ChartAreas["ChartArea3"].AxisY.MajorGrid.LineColor = System.Drawing.Color.Green;

            foreach (DataPoint pt in Chart3.Series["Series3"].Points)
            {
                if (pt.YValues[0] < dbValue_Min)
                {
                    pt.Color = System.Drawing.Color.Red;
                }
                else if (pt.YValues[0] > dbValue_Max)
                {
                    pt.Color = System.Drawing.Color.Red;
                }
                if (pt.YValues[0] >= 1)
                {
                    pt.MarkerStyle = MarkerStyle.Circle;
                    pt.MarkerSize = 5;
                    pt.MarkerColor = System.Drawing.Color.Transparent;
                    pt.MarkerBorderColor = System.Drawing.Color.Orange;
                    pt.MarkerBorderWidth = 2;
                }

                if (pt.YValues[0] == 0)
                {
                    pt.Color = System.Drawing.Color.Transparent;
                }

            }
            double dbGia_Tri_Min = Convert.ToDouble(dt_list.Rows[0]["Gia_Tri_Min"]);
            double dbGia_Tri_Max = Convert.ToDouble(dt_list.Rows[0]["Gia_Tri_Max"]);

            if (dbGia_Tri_Min >= dbValue_Min)
            {
                Chart3.ChartAreas["ChartArea3"].AxisY.Minimum = Math.Round(dbValue_Min - dbValue_Min * 0.2, 0);
            }
            else
                Chart3.ChartAreas["ChartArea3"].AxisY.Minimum = dbGia_Tri_Min;

            if (dbGia_Tri_Max <= dbValue_Max)
            {
                Chart3.ChartAreas["ChartArea3"].AxisY.Maximum = Math.Round(dbValue_Max - dbValue_Max * 0.2, 0);
            }
            else
                Chart3.ChartAreas["ChartArea3"].AxisY.Maximum = dbGia_Tri_Max;

            //Tô màu
            StripLine stripLine1 = new StripLine();
            stripLine1.StripWidth = 0;

            stripLine1.BorderColor = System.Drawing.Color.Transparent;
            stripLine1.BorderWidth = 0;
            stripLine1.Interval = 0;
            stripLine1.BackColor = System.Drawing.Color.RosyBrown;
            stripLine1.IntervalOffset = dbValue_Min;
            stripLine1.StripWidth = dbValue_Max - dbValue_Min;

            Chart3.ChartAreas["ChartArea3"].AxisX.LabelStyle.Angle = -90;

            stripLine1.BackSecondaryColor = System.Drawing.Color.Purple;
            stripLine1.BackGradientStyle = GradientStyle.LeftRight;

            Chart3.ChartAreas[0].AxisY.StripLines.Add(stripLine1);

        }

        #endregion
        #region Chart4
        private void LoadCode_Id4(string strMachine_ID)
        {
            conn = new connectEIP();
            conn.myopen();
            string strQuery = "SELECT Code_Id FROM QA_List WHERE Machine_Id = '" + strMachine_ID + "' ORDER BY Code_Id";
            DataTable dt = new DataTable();

            dt = conn.mysearch(strQuery);
            conn.myclose();

            if (dt.Rows.Count > 0 && dt != null)
            {
                ddCode_Id4.DataSource = dt;
                ddCode_Id4.DataTextField = "Code_Id";
                ddCode_Id4.DataValueField = "Code_Id";
                ddCode_Id4.DataBind();
                ddCode_Id4.SelectedIndex = 0;
            }
        }
        private void BindchartChart4(string Code_ID)
        {
            conn = new connectEIP();
            conn.myopen();

            DataSet ChartDataSet = new DataSet();
            string strQuery = "sp_Get_QA_Daily";
            System.Collections.Hashtable htPara = new System.Collections.Hashtable();
            htPara["CODE_ID"] = Code_ID;

            ChartDataSet = conn.ExecuteReturnDs(strQuery, htPara, CommandType.StoredProcedure);

            conn.myclose();

            DataTable dt_list = new DataTable();
            DataTable ChartData = new DataTable();

            ChartData = ChartDataSet.Tables[0];
            dt_list = ChartDataSet.Tables[1];

            string strTitle_X = "Date";
            string strTitle_Y = dt_list.Rows[0]["Code_Id"].ToString();
            string strTitale_Chart = dt_list.Rows[0]["Code_Id"].ToString() + " Chart";
            string strIsMax = "";
            double dbValue_Min = Convert.ToDouble(dt_list.Rows[0]["Value_Min"]);
            double dbValue_Max = Convert.ToDouble(dt_list.Rows[0]["Value_Max"]);

            for (int count = 0; count < ChartData.Rows.Count; count++)
            {

                strIsMax = ChartData.Rows[count]["Is_Max"].ToString();

                Chart4.Series["Series4"].Points.Add();

                if (strIsMax == "N")
                {
                    Chart4.Series["Series4"].Points[Chart4.Series["Series4"].Points.Count - 1].Label = (ChartData.Rows[count]["Gia_Tri"].ToString().Substring(0, (ChartData.Rows[count]["Gia_Tri"].ToString().Length - 2)));
                    Chart4.Series["Series4"].Points[Chart4.Series["Series4"].Points.Count - 1].LabelBorderColor = System.Drawing.Color.Blue;

                    //Chart4.Series["Series4"].Points[Chart4.Series["Series4"].Points.Count - 1].SetValueXY(ChartData.Rows[count]["Ngay"].ToString(), Convert.ToInt32(ChartData.Rows[count]["Gia_Tri"]));
                }
                //Chart4.Series["Series1"].Points[Chart4.Series["Series1"].Points.Count - 1].a
                Chart4.Series["Series4"].Points[Chart4.Series["Series4"].Points.Count - 1].SetValueXY(ChartData.Rows[count]["Ngay"].ToString(), Convert.ToDouble(ChartData.Rows[count]["Gia_Tri"]));


            }

            Chart4.Series[0].BorderWidth = 3;

            Chart4.ChartAreas["ChartArea4"].AxisX.Title = strTitle_X;
            Chart4.ChartAreas["ChartArea4"].AxisY.Title = strTitle_Y;
            Chart4.ChartAreas["ChartArea4"].AxisX.MajorGrid.Enabled = false;
            Chart4.ChartAreas["ChartArea4"].AxisY.MajorGrid.Enabled = true;
            Chart4.ChartAreas["ChartArea4"].AxisX.Interval = 1;
            Chart4.ChartAreas["ChartArea4"].AxisX.TitleForeColor = System.Drawing.Color.Blue;
            Chart4.ChartAreas["ChartArea4"].AxisY.TitleForeColor = System.Drawing.Color.Blue;
            Chart4.ChartAreas["ChartArea4"].AxisY.MajorGrid.LineColor = System.Drawing.Color.Green;

            foreach (DataPoint pt in Chart4.Series["Series4"].Points)
            {
                if (pt.YValues[0] < dbValue_Min)
                {
                    pt.Color = System.Drawing.Color.Red;
                }
                else if (pt.YValues[0] > dbValue_Max)
                {
                    pt.Color = System.Drawing.Color.Red;
                }
                if (pt.YValues[0] >= 1)
                {
                    pt.MarkerStyle = MarkerStyle.Circle;
                    pt.MarkerSize = 5;
                    pt.MarkerColor = System.Drawing.Color.Transparent;
                    pt.MarkerBorderColor = System.Drawing.Color.Orange;
                    pt.MarkerBorderWidth = 2;
                }

                if (pt.YValues[0] == 0)
                {
                    pt.Color = System.Drawing.Color.Transparent;
                }

            }
            double dbGia_Tri_Min = Convert.ToDouble(dt_list.Rows[0]["Gia_Tri_Min"]);
            double dbGia_Tri_Max = Convert.ToDouble(dt_list.Rows[0]["Gia_Tri_Max"]);

            if (dbGia_Tri_Min >= dbValue_Min)
            {
                Chart4.ChartAreas["ChartArea4"].AxisY.Minimum = Math.Round(dbValue_Min - dbValue_Min * 0.2, 0);
            }
            else
                Chart4.ChartAreas["ChartArea4"].AxisY.Minimum = dbGia_Tri_Min;

            if (dbGia_Tri_Max <= dbValue_Max)
            {
                Chart4.ChartAreas["ChartArea4"].AxisY.Maximum = Math.Round(dbValue_Max - dbValue_Max * 0.2, 0);
            }
            else
                Chart4.ChartAreas["ChartArea4"].AxisY.Maximum = dbGia_Tri_Max;
            //Tô màu
            StripLine stripLine1 = new StripLine();
            stripLine1.StripWidth = 0;

            stripLine1.BorderColor = System.Drawing.Color.Transparent;
            stripLine1.BorderWidth = 0;
            stripLine1.Interval = 0;
            stripLine1.BackColor = System.Drawing.Color.RosyBrown;
            stripLine1.IntervalOffset = dbValue_Min;
            stripLine1.StripWidth = dbValue_Max - dbValue_Min;

            Chart4.ChartAreas["ChartArea4"].AxisX.LabelStyle.Angle = -90;

            stripLine1.BackSecondaryColor = System.Drawing.Color.Purple;
            stripLine1.BackGradientStyle = GradientStyle.LeftRight;

            Chart4.ChartAreas[0].AxisY.StripLines.Add(stripLine1);

        }

        #endregion
        protected void ddCode_Id_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindchartCR(ddCode_Id.SelectedValue.ToString());
        }

        protected void ddCode_Id2_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindchartChart2(ddCode_Id2.SelectedValue.ToString());
        }

        protected void ddMachine2_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCode_Id2(ddMachine2.SelectedValue.ToString());
        }

        protected void ddMachine_Id3_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCode_Id3(ddMachine_Id4.SelectedValue.ToString());
        }

        protected void ddCode_Id3_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindchartChart3(ddCode_Id3.SelectedValue.ToString());
        }

        protected void ddMachine_Id4_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCode_Id4(ddMachine_Id4.SelectedValue.ToString());
        }

        protected void ddCode_Id4_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindchartChart4(ddCode_Id4.SelectedValue.ToString());
        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            intChec++;

            if(intChec == 5)
            {
                Timer1 = new Timer();
                Timer1.Enabled = true;
                Timer1.Interval = 10000;
                Timer1_Tick(sender, e);
            }
            // Chart 1
            
            Chart_CR.Series["Series_CR"].Points.Clear();
            Chart_CR.ChartAreas[0].AxisY.StripLines.Clear();
            if (ddCode_Id.SelectedIndex < (ddCode_Id.Items.Count - 1))
            {
                ddCode_Id.SelectedIndex = ddCode_Id.SelectedIndex + 1;
                ddCode_Id_SelectedIndexChanged(sender, e);
            }                
            else
            {
                if (ddMachine_Id.SelectedIndex < (ddMachine_Id.Items.Count - 1))
                {
                    ddMachine_Id.SelectedIndex = ddMachine_Id.SelectedIndex + 1;
                    
                }
                else
                    ddMachine_Id.SelectedIndex = 0;

                dd_SelectedIndexChanged(sender, e);
            }
                


            // Chart 2
            Chart2.Series["Series2"].Points.Clear();
            Chart2.ChartAreas[0].AxisY.StripLines.Clear();
            if (ddCode_Id2.SelectedIndex < (ddCode_Id2.Items.Count - 1))
                ddCode_Id2.SelectedIndex++;
            else
                ddCode_Id2.SelectedIndex = 0;
            ddCode_Id2_SelectedIndexChanged(sender, e);

            // Chart 3
            Chart3.Series["Series3"].Points.Clear();
            Chart3.ChartAreas[0].AxisY.StripLines.Clear();
            if (ddCode_Id3.SelectedIndex < (ddCode_Id3.Items.Count - 1))
                ddCode_Id3.SelectedIndex++;
            else
                ddCode_Id3.SelectedIndex = 0;
            ddCode_Id3_SelectedIndexChanged(sender, e);

            // Chart 4
            Chart4.Series["Series4"].Points.Clear();
            Chart4.ChartAreas[0].AxisY.StripLines.Clear();
            if (ddCode_Id4.SelectedIndex < (ddCode_Id4.Items.Count - 1))
                ddCode_Id4.SelectedIndex++;
            else
                ddCode_Id4.SelectedIndex = 0;
            ddCode_Id4_SelectedIndexChanged(sender, e);
        }
    }
}