using MessageService.Model.Flow;
using MessageService.Model.Application;
using MessageService.Model.Flow.enums;
using MessageService.Service.Flow;

namespace MessageService.Test
{
    [TestClass]
    public class FlowServiceTests
    {
        private FlowService _flowService = null!;

        [TestInitialize]
        public void Setup()
        {
            _flowService = new FlowService();
        }

        [TestMethod]
        public void Save_NewFlow_CreatesFlowWithNewId()
        {
            // Arrange
            var application = new ApplicationModel
            {
                Id = Guid.NewGuid(),
                applicationName = "TestApp",
                applicationType = ApplicationType.Producer
            };

            var message = new Message
            {
                Id = Guid.NewGuid(),
                MessageType = Types.Info,
                MessageData = "Test message"
            };

            var flow = new FlowModel
            {
                Id = Guid.Empty,
                Application = application,
                Message = message
            };

            // Act
            var savedFlow = _flowService.Save(flow);

            // Assert
            Assert.IsNotNull(savedFlow);
            Assert.AreNotEqual(Guid.Empty, savedFlow.Id);
            Assert.IsTrue(savedFlow.CreatedAt > DateTime.MinValue);
            Assert.IsTrue(savedFlow.UpdatedAt > DateTime.MinValue);
        }

        [TestMethod]
        public void GetFlow_ExistingId_ReturnsFlow()
        {
            // Arrange
            var application = new ApplicationModel
            {
                Id = Guid.NewGuid(),
                applicationName = "TestApp",
                applicationType = ApplicationType.Producer
            };

            var message = new Message
            {
                Id = Guid.NewGuid(),
                MessageType = Types.Info,
                MessageData = "Test message"
            };

            var flow = new FlowModel
            {
                Id = Guid.Empty,
                Application = application,
                Message = message
            };

            var savedFlow = _flowService.Save(flow);

            // Act
            var retrievedFlow = _flowService.GetFlow(savedFlow.Id);

            // Assert
            Assert.IsNotNull(retrievedFlow);
            Assert.AreEqual(savedFlow.Id, retrievedFlow.Id);
        }

        [TestMethod]
        public void GetFlow_NonExistingId_ReturnsNull()
        {
            // Act
            var retrievedFlow = _flowService.GetFlow(Guid.NewGuid());

            // Assert
            Assert.IsNull(retrievedFlow);
        }

        [TestMethod]
        public void GetFlows_ReturnsAllFlows()
        {
            // Arrange
            var application = new ApplicationModel
            {
                Id = Guid.NewGuid(),
                applicationName = "TestApp",
                applicationType = ApplicationType.Producer
            };

            var message1 = new Message
            {
                Id = Guid.NewGuid(),
                MessageType = Types.Info,
                MessageData = "Test message 1"
            };

            var message2 = new Message
            {
                Id = Guid.NewGuid(),
                MessageType = Types.Warning,
                MessageData = "Test message 2"
            };

            var flow1 = new FlowModel { Id = Guid.Empty, Application = application, Message = message1 };
            var flow2 = new FlowModel { Id = Guid.Empty, Application = application, Message = message2 };

            _flowService.Save(flow1);
            _flowService.Save(flow2);

            // Act
            var flows = _flowService.GetFlows().ToList();

            // Assert
            Assert.AreEqual(2, flows.Count);
        }

        [TestMethod]
        public void Save_ExistingFlow_UpdatesFlow()
        {
            // Arrange
            var application = new ApplicationModel
            {
                Id = Guid.NewGuid(),
                applicationName = "TestApp",
                applicationType = ApplicationType.Producer
            };

            var message = new Message
            {
                Id = Guid.NewGuid(),
                MessageType = Types.Info,
                MessageData = "Original message"
            };

            var flow = new FlowModel { Id = Guid.Empty, Application = application, Message = message };
            var savedFlow = _flowService.Save(flow);
            var originalCreatedAt = savedFlow.CreatedAt;
            var originalUpdatedAt = savedFlow.UpdatedAt;

            // Update the message
            savedFlow.Message.MessageData = "Updated message";

            // Act
            var updatedFlow = _flowService.Save(savedFlow);

            // Assert
            Assert.AreEqual(savedFlow.Id, updatedFlow.Id);
            Assert.AreEqual("Updated message", updatedFlow.Message.MessageData);
            Assert.AreEqual(originalCreatedAt, updatedFlow.CreatedAt, "CreatedAt should be preserved");
            Assert.IsTrue(updatedFlow.UpdatedAt >= originalUpdatedAt, "UpdatedAt should be updated");
        }

        [TestMethod]
        public void DeleteFlow_ExistingId_ReturnsTrue()
        {
            // Arrange
            var application = new ApplicationModel
            {
                Id = Guid.NewGuid(),
                applicationName = "TestApp",
                applicationType = ApplicationType.Producer
            };

            var message = new Message
            {
                Id = Guid.NewGuid(),
                MessageType = Types.Info,
                MessageData = "Test message"
            };

            var flow = new FlowModel { Id = Guid.Empty, Application = application, Message = message };
            var savedFlow = _flowService.Save(flow);

            // Act
            var result = _flowService.DeleteFlow(savedFlow.Id);

            // Assert
            Assert.IsTrue(result);
            Assert.IsNull(_flowService.GetFlow(savedFlow.Id));
        }

        [TestMethod]
        public void DeleteFlow_NonExistingId_ReturnsFalse()
        {
            // Act
            var result = _flowService.DeleteFlow(Guid.NewGuid());

            // Assert
            Assert.IsFalse(result);
        }
    }
}
