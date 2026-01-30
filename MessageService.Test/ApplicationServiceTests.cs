using MessageService.Model.Application;
using MessageService.Service.Application;

namespace MessageService.Test
{
    [TestClass]
    public class ApplicationServiceTests
    {
        private ApplicationService _applicationService = null!;

        [TestInitialize]
        public void Setup()
        {
            _applicationService = new ApplicationService();
        }

        [TestMethod]
        public void Save_NewApplication_CreatesApplicationWithNewId()
        {
            // Arrange
            var application = new ApplicationModel
            {
                Id = Guid.Empty,
                applicationName = "TestApp",
                applicationType = ApplicationType.Producer
            };

            // Act
            var savedApplication = _applicationService.Save(application);

            // Assert
            Assert.IsNotNull(savedApplication);
            Assert.AreNotEqual(Guid.Empty, savedApplication.Id);
        }

        [TestMethod]
        public void GetApplication_ExistingId_ReturnsApplication()
        {
            // Arrange
            var application = new ApplicationModel
            {
                Id = Guid.Empty,
                applicationName = "TestApp",
                applicationType = ApplicationType.Producer
            };
            var savedApplication = _applicationService.Save(application);

            // Act
            var retrievedApplication = _applicationService.GetApplication(savedApplication.Id);

            // Assert
            Assert.IsNotNull(retrievedApplication);
            Assert.AreEqual(savedApplication.Id, retrievedApplication.Id);
            Assert.AreEqual("TestApp", retrievedApplication.applicationName);
        }

        [TestMethod]
        public void GetApplication_NonExistingId_ReturnsNull()
        {
            // Act
            var retrievedApplication = _applicationService.GetApplication(Guid.NewGuid());

            // Assert
            Assert.IsNull(retrievedApplication);
        }

        [TestMethod]
        public void GetApplications_ReturnsAllApplications()
        {
            // Arrange
            var app1 = new ApplicationModel
            {
                Id = Guid.Empty,
                applicationName = "App1",
                applicationType = ApplicationType.Producer
            };
            var app2 = new ApplicationModel
            {
                Id = Guid.Empty,
                applicationName = "App2",
                applicationType = ApplicationType.Consumer
            };

            _applicationService.Save(app1);
            _applicationService.Save(app2);

            // Act
            var applications = _applicationService.GetApplications().ToList();

            // Assert
            Assert.AreEqual(2, applications.Count);
        }

        [TestMethod]
        public void Save_ExistingApplication_UpdatesApplication()
        {
            // Arrange
            var application = new ApplicationModel
            {
                Id = Guid.Empty,
                applicationName = "OriginalName",
                applicationType = ApplicationType.Producer
            };
            var savedApplication = _applicationService.Save(application);

            // Update the application
            savedApplication.applicationName = "UpdatedName";

            // Act
            var updatedApplication = _applicationService.Save(savedApplication);

            // Assert
            Assert.AreEqual(savedApplication.Id, updatedApplication.Id);
            Assert.AreEqual("UpdatedName", updatedApplication.applicationName);
        }

        [TestMethod]
        public void DeleteApplication_ExistingId_ReturnsTrue()
        {
            // Arrange
            var application = new ApplicationModel
            {
                Id = Guid.Empty,
                applicationName = "TestApp",
                applicationType = ApplicationType.Producer
            };
            var savedApplication = _applicationService.Save(application);

            // Act
            var result = _applicationService.DeleteApplication(savedApplication.Id);

            // Assert
            Assert.IsTrue(result);
            Assert.IsNull(_applicationService.GetApplication(savedApplication.Id));
        }

        [TestMethod]
        public void DeleteApplication_NonExistingId_ReturnsFalse()
        {
            // Act
            var result = _applicationService.DeleteApplication(Guid.NewGuid());

            // Assert
            Assert.IsFalse(result);
        }
    }
}
