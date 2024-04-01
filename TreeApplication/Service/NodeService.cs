using Microsoft.EntityFrameworkCore;
using TreeApplication.Context;
using TreeApplication.Interfaces;
using TreeApplication.Models;
using TreeApplication.Utility;

namespace TreeApplication.Service
{
    public class NodeService : INodeService
    {
        private readonly TreeContext _treeContext;

        public NodeService(TreeContext treeContext)
        {
            _treeContext = treeContext;
        }

        public async Task<bool> CreateNodeAsync(string treeName, int parentNodeId, string nodeName, CancellationToken cancellation)
        {
            var parentNode = await _treeContext.TreeNodes.Include(n => n.Children).FirstOrDefaultAsync(n => n.Id == parentNodeId, cancellation);

            if (parentNode == null)
                return false;

            if (IsNodeNameUnique(parentNode, nodeName))
            {
                var newNode = new TreeNode { Name = nodeName };
                _treeContext.TreeNodes.Add(newNode);
                parentNode.Children.Add(newNode);
                await _treeContext.SaveChangesAsync(cancellation);
                return true;
            }
            else
                throw new SecureException("The node name is not unique");
        }

        public async Task<bool> DeleteNodeAsync(string treeName, int nodeId, CancellationToken cancellation)
        {
            var rootNode = await _treeContext.TreeNodes.Include(n => n.Children).FirstOrDefaultAsync(n => n.Name == treeName, cancellation);
            if (rootNode == null)
                throw new InvalidOperationException($"There is no tree with name: {treeName}");
            if (rootNode.Id == nodeId)
                throw new SecureException("Cannot delete the root node");

            var nodeToDelete = await _treeContext.TreeNodes.Include(t => t.Children).FirstOrDefaultAsync(t => t.Id == nodeId, cancellation);
            if (nodeToDelete.ParentId == 0 || nodeToDelete.ParentId == null)
                throw new SecureException("Cannot delete this node which does not have a parent");

            if (nodeToDelete.Children.Any())
                throw new SecureException("Cannot delete a node that has children");

            var parentNode = await _treeContext.TreeNodes.Include(t => t.Children).FirstOrDefaultAsync(t => t.Children.Any(c => c.Id == nodeId), cancellation);
            if (parentNode == null)
                return false;

            parentNode.Children.Remove(nodeToDelete);
            await _treeContext.SaveChangesAsync(cancellation);
            return true;
        }

        public async Task<bool> RenameNodeAsync(string treeName, int nodeId, string newNodeName, CancellationToken cancellation)
        {
            var nodeToRename = await _treeContext.TreeNodes.Include(n => n.Parent).FirstOrDefaultAsync(n => n.Id == nodeId, cancellation);
            if (nodeToRename == null || nodeToRename.Parent == null)
                return false;

            if (!IsNodeNameUnique(nodeToRename.Parent, newNodeName))
                return false;

            nodeToRename.Name = newNodeName;
            await _treeContext.SaveChangesAsync(cancellation);
            return true;
        }

        private bool IsNodeNameUnique(TreeNode parentNode, string nodeName)
        {
            return !parentNode.Children.Any(c => c.Name == nodeName);
        }
    }
}
