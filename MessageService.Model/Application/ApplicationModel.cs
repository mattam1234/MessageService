namespace MessageService.Model.Application
{
    public class ApplicationModel
    {
        public Guid Id { get; set; }
        public required string applicationName { get; set; }
        public ApplicationType applicationType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
