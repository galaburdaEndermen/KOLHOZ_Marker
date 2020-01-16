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
            var sm = new SaveManager(@"tags.txt", @"marks.txt");
            var savedTags = sm.getTags();

            TagModel.Checked += Tags_CollectionChanged;
            foreach (var str in savedTags)
            {
                tags.Add(new TagModel(str));
            }

            Marks = new ObservableCollection<MarkModel>();
            var savedMarks = sm.getMarks();

            foreach (var str in savedMarks)
            {
                string title = str.Split('|')[0];
                string tags = str.Split('|')[1];
                string href = str.Split('|')[2];
                string icon = str.Split('|')[3];
                //var tmp = new MarkModel(Tags, marks, title, href, @icon);
                var tmp = new MarkModel(Tags, marks, title);
                tmp.Href = href;
                tmp.Icon = icon;

                tmp.setSelected(tags);
                marks.Add(tmp);

                RaisePropertyChanged("Marks");
            }



            isFiltering = false;

            add_Mark = new Command(addMark);
            openCommand = new Command(OpenPage);
            editCommand = new Command(editTags);
            delete = new Command(deleteMark);
            setings = new Command(startSetings);
        }

        private void Tags_CollectionChanged()
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
                Width = 325,
                ResizeMode = ResizeMode.NoResize,
                WindowStyle = WindowStyle.None,
                DataContext = new MarkAddingVievModel()
            };
            if (dialog.ShowDialog() == true)
            {
                marks.Add(new MarkModel(this.Tags, this.Marks, (dialog.DataContext as MarkAddingVievModel).Title, (dialog.DataContext as MarkAddingVievModel).Href, (dialog.DataContext as MarkAddingVievModel).Icon));
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
        
        private Command setings;
        public Command Setings { get { return setings; } }
        private void startSetings(object o)
        {
            Vievs.Setings dialog = new Vievs.Setings
            {
                Owner = main,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Height = 350,
                Width = 400,
                ResizeMode = ResizeMode.NoResize,
                WindowStyle = WindowStyle.None,
                DataContext = new SetingsVievModel(tags)
            };
            dialog.ShowDialog();
            //tags.Add(new TagModel("null", Tags_CollectionChanged));
            //tags.Remove(tags.Last<TagModel>());

            //RaisePropertyChanged("Tags");
        }


        public void set()
        {
            SaveManager sv = new SaveManager(@"tags.txt", @"marks.txt");
            sv.setSave(tags, marks);
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
