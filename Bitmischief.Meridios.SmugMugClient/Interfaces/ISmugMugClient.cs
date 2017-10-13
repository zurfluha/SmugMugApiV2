using Bitmischief.Meridios.SmugMug.Entities.Album;
using Bitmischief.Meridios.SmugMug.Entities.Image;
using Bitmischief.Meridios.SmugMug.Entities.Node;
using Bitmischief.Meridios.SmugMug.Entities.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bitmischief.Meridios.SmugMug.Interfaces
{
    public interface ISmugMugClient
    {
        Task<string> GetCurrentUserRootUriAsync();
        Task<UserEntity> GetAuthenticatedUserAsync();
        Task CreateNodeAsync(CreateNode node);
        Task<NodeEntity> GetRootNodeAsync();
        Task<IEnumerable<NodeEntity>> GetNodeChildrenAsync(NodeEntity parentNode);
        Task<IEnumerable<ImageEntity>> GetNodeImagesAsync(NodeEntity node);
        Task<bool> DeleteImageAsync(ImageEntity image);
        Task<bool> UpdateAlbumAsync(NodeEntity node, object updateAlbumObject);
        Task<bool> UpdateImageAsync(ImageEntity image, object obj);
        Task<AlbumEntity> GetAlbumForNodeAsync(NodeEntity node);
    }
}
