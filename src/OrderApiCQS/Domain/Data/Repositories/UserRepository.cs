using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApiCQS.Domain.Data.Repositories
{
    public interface IUserRepository
    {
        string GetUserNameFake(int userId);
    }

    public class UserRepository : IUserRepository
    {
        public string GetUserNameFake(int userId)
        {
            return "Onur SALKAYA";
        }
    }
}
