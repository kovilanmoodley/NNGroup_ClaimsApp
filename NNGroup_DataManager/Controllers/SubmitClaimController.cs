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


        // POST api/<SubmitClaimController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Create(ClaimRequest claimRequest)
        {
            int newId = _context.MakeClaim(claimRequest);
            if (newId == -1)
                return BadRequest("Employee or Client List is empty");
            if (newId == -2)
                return NotFound("Client ID not found");
            return Ok($"Claim created successfull. Your claim ID is {newId}");
        }
        

        
    }
}
