using System;
using Xunit;
using UserTestsModels;
using System.ComponentModel.DataAnnotations;

namespace UTTests
{
    public class UserModelTest
    {
        /// <summary>
        /// Tests for Constructor
        /// </summary>
        [Fact]
        public void VerifyEmptyConstructor() 
        {
            User test = new User();
            Assert.NotNull(test);
        }
        

        /// <summary>
        /// Tests for Id
        /// </summary>
        [Fact]
        public void VerifyIdIsInt() 
        {
            User test = new User();
            test.Id = 1;
            Type expected = typeof(int);
            Assert.Equal(test.Id.GetType(), expected);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void VerifyIdValidationShouldThrowException(int input)
        {
            User test = new User();
            Assert.Throws<ValidationException>(() => test.Id = input);
        }

        [Fact]
        public void VerifyId() 
        {
            User test = new User();
            int expected = 1;
            test.Id = expected;
            Assert.Equal(expected, test.Id);
        }


        /// <summary>
        /// Tests for AuthOId
        /// </summary>
        [Fact]
        public void VerifyAuth0IdIsString() 
        {
            User test = new User();
            test.Auth0Id = "abc";
            Type expected = typeof(string);
            Assert.Equal(test.Auth0Id.GetType(), expected);
        }

        [Fact]
        public void VerifyAuth0Id() 
        {
            User test = new User();
            var expected = "abc";
            test.Auth0Id = expected;
            Assert.Equal(expected, test.Auth0Id);
        }


        /// <summary>
        /// Tests for Revapoints
        /// </summary>
        [Fact]
        public void VerifyRevapointsIsInt() 
        {
            User test = new User();
            test.Revapoints = 1;
            Type expected = typeof(int);
            Assert.Equal(test.Revapoints.GetType(), expected);
        }

        [Fact]
        public void VerifyRevapoints() 
        {
            User test = new User();
            var expected = 1;
            test.Revapoints = expected;
            Assert.Equal(expected, test.Revapoints);
        }

    }
}