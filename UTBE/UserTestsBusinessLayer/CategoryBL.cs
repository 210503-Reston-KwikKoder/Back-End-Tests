
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserTestsDL;
using UserTestsModels;


namespace UserTestsBL
{
    public class CategoryBL : ICategoryBL
    {
        private readonly Repo _repo;
        public CategoryBL(UserTestDBContext context)
        {
            _repo = new Repo(context);
        }
        public async Task<Category> AddCategory(Category c)
        {
            return await _repo.AddCategory(c);
        }
        public async Task<List<Category>> GetAllCategories()
        {
            return await _repo.GetAllCategories();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await _repo.GetCategoryById(id);
        }
    }
}
