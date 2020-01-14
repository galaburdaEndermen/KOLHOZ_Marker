using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOLHOZ_Marker.Models
{
    class SaveManager
    {
        public SaveManager(string tags, string marks)
        {
            Tags = tags;
            Marks = marks;
        }

        private string Tags;
        private string Marks;

        public List<string> getMarks()
        {
            if (File.Exists(Marks))
            {
                List<string> toReturn = new List<string>();
                using (StreamReader sr = new StreamReader(Marks))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        toReturn.Add(line);
                    }
                }
                return toReturn;
            }
            else
            {
                System.IO.File.Create(Marks);
                System.Windows.MessageBox.Show("Configuration file doesn't exist, please, configurate programm by yourself.");
                return new List<string>();
            }
        }

        public List<string> getTags()
        {
            if (File.Exists(Tags))
            {
                List<string> toReturn = new List<string>();
                using (StreamReader sr = new StreamReader(Tags))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        toReturn.Add(line);
                    }
                }
                return toReturn;
            }
            else
            {
                System.IO.File.Create(Tags);
                System.Windows.MessageBox.Show("Configuration file doesn't exist, please, configurate programm by yourself.");
                return new List<string>();
            }
        }


        public void setSave(ObservableCollection<TagModel> tags, ObservableCollection<MarkModel> marks)
        {
            using (StreamWriter sw = new StreamWriter(Tags))
            {
                foreach (var tag in tags)
                {
                    sw.WriteLine(tag.TagName);
                }
            }

            using (StreamWriter sw = new StreamWriter(Marks))
            {
                foreach (var mark in marks)
                {
                    sw.WriteLine(mark.ToString());
                }
            }
        }

    }
}
