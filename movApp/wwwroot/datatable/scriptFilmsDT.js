$(document).ready(function () {

    var table = $('#example').DataTable(
        {
            //"aoColumnDefs": [
            //    { 'bSortable': false, 'aTargets': [2] } //отключаем сортировку для полей с кнопками edit 
            //],

            //"language": {
            //    "url": "/js/russian.json"
            //},
            
            "processing": true, // for show progress bar    
            "serverSide": true, // for process server side    
            "filter": true, // this is for disable filter (search box)    
            "orderMulti": false, // for disable multiple column at once 
            "bSortCellsTop": true,
            "ajax": {
                "url": "/Films/GetFilmsAjaxAsync",
                "type": "POST",
                "datatype": "json"
            },
            "columns":
                [
                    {
                        "data": "name", "render": function (data, type, full, meta) {
                            return '<a href="/Films/Details/' + full.id + '">'+full.name+'</a>';
                        } },
                    { "data": "year", "name": "Year" }

                ]
            
        }
    );

});


