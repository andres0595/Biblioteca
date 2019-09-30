/*Funcion para saber si una cadena contiene a otra*/

jQuery.fn.contiene = function (str, char) {
    if (str != "") {
        for (var i = str.length - 1; i > 0; i--) {
            if (str[i] == char) {
                return i;
            }
        }
    }
    return str.length;
}