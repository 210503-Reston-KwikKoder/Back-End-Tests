using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserTestsREST.DTO
{
    public class StatModel
    {
        public StatModel() { }
        public StatModel (string userid, double averageWPM, double averageAcc, int numTests, int totTTime, int categoryName){
            this.averageaccuracy = averageAcc;
            this.averagewpm = averageWPM;
            this.numberoftests = numTests;
            this.totaltesttime = totTTime;
            this.category = categoryName;
            this.userID = userid;
        }
        public string userID { get; set; }
        public double averagewpm { get; set; }
        public double averageaccuracy { get; set; }
        public int numberoftests { get; set; }
        public int totaltesttime { get; set; }
        public int category { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public double WLRatio { get; set; }
        public int WinStreak { get; set; }
    }
}
