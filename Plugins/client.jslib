mergeInto(LibraryManager.library, {
    GetCatalog: function () {
        return BX.bitrix_sessid();
    },
});