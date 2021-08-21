using System.Collections.Generic;
using LinenAndBird_inClass.DataAccess;
using LinenAndBird_inClass.Models;
using Microsoft.AspNetCore.Mvc;

namespace LinenAndBird_inClass.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/birds")] // nathan prefers this syntax for specificity
    [ApiController]
    public class BirdsController : ControllerBase
    {
        private BirdRepository _repo; //nathan leaves off "private"

        public BirdsController()
        {
            _repo = new BirdRepository();
        }

        [HttpGet]
        public IEnumerable<Bird> GetAllBirds()
        {
            //return Ok(_repo.GetAll());
            return _repo.GetAll();
        }

        [HttpPost]
        public IActionResult AddBird(Bird newBird)
        {
            // IsNullOrEmpty is more idiomatic than = "";
            if (string.IsNullOrEmpty(newBird.Name) || string.IsNullOrEmpty(newBird.Color))
            {
                return BadRequest("Name and Color are required fields");
            }

            _repo.Add(newBird);

            //return Ok(); any 2XX response 200-299 is good
            return Created("/api/birds/1", newBird);
            //if we had an api for GetSingleBird we wouldn't need "/api..." above
            // Created is a more specific "things are good" response
        }
    }
}
