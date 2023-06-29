using Microsoft.AspNetCore.Mvc;
using NNGroup_DataManager.DataAccess;
using ShareModels.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NNGroup_DataManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewClaimController : ControllerBase
    {
        public readonly IClaimDataAccess _context;
        // GET: api/<ViewClaimController>
        public ViewClaimController(IClaimDataAccess context)
        {
            _context = context;
        }
        

        // GET api/<ViewClaimController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Get(int id)
        {
            Claim claim = _context.ViewClaim(id)!;

            if (claim == null)
                return NotFound();
            else
                return Ok(claim);
        }

    }
}
