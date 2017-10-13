using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitmischief.Meridios.SmugMug.Entities.Request
{
    public class SmugMugResponsePages
    {
        public int Count { get; set; }
        public string FirstPage { get; set; }
        public string LastPage { get; set; }
        public string NextPage { get; set; }
        public int RequestedCount { get; set; }
        public int Start { get; set; }
        public int Total { get; set; }
    }
}
