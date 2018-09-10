using System;
using TrainingNet.Models.DataBase;

namespace TrainingNet.Models.Views
{
    public class MovieViewModel
    {
        public MovieViewModel(){
            
        }
        public MovieViewModel(Movie movie){
            ID = movie.ID;
            Price = movie.Price;
            ReleaseDate = movie.ReleaseDate;
            Genre = movie.Genre;
            Title = movie.Title;
        }
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
    }
}
