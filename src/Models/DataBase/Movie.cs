using System;
using System.ComponentModel.DataAnnotations;
using TrainingNet.Models.Views;

namespace TrainingNet.Models.DataBase
{
    public class Movie
    {
        
        public Movie(MovieViewModel movieViewModel){
            Id = movieViewModel.Id;
            Title = movieViewModel.Title;
            ReleaseDate = movieViewModel.ReleaseDate;
            Genre = movieViewModel.Genre;
            Price = movieViewModel.Price;
        }
        
        public void Update(MovieViewModel movieViewModel)
        {
            Title = movieViewModel.Title;
            ReleaseDate = movieViewModel.ReleaseDate;
            Genre = movieViewModel.Genre;
            Price = movieViewModel.Price;
        }
        
        public int Id { get; set; }
        public string Title { get; set; }
        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
    }
}
