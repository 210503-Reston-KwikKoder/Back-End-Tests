using System;
using Xunit;
using UserTestsModels;
using System.ComponentModel.DataAnnotations;

namespace UTTests
{
    public class UserModelTest
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void VerifyIdValidationShouldThrowException(int input)
        {
            User test = new User();
            Assert.Throws<ValidationException>(() => test.Id = input);
        }

        [Fact]
        public void VerifyIdValidation() 
        {
            User test = new User();
            int expected = 1;
            test.Id = expected;
            Assert.Equal(expected, test.Id);
        }

        [Fact]
        public void VerifyAuth0IdIsString() 
        {
            User test = new User();
            test.Auth0Id = "abc";
            Type expected = typeof(string);
            Assert.Equal(test.Auth0Id.GetType(), expected);
        }

        [Fact]
        public void VerifyRevapointsIsInt() 
        {
            User test = new User();
            test.Revapoints = 1;
            Type expected = typeof(int);
            Assert.Equal(test.Revapoints.GetType(), expected);
        }

        [Fact]
        public void VerifyEmptyConstructor() 
        {
            User test = new User();
            Assert.NotNull(test);
        }
    }
}