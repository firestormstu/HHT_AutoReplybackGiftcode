using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace HHT_AutoSendbackGiftcode_MVC
{
    public static class Utils
    {
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
        public static bool WriteToFileLog(string dataString)
        {
            var fileName ="Log"+ DateTime.Now.ToString("dd-MM-yyyy")+".txt";
            var currentDomain = AppDomain.CurrentDomain.BaseDirectory;
            var path = currentDomain+"/Log/" + fileName;
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
        public static bool WriteToFileUser(string dataString)
        {
            var fileName = "UserGotCode.txt";
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
        public static int ReadToFile(string fileName)
        {
            var currentDomain = AppDomain.CurrentDomain.BaseDirectory;
            var path = currentDomain + fileName;
            if (File.Exists(path)) // If file does not exists
            {
                string stringData=File.ReadAllText(path);
                return CountSplitString(stringData);
            }
            else
            {
                return -1;
            }            
        }
        public static int CountSplitString(string data)
        {
            var totalGiftcode = data.Split();
            return totalGiftcode.Length;
        }
    }
}