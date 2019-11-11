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
            WebRequest.DefaultWebProxy = null;
           
            Adress = adress;
            string home = Home(adress);
            string res;
            System.Net.ServicePointManager.Expect100Continue = false;

            DisableAdapter("Hamachi");

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

            if (!(File.Exists(AppDomain.CurrentDomain.BaseDirectory + getFileName(home))))
            {
                using (WebClient client = new WebClient())

                {
                    //client.Proxy = null;
                    try
                    {
                        string one = @"https://www.google.com/s2/favicons?domain=" + home;
                        string two = AppDomain.CurrentDomain.BaseDirectory + getFileName(home);
                        //client.DownloadFileAsync(new Uri(one), two);
                        client.DownloadFile(new Uri(one), two);
                    }
                    catch (Exception e)
                    {
                        string ms = e.ToString();
                        throw;
                    }

                }
            }
            else
            {

            }


            ///////////////////////

            //HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://www.google.com/s2/favicons?domain=" + home);
            //HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            //Stream stream = resp.GetResponseStream();
            //FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + getFileName(home), FileMode.Create, FileAccess.Write);
            //StreamWriter write = new StreamWriter(file);
            //int b;
            //for (int i = 0; ; i++)
            //{
            //    b = stream.ReadByte();
            //    if (b == -1) break;
            //    write.Write(b);
            //}
            //write.Close();
            //file.Close();



            //Console.WriteLine("***end***");
            //Console.ReadKey();


            ///////////////////////

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
            //result = result.Remove(result.IndexOf("www"), 3);
            //result = result.Remove(result.IndexOf("com"), 3);
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

            //WebClient x = new WebClient();
            ////x.Proxy = null;
            //string source = x.DownloadString(uri);
            //return source;

        }

        public static void DisableAdapter(string interfaceName)
        {
            try
            {

                System.Diagnostics.ProcessStartInfo psi =
                    new System.Diagnostics.ProcessStartInfo("netsh", "interface set interface \"" + interfaceName + "\" disable");
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                //psi.UseShellExecute = false; //?
                psi.UseShellExecute = true; //?
                psi.Verb = "runas";
                //psi.RedirectStandardOutput = true;
                //psi.RedirectStandardError = true;
                p.StartInfo = psi;
                p.Start();
            }
            catch (Exception e)
            {
                string a = e.ToString();
                throw;
            }
        }


        public string Adress { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }

    }
}
