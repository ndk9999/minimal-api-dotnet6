using XiaodiYan.FirstApiV8.Models;

namespace XiaodiYan.FirstApiV8.Services;

public class PostService : IPostService
{
    private static readonly List<Post> _posts = new();

    public Task<List<Post>> GetPostsAsync()
    {
        return Task.FromResult(_posts);
    }

    public Task<Post?> GetPostAsync(int id)
    {
        return Task.FromResult(_posts.FirstOrDefault(x => x.Id == id));
    }

    public Task CreatePostAsync(Post item)
    {
        _posts.Add(item);
        return Task.CompletedTask;
    }

    public Task<Post?> UpdatePostAsync(int id, Post item)
    {
        var post = _posts.FirstOrDefault(x => x.Id == id);

        if (post != null)
        {
            post.Title = item.Title;
            post.Body = item.Body;
            post.UserId = item.UserId;
        }

        return Task.FromResult(post);
    }

    public Task<bool> DeletePostAsync(int id)
    {
        var post = _posts.FirstOrDefault(x => x.Id == id);

        if (post != null)
        {
            _posts.Remove(post);
        }

        return Task.FromResult(post != null);
    }
}