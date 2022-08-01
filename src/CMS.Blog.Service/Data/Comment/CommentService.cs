namespace CMS.Blog.Service.CommentData
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _repository;
        private readonly IPostRepository _repositoryPost;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository repository,
                            IPostRepository repositoryPost,
                            IMapper mapper)
        {
            _repository = repository;
            _repositoryPost = repositoryPost;
            _mapper = mapper;
        }

        public async Task<IReadOnlyCollection<CommentDTO>> GetComments()
        {
            var post = await _repository.GetAll();

            return _mapper.Map<IReadOnlyCollection<CommentDTO>>(post);
        }

        public async Task<CommentDTO> GetComment(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            var post = await _repository.Get(id);

            return _mapper.Map<CommentDTO>(post);
        }

        public async Task<CommentDTO> CreateComment(CommentDTO record)
        {
            try
            {
                if (record.PostId == Guid.Empty)
                    throw new ArgumentNullException(nameof(record.PostId));

                var postExist = await _repositoryPost.Exist(record.PostId);

                if (!postExist)
                    throw new ArgumentNullException(nameof(record.PostId), "Post does't exist in Comment.");

                var newComment = _mapper.Map<Comment>(record);

                await _repository.Create(newComment);

                return _mapper.Map<CommentDTO>(newComment);
            }
            catch
            {
                throw;
            }
        }

        public async Task<CommentDTO> UpdateComment(Guid? id, CommentDTO record)
        {
            try
            {
                var idRecord = id ?? record.Id;

                var existRecord = await _repository.Get(idRecord.Value);

                if (existRecord is null)
                    throw new Exception("Record not found");

                _mapper.Map(record, existRecord);

                await _repository.Update(existRecord);

                return _mapper.Map<CommentDTO>(existRecord);
            }
            catch
            {

                throw;
            }
        }

        public async Task<bool> DeleteComment(Guid id)
        {
            try
            {
                var existRecord = await _repository.Get(id);

                if (existRecord is null)
                {
                    throw new ArgumentNullException(nameof(existRecord));
                }

                await _repository.Delete(existRecord);

            }
            catch
            {
                throw;
            }

            return true;
        }

        public async Task<ICollection<Comment>> GetCommentsByPostId(Guid postId)
        {
            return await _repository.GetCommentsByPostId(postId);
        }
    }
}
