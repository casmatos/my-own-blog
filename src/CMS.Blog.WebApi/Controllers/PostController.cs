using Microsoft.AspNetCore.Mvc;

namespace CMS.Blog.WebApi.Controllers
{
    [ApiController]
    [Route("posts")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;
        private readonly ILogger<PostController> _logger;

        public PostController(IPostService postService,
                                ICommentService commentService,
                                ILogger<PostController> logger)
        {
            _postService = postService;
            _commentService = commentService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetAll()
        {
            var result = await _postService.GetPosts();

            if (result is not null)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PostDTO>> Get([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogError("PostController(Get) - Param: Id is empty.");
                throw new ArgumentException(nameof(id));
            }

            var result = await _postService.GetPost(id);

            if (result is not null)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<PostDTO>> Post([FromBody] PostDTO post)
        {
            if (post is null)
            {
                throw new ArgumentNullException(nameof(post));
            }

            var newPost = await _postService.CreatePost(post);

            if (newPost is not null)
            {
                return Ok(newPost);
            }

            return BadRequest();
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<PostDTO>> Put([FromRoute] Guid id, [FromBody] PostDTO post)
        {
            if (post is null)
            {
                throw new ArgumentNullException(nameof(post));
            }

            if (id != Guid.Empty && post.Id != id)
            {
                _logger.LogError("PostController(PUT) - Param: Id is diferent from Record");
                throw new ArgumentException(nameof(post));
            }

            try
            {
                var editPost = await _postService.UpdatePost(id, post);

                if (editPost is not null)
                {
                    return Ok(editPost);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"PostController(PUT) - Exeption : {ex.Message}");
                return NotFound();
            }

            return BadRequest();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogError("PostController(Delete) - Param: Id is empty.");
                throw new ArgumentException(nameof(id));
            }

            try
            {
                var deleted = await _postService.DeletePost(id);

                if (deleted)
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"PostController(Delete) - Exeption : {ex.Message}");
                return NotFound();
            }

            return BadRequest();
        }

        [HttpGet("{id:guid}/comments")]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetComments([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogError("PostController(GetComments) - Param: Id is empty.");
                throw new ArgumentException(nameof(id));
            }

            try
            {
                var listCommentsByPost = await _commentService.GetCommentsByPostId(id);

                if (listCommentsByPost is not null && listCommentsByPost.Count > 0)
                {
                    return Ok(listCommentsByPost);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"PostController(GetComments) - Exeption : {ex.Message}");
                return NotFound();
            }

            return BadRequest();
        }
    }
}