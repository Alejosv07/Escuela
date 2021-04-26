function detection(x) {
    if (x.matches) { // If media query matches
        //addItem();
    } else {
        //removeItem();
    }
}

var x = window.matchMedia("(max-width: 768px)")
detection(x) // Call listener function at run time
x.addListener(detection) // Attach listener function on state changes

function addItem() {
    let list = document.getElementById("opNavbar");
    let li = document.createElement("li");
    li.className = "nav-item";
    li.innerHTML = '<a class="nav-link" href="MiInfoEstudiante.html">Mi información</a>';
    list.appendChild(li);
}

function removeItem() {
    let list = document.getElementById("opNavbar");
    while (list.firstChild) {
        list.removeChild(list.firstChild);
    };
}