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
        public MainVievModel(Window main)
        {

            this.main = main;
            tags = new ObservableCollection<TagModel>();
            tags.Add(new TagModel("test1", Tags_CollectionChanged));
            tags.Add(new TagModel("test2", Tags_CollectionChanged));
            tags.Add(new TagModel("test3", Tags_CollectionChanged));
            tags.Add(new TagModel("test4", Tags_CollectionChanged));

            Marks = new ObservableCollection<MarkModel>();
            Marks.Add(new MarkModel(Tags, marks, main , "1"));
            Marks.Add(new MarkModel(Tags, marks, main , "2"));
            Marks.Add(new MarkModel(Tags, marks, main , "3"));
         
            isFiltering = false;
            add_Mark = new Command(addMark);
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
                        if ((FilterText != "") && (FilterText != null))
                        {
                            return new ObservableCollection<MarkModel>((from item in marks where item.isAppropriate() && item.Title.Contains(FilterText) select item).ToList());
                        }
                        else
                        {
                            return new ObservableCollection<MarkModel>((from item in marks where item.isAppropriate() select item).ToList());
                        }
                        
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
                return tags;
            }
            set
            {
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


        public Window main;

        private Command add_Mark;
        public Command AddMark { get { return add_Mark; } }
        KOLHOZ_Marker.Vievs.MarkAdding setWindow;
    
        void addMark(object o)
        {
            if (setWindow == null)
            {
                setWindow = new KOLHOZ_Marker.Vievs.MarkAdding
                {
                    //DataContext = new SettingsVievModel(Timers)
                    Owner = main
                };
                setWindow.ShowDialog();
                setWindow = null;
                GC.Collect();
            }
        }

    }
}
