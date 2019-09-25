using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using KOLHOZ_Marker.Models;
using KOLHOZ_Marker.VievModels;
using KOLHOZ_Marker.Vievs;

namespace KOLHOZ_Marker.Models
{
    class MarkModel
    {

        public MarkModel(ObservableCollection<TagModel> Tags, ObservableCollection<MarkModel> Marks, Window main, string title)
        {
            Icon = @"pack://application:,,,/Resourses\WhiteTest.png";
            Title = title;
            Href = @"https://www.google.com.ua";

            this.Tags = Tags;
            this.marks = Marks;
            SelectedTags = new ObservableCollection<TagModel>();
            Tags_CollectionChanged(null, null);
            this.Tags.CollectionChanged += Tags_CollectionChanged;

            openCommand = new Command(OpenPage);
            editCommand = new Command(editTags);
            delete = new Command(deleteMark);

            Main = main;
        }
        Window Main;


        private void Tags_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if(Tags.Count != SelectedTags.Count)
            {
                ObservableCollection<TagModel> tmp = new ObservableCollection<TagModel>();
                for (int i = 0; i < Tags.Count; i++)
                {
                    tmp.Add(new TagModel(Tags[i]));
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

        public string Icon { get; set; }
        public string Title { get; set; }
        public string Href { get; set; }
        public ObservableCollection<TagModel> Tags { get; set; }
        public ObservableCollection<TagModel> SelectedTags { get; set; }
        private ObservableCollection<MarkModel> marks;

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



        private Command openCommand;
        public Command Open { get { return openCommand; } }
        void OpenPage(object o)
        {
            System.Diagnostics.Process.Start(Href);
        }

        private Command editCommand;
        public Command EditTags { get { return editCommand; } }
        void editTags(object o)
        {

            string parName = o as string;
            if (parName != null)
            {
                for (int i = 0; i < marks.Count;)
                {
                    if (marks[i].Title == parName)
                    {
                        TagEdit dialog = new TagEdit
                        {
                            Owner = Main,
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
                    if (marks[i].ToString() == parName)
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


        public string toString { get { return this.ToString(); } }


        public override string ToString()
        {
            return Title + Href;
        }
    }
}
