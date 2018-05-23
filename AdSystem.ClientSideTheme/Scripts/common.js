(function () {
    $(document).ready(function () {
        $('#menu-wrapper').load('/PagePartials/menu.html');
        //$('select[multiple]').select2({
        $('select').select2({
            dir: 'rtl'
        });
        
    });
})();