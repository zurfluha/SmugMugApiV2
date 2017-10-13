using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitmischief.Meridios.SmugMug.Entities.Uri
{
    public class UriEntity
    {
        [JsonProperty("Uri")]
        public string Id { get; set; }
        public string Locator { get; set; }
        public string LocatorType { get; set; }
        public string UriDescription { get; set; }
        public string EndpointType { get; set; }
    }
}
