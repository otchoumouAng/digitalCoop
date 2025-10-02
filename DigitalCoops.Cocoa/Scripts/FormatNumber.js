function formatNumber(amount) {

    var pattern = "0,000";
    /*switch (thousandseparator) {
        case ".":
            pattern = "0.000";
            break;
        case ",":
            pattern = "0,000";
            break;
        case " ":
            pattern = "0 000";
            break;
        default: pattern = "0 000";
    };
    */
    /*  Save the separator*/
    var thousandSep = Ext.util.Format.thousandSeparator;   
    Ext.util.Format.thousandSeparator = " ";    
    var decimalSep = Ext.util.Format.decimalSeparator;
    /*Ext.util.Format.decimalSeparator = decimalseparator;*/

    var formatted = Ext.util.Format.number(amount, pattern);    
    // restore  separator
    Ext.util.Format.thousandSeparator = thousandSep;
    Ext.util.Format.decimalSeparator = decimalSep;

    return formatted;
};

function formatNumberE(amount) {

    var pattern = "0,000";
    /*switch (thousandseparator) {
        case ".":
            pattern = "0.000";
            break;
        case ",":
            pattern = "0,000";
            break;
        case " ":
            pattern = "0 000";
            break;
        default: pattern = "0 000";
    };
    */
    /*  Save the separator*/
    var thousandSep = Ext.util.Format.thousandSeparator;
    Ext.util.Format.thousandSeparator = " ";
    //var decimalSep = Ext.util.Format.decimalSeparator;
    /*Ext.util.Format.decimalSeparator = decimalseparator;*/

    var formatted = Ext.util.Format.number(amount, pattern);
    // restore  separator
    Ext.util.Format.thousandSeparator = thousandSep;
    //Ext.util.Format.decimalSeparator = decimalSep;

    return formatted;
};


var unformatNumber = function (amount) {

    amount = amount.replace(/\s/g, '');
    return amount;
    /*if (!thousandseparator || !thousandseparator.trim().length) {
        amount = amount.replace(/\s/g, '');
        return amount;
    }*/

    /*switch (thousandseparator) {
        case ".":
            amount = amount.replace(/./g, '');
            return amount;
            break;
        case ",":
            amount = amount.replace(/,/g, '');
            return amount;
            break;                
        default: return amount;
    }  */
};
