using HHT_AutoSendbackGiftcode_MVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HHT_AutoSendbackGiftcode_MVC.Controllers
{
    public class WebHooksController : Controller
    {
        // GET: WebHooks
       // string Facebook_Page_ID = "272225871315132";
        string Facebook_Page_Access_Token = "EAARmBJ5iHrsBAObgcBiFbNwx7OYgKD5Y9N1uRqailzMLfrLcRrpBzlKNj4a4ZBxQedAjRXcjZCitl0Fv56mQMa90DQnXUzWvTg9JZBLddrQXKnDfaO6LxjdKYfRcKg6z0Musf2cILOVaVGuvyH9FKHZAnTTeDKXVVTPBZAsqUSYEJDZAcREffxZAsJW8NOhn1o1u5mHNW6BwwZDZD";
 /*       string Facebook_App_ID = "1238069929975483";
        string Facebook_App_Secret = "970fe3cdf40f7927da46e35e4089913f";*/
        [HttpGet]
        public ActionResult Receive()
        {
            var query = Request.QueryString;

           //_logWriter.WriteLine(Request.RawUrl);

            if (query["hub.mode"] == "subscribe" &&
                query["hub.verify_token"] == "123456Aa@")
            {
                //string type = Request.QueryString["type"];
                var retVal = query["hub.challenge"];
                return Json(int.Parse(retVal), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return HttpNotFound();
            }
        }
        [ActionName("Receive")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ReceivePost(MessageModel data)
        {
            Task.Factory.StartNew(() =>
            {
                foreach (var entry in data.entry)
                {
                    foreach (var message in entry.messaging)
                    {
                        if (string.IsNullOrWhiteSpace(message?.message?.text))
                            continue;

                        var msg = "You said: " + message.message.text;
                        var json = $@" {{recipient: {{  id: {message.sender.id}}},message: {{text: ""{msg}"" }}}}";
                        PostRaw("https://graph.facebook.com/v2.6/me/messages?access_token="+ Facebook_Page_Access_Token, json);
                    }
                }
            });

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
        private string PostRaw(string url, string data)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "POST";
            using (var requestWriter = new StreamWriter(request.GetRequestStream()))
            {
                requestWriter.Write(data);
            }

            var response = (HttpWebResponse)request.GetResponse();
            if (response == null)
                throw new InvalidOperationException("GetResponse returns null");

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                return sr.ReadToEnd();
            }
        }

    }
}