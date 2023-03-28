$(document).ready(function () {

    

    var dataTable = $('#Numbers').dataTable({
            "processing": true,
            "serverSide": true,
            "filter": true,
            "ajax": {
                "url": "/Wallet/Filter",
                "type": "POST",
                "data": function (data) {
                      data.from = $('#datepicker1').val();
                      data.to = $('#datepicker2').val();
                      data.statusId = $('#myList option:selected').val();
                },
            },
            "columnDefs": [{
                "targets": [0],
                "visible": false,
                "searchable": false,
            }],
            "columns": [
                { "data": "id", "name": "id", "autowidth": true }, // 0
                { "data": "serviceNumber", "name": "serviceNumber", "autowidth": true },// 1
                {
                    "render": function (data, type, row) { return '<span>' + row.uploadedDate.split('T')[0] + '</span>' },
                    "name": "uploadedDate", "autowidth": true
                },// 2
                { "data": "offer", "name": "offer", "autowidth": true },// 3
                {
                    "data": "status",
                    render: function (data, type, row) {
                        if (data == 1) { return 'New'; }
                        else if (data == 2) { return 'Done'; }
                        else if (data == 3) { return 'Failed'; }
                        else if (data == 4) { return 'In Progress'; }
                        else { return 'Null'; }
                    },//4
                },//5
                { "data": "responseMessage", "name": "responseMessage", "autowidth": true },//6
                //{
                //    "render": function (data, type, row) { return '<span>' + row.transactionDate.split('T')[0] + '</span>' },
                //    "name": "transactionDate", "autowidth": true
                //},// 7
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


});
