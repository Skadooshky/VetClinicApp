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
```

## Technologies Used

- C#
- .NET 7
- WPF
- Visual Studio
- Unit testing framework

## How to Run

1. Clone the repository:

```bash
git clone https://github.com/Skadooshky/VetClinicApp.git
```

2. Open the project folder.

3. Open `VetClinic.sln` in Visual Studio.

4. Restore NuGet packages if required.

5. Build the solution:

```bash
dotnet build
```

6. Run the `App` project from Visual Studio.

## Running Tests

To run the tests, open the solution in Visual Studio and use:

```text
Test > Run All Tests
```

Or run from the terminal:

```bash
dotnet test
```

## Notes

This project is intended as an assessment/coursework project. It focuses on applying object-oriented programming, layered design, and basic testing practices in a C# desktop application.

## Author

Shivarn Davidson
