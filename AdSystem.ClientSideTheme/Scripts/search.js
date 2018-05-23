'use strict'
(function () { 
    $(function () {
        showSelectedFieldSet();
        fillCitiesDropDown();
        $('#ad_type').on('change', function () {
            showSelectedFieldSet();
        });
        $('#cities').on('change', function () {
            $.getJSON('/DataSources/' + $(this).val() + '.json', function (districts) {
                $('#districts').html('');
                for (var i = 0; i < districts.length; i++) {
                    $('#districts').append(
                        $('<button>')
                            .addClass('btn btn-sm btn-success pull-right')
                            .attr('id', 'district_' + districts[i].id)
                            .attr('type', 'button')
                            .css('margin', '10px 0 0 15px')
                            .text(districts[i].name)
                            .on('click', function () {
                                //$(this).addClass('active');
                                $(this).toggleClass('active');
                            })
                    );
                        
                }
            });
        });
        $('#submit').on('click', submitSearchForm);
    });
    function fillCitiesDropDown() {
        $.getJSON('/DataSources/iran.json', function (cities) {
            for (var i = 0; i < cities.length; i++) {
                $('<option></option>')
                    .attr('value', cities[i].id)
                    .text(cities[i].city_name)
                    .appendTo('#cities');
            }
        });
    }
    function showSelectedFieldSet()
    {
        var selectedSearchType = $("#ad_type").val();
        var showSelector = '#' + selectedSearchType + '_fields';
        $('[id$=_fields]').not('.static').hide();
        $(showSelector).show();
        //switch (selectedSearchType) {
        //    case 'sale':
        //        $('#sale_fields').show();
        //        $('#rent_fields').hide();
        //    break;
        //    case 'rent':
        //        $('#sale_fields').hide();
        //        $('#rent_fields').show();
        //    break;
        //}
    }
    function submitSearchForm()
    {
        //console.log();
        var formArray = $('#search-form').serializeArray();
        var formData = new FormData();
        for (var i = 0; i < formArray.length; i++) {
            formData.append(formArray[i].name, formArray[i].value);
        }
        //console.log(formData.getAll('age'));
        //$.post('Search_Aciton.aspx', formArray, function (response) {
        //    $('#main-wrapper').html(response);
        //});
        $.ajax({
            url: 'Search_Action.aspx',
            method: 'POST',
            data: formArray,
            success: function (response) {
                $('#main-wrapper').html(response);
            },
            error: function () {
                //console.log('خطایی پیش آمده');
                $('<div>').addClass('alert alert-danger alert-dismissable')
                    .html("خطایی در ارتباط با سرور به وجود آمده")
                    .append('<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>')
                    .prependTo('#main-wrapper');
            }
        });
    }
})();