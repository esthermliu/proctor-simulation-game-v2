mergeInto(LibraryManager.library, {

    LogDocumentToFirebase: function (collectionNamePtr, jsonDataPtr) {
        const collectionName = UTF8ToString(collectionNamePtr);
        const jsonData = UTF8ToString(jsonDataPtr);
        window.firebaseManager.LogDocumentToFirestore(collectionName, jsonData);
    },

});