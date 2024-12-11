# ISIN Finder

A WPF application for finding and displaying trade information based on ISIN, ACID, or Trade ID.

## Features

- Search by ISIN, ACID, or Trade ID
- Display related trade information in a grid view
- View console output logs of all operations
- Support for multiple input values (comma or newline separated)
- Position Ladder information for each trade

## Prerequisites

- .NET 8.0 SDK or later
- Visual Studio 2022 (or Visual Studio 2019 version 16.9 or later)
- Windows OS (required for WPF)

## Getting Started

1. Clone the repository:
   ```
   git clone https://github.com/syriojohn/ISIN_finder.git
   ```

2. Open the solution in Visual Studio:
   - Navigate to the cloned directory
   - Open `ISIN_finder.sln`
   - Or use the command line: `start ISIN_finder.sln`

3. Build and Run:
   - Press F5 in Visual Studio to build and run
   - Or use the command line:
     ```
     dotnet build
     dotnet run
     ```

## Usage

1. Select the search type (ISIN, ACID, or Trade ID) from the dropdown
2. Enter one or more search values (separated by commas or newlines)
3. Click "Search" to find related trade information
4. View results in the grid
5. Switch to "Console Output" tab to view detailed operation logs
6. Use "Clear" to reset the form

## Dependencies

This project uses only standard .NET libraries:
- .NET 8.0
- WPF (Windows Presentation Foundation)

No additional NuGet packages or external dependencies are required.

## License

This project is licensed under the MIT License - see the LICENSE file for details.
