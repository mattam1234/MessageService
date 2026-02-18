using MessageService.Model.Application;

namespace MessageService.Service.Application
{
    public class ApplicationService
    {
        private readonly Dictionary<Guid, ApplicationModel> _applications = new Dictionary<Guid, ApplicationModel>();

        public ApplicationModel? GetApplication(Guid id)
        {
            return _applications.TryGetValue(id, out var application) ? application : null;
        }

        public IEnumerable<ApplicationModel> GetApplications()
        {
            return _applications.Values;
        }

        public ApplicationModel Save(ApplicationModel application)
        {
            if (application.Id == Guid.Empty)
            {
                application.Id = Guid.NewGuid();
                Create(application);
            }
            else
            {
                Update(application);
            }
            return application;
        }

        private void Create(ApplicationModel application)
        {
            application.CreatedAt = DateTime.UtcNow;
            application.UpdatedAt = DateTime.UtcNow;
            _applications[application.Id] = application;
        }

        private void Update(ApplicationModel application)
        {
            if (!_applications.TryGetValue(application.Id, out var existingApplication))
            {
                throw new KeyNotFoundException($"Application with ID {application.Id} not found.");
            }
            
            // Preserve server-managed fields from the existing stored application
            application.CreatedAt = existingApplication.CreatedAt;
            application.UpdatedAt = DateTime.UtcNow;
            _applications[application.Id] = application;
        }

        public bool DeleteApplication(Guid id)
        {
            return _applications.Remove(id);
        }
    }
}
