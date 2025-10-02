/* 
   Created by AA on 09/07/2017
   Description : function used to add new tabs to then main tabPanel of the viewPort
*/
function addTab(tabPanel, id, url, menuItem, mtitle,tabPanelFor) {
    var tab = tabPanel.getComponent(id);

    if (!tab) {
        tab = tabPanel.add({
            id: id,
            title: mtitle,
            closable: true,
            menuItem: menuItem,
            loader: {
                url: url,                
                renderer: 'frame',
                loadMask: {
                    showMask: true,
                    msg: 'Loading ' + mtitle + ' ' + tabPanelFor + ' ...'
                }
            }
        });

    }

    tabPanel.setActiveTab(tab);
}