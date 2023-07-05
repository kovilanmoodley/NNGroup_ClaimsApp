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
        public ActionResult<Claim> Create(ClaimRequest claimRequest)
        {
            try
            {
                int newID = _context.MakeClaim(claimRequest);
                if (newID == -1)
                    return BadRequest();
                if (newID == -2)
                    return BadRequest();


                Claim claim = _context.ViewClaim(newID, claimRequest.ClientID)!;
                /*var obj = new
                {
                    ClaimID = newId
                };
                var json = JsonSerializer.Serialize(obj);*/
                return Ok(claim);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        

        
    }
}
