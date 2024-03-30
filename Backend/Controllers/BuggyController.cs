using Abackend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers {
    public class BuggyController : BaseApiController {
        private readonly ApplicationDbContext _context;
        public BuggyController(ApplicationDbContext context) {
            _context = context;
        }

      

        [HttpGet("notfound")]
        public IActionResult GetNotFoundRequest() {
            var thing = _context.Products.Find(42);

            if(thing == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpGet("servererror")]
        public IActionResult GetServerError() {
            var thing = _context.Products.Find(42);
            var thingToReturn = thing.ToString();
            return Ok();
        }

        [HttpGet("badrequest")]
        public IActionResult GetBadRequest() {
             
            return Ok();
        }

        [HttpGet("badrequest/{id}")]
        public IActionResult GetNotFoundRequest(int id) {

            return Ok();
        }

       
    }
}
