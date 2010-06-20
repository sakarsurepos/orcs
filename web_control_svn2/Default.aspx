<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Robot._Default" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Remote Robot Control - Sjf TU Kosice</title>
    <style type="text/css">
        .style24
        {
            width: 273px;
            height: 197px;
        }
        .style25
        {
            height: 137px;
        }
        .style1
        {
            font-size: large;
        }
        .style51
        {
            font-size: medium;
            color: #003366;
        }
        .style55
        {
            width: 273px;
            height: 12px;
        }
        #form1
        {
            width: 695px;
            height: 855px;
        }
        .textnormal
        {
            font-size: small;
        }
        .textnormalbold
        {
            font-size: small;
            font-weight: bold;
        }
        .paneltitle
        {
            font-weight: bold;
        }
        .style68
        {
            width: 202px;
            height: 146px;
        }
        .style11
        {
            font-weight: bold;
        }
        .style73
        {
            font-size: small;
        }
        .style74
        {
            height: 146px;
        }
        .style76
        {
            height: 36px;
        }
        .style81
        {
            height: 1px;
        }
        .style83
        {
            height: 21px;
        }
        .style84
        {
            width: 368px;
        }
        .style85
        {
            height: 19px;
        }
        .style87
        {
            font-size: small;
            font-weight: bold;
        }
        </style>
<script type="text/JavaScript">
<!--
//function MyCallbackHandler(Result)
//{
//alert(Result); //Thanks
//}

window.onbeforeunload = function () //.onbeforeunload .onresize
{
    PageMethods.Clear_Garbage(); //MyCallbackHandler
    alert("Thank You for Remote Robot Control Testing");
}
function IBrowserSupported()
{
  if (Sys.Browser.agent == Sys.Browser.InternetExplorer || Sys.Browser.agent == Sys.Browser.Firefox)
   {                                
   return true;
   }
  else
   {
   return false;
   }
 }                            
 window.onload = function ()
 {
 if (IBrowserSupported())
  {
  PageMethods.BrowserCheck(); //connection to robot avaliable
  }
 else
  {
  alert('Internet Explorer and Firefox are supported only, You cannot control robot, Application is working as Cam Spectator only');
  }
 }
