using Domasna.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domasna.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<DomasnaApplicationUser> GetAll();
        DomasnaApplicationUser Get(string id);
        void Insert(DomasnaApplicationUser entity);
        void Update(DomasnaApplicationUser entity);
        void Delete(DomasnaApplicationUser entity);
    }
}
