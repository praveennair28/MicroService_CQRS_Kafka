
using Post.Query.Domain.Repositories;

namespace Post.Query.Domain.Repositories {

    public interface IPostRepository {
        Task CreateAsync(PostEntity post);
        Task UpdateAsync(PostEntity post);
        Task DeleteAsync(Guid postId);
        Task<PostEntity> GetByIdAsync(Guid postId);
        Task<List<PostEntity>> ListAllAsync(Guid postId);
        Task<List<PostEntity>> ListAllByAuthorAsync(string author);
        Task<List<PostEntity>> ListAllByLikesAsync(int numberOfLiokes);
        Task<List<PostEntity>> ListWithCommentAsync();
    }
}