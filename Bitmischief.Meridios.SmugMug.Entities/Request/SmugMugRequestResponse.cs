using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitmischief.Meridios.SmugMug.Entities.Request
{
    public class SmugMugRequestResponse
    {
        public string Uri { get; set; }
        public string Locator { get; set; }
        public string LocatorType { get; set; }
        public string UriDescription { get; set; }
        public string EndpointType { get; set; }
        public SmugMugResponsePages Pages { get; set; }

        public dynamic Album { get; set; }
        public dynamic User { get; set; }
        public dynamic Node { get; set; }
        public dynamic AlbumImage { get; set; }
    }
}
