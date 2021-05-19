using SmokersTavern.Data;
using SmokersTavern.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//Zain
namespace SmokersTavern.Service
{
    public class CategoryRepository : ICategoryRepository
    {
        private ApplicationDbContext _datacontext = null;
        private readonly IRepositoryService<Category> _categoryRepository;

        public CategoryRepository()
        {
            _datacontext = new ApplicationDbContext();
            _categoryRepository = new RepositoryService<Category>(_datacontext);
        }

        public void Dispose()
        {
            _datacontext.Dispose();
            _datacontext = null;
        }

        public Category GetById(int id)
        {
            return _categoryRepository.GetById(id);
        }

        public List<Category> GetAll()
        {
            return _categoryRepository.GetAll().ToList();
        }

        public void Insert(Category model)
        {
            _categoryRepository.Insert(model);
        }

        public void Update(Category model)
        {
            _categoryRepository.Update(model);
        }

        public void Delete(Category model)
        {
            _categoryRepository.Delete(model);
        }
        public IEnumerable<Category> Find(Func<Category, bool> predicate)
        {
            return _categoryRepository.Find(predicate).ToList();
        }
    }
}
