function hideDownloadButtons(value) {
    $("#downloadButton").prop('hidden', value);
    $("#response").html('');
    $('#deliveryFile').val('');
    $("#deliveryFile")[0].labels[0].innerText = 'Wybierz plik';
}

function renderResponse(data) {
    var component = '<table style="width: 100%;" class="table table-bordered table-sm">' +
        '<thead class="thead-light"><tr><th>Nazwa</th><th>Ilość</th><th>Cena</th><th>EAN</th><th>Opcje</th></tr></thead><tbody>';
    data.map(function (el) {
        var buttons = el['isConverted'] === false ? '<td><button onclick="editForm(\'add\',\'' + el['ean'] + '\')"class="btn btn-outline-success btn-sm" data-toggle="tooltip" data-placement="right" title="Dekompletuj">D</button></td>' :
            '<td><button onclick="editForm(\'edit\',\'' + el['ean'] + '\')"class="btn btn-outline-danger btn-sm" data-toggle="tooltip" data-placement="right" title="Ustawienia dekompletacji">S</button></td>';
        component += "<tr><td>" + el['productName'] + "</td>" +
            "<td>" + el['quantity'] + "</td>" +
            "<td>" + el['purchasePrice'] + "</td>" +
            "<td>" + el['ean'] + "</td>" +
            buttons +
            "</tr>";
    });
    component += "</tbody></table>";
    $("#response").html(component);
}

function editForm(option, ean) {
    var model = new FormData();
    model.append("ean", ean);
    loading(true);
    var url = "";
    switch (option) {
        case 'add':
            url = '/Decompletion/GetRowData';
            break;
        case 'edit':
            url = '/Decompletion/GetRowDataEdit';
            model.append("supplierId", $("#selectMenu").val());
            break;
    }

    $.ajax({
        url: url,
        type: "POST",
        data: model,
        processData: false,
        contentType: false,
        cache: false,
        enctype: 'multipart/form-data',

        success: function (resp) {
            loading(false);
            var editRow = {
                "supplierId": resp.data.supplierId,
                "supplierItemCode": resp.data.supplierItemCode,
                "supplierItemDescription": resp.data.supplierItemDescription,
                "supplierUnitOfMeasure": resp.data.supplierUnitOfMeasure,
                "buyerItemCode": resp.data.buyerItemCode,
                "buyerItemDescription": resp.data.buyerItemDescription,
                "buyerUnitOfMeasure": resp.data.buyerUnitOfMeasure,
                "ratio": resp.data.ratio
            };
            completeFormData(option, editRow);
        },

        error: function (xhr) {
            $("#modalTitle").html('Błąd aplikacji');
            $("#modalBody").html('Podczas ładowania modułu konwersji wystąpił błąd!');
            $("#modalWindow").modal({
                show: true,
                backdrop: 'static'
            });
            console.log("Błąd: ", xhr);
            loading(false);
        }
    });
}

function completeFormData(option, dt) {
    $("#editFormFooter").html(FormFooter(option, dt.supplierItemCode));
    if (option === 'edit') {
        $("#editFormTitle").html('Edycja dekompletacji');
    } else {
        $("#editFormTitle").html('Dodanie dekompletacji');
    }
    $("#editForm").modal({
        show: true,
        backdrop: 'static'
    });
    $("#SupplierItemCode").val(dt.supplierItemCode);
    $("#SupplierItemDescription").val(dt.supplierItemDescription);
    $("#SupplierUnitOfMeasure").val(dt.supplierUnitOfMeasure);
    $("#BuyerItemCode").val(dt.buyerItemCode);
    $("#BuyerItemDescription").val(dt.buyerItemDescription);
    $("#BuyerUnitOfMeasure").val(dt.buyerUnitOfMeasure);
    $("#Ratio").val(dt.ratio);
}

function BuyerItemCodeValidate(model) {
    var isError = false;
    if (model.get("BuyerItemCode").length === 0) {
        $("#buyerCodeError").prop('hidden', false);
        isError = true;
    } else {
        $("#buyerCodeError").prop('hidden', true);
    }
    return isError;
}

function BuyerItemDescriptionValidate(model) {
    var isError = false;
    if (model.get("BuyerItemDescription").length === 0) {
        $("#buyerItemDescriptionError").prop('hidden', false);
        isError = true;
    } else {
        $("#buyerItemDescriptionError").prop('hidden', true);
    }
    return isError;
}

function BuyerUnitOfMeasureValidate(model) {
    var isError = false;
    if (model.get("BuyerUnitOfMeasure").length === 0) {
        $("#buyerUnitOfMeasureError").prop('hidden', false);
        isError = true;
    } else {
        $("#buyerUnitOfMeasureError").prop('hidden', true);
    }
    return isError;
}

function RatioValidate(model) {
    var isError = false;
    var ratio = Number(model.get("Ratio"));
    if (isNaN(ratio) || ratio <= 0) {
        $("#ratioError").prop('hidden', false);
        isError = true;
    } else {
        $("#ratioError").prop('hidden', true);
    }
    return isError;
}

