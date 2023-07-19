# CodeChallenge - TV Show Favorites Console Application
## David Ignacio Reyes Murcia
## July 2023

This project is a console application to manage TV Shows and mark them as favorites. It is implemented in C# with .NET 7.

## Folder and File Structure
CsharpCodeChallenge
|-- CodeChallenge
| |-- Program.cs
| |-- tvshows.db
| |-- Models
| | |-- TVShow.cs
| |-- Data
| | |-- ITVShowRepository.cs
| | |-- TVShowContext.cs
| | |-- TVShowRepository.cs
| | |-- TVShowService.cs
| |-- CodeChallenge.csproj
|-- CodeChallenge.Tests
| |-- TVShowContextTests.cs
| |-- CodeChallenge.Tests.csproj


## File Descriptions

- `Program.cs`: The main entry point for the application.
- `TVShowService.cs`: Contains business logic to handle TV shows.
- `TVShow.cs`: Defines the data model for TV shows.
- `TVShowRepository.cs` & `ITVShowRepository.cs`: Defines and implements data access methods.
- `TVShowContext.cs`: The Entity Framework Core database context class.
- `tvshows.db`: The SQLite database file with only schema.
- `tvshowsdata.db`: The SQLite database file with 50 registries.
- `TVShowContextTests.cs`: Contains unit tests for the TVShowContext.
- `.csproj` files: Project files that include necessary dependencies.

## Installation, Setup and Requirements

This application requires .NET 7.

To run this application:

1. Clone this repository to your local machine.
2. Navigate to the project directory in your terminal.
3. Run `dotnet restore` to install necessary packages.
4. Run `dotnet run` to start the application.

## Usage

To use this application:

1. Start the program.
2. Follow the on-screen prompts to list TV shows, mark favorites, and more.

## Testing

The project includes unit tests implemented with xUnit in the `CodeChallenge.Tests` project. The `TVShowContextTests.cs` file contains tests for the data operations of the application.

- `ListShows_ShouldReturnAllTVShows`: Tests if the service returns all TV shows.
- `ListFavorites_ShouldReturnFavoriteTVShows`: Tests if the service correctly returns only favorited TV shows.
- `ToggleFavorite_ExistingTVShow_ShouldToggleFavoriteStatus`: Tests if the service can correctly toggle the favorite status of an existing TV show.
- `ToggleFavorite_NonExistingTVShow_ShouldThrowArgumentException`: Tests if the service correctly throws an exception when trying to toggle the favorite status of a non-existing TV show.
- `ListShows_EmptyDatabase_ShouldReturnEmptyList`: Tests if the service returns an empty list when the database is empty.
- `ToggleFavorite_DuplicateTVShow_ShouldNotAddAsFavorite`: Tests if the service correctly toggles the favorite status back when the same TV show is marked as favorite twice.

To run the tests, navigate to the `CodeChallenge.Tests` directory and run `dotnet test`.

## Limitations

This application is a simple console application and does not support multi-user access or real-time updates.

## SOLID Principles

This project follows SOLID principles:

- **Single Responsibility Principle**: Each class has a single responsibility. For example, `TVShowService` handles the business logic, while `TVShowRepository` interacts with the database.
- **Open/Closed Principle**: New features can be added by extending existing code, not by modifying it. For example, additional repositories can be added by implementing `ITVShowRepository`.
- **Liskov Substitution Principle**: `TVShowRepository` can be substituted with any other class that implements `ITVShowRepository` without affecting the program.
- **Interface Segregation Principle**: `ITVShowRepository` is a simple, specific interface.
- **Dependency Inversion Principle**: High-level modules (`TVShowService`) depend on low-level modules (`TVShowRepository`) through abstractions (`ITVShowRepository`).
