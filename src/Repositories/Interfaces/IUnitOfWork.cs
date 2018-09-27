using System;
using TrainingNet.Models.DataBase;
using TrainingNet.Repositories.Interfaces;

namespace TrainingNet.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IMovieRepository MovieRepository { get; }
        IUserRepository UserRepository { get; }
        int Complete();
    }
}
