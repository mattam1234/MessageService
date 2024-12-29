using MessageService.Model.Flow.enums;
using System.Diagnostics;

namespace MessageService.Model.Flow
{
    public class Message
    {
        public Guid Id { get; set; }
        public string? MessageName { get; set; }
        public required Types MessageType { get; set; }
        public string? MessageData { get; set; }
        public Trace? MessageTrace { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
