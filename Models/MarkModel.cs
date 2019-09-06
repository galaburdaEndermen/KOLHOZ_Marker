using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace KOLHOZ_Marker.Models
{
    class MarkModel
    {

        public MarkModel(ObservableCollection<TagModel> Tags)
        {
            //Icon = new Image();
            //var uri = new Uri(@"pack://application:,,,/Resourses\WhiteTest.png");
            //var bitmap = new BitmapImage(uri);
            //Icon.Source = bitmap;
            Icon = @"pack://application:,,,/Resourses\WhiteTest.png";
            Title = "test";

            this.Tags = Tags;
            SelectedTags = new ObservableCollection<TagModel>();
            Tags_CollectionChanged(null, null);
            this.Tags.CollectionChanged += Tags_CollectionChanged;
        }

        private void Tags_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if(Tags.Count != SelectedTags.Count)
            {
                ObservableCollection<TagModel> tmp = new ObservableCollection<TagModel>();
                for (int i = 0; i < Tags.Count; i++)
                {
                    tmp.Add(Tags[i]);
                    if(SelectedTags.Count > i)// провіряю, чи є такий індекс в SelectedTags
                    {
                        if(SelectedTags[i].IsCheked)
                        {
                            tmp[i].IsCheked = true;
                        }
                    }
                    else
                    {
                        tmp[i].IsCheked = false;
                    }
                }
                SelectedTags = tmp;
                //GC.Collect();
            }
        }

        //public Image Icon { get; set; }
        public string Icon { get; set; }
        public string Title { get; set; }
        public ObservableCollection<TagModel> Tags { get; set; }
        public ObservableCollection<TagModel> SelectedTags { get; set; }

        public bool isAppropriate()
        {
            bool toReturn = true;
            for (int i = 0; i < Tags.Count; i++)
            {
                if(Tags[i].IsCheked && !SelectedTags[i].IsCheked)
                {
                    toReturn = false;
                }
            }
            return toReturn;
        }


    }
}
