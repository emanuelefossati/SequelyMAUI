# Sequely

Sequely is a cross-platform application built using .NET MAUI (Multi-platform App UI) that allows users to manage MySQL databases. The application provides a user-friendly interface to connect to MySQL servers, browse databases, run queries, and manage database tables.

## Features

- **Cross-Platform**: Runs on Android, iOS, MacCatalyst, Tizen, and Windows (Tested on Windows 10).
- **Database Management**: Connect to MySQL servers, browse databases, and manage tables.
- **Query Execution**: Run SQL queries and view results.
- **Table Management**: Create, edit, and delete tables and rows.
- **User-Friendly Interface**: Built with MudBlazor for a modern and responsive UI.

## Getting Started

### Prerequisites

- .NET 8.0 SDK or later
- Visual Studio 2022 or later with MAUI workload installed
- MySQL server

### Installation

1. Clone the repository:

   ```sh
   git clone https://github.com/emanuelefossati/SequelyMAUI.git
   cd SequelyMAUI
   ```

2. Open the solution file [SequelyMAUI.sln] in Visual Studio.

3. Restore the NuGet packages:

   ```sh
   dotnet restore
   ```

4. Build and run the project on your preferred platform.

### Usage

1. Launch the application.
2. Navigate to the "Connections" page to add a new MySQL connection.
3. Enter the connection details and test the connection.
4. Once connected, navigate to the "MySQL Dashboard" to browse databases and manage tables.
5. Use the query tab to run SQL queries and view results.

## Project Structure

- **SequelyMAUI**: Main project directory containing the source code.
  - **Components**: Contains reusable UI components.
  - **Entities**: Contains entity classes representing database objects.
  - **Interfaces**: Contains service interfaces.
  - **Services**: Contains service implementations for database operations.
  - **Platforms**: Platform-specific code for Android, iOS, MacCatalyst, Tizen, and Windows.
  - **Resources**: Contains resources such as images, fonts, and styles.
  - **wwwroot**: Contains static web assets for Blazor WebView.

## Acknowledgements

- [MudBlazor](https://mudblazor.com/) - Blazor Component Library
- [MySqlConnector](https://mysqlconnector.net/) - MySQL ADO.NET Data Provider
- [Dapper](https://dapperlib.github.io/Dapper/) - Simple Object Mapper for .NET
