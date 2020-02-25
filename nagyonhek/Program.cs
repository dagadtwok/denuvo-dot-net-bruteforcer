using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Wiry.Base32;
using System.IO;
using System.Net;
using System.Globalization;
using Newtonsoft.Json;

namespace nagyonhek
{
    class Program
    {
        public static string ip = "";
        public static string pcuid = "";
        public static string mbserial = " ";
        public static string cpuserial = "";
        public static string lang = "";
        public static string pcname = "";
        public static string procinfo = "";
        public static string videoinfo = "";
        public static string raminfo = "";
        public static string mac = "";
        public static string appver = "";
        public static string genkey = "";
        public static List<string> keys = new List<string>();

        public static string Generate()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < 54; i++)
            {
                char c = chars[random.Next(0, chars.Length)];
                builder.Append(c);
            }

            keys.Add(builder.ToString());

            for (int i = 0; i < keys.Count; i++)
            {
                if (builder.ToString() == keys[i].ToString())
                {
                    Generate();
                }
                else {
                    return builder.ToString();
                }
            }
            return ":(";
        }

        static void Main(string[] args)
        {
            
            feketemagia();
        }

        public static void feketemagia() {
            string result;
          genkey = Generate();
            Console.WriteLine(genkey);
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    //byte[] bytes = webClient.DownloadData("http://alexfolder.ru/epic_activator/get_latest_ver.php");
                    // result = Encoding.UTF8.GetString(bytes);
                    //appver = result;
                    Console.WriteLine("Current app ver: " + appver);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("An error occured");

            }

            string asd = checkKeyRequest(genkey);
            Console.WriteLine(asd);

            String eskere = "";
            try
            {
                byte[] bytes = Base32Encoding.ZBase32.ToBytes(asd);
                eskere = Encoding.UTF8.GetString(bytes);
            }
            catch
            {
                eskere = "";
            }
            if (eskere.Contains("login"))
            {
                Console.WriteLine(eskere);
                if (!(File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\" + genkey + ".hek"))){
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\" + genkey + ".hek", eskere);
                }

            }
            feketemagia();

        }
        public static string StringToZBase32(string stringData, bool toUpper)
        {
            string result;
            try
            {
                string text = Base32Encoding.ZBase32.GetString(Encoding.UTF8.GetBytes(stringData));
                if (toUpper)
                {
                    text = text.ToUpper(CultureInfo.GetCultureInfo("en-US"));
                }
                result = text;
            }
            catch
            {
                result = "";
            }
            return result;
        }
        public static string EncodeString(string stringData) {
            return StringToZBase32(stringData, true);
        }

        public static string checkKeyRequest(string actKey) {
            string str = EncodeString(JsonConvert.SerializeObject(new Dictionary<string, string>
            {
                {
                    "pass",
                    "cK3Ln4DblB5"
                },
                {
                    "action",
                    "checkkey"
                },
                {
                    "cli_key",
                    actKey
                },
                {
                    "cli_ip",
                    ip
                },
                {
                    "cli_pcuid",
                    pcuid
                },
                {
                    "cli_mbserial",
                    mbserial
                },
                {
                    "cli_cpuserial",
                    cpuserial
                },
                {
                    "cli_lang",
                    lang
                },
                {
                    "cli_pcname",
                    pcname
                },
                {
                    "cli_procinfo",
                    procinfo
                },
                {
                    "cli_videoinfo",
                    videoinfo
                },
                {
                    "cli_raminfo",
                    raminfo
                },
                {
                    "cli_mac",
                    mac
                },
                {
                    "cli_appver",
                    appver
                }
            }).ToString());
            return getRequest("http://alexfolder.ru/epic_activator/check_key.php" + "?param=" + str);
        }

        public static string getRequest(string url) {
            string result;
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    byte[] bytes = webClient.DownloadData(url);
                    result = Encoding.UTF8.GetString(bytes);
                    Console.WriteLine(url);
                    Console.WriteLine("");
                }
            }
            catch (Exception)
            {
                result = "An error occured";
            }
            return result;
        }
    }
}
