using TrainingNet.Models.DataBase;
using TrainingNet.Repositories.Database;
using TrainingNet.Repositories.Interfaces;

namespace TrainingNet.Repositories
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        public UserRepository(DataBaseContext context) : base(context) { }
    }
}
