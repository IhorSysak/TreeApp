using System.ComponentModel.DataAnnotations;

namespace TreeApplication.Models.ModelDto.NodeDto
{
    public class DeleteNodeDto
    {
        [Required]
        [Display(Name = "TreeName")]
        [StringLength(100, MinimumLength = 2)]
        public string TreeName { get; set; }
        [Required]
        [Display(Name = "NodeId")]
        [Range(1, 100, ErrorMessage = "The NodeId variable has to be between 1 and 100")]
        public int NodeId { get; set; }
    }
}
