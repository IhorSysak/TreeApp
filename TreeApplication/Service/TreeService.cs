using Microsoft.EntityFrameworkCore;
using TreeApplication.Context;
using TreeApplication.Interfaces;
using TreeApplication.Models;

namespace TreeApplication.Service
{
    public class TreeService : ITreeService
    {
        private readonly TreeContext _treeContext;

        public TreeService(TreeContext treeContext) 
        {
            _treeContext = treeContext;
        }

        public async Task<List<TreeNode>> GetOrCreateTreeAutomaticallyAsync(string treeName, CancellationToken cancellation) 
        {
            var rootNode = _treeContext.TreeNodes.Include(d => d.Children).AsEnumerable()
                .Where(n => n.Name == treeName && n.ParentId == null).ToList();

            return rootNode;
        }

        public async Task<TreeNode> CreateTreeAsync(string treeName, CancellationToken cancellation) 
        {
            var newRootNode = new TreeNode { Name = treeName };
            _treeContext.TreeNodes.Add(newRootNode);
            await _treeContext.SaveChangesAsync(cancellation);

            return newRootNode;
        }
    }
}
