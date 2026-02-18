# MessageService
This package is a library that provides a message service to send messages to a flow. The messages can be sent to a flow in the following ways:

## Projects

### MessageService.GUI (New!)
A cross-platform graphical user interface for managing the MessageService built with Avalonia UI. Features include:
- **Applications Management**: Create, view, and delete applications (Producer, Consumer, or Both)
- **Publishers Management**: Manage message publishers
- **Subscribers Management**: Manage message subscribers
- **Notifiers Management**: Manage notification channels
- **Flows Management**: View and manage message flows with full message details

To run the GUI:
```bash
cd MessageService.GUI
dotnet run
```

See [MessageService.GUI/README.md](MessageService.GUI/README.md) for more details.

### Other Projects
- **MessageService**: Core library
- **MessageService.Model**: Data models
- **MessageService.Service**: Business logic services
- **MessageService.Console**: Console application
- **MessageService.Test**: Unit tests
- **TestMessageLoop**: Test utilities

## Usage
this package can be used in the following ways:
	- Using the GUI application to manage message flows visually
	- Implementing your own application to use the message service
	- Creating a new package that extends the message service


### Configuration
TBA
#### Database
TBA
#### MessageService
TBA
#### External NotificationService
TBA

### Reporting to a flow
TBA
### Listing to a flow
TBA
