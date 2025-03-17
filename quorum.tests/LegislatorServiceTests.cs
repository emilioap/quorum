using quorum.domain.Entities;
using quorum.domain.Enums;
using quorum.domain.Interfaces;
using quorum.service;
using Moq;

namespace quorum.tests
{
    public class LegislatorServiceTests
    {
        private readonly Mock<IDataRepository> _mockRepo;
        private readonly LegislatorService _legislatorService;

        public LegislatorServiceTests()
        {
            _mockRepo = new Mock<IDataRepository>();
            _legislatorService = new LegislatorService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetSupportedBills_ShouldReturnCorrectCount()
        {
            _mockRepo.Setup(r => r.VoteResults).Returns(new List<VoteResult>
            {
                new VoteResult { LegislatorId = 1, VoteType = VoteTypeEnum.Yea },
                new VoteResult { LegislatorId = 1, VoteType = VoteTypeEnum.Yea }
            }.AsQueryable().ToList());

            var result = await _legislatorService.GetSupportedBills(1);
            Assert.Equal(2, result);
        }

        [Fact]
        public async Task GetOpposedBills_ShouldReturnCorrectCount()
        {
            _mockRepo.Setup(r => r.VoteResults).Returns(new List<VoteResult>
            {
                new VoteResult { LegislatorId = 1, VoteType = VoteTypeEnum.Nay }
            }.AsQueryable().ToList());

            var result = await _legislatorService.GetOpposedBills(1);
            Assert.Equal(1, result);
        }
    }
}
