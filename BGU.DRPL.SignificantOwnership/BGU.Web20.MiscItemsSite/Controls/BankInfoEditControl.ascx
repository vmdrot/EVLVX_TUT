<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BankInfoEditControl.ascx.cs" Inherits="BGU.Web20.MiscItemsSite.Controls.BankInfoEditControl" %>
<%@ Register TagPrefix="uc" TagName="BootstrapTextEditBasic" Src="~/Controls/BootstrapTextEditBasic.ascx" %>
<%@ Register TagPrefix="uc" TagName="RcuKruLookupControl" Src="~/Controls/RcuKruLookupControl.ascx" %>

<div id="content">
    <ul id="tabs" class="nav nav-tabs" data-tabs="tabs">
        <li class="active"><a href="#red" data-toggle="tab">Вибрати</a></li>
        <li><a href="#orange" data-toggle="tab">Увести</a></li>
    </ul>
    <div id="my-tab-content" class="tab-content">
        <div class="tab-pane active" id="red">
          <div class="panel panel-primary">
            <div class="panel-heading">
              <h3 class="panel-title">Обрати банк з довідника</h3>
            </div>
            <div class="panel-body">
            <uc:RcuKruLookupControl ID="uabkcbx" runat="server" />
            </div>
          </div>
        </div>
        <div class="tab-pane" id="orange">
          <div class="panel panel-success">
            <div class="panel-heading">
              <h3 class="panel-title">Увести банк (не існує в довіднику)</h3>
            </div>
            <div class="panel-body">
                <uc:BootstrapTextEditBasic ID="edHeadMFO" runat="server" LabelDisplayName="МФО" LabelDescription="МФО" EditMaxLength="6" EditSize="6" EditWidth="20%" />
                <uc:BootstrapTextEditBasic ID="edRegistryNr" runat="server" LabelDisplayName="№ у реєстрі банків" LabelDescription="№ у реєстрі банків (лише для головних контор)" EditWidth="10%" />
                <uc:BootstrapTextEditBasic ID="edCode" runat="server" LabelDisplayName="Код банку" LabelDescription="Код банку (лише для головних контор)" EditWidth="10%" />
                <uc:BootstrapTextEditBasic ID="edName" runat="server" LabelDisplayName="Найменування банку" LabelDescription="Найменування банку (в оригіналі)" />
                <uc:BootstrapTextEditBasic ID="edNameUkr" runat="server" LabelDisplayName="Найменування банку (українською)" LabelDescription="Найменування банку(українською); заповнюється для банків, де оригінальна назва не українською." />
                <uc:BootstrapTextEditBasic ID="edSWIFTBIC" runat="server" LabelDisplayName="SWIFT адреса" LabelDescription="Див. http://www.swift.com/bsl/" />
            </div>
          </div>
        </div>
    </div>
</div>


<script type="text/javascript">
    jQuery(document).ready(function ($) {
        $('#tabs').tab();
    });
</script>    



