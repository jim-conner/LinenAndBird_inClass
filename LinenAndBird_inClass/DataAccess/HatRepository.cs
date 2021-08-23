using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinenAndBird_inClass.Models;

namespace LinenAndBird_inClass.DataAccess
{
    public class HatRepository
    {
        static List<Hat> _hats = new List<Hat>
        {
                new Hat
                {
                    Id = Guid.NewGuid(),
                    Color = "Blue",
                    Designer = "Jimbo",
                    Style = HatStyle.OpenBack
                },
                new Hat
                {
                    Id = Guid.NewGuid(),
                    Color = "Black",
                    Designer = "Charlie",
                    Style = HatStyle.WideBrim
                },
                new Hat
                {
                    Id = Guid.NewGuid(),
                    Color = "Turqoise",
                    Designer = "Nathan",
                    Style = HatStyle.Normal
                }
        };

        internal Hat GetById(Guid hatId)
        {
            return _hats.FirstOrDefault(hat => hat.Id == hatId); 
        }

        internal List<Hat> GetAll()
        {
            return _hats;
        }

        internal IEnumerable<Hat> GetHatsByStyle(HatStyle style)
        {
            return _hats.Where(hat => hat.Style == style);
        }

        internal void Add(Hat newHat)
        {
            newHat.Id = Guid.NewGuid();

            _hats.Add(newHat);
        }

    }
}
