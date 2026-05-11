# VetClinicApp

VetClinicApp is a C#/.NET desktop application for managing basic veterinary clinic records. The project was created as part of coursework and includes an application project and a separate test project.

## Features

- Manage animal records
- Store animal details such as name, species/type, breed, age, and owner information
- Use a structured business layer for application logic
- Separate application code from test code
- Includes unit tests for key functionality

## Project Structure

```text
VetClinicApp/
├── App/                 # Main application project
│   ├── BusinessLayer/   # Models, interfaces, and business logic
│   ├── ViewModels/      # View model classes
│   ├── Views/           # UI views
│   ├── Images/          # Application image assets
│   └── App.csproj
│
├── Tests/               # Unit test project
│   ├── AnimalTests.cs
│   ├── CustomerTests.cs
│   ├── ClinicTests.csproj
│   └── Tests.csproj
│
├── VetClinic.sln        # Main Visual Studio solution
├── Assessment2.sln      # Assessment solution file
├── .gitignore
└── README.md