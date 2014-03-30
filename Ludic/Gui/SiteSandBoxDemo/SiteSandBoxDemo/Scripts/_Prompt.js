var editor;

window.onload = function () {
    editor = ace.edit("editor");
    editor.setTheme("ace/theme/twilight");
    editor.getSession().setMode("ace/mode/csharp");
    $('[data-toggle="tooltip Console"]').tooltip({
        'placement': 'top',
        'template': '<div class="tooltip" style="margin-left: -52px;margin-top: 0px;"><div class="tooltip-arrow"></div><div class="tooltip-inner" style="padding-top: 15px;border-top-left-radius:15px;border-top-right-radius:15px;"><div class="tooltip-content"><p></p></div></div></div>'
    });
};