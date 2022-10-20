using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingstudio.Speach.Synthesis.Gymd2ssml
{
    public class Token
    {
        public string Text { get; set; }
        public string Type { get; set; }
        public string Level { get; set; }
        public string Strength { get; set; }
        public string Pitch { get; set; }
        public string Rate { get; set; }
        public string Volume { get; set; }
        public string Format { get; set; }
        public string Alias { get; set; }
        public string Voice { get; set; }


        public int Depth { get; set; }
        public bool Escaped { get; set; }
        public string Lang { get; set; }
        public bool Ordered { get; set; }

        public bool Pre { get; set; }

        public IList<string> Header { get; set; }
        public IList<string> Align { get; set; }
        public IList<IList<string>> Cells { get; set; }
    }
}
