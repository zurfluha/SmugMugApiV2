using Bitmischief.Meridios.SmugMug.Entities.Enums;
using Bitmischief.Meridios.SmugMug.Entities.Uri;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitmischief.Meridios.SmugMug.Entities.Node
{
    public class NodeEntity
    {
        public string Uri { get; set; }
        public string UriDescription { get; set; }
        public string ResponseLevel { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
        public string Description { get; set; }
        public string EffectiveSecurityType { get; set; }
        public bool HasChildren { get; set; }
        public bool IsRoot { get; set; }
        public List<string> Keywords { get; set; }
        public string Name { get; set; }
        public string NodeID { get; set; }
        public string PasswordHint { get; set; }
        public string SecurityType { get; set; }
        public bool ShowCoverImage { get; set; }
        public string SortDirection { get; set; }
        public string SortMethod { get; set; }
        public NodeType Type { get; set; }
        public string UrlName { get; set; }
        public string UrlPath { get; set; }
        public string WebUri { get; set; }
        public Dictionary<string, UriEntity> Uris { get; set; }
    }
}
