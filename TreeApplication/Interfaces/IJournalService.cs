using TreeApplication.Models;

namespace TreeApplication.Interfaces
{
    public interface IJournalService
    {
        Task<MJournal> GetSingleJournalAsync(int id, CancellationToken cancellation);
        Task<(IEnumerable<MJournal> Journals, int TotalCount)> GetJournalsPaginationAsync(int skip, int take, CancellationToken cancellation);
    }
}
