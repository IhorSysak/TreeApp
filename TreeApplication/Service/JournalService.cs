using Microsoft.EntityFrameworkCore;
using TreeApplication.Context;
using TreeApplication.Interfaces;
using TreeApplication.Models;

namespace TreeApplication.Service
{
    public class JournalService : IJournalService
    {
        private readonly TreeContext _treeContext;

        public JournalService(TreeContext treeContext) 
        {
            _treeContext = treeContext;
        }

        public async Task<MJournal> GetSingleJournalAsync(int id, CancellationToken cancellation)
        {
            var journal = await _treeContext.MJournals.FirstOrDefaultAsync(j => j.Id == id, cancellation);
            if (journal == null)
                throw new ArgumentNullException($"There is no journal by id: {id}");

            return journal;
        }

        public async Task<(IEnumerable<MJournal> Journals, int TotalCount)> GetJournalsPaginationAsync(int skip, int take, CancellationToken cancellation)
        {
            var journals = await _treeContext.MJournals.Skip(skip).Take(take).ToListAsync(cancellation);
            var journalsCount = await _treeContext.MJournals.CountAsync(cancellation);
            if (journals == null)
                throw new ArgumentNullException(nameof(journals));

            return (journals, journalsCount);
        }
    }
}
