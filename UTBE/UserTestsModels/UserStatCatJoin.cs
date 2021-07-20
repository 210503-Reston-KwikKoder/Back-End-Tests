using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserTestsModels
{
    public class UserStatCatJoin
    { 
        public UserStatCatJoin() { }
        public string UserId { get; set; }
        public User User { get; set; }
        public int UserStatId { get; set; }
        public UserStat UserStat { get; set; }
        public int CategoryId { get; set; }
        
    }
}
