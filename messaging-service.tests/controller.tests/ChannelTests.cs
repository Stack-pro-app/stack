using AutoMapper;
using messaging_service.Controllers;
using messaging_service.MappingProfiles;
using messaging_service.models.domain;
using messaging_service.models.dto.Detailed;
using messaging_service.models.dto.Others;
using messaging_service.models.dto.Requests;
using messaging_service.models.dto.Response;
using messaging_service.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace messaging_service.tests.controller.tests
{
    public class ChannelTests
    {
        private readonly Mock<IChannelRepository> mockChannelRepository;
        private readonly IMapper mapper;
        private readonly ChannelController controller;

        public ChannelTests()
        {
            mockChannelRepository = new Mock<IChannelRepository>();
            mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<ChannelProfile>()));
            controller = new ChannelController(mockChannelRepository.Object, mapper);
        }

        [Fact]
        public async Task CreateChannel_ValidChannel_ReturnsOk()
        {
            // Arrange
            var channelDto = new ChannelRequestDto { };
            mockChannelRepository.Setup(repo => repo.CreateChannelAsync(It.IsAny<Channel>())).Returns(Task.CompletedTask);

            // Act
            var result = await controller.CreateChannel(channelDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<ResponseDto>(okResult.Value);
            Assert.True(response.IsSuccess);
            Assert.Equal("Channel Successfully Created!", response.Message);
            Assert.Null(response.Result);
        }

        [Fact]
        public async Task CreateChannel_InvalidChannel_ReturnsBadRequest()
        {
            // Arrange
            var channelDto = new ChannelRequestDto { };
            mockChannelRepository.Setup(repo => repo.CreateChannelAsync(It.IsAny<Channel>())).ThrowsAsync(new Exception("Failed to create channel"));

            // Act
            var result = await controller.CreateChannel(channelDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var response = Assert.IsType<ResponseDto>(badRequestResult.Value);
            Assert.False(response.IsSuccess);
            Assert.Equal("Failed To Create Channel!", response.Message);
            Assert.Null(response.Result);
        }

        [Fact]
        public async Task DeleteChannel_ValidId_ReturnsOk()
        {
            // Arrange
            var channelId = 1;
            mockChannelRepository.Setup(repo => repo.DeleteChannelAsync(channelId)).Returns(Task.CompletedTask);

            // Act
            var result = await controller.DeleteChannel(channelId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<ResponseDto>(okResult.Value);
            Assert.True(response.IsSuccess);
            Assert.Equal("Succesfully deleted Channel", response.Message);
            Assert.Null(response.Result);
        }

        [Fact]
        public async Task DeleteChannel_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            var channelId = 1;
            mockChannelRepository.Setup(repo => repo.DeleteChannelAsync(channelId)).ThrowsAsync(new Exception("Failed to delete channel"));

            // Act
            var result = await controller.DeleteChannel(channelId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var response = Assert.IsType<ResponseDto>(badRequestResult.Value);
            Assert.False(response.IsSuccess);
            Assert.Equal("Failed To Delete Channel!", response.Message);
            Assert.Null(response.Result);
        }

        [Fact]
        public async Task UpdateChannel_ValidChannel_ReturnsOk()
        {
            // Arrange
            var channelDto = new ChannelUpdateDto { };
            mockChannelRepository.Setup(repo => repo.UpdateChannelAsync(It.IsAny<Channel>())).Returns(Task.CompletedTask);

            // Act
            var result = await controller.UpdateChannel(channelDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<ResponseDto>(okResult.Value);
            Assert.True(response.IsSuccess);
            Assert.Equal("Succesfully updated the Channel!", response.Message);
            Assert.Null(response.Result);
        }

        [Fact]
        public async Task UpdateChannel_InvalidChannel_ReturnsBadRequest()
        {
            // Arrange
            var channelDto = new ChannelUpdateDto { };
            mockChannelRepository.Setup(repo => repo.UpdateChannelAsync(It.IsAny<Channel>())).ThrowsAsync(new Exception("Failed to update channel"));

            // Act
            var result = await controller.UpdateChannel(channelDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var response = Assert.IsType<ResponseDto>(badRequestResult.Value);
            Assert.False(response.IsSuccess);
            Assert.Equal("Failed To Update Channel!", response.Message);
            Assert.Null(response.Result);
        }
        [Fact]
        public async Task GetChannel_ValidId_ReturnsOk()
        {
            // Arrange
            var channelId = 1;
            var channelDetailDto = new ChannelDetailDto { };
            mockChannelRepository.Setup(repo => repo.GetChannelAsync(channelId)).ReturnsAsync(channelDetailDto);

            // Act
            var result = await controller.GetChannel(channelId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<ResponseDto>(okResult.Value);
            Assert.True(response.IsSuccess);
            Assert.Equal("Found it!", response.Message);
            Assert.Equal(channelDetailDto, response.Result);
        }

        [Fact]
        public async Task GetChannel_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            var channelId = 1; 
            mockChannelRepository.Setup(repo => repo.GetChannelAsync(channelId)).ReturnsAsync((ChannelDetailDto)null);

            // Act
            var result = await controller.GetChannel(channelId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var response = Assert.IsType<ResponseDto>(badRequestResult.Value);
            Assert.False(response.IsSuccess);
            Assert.Equal("Failed To Find Channel!", response.Message);
            Assert.Null(response.Result);
        }

        [Fact]
        public async Task AddToPrivateChannel_ValidData_ReturnsOk()
        {
            // Arrange
            var channelId = 1;
            var user = new UserMinDto { };
            mockChannelRepository.Setup(repo => repo.AddUserToPrivateChannel(channelId, user.userId)).Returns(Task.CompletedTask);

            // Act
            var result = await controller.AddToPrivateChannel(channelId, user);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<ResponseDto>(okResult.Value);
            Assert.True(response.IsSuccess);
            Assert.Equal("User Added To Channel Successfully", response.Message);
            Assert.Null(response.Result);
        }

        [Fact]
        public async Task AddToPrivateChannel_InvalidData_ReturnsBadRequest()
        {
            // Arrange
            var channelId = 1;
            var user = new UserMinDto { };
            mockChannelRepository.Setup(repo => repo.AddUserToPrivateChannel(channelId, user.userId)).ThrowsAsync(new Exception("Failed to add user to channel"));

            // Act
            var result = await controller.AddToPrivateChannel(channelId, user);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var response = Assert.IsType<ResponseDto>(badRequestResult.Value);
            Assert.False(response.IsSuccess);
            Assert.Equal("Failed To Add User To Channel!", response.Message);
            Assert.Null(response.Result);
        }

        [Fact]
        public async Task RemoveFromPrivateChannel_ValidData_ReturnsOk()
        {
            // Arrange
            var channelId = 1;
            var userId = 1;
            mockChannelRepository.Setup(repo => repo.RemoveUserFromPrivateChannel(channelId, userId)).Returns(Task.CompletedTask);

            // Act
            var result = await controller.RemoveFromPrivateChannel(channelId, userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<ResponseDto>(okResult.Value);
            Assert.True(response.IsSuccess);
            Assert.Equal("User Removed From Channel Successfully", response.Message);
            Assert.Null(response.Result);
        }

        [Fact]
        public async Task RemoveFromPrivateChannel_InvalidData_ReturnsBadRequest()
        {
            // Arrange
            var channelId = 1;
            var userId = 1;
            mockChannelRepository.Setup(repo => repo.RemoveUserFromPrivateChannel(channelId, userId)).ThrowsAsync(new Exception("Failed to remove user from channel"));

            // Act
            var result = await controller.RemoveFromPrivateChannel(channelId, userId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var response = Assert.IsType<ResponseDto>(badRequestResult.Value);
            Assert.False(response.IsSuccess);
            Assert.Equal("Failed To Remove User From Channel!", response.Message);
            Assert.Null(response.Result);
        }
        [Fact]
        public async Task FindOrCreateOneToOneChannel_NewChannel_ReturnsOk()
        {
            // Arrange
            var request = new OneToOneChannelRequest { };
            var newChannel = new Channel { };
            mockChannelRepository.Setup(repo => repo.GetOneToOneChannel(request)).ReturnsAsync((Channel)null);
            mockChannelRepository.Setup(repo => repo.CreateOneToOneChannel(request)).ReturnsAsync(newChannel);

            // Act
            var result = await controller.FindOrCreateOneToOneChannel(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<ResponseDto>(okResult.Value);
            Assert.True(response.IsSuccess);
            Assert.Equal("Here's the One To One channel", response.Message);
            Assert.NotNull(response.Result);
        }

        [Fact]
        public async Task FindOrCreateOneToOneChannel_ExistingChannel_ReturnsOk()
        {
            // Arrange
            var request = new OneToOneChannelRequest { };
            var existingChannel = new Channel { };
            mockChannelRepository.Setup(repo => repo.GetOneToOneChannel(request)).ReturnsAsync(existingChannel);

            // Act
            var result = await controller.FindOrCreateOneToOneChannel(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<ResponseDto>(okResult.Value);
            Assert.True(response.IsSuccess);
            Assert.Equal("Here's the One To One channel", response.Message);
            Assert.NotNull(response.Result);
        }
    }
}
