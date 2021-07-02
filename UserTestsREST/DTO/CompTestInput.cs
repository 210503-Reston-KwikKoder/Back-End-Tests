using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserTestsREST.DTO
{
    public class CompTestInput:TypeTestInput
    {
        public CompTestInput() { }
        public bool won { get; set; }
        public int winStreak { get; set; }
        public string auth0Id { get; set; }
    }
}
