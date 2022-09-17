let dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#productTable').DataTable({
        "ajax": {
            "url": "/Admin/Product/GetAll"
        },
        "columns": [
            { "data": "title", "width": "20%" },
            { "data": "isbn", "width": "15%" },
            { "data": "price", "width": "15%" },
            { "data": "author", "width": "15%" },
            { "data": "category.name", "width": "15%" },
            {
                "data": "id",
                "width": "20%",
                // data is the Product.Id in this case
                "render": function (data) {
                    return `
                            <div class="w-100 btn-group" role="group">
                                <a href="/Admin/Product/Upsert?id=${data}" class="btn btn-primary me-2 w-50">
							        <i class="bi bi-pencil-square"></i>
							        Edit
						        </a>
						        <a class="btn btn-danger w-50" onclick="deleteProduct('/Admin/Product/Delete/${data}')">
							        <i class="bi bi-trash3-fill"></i>
							        Delete
						        </a>
                            </div>
                        `;
                }
            }
        ]
    });
}

function deleteProduct(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    } else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}