-->
</script>
</head>
<body>
<form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True" 
            EnableScriptGlobalization="True">
        </asp:ScriptManager>
        <asp:XmlDataSource ID="XmlDataCountries" runat="server" 
            DataFile="~/Resources/countries.xml"></asp:XmlDataSource>
        <asp:Timer ID="Timer1" runat="server" Interval="1000" ontick="Timer1_Tick" 
            Enabled="False">
        </asp:Timer>
    <table style="width: 697px; height: 455px; margin-right: 0px;" border="0">
        <tr align="left">
            <td class="style76" colspan="2">
                <asp:Panel ID="Panel18" runat="server" BorderWidth="1px" ForeColor="#003366">
                    <table style="width:100%;">
                        <tr>
                            <td class="style84">
                                <asp:Panel ID="Panel13" runat="server" Height="62px" Width="365px">
                                    <table cellpadding="0" cellspacing="0" style="width:100%; height: 45px;">
                                        <tr>
                                            <td class="style83">
                                                <span class="textnormal">
                                                <asp:UpdatePanel ID="UpdatePanel53" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <span class="textnormal"><span class="textnormal"></b><span 
                                                            class="textnormal"><span class="textnormal"><span class="textnormal">&nbsp;</span></span><span 
                                                            class="textnormal"><b>Robot Connection:</b></span></span><span 
                                                            class="textnormal">&nbsp;
                                                        <asp:Label ID="Label12" runat="server" ForeColor="#006600" 
                                                            Text="Please enter your Name and select Country "></asp:Label>
                                                        </span></span>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style85">
                                                <span class="style73">&nbsp;<b>Name:</b></span>&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:TextBox ID="TextBox17" runat="server" Font-Size="X-Small" Width="78px"></asp:TextBox>
                                                &nbsp;<span class="style87">Country:</span>
                                                <asp:DropDownList ID="DropDownList8" runat="server" 
                                                    DataSourceID="XmlDataCountries" DataTextField="country" 
                                                    DataValueField="country" Font-Size="X-Small" Width="162px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span class="style73">&nbsp;<b>Robot IP:</b></span>&nbsp;<asp:DropDownList ID="DropDownList1" 
                                                    runat="server" Font-Size="X-Small">
                                                    <asp:ListItem Value="147.232.20.70">Robot IP</asp:ListItem>
                                                    <asp:ListItem Value="147.232.20.241">Old Robot IP</asp:ListItem>
                                                </asp:DropDownList>
                                                &nbsp;<span class="style73">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Port:</b></span>
                                                <asp:DropDownList ID="DropDownList2" runat="server" Font-Size="X-Small">
                                                    <asp:ListItem>2000</asp:ListItem>
                                                    <asp:ListItem>1470</asp:ListItem>
                                                </asp:DropDownList>
                                                &nbsp; 
                                                <asp:Button ID="Button18" runat="server" Font-Size="7pt" Height="16px" 
                                                    onclick="Button18_Click" Text="CheckIP" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table style="width:100%; height: 56px;" border="0" 
                        cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td class="style83">
                                                    <b class="textnormal">&nbsp;<b> Status:&nbsp;&nbsp;
                                                    <asp:Label ID="Label1" runat="server" style="text-align: right" 
                                                        Text="Disconnected"></asp:Label>
                                                    &nbsp; Your IP is:
                                                    <asp:Label ID="Label3" runat="server" Text="unknown"></asp:Label>
                                                    </b></b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style83">
                                                    &nbsp;&nbsp;<asp:Button ID="Button1" runat="server" Height="19px" 
                                    onclick="Button10_Click" Text="Connect" Width="61px" Font-Size="X-Small" />
                                                    &nbsp;<asp:Button ID="Button2" runat="server" Enabled="False" Height="19px" 
                                    onclick="Button2_Click" Text="Disconnect" Width="78px" Font-Size="X-Small" />
                                                    &nbsp;<b><span class="style73">Timeout:</span></b>
                                                    <asp:Label ID="Label2" runat="server" Text="2 minutes limit"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style81">
                                                    <b>&nbsp;<b class="textnormal"> <b class="textnormal"><b class="textnormal">
                                                    <asp:Button ID="Button9" runat="server" Font-Size="X-Small" Height="19px" 
                                                        onclick="Button9_Click" Text="Check robot status" Width="99px" />
                                                    &nbsp;</b>Robot State:</b>&nbsp;&nbsp;<asp:Label 
                                                        ID="Label4" runat="server" Font-Size="Small" Text="unknown status"></asp:Label>
                                                    </b></b>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr align="left">
            <td class="style25">
                <asp:Panel ID="Panel17" runat="server" BorderWidth="1px" ForeColor="#003366">
                    <asp:UpdatePanel ID="UpdatePanel51" runat="server" 
    UpdateMode="Conditional">
                        <ContentTemplate>
                            <table style="width:41%;">
                                <tr>
                                    <td class="style68">
                                        <asp:Panel ID="Panel16" runat="server" BorderWidth="1px" ForeColor="#003366" 
                                        Width="198px">
                                            <applet align="middle" code="xplug.class" codebase="http://147.232.20.242:80/" 
                                            hspace="0" name="cvcs1" style="height: 143px; width: 198px" vspace="0">
                                                <param name="RemotePort" value="80" />
                                                <param name="Timeout" value="5000" />
                                                <param name="RotateAngle" value="0" />
                                                <param name="PreviewFrameRate" value="2" />
                                                <param name="DeviceSerialNo" value="" />
                                            </applet></asp:Panel>
                                    </td>
                                    <td class="style74">
                                        <asp:Panel ID="Panel15" runat="server" 
                                        Height="142px" Width="71px">
                                            <b>&nbsp;Camera:</b><br />
                                            <br />
                                            &nbsp;
                                            <asp:Button ID="Button13" runat="server" Text="Connect" Width="55px" 
                                                onclick="Button13_Click" Font-Size="6pt" />
                                            <br />
                                            <br />
                                            <asp:Button ID="Button14" runat="server" onclick="Button14_Click" 
                                                Text="Disconnect" Width="70px" Font-Size="6pt" />
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Button13" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </asp:Panel>
            </td>
            <td align="left" rowspan="3" valign="top">
            <cc1:TabContainer ID="TabContainer3" runat="server" ActiveTabIndex="2" 
                Height="391px" Width="400px"><cc1:TabPanel ID="SpectatorsCam" runat="server" HeaderText="HiRes Cam"><HeaderTemplate>
                HiRes Cam</HeaderTemplate><ContentTemplate><asp:UpdatePanel ID="UpdatePanel52" runat="server" UpdateMode="Conditional"><ContentTemplate><table 
                        style="width:100%;"><tr><td align="center" valign="middle"><br /><asp:Button ID="Button16" runat="server" Height="44px" Text="Connect" />&#160;&#160; <asp:Button ID="Button17" runat="server" Height="44px" Text="Disconnect" /><br /><br /><asp:Panel 
                            ID="Panel9" runat="server" BorderWidth="1px" ForeColor="#999999" Height="240px" 
                            Width="320px"><applet align="middle" code="xplug.class" 
                            codebase="http://10.1.10.222:80/" hspace="0" name="cvcs0" 
                            style="height: 240px; width: 320px" vspace="0"><param name="RemotePort" 
                                value="80" /><param name="Timeout" value="5000" /><param name="RotateAngle" 
                                value="0" /><param name="PreviewFrameRate" value="2" /><param 
                                name="DeviceSerialNo" value="" />
                        </applet></asp:Panel></td></tr></table></ContentTemplate><Triggers><asp:AsyncPostBackTrigger ControlID="Button13" EventName="Click" /></Triggers></asp:UpdatePanel></ContentTemplate></cc1:TabPanel><cc1:TabPanel ID="Maps" runat="server" HeaderText="Maps"><HeaderTemplate>
                
                
                Maps</HeaderTemplate>
                
