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

            string newFileName = Utils.GiftCodeFileName;
            string zGiftCodeCountName = Utils.BaseGiftCodeCount;
            var zTotalGiftcode = Utils.GetTotalGiftCode(zGiftCodeCountName);
            var zTotalUsedGiftcode = Utils.ReadToFile(newFileName);
            ViewBag.TotalUsedGiftcode = zTotalUsedGiftcode;
            ViewBag.TotalGiftcode = zTotalGiftcode;
            ViewBag.TotalRemainGiftcode = zTotalGiftcode- zTotalUsedGiftcode;
            var zLogFileToday = Utils.ReadLogFile();
            ViewBag.LogFileToday = zLogFileToday.Take(100);
            if (zTotalGiftcode >= 0)
            {
                ViewBag.notifi = "Có file giftcode.txt! Tải file thành công";
            }
            else if(zTotalGiftcode == -1)
            {
                ViewBag.notifi = "Không có file giftcode.txt! Vui lòng tải file Giftcode lên!";
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
                string zGiftCodeCountName = Utils.BaseGiftCodeCount;
                var isWrite=Utils.WriteToFile(data, newFileName);

                //write toltal base giftcode
                var TotalGiftcode = Utils.ReadToFile(newFileName);
                Utils.WriteToFile(TotalGiftcode.ToString(), zGiftCodeCountName);
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