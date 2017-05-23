
var now = new Date();
var url = "http://localhost:54211/api/";
var strDateTime =
        [
            addZero(now
                .getMonth() + 1),
            addZero(now.getDate()),
            now.getFullYear()
        ].join("/");

function addZero(num) {
    return (num >= 0 && num < 10) ? "0" + num : num + "";
}

$(document).ready(function () {
    $('.fecha').datetimepicker({
        startDate: Date.now(), //'+1971/05/01'or 1986/12/08
        timepicker: false,
        format: 'm/d/Y',
        //format: 'm/D/Y',
        //mask: true // '9999/19/39
    });
});

function selectTeeth(nroTooth) {
    var teeth = $("#Teeth").val();
    var areaPaint = "#tooth" + nroTooth; //alert(areaPaint);
    if (!teeth.includes(nroTooth)) {
        $("#Teeth").val(teeth + "," + nroTooth);
        teeth = $("#Teeth").val();
        if (teeth.startsWith(",")) {
            $("#Teeth").val(teeth.slice(1));
            $(areaPaint).attr("class", "opacColor tooth");
        }
    } else {
        teeth = $("#Teeth").val();
        teeth = teeth.replace(nroTooth, "");
        $("#Teeth").val(teeth.replace(",,", ","));
        teeth = $("#Teeth").val();
        $(areaPaint).attr("class", "transparentColor tooth");
        if (teeth.startsWith(",")) {
            $("#Teeth").val(teeth.slice(1));
        }
    }
    teeth = $("#Teeth").val();
    teeth = teeth.replace(",,", ",");
    if (teeth.endsWith(",")) {
        teeth = teeth.substring(0, teeth.length - 1);
    }
    $("#Teeth").val(teeth);
    var quantity = teeth.split(",");
    if (teeth.length == 0) {
        quantity = "0";
        $("#amount").val("");
    } else {

        $("#amount").val(quantity.length);
    }
}

function fillDropdown(items, control) {
    var $lLits = $("<ul/>");
    $.each(items, function (i, val) {

        var $lit = $("<li/>");
        var valVal = val.split(",")[0];
        var valText = val.split(",")[1];
        $lit.append($("<a href='#'>" + valText + "</a>").val(valVal));
        $lLits.append($lit);
    });
    $(control).html($lLits);
}

function fillProducts() {
    var lProducts = [
        "1,Appliances", "2,Full Contour Zirconia", "3,Full Contour Zir", "4,Milling Center Shipping", "5,Multi FZ - Full Contour Zi", "6,Multi substructures", "7,Full Cont Zir", "8,Substructures", "9,Zirconia substructures", "10,Custom Abutment - Hybrid", "11,Custom Abutment - Titanium", "12,DD cubeX² - Full Contor Zir", "13,Design services", "14,e.max CAD", "15,e.max Press", "16,Empress", "17,GW BruxZir - Full Contour Zi", "18,Implant Components", "19,Izir Bridge", "20,Library", "21,Mill Accessories", "22,Milled Wax", "23,Milling Materials", "24,Milling Tools", "25,NA", "26,Nacera Zirconia - Full Cont Zi", "27,Nacera Zirconia substructures", "28,Other", "29,Printed Models", "30,Rotary Instruments", "31,Screw Retained Crowns", "32,Sintron", "33,Temps", "34,Training", "35,Video Review"
    ];
    fillDropdown(lProducts, "#listProducts");
}

function fillBuccalAndLingualMargin() {
    var lBuccalMargin = [
    "1,Supra Ging +0.5mm", "2,Tissue Height", "3,Sub Ging -0.5mm (Default)", "4,Sub Ging -1.0mm", "5,Other"
    ];
    fillDropdown(lBuccalMargin, "#listBuccalMargin");
    fillDropdown(lBuccalMargin, "#listLingualMargin");
}

function fillBuccalAndLingualCompression() {
    var lBuccalCompression = [
    "1,No Compression", "2,0.5mm Comp (Default)", "3,1.0mm Comp", "4,Other"
    ];
    fillDropdown(lBuccalCompression, "#listBuccalTissueCompression");
    fillDropdown(lBuccalCompression, "#listLingualTissueCompression");
}

function fillAbutmentsParallel() {
    var abutmentsParallel = [
        "1, No (Default)", "2,Yes (Please indicate which in notes)", "3,Other"
    ];
    fillDropdown(abutmentsParallel, "#listAbutmentsParallel");
}

function fillShades() {
    var lShades = [
        "1,BLEACH", "2,A1", "3,A2", "4,A3", "5,A3.5", "6,B1", "7,B2", "8,B3", "9,B4", "10,C1", "11,C2", "12,C3", "13,C4", "14,D2", "15,D3", "16,D4"
    ];
    fillDropdown(lShades, "#listShade");
}

function fillMaterials() {
    var lMaterials = [
        "1,ZIRCONIA", "2,PMMA", "3,WAX", "4,E-MAX", "5,TITANIUM", "6,HYBRID-ZIRCONIA"
    ];
    fillDropdown(lMaterials, "#listMaterial");
}

