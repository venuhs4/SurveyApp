wsConnect = function (from, to,onconnect) {
    console.log(from, to);
    var ws = {};
    if ("WebSocket" in window) {
        ws = new WebSocket("ws://166.62.35.239:8001");
        ws.onopen = function () {
            console.log("connected to server..");
            var obj = { from, to };
            ws.send(JSON.stringify(obj));
            console.log("connection between " + from + " and " + to + " is established.");
            onconnect();
        };
       
        return ws;
    }
    else {
        alert("WebSocket NOT supported by your Browser!");
        return null;
    }
}
