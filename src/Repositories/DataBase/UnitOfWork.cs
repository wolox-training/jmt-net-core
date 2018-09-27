using Microsoft.EntityFrameworkCore;
using TrainingNet.Models.DataBase;
using TrainingNet.Repositories.Interfaces;

namespace TrainingNet.Repositories.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataBaseContext _context;

        public UnitOfWork(DataBaseContext context)
        {
            _context = context;
            MovieRepository = new MovieRepository(_context);
        }

        public IMovieRepository MovieRepository { get; }
        public IUserRepository UserRepository { get; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
