using System;
using System.Linq;
using bookStoreV2.DataAccess.Data;
using bookStoreV2.DataAccess.Repository.IRepository;
using bookStoreV2.Models;

namespace bookStoreV2.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Category category)
        {
            var objFromDb = _db.Categories.FirstOrDefault(s => s.Id == category.Id);
            if(objFromDb != null)
            { 
                objFromDb.Name = category.Name;
                _db.SaveChanges();
            }
        }
    }
}
