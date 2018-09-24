using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TrainingNet.Models.DataBase;
using TrainingNet.Repositories;
using TrainingNet.Repositories.Database;
using TrainingNet.Repositories.Interfaces;

namespace TrainingNet.Repositories
{

    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(DataBaseContext context) : base(context){ }

        public Movie GetMovieComments(int id)
        {
            return Context.Movies.Where(m => m.Id == id).Include(m => m.Comments).FirstOrDefault();
        }

        public IQueryable<String> GetGenres()
        {
            return Context.Movies.Select(s => s.Genre).Distinct();
        }
    }
}
