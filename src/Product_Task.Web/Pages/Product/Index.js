$(function () {
    var l = abp.localization.getResource('Product_Task');
    var createModal = new abp.ModalManager(abp.appPath + 'Product/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Product/EditModal');
   
    var dataTable = $('#ProductsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(product_Task.products.product.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible: abp.auth.isGranted('Product_Task.Products.Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible: abp.auth.isGranted('Product_Task.Products.Delete'),
                                    confirmMessage: function (data) {
                                        return l(
                                            'ProductDeletionConfirmationMessage',
                                            data.record.name
                                        );
                                    },
                                    action: function (data) {
                                        product_Task.products.product
                                            .delete(data.record.id)
                                            .then(function() {
                                                abp.notify.info(
                                                    l('SuccessfullyDeleted')
                                                );
                                                dataTable.ajax.reload();
                                            });
                                    }
                                }
                            ]
                    }
                },
                {
                    title: l('Image'),
                    data: "image",
                    render: function (data) {
                        return '<img src="/Images/' + data + '" class="avatar" width="50" height="50"/>';
                    }
                },
                {
                    title: l('Name'),
                    data: "name"
                },
                {
                    title: l('Category'),
                    data: "categoryName",
                },
                {
                    title: l('Price'),
                    data: "price"
                },
                {
                    title: l('Stock'),
                    data: "stock"
                },
            ]
        })
    );
    createModal.onResult(function () {
        dataTable.ajax.reload();
    });
    editModal.onResult(function () {
        dataTable.ajax.reload();
    });
    $('#NewProductButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});

function filterTable() {
   
    let dropdown, table, rows, cells, category, filter;
    dropdown = document.getElementById("categoryList");
    table = document.getElementById("ProductsTable");
    rows = table.getElementsByTagName("tr");
    filter = dropdown.value;
    for (let row of rows) { 
        cells = row.getElementsByTagName("td");

        if (cells.length>=6) {
            category = cells[3] || null;
            if (filter === "All" || !category || (filter === category.textContent)) {
                row.style.display = "";
            }
            else {
                row.style.display = "none";
            }

        } else {
            category = cells[2] || null;
            if (filter === "All" || !category || (filter === category.textContent)) {
                row.style.display = "";
            }
            else {
                row.style.display = "none";
            }

        }
       

       
    }
}

function openImageInput(selectedInputId) {
    document.getElementById(selectedInputId).click();
}

function readURL(input,targetId) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            document.getElementById(targetId).src = e.target.result;
        }
        reader.readAsDataURL(input.files[0]);
    }
}

