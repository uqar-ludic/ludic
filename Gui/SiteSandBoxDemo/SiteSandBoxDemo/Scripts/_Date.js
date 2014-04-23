function rafraichir() {
    var date = new Date();
    var texte = '';
    A = date.getFullYear();
    D = date.getDate();
    MO = date.getMonth() + 1;
    if (MO < 10)
        MO = "0" + MO;
    H = date.getHours();
    if (H < 10)
        H = "0" + H;
    M = date.getMinutes();
    if (M < 10)
        M = "0" + M;
    S = date.getSeconds()
    if (S < 10)
        S = "0" + S;
    texte += D + '/' + MO + '/' + A + ' ' + H + ':' + M + ':' + S;
    document.getElementById('date').innerHTML = texte;
}
setInterval('rafraichir()', 1000);