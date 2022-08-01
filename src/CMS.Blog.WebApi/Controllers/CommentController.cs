using Microsoft.AspNetCore.Mvc;

namespace CMS.Blog.WebApi.Controllers
{
    [ApiController]
    [Route("comments")]
    public class CommentController : ControllerBase
    {
        private readonly ILogger<CommentController> _logger;
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService,
                                ILogger<CommentController> logger)
        {
            _logger = logger;
            _commentService = commentService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetAll()
        {
            var result = await _commentService.GetComments();

            if (result is not null)
            {
                return Ok(result);
            }

            return BadRequest("No records found!");
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CommentDTO>> Get([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogError("CommentController(Delete) - Param: Id is empty.");
                throw new ArgumentException(nameof(id));
            }

            var result = await _commentService.GetComment(id);

            if (result is not null)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<CommentDTO>> Post([FromBody] CommentDTO comment)
        {
            if (comment is null)
            {
                throw new ArgumentNullException(nameof(comment));
            }

            var newComment = await _commentService.CreateComment(comment);

            if (newComment is not null)
            {
                return Ok(newComment);
            }

            return BadRequest();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] CommentDTO comment)
        {
            if (comment is null)
            {
                throw new ArgumentNullException(nameof(comment));
            }

            if (id != Guid.Empty && comment.Id != id)
            {
                _logger.LogError("CommentController(PUT) - Param: Id is diferent from Record");
                throw new ArgumentException(nameof(comment));
            }

            try
            {
                var updatePost = await _commentService.UpdateComment(id, comment);

                if (updatePost is not null)
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"CommentController(PUT) - Exeption : {ex.Message}");
                return NotFound();
            }

            return BadRequest();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogError("CommentController(Delete) - Param: Id is empty.");
                throw new ArgumentException(nameof(id));
            }

            try
            {
                var deleted = await _commentService.DeleteComment(id);

                if (deleted)
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"CommentController(Delete) - Exeption : {ex.Message}");
                return NotFound();
            }

            return BadRequest();
        }
        
    }
}