using quorum.domain.Entities;

namespace quorum.domain.Interfaces
{
    public interface IDataRepository
    {
        List<Legislator> Legislators { get; }
        List<Bill> Bills { get; }
        List<Vote> Votes { get; }
        List<VoteResult> VoteResults { get; }
    }
}
