using System.ComponentModel.DataAnnotations;

namespace TreeApplication.Models.ModelDto.JournalDto
{
    public class GetRangeDto
    {
        [Required]
        [Display(Name = "Skip")]
        [Range(1, 100, ErrorMessage = "The Skip variable has to be between 1 and 100")]
        public int Skip { get; set; }
        [Required]
        [Display(Name = "Take")]
        [Range(1, 100, ErrorMessage = "The Take variable has to be between 1 and 100")]
        public int Take { get; set; }
        public FilterDto? Filter { get; set; } = null;
    }
}
