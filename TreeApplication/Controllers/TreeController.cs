using Microsoft.AspNetCore.Mvc;
using TreeApplication.Interfaces;
using TreeApplication.Models.ModelDto.TreeDto;

namespace TreeApplication.Controllers
{
    [ApiController]
    [Route("api/user/tree")]
    public class TreeController : ControllerBase
    {
        private readonly ILogger<TreeController> _logger;
        private readonly ITreeService _treeService;

        public TreeController(ILogger<TreeController> logger, ITreeService treeService)
        {
            _logger = logger;
            _treeService = treeService;
        }

        [HttpPost("get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] GetTreeDto getTree, CancellationToken cancellation) 
        {
            _logger.LogInformation("Get tree Endpoint is executing");

            var tree = await _treeService.GetOrCreateTreeAutomaticallyAsync(getTree.TreeName, cancellation);

            if (tree.Any())
                return Ok(tree);

            var newTree = await _treeService.CreateTreeAsync(getTree.TreeName, cancellation);
            if (newTree == null)
                throw new ArgumentNullException(nameof(newTree));

            return Ok(newTree);
        }
    }
}
