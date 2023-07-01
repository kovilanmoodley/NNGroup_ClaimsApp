using Microsoft.AspNetCore.Mvc;
using NNGroup_DataManager.DataAccess;
using ShareModels.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NNGroup_DataManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApproveClaimController : ControllerBase
    {
        public readonly IClaimDataAccess _context;
        public ApproveClaimController(IClaimDataAccess context)
        {
            _context = context;
        }

        // POST api/<ApproveController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult ApproveClaim([FromBody] ClaimStatusChangeRequest claimStatusChangeRequest)
        {
            string result = _context.ApproveDenyClaim(claimStatusChangeRequest.ClaimID, claimStatusChangeRequest.ID, "Approved");
            if (result != "Ok")
                return BadRequest(result);
            else 
                return Ok("Claim has been approved");

        }

    }
}
