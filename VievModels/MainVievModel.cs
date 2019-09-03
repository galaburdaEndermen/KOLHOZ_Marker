using KOLHOZ_Marker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

            tags = new Dictionary<int, TagModel>();
            tags.Add("test1".GetHashCode(), new TagModel("test1"));
            tags.Add("test2".GetHashCode(), new TagModel("test2"));
            tags.Add("test3".GetHashCode(), new TagModel("test3"));
            tags.Add("test4".GetHashCode(), new TagModel("test4"));

            isFiltering = false;
        
        


            //var uri = new Uri(@"pack://application:,,,/Resourses\Cog.png");
            //var bitmap = new BitmapImage(uri);
        }


        public ObservableCollection<MarkModel> Marks { get; set; } //зробить фільтрацію лінгом
        //public ObservableCollection<TagModel> Tags { get; set; }
        public Dictionary<int, TagModel> tags;

        public bool isFiltering;
        public string FilterText { get; set; }


        public ObservableCollection<TagModel> Tags

        {
            get
            {
                return new ObservableCollection<TagModel>((from item in tags select item.Value).ToList());
            }
            set
            {
                tags = value.ToDictionary( t => t.GetHashCode());
            }
        }
    }
}
