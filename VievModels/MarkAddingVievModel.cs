using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using KOLHOZ_Marker.Models;

namespace KOLHOZ_Marker.VievModels
{
    class MarkAddingVievModel : INotifyPropertyChanged
    {

        public MarkAddingVievModel()
        {
            icon = @"pack://application:,,,/Resourses\WhiteTest.png";
            Exist = true;
        }


        private string title;
        public string Title { 
            get { return title; } 
            set { title = value; RaisePropertyChanged("Title"); }
        }

        private string href;
        public string Href 
        { 
            get 
            { 
                return href;
            } 
            set 
            {
                href = value; 
                RaisePropertyChanged("Href");
               
                if (isExist(href))
                {
                    HtmlRequester request = new HtmlRequester(href);
                    Title = request.Title;
                    Icon = request.Icon;
                }
                else
                {
                    Exist = false;
                }

            }
        }

        private string icon;
        public string Icon { get { return icon; } set { icon = value; RaisePropertyChanged("Icon"); } }

        private bool exist;
        public bool Exist 
        { 
            get { return exist; }
            set { exist = value; RaisePropertyChanged("Exist"); }
        }












        //bool isExist(string url)
        //{
        //    WebRequest webRequest = WebRequest.Create(url);
        //    //webRequest.Timeout = 200;
        //    WebResponse webResponse;
        //    try
        //    {
        //        webResponse = webRequest.GetResponse();
        //    }
        //    catch //If exception thrown then couldn't get response from address
        //    {
        //        return false;
        //    }
        //    return true;
        //}

        //bool isExist(string url)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    byte[] buf = new byte[8192];
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        //    Stream resStream = response.GetResponseStream();

        //    return resStream.Read(buf, 0, buf.Length) > 0;
        //}

        bool isExist(string url)
        {
            //WebClient client = new WebClient();
            //client.Proxy = null;
            //string res;
            //try
            //{
            //    res = client.DownloadString(url);

            //}
            //catch (Exception e)
            //{
            //    string ms = e.ToString();
            //    throw;
            //}

            //return res.Length > 0;


            return true;
        }

        //}
        //async void isExist(string url)
        //{
        //    // Call asynchronous network methods in a try/catch block to handle exceptions.
        //    HttpClient client = new HttpClient();
        //    try
        //    {
        //        HttpResponseMessage response = await client.GetAsync(url);
        //        response.EnsureSuccessStatusCode();
        //        string responseBody = await response.Content.ReadAsStringAsync();
        //        // Above three lines can be replaced with new helper method below
        //        // string responseBody = await client.GetStringAsync(uri);

            //        Console.WriteLine(responseBody);
            //    }
            //    catch (HttpRequestException e)
            //    {
            //        Console.WriteLine("\nException Caught!");
            //        Console.WriteLine("Message :{0} ", e.Message);
            //    }


            //}



            //public bool isExist(string url)
            //{
            //    try
            //    {
            //        var request = WebRequest.Create(url);
            //        request.Timeout = 5000;
            //        request.Method = "HEAD";

            //        using (var response = (HttpWebResponse)request.GetResponse())
            //        {
            //            response.Close();
            //            return response.StatusCode == HttpStatusCode.OK;
            //        }
            //    }
            //    catch (Exception exception)
            //    {
            //        return false;
            //    }
            //}


            //public bool isExist(string url)
            //{
            //    var uri = new Uri(url);

            //    using (HttpClient Client = new HttpClient())
            //    {

            //        HttpResponseMessage result = Client.GetAsync(uri).Result;
            //        HttpStatusCode StatusCode = result.StatusCode;

            //        switch (StatusCode)
            //        {

            //            case HttpStatusCode.Accepted:
            //                return true;
            //            case HttpStatusCode.OK:
            //                return true;
            //            default:
            //                return false;
            //        }
            //    }
            //}

            //public bool isExist(string url)
            //{
            //    StringBuilder sb = new StringBuilder();
            //    byte[] buf = new byte[8192];
            //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //    Stream resStream = response.GetResponseStream();
            //    int count = 0;
            //    do
            //    {
            //        count = resStream.Read(buf, 0, buf.Length);
            //        if (count != 0)
            //        {
            //            sb.Append(Encoding.Default.GetString(buf, 0, count));
            //        }
            //    }
            //    while (count > 0);
            //    return sb.ToString().Length > 0;

            //}


            //public bool isExist(string url)
            //{
            //    IPAddress[] addresses = Dns.GetHostAddresses(url);




            //    Ping ping = new Ping();

            //    try
            //    {
            //        PingReply reply = ping.Send(addresses[1], 2000);

            //        if (reply.Status == IPStatus.Success) return true;
            //        else return false;
            //    }
            //    catch (PingException e)
            //    {
            //        string mes = e.ToString();
            //        return false;
            //    }

            //}

            //public bool isExist(string url)
            //{
            //    IPHostEntry hostInfo = Dns.Resolve(url);
            //    // Get the IP address list that resolves to the host names contained in the 
            //    // Alias property.
            //    IPAddress[] address = hostInfo.AddressList;
            //    // Get the alias names of the addresses in the IP address list.
            //    String[] alias = hostInfo.Aliases;

            //    Console.WriteLine("Host name : " + hostInfo.HostName);
            //    Console.WriteLine("\nAliases : ");
            //    for (int index = 0; index < alias.Length; index++)
            //    {
            //        Console.WriteLine(alias[index]);
            //    }
            //    Console.WriteLine("\nIP Address list :");
            //    for (int index = 0; index < address.Length; index++)
            //    {
            //        Console.WriteLine(address[index]);


            //    }
            //    return true;
            //}

            //public bool isExist(string url)
            //{


            //    string hostname = url, message = "IP адреса для домена " + hostname + "\n";
            //    IPHostEntry entry = Dns.GetHostEntry(hostname);

            //    foreach (IPAddress a in entry.AddressList)
            //    {
            //        message += "  --> " + a.ToString() + "\n";
            //    }
            //    return message.Length > 0;
            //}





        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
