<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ctlCargaLogoEmpresa.aspx.cs" Inherits="Inicial.Controlador.ctlCargaLogoEmpresa" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
     <script language="javascript" type="text/javascript">
         function showWait() {
             if ($get('myFile').value.length > 0) {
                 $get('UpdateProgress1').style.display = 'block';
             }
         }
    </script>
</head>
<body >
    <table class="centrar">
        <tr>
            <td align="center">
                <img class="imgInfograma" width="150px" src="../logos/<% if(Request.Form["logo"] != null) Response.Write(Request.Form["logo"]); else if(Session["logo_empresa"] != null && !Session["logo_empresa"].ToString().Equals("")) Response.Write(Session["logo_empresa"]); else Response.Write("no_disponible.jpg"); %>"
                    alt="" />
                    <br />
                    <br />
            </td>
        </tr>
        <tr>
            <td align="center">
                <form id="form1" runat="server">
                <asp:ScriptManager ID="ScriptManager1" runat="server" />
               
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnUpload" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:FileUpload ID="myFile" runat="server" />
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="UploadFile" OnClientClick="javascript:showWait();" />
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                            <ProgressTemplate>
                                <asp:Label ID="lblWait" runat="server" BackColor="#0B519C" Font-Bold="True" ForeColor="White"
                                    Text="Cargando Archivo..."></asp:Label>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </ContentTemplate>
                </asp:UpdatePanel>
                </form>
            </td>
        </tr>
    </table>
</body>
</html>
