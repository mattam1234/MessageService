using MessageService.Model.Notifier;

namespace MessageService.Service.Notifier
{
    public class NotifierService
    {
        private readonly Dictionary<Guid, NotifierModel> _notifiers = new Dictionary<Guid, NotifierModel>();

        public NotifierModel? GetNotifier(Guid id)
        {
            return _notifiers.TryGetValue(id, out var notifier) ? notifier : null;
        }

        public IEnumerable<NotifierModel> GetNotifiers()
        {
            return _notifiers.Values;
        }

        public NotifierModel Save(NotifierModel notifier)
        {
            if (notifier.Id == Guid.Empty)
            {
                notifier.Id = Guid.NewGuid();
                Create(notifier);
            }
            else
            {
                Update(notifier);
            }
            return notifier;
        }

        private void Create(NotifierModel notifier)
        {
            notifier.CreatedAt = DateTime.UtcNow;
            notifier.UpdatedAt = DateTime.UtcNow;
            _notifiers[notifier.Id] = notifier;
        }

        private void Update(NotifierModel notifier)
        {
            if (_notifiers.ContainsKey(notifier.Id))
            {
                notifier.UpdatedAt = DateTime.UtcNow;
                _notifiers[notifier.Id] = notifier;
            }
            else
            {
                throw new KeyNotFoundException($"Notifier with ID {notifier.Id} not found.");
            }
        }

        public bool DeleteNotifier(Guid id)
        {
            return _notifiers.Remove(id);
        }
    }
}
