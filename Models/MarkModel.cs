using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace KOLHOZ_Marker.Models
{
    class MarkModel
    {

        public MarkModel()
        {
            //Icon = new Image();
            //var uri = new Uri(@"pack://application:,,,/Resourses\WhiteTest.png");
            //var bitmap = new BitmapImage(uri);
            //Icon.Source = bitmap;
            Icon = @"pack://application:,,,/Resourses\WhiteTest.png";
            Title = "test";
        }

        //public Image Icon { get; set; }
        public string Icon { get; set; }
        public string Title { get; set; }


    }
}
