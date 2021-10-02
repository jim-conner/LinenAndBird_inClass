using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinenAndBird_inClass.Models
{
    public class Bird
    {
        public Guid Id { get; set; } //unique Id generated for each bird
        public BirdType Type { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string Name { get; set; }

        public IEnumerable<BirdAccessory> Accessories { get; set; } //<string> returns error, change to BirdAccessory type... ALSO LIST NEEDS TO -> IEnumerable
    }

    public class BirdAccessory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid BirdId { get; set; }
    }

    public enum BirdType
    {
        Dead,
        Linen
    }
}
  