<ContentTemplate><asp:UpdatePanel ID="UpdatePanel8" runat="server" 
                            UpdateMode="Conditional"><ContentTemplate><table 
            style="width:100%;"><tr><td><b>Maps: <asp:DropDownList ID="mapd" runat="server" 
                AutoPostBack="True" ontextchanged="DropDownList9_TextChanged"><asp:ListItem>TUKE</asp:ListItem><asp:ListItem>Kosice</asp:ListItem><asp:ListItem 
                Value="TUKE Zoom">TUKE Zoom</asp:ListItem></asp:DropDownList></b></td></tr><tr><td align="left">&#160;&#160;<b>x: <asp:Label ID="Label13" runat="server" Text="00"></asp:Label>&#160;y: <asp:Label ID="Label14" runat="server" Text="00"></asp:Label>&#160;Long: <asp:Label ID="longm" runat="server" Text="00,0000000"></asp:Label>&#160;Latt: <asp:Label ID="lattm" runat="server" Text="00,0000000"></asp:Label></b></td></tr><tr><td 
                align="center" valign="middle"><b><asp:ImageButton ID="ImageButton1" 
                runat="server" Height="320px" ImageUrl="~/Resources/mapa_kosice.bmp" 
                onclick="ImageButton1_Click" Width="320px" /></b></td></tr></table></ContentTemplate><Triggers><asp:AsyncPostBackTrigger ControlID="mapd" EventName="TextChanged" /><asp:AsyncPostBackTrigger ControlID="ImageButton1" EventName="Click" /></Triggers></asp:UpdatePanel></ContentTemplate></cc1:TabPanel><cc1:TabPanel ID="Kinematics" runat="server" HeaderText="Kinematics"><HeaderTemplate>
                Kinematics</HeaderTemplate><ContentTemplate><asp:UpdatePanel ID="UpdatePanel9" runat="server" 
                            UpdateMode="Conditional"><ContentTemplate><b>&#160;Kinematics:<br /><br />&#160;</b><asp:Image 
                                ID="Image1" runat="server" Height="287px" Width="370px" /></ContentTemplate></asp:UpdatePanel></ContentTemplate></cc1:TabPanel></cc1:TabContainer>
            </td>
        </tr>
        <tr>
            <td class="style24">
    <cc1:TabContainer ID="Settings" runat="server" ActiveTabIndex="0" 
        Height="188px" Width="280px" Font-Size="Medium">
        <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Control"><ContentTemplate><asp:UpdatePanel ID="UpdatePanel3" runat="server" 
                    UpdateMode="Conditional"><ContentTemplate><b class="paneltitle">Robot control panel:</b><br /><br /><table 
                        style="width:75%; height: 138px;"><tr><td ><asp:Panel 
                            ID="Panel1" runat="server" BorderWidth="1px" ForeColor="#003366" 
                                    Height="142px" HorizontalAlign="Center" style="font-size: xx-small" 
                                    Width="133px"><span class="textnormal">Robot:</span><br /><br />&#160;<asp:Button 
                        ID="Button6" runat="server" Enabled="False" Height="22px" 
                                        onclick="Button6_Click" Text="Forward" Width="75px" 
                        Font-Size="8pt" /><br /><br /><asp:Button 
                        ID="Button4" runat="server" Enabled="False" Height="22px" 
                                        onclick="Button4_Click" Text="Left" Width="40px" 
                        Font-Size="8pt" />&#160;<asp:Button 
                        ID="Button8" runat="server" Enabled="False" Height="22px" 
                                        onclick="Button8_Click" Text="Stop  " Width="35px" 
                        Font-Size="8pt" />&#160;<asp:Button 
                        ID="Button5" runat="server" Enabled="False" Height="22px" 
                                        onclick="Button5_Click" Text="Right " Width="40px" 
                        Font-Size="8pt" /><br /><br /><asp:Button 
                        ID="Button7" runat="server" Enabled="False" Height="22px" 
                                        onclick="Button7_Click" Text="Backward" Width="75px" 
                        Font-Size="8pt" /><br /></asp:Panel></td><td class="style71">&nbsp;</td><td><asp:Panel 
                        ID="Panel2" runat="server" BorderWidth="1px" ForeColor="#003366" 
                            Height="143px" style="font-size: xx-small" Width="99px"><span class="textnormal">&nbsp; Parameters:</span><br />&#160;<br /><span class="textnormalbold">&#160; Speed: </span><asp:TextBox 
                        ID="ECommand_speed" runat="server" Width="21px">40</asp:TextBox>&#160; &#160;<br /><span class="textnormalbold">&#160;&#160;Angle:&#160; </span><asp:TextBox 
                        ID="ECommand_angle" runat="server" Width="21px">60</asp:TextBox>&#160; &#160;<br /><span class="textnormalbold">&#160;&#160;Time:&#160;&#160; </span><asp:TextBox ID="ECommand_time" 
                                runat="server" Width="28px">200</asp:TextBox>&#160; &#160;</asp:Panel></td></tr></table><br /></ContentTemplate><Triggers><asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" /><asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click" /></Triggers></asp:UpdatePanel>
        
        
        </ContentTemplate></cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="Devices"><ContentTemplate><asp:UpdatePanel 
                ID="UpdatePanel4" runat="server" UpdateMode="Conditional"><ContentTemplate><b class="paneltitle">&nbsp;&nbsp;&nbsp;&nbsp;Advanced Settings:<br /></b><br /><table 
                    style="width: 78%;"><tr><td 
                    class="style75">&#160;</td><td class="style85"><asp:Panel ID="Panel3" 
                            runat="server" BorderWidth="1px" ForeColor="#003366" Height="102px" 
                            Width="160px"><table style="width:99%;"><tr><td class="style84"><b>&#160;Camera:</b></td><td 
                                class="style26"><asp:CheckBox ID="CheckBox1" runat="server" Checked="True" 
                                oncheckedchanged="CheckBox1_CheckedChanged" style="font-weight: 700" Text=" " /></td></tr><tr><td 
                                    class="style84"><b><b>&#160;Direction Servo:</b> </b></td><td><b><asp:CheckBox 
                                        ID="CheckBox2" runat="server" Checked="True" 
                                        oncheckedchanged="CheckBox2_CheckedChanged" Text=" " /></b></td></tr><tr><td 
                                    class="style84"><b>&#160;Camera Servos:</b></td><td><b><asp:CheckBox ID="CheckBox3" 
                                        runat="server" Checked="True" oncheckedchanged="CheckBox3_CheckedChanged" 
                                        Text=" " /></b></td></tr><tr><td class="style84"><b><b>&#160;GPS:</b> </b></td><td><b><asp:CheckBox 
                                    ID="CheckBox4" runat="server" Checked="True" 
                                    oncheckedchanged="CheckBox4_CheckedChanged" Text=" " /></b></td></tr></table><br /><b>&#160;</b><br /><b>&#160;</b>&#160; <br /><b>&#160;</b></asp:Panel></td><td 
                        align="right" valign="top">&#160;<img src="Resources/kcmpci.png" /></td></tr></table><br /></ContentTemplate></asp:UpdatePanel>
        
        
        </ContentTemplate></cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel5" runat="server" HeaderText="GPS">
            <HeaderTemplate>
        GPS</HeaderTemplate>
            

