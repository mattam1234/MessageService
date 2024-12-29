namespace MessageService.Model.Application
{
    public class ApplicationModel
    {
        public Guid Id { get; set; }
        public required string applicationName { get; set; }
        public ApplicationType applicationType { get; set; }
    }
}
