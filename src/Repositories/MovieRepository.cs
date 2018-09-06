using System.Collections;
using Microsoft.EntityFrameworkCore;
using TrainingNet.Models.DataBase;
using TrainingNet.Repositories;
using TrainingNet.Repositories.Interfaces;

namespace TrainingNet.Repositories
{

    public class MovieRepository : Repository<Movie>
    {
        public MovieRepository(DbContext context) : base(context)
        {
        }
    }
}