using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class OrderDetailViewModel
    {
        public string order_sn{get;set;}
        public string order_status_content{get;set;}
        public decimal order_amount{get;set;}
        public string add_time{get;set;}
        public string consignee{get;set;}
        public string mobile{get;set;}
        public string address{get;set;}

        public List<OrderGoodsViewModel> order_goods { get; set; }

    }

}
