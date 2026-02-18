using MessageService.Model.Publisher;
using MessageService.Service.Publisher;

namespace MessageService.Test
{
    [TestClass]
    public class PublisherServiceTests
    {
        private PublisherService _publisherService = null!;

        [TestInitialize]
        public void Setup()
        {
            _publisherService = new PublisherService();
        }

        [TestMethod]
        public void Save_NewPublisher_CreatesPublisherWithNewId()
        {
            // Arrange
            var publisher = new PublisherModel
            {
                Id = Guid.Empty,
                Name = "TestPublisher",
                Description = "Test Description"
            };

            // Act
            var savedPublisher = _publisherService.Save(publisher);

            // Assert
            Assert.IsNotNull(savedPublisher);
            Assert.AreNotEqual(Guid.Empty, savedPublisher.Id);
            Assert.IsTrue(savedPublisher.CreatedAt > DateTime.MinValue);
            Assert.IsTrue(savedPublisher.UpdatedAt > DateTime.MinValue);
        }

        [TestMethod]
        public void GetPublisher_ExistingId_ReturnsPublisher()
        {
            // Arrange
            var publisher = new PublisherModel
            {
                Id = Guid.Empty,
                Name = "TestPublisher",
                Description = "Test Description"
            };
            var savedPublisher = _publisherService.Save(publisher);

            // Act
            var retrievedPublisher = _publisherService.GetPublisher(savedPublisher.Id);

            // Assert
            Assert.IsNotNull(retrievedPublisher);
            Assert.AreEqual(savedPublisher.Id, retrievedPublisher.Id);
            Assert.AreEqual("TestPublisher", retrievedPublisher.Name);
        }

        [TestMethod]
        public void GetPublisher_NonExistingId_ReturnsNull()
        {
            // Act
            var retrievedPublisher = _publisherService.GetPublisher(Guid.NewGuid());

            // Assert
            Assert.IsNull(retrievedPublisher);
        }

        [TestMethod]
        public void GetPublishers_ReturnsAllPublishers()
        {
            // Arrange
            var publisher1 = new PublisherModel
            {
                Id = Guid.Empty,
                Name = "Publisher1"
            };
            var publisher2 = new PublisherModel
            {
                Id = Guid.Empty,
                Name = "Publisher2"
            };

            _publisherService.Save(publisher1);
            _publisherService.Save(publisher2);

            // Act
            var publishers = _publisherService.GetPublishers().ToList();

            // Assert
            Assert.AreEqual(2, publishers.Count);
        }

        [TestMethod]
        public void Save_ExistingPublisher_UpdatesPublisher()
        {
            // Arrange
            var publisher = new PublisherModel
            {
                Id = Guid.Empty,
                Name = "OriginalName"
            };
            var savedPublisher = _publisherService.Save(publisher);
            var originalCreatedAt = savedPublisher.CreatedAt;
            var originalUpdatedAt = savedPublisher.UpdatedAt;

            // Update the publisher
            savedPublisher.Name = "UpdatedName";

            // Act
            var updatedPublisher = _publisherService.Save(savedPublisher);

            // Assert
            Assert.AreEqual(savedPublisher.Id, updatedPublisher.Id);
            Assert.AreEqual("UpdatedName", updatedPublisher.Name);
            Assert.AreEqual(originalCreatedAt, updatedPublisher.CreatedAt, "CreatedAt should be preserved");
            Assert.IsTrue(updatedPublisher.UpdatedAt >= originalUpdatedAt, "UpdatedAt should be updated");
        }

        [TestMethod]
        public void DeletePublisher_ExistingId_ReturnsTrue()
        {
            // Arrange
            var publisher = new PublisherModel
            {
                Id = Guid.Empty,
                Name = "TestPublisher"
            };
            var savedPublisher = _publisherService.Save(publisher);

            // Act
            var result = _publisherService.DeletePublisher(savedPublisher.Id);

            // Assert
            Assert.IsTrue(result);
            Assert.IsNull(_publisherService.GetPublisher(savedPublisher.Id));
        }

        [TestMethod]
        public void DeletePublisher_NonExistingId_ReturnsFalse()
        {
            // Act
            var result = _publisherService.DeletePublisher(Guid.NewGuid());

            // Assert
            Assert.IsFalse(result);
        }
    }
}
