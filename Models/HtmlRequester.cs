using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;

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

            if (!File.Exists(iconName))
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadFileAsync(new Uri(@"https://www.google.com/s2/favicons?domain=" + home), iconName);
                }
            }

            res = getResponse(adress);
            string title = Regex.Match(res, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>", RegexOptions.IgnoreCase).Groups["Title"].Value;
            byte[] bytes = Encoding.Default.GetBytes(title);
            title = Encoding.UTF8.GetString(bytes);

            Title = title;
            Icon = iconName;
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
            WebClient wc = new WebClient();
            return wc.DownloadString(uri);
        }

        public static void DisableAdapter(string interfaceName)
        {
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("netsh", "interface set interface \"" + interfaceName + "\" disable");
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            psi.UseShellExecute = true; 
            psi.Verb = "runas";
            p.StartInfo = psi;
            p.Start();
        }
        
        public static bool isExist(string href)
        {
            try
            {
                //Creating the HttpWebRequest
                HttpWebRequest request = WebRequest.Create(href) as HttpWebRequest;
                //Setting the Request method HEAD, you can also use GET too.
                request.Method = "HEAD";
                //Getting the Web Response.
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //Returns TRUE if the Status code == 200
                response.Close();
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static Thread jammer;

        public static void startJamming()
        {
            if (jammer == null)
            {
                jammer = new Thread(new ThreadStart(jamming));
                jammer.Name = "Jammer";
                jammer.Start();
            }
            else
            {
                if (jammer.ThreadState == ThreadState.Stopped)
                {
                    jammer.Resume();
                }
            }
        }
        public static void stopJamming()
        {
            if (jammer != null)
            {
                jammer.Suspend();
            }
        }

        private static void jamming()
        {
            DisableAdapter("Hamachi");
            Thread.Sleep(10000);
        }


        public string Adress { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }

    }
}
