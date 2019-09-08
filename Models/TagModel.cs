using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOLHOZ_Marker.Models
{
    class TagModel
    {
        public string TagName { get; set; }

        private bool isCheked;
        public bool IsCheked
        {
            get { return isCheked; }
            set { isCheked = value; Checked(); }
        }

        public TagModel(string s, TagStateHandler h)
        {
            Checked = h;
            TagName = s;
            IsCheked = false; 
        }

        public TagModel(TagModel another) // copy
        {
            this.Checked = another.Checked;
            this.TagName = another.TagName;
           
            IsCheked = false;
        }

        public override int GetHashCode()
        {
            return TagName.GetHashCode();
        }

        public delegate void TagStateHandler();
        public event TagStateHandler Checked;
    }
}
