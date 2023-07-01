using Microsoft.AspNetCore.Mvc;
using NNGroup_DataManager.DataAccess;
using ShareModels.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NNGroup_DataManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewClaimStatusController : ControllerBase
    {
        public readonly IClaimDataAccess _context;
        // GET: api/<ViewClaimController>
        public ViewClaimStatusController(IClaimDataAccess context)
        {
            _context = context;
        }


        // GET api/<ViewClaimStatusController>/5
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<string> Get(int claimid, int clientid)
        {
            ClaimStatusChangeRequest claimStatusChangeRequest = new() { ClaimID = claimid, ID = clientid };

            Claim claim = _context.ViewClaim(claimStatusChangeRequest)!;

            if (claim == null)
                return NotFound();
            else
                return Ok(claim.ClaimStatus);
        }
    }
}
