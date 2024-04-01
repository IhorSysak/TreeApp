using System.ComponentModel.DataAnnotations;

namespace TreeApplication.Models.ModelDto.NodeDto
{
    public class CreateNodeDto
    {
        [Required]
        [Display(Name = "TreeName")]
        [StringLength(100, MinimumLength = 2)]
        public string TreeName { get; set; }
        [Required]
        [Display(Name = "ParentNodeId")]
        [Range(1, 100, ErrorMessage = "The ParentNodeId variable has to be between 1 and 100")]
        public int ParentNodeId { get; set; }
        [Required]
        [Display(Name = "NodeName")]
        [StringLength(100, MinimumLength = 2)]
        public string NodeName { get; set; }
    }
}
