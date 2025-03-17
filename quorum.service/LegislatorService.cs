using quorum.domain.Enums;
using quorum.domain.Interfaces;

namespace quorum.service
{
    public class LegislatorService : ILegislatorService
    {
        private readonly IDataRepository _dataRepository;

        public LegislatorService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public async Task<int> GetSupportedBills(int id)
        {
            var supportedBills = _dataRepository.VoteResults
                .Where(v => v.LegislatorId == id && v.VoteType == VoteTypeEnum.Yea)
                .Count();

            return await Task.FromResult(supportedBills);
        }

        public async Task<int> GetOpposedBills(int id)
        {
            var opposedBills = _dataRepository.VoteResults
                .Where(v => v.LegislatorId == id && v.VoteType == VoteTypeEnum.Nay)
                .Count();

            return await Task.FromResult(opposedBills);
        }
    }
}