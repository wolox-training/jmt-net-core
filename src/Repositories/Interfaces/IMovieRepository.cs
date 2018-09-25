using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TrainingNet.Models.DataBase;

namespace TrainingNet.Repositories.Interfaces
{
    public interface IMovieRepository: IRepository<Movie>
    {
        IQueryable<String> GetGenres();
    }
}