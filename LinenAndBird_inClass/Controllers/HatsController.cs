using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinenAndBird_inClass.DataAccess;
using LinenAndBird_inClass.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinenAndBird_inClass.Controllers
{
    [Route("api/[controller]")] //exposed at this endpoint
    [ApiController] //an api controller, so it returns json or xml
    public class HatsController : ControllerBase

    {
        private HatRepository _repo;

        public HatsController()
        {
            _repo = new HatRepository();
        }
        

        [HttpGet]
        public List<Hat> GetAllHats()
        {
            //var repo = new HatRepository(); //don't need this now bc of field _repo
            //return _hats;
            return _repo.GetAll();
        }
        //GET /api/hats/styles/1 -> gets all openBackHats
        //
        [HttpGet("styles/{style}")] // like js template literals
        //he'll "pretend" there's  hat class for now
        // that way you don't get as distrcated trying to create a new file

        public List<Hat> GetHatsByStyle(HatStyle style) //could call IEnumerable instead of List<>
        {
            var matches = _repo.GetHatsByStyle(style);
            //var matches = _hats.Where(hat => hat.Style == style);
            return matches.ToList();  //little bit more expensive
        }

        [HttpPost] //for adding new data whereas put for total replacement
        public void AddHat(Hat newHat)
        {
            var repo = new HatRepository();
            _repo.Add(newHat); //using private field for repo now
            //_hats.Add(newHat);
        }
    }
}
