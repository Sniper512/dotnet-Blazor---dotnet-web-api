using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Web_Api.Data;
using Web_Api.Models;

namespace Web_Api.Repositories
{
    public interface ICommentRepository
    {
        Task<bool> AddCommentAsync(Comment item);
        Task<List<Comment>> GetAllCommentsForAPostAsync(int postId);
        Task<bool> CommentLikeAsync(int id);
        Task<bool> UpdateCommentAsync(Comment item);
        Task<bool> DeleteCommentAsync(int id);
        Task<int> GetCommentsCountAsync();
    }

    public class CommentRepository : ICommentRepository
    {
        private readonly MyDBContext _db;
        private readonly ILogger<CommentRepository> _logger;

        public CommentRepository(MyDBContext db, ILogger<CommentRepository> logger)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> AddCommentAsync(Comment item)
        {
            if (item == null)
            {
                _logger.LogWarning("Attempt to add null comment");
                return false;
            }

            try
            {
                var post = await _db.Posts.FindAsync(item.PostId);
                if (post == null)
                {
                    _logger.LogWarning("Attempt to add comment to non-existent post with ID {PostId}", item.PostId);
                    return false;
                }

                post.Comments.Add(item);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding comment");
                return false;
            }
        }

        public async Task<List<Comment>> GetAllCommentsForAPostAsync(int postId)
        {
            try
            {
                var comments = await _db.Comments
                    .Where(x => x.PostId == postId)
                    .ToListAsync();

                if (comments.Count == 0)
                {
                    _logger.LogInformation("No comments found for post with ID {PostId}", postId);
                }

                return comments;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching comments for post with ID {PostId}", postId);
                return new List<Comment>();
            }
        }

        public async Task<bool> CommentLikeAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid comment ID {Id} provided for like operation", id);
                return false;
            }

            try
            {
                var comment = await _db.Comments.FindAsync(id);
                if (comment == null)
                {
                    _logger.LogWarning("Comment with ID {Id} not found for like operation", id);
                    return false;
                }

                comment.Likes++;
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error liking comment with ID {Id}", id);
                return false;
            }
        }

        public async Task<bool> UpdateCommentAsync(Comment item)
        {
            if (item == null || item.Id <= 0)
            {
                _logger.LogWarning("Invalid comment provided for update");
                return false;
            }

            try
            {
                var existingComment = await _db.Comments.FindAsync(item.Id);
                if (existingComment == null)
                {
                    _logger.LogWarning("Comment with ID {Id} not found for update", item.Id);
                    return false;
                }

                existingComment.Content = item.Content;
                existingComment.CreatedDate = item.CreatedDate;

                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating comment with ID {Id}", item.Id);
                return false;
            }
        }

        public async Task<bool> DeleteCommentAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid comment ID {Id} provided for deletion", id);
                return false;
            }

            try
            {
                var comment = await _db.Comments.FindAsync(id);
                if (comment == null)
                {
                    _logger.LogWarning("Comment with ID {Id} not found for deletion", id);
                    return false;
                }

                _db.Comments.Remove(comment);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting comment with ID {Id}", id);
                return false;
            }
        }

        public async Task<int> GetCommentsCountAsync()
        {
            try
            {
                return await _db.Comments.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching comment count");
                return 0;
            }
        }
    }
}