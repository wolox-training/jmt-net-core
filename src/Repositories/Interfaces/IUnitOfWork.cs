using System;
using TrainingNet.Models.DataBase;
using TrainingNet.Repositories.Interfaces;

namespace TrainingNet.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Movie> MovieRepository { get; }
        int Complete();
    }
}
