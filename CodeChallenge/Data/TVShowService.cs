using System.Collections.Generic;
using System.Linq;

public class TVShowService
{
    private readonly ITVShowRepository _repository;

    public TVShowService(ITVShowRepository repository)
    {
        _repository = repository;
    }

    // Retrieve all TV shows from the repository
    public IEnumerable<TVShow> ListShows()
    {
        return _repository.GetAll();
    }

    // Retrieve favorited TV shows from the repository
    public IEnumerable<TVShow> ListFavorites()
    {
        return _repository.GetFavorites();
    }

    // Toggle the favorite status of a TV show in the repository
    public void ToggleFavorite(int id)
    {
        _repository.ToggleFavorite(id);
    }
}
