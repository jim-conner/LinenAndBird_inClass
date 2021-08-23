using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinenAndBird_inClass.Models
{
    public class Hat
    {
        public Guid Id { get; set; }
        public string Designer { get; set; }
        public string Color { get; set; }
        public HatStyle Style { get; set; } = HatStyle.Normal;

    }

    public enum HatStyle
    {
        Normal, //... = HatStyle.Normal; does the same on 12
        OpenBack,
        WideBrim
    }
}
