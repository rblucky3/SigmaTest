using Moq;
using Sigma.Core.Entities;
using Sigma.Service.Settings.Services;
using Sigma.Service.Settings.Interfaces;
using Sigma.Repository.Settings.Interfaces;

namespace Sigma.Test
{
    public class CandidateServiceTest
    {
        private Mock<ICandidateRepository> _mockCandidateRepository;
        private ICandidateService _candidateService;

        [SetUp]
        public void SetUp()
        {
            _mockCandidateRepository = new Mock<ICandidateRepository>();
            _candidateService = new CandidateService(_mockCandidateRepository.Object);
        }

        [Test]
        public async Task AddOrUpdateCandidateAsync_ShouldAddNewCandidate_WhenNotExist()
        {
            // Arrange
            var candidateDto = new Candidate
            {
                FirstName = "John",
                LastName = "Smith",
                PhoneNumber = "1234567890",
                Email = "john.doe@example.com",
                CallTimeInterval = "9 AM - 12 PM",
                LinkedInProfileUrl = "https://linkedin.com/in/johnsmith",
                GitHubProfileUrl = "https://github.com/johnsmith",
                Comment = "Looking for opportunities."
            };

          var candidate= _mockCandidateRepository
                .Setup(repo => repo.GetByEmailAsync(candidateDto.Email))
                .ReturnsAsync((Candidate)null); // Candidate doesn't exist

            // Act
            var result= await _candidateService.AddOrUpdateCandidateAsync(candidateDto);

            //Assert.IsNotNull(insertedCandidate);
            //Assert.AreEqual("John", insertedCandidate.);
            //Assert.AreEqual("Doe", insertedCandidate.LastName);


            // Assert
            _mockCandidateRepository.Verify(r => r.AddAsync(It.IsAny<Candidate>()), Times.Once);
            _mockCandidateRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task AddOrUpdateCandidateAsync_ShouldUpdateExistingCandidate_WhenExists()
        {
            // Arrange
            var candidateDto = new Candidate //DTO
            {
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "1234567890",
                Email = "john.doe@example.com",
                CallTimeInterval = "9 AM - 12 PM",
                LinkedInProfileUrl = "https://linkedin.com/in/johndoe",
                GitHubProfileUrl = "https://github.com/johndoe",
                Comment = "Looking for opportunities."
            };


            var existingCandidate = new Candidate
            {
                Id = 1,
                FirstName = "John",
                LastName = "Smith",
                Email = candidateDto.Email,
                Comment = "No Comments"
            };

            _mockCandidateRepository
                .Setup(repo => repo.GetByEmailAsync(candidateDto.Email))
                .ReturnsAsync(existingCandidate); // Candidate exists

            // Act
            await _candidateService.AddOrUpdateCandidateAsync(candidateDto);

            // Assert
            _mockCandidateRepository.Verify(r => r.UpdateAsync(It.IsAny<Candidate>()), Times.Once);
            _mockCandidateRepository.Verify(r => r.SaveChangesAsync(), Times.Once);

        }

        [Test]
        public async Task AddOrUpdateCandidateAsync_ShouldCreateNewCandidate_WhenValidModel()
        {
            // Arrange: Create a valid CandidateProfileDto object
            var candidateDto = new Candidate
            {
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "1234567890",
                Email = "john.doe@example.com",
                CallTimeInterval = "9 AM - 12 PM",
                LinkedInProfileUrl = "https://linkedin.com/in/johndoe",
                GitHubProfileUrl = "https://github.com/johndoe",
                Comment = "Looking for opportunities."
            };

            // Simulate that the candidate doesn't exist in the repository (return null)
            _mockCandidateRepository
                .Setup(repo => repo.GetByEmailAsync(candidateDto.Email))
                .ReturnsAsync((Candidate)null); // Candidate doesn't exist

            // Simulate the creation of a new candidate
            _mockCandidateRepository
                .Setup(repo => repo.AddAsync(It.IsAny<Candidate>()));

            // Act: Call the service method
            var result = await _candidateService.AddOrUpdateCandidateAsync(candidateDto);

            // Assert: Verify that the result is not null and the correct methods were called
            Assert.IsNotNull(result);  // Assert that the result is not null

            _mockCandidateRepository.Verify(repo => repo.AddAsync(It.IsAny<Candidate>()), Times.Once); // Verify AddAsync was called exactly once
        }
    }


}
