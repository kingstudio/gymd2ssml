using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingstudio.Speach.Synthesis.Gymd2ssml
{
    public class Gymd2ssml
    {
        public Options Options { get; set; }


        public Gymd2ssml()
            : this(null)
        {
        }

        public Gymd2ssml(Options options)
        {
            Options = options ?? new Options();
        }


        public static string Parse(string src, Options options)
        {
            var ssmd2ssml = new Gymd2ssml(options);
            return ssmd2ssml.Parse(src);
        }

        public virtual string Parse(string src)
        {
            if (String.IsNullOrEmpty(src))
            {
                return src;
            }

            TokensResult tokensResult = Lexer.Lex(src, Options);
            var tokens = tokensResult;
            var result = Parser.Parse(tokens, Options);
            return result;
        }
    }
}
