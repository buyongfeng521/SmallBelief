

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
function SearchGoods2() {
    var url = "/Common/SearchGoodsBy";
    var param = "cat_id=" + $("#ddlType2").val() + "&keywords=" + $("#keywords2").val();
    $.ajax({
        type: "GET",
        url: url,
        data: param,
        success: function (data) {
            var dataObj = eval("(" + data + ")");
            //alert(dataObj);
            $("#selSource2").empty();
            for (var item in dataObj) {
                $("#selSource2").append("<option value='" + dataObj[item]["goods_id"] + "'>" + dataObj[item]["goods_name"] + "</option>");
            }

            //绑定源双击事件
            BindSourceDBClick2();
        }
    });
}

//双击事件
function BindSourceDBClick2() {
    //双击源
    $("#selSource2 option").dblclick(function () {
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