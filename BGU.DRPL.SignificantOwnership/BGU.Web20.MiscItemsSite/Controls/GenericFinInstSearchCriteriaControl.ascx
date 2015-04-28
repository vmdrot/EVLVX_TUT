<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GenericFinInstSearchCriteriaControl.ascx.cs" Inherits="BGU.Web20.MiscItemsSite.Controls.GenericFinInstSearchCriteriaControl" %>
<%@ Register TagPrefix="uc" TagName="BootstrapTextEditBasic" Src="~/Controls/BootstrapTextEditBasic.ascx" %>
<%@ Register TagPrefix="uc" TagName="RcuKruLookupControl3" Src="~/Controls/RcuKruLookupControl3.ascx" %>
<%@ Register TagPrefix="uc" TagName="BootstrapDropDownBasic" Src="~/Controls/BootstrapDropDownBasic.ascx" %>
<%@ Register TagPrefix="uc" TagName="BootstrapCheckBoxBasic" Src="~/Controls/BootstrapCheckBoxBasic.ascx" %>


<div id="content">
 <uc:RcuKruLookupControl3 ID="uabkcbx" runat="server" AutoPostBack="false" OnSelectedMFOChanged="uabkcbx_OnSelectedMFOChanged" HeadOfficesOnly="true" SkipCategories="1,2,5,6" />
 <uc:BootstrapDropDownBasic ID="ddlCountry" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" LabelDisplayName="Країна" LabelDescription="Країна резидентності банку" />
 <uc:BootstrapTextEditBasic ID="edHeadMFO" runat="server" LabelDisplayName="МФО" LabelDescription="МФО" EditMaxLength="6" EditSize="6" EditWidth="20%" />
 <uc:BootstrapTextEditBasic ID="edRegistryNr" runat="server" LabelDisplayName="№ у реєстрі банків" LabelDescription="№ у реєстрі банків (лише для головних контор)" EditWidth="10%" />
 <uc:BootstrapTextEditBasic ID="edCode" runat="server" LabelDisplayName="Код банку" LabelDescription="Код банку (лише для головних контор)" EditWidth="10%" />
 <uc:BootstrapTextEditBasic ID="edName" runat="server" LabelDisplayName="Найменування банку" LabelDescription="Найменування банку (в оригіналі)" />
 <uc:BootstrapTextEditBasic ID="edNameUkr" runat="server" LabelDisplayName="Найменування банку (українською)" LabelDescription="Найменування банку(українською); заповнюється для банків, де оригінальна назва не українською." />
 <uc:BootstrapTextEditBasic ID="edSWIFTBIC" runat="server" LabelDisplayName="SWIFT адреса" LabelDescription="Див. http://www.swift.com/bsl/" />
</div>
