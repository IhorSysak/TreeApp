namespace TreeApplication.Models.ModelDto.JournalDto
{
    public class ResponseGetRangeDto
    {
        public int Skip { get; set; }
        public int Count { get; set; }
        public IEnumerable<JournalItemDto> JournalItems { get; set; }
    }
}
