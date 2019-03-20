using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCMovie.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MVCMovieContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MVCMovieContext>>()))
            {
                // Look for any movies.
                if (context.Movie.Any())
                {
                    return;   // DB has been seeded
                }

                context.Movie.AddRange(
                    new Movie
                    {
                        Title = "Avengers: Infinity War",
                        ReleaseDate = DateTime.Parse("2018-4-27"),
                        Genre = "Action",
                        Price = 7.99M,
                        Rating = "PG-13"
                    },

                    new Movie
                    {
                        Title = "Avatar",
                        ReleaseDate = DateTime.Parse("2009-12-21"),
                        Genre = "Science Fiction",
                        Price = 8.99M,
                        Rating = "PG-13"
                    },

                    new Movie
                    {
                        Title = "Superbad",
                        ReleaseDate = DateTime.Parse("2007-8-16"),
                        Genre = "Comedy",
                        Price = 9.99M,
                        Rating = "R"
                    },

                    new Movie
                    {
                        Title = "X-Men",
                        ReleaseDate = DateTime.Parse("2000-6-9"),
                        Genre = "Action",
                        Price = 3.99M,
                        Rating = "PG-13"
                    }
                );
                context.SaveChanges();
            }
        }

    }
}
