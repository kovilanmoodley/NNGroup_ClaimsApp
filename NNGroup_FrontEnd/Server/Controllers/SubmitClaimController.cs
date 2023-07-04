using Microsoft.AspNetCore.Mvc;
using NNGroup_FrontEnd.Server.DataAccess;
using ShareModels.Models;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NNGroup_FrontEnd.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmitClaimController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IClaimDataAccess _context;
        public SubmitClaimController(IConfiguration config,IClaimDataAccess context)
        {
            _config = config;
            _context = context;
        }


        // POST api/<SubmitClaimController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Claim> Create(ClaimRequest claimRequest)
        {
            int newID = _context.MakeClaim(claimRequest);
            if (newID == -1)
                return BadRequest("Employee or Client List is empty");
            if (newID == -2)
                return NotFound("Client ID not found");

            
            Claim claim = _context.ViewClaim(newID, claimRequest.ClientID)!;
            /*var obj = new
            {
                ClaimID = newId
            };
            var json = JsonSerializer.Serialize(obj);*/
            return Ok(claim);
        }
        

        
    }
}
