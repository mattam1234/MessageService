using MessageService.Model.Publisher;

namespace MessageService.Service.Publisher
{
    public class PublisherService
    {
        private readonly Dictionary<Guid, PublisherModel> _publishers = new Dictionary<Guid, PublisherModel>();

        public PublisherModel? GetPublisher(Guid id)
        {
            return _publishers.TryGetValue(id, out var publisher) ? publisher : null;
        }

        public IEnumerable<PublisherModel> GetPublishers()
        {
            return _publishers.Values;
        }

        public PublisherModel Save(PublisherModel publisher)
        {
            if (publisher.Id == Guid.Empty)
            {
                publisher.Id = Guid.NewGuid();
                Create(publisher);
            }
            else
            {
                Update(publisher);
            }
            return publisher;
        }

        private void Create(PublisherModel publisher)
        {
            publisher.CreatedAt = DateTime.UtcNow;
            publisher.UpdatedAt = DateTime.UtcNow;
            _publishers[publisher.Id] = publisher;
        }

        private void Update(PublisherModel publisher)
        {
            if (!_publishers.ContainsKey(publisher.Id))
            {
                throw new KeyNotFoundException($"Publisher with ID {publisher.Id} not found.");
            }
            publisher.UpdatedAt = DateTime.UtcNow;
            _publishers[publisher.Id] = publisher;
        }

        public bool DeletePublisher(Guid id)
        {
            return _publishers.Remove(id);
        }
    }
}
