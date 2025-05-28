/**
*   LISTE DES  CONSTANTES 
*/

var UPDATE = 'Update';
var ADD_NEW = 'AddNew';
var CONSULT = 'Consult';
var DEFAULT = 'Default';
var ENTIER = 'Entier';
var DECIMAL = 'Decimal';
var ALPHABETIQUE = 'Alphabetique';

/********************************************************/


/**
*   DEFINITION DE LA CLASSE ctlTextField pour JS
*   cette fonction est l'alter ego de la fonction c sharp
*
*
*********************************************************/
Ext.define('ctl.ctlTextField',
{
    extend: 'Ext.form.field.Text',

    alias: 'widget.ctlTextField',

    maxLength: 50,

    labelWidth: 100,

    cFormatDecimal: '###,###,###,##0.######',

    thousandseparator: ' ',

    decimalseparator: '.',

    isBold: false,

    setIsBold: function (value) {
        this.isBold = value;
    },

    getIsBold: function () {
        return this.isBold;
    },

    getcFormatDecimal: function () { return this.cFormatDecimal; },

    setcFormatDecimal: function (value) { this.cFormatDecimal = value; },

    execmode: 'Default',

    getExecmode: function () { return this.execmode; },

    setExecmode: function (value) {
        this.execmode = value;
        this.setReadOnly(value == 'Consult');
        this.refreshBackColor();

        if (this.execmode == 'Consult') {

            Ext.util.Format.thousandSeparator = this.thousandseparator;
            Ext.util.Format.decimalSeparator = this.decimalseparator;
            formatIfNumeric(this, this.getcFormatDecimal());
        }
    },

    typedevaleur: 'Default',

    getTypedevaleur: function () { return this.typedevaleur; },

    setTypedevaleur: function (value) { this.typedevaleur = value; },

    ispositive: true,

    getPositive: function () { return this.ispositive; },

    setPositive: function (value) { this.ispositive = value; },

    isrequired: false,

    getIsrequired: function () { return this.isrequired; },

    setIsrequired: function (value) {

        this.isrequired = value;

        if (this.isrequired)
            this.setRequired();
        else
            this.setNotRequired();

        //this.refreshBackColor();
    },

    setRequired: function () {

        this.allowBlank = true;
        this.indicatorCls = "IndicatorRedStar";
        this.indicatorText = "*";

    },


    setNotRequired: function () {
        this.AllowBlank = false;
        this.IndicatorCls = '';
        this.IndicatorText = '';
    },

    isfilled: false,

    getIsfilled: function () {
        if (this.value)
            return true;
        else
            return false;
    },

    maxvalue: 10000000000000000000,

    minvalue: -10000000000000000000,

    nbredecimal: 0,

    showmsgshenoccurs: false,

    isonerror: false,

    getIsonerror: function () { return this.isonerror; },

    setIsonerror: function (value) { this.isonerror = value; },

    canFormat: true,

    getCanFormat: function () { return this.canFormat; },

    setCanFormat: function (value) { this.canFormat = value; },

    isNumber: function () {
        return ((this.getTypedevaleur() == ENTIER) || (this.getTypedevaleur() == DECIMAL));
    },
    getDomObjet: function () { return this.inputEl; },
    isClassExists: function (value) {
        try {
            var dom = this.getDomObjet();

            var myRegExp = new RegExp(value);

            if (this.inputEl)
                return myRegExp.test(this.inputEl.dom.className);
            else
                return myRegExp.test(this.fieldCls);
        }
        catch (err) {
            Ext.Msg.alert('Erreur', 'ctlTextField : isClassExists : ' + err);
        }
    },
    ajouterClasse: function (value) {
        try {
            var dom = this.getDomObjet();

            if (this.inputEl) {
                if (!this.isClassExists(value)) {
                    this.inputEl.addCls(value);
                    // console.log('2. ajouter ' + value + ' dans ' + this.inputEl.dom.className);
                }
            }
            else {
                if (!this.isClassExists(value)) {
                    this.fieldCls += ' ' + value;
                    //                    console.log('2. ajouter ' + value);
                }
            }
        }
        catch (err) {
            Ext.Msg.alert('Erreur', 'ctlTextField : ajouterClasse : ' + err);
        }
    },
    retirerClasse: function (value) {
        try {
            var dom = this.getDomObjet();

            if (this.inputEl) {
                if (this.isClassExists(value)) {
                    this.inputEl.removeCls(value);
                    //                    console.log('1. retirer ' + value + ' dans ' + this.inputEl.dom.className);
                }
            }
            else {
                if (this.isClassExists(value)) {
                    this.fieldCls = value;
                }
                //                console.log('1. retirer ' + value);
            }

        }
        catch (err) {
            Ext.Msg.alert('Erreur', 'ctlTextField : retirerClasse : ' + err);
        }
    },
    retirerClassesCSS: function () {
        this.retirerClasse('Blank');
        this.retirerClasse('RequiredErr');
        this.retirerClasse('BlankConsult');
        this.retirerClasse('Required');

    }
    ,
    refreshBackColor: function () {
        try {
            if (this.getExecmode() != CONSULT) {
                if (!this.getIsfilled() && this.getIsrequired()) {
                    //mettre en champ requis                   
                    this.retirerClassesCSS();
                    this.ajouterClasse('Required');

                }
                else if (this.isonerror) {
                    //mettre en champ d'erreur
                    this.retirerClassesCSS();
                    this.ajouterClasse('RequiredErr');
                }
                else {
                    //mettre en champ par defaut
                    this.retirerClassesCSS();
                    this.ajouterClasse('Blank');
                    //console.log('Normal : ' + this.id);
                }
            }
            else {
                //mettre en champ de consultation
                this.retirerClassesCSS();
                this.ajouterClasse('BlankConsult');
                //console.log('Consultation : ' + this.id);
            }

            if (this.getIsBold()) {

                this.ajouterClasse('isBold');
            }
        }
        catch (err) {
            Ext.Msg.alert('Erreur', 'ctlTextField : refreshBackColor : ' + err);
        }
    },

    readValue: function () {
        try {
            Ext.util.Format.thousandSeparator = this.thousandseparator;
            Ext.util.Format.decimalSeparator = this.decimalseparator;

            if (this.isNumber() && this.getCanFormat()) {
                return this.unFormatTextField();
            }
            else
                return this.getValue();
        }
        catch (err) {
            Ext.Msg.alert('Erreur', 'ctlTextField : getValue : ' + err);
        }
    },
    unFormatTextField: function () {

        //        if (!this)
        //            throw "You must give a parameter to unFormatTextField";

        var str_value = this.value;

        try {
            if (str_value && this.isNumber() && this.getCanFormat()) {
                if (typeof (str_value) != 'undefined') {
                    var pEntiere, pDecimal;


                    //split array_value in 2 parts integer and decimal parts
                    var array_value = str_value.split(this.decimalseparator);


                    if (array_value.length > 1) {
                        pEntiere = array_value[0]; //retrieve integer part
                        pDecimal = array_value[1]; //retrieve decimal part
                    }
                    else {
                        pEntiere = str_value; //retrieve integer part
                        pDecimal = 'undefined';
                    }


                    var array_value_entier = pEntiere.split(this.thousandseparator); //split 'integer' part in 2  parts


                    if (array_value_entier.length > 1) {
                        if (pDecimal != 'undefined')
                            return array_value_entier.join('').toString() + '.' + pDecimal;
                        else
                            return array_value_entier.join('').toString();
                    }
                    else {

                        if (pDecimal != 'undefined')
                            return pEntiere + '.' + pDecimal;
                        else
                            return pEntiere;
                    }
                }
                else
                    return false;
            }
        } catch (err) {
            Ext.Msg.alert('infos', 'ctlTextField.js : unFormatTextField :' + err);
        }
    }
    ,
    unFormatTextFieldWithUserDecimalSeparatorAndThousandSeparator: function () {

        //        if (!this)
        //            throw "You must give a parameter to unFormatTextField";

        var str_value = this.value;

        try {
            if (str_value && this.isNumber() && this.getCanFormat()) {
                if (typeof (str_value) != 'undefined') {
                    var pEntiere, pDecimal;


                    //split array_value in 2 parts integer and decimal parts
                    var array_value = str_value.split(this.decimalseparator);


                    if (array_value.length > 1) {
                        pEntiere = array_value[0]; //retrieve integer part
                        pDecimal = array_value[1]; //retrieve decimal part
                    }
                    else {
                        pEntiere = str_value; //retrieve integer part
                        pDecimal = 'undefined';
                    }


                    var array_value_entier = pEntiere.split(this.thousandseparator); //split 'integer' part in 2  parts


                    if (array_value_entier.length > 1) {
                        if (pDecimal != 'undefined')
                            return array_value_entier.join('').toString() + this.decimalseparator + pDecimal;
                        else
                            return array_value_entier.join('').toString();
                    }
                    else {

                        if (pDecimal != 'undefined')
                            return pEntiere + this.decimalseparator + pDecimal;
                        else
                            return pEntiere;
                    }
                }
                else
                    return false;
            }
        } catch (err) {
            Ext.Msg.alert('infos', 'ctlTextField.js : unFormatTextField :' + err);
        }
    },
    onChange: function () {

        try {

            this.refreshBackColor();

            if (this.getExecmode() == CONSULT) {

                Ext.util.Format.thousandSeparator = this.thousandseparator;
                Ext.util.Format.decimalSeparator = this.decimalseparator;
                formatIfNumeric(this, this.getcFormatDecimal());
            }
        }
        catch (err) {
            Ext.Msg.alert('Erreur', 'ctlTextField : onChange : ' + err);
        }
    }
})

