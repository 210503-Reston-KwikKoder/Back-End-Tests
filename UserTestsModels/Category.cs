using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserTestsModels
{
    public class Category
    {
        public Category() { }
        public int Id { get; set; }
        [required]
        public int Name { get; set; }
    }
}