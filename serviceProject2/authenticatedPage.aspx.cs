using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace serviceProject2
{
    public partial class authenticatedPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sessionValid"] != null)
            {
                String IsValid = Session["sessionValid"].ToString();

                if (IsValid != "1")
                {
                    //Response.Redirect("authenticatedPage.aspx");
                    Response.Redirect("login.aspx");
                }
            }

            else
            {
                Response.Redirect("login.aspx");
            }

        }

        protected void logoutBtn_Click(object sender, EventArgs e)
        {
            Session["sessionValid"] = null;

            String AssertionID = Session["AssertionID"].ToString();

            String ResponseID = Session["ResponseID"].ToString();

            String RequestType = "Logout";

            String returnURL = HttpContext.Current.Request.Url.AbsoluteUri;
            String URI = WebConfigurationManager.AppSettings["IdentityProvider"].ToString();

            Response.Clear();

            StringBuilder sb = new StringBuilder();
            sb.Append("<html>");
            sb.AppendFormat(@"<body onload='document.forms[""form""].submit()'>");
            sb.AppendFormat("<form name='form' action='{0}' method='post'>", URI);

            sb.AppendFormat("<input type='hidden' name='ResponseID' value='{0}'>", ResponseID);
            sb.AppendFormat("<input type='hidden' name='returnURL' value='{0}'>", returnURL);
            sb.AppendFormat("<input type='hidden' name='RequestType' value='{0}'>", RequestType);

            sb.Append("</form>");
            sb.Append("</body>");
            sb.Append("</html>");

            Response.Write(sb.ToString());

            Response.End();
        }
    }
}