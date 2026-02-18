
using MessageService.Model.Flow;

namespace MessageService.Service.Flow
{
    public class FlowService
    {
        private readonly Dictionary<Guid, FlowModel> _flows = new Dictionary<Guid, FlowModel>();

        public FlowModel? GetFlow(Guid id)
        {
            return _flows.TryGetValue(id, out var flow) ? flow : null;
        }

        public IEnumerable<FlowModel> GetFlows()
        {
            return _flows.Values;
        }

        public FlowModel Save(FlowModel flow)
        {
            if (flow.Id == Guid.Empty)
            {
                flow.Id = Guid.NewGuid();
                Create(flow);
            }
            else
            {
                Update(flow);
            }
            return flow;
        }

        private void Create(FlowModel flow)
        {
            flow.CreatedAt = DateTime.UtcNow;
            flow.UpdatedAt = DateTime.UtcNow;
            _flows[flow.Id] = flow;
        }

        private void Update(FlowModel flow)
        {
            if (!_flows.TryGetValue(flow.Id, out var existingFlow))
            {
                throw new KeyNotFoundException($"Flow with ID {flow.Id} not found.");
            }
            
            // Preserve original CreatedAt, only update the modification time.
            flow.CreatedAt = existingFlow.CreatedAt;
            flow.UpdatedAt = DateTime.UtcNow;
            _flows[flow.Id] = flow;
        }

        public bool DeleteFlow(Guid id)
        {
            return _flows.Remove(id);
        }
    }
}
