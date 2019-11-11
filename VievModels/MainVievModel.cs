using KOLHOZ_Marker.Models;
using KOLHOZ_Marker.Vievs;
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
            Marks.Add(new MarkModel(Tags, marks, "1"));
            Marks.Add(new MarkModel(Tags, marks, "2"));
            Marks.Add(new MarkModel(Tags, marks, "3"));
         
            isFiltering = false;
            add_Mark = new Command(addMark);

            openCommand = new Command(OpenPage);
            editCommand = new Command(editTags);
            delete = new Command(deleteMark);
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
        void addMark(object o)
        {
            MarkAdding dialog = new MarkAdding
            {
                Owner = main,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Height = 350,
                Width = 600,
                ResizeMode = ResizeMode.NoResize,
                WindowStyle = WindowStyle.None,
                DataContext = new MarkAddingVievModel()


            };
            if (dialog.ShowDialog() == true)
            {
                Models.MarkModel newM = new MarkModel(this.Tags, this.Marks, (dialog.DataContext as MarkAddingVievModel).Title); //переробить від новий конструктор і скоротить
                newM.Icon = (dialog.DataContext as MarkAddingVievModel).Icon;
                marks.Add(newM);
            }
            
        }




        private Command openCommand;
        public Command Open { get { return openCommand; } }
        void OpenPage(object o)
        {
            string parName = o as string;
            for (int i = 0; i < marks.Count; i++)
            {
                if (marks[i].toString == parName)
                {
                    System.Diagnostics.Process.Start(marks[i].Href);
                    break;
                }
            }
            
        }

        private Command editCommand;
        public Command EditTags { get { return editCommand; } }
        void editTags(object o)
        {

            string parName = o as string;
            if (parName != null)
            {
                for (int i = 0; i < marks.Count; i++)
                {
                    if (marks[i].toString == parName)
                    {
                        TagEdit dialog = new TagEdit
                        {
                            Owner = main,
                            WindowStartupLocation = WindowStartupLocation.CenterOwner,
                            Height = 350,
                            Width = 400,
                            ResizeMode = ResizeMode.NoResize,
                            WindowStyle = WindowStyle.None,
                            DataContext = new TagEditVievModel(marks[i].SelectedTags)


                        };
                        if (dialog.ShowDialog() == true)
                        {
                            marks[i].SelectedTags = (dialog.DataContext as TagEditVievModel).Tags;
                        }
                        break;
                    }
                }
            }
        }

        private Command delete;
        public Command Delete { get { return delete; } }
        private void deleteMark(object parameter)
        {
            string parName = parameter as string;
            if (parName != null)
            {
                for (int i = 0; i < marks.Count;)
                {
                    if (marks[i].toString == parName)
                    {
                        marks.RemoveAt(i);
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }
    }
}
