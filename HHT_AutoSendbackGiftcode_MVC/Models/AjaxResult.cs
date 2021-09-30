using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HHT_AutoSendbackGiftcode_MVC.Models
{
    public class AjaxResult
    {
        public int Code { get; set; }
        public string Messager { get; set; }
        public object Data { get; set; }
    }
}