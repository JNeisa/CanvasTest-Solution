document.querySelector("#draw-input").onchange = function () {
    document.querySelector("#file-name").textContent = this.files[0].name;
    document.querySelector(".button-form").disabled = false;
}