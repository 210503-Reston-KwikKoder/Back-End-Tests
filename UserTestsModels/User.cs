using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserTestsModels
{
    public class User
    {
        public User() { }
        public int  Id { get; set; }
        [required]
        public string Auth0Id { get; set; }
        public int Revapoints { get; set; }
    }
}