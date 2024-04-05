﻿
using AutoMapper;
using Azure;
using messaging_service.Controllers;
using messaging_service.models.domain;
using messaging_service.models.dto.Detailed;
using messaging_service.models.dto.Minimal;
using messaging_service.models.dto.Requests;
using messaging_service.models.dto.Response;
using messaging_service.Repository;
using messaging_service.Repository.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace messaging_service.tests.repository.tests
{
    public class UserTests
    {
        public IUserRepository userRepository { get; set; }
        public IMapper mapper { get; set; }
        public UserController Controller { get; set; }
        public UserTests() {
            userRepository = Mock.Of<IUserRepository>();
            mapper = Mock.Of<IMapper>();
            Controller = new UserController(userRepository,mapper);
        }

        [Fact]
        public async Task CreateUser_ValidUser_VerifiesIfAValidUserIsStored()
        {
            //Arrange
            UserMinimalDto userDto = new()
            {
                AuthId = "Test",
                Name = "Test",
                Email = "Test",
            };

            User user = new()
            {
                Id = 1,
                Name = "Test",
                Email = "Test",
                AuthId = "Test",
                Created_at = DateTime.Now,
            };
            var responseDto = new ResponseDto { IsSuccess = true, Message = "Successfully Created !" };
            Mock.Get(mapper).Setup(m => m.Map<User>(userDto)).Returns(user);
            Mock.Get(userRepository).Setup(u=>u.CreateUserAsync(user)).Returns(Task.CompletedTask);
            //Act
            var result = await Controller.CreateUser(userDto);
            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<ResponseDto>(okResult.Value);
            Assert.True(response.IsSuccess);
            Assert.Equal(responseDto.Message,response.Message);
        }

        [Fact]
        public async Task GetUser_ValidAuthId_ReturnsOkWithUserDetails()
        {
            // Arrange
            string validAuthId = "validAuthId";
            var user = new User { /* Initialize user object with valid data */ };
            var expectedDto = new UserDetailDto { /* Initialize expected DTO with user details */ };
            Mock.Get(userRepository).Setup(repo => repo.GetUserAsync(validAuthId)).ReturnsAsync(user);
            Mock.Get(mapper).Setup(mapper => mapper.Map<UserDetailDto>(user)).Returns(expectedDto);

            // Act
            var result = await Controller.GetUser(validAuthId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<ResponseDto>(okResult.Value);
            Assert.True(response.IsSuccess);
            Assert.Equal(expectedDto, response.Result);
            // Additional assertions if necessary
        }

        [Fact]
        public async Task GetUser_InvalidAuthId_ReturnsNotFound()
        {
            // Arrange
            string invalidAuthId = "invalidAuthId";
            Mock.Get(userRepository).Setup(repo => repo.GetUserAsync(invalidAuthId)).ThrowsAsync(new ValidationException("No User Was Found"));

            // Act
            async Task<ActionResult<ResponseDto>> Action() => await Controller.GetUser(invalidAuthId);

            // Assert
            await Assert.ThrowsAsync<ValidationException>(Action);
        }

        [Fact]
        public async Task GetUserById_ValidId_ReturnsOkWithUserDetails()
        {
            // Arrange
            int validId = 123;
            var user = new User { };
            var expectedDto = new UserDetailDto { };
            Mock.Get(userRepository).Setup(repo => repo.GetUserAsync(validId)).ReturnsAsync(user);
            Mock.Get(mapper).Setup(mapper => mapper.Map<UserDetailDto>(user)).Returns(expectedDto);

            // Act & Assert
            async Task<ActionResult<ResponseDto>> Action() => await Controller.GetUserById(validId);
            var result = await Action();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<ResponseDto>(okResult.Value);
            Assert.True(response.IsSuccess);
            Assert.Equal(expectedDto, response.Result);
        }

        [Fact]
        public async Task DeleteUser_ValidAuthId_ReturnsOkWithSuccessMessage()
        {
            // Arrange
            string validAuthId = "validAuthId";
            Mock.Get(userRepository).Setup(repo => repo.DeleteUserAsync(validAuthId)).Returns(Task.CompletedTask);

            // Act & Assert
            async Task<ActionResult<ResponseDto>> Action() => await Controller.DeleteUser(validAuthId);
            var result = await Action();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<ResponseDto>(okResult.Value);
            Assert.True(response.IsSuccess);
            Assert.Null(response.Result);
            Assert.Equal("Successfully Deleted", response.Message);
        }

        [Fact]
        public async Task UpdateUser_ValidDto_ReturnsOkWithSuccessMessage()
        {
            // Arrange
            var userDto = new UserMinimalDto { };
            var user = new User { };
            Mock.Get(userRepository).Setup(repo => repo.UpdateUserAsync(user)).Returns(Task.CompletedTask);

            // Act & Assert
            async Task<ActionResult<ResponseDto>> Action() => await Controller.UpdateUser(userDto);
            var result = await Action();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<ResponseDto>(okResult.Value);
            Assert.True(response.IsSuccess);
            Assert.Null(response.Result);
            Assert.Equal("Successfully Updated", response.Message);
        }

        [Fact]
        public async Task AddUsersToWorkspace_ValidDto_ReturnsOkWithSuccessMessage()
        {
            // Arrange
            var usersDto = new UsersWorkSpaceDto { };
            IEnumerable<string> mockResult = new List<string>(); 
            Mock.Get(userRepository).Setup(repo => repo.AddUsersToWorkspace(usersDto.WorkspaceId, usersDto.UsersId)).ReturnsAsync(mockResult);

            // Act & Assert
            async Task<ActionResult<ResponseDto>> Action() => await Controller.AddUsersToWorkspace(usersDto);
            var result = await Action();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<ResponseDto>(okResult.Value);
            Assert.True(response.IsSuccess);
            Assert.Equal(mockResult.ToList(), response.Result);
            Assert.Equal("Added Users To Workspace", response.Message);
        }

        [Fact]
        public async Task GetUsersByWorkspaceId_ValidWorkspaceId_ReturnsOkWithUserDetails()
        {
            // Arrange
            int validWorkspaceId = 123; 
            var users = new List<UserDetailDto> { };
            Mock.Get(userRepository).Setup(repo => repo.GetUsersByWorkspaceAsync(validWorkspaceId)).ReturnsAsync(users);

            // Act & Assert
            async Task<ActionResult<ResponseDto>> Action() => await Controller.GetUsersByWorkspaceId(validWorkspaceId);
            var result = await Action();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<ResponseDto>(okResult.Value);
            Assert.True(response.IsSuccess);
            Assert.Equal(users, response.Result);
            Assert.Equal("Users from your workspace", response.Message);
        }

        [Fact]
        public async Task GetUsersByChannelId_ValidChannelId_ReturnsOkWithUserDetails()
        {
            // Arrange
            int validChannelId = 123;
            var users = new List<User> { };
            var userDtos = new List<UserDetailDto> { };
            Mock.Get(userRepository).Setup(repo => repo.GetUsersByChannelAsync(validChannelId)).ReturnsAsync(users);
            Mock.Get(mapper).Setup(mapper => mapper.Map<IEnumerable<UserDetailDto>>(users)).Returns(userDtos);

            // Act & Assert
            async Task<ActionResult<ResponseDto>> Action() => await Controller.GetUsersByChannelId(validChannelId);
            var result = await Action();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<ResponseDto>(okResult.Value);
            Assert.True(response.IsSuccess);
            Assert.Equal(userDtos, response.Result);
            Assert.Equal("Users from your channel", response.Message);
        }

        [Fact]
        public async Task RemoveUsersFromWorkspace_ValidIds_ReturnsOkWithSuccessMessage()
        {
            // Arrange
            int userId = 123; 
            int workspaceId = 456;
            Mock.Get(userRepository).Setup(repo => repo.RemoveUserFromWorkspace(workspaceId, userId)).Returns(Task.CompletedTask);

            // Act & Assert
            async Task<ActionResult<ResponseDto>> Action() => await Controller.RemoveUsersFromWorkspace(userId, workspaceId);
            var result = await Action();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<ResponseDto>(okResult.Value);
            Assert.True(response.IsSuccess);
            Assert.Null(response.Result);
            Assert.Equal("Successfully Deleted User From Workspace", response.Message);
        }

        [Fact]
        public async Task GetUserByEmail_ValidEmail_ReturnsOkWithUserDetails()
        {
            // Arrange
            string validEmail = "test@example.com"; 
            var user = new User { };
            var expectedDto = new UserDetailDto { };
            Mock.Get(userRepository).Setup(repo => repo.GetUserByEmailAsync(validEmail)).ReturnsAsync(user);
            Mock.Get(mapper).Setup(mapper => mapper.Map<UserDetailDto>(user)).Returns(expectedDto);

            // Act & Assert
            async Task<ActionResult<ResponseDto>> Action() => await Controller.GetUserByEmail(validEmail);
            var result = await Action();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<ResponseDto>(okResult.Value);
            Assert.True(response.IsSuccess);
            Assert.Equal(expectedDto, response.Result);
            Assert.Equal("User Found!", response.Message);
        }





    }
}
