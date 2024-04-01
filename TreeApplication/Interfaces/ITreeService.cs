using TreeApplication.Models;

namespace TreeApplication.Interfaces
{
    public interface ITreeService
    {
        Task<List<TreeNode>> GetOrCreateTreeAutomaticallyAsync(string treeName, CancellationToken cancellation);
        Task<TreeNode> CreateTreeAsync(string treeName, CancellationToken cancellation);
    }
}
