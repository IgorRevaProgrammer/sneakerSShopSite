function openModalSearchWindow () {
    var wsize = document.documentElement.clientWidth;
    if (wsize<575) {
        $('#modalSearch').modal('show');
    }
};
formSearch.onclick = openModalSearchWindow;
function openMakeReqWindow() {
     $('#modalReq').modal('show');
};
makeReq.onclick = openMakeReqWindow;