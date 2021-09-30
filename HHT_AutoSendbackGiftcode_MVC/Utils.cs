using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace HHT_AutoSendbackGiftcode_MVC
{
    public static class Utils
    {
        public static string GiftCodeFileName = "giftcode.txt";
        public static string UserGotCodeFileName = "UserGotCode.txt";
        public static string BaseGiftCodeCount = "GiftCodeCount.txt";
        public static bool WriteToFile(string dataString, string fileName)
        {
            var currentDomain = AppDomain.CurrentDomain.BaseDirectory;
            var path = currentDomain + fileName;
            try
            {
                if (!File.Exists(path)) // If file does not exists
                {
                    File.Create(path).Close(); // Create file
                    using (StreamWriter sw = File.AppendText(path))
                    {
                        sw.WriteLine(dataString); // Write text to .txt file
                    }
                }
                else // If file already exists
                {
                    // File.WriteAllText("FILENAME.txt", String.Empty); // Clear file
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine(dataString); // Write text to .txt files
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }  
            
        }
        public static bool WriteToFile(string[] dataArr, string fileName)
        {
            var currentDomain = AppDomain.CurrentDomain.BaseDirectory;
            var path = currentDomain + fileName;
            try
            {
                if (!File.Exists(path)) // If file does not exists
                {
                    File.Create(path).Close(); // Create file
                    File.WriteAllLines(path, dataArr.ToArray());
                  
                }
                else // If file already exists
                {
                    // File.WriteAllText("FILENAME.txt", String.Empty); // Clear file
                    File.WriteAllLines(path, dataArr.ToArray());

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public static bool WriteToFileLog(string userid,string Giftcode)
        {
            var fileName ="Log"+ DateTime.Now.ToString("dd-MM-yyyy")+".txt";
            var currentDomain = AppDomain.CurrentDomain.BaseDirectory;
            var path = currentDomain+"/Log/" + fileName;
            var dataString = DateTime.Now.ToString("dd/MM") + "   " +DateTime.Now.ToString("HH:mm:ss")+"   " + userid + "   " + Giftcode;
            try
            {
                if (!File.Exists(path)) // If file does not exists
                {
                    File.Create(path).Close(); // Create file
                    using (StreamWriter sw = File.AppendText(path))
                    {
                        sw.WriteLine(dataString); // Write text to .txt file
                    }
                }
                else // If file already exists
                {
                    // File.WriteAllText("FILENAME.txt", String.Empty); // Clear file
                    using (StreamWriter sw = File.AppendText(path))
                    {
                        sw.WriteLine(dataString); // Write text to .txt files
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public static bool WriteToFileUser(string dataString)
        {
            var fileName = UserGotCodeFileName;
            var currentDomain = AppDomain.CurrentDomain.BaseDirectory;
            var path = currentDomain + fileName;
            try
            {
                if (!File.Exists(path)) // If file does not exists
                {
                    File.Create(path).Close(); // Create file
                    using (StreamWriter sw = File.AppendText(path))
                    {
                        sw.WriteLine(dataString); // Write text to .txt file
                    }
                }
                else // If file already exists
                {
                    // File.WriteAllText("FILENAME.txt", String.Empty); // Clear file
                    using (StreamWriter sw = File.AppendText(path))
                    {
                        sw.WriteLine(dataString); // Write text to .txt files
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public static string GetNextGiftcode()
        {
            var fileName = GiftCodeFileName;
            var currentDomain = AppDomain.CurrentDomain.BaseDirectory;
            var path = currentDomain + fileName;
            try
            {
                if (!File.Exists(path)) // If file does not exists
                {
                    File.Create(path).Close(); // Create file
                    return "-99";
                }
                else // If file already exists
                {
                    //get 1 item
                    var zData = File.ReadAllText(path, Encoding.Default).Split(" \r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    var line1 = zData.FirstOrDefault();
                    //write less 1 item
                    WriteToFile(zData.Skip(1).ToArray(), fileName);
                    return line1;
                }
            }
            catch (Exception)
            {
                return "-99";
            }
        }
        public static bool CheckUserExist(string PSID)
        {
            var fileName = "UserGotCode.txt";
            var currentDomain = AppDomain.CurrentDomain.BaseDirectory;
            var path = currentDomain + fileName;
            try
            {
                if (!File.Exists(path)) // If file does not exists
                {
                    File.Create(path).Close(); // Create file
                    return false;
                }
                else // If file already exists
                {
                    string stringData = File.ReadAllText(path);
                    if (stringData.Contains(PSID))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

        }
        public static int GetTotalGiftCode(string fileName)
        {
            var currentDomain = AppDomain.CurrentDomain.BaseDirectory;
            var path = currentDomain + fileName;
            if (File.Exists(path)) // If file does exists
            {
                string stringData = File.ReadAllText(path);
                return int.Parse(stringData);
            }
            else
            {
                return -1;
            }
        }
        public static int ReadToFile(string fileName)
        {
            var currentDomain = AppDomain.CurrentDomain.BaseDirectory;
            var path = currentDomain + fileName;
            if (File.Exists(path)) // If file does exists
            {
                string stringData=File.ReadAllText(path);
                return CountSplitString(stringData);
            }
            else
            {
                return -1;
            }            
        }
        public static string[] ReadLogFile()
        {
            var fileName = "Log" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
            var currentDomain = AppDomain.CurrentDomain.BaseDirectory;
            if(!Directory.Exists(currentDomain + "/Log/"))
            {
                Directory.CreateDirectory(currentDomain + "/Log/");
            }    
             
            var path = currentDomain + "/Log/" + fileName;
            if (File.Exists(path)) // If file does exists
            {
                string[] stringData = File.ReadAllText(path, Encoding.Default).Split("\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                return stringData;
            }
            else
            {
                File.Create(path).Close(); // Create file
                return new string[0];
            }
        }
        public static int CountSplitString(string data)
        {
            string[] stringSeparators = new string[] { "\r\n" };
            var totalGiftcode = data.Split(stringSeparators , StringSplitOptions.RemoveEmptyEntries); 
            return totalGiftcode.Length;
        }
    }
}