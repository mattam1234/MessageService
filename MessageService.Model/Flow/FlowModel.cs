using MessageService.Model.Application;

namespace MessageService.Model.Flow
{
    public class FlowModel
    {
        public Guid Id { get; set; }
        public ApplicationModel Application { get; set; }
        public Message message { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
