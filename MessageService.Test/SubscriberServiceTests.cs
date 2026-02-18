using MessageService.Model.Subscriber;
using MessageService.Service.Subscriber;

namespace MessageService.Test
{
    [TestClass]
    public class SubscriberServiceTests
    {
        private SubscriberService _subscriberService = null!;

        [TestInitialize]
        public void Setup()
        {
            _subscriberService = new SubscriberService();
        }

        [TestMethod]
        public void Save_NewSubscriber_CreatesSubscriberWithNewId()
        {
            // Arrange
            var subscriber = new SubscriberModel
            {
                Id = Guid.Empty,
                Name = "TestSubscriber",
                Description = "Test Description"
            };

            // Act
            var savedSubscriber = _subscriberService.Save(subscriber);

            // Assert
            Assert.IsNotNull(savedSubscriber);
            Assert.AreNotEqual(Guid.Empty, savedSubscriber.Id);
            Assert.IsTrue(savedSubscriber.CreatedAt > DateTime.MinValue);
            Assert.IsTrue(savedSubscriber.UpdatedAt > DateTime.MinValue);
        }

        [TestMethod]
        public void GetSubscriber_ExistingId_ReturnsSubscriber()
        {
            // Arrange
            var subscriber = new SubscriberModel
            {
                Id = Guid.Empty,
                Name = "TestSubscriber",
                Description = "Test Description"
            };
            var savedSubscriber = _subscriberService.Save(subscriber);

            // Act
            var retrievedSubscriber = _subscriberService.GetSubscriber(savedSubscriber.Id);

            // Assert
            Assert.IsNotNull(retrievedSubscriber);
            Assert.AreEqual(savedSubscriber.Id, retrievedSubscriber.Id);
            Assert.AreEqual("TestSubscriber", retrievedSubscriber.Name);
        }

        [TestMethod]
        public void GetSubscriber_NonExistingId_ReturnsNull()
        {
            // Act
            var retrievedSubscriber = _subscriberService.GetSubscriber(Guid.NewGuid());

            // Assert
            Assert.IsNull(retrievedSubscriber);
        }

        [TestMethod]
        public void GetSubscribers_ReturnsAllSubscribers()
        {
            // Arrange
            var subscriber1 = new SubscriberModel
            {
                Id = Guid.Empty,
                Name = "Subscriber1"
            };
            var subscriber2 = new SubscriberModel
            {
                Id = Guid.Empty,
                Name = "Subscriber2"
            };

            _subscriberService.Save(subscriber1);
            _subscriberService.Save(subscriber2);

            // Act
            var subscribers = _subscriberService.GetSubscribers().ToList();

            // Assert
            Assert.AreEqual(2, subscribers.Count);
        }

        [TestMethod]
        public void Save_ExistingSubscriber_UpdatesSubscriber()
        {
            // Arrange
            var subscriber = new SubscriberModel
            {
                Id = Guid.Empty,
                Name = "OriginalName"
            };
            var savedSubscriber = _subscriberService.Save(subscriber);
            var originalCreatedAt = savedSubscriber.CreatedAt;
            var originalUpdatedAt = savedSubscriber.UpdatedAt;

            // Update the subscriber
            savedSubscriber.Name = "UpdatedName";

            // Act
            var updatedSubscriber = _subscriberService.Save(savedSubscriber);

            // Assert
            Assert.AreEqual(savedSubscriber.Id, updatedSubscriber.Id);
            Assert.AreEqual("UpdatedName", updatedSubscriber.Name);
            Assert.AreEqual(originalCreatedAt, updatedSubscriber.CreatedAt, "CreatedAt should be preserved");
            Assert.IsTrue(updatedSubscriber.UpdatedAt >= originalUpdatedAt, "UpdatedAt should be updated");
        }

        [TestMethod]
        public void DeleteSubscriber_ExistingId_ReturnsTrue()
        {
            // Arrange
            var subscriber = new SubscriberModel
            {
                Id = Guid.Empty,
                Name = "TestSubscriber"
            };
            var savedSubscriber = _subscriberService.Save(subscriber);

            // Act
            var result = _subscriberService.DeleteSubscriber(savedSubscriber.Id);

            // Assert
            Assert.IsTrue(result);
            Assert.IsNull(_subscriberService.GetSubscriber(savedSubscriber.Id));
        }

        [TestMethod]
        public void DeleteSubscriber_NonExistingId_ReturnsFalse()
        {
            // Act
            var result = _subscriberService.DeleteSubscriber(Guid.NewGuid());

            // Assert
            Assert.IsFalse(result);
        }
    }
}