<ContentTemplate><asp:UpdatePanel 
                ID="UpdatePanel5" runat="server" UpdateMode="Conditional"><ContentTemplate><b class="paneltitle">&#160;GPS &#160;Parameters:</b><table 
                        style="width: 63%;"><tr><td colspan="2" align="right">&nbsp;</td><td align="center" colspan="2">&#160;&#160; <asp:Label ID="GPS_Time" runat="server" Text="0:00"></asp:Label></td></tr><tr><td class="style83" 
                        ><asp:Panel ID="Panel12" runat="server" BorderWidth="1px" 
            ForeColor="#003366" Height="133px" Width="164px"><table style="width:99%;"><tr><td 
                    class="style86"><b>&#160;Longtitude:</b></td><td><asp:Label 
                        ID="GPS_Position_Longitude" runat="server" Text="0"></asp:Label></td></tr><tr><td 
                        class="style86"><b>&#160;Lattitude:</b></td><td><asp:Label 
                            ID="GPS_Position_Latitude" runat="server" Text="0"></asp:Label></td></tr><tr><td 
                        class="style86"><b>&#160;Course:</b></td><td><asp:Label ID="Course_Ovr_Ground" 
                            runat="server" Text="0"></asp:Label></td></tr><tr><td class="style86"><b>&#160;Speed:</b></td><td><asp:Label 
                        ID="GPS_Position_Speed" runat="server" Text="0"></asp:Label></td></tr><tr><td 
                        class="style86"><b>&#160;Status:</b></td><td><asp:Label ID="GPS_PositionStatus" 
                            runat="server" Text="0"></asp:Label></td></tr><tr><td class="style86">&#160;<b>Datum:</b></td><td><asp:Label 
                        ID="GPS_Position_Date" runat="server" Text="0"></asp:Label></td></tr></table><br /><b>&#160;</b><br /><b></b><br /><b>&#160;</b><br /><b>&#160;</b></asp:Panel></td><td 
            colspan="2" >&#160;</td><td align="right" valign="top"><img src="Resources/Globe1.png"></img></td></tr></table></ContentTemplate><Triggers><asp:AsyncPostBackTrigger 
        ControlID="Timer1" EventName="Tick" /></Triggers></asp:UpdatePanel>
        </ContentTemplate></cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel6" runat="server" HeaderText="Camera">
            <HeaderTemplate>
        Camera</HeaderTemplate>
            

