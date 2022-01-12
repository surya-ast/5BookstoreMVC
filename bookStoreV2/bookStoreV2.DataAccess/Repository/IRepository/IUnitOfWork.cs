using System;
namespace bookStoreV2.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {

        ICategoryRepository Category { get; }

        ISP_Call SP_Call { get; }
    }
}
