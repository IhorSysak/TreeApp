namespace TreeApplication.Interfaces
{
    public interface INodeService
    {
        Task<bool> CreateNodeAsync(string treeName, int parentNodeId, string nodeName, CancellationToken cancellation);
        Task<bool> DeleteNodeAsync(string treeName, int nodeId, CancellationToken cancellation);
        Task<bool> RenameNodeAsync(string treeName, int nodeId, string newNodeName, CancellationToken cancellation);
    }
}
