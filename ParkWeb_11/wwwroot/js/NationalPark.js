var datatable;
$(document).ready(function () {
    loadDataTable();
})
function loadDataTable() {
    datatable = $('#tbldata').DataTable({
        "ajax": {
            "url": "NationalPark/GetAll",
            "type": "GET",
            "datatype":"Json"
        },
        "columns": [
            { "data": "name", "width": "40%" },
            { "data": "state", "width": "40%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                        <a class="btn btn-info" href="NationalPark/Upsert/${data}">
                            <i class="fas fa-edit"></i></a>
                        <a class="btn btn-danger" onClick=Delete("NationalPark/Delete/${data}")>
                        <i class="fas fa-trash-alt"></i>
                        </a>
                        </div>
                    `;
                }
            }
        ]
    })
}
function Delete(url) {
    swal({
        title: "want to delete data",
        text: "Delete information!!!",
        buttons: true,
        dangerModel: true,
        icon: "warning"
    }).then((willdelete) => {
        if (willdelete) {
            $.ajax({
                url: url,
                type: "Delete",
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        datatable.ajax.reload();
                    }
                    else {
                        toastr.Error(data.message);
                    }
                }
            })
        }
    })
}
