using System;
using Xunit;
using UserTestsModels;
namespace UTTests
{
    public class ModelTesting
    {
       
     /// <summary>
     /// we check base on the type of category name, it will not be integer or int32 if not set or no not int value assigned 
     /// </summary>
        [Fact]
        public void newCategoryWithEmptyNameOrNameNotIntegerShouldFaild()
        {
        //Given
        Category category=new Category();
        
        //When
        category.Id=90;
        
        
        //Then
        Assert.False( category.Name.GetType() !=typeof(int));
     
        }


        /// <summary>
        /// we make sure that authId0 value is provided for any new user 
        /// </summary>
        [Fact]
        public void newUserWithEmptyAuth0IdShouldFaild()
        {
        //Given
        User user=new User();
        
        //When
        user.Id=9;
        
        
        //Then
        Assert.False(user.Auth0Id=="");
     
        }
   
   
    }
}
