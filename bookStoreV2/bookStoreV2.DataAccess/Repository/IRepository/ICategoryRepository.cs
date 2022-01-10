using System;
using bookStoreV2.Models;

namespace bookStoreV2.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category category);
    }
}
