$(document).ready(function () {

    var dataTable = $('#Users').dataTable({
            "processing": true,
            "serverSide": true,
            "filter": true,
            "ajax": {
                "url": "/Users/Filter",
                "type": "POST",
                "data": function (data) {
                      data.from = $('#datepicker1').val();
                      data.to = $('#datepicker2').val();
                },
            },
            "columnDefs": [{
                "targets": [0],
                "visible": false,
                "searchable": false,
            }],
            "columns": [
                { "data": "id", "name": "Id", "autowidth": true }, // 0
                { "data": "userName", "name": "UserName", "autowidth": true },// 1
                { "data": "fullName", "name": "FullName", "autowidth": true },// 2
                {
                    "render": function (data, type, row) { return '<span>' + row.createdOn.split('T')[0] + '</span>' },
                    "name": "CreatedOn", "autowidth": true
                },// 2
                { "data": "createdBy", "name": "CreatedBy", "autowidth": true },// 3
                {
    //                "render": function (data, type, row) { return '<a href="/Users/Delete?id="' + row.id + '"">' + row.id + '</a>' },
                    "render": function (data, type, row) { return '<form action="/Users/Delete" method="post"><input type="hidden" name="id" value="' + row.id + '" /><input type="submit" value="Delete" class="btn btn-danger"/></form>' },

                    "orderable": false
                    
                },
        ],
            "dom": 'Bfrtip',
            "buttons": 
            [
                'colvis',
                'copy',
                'print',
                {
                    extend: 'spacer',
                    style: 'bar',
                    text: 'export files'
                },
                'csv',
                'excel',
                'spacer',
                'pdf'
            ]
        });




    $("#myList").change(function () {

        dataTable.DataTable().draw();
    });


    $("#datepicker1").change(function () {

        dataTable.DataTable().draw();
    });

    $("#datepicker2").change(function () {

        dataTable.DataTable().draw();
    });


    $(".rmv").on("click", function () {

        this.remove();
    });

});
