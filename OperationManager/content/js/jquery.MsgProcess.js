(function ($) {
	$.extend($,
        {
        	//统一处理 返回的json数据格式
        	procAjaxData: function (data, funcSuc, funcErr) {
        		if (!data || !data.Status) {
        			return;
        		}

        		switch (data.Status) {    
        		    case "ok":
        		        if (data.Msg) {
        		            alert(data.Msg);
        		        }
        				if (funcSuc) funcSuc(data);
        				break;
        			case "err":
        				alert(data.Msg);
        				if (funcErr) funcErr(data);
        				break;
        		    case "nologin":
        			    alert(data.Msg);
        			    if (data.BackUrl) window.location = data.BackUrl;
        				break;
        		}
        	}
        });
}(jQuery));