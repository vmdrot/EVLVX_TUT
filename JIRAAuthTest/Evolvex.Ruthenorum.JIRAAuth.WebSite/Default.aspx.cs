using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Evolvex.Ruthenorum.JIRAAuth.WebSite
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (System.Threading.Thread.CurrentPrincipal != null && System.Threading.Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                lblUserGreeting.Visible = true;
                lblUserGreeting.Text = string.Format("Hello, {0}!", System.Threading.Thread.CurrentPrincipal.Identity.Name);
            }
        }
    }
}
