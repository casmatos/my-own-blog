using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Blog.Service.CommentData;
using CMS.Blog.Service.DTOData;
using CMS.Blog.Service.PostData;
using CMS.Blog.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Tests
{
    public class PostControllerTests
    {
        private Mock<IPostService> mockPostService = default;
        private Mock<ICommentService> mockCommentService = default;

        public PostControllerTests()
        {
            mockPostService = new Mock<IPostService>();
            mockCommentService = new Mock<ICommentService>();

        }

        [Fact]
        public async Task GetAll_Returns_Existing_Post()
        {

            // Arrange
            mockPostService
                        .Setup(service => service.GetPosts())
                        .ReturnsAsync(FillPosts())
                        .Verifiable();

            var expected = FillPosts();
            var controller = new PostController(mockPostService.Object, mockCommentService.Object, null);

            // Act
            var posts = await controller.GetAll();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(posts.Result);

            List<PostDTO> dadosApi = (List<PostDTO>)okObjectResult.Value;
            Assert.True(expected.Except(dadosApi).Any());
        }

        [Fact]
        public async Task GetOnePost_Returns_Existing_Post()
        {

            // Arrange
            var postObj = FillPosts().FirstOrDefault();

            mockPostService
                        .Setup(service => service.GetPost(postObj.Id.Value))
                        .ReturnsAsync(postObj);

            var controller = new PostController(mockPostService.Object, mockCommentService.Object, null);

            // Act
            var posts = await controller.Get(postObj.Id.Value);


            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(posts.Result);
            var returnValue = Assert.IsType<PostDTO>(okObjectResult.Value);

            Assert.Equal(postObj, returnValue);
        }

        [Fact]
        public async Task PostNewPost_Returns_Existing_Post()
        {

            // Arrange
            var postObj = new PostDTO
            {
                Id = Guid.NewGuid(),
                Title = "Windows 10",
                Content = "Operation System"
            };

            mockPostService
                        .Setup(service => service.CreatePost(It.IsAny<PostDTO>()))
                        .ReturnsAsync(postObj);

            var controller = new PostController(mockPostService.Object, mockCommentService.Object, null);

            // Act
            var posts = await controller.Post(postObj);


            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(posts.Result);
            var returnValue = Assert.IsType<PostDTO>(okObjectResult.Value);

            Assert.Equal(postObj, returnValue);
        }

        [Fact]
        public async Task UpdateExistPost_Returns_Ok()
        {

            // Arrange
            var postObj = new PostDTO
            {
                Id = Guid.NewGuid(),
                Title = "Update Title Post",
                Content = "Update Content Post",
            };

            mockPostService
                        .Setup(service => service.UpdatePost(It.IsAny<Guid>(), It.IsAny<PostDTO>()))
                        .ReturnsAsync(postObj);

            var controller = new PostController(mockPostService.Object, mockCommentService.Object, null);

            // Act
            var actionResultPosts = await controller.Put(postObj.Id.Value, postObj);


            // Assert
            Assert.NotNull(actionResultPosts);
            var objectResult = Assert.IsType<OkObjectResult>(actionResultPosts.Result);
            Assert.Equal(postObj, objectResult.Value);
        }

        [Fact]
        public async Task DeleteExistPost_Returns_Ok()
        {

            // Arrange
            bool postDeleted = true;
            Guid recordDeleted = Guid.NewGuid();

            mockPostService
                        .Setup(service => service.DeletePost(It.IsAny<Guid>()))
                        .ReturnsAsync(postDeleted);

            var controller = new PostController(mockPostService.Object, mockCommentService.Object, null);

            // Act
            var posts = await controller.Delete(recordDeleted);


            // Assert
            Assert.NotNull(posts);
            Assert.IsType<OkResult>(posts);
        }

        public List<PostDTO> FillPosts()
        {
            List<PostDTO> listPosts = new List<PostDTO>
            {
                new PostDTO
                {
                    Id = Guid.Parse("4f776fe3-c983-4328-a79a-7f5a5dc9fabd"),
                    Title = ".NET 5.0",
                    Content = "Best backend framework"
                },
                new PostDTO
                {
                    Id = Guid.Parse("ef801aa4-e88e-422c-bebb-7981dcebdffe"),
                    Title = "Blazor WASM",
                    Content = "Best front framework"
                }
            };

            return listPosts;
        }
    }
}