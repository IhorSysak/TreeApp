using System.ComponentModel.DataAnnotations;

namespace TreeApplication.Models.ModelDto.TreeDto
{
    public class GetTreeDto
    {
        [Required]
        [Display(Name = "TreeName")]
        [StringLength(100, MinimumLength = 2)]
        public string TreeName { get; set; }
    }
}
