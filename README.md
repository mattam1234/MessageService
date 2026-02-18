# MessageService

A .NET library that provides a message service for managing flows, publishers, subscribers, and notifiers. This library enables applications to send and receive messages through a flow-based architecture with in-memory storage.

## Table of Contents

- [Features](#features)
- [Requirements](#requirements)
- [Installation](#installation)
- [Architecture](#architecture)
- [Usage](#usage)
- [Project Structure](#project-structure)
- [Building and Testing](#building-and-testing)
- [Contributing](#contributing)
- [License](#license)

## Features

- **Flow-based messaging**: Messages are organized into flows with associated applications
- **Publisher/Subscriber pattern**: Support for both message publishing and subscription
- **In-memory storage**: Fast, lightweight storage using dictionaries
- **CRUD operations**: Full Create, Read, Update, Delete support for all entities
- **Message types**: Support for Info, Warning, Error, and other message types
- **Application types**: Support for Producer and Consumer application types
- **Notifier support**: Built-in notification system for message events

## Requirements

- **.NET 9.0 or higher**
- Visual Studio 2022 (or any compatible IDE)
- NuGet package manager

## Installation

### Using NuGet Package Manager

```bash
dotnet add package Netorrix.Util.MessageService
```

### From Source

1. Clone the repository:
```bash
git clone https://github.com/mattam1234/MessageService.git
cd MessageService
```

2. Build the solution:
```bash
dotnet build MessageService.sln
```

3. Add a reference to the project:
```bash
dotnet add reference /path/to/MessageService/MessageService.csproj
```

## Architecture

The MessageService library is organized into several components:

### Core Components

- **FlowModel**: Represents a message flow with an application and message
- **ApplicationModel**: Defines the application sending/receiving messages
- **Message**: Contains the actual message data and type
- **PublisherModel**: Represents a message publisher
- **SubscriberModel**: Represents a message subscriber
- **NotifierModel**: Handles notifications for message events

### Services

- **FlowService**: Manages flow CRUD operations
- **PublisherService**: Manages publisher CRUD operations
- **SubscriberService**: Manages subscriber CRUD operations
- **NotifierService**: Manages notifier CRUD operations

### Message Types

The library supports the following message types through the `Types` enum:
- `Info`: Informational messages
- `Warning`: Warning messages
- `Error`: Error messages
- And other custom types

### Application Types

- `Producer`: Applications that send messages
- `Consumer`: Applications that receive messages

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

### Creating a Flow

```csharp
using MessageService.Service.Flow;
using MessageService.Model.Flow;
using MessageService.Model.Application;
using MessageService.Model.Flow.enums;

// Create a flow service
var flowService = new FlowService();

// Create an application
var application = new ApplicationModel
{
    Id = Guid.NewGuid(),
    applicationName = "MyApp",
    applicationType = ApplicationType.Producer
};

// Create a message
var message = new Message
{
    Id = Guid.NewGuid(),
    MessageType = Types.Info,
    MessageData = "Hello, World!"
};

// Create and save a flow
var flow = new FlowModel
{
    Id = Guid.Empty,
    Application = application,
    Message = message
};

var savedFlow = flowService.Save(flow);
```

### Managing Publishers

```csharp
using MessageService.Service.Publisher;
using MessageService.Model.Publisher;

// Create a publisher service
var publisherService = new PublisherService();

// Create and save a publisher
var publisher = new PublisherModel
{
    Id = Guid.Empty,
    Name = "MyPublisher",
    Description = "Publishes important messages"
};

var savedPublisher = publisherService.Save(publisher);
```

### Managing Subscribers

```csharp
using MessageService.Service.Subscriber;
using MessageService.Model.Subscriber;

// Create a subscriber service
var subscriberService = new SubscriberService();

// Create and save a subscriber
var subscriber = new SubscriberModel
{
    Id = Guid.Empty,
    Name = "MySubscriber",
    Description = "Listens to message flows"
};

var savedSubscriber = subscriberService.Save(subscriber);
```

### Retrieving Flows

```csharp
// Get a specific flow
var flow = flowService.GetFlow(flowId);

// Get all flows
var allFlows = flowService.GetFlows();
```

### Updating a Flow

```csharp
// Modify an existing flow
flow.Message.MessageData = "Updated message";
var updatedFlow = flowService.Save(flow);
```

### Deleting a Flow

```csharp
// Delete a flow by ID
bool deleted = flowService.DeleteFlow(flowId);
```

## Project Structure

```
MessageService/
├── MessageService/              # Main library package
│   └── MessageService.csproj   # Package project file
├── MessageService.Model/        # Data models
│   ├── Application/            # Application models
│   ├── Flow/                   # Flow models
│   ├── Publisher/              # Publisher models
│   ├── Subscriber/             # Subscriber models
│   └── Notifier/               # Notifier models
├── MessageService.Service/      # Business logic services
│   ├── Application/            # Application services
│   ├── Flow/                   # Flow services
│   ├── Publisher/              # Publisher services
│   ├── Subscriber/             # Subscriber services
│   └── Notifier/               # Notifier services
├── MessageService.Test/         # Unit tests
├── MessageService.Console/      # Console application example
├── TestMessageLoop/             # Test message loop example
└── README.md                    # This file
```

## Building and Testing

### Building the Solution

```bash
# Build in Debug mode
dotnet build MessageService.sln

# Build in Release mode
dotnet build MessageService.sln --configuration Release
```

### Running Tests

```bash
# Run all tests
dotnet test MessageService.sln

# Run tests with verbose output
dotnet test MessageService.sln --verbosity normal
```

### Creating a NuGet Package

```bash
# Build and create package
dotnet pack MessageService/MessageService.csproj --configuration Release
```

The package will be created in the `bin/Release` directory.

## Contributing

Contributions are welcome! Here are some ways you can contribute:

1. **Report bugs**: Open an issue describing the bug and how to reproduce it
2. **Suggest features**: Open an issue describing the feature and its benefits
3. **Submit pull requests**: Fork the repository, make your changes, and submit a PR

### Development Guidelines

- Follow existing code style and conventions
- Write unit tests for new functionality
- Update documentation for significant changes
- Ensure all tests pass before submitting a PR
- Keep commits focused and write clear commit messages

## License

This project is licensed under the MIT License. See the project metadata for details.

## Package Information

- **Package Name**: Netorrix.Util.MessageService
- **Description**: A message service where flows are used for passing data with listeners and publishers using local storage on the flow
- **Tags**: Flow, Logger, Logging, Message, Error
- **Target Framework**: .NET 9.0
