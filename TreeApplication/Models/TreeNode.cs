using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreeApplication.Models
{
    public class TreeNode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public TreeNode? Parent { get; set; }
        public ICollection<TreeNode>? Children { get; set; } = new List<TreeNode>();
    }
}
