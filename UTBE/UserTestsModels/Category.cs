using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace UserTestsModels
{
    public class Category
    {
        public Category() { }
        public int Id { get; set; }
        [Required]
        public int Name { get; set; }
    }
}