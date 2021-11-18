using System;
using System.Collections.Generic;
using LinenAndBird_inClass.DataAccess;
using LinenAndBird_inClass.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace LinenAndBird_inClass.Controllers
{
    //[Route("api/[controller]")] //this is default synxtax for route
    [Route("api/birds")] // nathan prefers this syntax for specificity
    [ApiController]
    [Authorize] //works on an indv method basis, so you'd just tag that one controller method
    // ^^everything inside of this controller you need to be logged in to use this api

    //[AllowAnoyomous] allows the given method to be sent but not required
    public class BirdsController : FirebaseBaseController
    {
        private BirdRepository _repo; //nathan leaves off "private"

        //this is asking ASP.NET for the application configuration
        //this is known as Dependency Injection

    /*
        public BirdsController(IConfiguration config) // don't need IConfiguration config
        {
            var connectionString = config.GetConnectionString("LinenAndBird");
            _repo = new BirdRepository(connectionString); //dependency of BirdController
        }
    */

            //this is asking ASP.NET for the application configuration
            public BirdsController(BirdRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [AllowAnonymous] //see note above here is an example for the GetAll call
        public IActionResult GetAllBirds()
        {
            //var fbUserId = User.FindFirst(claim => claim.Type == "user id").Value;
            // instead of using this clunky version ^^ we can create a new base controller!!

            //var uid = GetFirebaseUid(); // don't even need this 
            var uid = FirebaseUid;
            return Ok(_repo.GetAll()); //why doesn't this work? - it does  though?
            //return _repo.GetAll();
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
