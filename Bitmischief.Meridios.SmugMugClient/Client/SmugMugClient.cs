using Bitmischief.Meridios.SmugMug.Constants;
using Bitmischief.Meridios.SmugMug.Entities.Album;
using Bitmischief.Meridios.SmugMug.Entities.Authentication;
using Bitmischief.Meridios.SmugMug.Entities.Enums;
using Bitmischief.Meridios.SmugMug.Entities.Image;
using Bitmischief.Meridios.SmugMug.Entities.Node;
using Bitmischief.Meridios.SmugMug.Entities.User;
using Bitmischief.Meridios.SmugMug.Interfaces;
using Bitmischief.Meridios.SmugMug.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bitmischief.Meridios.SmugMug.Client
{
    public class SmugMugClient : ISmugMugClient
    {
        private OAuthToken oauthToken;
        private SmugMugApiService apiService;

        public SmugMugClient(OAuthToken _token)
        {
            oauthToken = _token;
            apiService = new SmugMugApiService(oauthToken);
        }

        public async Task<UserEntity> GetAuthenticatedUserAsync()
        {
            var requestUri = $"{SmugMugConstants.Addresses.SmugMugApi}!authuser";
            return await apiService.GetRequestAsync<UserEntity>(requestUri);
        }

        public async Task CreateNodeAsync(CreateNode node)
        {
            var uri = string.Empty;
            if(node.UseRootNode)
            {
                uri = await GetCurrentUserRootUriAsync();
            }
            else
            {
                uri = $"{SmugMugConstants.Addresses.SmugMug}{node.NodeUri}";
            }

            var requestUri = uri + "!children";
            await apiService.PostRequestAsync(requestUri, node);
        }

        public async Task<string> GetCurrentUserRootUriAsync()
        {
            var authUser = await GetAuthenticatedUserAsync();

            return $"{SmugMugConstants.Addresses.SmugMug}{authUser.Uris["Node"].Id}";
        }
        
        public async Task<NodeEntity> GetRootNodeAsync()
        {
            var requestUri = await GetCurrentUserRootUriAsync();
            return await apiService.GetRequestAsync<NodeEntity>(requestUri);
        }

        public async Task<IEnumerable<NodeEntity>> GetNodeChildrenAsync(NodeEntity parentNode)
        {
            var requestUri = $"{SmugMugConstants.Addresses.SmugMug}{parentNode.Uri}!children";
            return await apiService.GetRequestAsync<IEnumerable<NodeEntity>>(requestUri);
        }
        
        public async Task<IEnumerable<ImageEntity>> GetNodeImagesAsync(NodeEntity node)
        {
            if(node.Type != NodeType.Album)
            {
                throw new InvalidOperationException("This node is not an album!");
            }

            var albumUri = node.Uris["Album"].Id;

            var requestUri = $"{SmugMugConstants.Addresses.SmugMug}{albumUri}!images";
            return await apiService.GetRequestAsync<IEnumerable<ImageEntity>>(requestUri);
        }

        public async Task<bool> DeleteImageAsync(ImageEntity image)
        {
            var requestUri = $"{SmugMugConstants.Addresses.SmugMug}{image.Uri}";
            await apiService.DeleteRequestAsync(requestUri);

            return true;
        }

        /// <summary>
        /// Update the passed in album entity by passing in an anonymous object with related values. This is implemented
        /// using an anonymous object because the PATCH verb only expects values for the things you want to update. IE:
        /// if I only want to update the keywords array obj, so I only set the keywords array property 
        /// and pass this: { "UploadKey": "test" }
        /// </summary>
        /// <param name="node"></param>
        /// <param name="updateAlbumObject">An anonymous object with the settings to be updated. Only specify the fields you wish to update.</param>
        /// <returns></returns>
        public async Task<bool> UpdateAlbumAsync(NodeEntity node, object updateAlbumObject)
        {
            var requestUri = $"{SmugMugConstants.Addresses.SmugMug}{node.Uris["Album"].Id}";
            await apiService.PatchRequestAsync(requestUri, updateAlbumObject);

            return true;
        }

        /// <summary>
        /// Update the passed in image entity by passing in an anonymous object with related values. This is implemented
        /// using an anonymous object because the PATCH verb only expects values for the things you want to update. IE:
        /// if I only want to update the keywords array obj, so I only set the keywords array property 
        /// and pass this: { "KeywordsArray": ["my", "test", "patch"], "ImageId": null" }
        /// SmugMug will attempt to update the keywords array as expected, but then will also try to set the ImageId to null, 
        /// which will fail.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="updateImageObject">An anonymous object with the settings to be updated. Only specify the fields you wish to update.</param>
        /// <returns></returns>
        public async Task<bool> UpdateImageAsync(ImageEntity image, object updateImageObject)
        {
            var requestUri = $"{SmugMugConstants.Addresses.SmugMug}{image.Uris["Image"].Id}";
            await apiService.PatchRequestAsync(requestUri, updateImageObject);

            return true;
        }

        public async Task<AlbumEntity> GetAlbumForNodeAsync(NodeEntity node)
        {
            if (node.Type != NodeType.Album)
            {
                throw new InvalidOperationException("This node is not an album!");
            }

            var albumUri = node.Uris["Album"].Id;

            var requestUri = $"{SmugMugConstants.Addresses.SmugMug}{albumUri}";
            return await apiService.GetRequestAsync<AlbumEntity>(requestUri);
        }
    }
}
