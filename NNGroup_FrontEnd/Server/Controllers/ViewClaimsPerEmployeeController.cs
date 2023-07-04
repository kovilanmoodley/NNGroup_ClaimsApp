using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NNGroup_FrontEnd.Server.DataAccess;
using ShareModels.Models;

namespace NNGroup_FrontEnd.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewClaimsPerEmployeeController : ControllerBase
    {
        public readonly IClaimDataAccess _context;
        public ViewClaimsPerEmployeeController(IClaimDataAccess context)
        {
            _context = context;
        }


        // GET api/<ViewClaimStatusController>/5
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Claim>> GetByEmployee(int employeeID)
        {

            List<Claim> claims = _context.ViewClaimByEmployee(employeeID)!;

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
