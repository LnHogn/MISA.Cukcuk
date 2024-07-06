document.getElementById("btnClose").addEventListener("click", function() {
    var dialog = document.getElementsByClassName("m-dialog");
    dialog[0].style.display = "none"; 
});

document.getElementById("btnAdd").addEventListener("click", function() {
    var dialog = document.getElementsByClassName("m-dialog");
    dialog[0].style.display = "block"; 
    document.getElementById('ma-nhan-vien').focus();
});

document.getElementById("btnClose-edit").addEventListener("click", function() {
    var dialog = document.getElementsByClassName("m-dialog m-dialog-edit");
    dialog[0].style.display = "none"; 
});

document.getElementsByClassName("btn-edit").addEventListener("click", function() {
    var dialog = document.getElementsByClassName("m-dialog m-dialog-edit");
    dialog[0].style.display = "block"; 
    document.getElementById('ma-nhan-vien').focus();
});


  