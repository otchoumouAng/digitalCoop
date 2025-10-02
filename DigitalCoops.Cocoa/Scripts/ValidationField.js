              /* Fonction qui va tester les champs (cwaTexField, cwa ComboBox et cwaDateField) réquis  de nos formulaires
*  afin d'être sûrs qu'ils ont tous été remplis
*  @parametre : l'id absolue de notre fenètre (window) i.e frmConnaissement_Detail_winConnaissement pour la fenetre détail dans Camion à Quai.
*  @return : booléen
*/
var isFormValid = function (winId) {    
    try {
        /* ici on récupère les différents composants cwaTexField, cwa ComboBox et cwaDateField qui 
        sont sur le formulaire ( i.e frmConnaissement_Detail_winConnaissement pour la fenetre détail dans Camion à Quai).
        dans le cas où on en trouve pas on retourne des tableaux  vides.
        */

        var TextFieldArray = Ext.ComponentQuery.query('window[id="' + winId + '"] textfield') || [];
        var ComboBoxArray = Ext.ComponentQuery.query('window[id="' + winId + '"] combobox') || [];
        var NumberFieldArray = Ext.ComponentQuery.query('window[id="' + winId + '"] numberfield') || [];
        var DateFieldArray = Ext.ComponentQuery.query('window[id="' + winId + '"] datefield') || [];
        var TextAreaArray = Ext.ComponentQuery.query('window[id="' + winId + '"] textarea') || [];

        /*Ensuite nous concatenons tous les tableaux obtenus obtenus
        */
        var allCustomComponentsOnWindowArray = TextFieldArray.concat(ComboBoxArray);
        allCustomComponentsOnWindowArray = allCustomComponentsOnWindowArray.concat(DateFieldArray);
        allCustomComponentsOnWindowArray = allCustomComponentsOnWindowArray.concat(TextAreaArray);
        allCustomComponentsOnWindowArray = allCustomComponentsOnWindowArray.concat(NumberFieldArray);

        /*on va tester si nos objets présents dans le tableau contienne une propriété isrequired, si cela est juste est que cette 
        * cette propriété est à true et enfin si cette objet contient une valeur.
        */
        if (allCustomComponentsOnWindowArray.length > 0) {

            var flag = Ext.each(allCustomComponentsOnWindowArray,
                            function (item, index, objectItSelf)
                            {                                
                                /*console.log(item.value + ' ' + item.name + ' ' + item.rawValue);*/
                                /*if (item.hasOwnProperty('allowBlank') && (item.value == null || item.value == '') && (item.allowBlank == false))*/
                                if (item.hasOwnProperty('allowBlank') && (item.rawValue == null || item.rawValue == '') && (item.allowBlank == false)) {
                                    /*flag = false;
                                    /*alert('proprieté false')*/
                                    /*console.log(flag);*/
                                    return false;
                                }
                            }
                     );

            return (flag === true);
        }
        else
            /*alert('proprieté ok')*/
            return true;


    } catch (e) {
        alert('Error validation js');
        /*Ext.Msg.show({ title: 'Erreur : isFormValid', msg: e.toString() });*/
    }
}      

