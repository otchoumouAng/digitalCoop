/*Filtre les types de mouvement en fonction du sens
 */
var SetTypeFilterValues = function (records, comboFils, comboParent) {
    var _comboParent = Ext.getCmp(comboParent);    
    var _comboFils = Ext.getCmp(comboFils);
    if ((records.length > 0) && _comboParent.value != -1) {
        _comboFils.store.remove(records[0]);
        return (records);
    } else {
        _comboFils.setValue(-1);
        return records;
    }
};

/*Ajoute et selectionne la nouvelle ligne du gridpanel quand
 la limite de page est 100
 */
var SelectNewRecordAddedOnGrid = function (record, grid) {
    var grid = Ext.getCmp(grid);
    var rec = grid.store.getById(record[0].data.ID);
    grid.store.loadPage(grid.store.findPage(rec), {
        callback: function () {
            grid.getSelectionModel().select(rec);
        }
    });
};

var getRowsForExcel = function (Coluns, Vals) {

    //1. Nom de colonnesExt.String.format(tpl, icon_url);
    /*var visibleCols = #{grpListe}.columnManager.getColumns();*/

    var cols = [];
    Coluns.forEach(function (item) {
        var Entete = item.text;
        Entete = Entete.replace(/\s/g, '');
        if (item.dataIndex != 'mIcon' && (item.dataIndex != 'ID' || item.dataIndex != 'IDUtilisateur' || item.dataIndex != 'id') && item.hidden === false)
            cols.push({ dataIndex: item.dataIndex, text: Entete });
    });

    //2. get rows
    /*var rows = #{grpListe}.getRowsValues({visibleOnly:true});*/
    if (Vals.length <= 0)
        return '';

    Vals.forEach(function (item) {
        delete item.ID;
        delete item.mIcon;
        if (item.IDUtilisateur)
            delete item.IDUtilisateur;
        if (item.id)
            delete item.id;
    });

    //3. map properties
    Vals.forEach(

        function (rowItem) {

            var tempValue;
            cols.forEach(function (colItem) {
                var dataIndex = colItem.dataIndex;
                var text = colItem.text;

                // save dataindex value
                tempValue = rowItem[dataIndex];
                if (tempValue.toString().indexOf(String.fromCharCode(160)) !== -1) {
                    tempValue = tempValue.replace(/\s+/g, "");
                    console.log(tempValue);
                }
                // delete dataindex property
                delete rowItem[dataIndex];

                // create new property "text" with right value saved
                rowItem[text] = tempValue;
            });
        });

    return Ext.encode(Vals);
};

var getColsForExcel = function (Coluns) {

    //1. Nom de colonnesExt.String.format(tpl, icon_url);
    /*var visibleCols = #{grpListe}.columnManager.getColumns();*/

    var cols = [];
    Coluns.forEach(function (item) {
        var Entete = item.text;
        Entete = Entete.replace(/\s/g, '');
        if (item.dataIndex != 'mIcon' && (item.dataIndex != 'ID' || item.dataIndex != 'IDUtilisateur' || item.dataIndex != 'id') && item.hidden === false)
            cols.push({ dataIndex: item.dataIndex, text: Entete });
    });

    return Ext.encode(cols);
};

/*
* fonction pour récuperer un composant à 
* partir de type et de son identifiant
*
*/
var SetMsgBoxConfirm = function (config,value) {
    /*console.log(config);
    console.log(value);*/
    if (value == false) {
        config.confirmation.message = 'Do you want to Disable the selected item ? ';
    } else {
        config.confirmation.message = 'Do you want to Enable the selected item ? ';
    }
    return config;
};




function getCmp(type_component, id_Component) {
    try {
        var cmp = Ext.ComponentQuery.query(type_component + '[id$=' + id_Component + ']')[0];
        if (cmp)
            return cmp;
        else
            Ext.Msg.alert('Error', 'WebTest : le composant ' + id_Component + ' est introuvable. veuillez bien vérifier le nom du composant.');
    }
    catch (err) {
        Ext.Msg.alert('Error', 'WebTest : getCmp : ' + err)
    }
}


/*
* fonction pour tester
* si une valeur est numérique
*
*/

function isNumeric(v) {
    //    return v.length > 0 && !isNaN(v) && v.search(/[A-Z]|[#]/ig) == -1;
    return Ext.isNumeric(v);
};


/*
* fonction pour modifier la propriété ExecMode
* d'un composant
*/
function setExecMode(strIdDateField, value) {

    try {
        if (strIdDateField) {
            var monDateField = Ext.getCmp(strIdDateField);

            if (monDateField)
                monDateField.setExecMode(value);
            else
                X.Msg.alert("Infos", 'DateField.js : setExecMode : reférence du textField ' + strIdDateField + ' introuvable.');

        }
    }
    catch (err) {
        X.Msg.alert("Infos", 'DateField.js : setExecMode : ' + err.Message);
    }
}


