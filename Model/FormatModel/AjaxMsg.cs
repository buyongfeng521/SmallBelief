using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.FormatModel
{
    public class AjaxMsg
    {
        public AjaxMsg()
        {
            Status = "err";
            Msg = "失败";
        }
        //ok,err,nologin
        public string Status { get; set; }

        public string Msg { get; set; }

        public string BackUrl { get; set; }

        public object Data { get; set; }

    }
}
