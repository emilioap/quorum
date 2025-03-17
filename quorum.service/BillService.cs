using quorum.domain.Enums;
using quorum.domain.Interfaces;

namespace quorum.service
{
    public class BillService : IBillService
    {
        private readonly IDataRepository _dataRepository;

        public BillService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public async Task<int> GetSupporters(int id)
        {
            var supporters = _dataRepository.VoteResults
                .Join(_dataRepository.Votes, vr => vr.VoteId, v => v.Id, (vr, v) => new { vr, v })
                .Where(x => x.v.BillId == id && x.vr.VoteType == VoteTypeEnum.Yea)
                .Count();

            return await Task.FromResult(supporters);
        }

        public async Task<int> GetOpposers(int id)
        {
            var opponents = _dataRepository.VoteResults
                .Join(_dataRepository.Votes, vr => vr.VoteId, v => v.Id, (vr, v) => new { vr, v })
                .Where(x => x.v.BillId == id && x.vr.VoteType == VoteTypeEnum.None)
                .Count();

            return await Task.FromResult(opponents);
        }

        public async Task<string> GetPrimarySponsor(int id)
        {
            var bill = _dataRepository.Bills.FirstOrDefault(b => b.Id == id);

            if (bill == null)
                return await Task.FromResult(string.Empty);

            var sponsor = _dataRepository.Legislators.FirstOrDefault(l => l.Id == bill.PrimarySponsorId)?.Name;

            return await Task.FromResult(sponsor);
        }
    }
}
