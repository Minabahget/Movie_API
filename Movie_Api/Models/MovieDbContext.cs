using Microsoft.EntityFrameworkCore;

namespace Movie_Api.Models
{
    public class MovieDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<MovieCategory> MovieCategories { get; set; }

        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
            .HasIndex(e => e.Name)
            .IsUnique().HasAnnotation("SqlServer:ErrorMessage", "Please insert a unique name."); 

            modelBuilder.Entity<Movie>()
            .HasMany(e => e.Categories)
             .WithMany(e => e.Movies)
                .UsingEntity<MovieCategory>(
          l => l.HasOne<Category>(e=>e.Category).WithMany(e => e.MovieCategories).HasForeignKey(e => e.CategoryId),
          r => r.HasOne<Movie>(e=>e.Movie).WithMany(e => e.MovieCategories).HasForeignKey(e => e.MovieId));
            modelBuilder.Entity<MovieCategory>().HasKey(e => new {e.MovieId,e.CategoryId});
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", Description = "Movies filled with action-packed scenes." },
                new Category { Id = 2, Name = "Comedy", Description = "Movies intended to make the audience laugh." },
                new Category { Id = 3, Name = "Drama", Description = "Movies that focus on emotional themes." },
                new Category { Id = 4, Name = "Adventure", Description = "Movies filled with adventurous journeys and exploration." },
                new Category { Id = 5, Name = "Historical", Description = "Movies based on historical events or settings." },
                new Category { Id = 6, Name = "Animation", Description = "Movies created using animation techniques." },
                new Category { Id = 7, Name = "Science Fiction", Description = "Movies that explore speculative or futuristic concepts." }       

    );

            // Seed Movies without specifying Categories
            modelBuilder.Entity<Movie>().HasData(
                new Movie
                {
                    Id = 1,
                    Title = "The last stand",
                    Description = "A vigilante known as Batman sets out to combat the injustices of Gotham City.",
                    CreatedDate = new DateOnly(2008, 7, 18),
                    Duration = new TimeOnly(2, 32, 0),
                    Rate = 9.0f,
                    ImagePath = "uploads\\movie1.jpg",
                },
                new Movie
                {
                    Id = 2,
                    Title = "Spider man 2",
                    Description = "A thief who enters the dreams of others to steal secrets from their subconscious.",
                    CreatedDate = new DateOnly(2010, 7, 16),
                    Duration = new TimeOnly(2, 28, 0),
                    Rate = 8.8f,
                    ImagePath = "uploads\\movie2.jpg",
                },
                new Movie
                {
                    Id = 3,
                    Title = "Spider man 3",
                    Description = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.",
                    CreatedDate = new DateOnly(1994, 10, 14),
                    Duration = new TimeOnly(2, 22, 0),
                    Rate = 9.3f,
                    ImagePath = "uploads\\movie3.jpg",
                },
                new Movie
                {
                    Id = 4,
                    Title = "valkyrie",
                    Description = "A group of German officers plot to assassinate Adolf Hitler during World War II.",
                    CreatedDate = new DateOnly(2008, 12, 25),
                    Duration = new TimeOnly(2, 1, 0),
                    Rate = 8.1f,
                    ImagePath = "uploads\\movie4.jpg",
                },
                new Movie
                {
                    Id = 5,
                    Title = "Gladiator",
                    Description = "A former Roman General sets out to exact vengeance against the corrupt emperor who murdered his family and sent him into slavery.",
                    CreatedDate = new DateOnly(2000, 5, 5),
                    Duration = new TimeOnly(2, 35, 0),
                    Rate = 8.5f,
                    ImagePath = "uploads\\movie5.jpg",
                },
                new Movie
                {
                    Id = 6,
                    Title = "Ice age",
                    Description = "Set during the Ice Age, a sabertooth tiger, a sloth, and a wooly mammoth find a lost human infant, and they try to return him to his tribe.",
                    CreatedDate = new DateOnly(2002, 3, 15),
                    Duration = new TimeOnly(1, 21, 0),
                    Rate = 7.5f,
                    ImagePath = "uploads\\movie6.jpg",
                },
                new Movie
                {
                    Id = 7,
                    Title = "Transformers",
                    Description = "An ancient struggle between two Cybertronian races, the heroic Autobots and the evil Decepticons, comes to Earth, with a clue to the ultimate power held by a teenager.",
                    CreatedDate = new DateOnly(2007, 7, 3),
                    Duration = new TimeOnly(2, 24, 0),
                    Rate = 7.1f,
                    ImagePath = "uploads\\movie7.jpg",
                },
                new Movie
                {
                    Id = 8,
                    Title = "X-men",
                    Description = "Two mutants come to a private academy for their kind whose resident superhero team must oppose a terrorist organization with similar powers.",
                    CreatedDate = new DateOnly(2000, 7, 14),
                    Duration = new TimeOnly(1, 44, 0),
                    Rate = 7.4f,
                    ImagePath = "uploads\\movie8.jpg",
                }
            );


            // Define relationships and seed data for the join table MovieCategory
            modelBuilder.Entity<MovieCategory>().HasData(
                // Relationships for "The last stand" movie
                new MovieCategory { MovieId = 1, CategoryId = 1 }, // Action
                new MovieCategory { MovieId = 1, CategoryId = 2 }, // Comedy

                // Relationships for "Spider man 2" movie
                new MovieCategory { MovieId = 2, CategoryId = 2 }, // Comedy

                // Relationships for "Spider man 3" movie
                new MovieCategory { MovieId = 3, CategoryId = 3 }, // Drama

                // Relationships for "valkyrie" movie
                new MovieCategory { MovieId = 4, CategoryId = 1 }, // Action
                new MovieCategory { MovieId = 4, CategoryId = 5 }, // Historical

                // Relationships for "Gladiator" movie
                new MovieCategory { MovieId = 5, CategoryId = 1 }, // Action
                new MovieCategory { MovieId = 5, CategoryId = 5 }, // Historical

                // Relationships for "Ice age" movie
                new MovieCategory { MovieId = 6, CategoryId = 6 }, // Animation
                new MovieCategory { MovieId = 6, CategoryId = 7 }, // Science Fiction

                // Relationships for "Transformers" movie
                new MovieCategory { MovieId = 7, CategoryId = 7 }, // Science Fiction

                // Relationships for "X-men" movie
                new MovieCategory { MovieId = 8, CategoryId = 1 }, // Action
                new MovieCategory { MovieId = 8, CategoryId = 7 }  // Science Fiction
            );

        }
    }
}