/*
*   LISTE DES GESTIONNAIRES D'EVENEMENTS
*/

function TextField_Enter(textField, mObjet) {
    try {
        if (textField.getCanFormat() && textField.isNumber()) {
            if (textField.value) {
                if (textField.getExecmode() != CONSULT) {
                    var mJson = Ext.decode(mObjet);
                    Ext.util.Format.thousandSeparator = mJson.thousandSeparator;
                    Ext.util.Format.decimalSeparator = mJson.decimalSeparator;
                    //UnFormatTextField(textField, mObjet);
                    textField.setValue(textField.unFormatTextFieldWithUserDecimalSeparatorAndThousandSeparator());
                }
            }
        }
    }
    catch (err) {
        Ext.Msg.alert('Erreur', 'ctlTextField : TextField_Enter : ' + err);
    }
}


function TextField_Leave(textField, mObjet) {

    try {

        if (textField.getExecmode() != CONSULT) {
            if (mObjet) {
                var mJson = Ext.decode(mObjet);
                Ext.util.Format.thousandSeparator = mJson.thousandSeparator;
                Ext.util.Format.decimalSeparator = mJson.decimalSeparator;
                formatIfNumeric(textField, textField.getcFormatDecimal());
            }
        }
    }
    catch (err) {
        Ext.Msg.alert('Erreur', 'ctlTextField : TextField_Enter : ' + err);
    }
}

