using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdSystem.ClientSideTheme
{
    public partial class Search_Aciton : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.Sleep(5000);
            var query = Request.QueryString["query"];
            //result.InnerHtml = query;
            mydiv.InnerHtml = "Sample Page";
        }

        protected void Unnamed1_Click(object sender, EventArgs e)
        {
            Response.Write("Button Clicked");
        }
    }
}