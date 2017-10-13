using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitmischief.Meridios.SmugMug.Entities.Album
{
    public class AlbumEntity
    {
        public string Uri { get; set; }
        public string UriDescription { get; set; }
        public string ResponseLevel { get; set; }
        public string AlbumKey { get; set; }
        public bool AllowDownloads { get; set; }
        public bool CanShare { get; set; }
        public string Description { get; set; }
        public bool External { get; set; }
        public bool HasDownloadPassword { get; set; }
        public int ImageCount { get; set; }
        public DateTime ImagesLastUpdated { get; set; }
        public IEnumerable<string> Keywords { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Name { get; set; }
        public string NiceName { get; set; }
        public string NodeID { get; set; }
        public string PasswordHint { get; set; }
        public bool Protected { get; set; }
        public string SecurityType { get; set; }
        public string SortDirection { get; set; }
        public string SortMethod { get; set; }
        public string Title { get; set; }
        public string UrlName { get; set; }
        public string UrlPath { get; set; }
        public string WebUri { get; set; }
    }
}
