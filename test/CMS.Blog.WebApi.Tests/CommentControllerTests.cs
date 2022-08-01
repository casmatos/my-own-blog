using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Blog.Service.CommentData;
using CMS.Blog.Service.DTOData;
using CMS.Blog.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Tests
{
    public class CommentControllerTests
    {
        [Fact]
        public async Task GetAll_Returns_Existing_Comments()
        {

            // Arrange
            var mockCommentService = new Mock<ICommentService>();

            mockCommentService
                        .Setup(service => service.GetComments())
                        .ReturnsAsync(FillComments())
                        .Verifiable();

            var expected = FillComments();
            var controller = new CommentController(mockCommentService.Object, null);

            // Act
            var comments = await controller.GetAll();


            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(comments.Result);

            List<CommentDTO> dadosApi = (List<CommentDTO>)okObjectResult.Value;
            Assert.True(expected.Except(dadosApi).Any());
        }

        [Fact]
        public async Task GetOneComment_Returns_Existing_Comment()
        {

            // Arrange
            var mockCommentService = new Mock<ICommentService>();
            var commentObj = FillComments().FirstOrDefault();

            mockCommentService
                        .Setup(service => service.GetComment(commentObj.Id.Value))
                        .ReturnsAsync(commentObj);

            var controller = new CommentController(mockCommentService.Object, null);

            // Act
            var comments = await controller.Get(commentObj.Id.Value);


            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(comments.Result);
            var returnValue = Assert.IsType<CommentDTO>(okObjectResult.Value);

            Assert.Equal(commentObj, returnValue);
        }

        [Fact]
        public async Task PostNewComment_Returns_Existing_Comment()
        {

            // Arrange
            var mockCommentService = new Mock<ICommentService>();
            var commentObj = new CommentDTO
            {
                Id = Guid.NewGuid(),
                Author = "New Author",
                Content = "New Content",
                PostId = Guid.NewGuid()
            };

            mockCommentService
                        .Setup(service => service.CreateComment(It.IsAny<CommentDTO>()))
                        .ReturnsAsync(commentObj);

            var controller = new CommentController(mockCommentService.Object, null);

            // Act
            var comments = await controller.Post(commentObj);


            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(comments.Result);
            var returnValue = Assert.IsType<CommentDTO>(okObjectResult.Value);

            Assert.Equal(commentObj, returnValue);
        }

        [Fact]
        public async Task UpdateExistComment_Returns_Ok()
        {

            // Arrange
            var mockCommentService = new Mock<ICommentService>();
            var commentObj = new CommentDTO
            {
                Id = Guid.NewGuid(),
                Author = "New Author Updated",
                Content = "New Content Updated",
                PostId = Guid.NewGuid()
            };

            mockCommentService
                        .Setup(service => service.UpdateComment(It.IsAny<Guid>(), It.IsAny<CommentDTO>()))
                        .ReturnsAsync(commentObj);

            var controller = new CommentController(mockCommentService.Object, null);

            // Act
            var comments = await controller.Put(commentObj.Id.Value, commentObj);


            // Assert
            Assert.NotNull(comments);
            Assert.IsType<OkResult>(comments);
        }

        [Fact]
        public async Task DeleteExistComment_Returns_Ok()
        {

            // Arrange
            var mockCommentService = new Mock<ICommentService>();
            bool commentDeleted = true;
            Guid recordDeleted = Guid.NewGuid();

            mockCommentService
                        .Setup(service => service.DeleteComment(It.IsAny<Guid>()))
                        .ReturnsAsync(commentDeleted);

            var controller = new CommentController(mockCommentService.Object, null);

            // Act
            var comments = await controller.Delete(recordDeleted);


            // Assert
            Assert.NotNull(comments);
            Assert.IsType<OkResult>(comments);
        }

        public List<CommentDTO> FillComments()
        {
            List<CommentDTO> listComments = new List<CommentDTO>
            {
                new CommentDTO
                {
                    Id = Guid.Parse("8c78607f-c246-4a92-8dc1-0c7cfc17eea4"),
                    Author = "Camilo",
                    Content = "Content .NET",
                    PostId = Guid.Parse("4f776fe3-c983-4328-a79a-7f5a5dc9fabd")
                },
                new CommentDTO
                {
                    Id = Guid.Parse("8c78607f-c246-4a92-8dc1-0c7cfc17eea4"),
                    Author = "Camilo The Big One",
                    Content = "Content Blazor",
                    PostId = Guid.Parse("ef801aa4-e88e-422c-bebb-7981dcebdffe")
                }
            };

            return listComments;
        }
    }
}