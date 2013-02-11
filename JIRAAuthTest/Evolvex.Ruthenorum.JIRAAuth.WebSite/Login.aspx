<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Evolvex.Ruthenorum.JIRAAuth.WebSite.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Login ID="ctrlLogin" runat="server" Width="100%" BorderStyle="solid" BorderWidth="0" TitleText=""
            RememberMeSet="true" DisplayRememberMe="true" RememberMeText="Remember me" TextLayout="TextOnTop"
            OnAuthenticate="ctrlLogin_Authenticate">
      </asp:Login>
    </div>
    </form>
</body>
</html>
