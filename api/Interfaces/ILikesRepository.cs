using api.DTOs;
using api.Entities;
using api.Helpers;

namespace api.Interfaces;

public interface ILikesRepository
{
    Task<UserLike> GetLike(int sourceUserId, int targetUserId);
    Task<User> GetUserWithLikes(int userId);
    Task<PagedList<LikeDto>> GetLikes(LikesParams likesParams);
}