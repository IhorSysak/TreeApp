using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TreeApplication.Interfaces;
using TreeApplication.Models.ModelDto.JournalDto;

namespace TreeApplication.Controllers
{
    [ApiController]
    [Route("api/user/journal")]
    public class JournalController : ControllerBase
    {
        private readonly ILogger<JournalController> _logger;
        private readonly IJournalService _journalService;
        private readonly IMapper _mapper;

        public JournalController(ILogger<JournalController> logger, IJournalService journalService, IMapper mapper) 
        {
            _logger = logger;
            _journalService = journalService;
            _mapper = mapper;
        }

        [HttpPost("getSingle")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetJournal([FromQuery] GetSingleDto getSingle, CancellationToken cancellation)
        {
            _logger.LogInformation($"Get journal by '{getSingle.Id}' id Endpoint is executing");

            var journals = await _journalService.GetSingleJournalAsync(getSingle.Id, cancellation);
            if (journals != null)
                return Ok(journals);
            else
                throw new ArgumentNullException(nameof(getSingle));
        }

        [HttpPost("getRange")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRangeJournal([FromQuery] GetRangeDto getRange, CancellationToken cancellation)
        {
            _logger.LogInformation("Get range journal Endpoint is executing");

            var (journals, journalCount) = await _journalService.GetJournalsPaginationAsync(getRange.Skip, getRange.Take, cancellation);

            if (journals == null)
                throw new ArgumentNullException(nameof(journals));

            if (getRange.Filter != null)
            {
                if (getRange.Filter.From.HasValue)
                    journals = journals.Where(d => d.CreatedAt >= getRange.Filter.From);
                if (getRange.Filter.To.HasValue)
                    journals = journals.Where(d => d.CreatedAt <= getRange.Filter.To);

                if (!string.IsNullOrEmpty(getRange.Filter.Search))
                    journals = journals.Where(d => d.Text.Contains(getRange.Filter.Search));
            }

            var journalDto = _mapper.Map<IEnumerable<JournalItemDto>>(journals);

            var journalResponses = new ResponseGetRangeDto
            {
                Skip = getRange.Skip,
                Count = journalCount,
                JournalItems = journalDto
            };

            return Ok(journalResponses);
        }
    }
}
