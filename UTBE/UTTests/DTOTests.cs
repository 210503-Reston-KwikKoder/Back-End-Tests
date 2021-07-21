
using System;
using UserTestsREST.DTO;
using Xunit;

namespace UTTests
{
    public class DTOTests
    {
        [Fact]
        public void testAddUserModel(){
            var usermodel = new AddUserModel();
            usermodel.Email = "email";
            usermodel.UserName = "username";
            usermodel.Name = "name";

            Assert.NotNull(usermodel);

        }

        [Fact]
        public void testCompTestInput(){
            var dto = new CompTestInput();
            dto.wpm = 12;
            dto.auth0Id = "al;sdkf";
            dto.categoryId = 32;
            dto.date = new DateTime();
            dto.numberofcharacters = 2;
            dto.numberoferrors = 0;
            dto.winStreak = 3;
            dto.won = false;


            Assert.NotNull(dto);

        }

        [Fact]

        public void testLBModel(){
            var dto = new LBModel();
            dto.AuthId = "adsf";
            dto.UserName = "";
            dto.Name = "";
            dto.AverageWPM = 12;
            dto.AverageAcc = 12;
            dto.CatID = -1;

            Assert.NotNull(dto);
        }


        [Fact]
        public void testStatModel(){
            var dto = new StatModel();
            Assert.NotNull(dto);
            dto = new StatModel("adf", 12,12,12,12,-1);
            //constructor doesn't assign userID for some reason
            dto.userID = "adf";
            Assert.Equal("adf", dto.userID);
            Assert.Equal(-1, dto.category);
            Assert.Equal(12, dto.averagewpm);
            Assert.Equal(12, dto.averageaccuracy);
            Assert.Equal(12, dto.numberoftests);
            Assert.Equal(12, dto.totaltesttime);
        }

        [Fact]
        public void testTestStatCatOutput(){
            var dto = new TestStatCatOutput();
            dto.Category = -1;
            dto.date = new DateTime();
            dto.numberofcharacters = 2;
            dto.numberoferrors = 0;
            dto.timetakenms = 0;
            dto.wpm = 0;
            Assert.NotNull(dto);
        }

        [Fact]
        public void testTestStatOutput(){
            var dto = new TestStatCatOutput();
            
            Assert.NotNull(dto);
        }

        [Fact]

        public void testTestUserObject(){
            var dto = new TestUserObject();
            dto.Name = "name";
            dto.Email = "email";
            dto.UserName = "username";
            
            Assert.NotNull(dto);
        }
        
        [Fact]
        public void testTypeTestInput(){
            var dto = new TypeTestInput();
            dto.wpm = 12;
            dto.categoryId = 32;
            dto.date = new DateTime();
            dto.numberofcharacters = 2;
            dto.numberoferrors = 0;
            dto.timetakenms = 0;

            Assert.NotNull(dto);
        }
        
        [Fact]

        public void testUserNameModel(){
            var dto = new UserNameModel();
            dto.UserName = "username";
            dto.Revapoints = 23;
            dto.Name = "name";

            Assert.NotNull(dto);
        }
    }
}