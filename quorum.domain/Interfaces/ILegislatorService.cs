namespace quorum.domain.Interfaces
{
    public interface ILegislatorService
    {
        public Task<int> GetSupportedBills(int id);
        public Task<int> GetOpposedBills(int id);
    }
}
