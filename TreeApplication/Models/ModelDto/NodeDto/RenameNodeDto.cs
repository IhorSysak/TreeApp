using System.ComponentModel.DataAnnotations;

namespace TreeApplication.Models.ModelDto.NodeDto
{
    public class RenameNodeDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "TreeName")]
        public string TreeName { get; set; }
        [Required]
        [Display(Name = "NodeId")]
        [Range(1, 100, ErrorMessage = "The NodeId variable has to be between 1 and 100")]
        public int NodeId { get; set; }
        [Required]
        [Display(Name = "NewNodeName")]
        [StringLength(100, MinimumLength = 2)]
        public string NewNodeName { get; set; }
    }
}
