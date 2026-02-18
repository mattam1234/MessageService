using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MessageService.Model.Application;
using MessageService.Model.Publisher;
using MessageService.Model.Subscriber;
using MessageService.Model.Notifier;
using MessageService.Model.Flow;
using MessageService.Model.Flow.enums;
using MessageService.Service.Application;
using MessageService.Service.Publisher;
using MessageService.Service.Subscriber;
using MessageService.Service.Notifier;
using MessageService.Service.Flow;

namespace MessageService.GUI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly ApplicationService _applicationService;
    private readonly PublisherService _publisherService;
    private readonly SubscriberService _subscriberService;
    private readonly NotifierService _notifierService;
    private readonly FlowService _flowService;

    [ObservableProperty]
    private ObservableCollection<ApplicationModel> _applications = new();

    [ObservableProperty]
    private ObservableCollection<PublisherModel> _publishers = new();

    [ObservableProperty]
    private ObservableCollection<SubscriberModel> _subscribers = new();

    [ObservableProperty]
    private ObservableCollection<NotifierModel> _notifiers = new();

    [ObservableProperty]
    private ObservableCollection<FlowModel> _flows = new();

    [ObservableProperty]
    private ApplicationModel? _selectedApplication;

    [ObservableProperty]
    private PublisherModel? _selectedPublisher;

    [ObservableProperty]
    private SubscriberModel? _selectedSubscriber;

    [ObservableProperty]
    private NotifierModel? _selectedNotifier;

    [ObservableProperty]
    private FlowModel? _selectedFlow;

    [ObservableProperty]
    private string _applicationStatusText = "No applications loaded";

    [ObservableProperty]
    private string _publisherStatusText = "No publishers loaded";

    [ObservableProperty]
    private string _subscriberStatusText = "No subscribers loaded";

    [ObservableProperty]
    private string _notifierStatusText = "No notifiers loaded";

    [ObservableProperty]
    private string _flowStatusText = "No flows loaded";

    public MainWindowViewModel()
    {
        _applicationService = new ApplicationService();
        _publisherService = new PublisherService();
        _subscriberService = new SubscriberService();
        _notifierService = new NotifierService();
        _flowService = new FlowService();

        LoadAllData();
    }

    private void LoadAllData()
    {
        RefreshApplications();
        RefreshPublishers();
        RefreshSubscribers();
        RefreshNotifiers();
        RefreshFlows();
    }

    // Application Commands
    [RelayCommand]
    private void AddApplication()
    {
        var newApp = new ApplicationModel
        {
            Id = Guid.NewGuid(),
            applicationName = $"Application {Applications.Count + 1}",
            applicationType = ApplicationType.Both,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        _applicationService.Save(newApp);
        RefreshApplications();
    }

    [RelayCommand]
    private void DeleteApplication()
    {
        if (SelectedApplication != null)
        {
            _applicationService.DeleteApplication(SelectedApplication.Id);
            RefreshApplications();
        }
    }

    [RelayCommand]
    private void RefreshApplications()
    {
        var apps = _applicationService.GetApplications().ToList();
        Applications.Clear();
        foreach (var app in apps)
        {
            Applications.Add(app);
        }
        ApplicationStatusText = $"Loaded {Applications.Count} application(s)";
    }

    // Publisher Commands
    [RelayCommand]
    private void AddPublisher()
    {
        var newPublisher = new PublisherModel
        {
            Id = Guid.NewGuid(),
            Name = $"Publisher {Publishers.Count + 1}",
            Description = "Auto-generated publisher",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        _publisherService.Save(newPublisher);
        RefreshPublishers();
    }

    [RelayCommand]
    private void DeletePublisher()
    {
        if (SelectedPublisher != null)
        {
            _publisherService.DeletePublisher(SelectedPublisher.Id);
            RefreshPublishers();
        }
    }

    [RelayCommand]
    private void RefreshPublishers()
    {
        var publishers = _publisherService.GetPublishers().ToList();
        Publishers.Clear();
        foreach (var publisher in publishers)
        {
            Publishers.Add(publisher);
        }
        PublisherStatusText = $"Loaded {Publishers.Count} publisher(s)";
    }

    // Subscriber Commands
    [RelayCommand]
    private void AddSubscriber()
    {
        var newSubscriber = new SubscriberModel
        {
            Id = Guid.NewGuid(),
            Name = $"Subscriber {Subscribers.Count + 1}",
            Description = "Auto-generated subscriber",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        _subscriberService.Save(newSubscriber);
        RefreshSubscribers();
    }

    [RelayCommand]
    private void DeleteSubscriber()
    {
        if (SelectedSubscriber != null)
        {
            _subscriberService.DeleteSubscriber(SelectedSubscriber.Id);
            RefreshSubscribers();
        }
    }

    [RelayCommand]
    private void RefreshSubscribers()
    {
        var subscribers = _subscriberService.GetSubscribers().ToList();
        Subscribers.Clear();
        foreach (var subscriber in subscribers)
        {
            Subscribers.Add(subscriber);
        }
        SubscriberStatusText = $"Loaded {Subscribers.Count} subscriber(s)";
    }

    // Notifier Commands
    [RelayCommand]
    private void AddNotifier()
    {
        var newNotifier = new NotifierModel
        {
            Id = Guid.NewGuid(),
            Name = $"Notifier {Notifiers.Count + 1}",
            Description = "Auto-generated notifier",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        _notifierService.Save(newNotifier);
        RefreshNotifiers();
    }

    [RelayCommand]
    private void DeleteNotifier()
    {
        if (SelectedNotifier != null)
        {
            _notifierService.DeleteNotifier(SelectedNotifier.Id);
            RefreshNotifiers();
        }
    }

    [RelayCommand]
    private void RefreshNotifiers()
    {
        var notifiers = _notifierService.GetNotifiers().ToList();
        Notifiers.Clear();
        foreach (var notifier in notifiers)
        {
            Notifiers.Add(notifier);
        }
        NotifierStatusText = $"Loaded {Notifiers.Count} notifier(s)";
    }

    // Flow Commands
    [RelayCommand]
    private void AddFlow()
    {
        // Get or create a sample application for the flow
        var app = Applications.FirstOrDefault();
        if (app == null)
        {
            app = new ApplicationModel
            {
                Id = Guid.NewGuid(),
                applicationName = "Default Application",
                applicationType = ApplicationType.Both,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            _applicationService.Save(app);
            RefreshApplications();
        }

        // Create a sample message
        var message = new Message
        {
            Id = Guid.NewGuid(),
            MessageName = $"Sample Message {Flows.Count + 1}",
            MessageType = Types.Info,
            MessageData = $"Sample message data created at {DateTime.Now}",
            MessageTrace = null,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        
        var newFlow = new FlowModel
        {
            Id = Guid.NewGuid(),
            Application = app,
            Message = message,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        _flowService.Save(newFlow);
        RefreshFlows();
    }

    [RelayCommand]
    private void DeleteFlow()
    {
        if (SelectedFlow != null)
        {
            _flowService.DeleteFlow(SelectedFlow.Id);
            RefreshFlows();
        }
    }

    [RelayCommand]
    private void RefreshFlows()
    {
        var flows = _flowService.GetFlows().ToList();
        Flows.Clear();
        foreach (var flow in flows)
        {
            Flows.Add(flow);
        }
        FlowStatusText = $"Loaded {Flows.Count} flow(s)";
    }
}
