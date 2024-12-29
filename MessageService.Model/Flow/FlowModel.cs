using MessageService.Model.Application;

namespace MessageService.Model.Flow
{
    public class FlowModel
    {
        public Guid Id { get; set; }
        public required ApplicationModel Application { get; set; }
        public required Message Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