$("#BuyerItemCode").change(function () {
    var model = setModel();
    BuyerItemCodeValidate(model);
});

$("#BuyerItemDescription").change(function () {
    var model = setModel();
    BuyerItemDescriptionValidate(model);
});

$("#BuyerUnitOfMeasure").change(function () {
    var model = setModel();
    BuyerUnitOfMeasureValidate(model);
});

$("#Ratio").change(function () {
    var model = setModel();
    RatioValidate(model);
});

function validateForm(model) {
    if (BuyerItemCodeValidate(model) ||
        BuyerItemDescriptionValidate(model) ||
        BuyerUnitOfMeasureValidate(model) ||
        RatioValidate(model))
        return true;
    return false;
}


function setModel() {
    var model = new FormData();
    model.append("SupplierId", $("#selectMenu").val());
    model.append("SupplierItemCode", $("#SupplierItemCode").val());
    model.append("SupplierItemDescription", $("#SupplierItemDescription").val());
    model.append("SupplierUnitOfMeasure", $("#SupplierUnitOfMeasure").val());
    model.append("BuyerItemCode", $("#BuyerItemCode").val());
    model.append("BuyerItemDescription", $("#BuyerItemDescription").val());
    model.append("BuyerUnitOfMeasure", $("#BuyerUnitOfMeasure").val());
    model.append("Ratio", $("#Ratio").val());
    return model;
}

function addConversion() {
    var model = setModel();
    var isError = validateForm(model);
    if (isError) {
        return null;
    }

    loading(true);
    $.ajax({
        url: '/Decompletion/AddConversion',
        type: "POST",
        data: model,
        processData: false,
        contentType: false,
        cache: false,
        enctype: 'multipart/form-data',

        success: function (resp) {
            loading(false);
            loadDataFromBufor();
            $("#editForm").modal('hide');
        },

        error: function (xhr) {
            console.log("Błąd: ", xhr);
            var checkProp = xhr.responseJSON.error;
            var errMessage = "";
            if ('innerException' in checkProp) {
                errMessage = checkProp.innerException.message;
            }
            if (errMessage.toLowerCase().includes("unique")) errMessage += '<br>' + "Nie mozesz dodać dekompletacji dla tego samego kodu EAN";
            $("#modalTitle").html('Błąd aplikacji');
            $("#modalBody").html('Podczas dodawania dekompletacji wystąpił błąd:<br>' + errMessage);
            $("#modalWindow").modal({
                show: true,
                backdrop: 'static'
            });
            loading(false);
        }
    });
}

function onSupplierSelect() {
    $("#response").html('');
    $('#deliveryFile').val('');
    $("#deliveryFile")[0].labels[0].innerText = 'Wybierz plik';
    $("#downloadButton").prop('hidden', true);
    $("#response").html('');
    if ($("#selectMenu").val() <= 10) {
        $("#selectFile").prop('hidden', false);
        $("#step2-info").prop('hidden', false);
        $("#error").prop('hidden', true);
    } else {
        $("#selectFile").prop('hidden', true);
        $("#step2-info").prop('hidden', true);
        $("#error").prop('hidden', true);
    }
}

function loading(prop) {
    $("#loadingInfo").prop('hidden', !prop);
    $("#loading-spinner").prop('hidden', !prop);
    $("#deliveryFile").prop('disabled', prop);
}

$("#deliveryFile").change(function () {
    loadDataFromFile();
});

function loadDataFromFile() {
    loading(true);
    var files = $('#deliveryFile')[0].files;
    var model = new FormData();
    model.append("files", $('#deliveryFile')[0].files[0]);
    model.append("supplierId", $("#selectMenu").val());
    $.ajax({
        url: '/Converter/UploadFiles',
        type: "POST",
        data: model,
        processData: false,
        contentType: false,
        cache: false,
        enctype: 'multipart/form-data',

        success: function (data) {
            loading(false);
            if (data['error'] === false) {
                $("#error").prop('hidden', true);
                renderResponse(data['data']);
                $("#downloadButton").prop('hidden', false);
                $('[data-toggle="tooltip"]').tooltip();
            } else {
                $("#response").html('');
                $("#error").prop('hidden', false);
                $("#downloadButton").prop('hidden', true);
            }
        },

        error: function (xhr) {
            console.log(xhr);
            loading(false);
            $("#downloadButton").prop('hidden', true);
        }
    });
}

