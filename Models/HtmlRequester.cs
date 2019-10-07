using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
            try
            {
                res = getResponse(adress);
            }
            catch (Exception e)
            {
                string mes = e.ToString();
                throw;
            }
            

            string title = Regex.Match(res, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>", RegexOptions.IgnoreCase).Groups["Title"].Value;

            byte[] bytes = Encoding.Default.GetBytes(title);
            title = Encoding.UTF8.GetString(bytes);
            Title = title;

            using (WebClient client = new WebClient())
            {
                //client.Proxy = null;
                try
                {
                    client.DownloadFile(new Uri(@"https://www.google.com/s2/favicons?domain=" + home), AppDomain.CurrentDomain.BaseDirectory + getFileName(home));
                }
                catch (Exception e)
                {
                    string ms = e.ToString();
                    throw;
                }
                
            }
            Icon = AppDomain.CurrentDomain.BaseDirectory + getFileName(home);


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
            result = result.Remove(result.IndexOf("www"), 3);
            result = result.Remove(result.IndexOf("com"), 3);
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
            //request.Method = "HEAD";
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


        public string Adress { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }

    }
}
