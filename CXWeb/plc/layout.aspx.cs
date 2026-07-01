using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.IO;
using System.Data;

namespace CXWeb.plc
{
    public partial class layout : System.Web.UI.Page
    {
        protected static connectDB conn;
        protected static DataTable structure;
        protected static DataTable state;
        protected static string[] color = { "white", "green", "yellow", "red" };
        protected static string max_width = "800";

        protected void Page_Load(object sender, EventArgs e)
        {
            conn = new connectDB();
            if (!IsPostBack)
                loadDiagram();
        }

        protected void loadDiagram()
        {
            loadData();
            loadMachineState();
            loadStructure();
            Timer1.Enabled = true;
        }

        private void loadData()
        {
            var strDep = Dep.SelectedItem.Text.Split('-');
            string depId = strDep[0].Trim();
            string depNameVN = "";
            string depNameCN = "";
            if (strDep.Length > 1)
            {
                depNameVN = strDep[2].Trim();
                depNameCN = strDep[1].Trim();
            }
            title.Text = depNameCN + "機器佈局 - Sơ đồ bố trí máy móc của " + depNameVN;
            conn.myopen();
            string strQuery = "SELECT * FROM PLC_LayoutStructure WHERE dep='" + depId + "'";
            structure = new DataTable();
            structure = conn.mysearch(strQuery);
            conn.myclose();
        }

        private void loadStructure()
        {
            var diagram = structure.Select().FirstOrDefault(x => x["stype"].ToString() == "diagram");
            brackets.ShapesCollection.Clear();
            brackets.ConnectionsCollection.Clear();
            if (diagram != null)
            {
                brackets.Width = new Unit(Convert.ToDouble(diagram["width"]), UnitType.Pixel);
                brackets.Height = new Unit(Convert.ToDouble(diagram["height"]), UnitType.Pixel);
                max_width = diagram["width"].ToString();

                structure.Select().Where(x => x["stype"].ToString().Substring(0, 5) == "shape").ToList().ForEach(shape => addShapes(shape));
                structure.Select().Where(x => x["stype"].ToString() == "connection").ToList().ForEach(connect => connectShapes(connect));
                structure.Select().Where(x => x["stype"].ToString().Contains("machine")).ToList().ForEach(machine => addMachine(machine));
            }
            if (Dep.SelectedIndex == 0)
                brackets.Zoom = 0.254;
            else
                brackets.Zoom = 1;
        }

        private void addShapes(DataRow row)
        {
            var shape = createShape(row);
            brackets.ShapesCollection.Add(shape);
        }

        private DiagramShape createShape(DataRow row)
        {
            var shape = new DiagramShape()
            {
                Id = row["id"].ToString(),
                Editable = false,
                MinWidth = Convert.ToDouble(row["width"]),
                Width = Convert.ToDouble(row["width"]),
                MinHeight = Convert.ToDouble(row["height"]),
                Height = Convert.ToDouble(row["height"]),
                X = Convert.ToDouble(row["x"]),
                Y = Convert.ToDouble(row["y"]),
                Type = row["stype"].ToString().Split('-')[1],
                Fill = row["color"].ToString()
            };
            shape.StrokeSettings.Color = "black";
            shape.StrokeSettings.DashType = Telerik.Web.UI.Diagram.StrokeDashType.Solid;
            shape.StrokeSettings.Width = 0.99;
            shape.ContentSettings.Text = row["scontent"].ToString();
            shape.ContentSettings.Color = "black";
            if (!row.IsNull("strokeWidth") || row["strokeWidth"].ToString().Trim() != "")
                shape.ContentSettings.FontSize = Convert.ToDouble(row["strokeWidth"]);
            shape.RotationSettings.Angle = Convert.ToDouble(row["angel"]);
            return shape;
        }

        private void connectShapes(DataRow row)
        {
            var connection = createConnection(row);
            brackets.ConnectionsCollection.Add(connection);
        }