function loadDataFromBufor(option) {
    loading(true);
    var model = new FormData();
    model.append("supplierId", $("#selectMenu").val());
    if (option === 'no_convert') {
        model.append("convert", false);
    }
    $.ajax({
        url: '/Converter/GetData',
        type: "POST",
        data: model,
        processData: false,
        contentType: false,
        cache: false,
        enctype: 'multipart/form-data',

        success: function (data) {
            loading(false);
            if (data['error'] === false) {
                $("#error").prop('hidden', true);
                renderResponse(data['data']);
                $("#downloadButton").prop('hidden', false);
                $('[data-toggle="tooltip"]').tooltip();
            } else {
                $("#response").html('');
                $("#error").prop('hidden', false);
                $("#downloadButton").prop('hidden', true);
            }
        },

        error: function (xhr) {
            console.log(xhr);
            loading(false);
            $("#downloadButton").prop('hidden', true);
        }
    });
}

function FormFooter(option, ean) {
    var component = "";
    if (option === 'add') {
        component = '<button type="button" style="width: 100px;" onclick="addConversion()" class="btn btn-outline-success">Zapisz</button>' +
            '<button type="button" style="width: 100px;" onclick="editFormClose()" class="btn btn-outline-secondary" data-dismiss="modal">Zamknij</button>';
    } else if (option === 'edit') {
        component = '<button type="button" style="width: 100px;" onclick="removeConversion(\'' + ean + '\')" class="btn btn-outline-danger" data-dismiss="modal">Usuń</button>' +
            '<button type="button" onclick="updateConversion(\'' + ean + '\')" class="btn btn-outline-success" >Zapisz zmiany</button>' +
            '<button type="button" style="width: 100px;" onclick="editFormClose()" class="btn btn-outline-secondary" data-dismiss="modal">Zamknij</button>';
    }
    return component;
}

function editFormClose() {
    $("#buyerCodeError").prop('hidden', true);
    $("#buyerItemDescriptionError").prop('hidden', true);
    $("#buyerUnitOfMeasureError").prop('hidden', true);
    $("#ratioError").prop('hidden', true);
}

function removeConversion(ean) {
    var model = new FormData();
    model.append("ean", ean);
    model.append("supplierId", $("#selectMenu").val());

    loading(true);
    $.ajax({
        url: '/Decompletion/RemoveConvert',
        type: "POST",
        data: model,
        processData: false,
        contentType: false,
        cache: false,
        enctype: 'multipart/form-data',

        success: function (resp) {
            loading(false);
            loadDataFromBufor();
        },

        error: function (xhr) {
            console.log("Błąd: ", xhr);
            var checkProp = xhr.responseJSON.error;
            var errMessage = "";
            if ('innerException' in checkProp) {
                errMessage = checkProp.innerException.message;
            }
            if (errMessage.toLowerCase().includes("unique")) errMessage += '<br>' + "Nie mozesz dodać dekompletacji dla tego samego kodu EAN";
            $("#modalTitle").html('Błąd aplikacji');
            $("#modalBody").html('Podczas usuwania dekompletacji wystąpił błąd:<br>' + errMessage);
            $("#modalWindow").modal({
                show: true,
                backdrop: 'static'
            });
            loading(false);
        }
    });
}

function updateConversion(ean) {
    var model = setModel();

    var isError = validateForm(model);

    if (isError) {
        return null;
    }


    loading(true);
    $.ajax({
        url: '/Decompletion/UpdateConversion',
        type: "POST",
        data: model,
        processData: false,
        contentType: false,
        cache: false,
        enctype: 'multipart/form-data',

        success: function (resp) {
            loading(false);
            loadDataFromBufor();
            $("#editForm").modal('hide');
        },

        error: function (xhr) {
            console.log("Błąd: ", xhr);
            var checkProp = xhr.responseJSON.error;
            var errMessage = "";
            if ('innerException' in checkProp) {
                errMessage = checkProp.innerException.message;
            }
            if (errMessage.toLowerCase().includes("unique")) errMessage += '<br>' + "Nie mozesz dodać dekompletacji dla tego samego kodu EAN";
            $("#modalTitle").html('Błąd aplikacji');
            $("#modalBody").html('Podczas usuwania dekompletacji wystąpił błąd:<br>' + errMessage);
            $("#modalWindow").modal({
                show: true,
                backdrop: 'static'
            });
            loading(false);
        }
    });
}

function removeUser(userId) {
    var model = new FormData();
    model.append("userId", userId);
    $.ajax({
        url: '/Home/DeleteUser',
        type: "POST",
        data: model,
        processData: false,
        contentType: false,
        cache: false,
        enctype: 'multipart/form-data',

        success: function (resp) {
            location.href = '/Home/Privacy';
        },

        error: function (xhr) {
            console.log("Błąd: ", xhr);
        }
    });
}

function confirmUserEmail(userId) {
    var model = new FormData();
    model.append("userId", userId);
    $.ajax({
        url: '/Home/ConfirmUserEmail',
        type: "POST",
        data: model,
        processData: false,
        contentType: false,
        cache: false,
        enctype: 'multipart/form-data',

        success: function (resp) {
            location.href = '/Home/Privacy';
        },

        error: function (xhr) {
            console.log("Błąd: ", xhr);
        }
    });
}