using KOLHOZ_Marker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOLHOZ_Marker.VievModels
{
    class SetingsVievModel : INotifyPropertyChanged
    {
        public SetingsVievModel(ObservableCollection<TagModel> tags)
        {
            Tags = tags;
            ToAdd = "";

            delete = new Command(deleteTag);
            add = new Command(addTag);
        }

        public ObservableCollection<TagModel> Tags { get; set; }

        private string toAdd;
        public string ToAdd { get { return toAdd; } set { toAdd = value; RaisePropertyChanged("ToAdd"); } }

        private Command delete;
        public Command Delete { get { return delete; } }
        private void deleteTag(object parameter)
        {
            string parName = parameter as string;
            if (parName != null)
            {
                for (int i = 0; i < Tags.Count;)
                {
                    if (Tags[i].TagName == parName)
                    {
                        Tags.RemoveAt(i);

                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }

        private Command add;
        public Command Add { get { return add; } }
        private void addTag(object o)
        {
            if (!string.IsNullOrWhiteSpace(ToAdd))
            {
                Tags.Add(new TagModel(ToAdd));
                ToAdd = "";
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
