using Microsoft.AspNetCore.Mvc;
using NNGroup_FrontEnd.Server.DataAccess;
using ShareModels.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NNGroup_FrontEnd.Server.Controllers
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
        public ActionResult<ServiceResult> ApproveClaim([FromBody] ClaimStatusChangeRequest claimStatusChangeRequest)
        {
            string result = _context.ApproveDenyClaim(claimStatusChangeRequest.ClaimID, claimStatusChangeRequest.ID, "Approved");
            ServiceResult serviceResult = new();

            if (result != "Ok")
            {
                serviceResult.ResultStatus = result;
                return BadRequest(result);
            }
            else
            {
                serviceResult.ResultStatus = "Claim has been approved";
                return Ok(serviceResult.ResultStatus);
            }

        }

    }
}