<ContentTemplate><asp:UpdatePanel 
                ID="UpdatePanel6" runat="server" UpdateMode="Conditional"><ContentTemplate><table 
            style="width: 77%; height: 157px;"><tr><td colspan="2" valign="top"><span 
                class="paneltitle">Camera:&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <table 
                style="width:70%;"><tr><td class="style79"><span class="style11" lang="en-us" 
                        style="font-family: Arial, Helvetica, sans-serif"><span lang="en-us" 
                        style="font-family: Arial, Helvetica, sans-serif"><span lang="en-us" 
                        style="font-family: Arial, Helvetica, sans-serif"><b><span lang="en-us" 
                        style="font-family: Arial, Helvetica, sans-serif"><span class="style1"><cc1:SliderExtender 
                        ID="E3" runat="server" BehaviorID="SliderExtender3s" BoundControlID="Label3s" 
                        Length="170" Maximum="126" Minimum="1" TargetControlID="TextBox3s"></cc1:SliderExtender></span></span></b></span></span></span></td><td><span 
                            class="style11" lang="en-us" style="font-family: Arial, Helvetica, sans-serif"><span 
                            lang="en-us" style="font-family: Arial, Helvetica, sans-serif"><span 
                            lang="en-us" style="font-family: Arial, Helvetica, sans-serif"><b><span 
                            lang="en-us" style="font-family: Arial, Helvetica, sans-serif"><span 
                            class="style1"><cc1:SliderExtender ID="E2" runat="server" 
                            BoundControlID="Label2s" Length="105" Maximum="126" Minimum="1" 
                            Orientation="Vertical" TargetControlID="TextBox2s"></cc1:SliderExtender></span></span></b></span></span></span></td></tr></table>&#160;</span></td></tr><tr><td 
                valign="top" class="style81" rowspan="2"><asp:Panel ID="Panel5" 
                runat="server" BorderWidth="1px" ForeColor="#003366" Height="142px" 
                Width="58px"><table align="left" cellspacing="0" 
                    style="width: 97%; height: 143px;"><tr><td align="center" class="textnormalbold" 
                            colspan="2">Up</td></tr><tr><td><b>&#160;<span class="textnormal">C<br />&#160;a<br />&#160;m<br />&#160;e<br />&#160;r<br />&#160;a</span></b></td><td 
                            align="left" width="27"><asp:TextBox ID="TextBox2s" runat="server" 
                            Height="105px" ontextchanged="TextBox2s_TextChanged" Width="24px">63</asp:TextBox></td></tr><tr><td 
                            align="center" colspan="2"><b><span class="textnormalbold">Down</span></b></td></tr></table></asp:Panel></td><td 
                valign="top"><span class="style11" lang="en-us" 
                    style="font-family: Arial, Helvetica, sans-serif"><span lang="en-us" 
                    style="font-family: Arial, Helvetica, sans-serif"><table cellpadding="0" 
                    cellspacing="0" style="width:76%; height: 88px; margin-top: 3px;"><tr><td 
                            align="left" class="style80" rowspan="2" valign="top"><span class="style11" 
                            lang="en-us" style="font-family: Arial, Helvetica, sans-serif"><span 
                            lang="en-us" style="font-family: Arial, Helvetica, sans-serif"><asp:Panel 
                            ID="Panel14" runat="server" BorderWidth="1px" ForeColor="#003366" Height="84px" 
                            Width="106px">&#160;<span class="style11" lang="en-us" 
                                style="font-family: Arial, Helvetica, sans-serif"><span class="style10" 
                                lang="en-us" style="font-family: Arial, Helvetica, sans-serif"><span 
                                class="style11" lang="en-us" style="font-family: Arial, Helvetica, sans-serif"><span 
                                lang="en-us" style="font-family: Arial, Helvetica, sans-serif"><b><span 
                                lang="en-us" style="font-family: Arial, Helvetica, sans-serif"><span>Digital Zoom:<br /></span><span 
                                class="style1">&#160;<input onclick="cvcs.Zoom(1)" 
                                style="height: 19px; width: 22px" type="button" value="x1" /><input 
                                onclick="cvcs.Zoom(2)" style="height: 19px; width: 21px" type="button" 
                                value="x2" /><input onclick="cvcs.Zoom(3)" 
                                style="height: 19px; width: 23px" type="button" value="x3" /><input 
                                onclick="cvcs.Zoom(4)" style="height: 19px; width: 23px" type="button" 
                                value="x4" /><span class="style11" lang="en-us" 
                                style="font-family: Arial, Helvetica, sans-serif"><span lang="en-us" 
                                style="font-family: Arial, Helvetica, sans-serif"><b><span class="style1"><asp:Panel 
                                ID="Panel7" runat="server" BorderWidth="0px" Height="46px" 
                                style="font-size: 5pt" Width="95px"><table style="width: 100%; height: 45px;"><tr><td 
                                        class="style73"><span class="style11" lang="en-us" 
                                        style="font-family: Arial, Helvetica, sans-serif"><span lang="en-us" 
                                        style="font-family: Arial, Helvetica, sans-serif"><b><span class="style1"><span 
                                        class="textnormalbold"><span style="font-size: small">&#160;&#160; Vert: </span><asp:Label 
                                        ID="Label2s" runat="server" Text="63"></asp:Label><br /></span><span><span 
                                        class="textnormalbold">&#160;&#160; Hor:&#160; <asp:Label ID="Label3s" runat="server" 
                                        Text="63"></asp:Label></span></span></span></b></span></span></td></tr></table></asp:Panel></span></b></span></span></span></span></b></span></span></span></span></asp:Panel></span></span></td><td><img 
                                src="Resources/webcam.png" /></td></tr></table></span></span></td></tr><tr><td><b><asp:Panel 
                ID="Panel6" runat="server" BorderWidth="1px" ForeColor="#003366" Height="46px" 
                Width="175px"><table style="width: 175px; height: 35px;"><tr><td>Left</td><td><b 
                    class="textnormalbold">&#160;&#160; Camera</b></td><td align="right"><b><span 
                        class="textnormalbold">Right</span></b><br /></td></tr><tr><td 
                        class="style1" colspan="3"><asp:TextBox ID="TextBox3s" runat="server" 
                        ontextchanged="TextBox3s_TextChanged" Width="170px">63</asp:TextBox></td></tr></table></asp:Panel></b></td></tr></table></ContentTemplate></asp:UpdatePanel></ContentTemplate></cc1:TabPanel>
        <cc1:TabPanel ID="New" runat="server" HeaderText="P1">
            <HeaderTemplate>
        P1</HeaderTemplate>
            

