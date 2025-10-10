var LoadStore = function (storeId) {
    if (App.storeId.getStore().isLoaded() == false) {
        App.storeId.getStore().load();
    }

};