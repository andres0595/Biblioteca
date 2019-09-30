/*
* DESACTIVAR CLIC DERECHO
*/



$(document).ready(function () {
    var message = "";
    function clickIE() { if (document.all) { (message); return false; } }
    function clickNS(e) { if (document.layers || (document.getElementById && !document.all)) { if (e.which == 2 || e.which == 3) { (message); return false; } } }
    if (document.layers) { document.captureEvents(Event.MOUSEDOWN); document.onmousedown = clickNS; } else { document.onmouseup = clickNS; document.oncontextmenu = clickIE; }
    document.oncontextmenu = new Function("return false");

    $("textarea").keypress(function (e) {
        if (e.which == 13) {
            return false;
        }
    });
});

/*
patron = /\d/; // Solo acepta números
patron = /\w/; // Acepta números y letras
patron = /\D/; // No acepta números
patron =/[A-Za-z\s]/;letras sin n ni Ñ
patron =/[A-Za-zñÑ\s]/; // igual que el ejemplo, pero acepta también las letras ñ y Ñ
*/

function quitarEnter(texto) {
    var pat = new RegExp(String.fromCharCode(13), "g");
    var pat2 = new RegExp(String.fromCharCode(10), "g");
    texto = texto.replace(pat, "***");
    texto = texto.replace(pat2, "***");
    return texto;
}

function ponerEnter(texto) {
    var textoPartir = texto.split("***");
    var temp = '';
    var long = textoPartir.length;
    for (var i = 0; i < long; i++) {
        temp += textoPartir[i] + '\n';
    }
    temp = temp.replace("***", '\n');
    return temp;
    /*    var textoReemplazo = texto.replace("***",'\n');
    return textoReemplazo;*/
}

if (window.history) {
    function noBack() { window.history.forward(); }
    noBack();
    window.onload = noBack;
    window.onpageshow = function (evt) { if (evt.persisted) noBack(); }
    window.onunload = function () { void (0); }
}

function numerosEnteroDecimal(obj) {
    var re = /^[0-9]{1,100}((\.([0-9]{1,2}))|([0-9]{0,100}))$/;
    if (!re.test(obj.value)) {
        obj.focus();
    }
}

function isEnter(e) {
    if (window.event) {
        e = window.event;
    }
    if (e.keyCode == 13) {
        ingresar();
    }
}


function letras(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    if (tecla == 8) return true;
    if (tecla == 241) return true; /*ñ*/
    if (tecla == 209) return true; /*Ñ*/
    if (tecla == 64) return true; /*@*/
    if (tecla == 46) return true; /*.*/
    patron = /[A-Za-z\s]/;
    te = String.fromCharCode(tecla);
    return patron.test(te);
}

function letrassinespacio(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    if (tecla == 8) return true;
    patron = /[A-Za-z]/;
    te = String.fromCharCode(tecla);
    return patron.test(te);
}

function numerosLetras(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    if (tecla == 8) return true;
    patron = /[\s\w]/;
    te = String.fromCharCode(tecla);
    return patron.test(te);
}

function numerosLetrasGuion(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    if (tecla == 8) return true;
    patron = /[-\w]/;
    te = String.fromCharCode(tecla);
    return patron.test(te);
}

function numerospunto(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    if (tecla == 8) return true;
    patron = /[\d.]/;
    te = String.fromCharCode(tecla);
    return patron.test(te);
}

function numeroseslach(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    if (tecla == 8) return true;
    /*patron = /[\d/]/;*/
    te = String.fromCharCode(tecla);
    return patron.test(te);
}

function numerosguion(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    if (tecla == 8) return true;
    patron = /[\d-]/;
    te = String.fromCharCode(tecla);
    return patron.test(te);
}

function vdireccion(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    if (tecla == 8) return true;
    
    patron = /[\-\#\ª\ñ\.\,\+\(\)\;\Ñ\@\º\w\s\/]/;
    te = String.fromCharCode(tecla);
    return patron.test(te);
}



function numeros(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    if (tecla == 8) return true;
    if (tecla == 86) return true; /*V*/
    if (tecla == 118) return true; /*v*/
    if (tecla == 67) return true; /*C*/
    if (tecla == 99) return true; /*c*/
    if (tecla == 88) return true; /*X*/
    if (tecla == 120) return true; /*x*/
    patron = /\d/;
    te = String.fromCharCode(tecla);
    return patron.test(te);
}

function control(e, id) {
    if (e.ctrlKey == true && e.keyCode == 86 || e.ctrlKey == false && e.keyCode == 17) {

        muestraVentana('COMANDO RESTRINGIDO');
        document.getElementById(id).value = '';
    }

}

function vemail(email) {
    patron = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,4})+$/;
    if (patron.test(email) || email == "") {
        return true;
    } else {
        return false;
    }
}

function mayusculas(campo) {
    campo.value = campo.value.toUpperCase();
}

function ponerFocoOb(campo) {
    campo.style.background = "#FCFCFC";
}

function ponerFocoOp(campo) {
    campo.style.background = "#FCFCFC";
}

function quitarFoco(campo) {
    if (campo.value != "") {
        campo.style.background = "#FFFFFF";
        campo.style.border = "1px solid gray";
    } else {
        campo.style.background = "#FFFFFF";
        campo.style.border = "2px solid red";
    }
}

function quitarFocob(campo) {
    campo.style.background = "#FFFFFF";
}


/* ABRIR UNA PESTAÑA EN LA MISMA VENTANA DEL NAVEGADOR*/
function openInNewTab(URL) {
    var temporalForm = document.createElement('form');
    with (temporalForm) {
        setAttribute('method', 'GET');
        setAttribute('action', URL);
        setAttribute('target', '_blank');
    }

    var paramsString = URL.substring(URL.indexOf('?') + 1, URL.length);
    var paramsArray = paramsString.split('&');

    for (var i = 0; i < paramsArray.length; ++i) {
        var elementIndex = paramsArray[i].indexOf('=');
        var elementName = paramsArray[i].substring(0, elementIndex);
        var elementValue = paramsArray[i].substring(elementIndex + 1, paramsArray[i].length);

        var temporalElement = document.createElement('input');
        with (temporalElement) {
            setAttribute('type', 'hidden');
            setAttribute('name', elementName);
            setAttribute('value', elementValue);
        }
        temporalForm.appendChild(temporalElement);
    }

    document.body.appendChild(temporalForm);
    temporalForm.submit();
    document.body.removeChild(temporalForm);
}