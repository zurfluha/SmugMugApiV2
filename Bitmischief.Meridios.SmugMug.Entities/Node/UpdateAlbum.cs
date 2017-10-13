using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitmischief.Meridios.SmugMug.Entities.Node
{
    public class UpdateAlbum
    {
        [JsonProperty("UploadKey")]
        public string NewUploadKey { get; set; }
    }
}