<ContentTemplate><asp:UpdatePanel ID="UpdatePanel50" runat="server" 
        UpdateMode="Conditional"><ContentTemplate><b style="font-size: x-small">&#160;</b><b><span 
            class="paneltitle">Easy Robot programming Interface:</span><span><br /></span></b><b style="font-size: x-small"><br /><table style="width:49%;"><tr><td >&#160;</td><td><b><asp:Panel ID="Panel10" runat="server" BorderWidth="1px" ForeColor="#003366"><table cellspacing="0" style="width: 91%; height: 140px;"><tr><td >&nbsp;n.</td><td align="center" class="style77" >command</td><td 
                        >&nbsp;speed</td><td >&nbsp;angle</td><td align="center" class="style77" >time</td></tr><tr><td>&#160;1</td><td class="style77"><asp:DropDownList ID="DropDownList3" runat="server" Height="22px" Width="78px"><asp:ListItem>nothing</asp:ListItem><asp:ListItem>forward</asp:ListItem><asp:ListItem>backward</asp:ListItem><asp:ListItem>left</asp:ListItem><asp:ListItem>right</asp:ListItem></asp:DropDownList></td><td><b style="font-size: x-small"><asp:TextBox ID="TextBox18" runat="server" Width="21px">0</asp:TextBox></b></td><td><b style="font-size: x-small"><asp:TextBox ID="TextBox19" runat="server" Width="21px">0</asp:TextBox></b></td><td><asp:TextBox ID="TextBox4" runat="server" Width="28px">0</asp:TextBox></td></tr><tr><td>&nbsp;2</td><td 
                        ><asp:DropDownList ID="DropDownList5" runat="server" 
                        Height="22px" Width="78px"><asp:ListItem>nothing</asp:ListItem><asp:ListItem>forward</asp:ListItem><asp:ListItem>backward</asp:ListItem><asp:ListItem>left</asp:ListItem><asp:ListItem>right</asp:ListItem></asp:DropDownList></td><td ><asp:TextBox ID="TextBox5" runat="server" Width="21px">0</asp:TextBox></td><td ><asp:TextBox ID="TextBox6" 
                            runat="server" Width="21px">0</asp:TextBox></td><td ><asp:TextBox ID="TextBox7" runat="server" Width="28px">0</asp:TextBox></td></tr><tr><td>&#160;3</td><td><asp:DropDownList 
                        ID="DropDownList4" runat="server" Height="22px" Width="78px"><asp:ListItem>nothing</asp:ListItem><asp:ListItem>forward</asp:ListItem><asp:ListItem>backward</asp:ListItem><asp:ListItem>left</asp:ListItem><asp:ListItem>right</asp:ListItem></asp:DropDownList></td><td><asp:TextBox ID="TextBox8" runat="server" Width="21px">0</asp:TextBox></td><td><asp:TextBox ID="TextBox9" runat="server" Width="21px">0</asp:TextBox></td><td><asp:TextBox 
                        ID="TextBox10" runat="server" Width="28px">0</asp:TextBox></td></tr><tr><td>&#160;4</td><td><asp:DropDownList 
                        ID="DropDownList7" runat="server" Height="22px" Width="78px"><asp:ListItem>nothing</asp:ListItem><asp:ListItem>forward</asp:ListItem><asp:ListItem>backward</asp:ListItem><asp:ListItem>left</asp:ListItem><asp:ListItem>right</asp:ListItem></asp:DropDownList></td><td><asp:TextBox ID="TextBox11" runat="server" Width="21px">0</asp:TextBox></td><td><asp:TextBox ID="TextBox12" runat="server" Width="21px">0</asp:TextBox></td><td><asp:TextBox 
                        ID="TextBox13" runat="server" Width="28px">0</asp:TextBox></td></tr><tr><td class="style77">&nbsp;5</td><td><asp:DropDownList 
                        ID="DropDownList6" runat="server" Height="22px" Width="78px"><asp:ListItem>nothing</asp:ListItem><asp:ListItem>forward</asp:ListItem><asp:ListItem>backward</asp:ListItem><asp:ListItem>left</asp:ListItem><asp:ListItem>right</asp:ListItem></asp:DropDownList></td><td><asp:TextBox ID="TextBox14" runat="server" Width="21px">0</asp:TextBox></td><td class="style77"><asp:TextBox ID="TextBox15" runat="server" Width="21px">0</asp:TextBox></td><td><b style="font-size: x-small"><asp:TextBox ID="TextBox20" runat="server" Width="28px">0</asp:TextBox></b></td></tr></table></asp:Panel></b></td><td><b style="font-size: x-small"><asp:Panel 
                ID="Panel11" runat="server" BorderWidth="1px" ForeColor="#003366" 
                    Height="144px"><table style="width:100%;"><tr><td class="style78" style="font-size: x-small"><br /><asp:Button 
                    ID="Button10" runat="server" Height="22px" Text="&gt;" 
                                    Width="20px" Font-Size="X-Small" /><br /><br /></td></tr><tr><td class="style78" style="font-size: x-small"><asp:Button 
                        ID="Button11" runat="server" Height="21px" Text="1" 
                                    Width="20px" Font-Size="X-Small" /><br /><br /></td></tr><tr><td class="style78"><asp:Button 
                        ID="Button12" runat="server" Height="21px" Text="2" 
                                    Width="21px" Font-Size="X-Small" /></td></tr></table></asp:Panel></b></td></tr></table></b></ContentTemplate></asp:UpdatePanel>
        </ContentTemplate>
        
