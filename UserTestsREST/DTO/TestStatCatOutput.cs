using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserTestsREST.DTO
{
    public class TestStatCatOutput:TestStatOutput
    {
        public TestStatCatOutput() { }
        public int Category { get; set; }
    }
}
