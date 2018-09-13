using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrainingNet.Models.DataBase;

namespace TrainingNet.Models.Views
{
    public class MovieViewModel
    {
        public MovieViewModel(){ }
        
        public MovieViewModel(Movie movie)
        {
            Id = movie.Id;
            Price = movie.Price;
            ReleaseDate = movie.ReleaseDate;
            Genre = movie.Genre;
            Title = movie.Title;
            Rating = movie.Rating;
        }
        
        public int Id { get; set; }
  
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Title { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]    
        public DateTime ReleaseDate { get; set; }

        [Required]
        [StringLength(30)]
        public string Genre { get; set; }

        [Range(1, 100)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Range(0,5)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Rating { get; set; }
    }
}
