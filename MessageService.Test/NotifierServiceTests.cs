using MessageService.Model.Notifier;
using MessageService.Service.Notifier;

namespace MessageService.Test
{
    [TestClass]
    public class NotifierServiceTests
    {
        private NotifierService _notifierService = null!;

        [TestInitialize]
        public void Setup()
        {
            _notifierService = new NotifierService();
        }

        [TestMethod]
        public void Save_NewNotifier_CreatesNotifierWithNewId()
        {
            // Arrange
            var notifier = new NotifierModel
            {
                Id = Guid.Empty,
                Name = "TestNotifier",
                Description = "Test Description"
            };

            // Act
            var savedNotifier = _notifierService.Save(notifier);

            // Assert
            Assert.IsNotNull(savedNotifier);
            Assert.AreNotEqual(Guid.Empty, savedNotifier.Id);
            Assert.IsTrue(savedNotifier.CreatedAt > DateTime.MinValue);
            Assert.IsTrue(savedNotifier.UpdatedAt > DateTime.MinValue);
        }

        [TestMethod]
        public void GetNotifier_ExistingId_ReturnsNotifier()
        {
            // Arrange
            var notifier = new NotifierModel
            {
                Id = Guid.Empty,
                Name = "TestNotifier",
                Description = "Test Description"
            };
            var savedNotifier = _notifierService.Save(notifier);

            // Act
            var retrievedNotifier = _notifierService.GetNotifier(savedNotifier.Id);

            // Assert
            Assert.IsNotNull(retrievedNotifier);
            Assert.AreEqual(savedNotifier.Id, retrievedNotifier.Id);
            Assert.AreEqual("TestNotifier", retrievedNotifier.Name);
        }

        [TestMethod]
        public void GetNotifier_NonExistingId_ReturnsNull()
        {
            // Act
            var retrievedNotifier = _notifierService.GetNotifier(Guid.NewGuid());

            // Assert
            Assert.IsNull(retrievedNotifier);
        }

        [TestMethod]
        public void GetNotifiers_ReturnsAllNotifiers()
        {
            // Arrange
            var notifier1 = new NotifierModel
            {
                Id = Guid.Empty,
                Name = "Notifier1"
            };
            var notifier2 = new NotifierModel
            {
                Id = Guid.Empty,
                Name = "Notifier2"
            };

            _notifierService.Save(notifier1);
            _notifierService.Save(notifier2);

            // Act
            var notifiers = _notifierService.GetNotifiers().ToList();

            // Assert
            Assert.AreEqual(2, notifiers.Count);
        }

        [TestMethod]
        public void Save_ExistingNotifier_UpdatesNotifier()
        {
            // Arrange
            var notifier = new NotifierModel
            {
                Id = Guid.Empty,
                Name = "OriginalName"
            };
            var savedNotifier = _notifierService.Save(notifier);
            var originalCreatedAt = savedNotifier.CreatedAt;
            var originalUpdatedAt = savedNotifier.UpdatedAt;

            // Update the notifier
            savedNotifier.Name = "UpdatedName";

            // Act
            var updatedNotifier = _notifierService.Save(savedNotifier);

            // Assert
            Assert.AreEqual(savedNotifier.Id, updatedNotifier.Id);
            Assert.AreEqual("UpdatedName", updatedNotifier.Name);
            Assert.AreEqual(originalCreatedAt, updatedNotifier.CreatedAt, "CreatedAt should be preserved");
            Assert.IsTrue(updatedNotifier.UpdatedAt >= originalUpdatedAt, "UpdatedAt should be updated");
        }

        [TestMethod]
        public void DeleteNotifier_ExistingId_ReturnsTrue()
        {
            // Arrange
            var notifier = new NotifierModel
            {
                Id = Guid.Empty,
                Name = "TestNotifier"
            };
            var savedNotifier = _notifierService.Save(notifier);

            // Act
            var result = _notifierService.DeleteNotifier(savedNotifier.Id);

            // Assert
            Assert.IsTrue(result);
            Assert.IsNull(_notifierService.GetNotifier(savedNotifier.Id));
        }

        [TestMethod]
        public void DeleteNotifier_NonExistingId_ReturnsFalse()
        {
            // Act
            var result = _notifierService.DeleteNotifier(Guid.NewGuid());

            // Assert
            Assert.IsFalse(result);
        }
    }
}
