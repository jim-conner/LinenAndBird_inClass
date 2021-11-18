using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinenAndBird_inClass.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace LinenAndBird_inClass.Controllers
{
    [Route("api/badges")]
    //[Route("api/rower/rowerId/badges")] for rower's profile
    //^^ this what Chistopher said their endpoint would actually look like

    [ApiController]

    public class BadgesController : Controller
    {
        private BadgeRepository _repo;

        public BadgesController(BadgeRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetAllUserBadges(Guid userId)
        {
            return Ok(_repo.GetByUserId(userId));
        }
    }
}
