using AutoMapper;
using notif_service.Controllers;
using notif_service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using notif_service.Models;


namespace notif_service.tests
{
    public class ControllerTests
    {
        private readonly NotificationController _controller;
        private readonly IMapper _mapper;
        private readonly INotificationService _service;
        public ControllerTests()
        {
            _mapper = Mock.Of<IMapper>();
            _service = Mock.Of<INotificationService>();
            _controller = new NotificationController(_service, _mapper);
        }

        [Fact]
        public async Task GetUnseenNotifications_Returns_OK()
        {
            // Arrange
            var notificationString = "test";
            var notifications = new List<Notification>();
            var expectedResponse = new ResponseDto
            {
                IsSuccess = true,
                Message = "You Have Unseen Notifications!",
                Result = new List<NotificationDto>()
            };

            Mock.Get(_service).Setup(x => x.GetUnseenNotificationsAsync(notificationString))
                .ReturnsAsync(notifications);
            Mock.Get(_mapper).Setup(x => x.Map<List<NotificationDto>>(notifications))
                .Returns((List<NotificationDto>)expectedResponse.Result);

            // Act
            var result = await _controller.GetUnseenNotifications(notificationString);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<ResponseDto>(okResult.Value);
            Assert.True(response.IsSuccess);
            Assert.Equal(expectedResponse.Message, response.Message);
            Assert.Equal(expectedResponse.Result, response.Result);
        }

        [Fact]
        public async Task GetseenNotifications_Returns_OK()
        {
            // Arrange
            var notificationString = "test";
            var page = 1;
            var notifications = new List<Notification>();
            var expectedResponse = new ResponseDto
            {
                IsSuccess = true,
                Message = "Old Notifications!",
                Result = new List<NotificationDto>()
            };

            Mock.Get(_service).Setup(x => x.GetMoreNotificationsAsync(notificationString, page))
                .ReturnsAsync(notifications);
            Mock.Get(_mapper).Setup(x => x.Map<List<NotificationDto>>(notifications))
                .Returns((List<NotificationDto>)expectedResponse.Result);

            // Act
            var result = await _controller.GetseenNotifications(notificationString, page);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<ResponseDto>(okResult.Value);
            Assert.True(response.IsSuccess);
            Assert.Equal(expectedResponse.Message, response.Message);
            Assert.Equal(expectedResponse.Result, response.Result);
        }
        [Fact]
        public async Task SetSeenNotifications_Returns_OK()
        {
            // Arrange
            var notificationString = "test";

            // Act
            var result = await _controller.SetSeenNotifications(notificationString);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<ResponseDto>(okResult.Value);
            Assert.True(response.IsSuccess);
            Assert.Equal("Notifications Seen!", response.Message);
        }

        [Fact]
        public async Task AddNotification_Returns_OK()
        {
            // Arrange
            var notificationDto = new NotificationDtoV2();
            var notification = new Notification();
            Mock.Get(_mapper).Setup(x => x.Map<Notification>(notificationDto)).Returns(notification);

            // Act
            var result = await _controller.AddNotification(notificationDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<ResponseDto>(okResult.Value);
            Assert.True(response.IsSuccess);
            Assert.Equal("Notifications Added!", response.Message);
        }
    }
}