using System;
using TrainingNet.Models.Views;

namespace TrainingNet.Models.DataBase
{
    public class Movie
    {
<<<<<<< HEAD
        public void Update(MovieViewModel movieViewModel)
        {
=======
        public void Update(MovieViewModel movieViewModel){
>>>>>>> d8b5ed90a8df37bb6ddda45cf66204ea1f79a223
            Title = movieViewModel.Title;
            ReleaseDate = movieViewModel.ReleaseDate;
            Genre = movieViewModel.Genre;
            Price = movieViewModel.Price;
        }
<<<<<<< HEAD

=======
>>>>>>> d8b5ed90a8df37bb6ddda45cf66204ea1f79a223
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
    }
}
