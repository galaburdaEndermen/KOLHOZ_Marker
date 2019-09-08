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
            tags = new ObservableCollection<TagModel>();
            tags.Add(new TagModel("test1", Tags_CollectionChanged));
            tags.Add(new TagModel("test2", Tags_CollectionChanged));
            tags.Add(new TagModel("test3", Tags_CollectionChanged));
            tags.Add(new TagModel("test4", Tags_CollectionChanged));

            Marks = new ObservableCollection<MarkModel>();
            Marks.Add(new MarkModel(Tags));
            Marks.Add(new MarkModel(Tags));
            Marks.Add(new MarkModel(Tags));

            isFiltering = false;
            
        }

        

        private void Tags_CollectionChanged() // на лямбду помінять?
        {
            RaisePropertyChanged("Marks");
        }

        private ObservableCollection<MarkModel> marks;
        public ObservableCollection<MarkModel> Marks 
        {
            get
            {
                if(IsFiltering)
                {
                    bool isTagsCheked = false;
                    foreach(var t in tags)
                    {
                        if (t.IsCheked) { isTagsCheked = true; }
                    }
                    if(isTagsCheked)
                    {
                        return new ObservableCollection<MarkModel>((from item in marks where item.isAppropriate() && item.Title.Contains(FilterText) select item).ToList());
                    }
                    else
                    {
                        if((FilterText != "") && (FilterText != null))
                        {
                            return new ObservableCollection<MarkModel>((from item in marks where item.Title.Contains(FilterText) select item).ToList());
                        }
                        else
                        {
                            return marks;
                        }
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
      
        public ObservableCollection<TagModel> tags;

        public ObservableCollection<TagModel> Tags

        {
            get
            {
                //return new ObservableCollection<TagModel>((from item in tags select item.Value).ToList());
                //var toReturn = new ObservableCollection<TagModel>((from item in tags select item.Value).ToList());
                //toReturn.Col

                return tags;

            }
            set
            {
                //tags = value.ToDictionary( t => t.GetHashCode());
                //RaisePropertyChanged("Tags");
                tags = value;
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
