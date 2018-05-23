using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdSystem.ClientSideTheme
{
    public partial class Search_Aciton : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var query = Request.QueryString["query"];
            //result.InnerHtml = query;
        }
    }
}