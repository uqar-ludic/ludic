var editor;
var compil;
var success;

window.onload = function () {
    compil = ace.edit("compil");
    success = ace.edit("success");
    editor = ace.edit("editor");
    success.setTheme("ace/theme/twilight");
    compil.setTheme("ace/theme/twilight");
    editor.setTheme("ace/theme/twilight");
    success.getSession().setMode("ace/mode/csharp");
    compil.getSession().setMode("ace/mode/csharp");
    editor.getSession().setMode("ace/mode/csharp");
    $('[data-toggle="tooltip Console"]').tooltip({
        'placement': 'top',
        'template': '<div class="tooltip" style="margin-left: -52px;margin-top: 0px;"><div class="tooltip-arrow"></div><div class="tooltip-inner" style="padding-top: 15px;border-top-left-radius:15px;border-top-right-radius:15px;"><div class="tooltip-content"><p></p></div></div></div>'
    });
};