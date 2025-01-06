using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Web_Api.Data;
using Web_Api.Models;

namespace Web_Api.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly MyDBContext _db;
        private readonly ILogger<PostRepository> _logger;

        public PostRepository(MyDBContext db, ILogger<PostRepository> logger)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> AddItemAsync(Post item)
        {
            if (item == null)
            {
                _logger.LogWarning("Attempt to add null post");
                return false;
            }

            try
            {
                await _db.Posts.AddAsync(item);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding post");
                return false;
            }
        }

        public async Task<List<Post>> GetAllItemsAsync()
        {
            try
            {
                return await _db.Posts.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all posts");
                return new List<Post>();
            }
        }

        public async Task<Post> GetItemByIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid ID {Id} provided", id);
                return null;
            }

            try
            {
                return await _db.Posts.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching post with ID {Id}", id);
                return null;
            }
        }

        public async Task<bool> UpdateItemAsync(Post item)
        {

            if (item == null || item.Id <= 0)
            {
                _logger.LogWarning("Invalid post provided for update");
                return false;
            }

            try
            {
                _db.Posts.Update(item);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating post with ID {Id}", item.Id);
                return false;
            }
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid ID {Id} provided for deletion", id);
                return false;
            }

            try
            {
                var item = await _db.Posts.FindAsync(id);
                if (item == null)
                {
                    _logger.LogWarning("Post with ID {Id} not found for deletion", id);
                    return false;
                }

                _db.Posts.Remove(item);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting post with ID {Id}", id);
                return false;
            }
        }

        public async Task<int> GetCountAsync()
        {
            try
            {
                return await _db.Posts.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching post count");
                return 0;
            }
        }
    }

    public interface IPostRepository
    {
        Task<bool> AddItemAsync(Post item);
        Task<List<Post>> GetAllItemsAsync();
        Task<Post> GetItemByIdAsync(int id);
        Task<bool> UpdateItemAsync(Post item);
        Task<bool> DeleteItemAsync(int id);
        Task<int> GetCountAsync();
    }
}