</cc1:TabPanel>
                </cc1:TabContainer>
                <br />
    <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="Panel8" runat="server" BorderWidth="1px" 
    ForeColor="#999999" Width="278px">
                <b><span class="style51">&nbsp;<img 
                    src="Resources/energy.png" align="bottom" border="0" 
                    style="height: 15px; width: 18px;" vspace="0" /> Battery status:<span class="style1">
                <asp:Label ID="Label10" runat="server" style="font-size: medium" Text="50%"></asp:Label>
                &nbsp;</span></span><span class="style1">&nbsp;</span><span class="style51">Voltage:</span><span class="style1">
                <asp:TextBox ID="TextBox1" runat="server" Font-Size="9pt" Height="16px" 
                    Width="28px"></asp:TextBox>
                </span></b>
            </asp:Panel>
            &nbsp;
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="TextBox1" EventName="TextChanged" />
        </Triggers>
                
    </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="style55">
                </td>
        </tr>
    </table>
        <table style="width:100%; height: 3px;">
            <tr>
                <td align="center" rowspan="4">
                    &nbsp;</td>
                            <td align="center">
                     <span class="style11" lang="en-us">Top 10 / Best Robot Operators Table Table</span>
                    <asp:UpdatePanel ID="UpdatePanel54" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                    <asp:DataGrid ID="DataGrid1" runat="server" 
    BackColor="White" CellPadding="3" 
                        CellSpacing="1" EnableViewState="False" 
    ForeColor="Black" GridLines="None" 
                        Width="425px" Font-Size="Small">
                                            <HeaderStyle backcolor="#4A3C8C" font-bold="True" forecolor="White" />
                                            <ItemStyle backcolor="#DEDFDE" />
                                        </asp:DataGrid>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="Button15" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                    </td>
                        </tr>
            <tr>
                            <td align="center">
                                <asp:Button ID="Button15" runat="server" Text="Refresh Table" />
                            &nbsp;<asp:Button ID="Button19" runat="server" onclick="Button19_Click" 
                                    Text="MS SQL DB insert" />
                            </td>
                        </tr>
            <tr>
                            <td align="center">
                    <b>Technical University of Kosice, KBIAaM, 2010.</b></td>
            </tr>
        </table>
    </form>
        </body>
</html>