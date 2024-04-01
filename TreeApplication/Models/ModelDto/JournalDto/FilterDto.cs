using System.ComponentModel.DataAnnotations;

namespace TreeApplication.Models.ModelDto.JournalDto
{
    public class FilterDto
    {
        [DataType(DataType.Date)]
        public DateTime? From { get; set; }
        [DataType(DataType.Date)]
        public DateTime? To { get; set; }
        [StringLength(100, MinimumLength = 2)]
        public string? Search { get; set; }
    }
}
