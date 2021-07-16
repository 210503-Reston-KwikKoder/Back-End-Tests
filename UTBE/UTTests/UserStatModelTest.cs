using System;
using Xunit;
using UserTestsModels;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace UTTests
{
    public class UserStatModelTest
    {
        /// <summary>
        /// Tests for Constructor
        /// </summary>
        [Fact]
        public void VerifyEmptyConstructor() 
        {
            UserStat test = new UserStat();
            Assert.NotNull(test);
        }

        /// <summary>
        /// Tests for Id
        /// </summary>
        [Fact]
        public void VerifyIdIsInt() 
        {
            UserStat test = new UserStat();
            test.Id = 1;
            Type expected = typeof(int);
            Assert.Equal(test.Id.GetType(), expected);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void VerifyIdValidationShouldThrowException(int input)
        {
            UserStat test = new UserStat();
            Assert.Throws<ValidationException>(() => test.Id = input);
        }

        [Fact]
        public void VerifyId() 
        {
            UserStat test = new UserStat();
            int expected = 1;
            test.Id = expected;
            Assert.Equal(expected, test.Id);
        }


        /// <summary>
        /// Tests for NumberOfTests
        /// </summary>
        [Fact]
        public void VerifyNumberOfTests() 
        {
            UserStat test = new UserStat();
            var expected = 1;
            test.NumberOfTests = expected;
            Assert.Equal(expected, test.NumberOfTests);
        }

        [Fact]
        public void VerifyNumberOfTestsIsInt() 
        {
            UserStat test = new UserStat();
            test.NumberOfTests = 1;
            Type expected = typeof(int);
            Assert.Equal(test.NumberOfTests.GetType(), expected);
        }

        [Theory]
        [InlineData(-1)]
        public void VerifyNumberOfTestsValidationShouldThrowException(int input)
        {
            UserStat test = new UserStat();
            Assert.Throws<ValidationException>(() => test.NumberOfTests = input);
        }

        /// <summary>
        /// Tests for AverageWPM
        /// </summary>
        [Fact]
        public void VerifyAverageWPMIsDouble() 
        {
            UserStat test = new UserStat();
            test.AverageWPM = 1.0;
            Type expected = typeof(double);
            Assert.Equal(test.AverageWPM.GetType(), expected);
        }

        [Fact]
        public void VerifyAverageWPM() 
        {
            UserStat test = new UserStat();
            var expected = 1.0;
            test.AverageWPM = expected;
            Assert.Equal(expected, test.AverageWPM);
        }

        /// <summary>
        /// Tests for AverageAccuracy
        /// </summary>
        [Fact]
        public void VerifyAverageAccuracyIsDouble() 
        {
            UserStat test = new UserStat();
            test.AverageAccuracy = 1.0;
            Type expected = typeof(double);
            Assert.Equal(test.AverageAccuracy.GetType(), expected);
        }

        [Fact]
        public void VerifyAverageAccuracy() 
        {
            UserStat test = new UserStat();
            var expected = 1.0;
            test.AverageAccuracy = expected;
            Assert.Equal(expected, test.AverageAccuracy);
        }

        
        /// <summary>
        /// Tests for TotalTestTime
        /// </summary>
        [Fact]
        public void VerifyTotalTestTime() 
        {
            UserStat test = new UserStat();
            var expected = 1;
            test.TotalTestTime = expected;
            Assert.Equal(expected, test.TotalTestTime);
        }

        [Fact]
        public void VerifyTotalTestTimeIsInt() 
        {
            UserStat test = new UserStat();
            test.TotalTestTime = 1;
            Type expected = typeof(int);
            Assert.Equal(test.TotalTestTime.GetType(), expected);
        }
        
        [Theory]
        [InlineData(-1)]
        public void VerifyTotalTestTimeValidationShouldThrowException(int input)
        {
            UserStat test = new UserStat();
            Assert.Throws<ValidationException>(() => test.TotalTestTime = input);
        }

        /// <summary>
        /// Tests for UserStatCatJoin
        /// </summary>
        [Fact]
        public void VerifyUserStatCatJoinIsRightType() 
        {
            UserStat test = new UserStat();
            test.UserStatCatJoin = new UserStatCatJoin();
            Type expected = typeof(UserStatCatJoin);
            Assert.Equal(test.UserStatCatJoin.GetType(), expected);
        }

        [Fact]
        public void VerifyUserStatCatJoin() 
        {
            var expected = 1;
            
            UserStat test = new UserStat()
            {
                Id = expected
            };

            test.UserStatCatJoin = new UserStatCatJoin()
            {
                UserStatId = test.Id
            };           

            Assert.Equal(expected, test.UserStatCatJoin.UserStatId);
        }

        /// <summary>
        /// Tests for TypeTests
        /// </summary>
        [Fact]
        public void VerifyTypeTestsHasRightType() 
        {
            UserStat test = new UserStat();
            test.TypeTests = new List<TypeTest>();
            test.TypeTests.Add(new TypeTest(){Id = 1});
            Type expected = typeof(TypeTest);
            Assert.Equal(test.TypeTests[0].GetType(), expected);
        }

        [Fact]
        public void VerifyTypeTests()
        {
            var expected = 1;
            UserStat test = new UserStat()
            {
                Id = expected
            };
            test.TypeTests = new List<TypeTest>();
            test.TypeTests.Add(
                new TypeTest
                {
                    UserStatId = test.Id
                });
            Assert.Equal(expected, test.TypeTests[0].UserStatId);
            Assert.True(test.TypeTests.Count == 1);
        }

        [Fact]
        public void TestUserStatTypeTest(){
            TypeTest test = new TypeTest();
            test.UserStat = new UserStat();
            test.UserStat.NumberOfTests=1;
            Assert.Equal(1, test.UserStat.NumberOfTests);
        }

        [Fact] 
        public void UserStatCatJoinTest(){
            var join = new UserStatCatJoin();
            join.User = new User(){ Auth0Id = "varchar", Revapoints = 10};
            join.UserStat = new UserStat(){NumberOfTests = 1};

            Assert.Equal(1, join.UserStat.NumberOfTests);
            Assert.Equal(10, join.User.Revapoints);
            Assert.Equal("varchar", join.User.Auth0Id);

        }

    }
}