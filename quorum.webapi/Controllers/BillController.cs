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

        [HttpGet("{id}/supporters")]
        public async Task<IActionResult> GetSupporters(int id)
        {
            var supporters = await _billService.GetSupporters(id);

            return Ok(new { BillId = id, Supporters = supporters });
        }

        [HttpGet("{id}/opposers")]
        public async Task<IActionResult> GetOpposers(int id)
        {
            var opponents = await _billService.GetOpposers(id);

            return Ok(new { BillId = id, Opponents = opponents });
        }

        [HttpGet("{id}/sponsor")]
        public async Task<IActionResult> GetPrimarySponsor(int id)
        {
            var sponsor = await _billService.GetPrimarySponsor(id);

            if (sponsor.Equals(string.Empty))
                return NotFound("Bill not found");

            return Ok(new { BillId = id, PrimarySponsor = sponsor });
        }
    }
}