/*
* fonction pour modifier la propriété ExecMode
* d'un composant
*/

function putFormInMode(execMode) {
    try {

        console.log('top function');
        var mesTextFields = Ext.ComponentQuery.query('TextField');
        var monDateField = Ext.ComponentQuery.query('DateField');
        console.log('before loop');
        for (var index = 0; index < mesTextFields.length; index++) {
            var textFieldItem = (function () { return mesTextFields[index]; })();

            textFieldItem.setExecMode(execMode);
        }
        console.log('after loop');
        App.frmFournisseur_Detail_dtfDateCreation.setExecMode(execMode);

        if (execMode == 'Consult') {
            App.frmFournisseur_Detail_chkFixerSeuilFinancement.setReadOnly(true);
            App.frmFournisseur_Detail_btnOk.hidden = true;
        }
        else {
            App.frmFournisseur_Detail_chkFixerSeuilFinancement.setReadOnly(false);
            App.frmFournisseur_Detail_btnOk.hidden = false;
        }
    }
    catch (err) {
        Ext.Msg.alert('Error', 'putFormInMode : ' + err);
    }
}

/*
Convertit un nombre en entier en arrondissant à l'ordre 0
Ajouté par Mz le 27 Jan 2015
*/
function RoundInt(v) {
    try {
        if ((parseFloat(v) - parseInt(v)) > 0.5) {
            return parseInt(v) + 1;
        }
        else {
            return parseInt(v);
        }
    } catch (err) {
        return 0;
    }
}


/*
Fonction pour tester si un objet est une date
Rajouté par Mz le 28/01/2015
(A Tester)
*/
function IsDate(myDate) {
    return myDate.constructor.toString().indexOf("Date") > -1;
}

/*
Fonction pour convertir un objet en un type Date
Rajouté par Mz le 28/01/2015
(A tester)
*/
function ToDate(v) {
    var parts = v.split("/");
    return new Date(parts[2], parts[1] - 1, parts[0]);
}



/* Fonction qui va tester les champs (TexField,  ComboBox et DateField) réquis  de nos formulaires
*  afin d'être sûrs qu'ils ont tous été remplis
*  @parametre : l'id absolue de notre fenètre (window) i.e frmConnaissement_Detail_winConnaissement pour la fenetre détail dans Camion à Quai.
*  @return : booléen
*/
var isFormValid = function (winId) {

    try {
        /* ici on récupère les différents composants TexField,  ComboBox et DateField qui 
        sont sur le formulaire ( i.e frmConnaissement_Detail_winConnaissement pour la fenetre détail dans Camion à Quai).
        dans le cas où on en trouve pas on retourne des tableaux  vides.
        */

        var TextFieldArray = Ext.ComponentQuery.query('window[id="' + winId + '"] TextField') || [];
        var ComboBoxArray = Ext.ComponentQuery.query('window[id="' + winId + '"] ComboBox') || [];
        var DateFieldArray = Ext.ComponentQuery.query('window[id="' + winId + '"] DateField') || [];
        var TextAreaArray = Ext.ComponentQuery.query('window[id="' + winId + '"] TextArea') || [];

        /*Ensuite nous concatenons tous les tableaux obtenus obtenus
        */
        var allCustomComponentsOnWindowArray = TextFieldArray.concat(ComboBoxArray);
        allCustomComponentsOnWindowArray = allCustomComponentsOnWindowArray.concat(DateFieldArray);
        allCustomComponentsOnWindowArray = allCustomComponentsOnWindowArray.concat(TextAreaArray);

        /*on va tester si nos objets présents dans le tableau contienne une propriété isrequired, si cela est juste est que cette 
        * cette propriété est à true et enfin si cette objet contient une valeur.
        */
        if (allCustomComponentsOnWindowArray.length > 0) {

            var flag = Ext.each(allCustomComponentsOnWindowArray,
                            function (item, index, objectItSelf) {
                                if (item.hasOwnProperty('isrequired') && item.isrequired && !item.value) {
                                    flag = false;
                                    return flag;
                                }
                            }
                     );

            return (flag === true);
        }
        else
            return true;


    } catch (e) {
        Ext.Msg.show({ title: 'Error : isFormValid', msg: e.toString() });
    }
}

