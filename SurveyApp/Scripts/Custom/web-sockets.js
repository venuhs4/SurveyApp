//var hostIP = "166.62.35.239";
var hostIP = "localhost";
wsConnect = function (from, to,data, onconnect) {
    debugger;   
    console.log(from, to);
    var ws = {};
    if ("WebSocket" in window) {
        ws = new WebSocket("ws://" + hostIP + ":8001");
        ws.onopen = function () {
            console.log("connected to server..");
            var obj = { from, to, data };
            onconnect();
            ws.send(JSON.stringify(obj));
            console.log("connection between " + from + " and " + to + " is established.");
        };
       
        return ws;
    }
    else {
        alert("WebSocket NOT supported by your Browser!");
        return null;
    }
}

function getChanges(oldString, newString) {
    var addedIndex = false;
    var changes = [];
    var obj = [];
    var oi = 0, ni = 0;
    while (oi < oldString.length && ni < newString.length) {
        if (newString.charAt(ni) != oldString.charAt(oi)) {
            if (!addedIndex) {
                obj.push(oi);
                obj.push(newString.charAt(ni));
                addedIndex = true;
            }
            else {
                obj[1] += newString.charAt(ni);
            }
            ni++;
        }
        else {
            if (addedIndex) {
                changes.push(obj);
                obj = [];
                addedIndex = false;
            }
            oi++;
            ni++;
        }
    };
    if (addedIndex) {
        changes.push(obj);
        obj = [];
        addedIndex = false;
    }
    obj = [];
    if (ni == newString.length && oi < oldString.length) {
        obj.push(-1 * oi);
        changes.push(obj);
    }
    if (oi == oldString.length && ni < newString.length) {
        obj.push(ni);
        obj.push(newString.substring(ni));
        changes.push(obj);
    }
    return changes;
}
function getOriginal(oldStrings, changes) {
    var result = oldStrings;
    for (var i = changes.length - 1; i >= 0  ; i--) {
        if (changes[i][0] < 0) {
            result = result.substring(0, -1 * changes[i][0]);
        }
        else {
            result = result.substring(0, changes[i][0]) + changes[i][1] + result.substring(changes[i][0]);
        }
    };
    return result;
}
