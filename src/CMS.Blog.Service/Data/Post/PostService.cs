namespace CMS.Blog.Service.PostData
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;

        public PostService(IPostRepository repository,
                            IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public async Task<IReadOnlyCollection<PostDTO>> GetPosts()
        {
            var post = await _repository.GetAll();

            return _mapper.Map<IReadOnlyCollection<PostDTO>>(post);
        }

        public async Task<PostDTO> GetPost(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            var post = await _repository.Get(id);

            return _mapper.Map<PostDTO>(post);
        }

        public async Task<PostDTO> CreatePost(PostDTO record)
        {
            try
            {
                var newPost = _mapper.Map<Post>(record);

                await _repository.Create(newPost);

                return _mapper.Map<PostDTO>(newPost);
            }
            catch
            {
                throw;
            }
        }

        public async Task<PostDTO> UpdatePost(Guid? id, PostDTO record)
        {
            try
            {
                var idRecord = id ?? record.Id;

                var existRecord = await _repository.Get(idRecord.Value);

                if (existRecord is null)
                    throw new Exception("Record not found");

                _mapper.Map(record, existRecord);

                await _repository.Update(existRecord);

                return _mapper.Map<PostDTO>(existRecord);
            }
            catch
            {

                throw;
            }
        }

        public async Task<bool> DeletePost(Guid id)
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
    }
}
