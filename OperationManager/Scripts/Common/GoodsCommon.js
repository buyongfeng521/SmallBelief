

//商品选择器
function show(showVal) {
    if (showVal) {
        $("#hideGoodsType").val(showVal);
    }
    else {
        $("#hideGoodsType").val("");
    }
    $('#modalGoods').modal();
}


//搜索产品
function SearchGoods() {
    var url = "/Common/SearchGoodsBy";
    var param = "cat_id=" + $("#ddlType").val() + "&keywords=" + $("#keywords").val();
    $.ajax({
        type: "GET",
        url: url,
        data: param,
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

//双击事件
function BindSourceDBClick() {
    //双击源
    $("#selSource option").dblclick(function () {
        var optValue = $(this).val();
        var optTxt = $(this).text();

        var flag = $("#hideGoodsType").val();
        if (flag == "1") {
            $("#lbClickGoods").html(optTxt);
            $("#clickGoods").val(optValue);
            $("#show_title").val(optTxt);
        }
        else {
            $("#lbGoods").html(optTxt);
            $("#goods").val(optValue);
        }

        


        //关闭弹出框
        $('#modalGoods').modal('hide');
    });
}