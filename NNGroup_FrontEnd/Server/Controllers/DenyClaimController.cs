using Microsoft.AspNetCore.Mvc;
using NNGroup_FrontEnd.Server.DataAccess;
using ShareModels.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NNGroup_FrontEnd.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DenyClaimController : ControllerBase
    {
        public readonly IClaimDataAccess _context;
        public DenyClaimController(IClaimDataAccess context)
        {
            _context = context;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult DenyClaim([FromBody] ClaimStatusChangeRequest claimStatusChangeRequest)
        {
            string result = _context.ApproveDenyClaim(claimStatusChangeRequest.ClaimID, claimStatusChangeRequest.ID, "Denied");
            if (result != "Ok")
                return BadRequest(result);
            else
                return Ok("Claim has been denied!!!");

        }
    }
}
