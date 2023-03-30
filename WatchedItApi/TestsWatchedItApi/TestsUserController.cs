using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WatchedItApi.Controllers;
using WatchedItApi.Data;
using WatchedItApi.Models;

namespace TestsWatchedItApi
{
    public class TestsUserController
    {
        Context _context = null!;

        public UsersController usersController = null!;

        public TestsUserController()
        {
            SeedDb();
        }

        [Fact]
        private void SeedDb()
        {
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestsUserControllerDb")
                .Options;

            _context = new Context(options);
            _context.Database.EnsureDeleted();
            usersController = new UsersController(_context);

            var dtoPieter = new UserDto
            {
                Id = 1,
                Name = "pieter",
                Email = "peter@example.com",
                Phone = "+31619011796",
                Password = "user123",
            };

            var dtoJan = new UserDto
            {
                Id = 2,
                Name = "jan",
                Email = "jan@example.com",
                Phone = "+31619011888",
                Password = "user",
            };

            User newUserPieter = new User(dtoPieter);
            User newUserJan = new User(dtoJan);
            newUserPieter.encrypt();
            newUserJan.encrypt();


            _context.Users.Add(newUserPieter);
            _context.Users.Add(newUserJan);
            _context.SaveChanges();
        }

        [Fact]
        public async Task Test_GetAll_OKResult()
        {
            //arrange

            //act
            var user = await usersController.GetAll();
            var result = user;

            //assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Test_GetUserById_OKResult()
        {
            //arrange
            int userid = 1;

            //act
            var user = await usersController.GetUser(userid);
            var result = (ObjectResult)user;

            //assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Test_GetUserById_NotFoundResult()
        {
            //arrange
            var userid = 0;

            //act
            var user = await usersController.GetUser(userid);
            var result = (NotFoundResult)user;

            //assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task Test_Login_OkResult()
        {
            //arrange
            var myuser = new UserDto
            {
                Email = "peter@example.com",
                Password = "user123"
            };

            //act
            var user = await usersController.Login(myuser);
            var result = (ObjectResult)user;

            //assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Test_Login_BadRequestResult()
        {
            //arrange
            var myuser = new UserDto
            {
                Email = "peter@example.com",
                Password = "user123456789"
            };

            //act
            var user = await usersController.Login(myuser);
            var result = (ObjectResult)user;

            //assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task Test_Register_OkRequestResult()
        {
            //arrange
            var myuser = new UserDto
            {
                Name = "Piet",
                Email = "piet@example.com",
                Phone = "+31611201030",
                Password = "user12345"
            };

            //act
            var user = await usersController.SignUp(myuser);
            var result = (ObjectResult)user;

            //assert
            Assert.NotNull(result);
            Assert.Equal(201, result.StatusCode);
        }

        [Fact]
        public async Task Test_Register_BadRequestResult()
        {
            //arrange
            var myuser = new UserDto
            {
                Name = "Piet",
                Email = null,
                Phone = "+31611201030",
                Password = "user12345"
            };

            //act
            var user = await usersController.SignUp(myuser);
            var result = (ObjectResult)user;

            //assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task Test_AddFriend_OkRequestResult()
        {
            //arrange
            var myuser = new UserDto
            {
                Id = 1,
                Phone = "+31619011888",
            };

            //act
            var user = await usersController.AddFriend(myuser);
            var result = (ObjectResult)user;

            //assert
            Assert.NotNull(result);
            Assert.Equal(201, result.StatusCode);
        }

        [Fact]
        public async Task Test_AddFriend_BadRequestResult()
        {
            //arrange
            var myuser = new UserDto
            {
                Id = 111,
                Phone = "+31611201030",
            };

            //act
            var user = await usersController.AddFriend(myuser);
            var result = (ObjectResult)user;

            //assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task Test_GetFriends_OKResult()
        {
            //arrange
            int userid = 1;

            //act
            var user = await usersController.GetFriends(userid);
            var result = (ObjectResult)user;

            //assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Test_DeleteFriend_BadRequestResult()
        {
            //arrange
            int friendid = 0;

            //act
            var user = await usersController.DeleteFriend(friendid);
            var result = (BadRequestResult)user;

            //assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }
    }
}