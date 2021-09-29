using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace HHT_AutoSendbackGiftcode_MVC.Controllers
{
    public class APIController : Controller
    {
        [HttpGet]
        public String Webhooks()
        {
            return System.Web.HttpContext.Current.Request.QueryString["hub.challenge"].ToString();
        }

        [HttpPost]
        public HttpResponseMessage Webhooks(String a)
        {
            try
            {
                //You got the data do whatever you want here!!!Happy programming!!  
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.BadGateway);
            }
        }

    }
}