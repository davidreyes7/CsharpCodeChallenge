using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CodeChallenge.Tests
{
    public class TVShowContextTests
    {
        private DbContextOptions<TVShowContext> GetInMemoryDatabaseOptions(string databaseName)
        {
            return new DbContextOptionsBuilder<TVShowContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;
        }

        [Fact]
        public void ListShows_ShouldReturnAllTVShows()
        {
            // Arrange
            var options = GetInMemoryDatabaseOptions("ListShows_ShouldReturnAllTVShows");

            using (var context = new TVShowContext(options))
            {
                var tvShows = new List<TVShow>
                {
                    new TVShow { Id = 1, Title = "Show 1", Favorite = false },
                    new TVShow { Id = 2, Title = "Show 2", Favorite = true },
                    new TVShow { Id = 3, Title = "Show 3", Favorite = false }
                };

                context.TVShows.AddRange(tvShows);
                context.SaveChanges();
            }

            // Act
            IEnumerable<TVShow> result;
            using (var context = new TVShowContext(options))
            {
                var repository = new TVShowRepository(context);
                var service = new TVShowService(repository);

                result = service.ListShows();
            }

            // Assert
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void ListFavorites_ShouldReturnFavoriteTVShows()
        {
            // Arrange
            var options = GetInMemoryDatabaseOptions("ListFavorites_ShouldReturnFavoriteTVShows");

            using (var context = new TVShowContext(options))
            {
                var tvShows = new List<TVShow>
                {
                    new TVShow { Id = 1, Title = "Show 1", Favorite = true },
                    new TVShow { Id = 2, Title = "Show 2", Favorite = true },
                    new TVShow { Id = 3, Title = "Show 3", Favorite = false }
                };

                context.TVShows.AddRange(tvShows);
                context.SaveChanges();
            }

            // Act
            IEnumerable<TVShow> result;
            using (var context = new TVShowContext(options))
            {
                var repository = new TVShowRepository(context);
                var service = new TVShowService(repository);

                result = service.ListFavorites();
            }

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void ToggleFavorite_ExistingTVShow_ShouldToggleFavoriteStatus()
        {
            // Arrange
            var options = GetInMemoryDatabaseOptions("ToggleFavorite_ExistingTVShow_ShouldToggleFavoriteStatus");

            using (var context = new TVShowContext(options))
            {
                var tvShow = new TVShow { Id = 1, Title = "Show 1", Favorite = false };
                context.TVShows.Add(tvShow);
                context.SaveChanges();
            }

            // Act
            using (var context = new TVShowContext(options))
            {
                var repository = new TVShowRepository(context);
                var service = new TVShowService(repository);

                service.ToggleFavorite(1);
            }

            // Assert
            using (var context = new TVShowContext(options))
            {
                var updatedTVShow = context.TVShows.Find(1);
                Assert.True(updatedTVShow.Favorite);
            }
        }

        [Fact]
        public void ToggleFavorite_NonExistingTVShow_ShouldThrowArgumentException()
        {
            // Arrange
            var options = GetInMemoryDatabaseOptions("ToggleFavorite_NonExistingTVShow_ShouldThrowArgumentException");

            // Act and Assert
            using (var context = new TVShowContext(options))
            {
                var repository = new TVShowRepository(context);
                var service = new TVShowService(repository);

                Assert.Throws<ArgumentException>(() => service.ToggleFavorite(10));
            }
        }

        [Fact]
        public void ListShows_EmptyDatabase_ShouldReturnEmptyList()
        {
            // Arrange
            var options = GetInMemoryDatabaseOptions("ListShows_EmptyDatabase_ShouldReturnEmptyList");

            // Act
            IEnumerable<TVShow> result;
            using (var context = new TVShowContext(options))
            {
                var repository = new TVShowRepository(context);
                var service = new TVShowService(repository);
                result = service.ListShows();
            }

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void ToggleFavorite_DuplicateTVShow_ShouldNotAddAsFavorite()
        {
            // Arrange
            var options = GetInMemoryDatabaseOptions("ToggleFavorite_DuplicateTVShow_ShouldNotAddAsFavorite");

            using (var context = new TVShowContext(options))
            {
                var tvShow = new TVShow { Id = 1, Title = "Show 1", Favorite = false };
                context.TVShows.Add(tvShow);
                context.SaveChanges();
            }

            // Act
            using (var context = new TVShowContext(options))
            {
                var repository = new TVShowRepository(context);
                var service = new TVShowService(repository);
                service.ToggleFavorite(1);
                service.ToggleFavorite(1); // Toggling again should change the status back
            }

            // Assert
            using (var context = new TVShowContext(options))
            {
                var updatedTVShow = context.TVShows.Find(1);
                Assert.False(updatedTVShow.Favorite); // Updated assertion
            }
        }

    }
}
