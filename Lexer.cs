using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Kingstudio.Speach.Synthesis.Gymd2ssml
{
    public class Lexer
    {
        private readonly Options options;
        private readonly BlockRules rules;


        public Lexer(Options options)
        {
            this.options = options ?? new Options();
            this.rules = new NormalBlockRules();
        }


        /// <summary>
        /// Static Lex Method
        /// </summary>
        public static TokensResult Lex(string src, Options options)
        {
            var lexer = new Lexer(options);
            return lexer.Lex(src);
        }

        /// <summary>
        /// Preprocessing
        /// </summary>
        protected virtual TokensResult Lex(string src)
        {
            src = src
                .ReplaceRegex(@"\r\n|\r", "\n")
                .Replace("\t", "    ")
                .Replace("\u00a0", " ")
                .Replace("\u2424", "\n");

            return Token(src, true);
        }

        /// <summary>
        /// Lexing
        /// </summary>
        protected virtual TokensResult Token(string srcOrig, bool top, TokensResult result = null)
        {
            var src = Regex.Replace(srcOrig, @"^ +$", "", RegexOptions.Multiline);
            IList<string> cap;
            var tokens = result ?? new TokensResult();

            while (!String.IsNullOrEmpty(src))
            {
                // newline
                if ((cap = this.rules.Newline.Exec(src)).Any())
                {
                    src = src.Substring(cap[0].Length);
                    if (cap[0].Length > 1)
                    {
                        tokens.Add(new Token
                        {
                            Type = "space"
                        });
                    }
                }

                // voice
                if (top && (cap = this.rules.Voice.Exec(src)).Any())
                {
                    src = src.Substring(cap[0].Length);
                    string text = cap[3];
                    tokens.Add(new Token
                    {
                        Type = "voice",
                        Text = text,
                        Voice = cap[1],
                        Lang = cap[2]
                    });
                    continue;
                }

                // prosody
                if (top && (cap = this.rules.Prosody.Exec(src)).Any())
                {
                    src = src.Substring(cap[0].Length);
                    string text = cap[4];
                    tokens.Add(new Token
                    {
                        Type = "prosody",
                        Text = text,
                        Pitch = cap[1],
                        Rate = cap[2],
                        Volume = cap[3]
                    });
                    continue;
                }

                // question
                if (top && (cap = this.rules.Question.Exec(src)).Any())
                {
                    src = src.Substring(cap[0].Length);
                    tokens.Add(new Token
                    {
                        Type = "question",
                        Text = cap[1][cap[1].Length - 1] == '\n'
                          ? cap[1].Substring(0, cap[1].Length - 1)
                          : cap[1]
                    });
                    continue;
                }

                // answer
                if (top && (cap = this.rules.Answer.Exec(src)).Any())
                {
                    src = src.Substring(cap[0].Length);
                    tokens.Add(new Token
                    {
                        Type = "answer",
                        Text = cap[1][cap[1].Length - 1] == '\n'
                          ? cap[1].Substring(0, cap[1].Length - 1)
                          : cap[1]
                    });
                    continue;
                }

                // comment
                if (top && (cap = this.rules.SinglelineComment.Exec(src)).Any())
                {
                    src = src.Substring(cap[0].Length);
                    tokens.Add(new Token
                    {
                        Type = "comment",
                        Text = cap[1]
                    });
                    continue;
                }

                // comment
                if (top && (cap = this.rules.MultilineComment.Exec(src)).Any())
                {
                    src = src.Substring(cap[0].Length);
                    tokens.Add(new Token
                    {
                        Type = "comments",
                        Text = cap[1]
                    });
                    continue;
                }

                // top-level p
                if (top && (cap = this.rules.Paragraph.Exec(src)).Any())
                {
                    src = src.Substring(cap[0].Length);
                    tokens.Add(new Token
                    {
                        Type = "paragraph",
                        Text = cap[1][cap[1].Length - 1] == '\n'
                          ? cap[1].Substring(0, cap[1].Length - 1)
                          : cap[1]
                    });
                    continue;
                }

                // text
                if ((cap = this.rules.Text.Exec(src)).Any())
                {
                    // Top-level should never reach here.
                    src = src.Substring(cap[0].Length);
                    tokens.Add(new Token
                    {
                        Type = "text",
                        Text = cap[0]
                    });
                    continue;
                }

                if (!String.IsNullOrEmpty(src))
                {
                    throw new Exception("Infinite loop on byte: " + (int)src[0]);
                }
            }

            return tokens;
        }
    }
}
