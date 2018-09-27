using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using TrainingNet.Models.DataBase;

namespace TrainingNet.Models.Views
{
    public class MovieGenreViewModel
    {
        public IEnumerable<MovieViewModel> Movies { get; set; }
        public SelectList Genres { get; set; }
        public string MovieGenre { get; set; }
    }
}
