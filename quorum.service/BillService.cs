using Microsoft.Extensions.Caching.Memory;
using quorum.domain.Enums;
using quorum.domain.Interfaces;

namespace quorum.service
{
    public class BillService : IBillService
    {
        private readonly IDataRepository _dataRepository;
        private readonly IMemoryCache _cache;

        public BillService(IDataRepository dataRepository, IMemoryCache cache)
        {
            _dataRepository = dataRepository;
            _cache = cache;
        }

        public async Task<int> GetSupporters(int id)
        {
            return await _cache.GetOrCreateAsync($"Supporters_{id}", entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                var supporters = _dataRepository.VoteResults
                    .Join(_dataRepository.Votes, vr => vr.VoteId, v => v.Id, (vr, v) => new { vr, v })
                    .Where(x => x.v.BillId == id && x.vr.VoteType == VoteTypeEnum.Yea)
                    .Count();
                return Task.FromResult(supporters);
            });
        }

        public async Task<int> GetOpposers(int id)
        {
            return await _cache.GetOrCreateAsync($"Opposers_{id}", entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                var opponents = _dataRepository.VoteResults
                    .Join(_dataRepository.Votes, vr => vr.VoteId, v => v.Id, (vr, v) => new { vr, v })
                    .Where(x => x.v.BillId == id && x.vr.VoteType == VoteTypeEnum.Nay)
                    .Count();
                return Task.FromResult(opponents);
            });
        }

        public async Task<string> GetPrimarySponsor(int id)
        {
            return await _cache.GetOrCreateAsync($"PrimarySponsor_{id}", entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                var bill = _dataRepository.Bills.FirstOrDefault(b => b.Id == id);
                if (bill == null)
                    return Task.FromResult(string.Empty);

                var sponsor = _dataRepository.Legislators.FirstOrDefault(l => l.Id == bill.PrimarySponsorId)?.Name;
                return Task.FromResult(sponsor);
            });
        }
    }
}
