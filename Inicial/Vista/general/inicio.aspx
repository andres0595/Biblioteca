<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/master/pagina_maestra.Master"
    AutoEventWireup="true" CodeBehind="inicio.aspx.cs" Inherits="Inicial.Vista.general.inicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="server">
    .:: Inicio ::.</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contenidoSistema" runat="server">
    <script src="../../Recursos/js/graficas/jpgraph.js" type="text/javascript"></script>
    <script src="../../Recursos/js/ajax/presentacion.js" type="text/javascript"></script>

   <%-- <script src="../../Recursos/js/ajax/inicioSP2017V2.js" type="text/javascript"></script>--%>

    <script src="../../Recursos/js/ajax/inicioSP2018V2.js" type="text/javascript"></script>

    <%--<script src="../../Recursos/js/ajax/inicioSP2017.js" type="text/javascript"></script>--%>
    <%--<script src="../../Recursos/js/ajax/seguiPerOctubre2017.js" type="text/javascript"></script>--%>
    <%--<script src="../../Recursos/js/ajax/PacientesCargados.js" type="text/javascript"></script>--%>
    <%--<script src="../../Recursos/js/ajax/actividadesAbiertas.js" type="text/javascript"></script>--%>
    <%--FANCYBOX JS --%>
    <script type="text/javascript" src="../../Recursos/js/fancybox/jquery.mousewheel-3.0.4.pack.js"></script>
    <script type="text/javascript" src="../../Recursos/js/fancybox/jquery.fancybox-1.3.4.pack.js"></script>
    <%--FANCYBOX CSS --%>
    <link rel="stylesheet" type="text/css" href="../../Recursos/js/fancybox/jquery.fancybox-1.3.4.css"
        media="screen" />
    <link href="../../Recursos/css/login.css" rel="stylesheet" type="text/css" />
    <span class="ocultar" id="idMenuForm">I.1</span>
    <%--LIBRERIAS PARA GRAFICAS --%>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <script type="text/javascript">

        // Load the Visualization API and the piechart package.
        google.load('visualization', '1.0', { 'packages': ['corechart'] });
        //$(document).ready(function () {
        //    drawChart();
        //})
    </script>
    <div id="imgIndicador" class="listadoGeneral botonIndicador" style="width: 860px;
        display: none">
        <table class="centrar" style="width:777px;">
            <tr>
         

                <td style="font-weight: bold;" colspan="6" align="center">
                    Pacientes en el programa
                    <br />
                    de Atención Preferencial
               
                    <br />
                    <label style="color: #FF7811; font-size: 33px; cursor:pointer;" id="lblpacientesActivosInicial" onclick="direccionarPacientes(1)"
                        onmouseover='mostrarDiv(4);' onmouseout='ocultarDiv();'>
                        0</label><span id='divIndicadorRegional4' style='position:fixed; background-color:white; z-index:2; padding-left:70px;'></span>
                        <br /><br />
                </td>
   <%--<td style="border-bottom: 2px solid; border-color:Gray;" colspan="4"></td>--%>
            </tr>

          

            <tr>
            <td colspan="6" align="left">
            <input type="checkbox" id="chkinfopacruta" style="display:none;" /> &nbsp;&nbsp;&nbsp;<span  style=" font-size:13px; font-variant: inherit; font-weight:bolder;">Información Pacientes en Ruta</span></td>
            </tr>
            <tr><td><br /></td></tr>
            <tr>
              
            
                <td align="center" style="font-weight: bold;" colspan="2">
                <br />
                    Pacientes Activos
                    <br />
                    hasta el
                    <script type="text/javascript">
                        var meses = new Array("Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre");
                        var f = new Date();
                        document.write(f.getDate() + " de " + meses[f.getMonth()] + " del " + f.getFullYear());
                    </script>
                    <br />
                    <label style="color: green; font-size: 33px; cursor:pointer;" id="lblpacientesActivos" onclick="direccionarPacientes(2)"
                        onmouseover='mostrarDiv(5);' onmouseout='ocultarDiv();'>
                        0</label><span id='divIndicadorRegional5' style='position:fixed; background-color:white; z-index:2;'></span>
                        <br /><br />
                </td>
             
                <td align="center" style="font-weight: bold;" colspan="2">
                    Pacientes Inactivos
                    <br />
                    hasta el
                    <script type="text/javascript">
                        var meses = new Array("Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre");
                        var f = new Date();
                        document.write(f.getDate() + " de " + meses[f.getMonth()] + " del " + f.getFullYear());
                    </script>
                    <br />
                    <label style="color: red; font-size: 33px; cursor:pointer;" id="lblpacientesCierreInicial" onclick="direccionarPacientes(3)"
                        onmouseover='mostrarDiv(6);' onmouseout='ocultarDiv();'>
                        0</label><span id='divIndicadorRegional6' style='position:fixed; background-color:white; z-index:2;'></span>
                </td>
  
            
                  <td align="center" style="font-weight: bold;" colspan="2">
                    Pacientes Por Contactar
                    <br />
                    hasta el
                    <script type="text/javascript">
                        var meses = new Array("Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre");
                        var f = new Date();
                        document.write(f.getDate() + " de " + meses[f.getMonth()] + " del " + f.getFullYear());
                    </script>
                    <br />
                    <label style="color: Teal; font-size: 33px; cursor:pointer;" id="lblpacientesPorContactoInicial" 
                        onmouseover='mostrarDiv(7);' onmouseout='ocultarDiv();'>
                        0</label><span id='divIndicadorRegional7' style='position:fixed; background-color:white; z-index:2;'></span>
                </td>
            </tr>
            
   
            <tr>
 
              
                <td align="center" style="padding-right: 2px; padding-bottom: 262px;" colspan="2">
                    <div class="listadoGeneral " id="divlistarActivosConTramites" style="max-width: 90px;">
                        <table>
                            <tr>
                              
                             
                                <td class="encabezado">
                                    TIPO
                                </td>
                                <td class="encabezado">
                                    CANT.
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Menor</b>
                                </td>
                                <td class="cuerpoListado5">
                                    <label id="lbl11">
                                        0</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Adulto</b>
                                </td>
                                <td class="cuerpoListado5">
                                    <label id="lbl12">
                                        0</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Adulto Mayor</b>
                                </td>
                                <td class="cuerpoListado5">
                                    <label id="lbl13">
                                        0</label>
                                </td>
                            </tr>
                            <tr >
                                <td class="cuerpoListado10">
                                    <b>Sin Edad</b>
                                </td>
                                <td class="cuerpoListado5">
                                    <label id="lbl14">
                                        0</label>
                                </td>
                            </tr>
                        </table>
                    </div>
                   <div id="divGraficaActivos" style="position: absolute; padding-top: 7px; padding-left: 17px; background-color:transparent; z-index:1;">
                    </div>
                </td>
                <td align="center" style="padding-left: 2px; padding-bottom: 262px;" colspan = "2">
                    <div class="listadoGeneral " id="divlistarActivosSinTramites" style="max-width: 90px;">
                        <table>
                            <tr>
                                <td class="encabezado">
                                    TIPO
                                </td>
                                <td class="encabezado">
                                    CANT.
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Menor</b>
                                </td>
                                <td class="cuerpoListado5">
                                    <label id="lbl15">
                                        0</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Adulto</b>
                                </td>
                                <td class="cuerpoListado5">
                                    <label id="lbl16">
                                        0</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Adulto Mayor</b>
                                </td>
                                <td class="cuerpoListado5">
                                    <label id="lbl17">
                                        0</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Sin Edad</b>
                                </td>
                                <td class="cuerpoListado5">
                                    <label id="lbl18">
                                        0</label>
                                </td>
                            </tr>
                        </table>
                    </div>

                         <div id="divGraficaInactivos" style="position: absolute; padding-top: 7px; padding-left: 20px; background-color:transparent; z-index:1;">
                    </div>
                </td>

                <td align="center" style="padding-right: 2px; padding-bottom: 262px;" >
                    <div class="listadoGeneral " id="divlistarPorContactar" style="max-width: 90px;">
                        <table>
                            <tr>
                              
                             
                                <td class="encabezado">
                                    TIPO
                                </td>
                                <td class="encabezado">
                                    CANT.
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Menor</b>
                                </td>
                                <td class="cuerpoListado5">
                                    <label id="lbl19">
                                        0</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Adulto</b>
                                </td>
                                <td class="cuerpoListado5">
                                    <label id="lbl20">
                                        0</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Adulto Mayor</b>
                                </td>
                                <td class="cuerpoListado5">
                                    <label id="lbl21">
                                        0</label>
                                </td>
                            </tr>
                            <tr >
                                <td class="cuerpoListado10">
                                    <b>Sin Edad</b>
                                </td>
                                <td class="cuerpoListado5">
                                    <label id="lbl22">
                                        0</label>
                                </td>
                            </tr>
                        </table>
                    </div>
                   <div id="divGraficaPorContactar" style="position: absolute; padding-top: 7px; padding-left: 17px; background-color:transparent; z-index:1;">
                    </div>
                </td>
            
                
                

                


                

             <td></td>
                


            </tr>
    
         
 
  <tr><td style="" colspan="8"></td></tr>
  </table>
  
  <table style="display:none;">
     <tr>
            <td colspan="6" align="left">
            <br />
            <input type="checkbox" id="chkinfogespaciente" onclick="mostrargestioes2018()" /> &nbsp;&nbsp;&nbsp;<span  style=" font-size:13px; font-variant: inherit; font-weight:bolder;">Información de Gestión</span></td>
         
            </tr>
            <tr>
            <td></td>
            </tr>
  </table>
  
  <table id="tablegestioninfo" style="display:none;">

   
  <tr>        
  <td align="center" style="font-weight: bold;" colspan="2">
                    Pacientes Cargados en el
                    <br />
                    mes de
                    <script type="text/javascript">
                        var meses = new Array("Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre");
                        var f = new Date();
                        document.write(meses[f.getMonth()] + " del " + f.getFullYear());
                    </script>
                    <br />
                    <label style="color: blue; font-size: 33px; cursor: pointer;" id="lblpacientesCargados"
                        onclick="iralcargue('-1','-1','-2','-3')" onmouseover='mostrarDiv(1);' onmouseout='ocultarDiv();'>
                        0</label><span id='divIndicadorRegional1' style='position:fixed; background-color:white; z-index:2; top:555px;'></span>
                </td>

                <td style="border-right: 2px solid; border-color:Gray;"></td>

                  <td align="center" style="font-weight: bold;" colspan="3">
                    Contactabilidad<br />en el
                    
                    mes de <br />
                    <script type="text/javascript">
                        var meses = new Array("Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre");
                        var f = new Date();
                        document.write(meses[f.getMonth()] + " del " + f.getFullYear());
                    </script>
                    <br />
                
                </td>
                <td style="border-right: 2px solid; border-color:Gray;">
                </td>

                    <td style="font-weight: bold;">
                 
                 
                    <br />
                    Seguimientos<br />Programados
                    <br />
                    en el mes de
                    <br />
                    <script type="text/javascript">
                        var meses = new Array("Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre");
                        var f = new Date();
                        document.write(meses[f.getMonth()] + " del " + f.getFullYear());
                    </script>
                    <br />
                    <label style="color: #1269A2; font-size: 33px;" id="lblpacientesSegPerProgramados" onmouseover='mostrarDiv(11);' onmouseout='ocultarDiv();'>
                        0</label><span id='divIndicadorRegional11' style='position:fixed; background-color:white; top:550px; left:999px; z-index:2;'></span>
                    <br />
                </td>
                <td style="font-weight: bold;">
                    &nbsp;&nbsp;&nbsp;&nbsp;
                 
                    <br />
                    Seguimientos<br />
                    <label id="lblseguiperrealizados">
                        Periódicos<br />
                        Ejecutados</label>
                    <br />
                    en el mes de
                    <br />
                    <script type="text/javascript">
                        var meses = new Array("Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre");
                        var f = new Date();
                        document.write(meses[f.getMonth()] + " del " + f.getFullYear());
                    </script>
                    <br />
                    <label style="color: #4FA7FF; font-size: 33px; cursor: pointer;" id="lblpacientesSegPerRealizadosTotal"
                        onclick="iralseguimiento(-2)" onmouseover='mostrarDiv(12);' onmouseout='ocultarDiv();'>
                        0</label><span id='divIndicadorRegional12' style='position:fixed; background-color:white; top:550px; left:1080px; z-index:2;'></span>
                
                </td>
  
  </tr><tr>
    <td align="center" style="font-weight: bold;  width:145px; ">
                    Pacientes Cargados
                    <br />
                    Masivamente
                    <br />
                    <label style="color: blue; font-size: 33px; cursor: pointer;" id="lblpacientesMasivos"
                        onclick="iralcargue('-1','5','-2','-3')" onmouseover='mostrarDiv(3);' onmouseout='ocultarDiv();'>
                        0</label><span id='divIndicadorRegional3' style='position:fixed; background-color:white; z-index:2; top:600px;'></span>
                </td>
               
                <td align="center" style="font-weight: bold; ">
                
                    Pacientes Cargados
                    <br />
                    Individualmente
                    <br />
                    <label style="color: blue; font-size: 33px; cursor: pointer;" id="lblpacientesIndividuales"
                        onclick="iralcargue('-1','4','-2','-3')" onmouseover='mostrarDiv(2);' onmouseout='ocultarDiv();'>
                        0</label><span id='divIndicadorRegional2' style='position:fixed; background-color:white; z-index:2; top:600px;'></span>
                </td>
 <td style="border-right: 2px solid; border-color:Gray;   " > </td> 
    <td  style="font-weight: bold; width:212px;"> 
     Contacto Masivo<br />en el mes de<br />
                    <script type="text/javascript">
                        var meses = new Array("Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre");
                        var f = new Date();
                        document.write(meses[f.getMonth()] + " del " + f.getFullYear());
                    </script>
                    <br /> <br />
    </td>
     
   <td  style="font-weight: bold; "> 
        Contacto Individual<br /> en el mes de<br />
                    <script type="text/javascript">
                        var meses = new Array("Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre");
                        var f = new Date();
                        document.write(meses[f.getMonth()] + " del " + f.getFullYear());
                    </script>
                    <br /> <br />
      
    </td>
   
    <td></td>
     <td style="border-right: 2px solid; border-color:Gray;"></td>
     <td style="font-weight: bold;  padding-right: 5px;" 
                    align="center">
                    <div id="divseguiperdetallado2" align="center">
                        Seguimientos<br />
                        Periódicos<br />
                        Efectivos
                        <br />
                        <label style="color: #4FA7FF; font-size: 33px; cursor: pointer;" id="lblpacientesSegPerRealizados"
                            onclick="iralseguimiento(-2)" onmouseover='mostrarDiv(13);' onmouseout='ocultarDiv();' >
                            0</label><span id='divIndicadorRegional13' style='position:fixed; background-color:white; top:660px; left:1059px; z-index:2;'></span>
                        <br />
                    
                    </div>
                    <br />
                  
                    <div style="padding-left: 7px;">
                        <button id="lblverseguiper" style="background-color: ButtonFace; cursor: pointer;
                            font-weight: bold; font-size: 11px; height: 50px; width: 90px;" onclick="verseguiper();">
                            <b>Ver Efectivos<br />
                                No Efectivos</b></button>
                    </div>
                </td>
                <td style="font-weight: bold;  padding-left: 5px;" 
                    align="center">
                    <div id="divseguiperdetallado" align="center">
                        Seguimientos<br />
                        Periódicos<br />
                        No Efectivos
                        <br />
                        <label style="color: #4FA7FF; font-size: 33px;" id="lblpacientesNoSegPerRealizados" onmouseover='mostrarDiv(14);' onmouseout='ocultarDiv();'>                            
                            0</label><span id='divIndicadorRegional14' style='position:fixed; background-color:white; top:660px; left:1180px;  z-index:2;'></span>
                        <br /><br /><br /><br /><br />
                  </div>
                </td>


            
     

            
            </tr>
       
       <tr>
  



         <td style=" padding-left:7px;">
                    <div class="listadoGeneral" id="divlistarPacMasivos" style="max-width: 110px">
                        <table>
                            <tr>
                                <td class="encabezado">
                                    SEMANA
                                </td>
                                <td class="encabezado">
                                    CANT.
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Semana 1</b>
                                </td>
                                <td class="cuerpoListado10">
                                    <label id="lbl1">
                                        0</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Semana 2</b>
                                </td>
                                <td class="cuerpoListado10">
                                    <label id="lbl2">
                                        0</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Semana 3</b>
                                </td>
                                <td class="cuerpoListado10">
                                    <label id="lbl3">
                                        0</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Semana 4</b>
                                </td>
                                <td class="cuerpoListado10">
                                    <label id="lbl4">
                                        0</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Semana 5</b>
                                </td>
                                <td class="cuerpoListado10">
                                    <label id="lbl5">
                                        0</label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td align="center">
                    <div class="listadoGeneral" id="divlistarPacIndividial" style="max-width: 110px">
                        <table>
                            <tr>
                                <td class="encabezado">
                                    SEMANA
                                </td>
                                <td class="encabezado">
                                    CANT.
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Semana 1</b>
                                </td>
                                <td class="cuerpoListado10">
                                    <label id="lbl6">
                                        0</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Semana 2</b>
                                </td>
                                <td class="cuerpoListado10">
                                    <label id="lbl7">
                                        0</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Semana 3</b>
                                </td>
                                <td class="cuerpoListado10">
                                    <label id="lbl8">
                                        0</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Semana 4</b>
                                </td>
                                <td class="cuerpoListado10">
                                    <label id="lbl9">
                                        0</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Semana 5</b>
                                </td>
                                <td class="cuerpoListado10">
                                    <label id="lbl10">
                                        0</label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td style="border-right: 2px solid; border-color:Gray;"></td>
                
                <td style="padding-left:7px;">
                   <div class="listadoGeneral " id="div5" style="max-width: 180px" align="center">
                        <table>
                            <tr>
                                <td class="encabezado">
                                    TIPO
                                </td>
                                <td class="encabezado">
                                    EFECT
                                </td>

                            <td class="encabezado">
                                    NO EFECT
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Sem1</b>
                                </td>
                                <td class="cuerpoListado10">
                                    <label id="lbl1rai">
                                        0</label>
                                </td>

                              <td class="cuerpoListado10">
                                    <label id="lbl1raii">
                                        0</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Sem2</b>
                                </td>
                                <td class="cuerpoListado10">
                                    <label id="lbl2dai">
                                        0</label>
                                </td>
                                    <td class="cuerpoListado10">
                                    <label id="lbl2daii">
                                        0</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Sem3</b>
                                </td>
                                <td class="cuerpoListado10">
                                    <label id="lbl3rai">
                                        0</label>
                                </td>
                                 <td class="cuerpoListado10">
                                    <label id="lbl3raii">
                                        0</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Sem4</b>
                                </td>
                                <td class="cuerpoListado10">
                                    <label id="lblNCi">
                                        0</label>
                                </td>
                              <td class="cuerpoListado10">
                                    <label id="lblNCii">
                                        0</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Sem5</b>
                                </td>
                                <td class="cuerpoListado10">
                                    <label id="lbl4tai">
                                        0</label>
                                </td>

                             <td class="cuerpoListado10">
                                    <label id="lbl4taii">
                                        0</label>
                                </td>
                            </tr>
                            
                        </table>
                    </div>
    </td>
  
    <td>


                      <div class="listadoGeneral " id="div6" style="max-width: 180px;" align="center">
                        <table>
                            <tr>
                                <td class="encabezado">
                                    TIPO
                                </td>
                                <td class="encabezado">
                                    EFECT
                                </td>
                                <td class="encabezado">
                                    NO EFECT
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Sem1</b>
                                </td>
                                <td class="cuerpoListado10">
                                    <label id="lbl1raf">
                                        0</label>
                                </td>
                                 <td class="cuerpoListado10">
                                    <label id="lbl1raff">
                                        0</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Sem2</b>
                                </td>
                                <td class="cuerpoListado10">
                                    <label id="lbl2daf">
                                        0</label>
                                </td>
                              <td class="cuerpoListado10">
                                    <label id="lbl2daff">
                                        0</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Sem3</b>
                                </td>
                                <td class="cuerpoListado10">
                                    <label id="lbl3raf">
                                        0</label>
                                </td>
                                <td class="cuerpoListado10">
                                    <label id="lbl3raff">
                                        0</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Sem4</b>
                                </td>
                                <td class="cuerpoListado10">
                                    <label id="lblncf">
                                        0</label>
                                </td>
                             <td class="cuerpoListado10">
                                    <label id="lblncff">
                                        0</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Sem5</b>
                                </td>
                                <td class="cuerpoListado10">
                                    <label id="lbl4taf">
                                        0</label>
                                </td>
                             <td class="cuerpoListado10">
                                    <label id="lbl4taff">
                                        0</label>
                                </td>
                            </tr>
                  
                        </table>
                  </div>
    </td>
    <td></td><td style="border-right: 2px solid; border-color:Gray;"></td>
       <td style="padding-left:7px;">
            <div class="listadoGeneral " id="divlistarSegPerRealizados" style="max-width: 110px">
                        </div>
        </td>

        <td>
              <div class="listadoGeneral " id="divlistarSegPerNoRealizados" style="max-width: 110px">
                        </div>
                 
                  
        
       </td>
      

    
       </tr>
  
  <tr>
  
   
    <td colspan="7"></td>
  <td>
    <div class="listadoGeneral " id="divlistarSegPerRealizados2" style="max-width: 110px; display:none;">
                    </div>
  </td>
            
        
  
  </tr>


  <%--<tr>
    <td></td><td></td><td></td>
  
    
     <td>
                    <div class="listadoGeneral" id="divlistarPacMasivos" style="max-width: 110px">
                        <table>
                            <tr>
                                <td class="encabezado">
                                    SEMANA
                                </td>
                                <td class="encabezado">
                                    CANT.
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Semana 1</b>
                                </td>
                                <td class="cuerpoListado10">
                                    <label id="lbl1">
                                        0</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Semana 2</b>
                                </td>
                                <td class="cuerpoListado10">
                                    <label id="lbl2">
                                        0</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Semana 3</b>
                                </td>
                                <td class="cuerpoListado10">
                                    <label id="lbl3">
                                        0</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Semana 4</b>
                                </td>
                                <td class="cuerpoListado10">
                                    <label id="lbl4">
                                        0</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Semana 5</b>
                                </td>
                                <td class="cuerpoListado10">
                                    <label id="lbl5">
                                        0</label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td align="center">
                    <div class="listadoGeneral" id="divlistarPacIndividial" style="max-width: 110px">
                        <table>
                            <tr>
                                <td class="encabezado">
                                    SEMANA
                                </td>
                                <td class="encabezado">
                                    CANT.
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Semana 1</b>
                                </td>
                                <td class="cuerpoListado10">
                                    <label id="lbl6">
                                        0</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Semana 2</b>
                                </td>
                                <td class="cuerpoListado10">
                                    <label id="lbl7">
                                        0</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Semana 3</b>
                                </td>
                                <td class="cuerpoListado10">
                                    <label id="lbl8">
                                        0</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Semana 4</b>
                                </td>
                                <td class="cuerpoListado10">
                                    <label id="lbl9">
                                        0</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Semana 5</b>
                                </td>
                                <td class="cuerpoListado10">
                                    <label id="lbl10">
                                        0</label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
    
     <td align="center" style="padding-right: 2px; padding-bottom: 395px; display: none;">
                    <div class="listadoGeneral " id="div1" style="max-width: 85px">
                        <table>
                            <tr>
                                <td class="encabezado">
                                    TIPO
                                </td>
                                <td class="encabezado">
                                    CANT.
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Nuevo</b>
                                </td>
                                <td class="cuerpoListado10">
                                    21
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Dudoso</b>
                                </td>
                                <td class="cuerpoListado10">
                                    80
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="divGraficaPorEdad" style="position: absolute; padding-top: 66px; padding-left: 0px;">
                    </div>
                </td>
                <td align="center" style="padding-left: 2px; padding-bottom: 367px; display: none;">
                    <div class="listadoGeneral " id="div3" style="max-width: 111px">
                        <table>
                            <tr>
                                <td class="encabezado">
                                    TIPO
                                </td>
                                <td class="encabezado">
                                    CANT.
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>No Contactable</b>
                                </td>
                                <td class="cuerpoListado10">
                                    706
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Cerrado No Contactable</b>
                                </td>
                                <td class="cuerpoListado10">
                                    0
                                </td>
                            </tr>
                        </table>
                    </div>
                  
    </td><td>
                   <div class="listadoGeneral " id="div4" style="max-width: 120px" align="center">
                        <table>
                            <tr>
                                <td class="encabezado">
                                    TIPO
                                </td>
                                <td class="encabezado">
                                    EFECT
                                </td>

                            <td class="encabezado">
                                    NO EFECT
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Sem1</b>
                                </td>
                                <td class="cuerpoListado10">
                                    <label id="lbl1rai">
                                        0</label>
                                </td>

                              <td class="cuerpoListado10">
                                    <label id="lbl1raii">
                                        0</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Sem2</b>
                                </td>
                                <td class="cuerpoListado10">
                                    <label id="lbl2dai">
                                        0</label>
                                </td>
                                    <td class="cuerpoListado10">
                                    <label id="lbl2daii">
                                        0</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Sem3</b>
                                </td>
                                <td class="cuerpoListado10">
                                    <label id="lbl3rai">
                                        0</label>
                                </td>
                                 <td class="cuerpoListado10">
                                    <label id="lbl3raii">
                                        0</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Sem4</b>
                                </td>
                                <td class="cuerpoListado10">
                                    <label id="lblNCi">
                                        0</label>
                                </td>
                              <td class="cuerpoListado10">
                                    <label id="lblNCii">
                                        0</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Sem5</b>
                                </td>
                                <td class="cuerpoListado10">
                                    <label id="lbl4tai">
                                        0</label>
                                </td>

                             <td class="cuerpoListado10">
                                    <label id="lbl4taii">
                                        0</label>
                                </td>
                            </tr>
                            
                        </table>
                    </div>
    </td>
    
    <td>
                      <div class="listadoGeneral " id="div5" style="max-width: 120px;" align="center">
                        <table>
                            <tr>
                                <td class="encabezado">
                                    TIPO
                                </td>
                                <td class="encabezado">
                                    EFECT
                                </td>
                                <td class="encabezado">
                                    NO EFECT
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Sem1</b>
                                </td>
                                <td class="cuerpoListado10">
                                    <label id="lbl1raf">
                                        0</label>
                                </td>
                                 <td class="cuerpoListado10">
                                    <label id="lbl1raff">
                                        0</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Sem2</b>
                                </td>
                                <td class="cuerpoListado10">
                                    <label id="lbl2daf">
                                        0</label>
                                </td>
                              <td class="cuerpoListado10">
                                    <label id="lbl2daff">
                                        0</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Sem3</b>
                                </td>
                                <td class="cuerpoListado10">
                                    <label id="lbl3raf">
                                        0</label>
                                </td>
                                <td class="cuerpoListado10">
                                    <label id="lbl3raff">
                                        0</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Sem4</b>
                                </td>
                                <td class="cuerpoListado10">
                                    <label id="lblncf">
                                        0</label>
                                </td>
                             <td class="cuerpoListado10">
                                    <label id="lblncff">
                                        0</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="cuerpoListado10">
                                    <b>Sem5</b>
                                </td>
                                <td class="cuerpoListado10">
                                    <label id="lbl4taf">
                                        0</label>
                                </td>
                             <td class="cuerpoListado10">
                                    <label id="lbl4taff">
                                        0</label>
                                </td>
                            </tr>
                  
                        </table>
                  </div>
    </td>
    
   
    
   <td></td>
    
      
          
  </tr>--%>
  
  
        </table>
    </div>
    <div id="divimgVitaly" class="centrar" style="width: 808px; height: 333px; display: none;
        padding-top: 100px;" align="center">
        <table class="centrar responsive" align="center">
            <tr class="centrar" align="center">
                <td class=" centrar" align="center">
                    <img src="../../Recursos/imagenes/administracion/heart.png" />
                </td>
            </tr>
        </table>
    </div>
    <%--<tr>
            <td >
                <div id="listadoNoEjecutadas" class="centrar listadoGeneral ">
                </div>
            </td>
        </tr>--%>
    <!--FORMULARIO PARA EJECUTAR SEGUIMIENTO-->
    <%--<div style="display: none;">
        <div id="divEjecutar" class="fancyNormal tbBorde">
            <table class="centrar">
                <tr>
                    <td colspan="2" align="center">
                        <span class="tituloForm">EJECUTAR ACTIVIDAD</span><br />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div id="listadoDetalleEjecutar" class=" listadoGeneralDetalle centrar">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="1">
                        <table>
                            <tr>
                                <td>
                                    Actividad Realizada
                                </td>
                                <td>
                                    <input type="radio" id="rbSiActividad" checked="True" onclick="estadoActividad('siActividad')" />
                                </td>
                                <td>
                                    SI
                                </td>
                                <td>
                                    <input type="radio" id="rbNoActividad" onclick="estadoActividad('noActividad')" />
                                </td>
                                <td>
                                    NO
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td colspan="1">
                        <table>
                            <tr>
                                <td>
                                    Objetivo Cumplido
                                </td>
                                <td>
                                    <input type="radio" id="rbSiObjetivo" checked="True" onclick="estadoObjetivo('siObjetivo')" />
                                </td>
                                <td>
                                    SI
                                </td>
                                <td>
                                    <input type="radio" id="rbNoObjetivo" onclick="estadoObjetivo('noObjetivo')" />
                                </td>
                                <td>
                                    NO
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table id="mostrarFechaHora" style="display: block">
                            <tr>
                                <td colspan="6">
                                    <span>Fecha y Hora de Ejecución</span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Fecha
                                </td>
                                <td>
                                    <input type="text" id="txtFechaEjecucion" class="campoTexto campoMinuscula obligatorio" />
                                </td>
                                <td>
                                    Hora Inicio
                                </td>
                                <td>
                                    <select id="selHorasInicioEjecucion" class="selectHora obligatorio">
                                        <option>-H-</option>
                                        <option>0</option>
                                        <option>1</option>
                                        <option>2</option>
                                        <option>3</option>
                                        <option>4</option>
                                        <option>5</option>
                                        <option>6</option>
                                        <option>7</option>
                                        <option>8</option>
                                        <option>9</option>
                                        <option>10</option>
                                        <option>11</option>
                                        <option>12</option>
                                        <option>13</option>
                                        <option>14</option>
                                        <option>15</option>
                                        <option>16</option>
                                        <option>17</option>
                                        <option>18</option>
                                        <option>19</option>
                                        <option>20</option>
                                        <option>21</option>
                                        <option>22</option>
                                        <option>23</option>
                                    </select>
                                </td>
                                <td>
                                    <select id="selMinutosInicioEjecucion" class="selectHora obligatorio">
                                        <option>-M-</option>
                                        <option>00</option>
                                        <option>01</option>
                                        <option>02</option>
                                        <option>03</option>
                                        <option>04</option>
                                        <option>05</option>
                                        <option>06</option>
                                        <option>07</option>
                                        <option>08</option>
                                        <option>09</option>
                                        <option>10</option>
                                        <option>11</option>
                                        <option>12</option>
                                        <option>13</option>
                                        <option>14</option>
                                        <option>15</option>
                                        <option>16</option>
                                        <option>17</option>
                                        <option>18</option>
                                        <option>19</option>
                                        <option>20</option>
                                        <option>21</option>
                                        <option>22</option>
                                        <option>23</option>
                                        <option>24</option>
                                        <option>25</option>
                                        <option>26</option>
                                        <option>27</option>
                                        <option>28</option>
                                        <option>29</option>
                                        <option>30</option>
                                        <option>31</option>
                                        <option>32</option>
                                        <option>33</option>
                                        <option>34</option>
                                        <option>35</option>
                                        <option>36</option>
                                        <option>37</option>
                                        <option>38</option>
                                        <option>39</option>
                                        <option>40</option>
                                        <option>41</option>
                                        <option>42</option>
                                        <option>43</option>
                                        <option>44</option>
                                        <option>45</option>
                                        <option>46</option>
                                        <option>47</option>
                                        <option>48</option>
                                        <option>49</option>
                                        <option>50</option>
                                        <option>51</option>
                                        <option>52</option>
                                        <option>53</option>
                                        <option>54</option>
                                        <option>55</option>
                                        <option>56</option>
                                        <option>57</option>
                                        <option>58</option>
                                        <option>59</option>
                                    </select>
                                </td>
                                <td>
                                    Hora Fin
                                </td>
                                <td>
                                    <select id="selHorasFinEjecucion" class="selectHora obligatorio">
                                        <option>-H-</option>
                                        <option>0</option>
                                        <option>1</option>
                                        <option>2</option>
                                        <option>3</option>
                                        <option>4</option>
                                        <option>5</option>
                                        <option>6</option>
                                        <option>7</option>
                                        <option>8</option>
                                        <option>9</option>
                                        <option>10</option>
                                        <option>11</option>
                                        <option>12</option>
                                        <option>13</option>
                                        <option>14</option>
                                        <option>15</option>
                                        <option>16</option>
                                        <option>17</option>
                                        <option>18</option>
                                        <option>19</option>
                                        <option>20</option>
                                        <option>21</option>
                                        <option>22</option>
                                        <option>23</option>
                                    </select>
                                </td>
                                <td>
                                    <select id="selMinutosFinEjecucion" class="selectHora obligatorio">
                                        <option>-M-</option>
                                        <option>00</option>
                                        <option>01</option>
                                        <option>02</option>
                                        <option>03</option>
                                        <option>04</option>
                                        <option>05</option>
                                        <option>06</option>
                                        <option>07</option>
                                        <option>08</option>
                                        <option>09</option>
                                        <option>10</option>
                                        <option>11</option>
                                        <option>12</option>
                                        <option>13</option>
                                        <option>14</option>
                                        <option>15</option>
                                        <option>16</option>
                                        <option>17</option>
                                        <option>18</option>
                                        <option>19</option>
                                        <option>20</option>
                                        <option>21</option>
                                        <option>22</option>
                                        <option>23</option>
                                        <option>24</option>
                                        <option>25</option>
                                        <option>26</option>
                                        <option>27</option>
                                        <option>28</option>
                                        <option>29</option>
                                        <option>30</option>
                                        <option>31</option>
                                        <option>32</option>
                                        <option>33</option>
                                        <option>34</option>
                                        <option>35</option>
                                        <option>36</option>
                                        <option>37</option>
                                        <option>38</option>
                                        <option>39</option>
                                        <option>40</option>
                                        <option>41</option>
                                        <option>42</option>
                                        <option>43</option>
                                        <option>44</option>
                                        <option>45</option>
                                        <option>46</option>
                                        <option>47</option>
                                        <option>48</option>
                                        <option>49</option>
                                        <option>50</option>
                                        <option>51</option>
                                        <option>52</option>
                                        <option>53</option>
                                        <option>54</option>
                                        <option>55</option>
                                        <option>56</option>
                                        <option>57</option>
                                        <option>58</option>
                                        <option>59</option>
                                    </select>
                                </td>
                            </tr>
                </tr>
                </table> </td> </tr>
                <tr>
                    <td colspan="2">
                        Observaciones
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <textarea id="txtObservacionesEjecutar" class="observacion campoMayuscula" style="width: 480px"></textarea>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <br />
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <table class="centrar">
                            <tr>
                                <td align="center">
                                    <span class="leyenda">Guardar</span><br />
                                    <img src="../../Recursos/imagenes/administracion/guardar.png" alt="Guardar" class="imgAdmin"
                                        onclick="guardaEjecutarSeguimiento()" />
                                </td>
                                <td align="center">
                                    <span class="leyenda">Cancelar</span><br />
                                    <img src="../../Recursos/imagenes/administracion/cancelar.png" alt="Cancelar" class="imgAdmin"
                                        onclick="cerrarFancy()" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>--%>
    <!--FORMULARIO PARA EJECUTAR SEGUIMIENTO-->
    <div style="display: none;">
        <div id="divEjecutar" class="fancyNormal tbBorde">
            <table class="centrar">
                <tr>
                    <td colspan="2" align="center">
                        <span class="tituloForm">EJECUTAR ACTIVIDAD</span><br />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 210px" class="colorFila">
                        <label id="lblTipoActividad">
                        </label>
                    </td>
                    <td align="left" style="width: 205px" class="colorFila">
                        <label id="lblFechaHora">
                        </label>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="colorFila">
                        <span>Objetivo:</span>
                        <label id="lblObjetivo">
                        </label>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="colorFila">
                        <span>Cliente:</span>
                        <label id="lblCliente">
                        </label>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="colorFila">
                        <span>Persona:</span>
                        <label id="lblPersona">
                        </label>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="colorFila">
                        <span>Oportunidad:</span>
                        <label id="lblOportunidad">
                        </label>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="colorFila">
                        <span>Responsable:</span>
                        <label id="lblResponsable">
                        </label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="1">
                        <table>
                            <tr>
                                <td>
                                    Actividad Realizada
                                </td>
                                <td>
                                    <input type="radio" id="rbSiActividad" checked="True" onclick="estadoActividad('siActividad')" />
                                </td>
                                <td>
                                    SI
                                </td>
                                <td>
                                    <input type="radio" id="rbNoActividad" onclick="estadoActividad('noActividad')" />
                                </td>
                                <td>
                                    NO
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td colspan="1">
                        <table>
                            <tr>
                                <td>
                                    Objetivo Cumplido
                                </td>
                                <td>
                                    <input type="radio" id="rbSiObjetivo" checked="True" onclick="estadoObjetivo('siObjetivo')" />
                                </td>
                                <td>
                                    SI
                                </td>
                                <td>
                                    <input type="radio" id="rbNoObjetivo" onclick="estadoObjetivo('noObjetivo')" />
                                </td>
                                <td>
                                    NO
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table id="mostrarFechaHora" style="display: block">
                            <tr>
                                <td colspan="8">
                                    <span>Fecha y Hora de Ejecución</span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Fecha
                                </td>
                                <td>
                                    <input type="text" id="txtFechaEjecucion" class="campoTexto campoMinuscula obligatorio" />
                                </td>
                                <td>
                                    Hora Inicio
                                </td>
                                <td>
                                    <input type="text" id="horaInicioEjecucion" class="campoTextoCorto obligatorio" maxlength="5"
                                        placeholder="HH:MM" onblur="validaHora(this.id)" onkeypress="return numeros(event);"
                                        onkeyup="colocarDosPuntos(this.id)" />
                                    <select id="AmPmHoraInicioEjecucion" class="selectCorto" style="width: 50px;">
                                        <option value="AM">AM</option>
                                        <option value="PM">PM</option>
                                    </select>
                                </td>
                                <td>
                                    Hora Fin
                                </td>
                                <td>
                                    <input type="text" id="horaFinEjecucion" class="campoTextoCorto obligatorio" maxlength="5"
                                        placeholder="HH:MM" onblur="validaHora(this.id)" onkeypress="return numeros(event);"
                                        onkeyup="colocarDosPuntos(this.id)" />
                                    <select id="AmPmHoraFinEjecucion" class="selectCorto" style="width: 50px;">
                                        <option value="AM">AM</option>
                                        <option value="PM">PM</option>
                                    </select>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span>Etapa:</span>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        Actual:
                        <input type="text" id="txtActual" class="campoTexto campoMayuscula" />
                        Siguiente:
                        <input type="text" id="txtSiguiente" class="campoTexto campoMayuscula" />
                    </td>
                </tr>
                <tr>
                    <td colspan="1">
                        <table id="cambiarEtapa">
                            <tr>
                                <td>
                                    Cambiar de etapa?
                                </td>
                                <td>
                                    <input type="radio" id="etapaNO" checked="True" onclick="CambiarEtapa('NO')" />
                                </td>
                                <td>
                                    NO
                                </td>
                                <td>
                                    <input type="radio" id="etapaSI" onclick="CambiarEtapa('SI')" />
                                </td>
                                <td>
                                    SI
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        Observaciones
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <textarea class="descripcionMediana campoMayuscula descripcion" id="txtObservacionesEjecutar"
                            style="min-width: 490px;" rows="2" cols="9"></textarea>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <br />
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table align="center">
                            <tr>
                                <td align="center" colspan="2">
                                    ARCHVIOS DE SOPORTE.
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <div id="iframeArchivo">
                                    </div>
                                </td>
                            </tr>
                            <%--<tr id="mostrarListadoArchivos">
                                <td>
                                    <div id="listadoArchivos" class="centrar listadoGeneral700" style="width: 470px; display: none">  
                                    </div>
                                </td>
                            </tr>--%>
                            <tr>
                                <td align="center" colspan="2">
                                    <div id="listadoGeneralArchivos" class="centrar listadoGeneral" style="width: 500px">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <br />
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <table class="centrar">
                            <tr>
                                <td align="center">
                                    <div id="Div2">
                                        <span class="leyenda">Guardar</span><br />
                                        <img src="../../Recursos/imagenes/administracion/guardar.png" title="Guardar" alt="Guardar"
                                            class="imgAdmin" onclick="guardaEjecutarSeguimiento()" />
                                    </div>
                                </td>
                                <td align="center">
                                    <span class="leyenda">Cancelar</span><br />
                                    <img src="../../Recursos/imagenes/administracion/cancelar.png" onclick="cerrarFancy()"
                                        alt="Cancelar" class="imgAdmin" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <!--FORMULARIO PARA LOS DETALLES-->
    <div style="display: none;">
        <div id="divDetalle" style="overflow: hidden;" class="fancyNormal tbBorde">
            <table class="centrar">
                <tr>
                    <td align="center">
                        <span class="tituloForm">DETALLE CLIENTES</span>
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="checkbox" id="ckHabilitar" onclick="mostrarListado(this, 3)" />
                        <span class="subtitulosDetalle">SELECCIONAR TODOS</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="listadoDetalle" class=" listadoGeneralDetalle centrar">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="checkbox" id="ckContactos" onclick="mostrarListado(this, 1)" />
                        <span class="subtitulosDetalle">CONTACTOS</span>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="6">
                        <div id="listadoContactos" class="centrar listadoGeneral">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="checkbox" id="ckArchivos" onclick="mostrarListado(this, 2)" />
                        <span class="subtitulosDetalle">ARCHIVOS</span>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="6">
                        <div id="listadoArchivos" class="centrar listadoGeneral">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="5">
                        <table class="centrar">
                            <tr>
                                <td>
                                    <div>
                                        <span class="leyenda">Cancelar</span><br />
                                        <img src="../../Recursos/imagenes/administracion/cancelar.png" alt="Cancelar" class="imgAdmin"
                                            onclick="cerrarFancy()" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>




    <!--FORMULARIO PARA LOS DETALLES-->
    <div style="display: none;">
        <div id="divIndicadorRegionalL" style="overflow: hidden; width:222px;" class="fancyNormal tbBorde">
            <table class="centrar">
                <tr>
                    <td align="center">
                        <span class="tituloForm">CANTIDAD POR REGIONAL</span>
                        <br />
                        <br />
                    </td>
                </tr>
               
             
                <tr>
                    <td align="center">
                        <div id="divlistadoIndicadorRegional" class="centrar listadoGeneral">
                        </div>
                    </td>
                </tr>
            
            </table>
        </div>
    </div>
</asp:Content>
