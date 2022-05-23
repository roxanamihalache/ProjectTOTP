<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebPage.aspx.cs" Inherits="TotpMRM.WebPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID ="sp" runat ="server"> </asp:ScriptManager>
        <asp:Timer ID ="timerTest" runat ="server" Interval ="1000" OnTick ="timerTest_tick"> </asp:Timer>
        <asp:UpdatePanel ID="up" runat ="server" UpdateMode ="Conditional">
        <ContentTemplate>
            <asp:literal ID ="totpMsg" runat ="server"> </asp:literal>
            <br />
            <br />
            <asp:literal ID ="litMsg" runat ="server"> </asp:literal>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID ="timerTest" EventName ="tick"/>
        </Triggers>
        </asp:UpdatePanel>
        <div>
        </div>
    </form>
</body>
</html>
