using Microsoft.AspNetCore.Mvc;
using NNGroup_DataManager.DataAccess;
using ShareModels.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NNGroup_DataManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmitClaimController : ControllerBase
    {
        public readonly IClaimDataAccess _context;
        public SubmitClaimController(IClaimDataAccess context)
        {
            _context = context;
        }

        // GET api/<SubmitClaimController>/5
        [HttpGet("{id}")]
        public Claim GetById(int id)
        {
            return _context.ViewClaim(id);
        }

        // POST api/<SubmitClaimController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Claim> Create(Claim claim)
        {
            int newId = _context.MakeClaim(claim);
            return CreatedAtAction(nameof(GetById), new { id = newId }, _context.ViewClaim(newId));
        }
        

        
    }
}
