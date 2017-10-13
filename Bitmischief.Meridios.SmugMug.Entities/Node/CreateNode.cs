using Bitmischief.Meridios.SmugMug.Entities.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitmischief.Meridios.SmugMug.Entities.Node
{
    public class CreateNode
    {
        public string Name { get; set; }
        public string UrlName { get; set; }
        public string Privacy { get; set; }
        public string Description { get; set; }
        public bool UseRootNode { get; set; } = true;
        public string NodeUri { get; set; }

        // Type
        [JsonIgnore]
        public NodeType Type { get; set; }


        [JsonProperty("Type")]
        public string TypeString
        {
            get
            {
                return Type.ToString();
            }
        }
    }
}
