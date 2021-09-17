using System;
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
            //return Ok(_repo.GetAll()); //why doesn't this work?
            return _repo.GetAll();
        }
        
        [HttpGet("{id}")]
        public IActionResult GetBirdById(Guid id)
        {
            var bird = _repo.GetById(id);

            if (bird == null)
            {
                return NotFound($"No bird with the id {id} was found.");
            }

            return Ok(bird);

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
            return Created($"/api/birds/{newBird.Id}", newBird);
            //if we had an api for GetSingleBird we wouldn't need "/api..." above
            // Created is a more specific "things are good" response
        }

        //api/birds/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteBird(Guid id)
        {
            _repo.Remove(id);

            return Ok();
        }

        //api/birds/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateBird(Guid id, Bird bird)
        {
            var birdToUpdate = _repo.GetById(id);

            if (birdToUpdate == null)
            {
                return NotFound($"Cound not find bird with the id: {id} to update");
            }

            var updatedBird = _repo.Update(id, bird);

            return Ok(updatedBird);
        }

    }
}
