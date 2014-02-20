function ConsoleFormSave() {
    document.getElementById('ActionConsole').value = 1;
    document.getElementById('ConsoleForm').submit();
    
}

function ConsoleFormCompile() {
    document.getElementById('ActionConsole').value = 2;
    document.getElementById('ConsoleForm').submit();
}

function ConsoleFormComment() {
    document.getElementById('ActionConsole').value = 3;
    document.getElementById('ConsoleForm').submit();
}
