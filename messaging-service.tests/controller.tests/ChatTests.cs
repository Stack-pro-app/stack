using AutoMapper;
using messaging_service.models.domain;
using Moq;
using messaging_service.Controllers;
using messaging_service.models.dto.Requests;
using messaging_service.models.dto.Response;
using Microsoft.AspNetCore.Mvc;
using messaging_service.MappingProfiles;
using messaging_service.Repository.Interfaces;
using messaging_service.models.dto.Detailed;
namespace messaging_service.tests.controller.tests
{
    public class ChatControllerTests
    {
        private readonly Mock<IChatRepository> mockChatRepository;
        private readonly IMapper mapper;
        private readonly ChatController controller;

        public ChatControllerTests()
        {
            mockChatRepository = new Mock<IChatRepository>();
            mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<ChatProfile>()));
            controller = new ChatController(mockChatRepository.Object, mapper);
        }

        [Fact]
        public async Task StoreMessage_ValidMessage_ReturnsOkWithSuccessMessage()
        {
            // Arrange
            var messageRequestDto = new MessageRequestDto { Message = "Hello",ChannelId= 1234,UserId= 12 };
            mockChatRepository.Setup(repo => repo.CreateChatAsync(It.IsAny<Chat>())).Returns(Task.CompletedTask);

            // Act
            var result = await controller.StoreMessage(messageRequestDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<ResponseDto>(okResult.Value);
            Assert.True(response.IsSuccess);
            Assert.Equal("Succesfully Stored Your Message!", response.Message);
        }

        [Fact]
        public async Task DeleteMessage_ValidMessageId_ReturnsOkWithSuccessMessage()
        {
            // Arrange
            var messageId = Guid.NewGuid(); 
            mockChatRepository.Setup(repo => repo.DeleteChatPermAsync(messageId)).Returns(Task.CompletedTask);

            // Act
            var result = await controller.DeleteMessage(messageId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<ResponseDto>(okResult.Value);
            Assert.True(response.IsSuccess);
            Assert.Equal("Succesfully Deleted Your Message!", response.Message);
        }

        [Fact]
        public async Task UpdateMessage_ValidMessage_ReturnsOkWithSuccessMessage()
        {
            // Arrange
            var messageUpdateDto = new MessageUpdateDto { };
            mockChatRepository.Setup(repo => repo.UpdateChatAsync(messageUpdateDto.MessageId, messageUpdateDto.Message)).Returns(Task.CompletedTask);

            // Act
            var result = await controller.UpdateMessage(messageUpdateDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<ResponseDto>(okResult.Value);
            Assert.True(response.IsSuccess);
            Assert.Equal("Succesfully Updated Your Message!", response.Message);
        }
        [Fact]
        public async Task GetMessageById_ValidId_ReturnsOkWithMessageDetailDto()
        {
            // Arrange
            var messageId = 1;
            var messageDetailDto = new MessageDetailDto {  };
            mockChatRepository.Setup(repo => repo.GetMessageAsync(messageId)).ReturnsAsync(messageDetailDto);

            // Act
            var result = await controller.GetMessageById(messageId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<ResponseDto>(okResult.Value);
            Assert.True(response.IsSuccess);
            Assert.Equal("Here's The Message!", response.Message);
            Assert.Equal(messageDetailDto, response.Result);
        }

        [Fact]
        public async Task GetMessageById_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            var messageId = 1;
            mockChatRepository.Setup(repo => repo.GetMessageAsync(messageId)).ThrowsAsync(new Exception("Message not found"));

            // Act
            var result = await controller.GetMessageById(messageId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var response = Assert.IsType<ResponseDto>(badRequestResult.Value);
            Assert.False(response.IsSuccess);
            Assert.Equal("Can't find the Message!", response.Message);
        }

        [Fact]
        public async Task GetMessageByChannelId_ValidChannelId_ReturnsOkWithMessageDetailDtoList()
        {
            // Arrange
            var channelId = 1;
            var page = 1; 
            var messages = new List<MessageDetailDto> { };
            mockChatRepository.Setup(repo => repo.GetChannelLastMessagesAsync(channelId, page)).ReturnsAsync(messages);

            // Act
            var result = await controller.GetMessageByChannelId(channelId, page);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<ResponseDto>(okResult.Value);
            Assert.True(response.IsSuccess);
            Assert.Equal("Here's The Messages!", response.Message);
            Assert.Equal(messages, response.Result);
        }

        [Fact]
        public async Task GetMessageByChannelId_InvalidChannelId_ReturnsBadRequest()
        {
            // Arrange
            var channelId = 1;
            var page = 1;
            mockChatRepository.Setup(repo => repo.GetChannelLastMessagesAsync(channelId, page)).ThrowsAsync(new Exception("Channel not found"));

            // Act
            var result = await controller.GetMessageByChannelId(channelId, page);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var response = Assert.IsType<ResponseDto>(badRequestResult.Value);
            Assert.False(response.IsSuccess);
            Assert.Equal("Can't find the Messages!", response.Message);
        }

    }

}
