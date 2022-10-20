using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Kingstudio.Speach.Synthesis.Gymd2ssml
{
    /// <summary>
    /// Inline Lexer and Compiler
    /// </summary>
    public class InlineLexer
    {
        private Random _random = new Random();

        private Options _options;
        private InlineRules _rules;

        public InlineLexer(Options options)
        {
            _options = options ?? new Options();
            this._rules = new NormalInlineRules();
        }

        /// <summary>
        /// Lexing/Compiling
        /// </summary>
        public virtual string Output(string src)
        {
            var @out = String.Empty;
            IList<string> cap;

            while (!String.IsNullOrEmpty(src))
            {
                // escape
                cap = this._rules.Escape.Exec(src);
                if (cap.Any())
                {
                    src = src.Substring(cap[0].Length);
                    @out += cap[1];
                    continue;
                }

                // emphasis
                cap = this._rules.Emphasis.Exec(src);
                if (cap.Any())
                {
                    src = src.Substring(cap[0].Length);

                    if (_options.Text)
                    {
                        @out += cap[1];
                    }
                    else
                    {
                        @out += _options.Renderer.Emphasis(cap[2], cap[1]);
                    }
                    continue;
                }

                // sub
                cap = this._rules.Sub.Exec(src);
                if (cap.Any())
                {
                    src = src.Substring(cap[0].Length);

                    if (_options.Text)
                    {
                        @out += cap[1];
                    }
                    else
                    {
                        @out += _options.Renderer.Sub(cap[2], cap[1]);
                    }
                    continue;
                }

                // break
                cap = this._rules.Break.Exec(src);
                if (cap.Any())
                {
                    src = src.Substring(cap[0].Length);

                    if (_options.Text)
                    {
                        //不需要返回任何字
                        @out += string.Empty;
                    }
                    else
                    {
                        @out += _options.Renderer.Break(StringHelper.Escape(cap[1]));
                    }
                    continue;
                }

                // audio
                cap = this._rules.Audio.Exec(src);
                if (cap.Any())
                {
                    src = src.Substring(cap[0].Length);

                    if (_options.Text)
                    {
                        //不需要返回任何字
                        @out += string.Empty;
                    }
                    else
                    {
                        @out += _options.Renderer.Audio(cap[2], StringHelper.Escape(cap[1]));
                    }
                    continue;
                }

                // image
                cap = this._rules.Image.Exec(src);
                if (cap.Any())
                {
                    src = src.Substring(cap[0].Length);

                    if (_options.Text)
                    {
                        //不需要返回任何字
                        @out += string.Empty;
                    }
                    else
                    {
                        @out += _options.Renderer.Image(cap[2], StringHelper.Escape(cap[1]));
                    }
                    continue;
                }

                // repeat
                cap = this._rules.Repeat.Exec(src);
                if (cap.Any())
                {
                    src = src.Substring(cap[0].Length);

                    if (_options.Text)
                    {
                        @out += cap[1];
                    }
                    else
                    {
                        @out += _options.Renderer.Repeat(cap[2], cap[3], StringHelper.Escape(cap[1]));
                    }
                    continue;
                }

                // note singleline
                cap = this._rules.SinglelineComment.Exec(src);
                if (cap.Any())
                {
                    src = src.Substring(cap[0].Length);

                    if (_options.Text)
                    {
                        @out += cap[1];
                    }
                    else
                    {
                        @out += _options.Renderer.Comment(StringHelper.Escape(cap[1]));
                    }
                    continue;
                }

                // note multiline
                cap = this._rules.MultilineComment.Exec(src);
                if (cap.Any())
                {
                    src = src.Substring(cap[0].Length);

                    if (_options.Text)
                    {
                        @out += cap[1];
                    }
                    else
                    {
                        @out += _options.Renderer.Comment(StringHelper.Escape(cap[1]), true, true);
                    }
                    continue;
                }

                // voice
                cap = this._rules.Voice.Exec(src);
                if (cap.Any())
                {
                    src = src.Substring(cap[0].Length);

                    if (_options.Text)
                    {
                        @out += cap[1];
                    }
                    else
                    {
                        @out += _options.Renderer.Voice(cap[2], cap[3], StringHelper.Escape(cap[1]), false);
                    }
                    continue;
                }

                // cardinal or number
                cap = this._rules.Cardinal.Exec(src);
                if (cap.Any())
                {
                    src = src.Substring(cap[0].Length);

                    if (_options.Text)
                    {
                        @out += cap[1];
                    }
                    else
                    {
                        // 都是cardinal
                        @out += _options.Renderer.Sayas(StringHelper.Escape(cap[1]), "cardinal");
                    }
                    continue;
                }

                // ordinal
                cap = this._rules.Ordinal.Exec(src);
                if (cap.Any())
                {
                    src = src.Substring(cap[0].Length);

                    if (_options.Text)
                    {
                        @out += cap[1];
                    }
                    else
                    {
                        @out += _options.Renderer.Sayas(StringHelper.Escape(cap[1]), cap[2]);
                    }
                    continue;
                }

                // characters
                cap = this._rules.Characters.Exec(src);
                if (cap.Any())
                {
                    src = src.Substring(cap[0].Length);

                    if (_options.Text)
                    {
                        @out += cap[1];
                    }
                    else
                    {
                        @out += _options.Renderer.Sayas(StringHelper.Escape(cap[1]), cap[2]);
                    }
                    continue;
                }

                // date
                cap = this._rules.Date.Exec(src);
                if (cap.Any())
                {
                    src = src.Substring(cap[0].Length);

                    if (_options.Text)
                    {
                        @out += cap[1];
                    }
                    else
                    {
                        @out += _options.Renderer.Date(StringHelper.Escape(cap[1]), "date", cap[2]);
                    }
                    continue;
                }

                // telephone
                cap = this._rules.Telephone.Exec(src);
                if (cap.Any())
                {
                    src = src.Substring(cap[0].Length);

                    if (_options.Text)
                    {
                        @out += cap[1];
                    }
                    else
                    {
                        @out += _options.Renderer.Telephone(StringHelper.Escape(cap[1]), "telephone", cap[3]);
                    }
                    continue;
                }

                // time
                cap = this._rules.Time.Exec(src);
                if (cap.Any())
                {
                    src = src.Substring(cap[0].Length);

                    if (_options.Text)
                    {
                        @out += cap[1];
                    }
                    else
                    {
                        @out += _options.Renderer.Time(StringHelper.Escape(cap[1]), "time", cap[2]);
                    }
                    continue;
                }

                // pitch
                cap = this._rules.Pitch.Exec(src);
                if (cap.Any())
                {
                    src = src.Substring(cap[0].Length);

                    if (_options.Text)
                    {
                        @out += cap[1];
                    }
                    else
                    {
                        @out += _options.Renderer.Pitch(StringHelper.Escape(cap[1]), cap[2], false);
                    }
                    continue;
                }

                // volume
                cap = this._rules.Volume.Exec(src);
                if (cap.Any())
                {
                    src = src.Substring(cap[0].Length);

                    if (_options.Text)
                    {
                        @out += cap[1];
                    }
                    else
                    {
                        @out += _options.Renderer.Volume(StringHelper.Escape(cap[1]), cap[2], false);
                    }
                    continue;
                }

                // rate
                cap = this._rules.Rate.Exec(src);
                if (cap.Any())
                {
                    src = src.Substring(cap[0].Length);

                    if (_options.Text)
                    {
                        @out += cap[1];
                    }
                    else
                    {
                        @out += _options.Renderer.Rate(StringHelper.Escape(cap[1]), cap[2], false);
                    }
                    continue;
                }

                // phoneme
                cap = this._rules.Phoneme.Exec(src);
                if (cap.Any())
                {
                    src = src.Substring(cap[0].Length);

                    if (_options.Text)
                    {
                        @out += cap[1];
                    }
                    else
                    {
                        @out += _options.Renderer.Phoneme(StringHelper.Escape(cap[1]), cap[2]);
                    }
                    continue;
                }

                // prosody
                cap = this._rules.Prosody.Exec(src);
                if (cap.Any())
                {
                    src = src.Substring(cap[0].Length);

                    if (_options.Text)
                    {
                        @out += cap[1];
                    }
                    else
                    {
                        @out += _options.Renderer.Prosody(StringHelper.Escape(cap[1]), cap[2], cap[3], cap[4], false);
                    }
                    continue;
                }

                // text
                cap = this._rules.Text.Exec(src);
                if (cap.Any())
                {
                    src = src.Substring(cap[0].Length);

                    if (_options.Text)
                    {
                        @out += cap[0];
                    }
                    else
                    {
                        @out += _options.Renderer.Text(StringHelper.Escape(this.Smartypants(cap[0])));
                    }
                    continue;
                }

                if (!String.IsNullOrEmpty(src))
                {
                    throw new Exception("Infinite loop on byte: " + (int)src[0]);
                }
            }

            return @out;
        }

        /// <summary>
        /// Smartypants Transformations
        /// </summary>
        protected virtual string Smartypants(string text)
        {
            if (!this._options.Smartypants) return text;

            return text
                // em-dashes
                .Replace("---", "\u2014")
                // en-dashes
                .Replace("--", "\u2013")
                // opening singles
                .ReplaceRegex(@"(^|[-\u2014/(\[{""\s])'", "$1\u2018")
                // closing singles & apostrophes
                .Replace("'", "\u2019")
                // opening doubles
                .ReplaceRegex(@"(^|[-\u2014/(\[{\u2018\s])""", "$1\u201c")
                // closing doubles
                .Replace("\"", "\u201d")
                // ellipses
                .Replace("...", "\u2026");
        }
    }
}
