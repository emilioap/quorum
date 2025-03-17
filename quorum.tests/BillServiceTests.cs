using Moq;
using quorum.domain.Entities;
using quorum.domain.Enums;
using quorum.domain.Interfaces;
using quorum.service;

namespace quorum.tests
{
    public class BillServiceTests
    {
        private readonly Mock<IDataRepository> _mockRepo;
        private readonly BillService _billService;

        public BillServiceTests()
        {
            _mockRepo = new Mock<IDataRepository>();
            _billService = new BillService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetSupporters_ShouldReturnCorrectCount()
        {
            _mockRepo.Setup(r => r.VoteResults).Returns(new List<VoteResult>
            {
                new VoteResult { VoteId = 1, LegislatorId = 1, VoteType = VoteTypeEnum.Yea },
                new VoteResult { VoteId = 2, LegislatorId = 2, VoteType = VoteTypeEnum.Yea }
            }.AsQueryable().ToList());

            _mockRepo.Setup(r => r.Votes).Returns(new List<Vote>
            {
                new Vote { Id = 1, BillId = 10 },
                new Vote { Id = 2, BillId = 10 }
            }.AsQueryable().ToList());

            var result = await _billService.GetSupporters(10);
            Assert.Equal(2, result);
        }

        [Fact]
        public async Task GetOpposers_ShouldReturnCorrectCount()
        {
            _mockRepo.Setup(r => r.VoteResults).Returns(new List<VoteResult>
            {
                new VoteResult { VoteId = 1, LegislatorId = 1, VoteType = VoteTypeEnum.Nay }
            }.AsQueryable().ToList());

            _mockRepo.Setup(r => r.Votes).Returns(new List<Vote>
            {
                new Vote { Id = 1, BillId = 10 }
            }.AsQueryable().ToList());

            var result = await _billService.GetOpposers(10);
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task GetPrimarySponsor_ShouldReturnCorrectName()
        {
            _mockRepo.Setup(r => r.Bills).Returns(new List<Bill>
            {
                new Bill { Id = 10, PrimarySponsorId = 1 }
            }.AsQueryable().ToList());

            _mockRepo.Setup(r => r.Legislators).Returns(new List<Legislator>
            {
                new Legislator { Id = 1, Name = "John Doe" }
            }.AsQueryable().ToList());

            var result = await _billService.GetPrimarySponsor(10);
            Assert.Equal("John Doe", result);
        }
    }
}