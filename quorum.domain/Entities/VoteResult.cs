using quorum.domain.Enums;

namespace quorum.domain.Entities
{
    public class VoteResult
    {
        public int Id { get; set; }
        public int LegislatorId { get; set; }
        public int VoteId { get; set; }
        public VoteTypeEnum VoteType { get; set; }
    }
}
