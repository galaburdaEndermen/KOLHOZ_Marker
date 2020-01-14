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
    class TagEditVievModel : INotifyPropertyChanged
    {
        public TagEditVievModel(ObservableCollection<TagModel> tags)
        {
            ObservableCollection<TagModel> tmp = new ObservableCollection<TagModel>();
            for (int i = 0; i < tags.Count; i++)
            {
                tmp.Add(new TagModel(tags[i]));
              
                tmp[i].IsCheked = tags[i].IsCheked;
                
            }
            this.tags = tmp;
        }

        ObservableCollection<TagModel> tags;
        public ObservableCollection<TagModel> Tags { get { return tags; } }

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
