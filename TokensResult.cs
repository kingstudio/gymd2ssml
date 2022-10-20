using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingstudio.Speach.Synthesis.Gymd2ssml
{
    public class TokensResult
    {
        public IList<Token> Tokens { get; set; }
        public int Length { get { return Tokens.Count; } }

        public IEnumerable<Token> Reverse()
        {
            return Tokens.Reverse();
        }

        public TokensResult()
        {
            Tokens = new List<Token>();
        }


        public void Add(Token token)
        {
            Tokens.Add(token);
        }
    }
}
