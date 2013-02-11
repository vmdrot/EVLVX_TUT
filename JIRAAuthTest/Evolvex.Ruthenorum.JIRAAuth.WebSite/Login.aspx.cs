using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace Evolvex.Ruthenorum.JIRAAuth.WebSite
{
    public partial class Login : System.Web.UI.Page
    {


        protected void ctrlLogin_Authenticate(object sender, AuthenticateEventArgs e)
        {
            e.Authenticated = Membership.ValidateUser(ctrlLogin.UserName, ctrlLogin.Password);
        }
    }
}
