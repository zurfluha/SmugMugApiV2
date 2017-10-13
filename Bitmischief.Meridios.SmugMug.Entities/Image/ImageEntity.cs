using Bitmischief.Meridios.SmugMug.Entities.Uri;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitmischief.Meridios.SmugMug.Entities.Image
{
    public class ImageEntity
    {
        public string Uri { get; set; }
        public string UriDescription { get; set; }
        public int Altitude { get; set; }
        public string ArchivedMD5 { get; set; }
        public int ArchivedSize { get; set; }
        public string ArchivedUri { get; set; }
        public bool CanBuy { get; set; }
        public bool CanEdit { get; set; }
        public string Caption { get; set; }
        public bool Collectable { get; set; }
        public DateTime Date { get; set; }
        public string FileName { get; set; }
        public string Format { get; set; }
        public bool Hidden { get; set; }
        public string ImageKey { get; set; }
        public bool IsArchive { get; set; }
        public bool IsVideo { get; set; }
        public IEnumerable<string> KeywordArray { get; set; }
        public string Keywords { get; set; }
        public DateTime LastUpdated { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int OriginalHeight { get; set; }
        public int OriginalSize { get; set; }
        public int OriginalWidth { get; set; }
        public bool Processing { get; set; }
        public bool Protected { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Title { get; set; }
        public string UploadKey { get; set; }
        public string Watermark { get; set; }
        public string WebUri { get; set; }
        public Dictionary<string, UriEntity> Uris { get; set; }
    }
}
