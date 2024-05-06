using AutoMapper;
using messaging_service.Controllers;
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
    public class WorkspaceTests
    {
        public IWorkspaceRepository workspaceRepository { get; set; }
        public IMapper mapper { get; set; }
        public WorkspaceController Controller { get; set; }
        public WorkspaceTests()
        {
            workspaceRepository = Mock.Of<IWorkspaceRepository>();
            mapper = Mock.Of<IMapper>();
            Controller = new WorkspaceController(workspaceRepository, mapper);
        }

        [Fact]
        public async Task CreateWorkspace_ValidWorkspace_ReturnsOkWithSuccessMessage()
        {
            // Arrange
            var workspace = new Workspace();
            var workspace1 = new WorkspaceRequestDto();
            Mock.Get(workspaceRepository).Setup(repo => repo.CreateWorkspaceAsync(workspace)).Returns(Task.CompletedTask);

            // Act & Assert
            async Task<ActionResult<ResponseDto>> Action() => await Controller.CreateWorkspace(workspace1);
            var result = await Action();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<ResponseDto>(okResult.Value);
            Assert.True(response.IsSuccess);
            Assert.Null(response.Result);
            Assert.Equal("Workspace Succesfully Created", response.Message);
        }

        [Fact]
        public async Task DeleteWorkspace_ValidId_ReturnsOkWithSuccessMessage()
        {
            // Arrange
            int validId = 123;
            Mock.Get(workspaceRepository).Setup(repo => repo.DeleteWorkspaceAsync(validId)).Returns(Task.CompletedTask);

            // Act & Assert
            async Task<ActionResult<ResponseDto>> Action() => await Controller.DeleteWorkspace(validId);
            var result = await Action();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<ResponseDto>(okResult.Value);
            Assert.True(response.IsSuccess);
            Assert.Null(response.Result);
            Assert.Equal("Workspace Succesfully Deleted", response.Message);
        }

        [Fact]
        public async Task ChangeWorkspaceName_ValidIdAndName_ReturnsOkWithSuccessMessage()
        {
            // Arrange
            int validId = 123;
            var workspaceName = new WorkspaceName { };
            Mock.Get(workspaceRepository).Setup(repo => repo.UpdateWorkspaceAsync(validId, workspaceName.Name)).Returns(Task.CompletedTask);

            // Act & Assert
            async Task<ActionResult<ResponseDto>> Action() => await Controller.ChangeWorkspaceName(validId, workspaceName);
            var result = await Action();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<ResponseDto>(okResult.Value);
            Assert.True(response.IsSuccess);
            Assert.Null(response.Result);
            Assert.Equal("Workspace Name Succesfully Updated", response.Message);
        }

        [Fact]
        public async Task GetWorkspace_ValidIdAndUserId_ReturnsOkWithWorkspaceDetails()
        {
            // Arrange
            int validId = 123;
            int validUserId = 456; 
            var workspaceDetailDto = new WorkspaceDetailDto { };
            Mock.Get(workspaceRepository).Setup(repo => repo.GetWorkspaceAsync(validId, validUserId)).ReturnsAsync(workspaceDetailDto);

            // Act & Assert
            async Task<ActionResult<ResponseDto>> Action() => await Controller.GetWorkspace(validId, validUserId);
            var result = await Action();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<ResponseDto>(okResult.Value);
            Assert.True(response.IsSuccess);
            Assert.Equal(workspaceDetailDto, response.Result);
            Assert.Equal("Succesfully got your workspace!", response.Message);
        }



    }
}
