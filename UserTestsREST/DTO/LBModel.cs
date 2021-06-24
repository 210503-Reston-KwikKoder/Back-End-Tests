using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserTestsREST.DTO
{
    public class LBModel
    {
       
        public LBModel() { }
        public string AuthId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public double AverageWPM { get; set; }
        public double AverageAcc { get; set; }
        public int CatID { get; set; }
        
    }
}
