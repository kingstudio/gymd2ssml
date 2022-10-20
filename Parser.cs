using mshtml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingstudio.Speach.Synthesis.Gymd2ssml
{
    public class Parser
    {
        private Options _options;
        private InlineLexer inline;
        private Stack<Token> tokens;
        private Token token;


        public Parser(Options options)
        {
            this.tokens = new Stack<Token>();
            this.token = null;
            _options = options ?? new Options();
        }


        /// <summary>
        /// Static Parse Method
        /// </summary>
        public static string Parse(TokensResult src, Options options)
        {
            var parser = new Parser(options);
            return parser.Parse(src);
        }

        /// <summary>
        /// Parse Loop
        /// </summary>
        public virtual string Parse(TokensResult src)
        {
            this.inline = new InlineLexer(this._options);
            this.tokens = new Stack<Token>(src.Reverse());

            var @out = String.Empty;
            while (Next() != null)
            {
                @out += Tok();
            }
            if (!_options.Text)
            {
                //最后加上speak元素
                if (_options.Prosody)
                {
                    @out = _options.Renderer.Prosody(@out, _options.Pitch, _options.Rate, _options.Volume, true);
                }
                if (_options.Voice)
                {
                    @out = _options.Renderer.Voice(_options.SsmlDefaultVoice, _options.SsmlLanguage, @out, true);
                }
                if (_options.Speak)
                {
                    @out = _options.Renderer.Speak(@out);
                }
            }

            return @out;
        }


        /// <summary>
        /// Next Token
        /// </summary>
        protected virtual Token Next()
        {
            return this.token = (this.tokens.Any()) ? this.tokens.Pop() : null;
        }


        /// <summary>
        /// Preview Next Token
        /// </summary>
        protected virtual Token Peek()
        {
            return this.tokens.Peek() ?? new Token();
        }


        /// <summary>
        /// Parse Text Tokens
        /// </summary>    
        protected virtual string ParseText()
        {
            var body = this.token.Text;

            while (this.Peek().Type == "text")
            {
                body += '\n' + this.Next().Text;
            }

            return this.inline.Output(body);
        }

        /// <summary>
        /// Parse Current Token
        /// </summary>    
        protected virtual string Tok()
        {
            switch (this.token.Type)
            {
                case "space":
                    {
                        return String.Empty;
                    }
                case "voice":
                    {
                        if (_options.Text)
                        {
                            return this.inline.Output(this.token.Text);
                        }
                        return _options.Renderer.Voice(this.token.Voice, this.token.Lang, this.inline.Output(this.token.Text), false, true);
                    }
                case "prosody":
                    {
                        if (_options.Text)
                        {
                            return this.inline.Output(this.token.Text);
                        }
                        return _options.Renderer.Prosody( this.inline.Output(this.token.Text), this.token.Pitch, this.token.Rate, this.token.Volume, false, false);
                    }
                case "question":
                    {
                        if (_options.Text)
                        {
                            return this.token.Text;
                        }
                        return _options.Renderer.Question(this.token.Text);
                    }
                case "answer":
                    {
                        if (_options.Text)
                        {
                            return this.token.Text;
                        }
                        return _options.Renderer.Answer(this.token.Text);
                    }
                case "comment":
                    {
                        if (_options.Text)
                        {
                            return this.token.Text;
                        }
                        return _options.Renderer.Comment(StringHelper.Escape(this.token.Text));
                    }
                case "comments":
                    {
                        if (_options.Text)
                        {
                            return this.token.Text;
                        }
                        return _options.Renderer.Comment(StringHelper.Escape(this.token.Text), true, true);
                    }
                case "paragraph":
                    {
                        return _options.Renderer.Paragraph(this.inline.Output(this.token.Text), _options.Breaks);
                    }
                case "text":
                    {
                        return _options.Renderer.Paragraph(this.ParseText(), _options.Breaks);
                    }
            }

            throw new Exception();
        }
    }
}
