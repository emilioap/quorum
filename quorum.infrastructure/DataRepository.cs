using CsvHelper;
using CsvHelper.Configuration;
using quorum.domain.Entities;
using quorum.domain.Interfaces;
using System.Globalization;


namespace quorum.service
{
    public class DataRepository : IDataRepository
    {
        private readonly string _dataDirectory;

        public List<Legislator> Legislators { get; private set; }
        public List<Bill> Bills { get; private set; }
        public List<Vote> Votes { get; private set; }
        public List<VoteResult> VoteResults { get; private set; }

        public DataRepository()
        {
            _dataDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Source");
            LoadData();
        }

        private void LoadData()
        {
            Legislators = ReadCsv<Legislator>("legislators.csv");
            Bills = ReadCsv<Bill>("bills.csv");
            Votes = ReadCsv<Vote>("votes.csv");
            VoteResults = ReadCsv<VoteResult>("vote_results.csv");
        }

        private List<T> ReadCsv<T>(string fileName)
        {
            var filePath = Path.Combine(_dataDirectory, fileName);
            if (!File.Exists(filePath)) return new List<T>();

            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header
                    .Replace("sponsor_id", "PrimarySponsorId")
                    .Replace("bill_id", "BillId")
                    .Replace("legislator_id", "LegislatorId")
                    .Replace("vote_id", "VoteId")
                    .Replace("vote_type", "VoteType")
                    .ToLower()
            });

            return csv.GetRecords<T>().ToList();
        }
    }
}
