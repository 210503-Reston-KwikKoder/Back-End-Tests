using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserTestsModels;

namespace UserTestsBL
{
    public interface ICategoryBL
    {
        /// <summary>
        /// Adds a user, returns added category, null if an error
        /// </summary>
        /// <param name="c">category to add</param>
        /// <returns>Category added, null if not found</returns>
        Task<Category> AddCategory(Category c);
        /// <summary>
        /// Adds a user, returns added category, null if an error
        /// </summary>
        /// <param name="c">category to add</param>
        /// <returns>Category added, null if not found</returns>

        Task<List<Category>> GetAllCategories();
      
        /// <summary>
        /// Getting category by it's id, not name
        /// </summary>
        /// <param name="id">id in database</param>
        /// <returns>category with a given id</returns>
        Task<Category> GetCategoryById(int id);
    }
}
