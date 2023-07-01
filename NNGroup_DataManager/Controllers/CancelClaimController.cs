using Microsoft.AspNetCore.Mvc;
using NNGroup_DataManager.DataAccess;
using ShareModels.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NNGroup_DataManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CancelClaimController : ControllerBase
    {
        public readonly IClaimDataAccess _context;
        public CancelClaimController(IClaimDataAccess context)
        {
            _context = context;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult CancelClaim([FromBody] ClaimStatusChangeRequest claimStatusChangeRequest)
        {
            string result = _context.CancelClaim(claimStatusChangeRequest.ClaimID, claimStatusChangeRequest.ID);
            if (result != "Ok")
                return BadRequest(result);
            else
                return Ok("Claim has been cancelled");

        }
    }
}
