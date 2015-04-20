<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BankInfoTestFrm2.aspx.cs" Inherits="BGU.Web20.MiscItemsSite.Tests.BankInfoTestFrm2" %>
<%@ Register TagPrefix="uc" TagName="BankInfoEditControl2" Src="~/Controls/BankInfoEditControl2.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphTitle" runat="server">BankInfo structure input form proto & testing
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphDivTitle" runat="server">BankInfo structure input form proto & testing
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
<table width="100%">
<tr>
<td width="10%"></td>
<td width="75%">
    <form id="form1" runat="server">
    <uc:BankInfoEditControl2 runat="server" id="bkie" />
    </form>
    </td>
    <td width="15%"></td>
    </tr>
</table>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphFooter" runat="server">
</asp:Content>
