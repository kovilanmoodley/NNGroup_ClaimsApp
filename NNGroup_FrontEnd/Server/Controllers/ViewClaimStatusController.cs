using Microsoft.AspNetCore.Mvc;
using NNGroup_FrontEnd.Server.DataAccess;
using NNGroup_FrontEnd.Shared;
using ShareModels.Models;
using System.Text.Json;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NNGroup_FrontEnd.Server.Controllers
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

        [HttpGet]

        // GET api/<ViewClaimStatusController>/5
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ShareModels.Models.Claim> Get(int claimID, int clientID)
        {

            ShareModels.Models.Claim claim = _context.ViewClaim(claimID, clientID)!;

            if (claim == null)
            {

                return BadRequest();
            }
            else
            {
                return Ok(claim);
            }
        }
    }
}
