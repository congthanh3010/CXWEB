using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Xml;
using System.IO;
using HtmlAgilityPack;

namespace CXWeb.kanban
{
    public partial class healthy_notice : System.Web.UI.Page
    {
        private static string page = "";
        private static string unit = "";

        private static string path = "";
        private List<ImageUrl> img_list = new List<ImageUrl>();

        private static string domain = "https://tuoitre.vn";
        //private static string main_url = "/tim-kiem.htm?keywords=thêm%20ca%20covid-19&zoneId=12";
        //private static string sub_url = "/tim-kiem.htm?keywords=thêm%20ca%20covid-19%20việt%20nam&zoneId=12";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                page = base.Request.QueryString["page"];
                unit = base.Request.QueryString["u"];
                //Image2.ImageUrl = getImage(main_url) != "" ? getImage(main_url) : getImage(sub_url);

                path = Request.PhysicalApplicationPath + "/image/image_list.xml";
                getImageUrl();

                if (img_list.Count == 0)
                {
                    if (page != null)
                    {
                        if (page == "kanban_zhizao2")
                            Response.Redirect("/kanban/kanban_zhizao2.aspx");
                        else
                            Response.Redirect($"/kanban/kanban_zhizao4.aspx?page={page}&u={unit}");
                    }
                }

                Timer1.Interval = img_list.GroupBy(u => u.page).Count() * 15000;
                Timer1.Enabled = true;
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            if (page != null)
            {
                Timer1.Enabled = false;
                if (page == "kanban_zhizao2")
                    Response.Redirect("/kanban/kanban_zhizao2.aspx");
                else
                    Response.Redirect($"/kanban/kanban_zhizao4.aspx?page={page}&u={unit}");
            }
        }

        private string getImage(string url)
        {
            string result_url = "";
            WebClient client = new WebClient();
            string htmlString = "";
            HtmlNode node;

            // Get url of latest news
            htmlString = client.DownloadString(domain + url);
            var doc = new HtmlDocument();
            doc.LoadHtml(htmlString);

            node = doc.DocumentNode.SelectSingleNode("//div[@class='box-news-latest isstream']//ul//li//a");
            if (node != null && node.Attributes.Any(att => att.Name == "href"))
                result_url = node.Attributes["href"].Value;
            else
                return "";

            // Get image url from latest news page
            htmlString = client.DownloadString(domain + result_url);
            doc = new HtmlDocument();
            doc.LoadHtml(htmlString);

            HtmlNodeCollection node_list = doc.DocumentNode.SelectNodes("//div[@id='main-detail-body']//div");
            var filter = node_list.Where(n => n.HasAttributes && (n.Attributes["class"].Value == "VCSortableInPreviewMode"
                                                                  || n.Attributes["class"].Value == "VCSortableInPreviewMode active")
                                           && n.Attributes["type"].Value == "Photo");
            foreach (HtmlNode n in filter)
            {
                node = n.SelectSingleNode("div//img");
                if (node != null && node.Attributes.Any(att => att.Name == "data-original"))
                {
                    if (node.Attributes["data-original"].Value.Contains("cap-nhat-covid-19-viet-nam"))
                        return node.Attributes["data-original"].Value;
                }
            }
            
            return "";
        }

        private void getImageUrl()
        {
            img_list = new List<ImageUrl>();
            XmlDocument xdoc = new XmlDocument();
            if (File.Exists(path))
            {
                xdoc.Load(path);
                if (xdoc.HasChildNodes)
                {
                    foreach (XmlNode node in xdoc.DocumentElement.SelectNodes("ImageUrl"))
                    {
                        img_list.Add(new ImageUrl()
                        {
                            index = int.Parse(node["index"].InnerText),
                            page = int.Parse(node["page"].InnerText),
                            url = node["url"].InnerText
                        });
                    }
                    if (img_list.Count > 0)
                    {
                        var group_list = img_list.GroupBy(u => u.page).ToList();
                        Repeater1.DataSource = group_list;
                        Repeater1.DataBind();
                    }
                }
            }
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                int key = int.Parse((e.Item.FindControl("fieldPage") as HiddenField).Value);
                var result = img_list.FindAll(img => img.page == key);

                Repeater repImage = (Repeater)e.Item.FindControl("Repeater2");
                repImage.DataSource = result;
                repImage.DataBind();
            }
        }
    }
}