using HHT_AutoSendbackGiftcode_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HHT_AutoSendbackGiftcode_MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string newFileName = "giftcode.txt";
            var TotalGiftcode = Utils.ReadToFile(newFileName);
            ViewBag.TotalGiftcode= TotalGiftcode;
            if (TotalGiftcode > 0)
            {
                ViewBag.notifi = "Load file thành công";
            }
            else
            {
                ViewBag.notifi = "Lỗi.Load file thất bại";
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }   
        [HttpPost]
        public ActionResult HandleDataFile(string data)
        {
            AjaxResult result = new AjaxResult();
            try
            {
                string newFileName = Utils.GiftCodeFileName;
                var isWrite=Utils.WriteToFile(data, newFileName);
                if (isWrite)
                {
                    result.Code = 1;
                    result.Messager = "Load file thành công";
                    result.Data = Utils.CountSplitString(data);
                    return Json(result);
                }
                else
                {
                    result.Code = 99;
                    result.Messager = "Lỗi trong việc tạo File.";
                    return Json(result);
                }
                
            }
            catch (Exception)
            {
                result.Code = 99;
                result.Messager = "Thất bại.Có lỗi xẩy ra.";
                return Json(result);
            }    
        }
    }
}