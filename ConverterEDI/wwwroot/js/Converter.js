$(document).ready(function () {
    var dt = {
        "buyerItemCode": "",
        "buyerItemDescription": "",
        "buyerUnitOfMeasure": "",
        "ratio": ""
    };
    $("#BuyerItemCode").val(dt.buyerItemCode);
    $("#BuyerItemDescription").val(dt.buyerItemDescription);
    $("#BuyerUnitOfMeasure").val(dt.buyerUnitOfMeasure);
    $("#Ratio").val(dt.ratio);

    $("input").change(function () {
        var model = setModel();
        validateForm(model);
    });
});

function hideDownloadButtons(value) {
    $("#downloadButton").prop('hidden', value);
    $("#response").html('');
    $('#deliveryFile').val('');
    $("#deliveryFile")[0].labels[0].innerText = 'Wybierz plik';
}

function renderResponse(data) {
    var component = '<table style="width: 100%;" class="table table-bordered table-sm">' +
        '<thead class="thead-light"><tr><th>Nazwa</th><th>Ilość</th><th>Cena</th><th>EAN</th><th></th></tr></thead><tbody>';
    data.map(function (el) {
        var buttons = el['isConverted'] === false ? '<td><button onclick="editForm(\'' + el['ean'] + '\')"class="btn btn-success btn-sm">D</button></td>' :
            '<td><button onclick="editForm(\'' + el['ean'] + '\')"class="btn btn-danger btn-sm">X</button></td>';
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


function editForm(ean) {
    var model = new FormData();
    model.append("ean", ean);
    loading(true);

    $.ajax({
        url: '/Decompletion/GetRowData',
        type: "POST",
        data: model,
        processData: false,
        contentType: false,
        cache: false,
        enctype: 'multipart/form-data',

        success: function (resp) {
            loading(false);
            var editRow = {
                "supplierItemCode": resp.data.ean,
                "supplierItemDescription": resp.data.productName,
                "supplierUnitOfMeasure": resp.data.unit,
                "buyerItemCode": "",
                "buyerItemDescription": "",
                "buyerUnitOfMeasure": "",
                "ratio": ""
            };
            completeFormData(editRow);
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

function completeFormData(dt) {
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


function validateForm(model) {
    var isError = false;
    if (model.get("BuyerItemCode").length === 0) {
        $("#buyerCodeError").prop('hidden', false);
        isError = true;
    } else {
        $("#buyerCodeError").prop('hidden', true);
    }

    if (model.get("BuyerItemDescription").length === 0) {
        $("#buyerItemDescriptionError").prop('hidden', false);
        isError = true;
    } else {
        $("#buyerItemDescriptionError").prop('hidden', true);
    }

    if (model.get("BuyerUnitOfMeasure").length === 0) {
        $("#buyerUnitOfMeasureError").prop('hidden', false);
        isError = true;
    } else {
        $("#buyerUnitOfMeasureError").prop('hidden', true);
    }

    if (Number(model.get("Ratio")) <= 0) {
        $("#ratioError").prop('hidden', false);
        isError = true;
    } else {
        $("#ratioError").prop('hidden', true);
    }

    return isError;
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
    console.log(isError);
    if (isError) {
        return null;
    }

    $("#editForm").modal('hide');

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
    if ($("#selectMenu").val() <= 3) {
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
            console.log(data['data']);
            if (data['error'] === false) {
                $("#error").prop('hidden', true);
                renderResponse(data['data']);
                $("#downloadButton").prop('hidden', false);
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

function loadDataFromBufor() {
    loading(true);
    var model = new FormData();
    model.append("supplierId", $("#selectMenu").val());
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
            console.log(data['data']);
            if (data['error'] === false) {
                $("#error").prop('hidden', true);
                renderResponse(data['data']);
                $("#downloadButton").prop('hidden', false);
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