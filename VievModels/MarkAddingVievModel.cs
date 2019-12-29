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
using System.Windows;
using KOLHOZ_Marker.Models;

namespace KOLHOZ_Marker.VievModels
{
    class MarkAddingVievModel : INotifyPropertyChanged
    {

        public MarkAddingVievModel()
        {
            icon = @"pack://application:,,,/Resourses\WhiteTest.png";
            Exist = true;

            Href = Clipboard.GetText();

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
               
                if (HtmlRequester.isExist(href))
                {
                    Exist = true;
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
