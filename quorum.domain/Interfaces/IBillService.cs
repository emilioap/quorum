namespace quorum.domain.Interfaces
{
    public interface IBillService
    {
        public Task<int> GetSupporters(int id);
        public Task<int> GetOpposers(int id);
        public Task<string> GetPrimarySponsor(int id);
    }
}
