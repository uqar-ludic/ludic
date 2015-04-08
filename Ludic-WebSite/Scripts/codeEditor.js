var editor = ace.edit("code-editor-div");
editor.setTheme("ace/theme/github");
editor.getSession().setMode("ace/mode/csharp");


var textarea = $('textarea[name="code-editor-text"]');
textarea.hide();
$('#submit').on('click', function () {
    textarea.val(editor.getSession().getValue());
});