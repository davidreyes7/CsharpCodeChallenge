using System;
using System.Collections.Generic;
using System.Linq;

public class TVShowRepository : ITVShowRepository
{
    private readonly TVShowContext _context;

    public TVShowRepository(TVShowContext context)
    {
        _context = context;
    }

    // Retrieve all TV shows from the database
    public IEnumerable<TVShow> GetAll()
    {
        return _context.TVShows.ToList(); 
    }

    // Retrieve favorited TV shows from the database
    public IEnumerable<TVShow> GetFavorites()
    {
        return _context.TVShows.Where(s => s.Favorite).ToList(); 
    }

    // Retrieve a TV show by ID from the database
    public TVShow GetById(int id)
    {
        return _context.TVShows.FirstOrDefault(s => s.Id == id); 
    }

    // Toggle the favorite status of the TV show
    public void ToggleFavorite(int id)
    {
        var show = _context.TVShows.FirstOrDefault(s => s.Id == id);

        if (show != null)
        {
            show.Favorite = !show.Favorite; 
            _context.SaveChanges();
        }
        else
        {
            throw new ArgumentException("TV show not found.", nameof(id));
        }
    }
}
