var datatable;
$(document).ready(function () {
    loadDataTable();
})
function loadDataTable() {
    datatable = $('#tbldata').DataTable({
        "ajax": {
            "url": "Trail/GetAll",
            "type": "Get",
            "datatype": "Json"
        },
        "columns": [
            { "data": "nationalPark.name", "width": "20%" },
            { "data": "name", "width": "20%" },
            { "data": "distance", "width": "20%" },
            {"data":"elevation","width":"20%"},
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                        <a class="btn btn-info" href="Trail/Upsert/${data}">
                            <i class="fas fa-edit"></i></a>
                        <a class="btn btn-danger" onClick=Delete("Trail/Delete/${data}")>
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
