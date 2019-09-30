<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ctlCargaFoto.aspx.cs" Inherits="Inicial.Controlador.ctlCargaFoto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
        function showWait() {
            if ($get('myFile').value.length > 0) {
                $get('UpdateProgress1').style.display = 'block';
            }
        }

        function cambiaColorFuente() {
            document.getElementById('myFile').style.color = "black";
        }
    </script>
    <script src="../Recursos/js/send_request.js" type="text/javascript"></script>
</head>
<body>
    <table class="centrar">
        <tr>
            <td align="center">
                <input type="text" style="display: none;" id="txtImgSesion" value="<%if(Request.QueryString.Get("infog") != null){Response.Write(Request.QueryString.Get("infog"));}else{Response.Write("");}%>" />
                <img id="imgLogoADM" class="imgInfograma" width="120px" height="150" src="../Fotos/<% if(Session["usu_foto"] != null && !Session["usu_foto"].ToString().Equals("")) Response.Write(Session["usu_foto"]); else Response.Write("no_disponible.jpg"); %>"
                    alt="" />
            </td>
        </tr>
        <tr>
            <td align="center" style="width: 80px">
                <%--  <asp:TextBox id= "nomImgActual" runat="server" style="display:none;"></asp:TextBox>--%>
                <form id="form1" runat="server">
                <asp:ScriptManager ID="ScriptManager1" runat="server" />
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnUpload" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:FileUpload ID="myFile" runat="server" Style="color: #FFF;" OnChange="javascript:cambiaColorFuente();" />
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        <asp:Button ID="btnUpload" runat="server" Text="Cargar" OnClick="UploadFile" OnClientClick="javascript:showWait();" />
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                            <ProgressTemplate>
                                <asp:Label ID="lblWait" runat="server" BackColor="#000000" Font-Bold="True" ForeColor="White"
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
