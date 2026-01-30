using MessageService.Model.Subscriber;

namespace MessageService.Service.Subscriber
{
    public class SubscriberService
    {
        private readonly Dictionary<Guid, SubscriberModel> _subscribers = new Dictionary<Guid, SubscriberModel>();

        public SubscriberModel? GetSubscriber(Guid id)
        {
            return _subscribers.TryGetValue(id, out var subscriber) ? subscriber : null;
        }

        public IEnumerable<SubscriberModel> GetSubscribers()
        {
            return _subscribers.Values;
        }

        public SubscriberModel Save(SubscriberModel subscriber)
        {
            if (subscriber.Id == Guid.Empty)
            {
                subscriber.Id = Guid.NewGuid();
                Create(subscriber);
            }
            else
            {
                Update(subscriber);
            }
            return subscriber;
        }

        private void Create(SubscriberModel subscriber)
        {
            subscriber.CreatedAt = DateTime.UtcNow;
            subscriber.UpdatedAt = DateTime.UtcNow;
            _subscribers[subscriber.Id] = subscriber;
        }

        private void Update(SubscriberModel subscriber)
        {
            if (_subscribers.ContainsKey(subscriber.Id))
            {
                subscriber.UpdatedAt = DateTime.UtcNow;
                _subscribers[subscriber.Id] = subscriber;
            }
            else
            {
                throw new KeyNotFoundException($"Subscriber with ID {subscriber.Id} not found.");
            }
        }

        public bool DeleteSubscriber(Guid id)
        {
            return _subscribers.Remove(id);
        }
    }
}
