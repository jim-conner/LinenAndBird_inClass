using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinenAndBird_inClass.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinenAndBird_inClass.Controllers
{
    [Route("api/[controller]")] //exposed at this endpoint
    [ApiController] //an api controller, so it returns json or xml
    public class HatsController : ControllerBase
    {
        static List<Hat> _hats = new List<Hat>
        {
                new Hat
                {
                    Color = "Blue",
                    Designer = "Jimbo",
                    Style = HatStyle.OpenBack
                },
                new Hat
                {
                    Color = "Black",
                    Designer = "Charlie",
                    Style = HatStyle.WideBrim
                },
                new Hat
                {
                    Color = "Turqoise",
                    Designer = "Nathan",
                    Style = HatStyle.Normal
                }
        };

        [HttpGet]

            
        public List<Hat> GetAllHats()
        {
            return _hats;
        }
        //GET /api/hats/styles/1 -> gets all openBackHats
        [HttpGet("styles/{style}")] // like js template literals
        //he'll "pretend" there's  hat class for now
        // that way you don't get as distrcated trying to create a new file

        public List<Hat> GetHatsByStyle(HatStyle style) //could call IEnumerable instead of List<>
        {
            var matches = _hats.Where(hat => hat.Style == style);
            return matches.ToList();  //little bit more expensive
        }

        [HttpPost] //for adding new data whereas put for total replacement
        public void AddHat(Hat newHat)
        {
            _hats.Add(newHat);
        }
    }
}
