﻿using Microsoft.AspNetCore.Mvc;
using NNGroup_FrontEnd.Server.DataAccess;
using ShareModels.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NNGroup_FrontEnd.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewAuditClaimController : ControllerBase
    {
        public readonly IClaimDataAccess _context;
        public ViewAuditClaimController(IClaimDataAccess context)
        {
            _context = context;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Get(int claimid, int employeeid)
        {
            try
            {
                List<AuditClaim> fullClaimHistory = _context.ViewFullClaimHistory(claimid, employeeid)!;

                if (fullClaimHistory == null)
                    return BadRequest();
                else
                    return Ok(fullClaimHistory);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
