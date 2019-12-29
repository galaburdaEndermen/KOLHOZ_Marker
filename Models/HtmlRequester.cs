using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KOLHOZ_Marker.Models
{
    class HtmlRequester
    {
        public HtmlRequester(string url)
        {
            string adress = url;
            Adress = adress;
            string home = Home(adress);
            string res;
            string iconName = AppDomain.CurrentDomain.BaseDirectory + getFileName(home);

            //якась магія, можна попробувать удалить
            WebRequest.DefaultWebProxy = null;
            //System.Net.ServicePointManager.Expect100Continue = false;
            DisableAdapter("Hamachi");
            //

            //res = getResponse(adress);
            //string title = Regex.Match(res, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>", RegexOptions.IgnoreCase).Groups["Title"].Value;
            //byte[] bytes = Encoding.Default.GetBytes(title);
            //title = Encoding.UTF8.GetString(bytes);


            //if (!File.Exists(iconName))
            //{
            //    using (WebClient client = new WebClient())
            //    {
            //        //client.DownloadFileAsync(new Uri(@"https://www.google.com/s2/favicons?domain=" + home); //хай буде, вдруг пригодиться
            //        client.DownloadFile(new Uri(@"https://www.google.com/s2/favicons?domain=" + home), iconName);
            //    }
            //}


            if (!File.Exists(iconName))
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadFileAsync(new Uri(@"https://www.google.com/s2/favicons?domain=" + home), iconName); //хай буде, вдруг пригодиться
                    //client.DownloadFile(new Uri(@"https://www.google.com/s2/favicons?domain=" + home), iconName);
                }
            }

            res = getResponse(adress);
            string title = Regex.Match(res, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>", RegexOptions.IgnoreCase).Groups["Title"].Value;
            byte[] bytes = Encoding.Default.GetBytes(title);
            title = Encoding.UTF8.GetString(bytes);



            Title = title;
            Icon = iconName;

            //GC.Collect();
        }

        private string getFileName(string uri)
        {
            char[] arr = uri.ToCharArray();
            Array.Reverse(arr);

            uri = new string(arr);

            int index = 0;
            string result = @"";
            while (uri[index] != '/')
            {
                result += uri[index];
                index++;
            }

            arr = result.ToCharArray();
            Array.Reverse(arr);
            result = new string(arr);

            for (int i = 0; i < result.Length; i++)
            {
                if (result[i] == '.')
                {
                    result = result.Remove(i, 1);
                }
            }
            result += @".ico";


            Console.WriteLine(result);
            return result;


        }

        private string Home(string uri)
        {
            StringBuilder result = new StringBuilder(@"");
            int index = 0;
            byte count = 0;
            while (count != 3 && index < uri.Length)
            {
                result.Append(uri[index]);
                if (uri[index] == '/') { count++; }
                if(count == 3) { result.Remove(result.Length - 1, 1); }
                index++;

            }
            Console.WriteLine(result);
            return result.ToString();
        }

        private string getResponse(string uri)
        {
            StringBuilder sb = new StringBuilder();
            byte[] buf = new byte[8192];
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);

            request.Proxy = null;
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream resStream = response.GetResponseStream();
            int count = 0;
            do
            {
                count = resStream.Read(buf, 0, buf.Length);
                if (count != 0)
                {
                    sb.Append(Encoding.Default.GetString(buf, 0, count));
                }
            }
            while (count > 0);
            return sb.ToString();
        }

        public static void DisableAdapter(string interfaceName)
        {
                System.Diagnostics.ProcessStartInfo psi =
                    new System.Diagnostics.ProcessStartInfo("netsh", "interface set interface \"" + interfaceName + "\" disable");
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                psi.UseShellExecute = true; 
                psi.Verb = "runas";
                p.StartInfo = psi;
                p.Start();
        }
        
        public static bool isExist(string href)
        {
            //bool result = true;
            //DisableAdapter("Hamachi");

            //try
            //{
            //    WebRequest webRequest = WebRequest.Create(href);
            //    webRequest.Timeout = 500; // miliseconds
            //    webRequest.Method = "HEAD";
            //    webRequest.GetResponse();
            //}
            //catch
            //{
            //    result = false;
            //}

            //return result;

            DisableAdapter("Hamachi");

            //if (href.Contains("https://"))
            //{
            //    href = href.Remove(href.IndexOf("https://"), "https://".Length);
            //}


            //bool br = false;
            //try
            //{
            //    IPHostEntry ipHost = Dns.GetHostEntry(href);
            //    br = true;
            //}
            //catch (SocketException se)
            //{
            //    br = false;
            //}
            //return br;


            try
            {
                WebClient wc = new WebClient();
                string HTMLSource = wc.DownloadString(href);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    

        public string Adress { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }

    }
}
