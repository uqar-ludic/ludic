﻿window.onload = function () {

    /*
     * This hook adds autosizing functionality
     * to your textarea
     */
    BehaveHooks.add(['keydown'], function (data) {
        var numLines = data.lines.total,
            fontSize = parseInt(getComputedStyle(data.editor.element)['font-size']),
            padding = parseInt(getComputedStyle(data.editor.element)['padding']),
            height = (((numLines * fontSize) + padding));
        if (height < 250)
        {
            data.editor.element.style.height = 250 + 'px';
            document.getElementById('NumConsole').style.height = 250 + 'px';
        }
        else
        {
            data.editor.element.style.height = height + 'px';
            document.getElementById('NumConsole').style.height = height + 'px';
        }
    });

    /*
     * This hook adds Line Number Functionality
     */
    BehaveHooks.add(['keydown'], function (data) {
        var numLines = data.lines.total,
            house = document.getElementsByClassName('line-nums')[0],
            html = '',
            i;
        for (i = 0; i < numLines; i++) {
            html += '<div>' + (i + 1) + '</div>';
        }
        house.innerHTML = html;
    });

    var editor = new Behave({

        textarea: document.getElementById('Console'),
        replaceTab: true,
        softTabs: true,
        tabSize: 4,
        autoOpen: true,
        overwrite: true,
        autoStrip: true,
        autoIndent: true
    });

};