/*
*   LISTE DES FONCTIONS 
*/


function isNegative(value) {
    if (value) {
        var flag = parseFloat(value) < 0;
        return flag;
    }
    else
        return false;
}

/* A ne plus utiliser */
function replaceCommaByPoint(value) {
    if (value) {
        //si la valeur brute contient des virgules
        //on les retire car cela empeche le formatage avec la fonction de formatage Ext.NET
        var containsComma = /,/.test(value);
        if (containsComma)
            value = value.replace(',', '.');
    }
    return value;
}



function replaceDecimalSeparatorByPointAndThousandSeparatorByEmpty(value) {


    try {

        if (value) {

            var pEntiere, pDecimal;


            //split array_value in 2 parts integer and decimal parts
            var array_value = value.split(Ext.util.Format.decimalSeparator);

            if (array_value) {
                pEntiere = array_value[0]; //retrieve integer part
                pDecimal = array_value[1]; //retrieve decimal part
            }
            else {
                pEntiere = str_value; //retrieve integer part
                pDecimal = undefined;
            }


            var array_value_entier = pEntiere.split(Ext.util.Format.thousandSeparator); //split 'integer' part in 2  parts


            if (array_value_entier) {
                if (pDecimal)
                    return array_value_entier.join('').toString() + '.' + pDecimal;
                else
                    return array_value_entier.join('').toString();
            }
            else {

                if (pDecimal)
                    return pEntiere + '.' + pDecimal;
                else
                    return pEntiere;
            }

        }
    }
    catch (err) {
        Ext.Msg.alert('infos', 'ctlCommonFunctions.js : replaceDecimalSeparatorByPointAndThousandSeparatorByEmpty :' + err);
    }
    return value;
}


function formatIfNumeric(textField, format) {
    try {
        if (textField.getCanFormat() && textField.isNumber()) {
            if (textField.value) {
                var valeur = textField.value;
                //valeur = replaceCommaByPoint(valeur);
                valeur = replaceDecimalSeparatorByPointAndThousandSeparatorByEmpty(valeur);
                formatNumericField(textField, valeur, format);
                return true;
            }
            else
                return false;
        }
        else
            return false;
    }
    catch (err) {
        Ext.Msg.alert('Erreur', 'ctlTextField : formatIfNumeric : ' + err);
    }
}


// Fonction assurant le formatage 
// du texte présent dans le textField


