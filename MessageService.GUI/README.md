# MessageService GUI

This is a cross-platform GUI application for managing the MessageService using Avalonia UI.

## Features

The MessageService GUI provides a modern, tabbed interface for managing all aspects of the MessageService:

### 📱 Application Management
- View all registered applications
- Add new applications
- Delete existing applications
- See application type (Producer, Consumer, or Both)

### 📤 Publisher Management
- View all publishers
- Add new publishers
- Delete existing publishers
- Track creation timestamps

### 📥 Subscriber Management
- View all subscribers
- Add new subscribers
- Delete existing subscribers
- Track creation timestamps

### 🔔 Notifier Management
- View all notifiers
- Add new notifiers
- Delete existing notifiers
- Track creation timestamps

### 🔄 Flow Management
- View all message flows
- Add new flows
- Delete existing flows
- See application and message details
- View message types (Error, Info, Warning, Success, etc.)

## Running the Application

### Prerequisites
- .NET 10.0 SDK or later
- Windows, macOS, or Linux

### To Run
```bash
cd MessageService.GUI
dotnet run
```

### To Build
```bash
dotnet build MessageService.GUI/MessageService.GUI.csproj
```

## Architecture

The GUI follows the MVVM (Model-View-ViewModel) pattern:
- **Models**: Provided by MessageService.Model project
- **Views**: XAML files in the Views folder
- **ViewModels**: C# classes in the ViewModels folder using CommunityToolkit.Mvvm

The GUI integrates with the existing MessageService.Service layer, providing a visual interface for:
- ApplicationService
- PublisherService
- SubscriberService
- NotifierService
- FlowService

## User Interface

The main window features:
- **Header**: Title and description
- **Tabs**: Separate tabs for Applications, Publishers, Subscribers, Notifiers, and Flows
- **DataGrids**: Display all entities with sortable columns
- **Action Buttons**: Add, Delete, and Refresh buttons for each entity type
- **Status Bar**: Shows count of loaded entities

## Technology Stack

- **Avalonia UI**: Cross-platform XAML-based UI framework
- **CommunityToolkit.Mvvm**: For MVVM pattern implementation
- **.NET 10.0**: Latest .NET framework
- **MessageService.Model & .Service**: Backend integration

## Future Enhancements

Potential improvements for the GUI:
- Add edit dialogs for modifying existing entities
- Advanced filtering and search
- Real-time message monitoring
- Visual flow designer
- Statistics and analytics dashboards
- Import/Export functionality
- Dark/Light theme toggle
