function sendRequest(url, args, customFunction) {
    req = false;
    if (window.XMLHttpRequest) {
        req = new XMLHttpRequest();
        req.onreadystatechange = customFunction;
        if (args == "")
            req.open("GET", url, true);
        else
            req.open("GET", url + "?" + args, true);
        req.send("");
    }
    else {
        window.alert("This browser not is supported");
    }
    return req;
}

function newArg(key, value) {
    return key + "=" + encodeURIComponent(value);
}

function isValidResponse(response) {
    if (response.readyState == 4) {
        if (response.status == 200)
            return true;
        else
            alert('Error en Conexion...');
        return false;
    }
}