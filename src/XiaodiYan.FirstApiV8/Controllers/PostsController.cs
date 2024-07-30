using Microsoft.AspNetCore.Mvc;
using XiaodiYan.FirstApiV8.Models;
using XiaodiYan.FirstApiV8.Services;

namespace XiaodiYan.FirstApiV8;

[Route("api/[controller]")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly IPostService _postService;

    public PostsController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Post>>> GetPosts()
    {
        var posts = await _postService.GetPostsAsync();
        return Ok(posts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Post>> GetPost(int id)
    {
        var post = await _postService.GetPostAsync(id);

        return post == null ? NotFound() : Ok(post);
    }

    [HttpPost]
    public async Task<ActionResult<Post>> CreatePost(Post post)
    {
        await _postService.CreatePostAsync(post);

        return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdatePost(int id, Post post)
    {
        if (id != post.Id) return BadRequest();

        var updatedPost = await _postService.UpdatePostAsync(id, post);

        return updatedPost == null ? NotFound() : Ok(updatedPost);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePost(int id)
    {
        return await _postService.DeletePostAsync(id) ? NoContent() : NotFound();
    }
}
