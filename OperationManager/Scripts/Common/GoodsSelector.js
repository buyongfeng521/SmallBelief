//查询
function SearchGoods() {
    var url = "/Admin/Promotion/SearchGoodsBy";
    var param = "cat_id=" + $("#ddlType").val() + "&keywords=" + $("#keywords").val();
    $.ajax({
        type: "GET",
        url: url,
        data: param,//"name=John&location=Boston",
        success: function (data) {
            var dataObj = eval("(" + data + ")");
            //alert(dataObj);
            $("#selSource").empty();
            for (var item in dataObj) {
                $("#selSource").append("<option value='" + dataObj[item]["goods_id"] + "'>" + dataObj[item]["goods_name"] + "</option>");
            }

            //绑定源双击事件
            BindSourceDBClick();
        }
    });
}

//绑定source双击事件
function BindSourceDBClick() {
    //双击源
    $("#selSource option").dblclick(function () {
        var optValue = $(this).val();
        var optTxt = $(this).text();

        var isHas = false;
        $("#selDesc option").each(function () {
            if ($(this).val() == optValue) {
                isHas = true;
            }
        });

        if (isHas == false) {
            $("#selDesc").append("<option value='" + optValue + "'>" + optTxt + "</option>");

            //绑定双击事件
            BindDescDBClick();
        }
    });
}

//绑定desc双击事件
function BindDescDBClick() {
    $("#selDesc option").dblclick(function () {
        $(this).remove();
    })
}

//商品筛选 按钮操作
function ToRightAll() {
    $("#selSource option").each(function () {
        if (IsHasDesc($(this).val()) == false) {
            var optValue = $(this).val();
            var optTxt = $(this).text();
            $("#selDesc").append("<option value='" + optValue + "'>" + optTxt + "</option>");
        }
    });
    //绑定双击事件
    BindDescDBClick();
}
function ToLeftAll() {
    $("#selDesc option").each(function () {
        $(this).remove();
    });
}
function ToRight() {
    var selOpt = $("#selSource").find("option:selected");
    if (selOpt) {
        var optValue = selOpt.val();
        var optTxt = selOpt.text();
        if (IsHasDesc(optValue) == false) {
            $("#selDesc").append("<option value='" + optValue + "'>" + optTxt + "</option>");
            //绑定双击事件
            BindDescDBClick();
        }

    }
}
function ToLeft() {
    var selOpt = $("#selDesc").find("option:selected");
    if (selOpt) {
        selOpt.remove();
    }
}

//目标是否存在
function IsHasDesc(data) {
    var isHas = false;
    $("#selDesc option").each(function () {
        if ($(this).val() == data) {
            isHas = true;
        }
    })
    return isHas;
}