using XiaodiYan.FirstApiV8.Models;

namespace XiaodiYan.FirstApiV8.Services;

public interface IPostService
{
    Task<List<Post>> GetPostsAsync();

    Task<Post?> GetPostAsync(int id);

    Task CreatePostAsync(Post item);

    Task<Post?> UpdatePostAsync(int id, Post item);

    Task<bool> DeletePostAsync(int id);
}