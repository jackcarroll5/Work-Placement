<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Demo.aspx.cs" Inherits="DemoApp.Demo" %>
<%@ Register Src="~/Apu34Control.ascx" TagName="WebControl" TagPrefix="TWebControl"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
          <TWebControl:WebControl ID="Header" runat="server" MinValue="100"/>
        <div>

            <%Response.Write("Hello World!"); %>
            <br />
            <br />
            <asp:Label ID="lblName" runat="server" Text="Name"></asp:Label>
&nbsp;
            <asp:TextBox ID="txtName" runat="server" Width="186px"></asp:TextBox>
            <br />
            <br />
            <br />
            <br />
            <br />
          
        </div>
        <asp:ListBox ID="lstLocation" runat="server">
            <asp:ListItem>Mumbai</asp:ListItem>
            <asp:ListItem>Bangalore</asp:ListItem>
            <asp:ListItem>Hyderabad</asp:ListItem>
        </asp:ListBox>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:RadioButton ID="radMale" runat="server" Text="Male" />
        <br />
        <br />
        <asp:RadioButton ID="radFemale" runat="server" Text="Female" />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:CheckBox ID="chkC" runat="server" Text="C#" />
        <br />
        <br />
        <asp:CheckBox ID="chkASP" runat="server" Text="ASP.Net" />
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
        <br />
        <br />
    </form>
</body>
</html>
