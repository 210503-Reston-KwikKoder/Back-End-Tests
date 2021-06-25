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
        private int _id;
        public User() { }

        /// <summary>
        /// Set Validation: Id should not less or equal to 0
        /// </summary>
        /// <value></value>
        public int  Id
        { 
            get { return _id; }
            set 
            {
                if (value <= 0) throw new ValidationException("Id should not less or equal to 0");
                _id = value;
            }
        }

        [Required]
        public string Auth0Id { get; set; }
        public int Revapoints { get; set; }
    }
}