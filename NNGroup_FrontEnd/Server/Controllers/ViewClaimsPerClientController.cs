using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NNGroup_FrontEnd.Server.DataAccess;
using ShareModels.Models;

namespace NNGroup_FrontEnd.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewClaimsPerClientController : ControllerBase
    {
        public readonly IClaimDataAccess _context;
        public ViewClaimsPerClientController(IClaimDataAccess context)
        {
            _context = context;
        }


        // GET api/<ViewClaimStatusController>/5
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Claim>> GetByClient(int clientID)
        {

            List<Claim> claims = _context.ViewClaimByClient(clientID)!;

            if (claims.Count == 0)
            {

                return BadRequest();
            }
            else
            {
                return Ok(claims);
            }
        }

    }
}
