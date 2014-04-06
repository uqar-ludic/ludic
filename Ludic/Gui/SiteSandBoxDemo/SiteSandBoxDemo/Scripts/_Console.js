function ConsoleFormSave() {
    document.getElementById('Console').value = editor.getSession().getValue();
    document.getElementById('Compil').value = outCompil.getSession().getValue();
    document.getElementById('Success').value = outSuccess.getSession().getValue();
    document.getElementById('ActionConsole').value = 1;
    document.getElementById('ConsoleForm').submit();
}

function ConsoleFormCompile() {
    document.getElementById('Console').value = editor.getSession().getValue();
    document.getElementById('Compil').value = outCompil.getSession().getValue();
    document.getElementById('Success').value = outSuccess.getSession().getValue();
    document.getElementById('ActionConsole').value = 2;
    document.getElementById('ConsoleForm').submit();
}

function ConsoleFormComment() {
    document.getElementById('ActionConsole').value = 3;
    document.getElementById('ConsoleForm').submit();
}

function test(star)
{
    document.getElementById('rating').value = star.value;
}

