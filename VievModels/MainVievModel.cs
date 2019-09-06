using KOLHOZ_Marker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace KOLHOZ_Marker.VievModels
{
    class MainVievModel : INotifyPropertyChanged
    {
        public MainVievModel()
        {
            tags = new Dictionary<int, TagModel>();
            tags.Add("test1".GetHashCode(), new TagModel("test1"));
            tags.Add("test2".GetHashCode(), new TagModel("test2"));
            tags.Add("test3".GetHashCode(), new TagModel("test3"));
            tags.Add("test4".GetHashCode(), new TagModel("test4"));

            Marks = new ObservableCollection<MarkModel>();
            Marks.Add(new MarkModel(Tags));
            Marks.Add(new MarkModel(Tags));
            Marks.Add(new MarkModel(Tags));

            isFiltering = false;

            //var uri = new Uri(@"pack://application:,,,/Resourses\Cog.png");
            //var bitmap = new BitmapImage(uri);
        }


        private ObservableCollection<MarkModel> marks;
        public ObservableCollection<MarkModel> Marks //зробить фільтрацію лінгом
        {
            get
            {
                if(IsFiltering)
                {
                    bool isTagsCheked = false;
                    foreach(var t in tags)
                    {
                        if (t.Value.IsCheked) { isTagsCheked = true; }
                    }
                    if(isTagsCheked)
                    {
                        return new ObservableCollection<MarkModel>((from item in marks where item.isAppropriate() && item.Title.Contains(FilterText) select item).ToList());
                    }
                    else
                    {
                        return new ObservableCollection<MarkModel>((from item in marks where item.Title.Contains(FilterText) select item).ToList());
                    }
                    
                }
                else
                {
                    return marks;
                }
            }
            set { marks = value; }
        } 
      

        private bool isFiltering;
        private string filterText;
        public string FilterText
        {
            get { return filterText; }
            set { filterText = value; RaisePropertyChanged("Marks"); }
        }
        public bool IsFiltering
        {
            get { return isFiltering || !(string.IsNullOrEmpty(FilterText)); }
            set { isFiltering = value;  }
        }
      
        public Dictionary<int, TagModel> tags;

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
