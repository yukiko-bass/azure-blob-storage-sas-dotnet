const { BlockBlobClient, AnonymousCredential } = require("@azure/storage-blob");

blobUpload = function(file, sasuri, container) {
    var blobName = buildBlobName(file);
    var splitedSASUri = splitSasuri(sasuri);
    var login = `${splitedSASUri[0]}/${container}/${blobName}?${splitedSASUri[1]}`;
    var blockBlobClient = new BlockBlobClient(login, new AnonymousCredential());
    blockBlobClient.uploadBrowserData(file);
}

function buildBlobName(file) {
    var filename = file.name.substring(0, file.name.lastIndexOf('.'));
    var ext = file.name.substring(file.name.lastIndexOf('.'));
    return filename + '_' + Math.random().toString(16).slice(2) + ext;
}

function splitSasuri(sasuri) {
    // ? で分ける
    var url = sasuri.substring(0, sasuri.lastIndexOf('?'));
    var sasKey = sasuri.substring(sasuri.lastIndexOf('?'));
    return [url, sasKey];
}