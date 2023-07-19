using System.Collections.Generic;

public interface ITVShowRepository
{
    // Retrieve all TV shows
    IEnumerable<TVShow> GetAll();

    // Retrieve favorited TV shows
    IEnumerable<TVShow> GetFavorites();

    // Retrieve a TV show by ID
    TVShow GetById(int id);

    // Toggle the favorite status of a TV show
    void ToggleFavorite(int id); 
}
