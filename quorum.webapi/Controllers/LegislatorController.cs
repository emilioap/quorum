using Microsoft.AspNetCore.Mvc;
using quorum.domain.Interfaces;

namespace quorum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LegislatorController : ControllerBase
    {
        private readonly ILegislatorService _legislatorService;

        public LegislatorController(ILegislatorService legislatorService)
        {
            _legislatorService = legislatorService;
        }

        /// <summary>
        /// Gets the number of bills a legislator has supported.
        /// </summary>
        /// <param name="legislatorId">Legislator ID</param>
        /// <returns>Number of supported bills</returns>
        [HttpGet("{legislatorId}/supported-bills")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetSupportedBills(int legislatorId)
        {
            try
            {
                if (legislatorId <= 0)
                    return BadRequest("Invalid Legislator ID");

                var supportedBills = await _legislatorService.GetSupportedBills(legislatorId);

                return Ok(new { LegislatorId = legislatorId, SupportedBills = supportedBills });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while processing your request.", Details = ex.Message });
            }
        }

        /// <summary>
        /// Gets the number of bills a legislator has opposed.
        /// </summary>
        /// <param name="legislatorId">Legislator ID</param>
        /// <returns>Number of opposed bills</returns>
        [HttpGet("{legislatorId}/opposed-bills")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetOpposedBills(int legislatorId)
        {
            try
            {
                if (legislatorId <= 0)
                    return BadRequest("Invalid Legislator ID");

                var result = await _legislatorService.GetOpposedBills(legislatorId);

                return Ok(new { LegislatorId = legislatorId, OpposedBills = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while processing your request.", Details = ex.Message });
            }
        }
    }
}
