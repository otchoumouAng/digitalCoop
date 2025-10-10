var FormatAmount = function (val) {
    val = val.toString();
    val = val.replace(/ /g, '');
    val = val.replace(/\B(?=(\d{3})+(?!\d))/g, " ");
    return val;
};