var getCustomizedFormComponents = function (winId) {
    try {
        /* ici on récupère les différents composants TexField,  ComboBox, TextArea et DateField qui 
        sont sur le formulaire ( i.e frmConnaissement_Detail_winConnaissement pour la fenetre détail dans Camion à Quai).
        dans le cas où on en trouve pas on retourne des tableaux  vides.
        */

        var TextFieldArray = Ext.ComponentQuery.query('window[id="' + winId + '"] TextField') || [];
        var ComboBoxArray = Ext.ComponentQuery.query('window[id="' + winId + '"] ComboBox') || [];
        var DateFieldArray = Ext.ComponentQuery.query('window[id="' + winId + '"] DateField') || [];
        var TextAreaArray = Ext.ComponentQuery.query('window[id="' + winId + '"] TextArea') || [];

        /*Ensuite nous concatenons tous les tableaux obtenus obtenus
        */
        var allCustomComponentsOnWindowArray = TextFieldArray.concat(ComboBoxArray);
        allCustomComponentsOnWindowArray = allCustomComponentsOnWindowArray.concat(DateFieldArray);
        allCustomComponentsOnWindowArray = allCustomComponentsOnWindowArray.concat(TextAreaArray);


        return allCustomComponentsOnWindowArray;

    } catch (e) {
        Ext.Msg.show({ title: 'Error : getCustomizedFormComponents', msg: e.toString() });
    }
}


/* Fonction qui va mettre les champs (TexField,  ComboBox, TextArea et DateField) de nos formulaires
*  dans un mode d'execution donnée.
*  @parametre : l'id absolue de notre fenètre (window) i.e 'frmConnaissement_Detail_winConnaissement' pour la fenetre détail dans Camion à Quai.
*  @parametre : le mode d'execution {'AddNew', 'Consult' , 'Approve', 'Update'}
*  
*/
var setExecMode = function (winId, execMode) {

    try {
        /* ici on récupère les différents composants TexField,  ComboBox et DateField qui 
        sont sur le formulaire ( i.e frmConnaissement_Detail_winConnaissement pour la fenetre détail dans Camion à Quai).
        dans le cas où on en trouve pas on retourne des tableaux  vides.
        */
        var allCustomComponentsOnWindowArray = getCustomizedFormComponents(winId);
       

        /*on va maintenant appliquer le mode d'execution pour chacun des élements présent sur le formulaire
        1   -   textField
        2   -   ComboBox
        3   -   DataField
        4   -   TextArea
        */
        if (allCustomComponentsOnWindowArray.length > 0) {

            var flag = Ext.each(allCustomComponentsOnWindowArray,
                            function (item, index, objectItSelf) {
                                item.setExecmode(execMode);
                            }
                     );
        }

    } catch (e) {
        Ext.Msg.show({ title: 'Error : setExecMode', msg: e.toString() });
    }
}


/* Fonction qui va reinitialiser les champs (TexField,  ComboBox, TextArea et DateField) de nos formulaires
*  @parametre : l'id absolue de notre fenètre (window) i.e 'frmConnaissement_Detail_winConnaissement' pour la fenetre détail dans Camion à Quai.*  
*  
*/
var resetControls = function (winId) {

    try {
        /* ici on récupère les différents composants TexField,  ComboBox et DateField qui 
        sont sur le formulaire ( i.e frmConnaissement_Detail_winConnaissement pour la fenetre détail dans Camion à Quai).
        dans le cas où on en trouve pas on retourne des tableaux  vides.
        */
        var allCustomComponentsOnWindowArray = getCustomizedFormComponents(winId);
       

        /*on va maintenant appliquer la réinitialisation à chacun des élements présent sur le formulaire
        1   -   textField
        2   -   ComboBox
        3   -   DataField
        4   -   TextArea
        */

        if (allCustomComponentsOnWindowArray.length > 0) {

            var flag = Ext.each(allCustomComponentsOnWindowArray,
                            function (item, index, objectItSelf) {
                                item.reset();
                            }
                     );
        }
      

    } catch (e) {
                        Ext.Msg.show({ title: 'Error : resetControls', msg: e.toString() });
    }
}









// fonctionassurant le deformatage  du text présent 
// dans le textField

function UnFormatTextField(textField) {

    if (!textField)
        throw "You must give a parameter to UnFormatNumberTextField";

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
                        return array_value_entier.join('').toString() + Ext.util.Format.decimalSeparator + pDecimal;
                    else
                        return array_value_entier.join('').toString();
                }
                else {

                    if (pDecimal)
                        return pEntiere + Ext.util.Format.decimalSeparator + pDecimal;
                    else
                        return pEntiere;
                }
            }
        }
    } catch (err) {
        Ext.Msg.alert('infos', 'TextField.js : UnFormatNumber(textField , mObjet) :' + err);
    }
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
        Ext.Msg.alert('infos', 'CommonFunctions.js : replaceDecimalSeparatorByPointAndThousandSeparatorByEmpty :' + err);
    }
    return value;
}
   