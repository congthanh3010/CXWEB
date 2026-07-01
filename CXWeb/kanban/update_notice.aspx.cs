using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using System.IO;

namespace CXWeb.kanban
{
    public class ImageUrl
    {
        public int index { get; set; }
        public int page { get; set; }
        public string url { get; set; }
    }

    public partial class update_notice : System.Web.UI.Page
    {
        private static string path = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            path = Request.PhysicalApplicationPath + "/image/image_list.xml";
            if (!IsPostBack)
            {
                getData();
            }
        }

        private void getData()
        {
            btnSave.Enabled = false;
            XmlDocument docx = new XmlDocument();
            if (File.Exists(path))
            {
                docx.Load(path);
                if (docx.HasChildNodes)
                {
                    List<ImageUrl> lst = new List<ImageUrl>();
                    foreach (XmlNode node in docx.DocumentElement.SelectNodes("ImageUrl"))
                    {
                        lst.Add(new ImageUrl()
                        {
                            index = int.Parse(node["index"].InnerText),
                            page = int.Parse(node["page"].InnerText),
                            url = node["url"].InnerText
                        });
                    }
                    Repeater1.Dispose();
                    ListView1.Dispose();
                    Repeater1.DataSource = lst;
                    ListView1.DataSource = lst;
                    Repeater1.DataBind();
                    ListView1.DataBind();
                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            addRow();
            btnSave.Enabled = true;
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            int row_index = int.Parse(((sender as Button).NamingContainer.FindControl("fieldIndex") as HiddenField).Value);
            removeRow(row_index);
            btnSave.Enabled = true;
        }

        private void addRow()
        {
            List<ImageUrl> url_list = new List<ImageUrl>();
            foreach (RepeaterItem item in Repeater1.Items)
            {
                url_list.Add(new ImageUrl()
                {
                    index = int.Parse((item.FindControl("fieldIndex") as HiddenField).Value),
                    page = int.Parse((item.FindControl("txtPage") as TextBox).Text),
                    url = (item.FindControl("txtImageUrl") as TextBox).Text
                });
            }
            url_list.Add(new ImageUrl()
            {
                index = url_list.Count > 0 ? url_list.Max(i => i.index) + 1 : 0,
                page = 1
            });
            Repeater1.Dispose();
            ListView1.Dispose();
            Repeater1.DataSource = url_list;
            ListView1.DataSource = url_list;
            Repeater1.DataBind();
            ListView1.DataBind();
        }

        private void removeRow(int row_index)
        {
            List<ImageUrl> url_list = new List<ImageUrl>();
            foreach (RepeaterItem item in Repeater1.Items)
            {
                int tmp = int.Parse((item.FindControl("fieldIndex") as HiddenField).Value);
                if (tmp != row_index)
                {
                    url_list.Add(new ImageUrl()
                    {
                        index = int.Parse((item.FindControl("fieldIndex") as HiddenField).Value),
                        page = int.Parse((item.FindControl("txtPage") as TextBox).Text),
                        url = (item.FindControl("txtImageUrl") as TextBox).Text
                    });
                }
            }
            if (url_list.Count == 0)
            {
                url_list.Add(new ImageUrl()
                {
                    index = url_list.Count > 0 ? url_list.Max(i => i.index) + 1 : 0,
                    page = 1
                });
            }
            Repeater1.Dispose();
            ListView1.Dispose();
            Repeater1.DataSource = url_list;
            ListView1.DataSource = url_list;
            Repeater1.DataBind();
            ListView1.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<ImageUrl> url_list = new List<ImageUrl>();
            foreach (RepeaterItem item in Repeater1.Items)
            {
                string url = (item.FindControl("txtImageUrl") as TextBox).Text;
                if (url != "")
                {
                    url_list.Add(new ImageUrl()
                    {
                        index = int.Parse((item.FindControl("fieldIndex") as HiddenField).Value),
                        page = int.Parse((item.FindControl("txtPage") as TextBox).Text),
                        url = (item.FindControl("txtImageUrl") as TextBox).Text
                    });
                }
            }
            StreamWriter sw = new StreamWriter(Request.PhysicalApplicationPath + "/image/image_list.xml");
            var writer = new System.Xml.Serialization.XmlSerializer(typeof(List<ImageUrl>));
            writer.Serialize(sw, url_list);
            sw.Close();
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "alert(\"Đã lưu!\");", true);
            btnSave.Enabled = false;
        }
    }
}