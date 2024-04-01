using System.ComponentModel.DataAnnotations;

namespace TreeApplication.Models.ModelDto.JournalDto
{
    public class GetSingleDto
    {
        [Required]
        [Display(Name = "Id")]
        [Range(1, 100, ErrorMessage = "The Id variable has to be between 1 and 100")]
        public int Id { get; set; }
    }
}
