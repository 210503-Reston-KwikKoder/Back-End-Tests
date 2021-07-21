using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace UserTestsModels
{
    public class User
    {
        public User() { }
       
        public string Auth0Id { get; set; }
        public int Revapoints { get; set; }
    }
}