using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bitmischief.Meridios.SmugMug.Interfaces;
using Bitmischief.Meridios.SmugMug.Client;
using System.Threading.Tasks;
using Bitmischief.Meridios.SmugMug.Entities.Authentication;
using Bitmischief.Meridios.SmugMug.Entities.Node;
using System;
using Bitmischief.Meridios.SmugMug.Entities.Enums;
using System.Linq;
using System.Collections.Generic;
using Bitmischief.Meridios.SmugMug.Entities.Image;

namespace Bitmischief.Meridios.SmugMug.Tests
{
    [TestClass]
    public class SmugMugClientTests
    {
        private ISmugMugClient client;
        private const string API_KEY = "PKGjpnQ77H5KsN8vh3kMfX2HG7kTP8m5";
        private const string API_SECRET = "72CQQhZ7NtZMMFw4pxtqgzswGCN4z5L38LQ4fmMMzd7VL3hdrC6x5zTZvwtRBnNJ";
        private const string OAUTH_TOKEN = "pVCjVZFh4RS6TrvxdrWPdLkR72t5snZ9";
        private const string OAUTH_SECRET = "PCG6Z9HfJFGP5BQxKJFjHNrgCzNKtJmNVnHjtQdtJmnGxz4p7ktvPG5vZ2hJh8XD";

        private NodeEntity brendaHoffmanFolder;
        private IEnumerable<NodeEntity> Bethany5kNodes;

        [TestInitialize]
        public async Task Initialize()
        {
            client = new SmugMugClient(new OAuthToken(API_KEY, API_SECRET, OAUTH_TOKEN, OAUTH_SECRET));

            var rootNode = await client.GetRootNodeAsync();
            var childrenNodes = await client.GetNodeChildrenAsync(rootNode);
            brendaHoffmanFolder = childrenNodes.FirstOrDefault(n => n.NodeID == "mVVhcs");
            Bethany5kNodes = await client.GetNodeChildrenAsync(brendaHoffmanFolder);
        }

        private static string GenerateRandomId()
        {
            return Guid.NewGuid().ToString();
        }

        [TestMethod]
        public async Task TestCreateNodeAsync_CreateAlbum()
        {
            var random = GenerateRandomId();

            await client.CreateNodeAsync(new CreateNode
            {
                Type = NodeType.Album,
                Description = "Test album",
                Name = $"Test Album {random}",
                Privacy = "Public",
                UrlName = $"Testalbum{random}"
            });
        }

        [TestMethod]
        public async Task TestCreateNodeAsync_CreateAlbumInSubfolder()
        {
            var random = GenerateRandomId();

            var rootNode = await client.GetRootNodeAsync();
            var rootChildren = await client.GetNodeChildrenAsync(rootNode);
            var childFolder = rootChildren
                                        .Where(c => c.Type == NodeType.Folder)
                                        .Last();

            await client.CreateNodeAsync(new CreateNode
            {
                Type = NodeType.Album,
                Description = "Test album",
                Name = $"Test Album {random}",
                Privacy = "Public",
                UrlName = $"Testalbum{random}",
                UseRootNode = false,
                NodeUri = childFolder.Uri
            });
        }

        [TestMethod]
        public async Task TestCreateNodeAsync_CreateFolder()
        {
            var random = GenerateRandomId();

            await client.CreateNodeAsync(new CreateNode
            {
                Type = NodeType.Folder,
                Description = "Test folder",
                Name = $"Test Folder {random}",
                Privacy = "Public",
                UrlName = $"Testfolder{random}",
            });
        }

        [TestMethod]
        public async Task TestGetRootNodeAsync()
        {
            var node = await client.GetRootNodeAsync();
            Assert.IsTrue(node != null);
        }

        [TestMethod]
        public async Task TestGetNodeChildrenAsync()
        {
            var rootNode = await client.GetRootNodeAsync();
            var childrenNodes = await client.GetNodeChildrenAsync(rootNode);
            Assert.IsTrue(childrenNodes.Any());
        }

        [TestMethod]
        public async Task TestGetAlbumAndThenImageAsync()
        {
            var rootNode = await client.GetRootNodeAsync();
            var childrenNodes = await client.GetNodeChildrenAsync(rootNode);

            var brendaHoffmanNodeId = "Dm8wpc";
            var brendaHoffmanFolder = childrenNodes.FirstOrDefault(n => n.NodeID == brendaHoffmanNodeId);

            var brendaHoffmanChildNodes = await client.GetNodeChildrenAsync(brendaHoffmanFolder);
            var brendaHoffmanAlbums = brendaHoffmanChildNodes.Where(n => n.Type == NodeType.Album);

            var albumIdImLookingFor = "6mx2NT";
            var myAlbum = brendaHoffmanAlbums.FirstOrDefault(n => n.NodeID == albumIdImLookingFor);

            var images = await client.GetNodeImagesAsync(myAlbum);

            var myImage = images.FirstOrDefault(i => i.ImageKey == "BJBWKLg");

            Assert.IsTrue(myImage != null);
        }

        [TestMethod]
        public async Task TestGetNodeImagesAsync()
        {
            var images = await client.GetNodeImagesAsync(Bethany5kNodes.First());
            Assert.IsTrue(images != null);
        }

        [TestMethod]
        public async Task TestDeleteImageAsync()
        {
            var images = await client.GetNodeImagesAsync(Bethany5kNodes.First());
            var lastImageInAlbum = images.Last();

            await client.DeleteImageAsync(lastImageInAlbum);

            Assert.IsTrue(images != null);
        }

        [TestMethod]
        public async Task TestUpdateAlbumAsync()
        {
            var result = await client.UpdateAlbumAsync(Bethany5kNodes.First(), new {
                UploadKey = "austin"
            });

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task TestUpdateImageAsync_UpdatingKeywordArray()
        {
            var images = await client.GetNodeImagesAsync(Bethany5kNodes.First());
            var lastImageInAlbum = images.Last();

            await client.UpdateImageAsync(lastImageInAlbum, new { KeywordArray = new string[] { "my", "test", "keywords" } });

            Assert.IsTrue(images != null);
        }

        [TestMethod]
        public async Task TestGetAlbumForNodeAsync()
        {
            var album = await client.GetAlbumForNodeAsync(Bethany5kNodes.First());
            Assert.IsTrue(!string.IsNullOrEmpty(album.AlbumKey));
        }
    }
}