        private DiagramConnection createConnection(DataRow row)
        {
            var connection = new DiagramConnection();
            connection.Id = row["id"].ToString();
            connection.StrokeSettings.DashType = row["scontent"].ToString().Trim() == "" ? Telerik.Web.UI.Diagram.StrokeDashType.Solid : (Telerik.Web.UI.Diagram.StrokeDashType)(Convert.ToInt16(row["scontent"]));
            connection.StrokeSettings.Color = row["color"].ToString();
            connection.StrokeSettings.Width = Convert.ToDouble(row["strokeWidth"]);

            connection.FromConnector = row["fromConnect"].ToString();
            connection.FromSettings.ShapeId = row["fromId"].ToString();
            connection.ToConnector = row["toConnect"].ToString();
            connection.ToSettings.ShapeId = row["toId"].ToString();
            var lstPoint = structure.Select().Where(x => x["stype"].ToString() == "point" && x["objectId"].ToString() == row["id"].ToString()).ToList();
            foreach (var p in lstPoint)
            {
                connection.PointsCollection.Add(new DiagramConnectionPoint() { X = Convert.ToDouble(p["x"]), Y = Convert.ToDouble(p["y"]) });
            }
            return connection;
        }

        private void addMachine(DataRow row)
        {
            var machine = createMachine(row);
            brackets.ShapesCollection.Add(machine);
        }

        private DiagramShape createMachine(DataRow row)
        {
            var shape = new DiagramShape()
            {
                Id = row["id"].ToString(),
                Editable = false,
                Type = row["stype"].ToString().Split('-').Length > 1 ? row["stype"].ToString().Split('-')[1] : "rectangle",
                MinWidth = Convert.ToDouble(row["width"]),
                Width = Convert.ToDouble(row["width"]),
                MinHeight = Convert.ToDouble(row["height"]),
                Height = Convert.ToDouble(row["height"]),
                X = Convert.ToDouble(row["x"]),
                Y = Convert.ToDouble(row["y"])
            };
            var mState = state.Select().FirstOrDefault(x => x["MachineId"].ToString() == row["objectId"].ToString());
            var index = mState == null || mState.IsNull("State") ? 0 : Convert.ToInt16(mState["State"]);
            shape.FillSettings.Color = color[index];
            shape.RotationSettings.Angle = Convert.ToDouble(row["angel"]);
            shape.ContentSettings.Text = row["scontent"].ToString();
            shape.ContentSettings.Color = row["color"].ToString();
            shape.ContentSettings.FontSize = Convert.ToDouble(row["strokeWidth"]);
            shape.ContentSettings.FontWeight = "bold";
            shape.StrokeSettings.DashType = Telerik.Web.UI.Diagram.StrokeDashType.Solid;
            shape.StrokeSettings.Color = "black";
            shape.StrokeSettings.Width = 0.99;
            return shape;
        }

        private void loadMachineState()
        {
            var arrDep = Dep.SelectedValue.Split(';');
            var depId = string.Join(",", arrDep.Select(i => string.Format("'{0}'", i)));
            string strQuery = "SELECT pml.MachineId, pmax.TTime, pv.[State]\n"
                            + "FROM PLC_MachineList pml\n"
                            + "LEFT JOIN (\n"
                            + "	SELECT MACHINE_CODE, MAX(TTime) TTime\n"
                            + "	FROM PLC_VL\n"
                            + "	WHERE TTime BETWEEN DATEADD(mi,-3,GETDATE()) AND GETDATE()\n"
                            + "	GROUP BY MACHINE_CODE\n"
                            + ") pmax ON pmax.MACHINE_CODE=pml.MachineId\n"
                            + "LEFT JOIN PLC_VL pv ON pv.MACHINE_CODE = pmax.MACHINE_CODE AND pv.TTime = pmax.TTime\n";
            if (depId.Trim() != "''")
                strQuery += "WHERE pml.DepId IN (" + depId + ")";
            conn.myopen();
            state = new DataTable();
            state = conn.mysearch(strQuery);
            conn.myclose();
        }

        protected void Ajax_Request(object sender, AjaxRequestEventArgs e)
        {
            //var json = File.Exists(MapPath("~/App_Data/diagram-CT1.json")) ? File.ReadAllText(MapPath("~/App_Data/diagram-CT1.json")) : null;
            //if (json != null)
            //    ajaxManager.ResponseScripts.Add(string.Format("loadFromServer({0})", json));
        }

        protected void Dep_SelectedIndexChanged(object sender, EventArgs e)
        {
            Timer1.Enabled = false;
            loadDiagram();
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            loadDiagram();
        }
    }
}