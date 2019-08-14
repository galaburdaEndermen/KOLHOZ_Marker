using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace KOLHOZ_Marker.Models
{
    class TagModel
    {
        public TagModel()
        {
            TagColor = new SolidColorBrush((Color.FromRgb(221, 35, 35)));
            TagName = "test";
        }

        public SolidColorBrush TagColor { get; set; }
        public string TagName { get; set; }
    }
}
