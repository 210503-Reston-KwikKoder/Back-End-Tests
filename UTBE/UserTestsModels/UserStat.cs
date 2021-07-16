using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace UserTestsModels
{
    public class UserStat
    {
        private int _id;
        private int _numberOfTests;
        private int _totalTestTime;
        public UserStat() { }

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
        
        public double AverageWPM{ get; set; }
        public double AverageAccuracy { get; set; }

        /// <summary>
        /// Set Validation: NumberOfTests should not be negative
        /// </summary>
        /// <value></value>
        public int NumberOfTests {
            get { return _numberOfTests; }
            set
            {
                if (value < 0) throw new ValidationException("NumberOfTests should not be negative");
                _numberOfTests = value;
            }
         }

        /// <summary>
        /// Set Validation: TotalTestTime should not be negative
        /// </summary>
        /// <value></value>
        public int TotalTestTime {
            get { return _totalTestTime; }
            set
            {
                if (value < 0) throw new ValidationException("TotalTestTime should not be negative");
                _totalTestTime = value;
            }
        }

        public int Wins { get; set; }

        public int Losses { get; set; }
        
        public double WLRatio { get; set; }

        public int WinStreak { get; set; }
        
        public UserStatCatJoin UserStatCatJoin { get; set; }
        public List<TypeTest> TypeTests { get; set; }
    }
}
