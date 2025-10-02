var renderObject = function (value) {
    if (!Ext.isEmpty(value)) {
        if (value.AsString)
            return value.AsString;
    }

    return '';
}

var renderIcon = function (value) {
    var tpl = '<span class="glyphicon {0}" aria-hidden="true"></span>';    

    var icon_url = Renderers.getBootStrapIcons(value);    

    return Ext.String.format(tpl, icon_url);
}



var iconRenderer = function (value, metaData, record) {
    metaData.css = Renderers.getIcons(value);
}


var Renderers =
{
    getIcons: function (value) {
        if (value === 0) {
            return 'icon-delete';//Ext.net.ResourceMgr.getIcon('Icon.BulletCross');//'glyphicon-remove';//Ext.net.ResourceMgr.getIconUrl('Icon.BulletCross');
        }
        else if (value == 1) {
            return 'action_approve_02';// 'glyphicon-ok';//Ext.net.ResourceMgr.getIconUrl('Icon.Tick');
        }
        else if (value == 2) {
            return 'action_new_02';//'glyphicon-file';//Ext.net.ResourceMgr.getIconUrl('Icon.Add'); // New  glyphicon glyphicon-ok
        }
    },
    getBootStrapIcons: function (value) {
        if (value === 0) {
            return 'glyphicon-remove red'; // glyphicon glyphicon-remove red
        }
        else if (value == 1) {
            return 'glyphicon-ok green';
        }
        else if (value == 2) {
            return 'glyphicon-file blue'; // glyphicon glyphicon-file blue
        }
    }
}

var renderDateLong = function (value) {
    if (!Ext.isEmpty(value)) {
        return Ext.util.Format.date(value, 'd/m/Y H:i:s');
    }

    return '';
};

var SetPieChartLabelInnerCanevas = function (sprite, record, attributes, index, store) {

    var ind = index.store.findExact('Libelle', sprite); /* the field containing the current label*/
    var val = index.store.getAt(ind).data.Total;
    var storeLength = index.store.data.length;
    var total = 0;
    for (var i = 0; i < storeLength; i++) {
        total += index.store.getAt(i).data.Total;
    }
    
    var PrCent = Math.round(val / total * 100) + '%';
    /*console.log(index);*/
    
    return PrCent; /* ToString to show int Value in Canevas */

};