function fillDesignStyle() {
    var lDesignStyles = [
        "1,Coping (Default)", "2,Anatomic Coping", "3,Cutback Crown (specify in notes)", "4,Other"
    ];
    fillDropdown(lDesignStyles, "#listDesignStyle");
}

function fillModelImplantConnection() {
    var lModelImplantConnections = [
        "1,DirectHex Printed Analog (Default)", "2,Abutment as Removeable Die"
    ];
    fillDropdown(lModelImplantConnections, "#listModelImplantConnection");
}

function fillArticulatorType() {
    var lArticulatorTypes = [
        "1,iTero (Default)", "2,Vertex", "3,3Shape", "4,Ortho Base", "5,None"
    ];
    fillDropdown(lArticulatorTypes, "#listArticulatorType");
}

function notifyAlertify(message, type) {
    switch (type) {
        case "success":
            alertify.success(message);
        default:
    }
}

function clearFormElements(className) {
    className = "." + className;
    $(className).find(':input').each(function () {
        switch (this.type) {
            case 'password':
            case 'text':
            case 'textarea':
            case 'file':
            case 'select-one':
            case 'select-multiple':
            case 'date':
            case 'number':
            case 'tel':
            case 'email':
                $(this).val('');
                break;
            case 'checkbox':
            case 'radio':
                this.checked = false;
                break;
        }
    });
}

function randomString(length, chars) {
    var mask = '';
    if (chars.indexOf('a') > -1) mask += 'abcdefghijklmnopqrstuvwxyz';
    if (chars.indexOf('A') > -1) mask += 'ABCDEFGHIJKLMNOPQRSTUVWXYZ';
    if (chars.indexOf('#') > -1) mask += "0123456789";
    if (chars.indexOf('!') > -1) mask += "~`!#$%^&*()_+-={}[]:\";'<>?,./|\\";
    var result = '';
    for (var i = length; i > 0; --i) result += mask[Math.round(Math.random() * (mask.length - 1))];
    return result;
}

//$("#btnAdd").click(function() { //check validation of order item
//    var isValidItem = true;
//    if ($("#itemName").val().trim() == '') {
//        isValidItem = false;
//        $("#itemName").siblings("span.error").CSS("visibility", "visible");
//    } else {
//        $("#itemName").siblings("span.error").CSS("visibility", "hidden");
//    }
//    if (!($("#quantity").val().trim() != '' && !isNaN($("#quantity").val().trim()))) {
//        isValidItem = false;
//        $("#quantity").siblings("span.error").CSS("visibility", "visible");
//    } else {
//        $("#quantity").siblings("span.error").CSS("visibility", "hidden");
//    }
//    if (!($("#details").val().trim() != '' && !isNaN($("#details").val().trim()))) {
//        isValidItem = false;
//        $("#details").siblings("span.error").CSS("visibility", "visible");
//    } else {
//        $("#details").siblings("span.error").CSS("visibility", "hidden");
//    }
//    if (isValidItem) {
//        orderItems.push({
//            ItenName: $("#itemName").val().trim(),
//            Quantity: parseInt($("#quantity").val().trim()),
//            Details: $("#details").val().trim()
//        });
//        $("#itemName").val("").focus();
//        $("#quantity").val("");
//        $("#details").val("");
//        generatedItemTable();
//    }
//});
//save button click function

$("#submit").click(function () { //validation order
    var isAllValid = true;
    if (orderItem.length == 0) {
        $("#orderitems").html("<span style='color:red'> Please add order items</span>");
        isAllValid = false;
    }
    if ($("#patientname").val().trim() == '') {
        $("#patientname").siblings("span.error").CSS("visibility", "visible");
        isAllValid = false;
    } else {
        $("#patientname").siblings("span.error").CSS("visibility", "visible");
    }
    //save if valid
});
//function for show added items in table
//function generatedItemTable() {
//    if (orderItem.length > 0) {
//        var $table = $("<table/>");
//        $table.append("<thead><tr><th>Item</th><th>Quantity</th><th>Details</th></tr></thead>");
//        var $tbody = $("<tbody>");
//        $.each(orderItems, function(i, val) {
//            var $row = $("<tr/>");
//            $row.append($("<td/>").html(val.ItemName));
//            $row.append($("<td/>").html(val.Quantity));
//            $row.append($("<td/>").html(val.Details));
//            $tbody.append($row);
//        });
//        $table.append($tbody);
//        $("#orderItems").html($table);
//    }
//}

function submitbutton_click() {
    var submitbutton = document.getElementById('SubmitButton');
    var uploadobj = document.getElementById('myuploader');
    if (!window.filesuploaded) {
        if (uploadobj.getqueuecount() > 0) {
            uploadobj.startupload();
        }
        else {
            var uploadedcount = parseInt(submitbutton.getAttribute("itemcount")) || 0;
            if (uploadedcount > 0) {
                return true;
            }
            alert("Please browse files for upload");
        }
        return false;
    }
    window.filesuploaded = false;
    return true;
}
function CuteWebUI_AjaxUploader_OnPostback() {
    window.filesuploaded = true;
    var submitbutton = document.getElementById('SubmitButton');
    submitbutton.click();
    return false;
}