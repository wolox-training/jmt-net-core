using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using TrainingNet.Models.DataBase;
using TrainingNet.Paging;

namespace TrainingNet.Models.Views
{
    public class MovieGenreViewModel
    {
        public PaginatedList<MovieViewModel> Movies { get; set; }
        public SelectList Genres { get; set; }
        public string MovieGenre { get; set; }
    }
}
