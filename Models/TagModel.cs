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
        public bool IsCheked { get; set; }

        public TagModel(string s)
        {
            TagName = s;
            IsCheked = false;
        }

        public override int GetHashCode()
        {
            return TagName.GetHashCode();
        }
    }
}
