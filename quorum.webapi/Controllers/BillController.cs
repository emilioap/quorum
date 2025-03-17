using Microsoft.AspNetCore.Mvc;
using quorum.domain.Interfaces;

namespace quorum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IBillService _billService;

        public BillController(IBillService billService)
        {
            _billService = billService;
        }

        /// <summary>
        /// Gets the number of legislators who supported a bill.
        /// </summary>
        /// <param name="id">Bill ID</param>
        /// <returns>Number of supporters</returns>
        [HttpGet("{id}/supporters")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetSupporters(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid Bill ID");

                var supporters = await _billService.GetSupporters(id);

                return Ok(new { BillId = id, Supporters = supporters });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while processing your request.", Details = ex.Message });
            }
        }

        /// <summary>
        /// Gets the number of legislators who opposed a bill.
        /// </summary>
        /// <param name="id">Bill ID</param>
        /// <returns>Number of opponents</returns>
        [HttpGet("{id}/opposers")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetOpposers(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid Bill ID");

                var opponents = await _billService.GetOpposers(id);

                return Ok(new { BillId = id, Opponents = opponents });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while processing your request.", Details = ex.Message });
            }
        }

        /// <summary>
        /// Gets the primary sponsor of a bill.
        /// </summary>
        /// <param name="id">Bill ID</param>
        /// <returns>Name of the primary sponsor</returns>
        [HttpGet("{id}/sponsor")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetPrimarySponsor(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid Bill ID");

                var sponsor = await _billService.GetPrimarySponsor(id);

                if (string.IsNullOrEmpty(sponsor))
                    return NotFound(new { Message = "Bill not found" });

                return Ok(new { BillId = id, PrimarySponsor = sponsor });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while processing your request.", Details = ex.Message });
            }
        }
    }
}