function formatNumericField(textField, value, format) {
    try {



        if (value) {
            // si la valeur est négative
            // car la fonction de  formatage Ext.NET ne sait que formater les nombres positifs
            var estNegatif = isNegative(value);
            if (estNegatif) {
                // je retire le signe pour formater le chiffre
                value = value.substr(1, value.length - 1);

            }
            //je formate                    
            var chaine = Ext.util.Format.number(value, format);


            //if value formatting failed that means, value string 
            //is not a numeric value only
            //so we keep his raw value
            if (!chaine) {
                chaine = value;

                //                Ext.Msg.alert('infos', 'data format is wrong.' + textField.id + ' ' + value);
            }
            else {

                // for french culture, value is ended with a comma due to ext.net format bug
                //so we remove it by adding empty string at his place
                var endByComma = /,$/.test(chaine);
                if (endByComma) {
                    chaine = chaine.substr(0, chaine.length - 1);
                    chaine = Ext.String.trim(chaine);
                }

                if (estNegatif)
                    textField.setValue('-' + chaine);
                else
                    textField.setValue(chaine);
            }
        }
        else {
            return textField;
            Ext.Msg.alert('infos', 'ctlTextField.js : formatNumericField :vous devez donner un champ textField ou ctlTextField à cette fonction.');
        }


    }
    catch (err) {
        Ext.Msg.alert('infos', 'ctlTextField.js : formatNumericField :' + err);
    }
}

// fonctionassurant le deformatage  du text présent 
// dans le textField

function UnFormatTextField(textField, mObjet) {

    if (!textField)
        throw "You must give a parameter to fnUnFormatNumberTextField";

    var str_value = textField.value;

    try {
        if (str_value && textField.isNumber() && textField.getCanFormat()) {
            if (typeof (str_value) != 'undefined') {
                var pEntiere, pDecimal;


                //split array_value in 2 parts integer and decimal parts
                var array_value = str_value.split(Ext.util.Format.decimalSeparator);

                if (array_value) {
                    pEntiere = array_value[0]; //retrieve integer part
                    pDecimal = array_value[1]; //retrieve decimal part
                }
                else {
                    pEntiere = str_value; //retrieve integer part
                    pDecimal = undefined;
                }


                var array_value_entier = pEntiere.split(Ext.util.Format.thousandSeparator); //split 'integer' part in 2  parts


                if (array_value_entier) {
                    if (pDecimal)
                        textField.setValue(array_value_entier.join('').toString() + Ext.util.Format.decimalSeparator + pDecimal);
                    else
                        textField.setValue(array_value_entier.join('').toString());
                }
                else {

                    if (pDecimal)
                        textField.setValue(pEntiere + Ext.util.Format.decimalSeparator + pDecimal);
                    else
                        textField.setValue(pEntiere);
                }
            }
        }
    } catch (err) {
        Ext.Msg.alert('infos', 'ctlTextField.js : fnUnFormatNumber(textField , mObjet) :' + err);
    }
}


//fonction pour modifier 
// les propriétés de notre textField depuis du code c sharp
// vu que la modification de ses propriétés est impossible en utilisant le code
// normalement

function alterProperty(strIdTextField, property_name_js, property_value) {

    try {


        if (strIdTextField) {
            var monTextField = Ext.getCmp(strIdTextField);

            if (property_name_js) {
                if (monTextField) {
                    switch (property_name_js) {

                        case 'isrequired':
                            monTextField.setIsrequired(property_value);
                            break;

                        case 'typedevaleur':
                            monTextField.setTypedevaleur(property_value);
                            break;

                        case 'canFormat':
                            monTextField.setCanFormat(property_value);
                            break;

                        case 'execmode':
                            //                            console.log('ctlTexField.js : setExecmode :' + property_value);
                            monTextField.setExecmode(property_value);
                            break;
                        default:
                            //                            Ext.Msg.alert("Infos", 'ctlTextField.js : alterProperty : nom de propriété inconnue.');
                            //                            console.log('ctlTextField.js : alterProperty : Nom de  propriété inconnue');
                            break;
                    }
                }
                //                else
                //                    console.log('ctlTextField.js : alterProperty : reférence du textField ' + strIdTextField + ' introuvable.');
            }
            //            else
            //                console.log('ctlTextField.js : alterProperty : reférence du textField ' + strIdTextField + ' introuvable.');
        }
    }
    catch (err) {
        Ext.Msg.alert("Infos", 'ctlTextField.js : alterProperty : ' + err.Message);
    }
}

