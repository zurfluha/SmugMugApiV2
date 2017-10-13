using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bitmischief.Meridios.SmugMug.Entities.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum NodeType
    {
        [EnumMember(Value = "System Album")]
        SystemAlbum,
        [EnumMember(Value = "Album")]
        Album,
        [EnumMember(Value = "Folder")]
        Folder,
        [EnumMember(Value = "Page")]
        Page
    };
}
