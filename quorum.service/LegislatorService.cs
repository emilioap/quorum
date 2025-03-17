using Microsoft.Extensions.Caching.Memory;
using quorum.domain.Enums;
using quorum.domain.Interfaces;

namespace quorum.service
{
    public class LegislatorService : ILegislatorService
    {
        private readonly IDataRepository _dataRepository;
        private readonly IMemoryCache _cache;

        public LegislatorService(IDataRepository dataRepository, IMemoryCache cache)
        {
            _dataRepository = dataRepository;
            _cache = cache;
        }

        public async Task<int> GetSupportedBills(int id)
        {
            return await _cache.GetOrCreateAsync($"SupportedBills_{id}", entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                var supportedBills = _dataRepository.VoteResults
                    .Where(v => v.LegislatorId == id && v.VoteType == VoteTypeEnum.Yea)
                    .Count();
                return Task.FromResult(supportedBills);
            });
        }

        public async Task<int> GetOpposedBills(int id)
        {
            return await _cache.GetOrCreateAsync($"OpposedBills_{id}", entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                var opposedBills = _dataRepository.VoteResults
                    .Where(v => v.LegislatorId == id && v.VoteType == VoteTypeEnum.Nay)
                    .Count();
                return Task.FromResult(opposedBills);
            });
        }
    }
}
