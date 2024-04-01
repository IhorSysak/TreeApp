using Microsoft.AspNetCore.Mvc;
using TreeApplication.Interfaces;
using TreeApplication.Models.ModelDto.NodeDto;
using TreeApplication.Utility;

namespace TreeApplication.Controllers
{
    [ApiController]
    [Route("api/user/tree/node")]
    public class NodeController : ControllerBase
    {
        private readonly ILogger<NodeController> _logger;
        private readonly INodeService _nodeService;

        public NodeController(ILogger<NodeController> logger, INodeService nodeService)
        {
            _logger = logger;
            _nodeService = nodeService;
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromQuery] CreateNodeDto createNode, CancellationToken cancellation)
        {
            _logger.LogInformation("Create node Endpoint is executing");

            bool success = await _nodeService.CreateNodeAsync(createNode.TreeName, createNode.ParentNodeId, createNode.NodeName, cancellation);
            if (success)
                return Ok();
            else
                throw new SecureException($"A node with the name '{createNode.NodeName}' already exists under the parent node with id {createNode.ParentNodeId}.");
        }

        [HttpPost("delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromQuery] DeleteNodeDto deleteNode, CancellationToken cancellation)
        {
            _logger.LogInformation("Delete node Endpoint is executing");

            bool success = await _nodeService.DeleteNodeAsync(deleteNode.TreeName, deleteNode.NodeId, cancellation);
            if (success)
                return Ok();
            else
                throw new SecureException("You can not delete this node");
        }

        [HttpPost("rename")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Rename([FromQuery] RenameNodeDto renameNode, CancellationToken cancellation)
        {
            _logger.LogInformation("Rename node Endpoint is executing");

            var success = await _nodeService.RenameNodeAsync(renameNode.TreeName, renameNode.NodeId, renameNode.NewNodeName, cancellation);
            if (success)
                return Ok();
            else
                throw new SecureException("You can not rename this node");
        }
    }
}
