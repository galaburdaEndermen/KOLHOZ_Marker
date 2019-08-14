using KOLHOZ_Marker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace KOLHOZ_Marker.VievModels
{
    class MainVievModel
    {
        public MainVievModel()
        {
            Marks = new ObservableCollection<MarkModel>();
            Marks.Add(new MarkModel());
            Marks.Add(new MarkModel());
            Marks.Add(new MarkModel());

            Tags = new ObservableCollection<TagModel>();
            Tags.Add(new TagModel());
            Tags.Add(new TagModel());
            Tags.Add(new TagModel());

            //var uri = new Uri(@"pack://application:,,,/Resourses\Cog.png");
            //var bitmap = new BitmapImage(uri);
        }


        public ObservableCollection<MarkModel> Marks { get; set; }
        public ObservableCollection<TagModel> Tags { get; set; }
    }
}
