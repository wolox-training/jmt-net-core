using System;
using System.ComponentModel.DataAnnotations;
using TrainingNet.Models.Views;

namespace TrainingNet.Models.DataBase
{
    public class Movie
    {

        public Movie() { }
        public Movie(MovieViewModel movieViewModel)
        {
            Id = movieViewModel.Id;
            Title = movieViewModel.Title;
            ReleaseDate = movieViewModel.ReleaseDate;
            Genre = movieViewModel.Genre;
            Price = movieViewModel.Price;
            Rating = movieViewModel.Rating;
        }

        public void Update(MovieViewModel movieViewModel)
        {
            Title = movieViewModel.Title;
            ReleaseDate = movieViewModel.ReleaseDate;
            Genre = movieViewModel.Genre;
            Price = movieViewModel.Price;
            Rating = movieViewModel.Rating;
        }
        
        public override string ToString(){
            return    "Title: " + Title + '\n'
                    + "Price: " + Price.ToString() + '\n'
                    + "Release Date: " + ReleaseDate.ToString() + '\n'
                    + "Genre: " + Genre + '\n'
                    + "Rating: " + Rating.ToString() + '\n';
        }

        public int Id { get; set; }
        public string Title { get; set; }
        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
        public decimal Rating { get; set; }
    }
}
