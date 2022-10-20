using System;
using System.Windows.Documents;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Kingstudio.Speach.Synthesis.Gymd2ssml
{
    public class Renderer
    {
        #region Properties

        public Options Options { get; set; }

        #endregion

        #region Constructors

        public Renderer()
            : this(null)
        {
        }

        public Renderer(Options options)
        {
            Options = options ?? new Options();
        }

        #endregion

        #region Methods

        public virtual string Speak(string content)
        {
            return $"<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>\n<speak version=\"{Options.SsmlVersion}\" xmlns=\"{Options.SsmlNameSpace}\" xml:lang=\"{Options.SsmlLanguage}\">\n{content}</speak>\n";
        }

        public virtual string Audio(string src, string content)
        {
            
            var output = string.IsNullOrEmpty(content) ?$"<audio src=\"{src}\" />": $"<audio src=\"{src}\" >{content}</audio> ";

            return output;
        }

        public virtual string Image(string src, string content)
        {
            var output = string.IsNullOrEmpty(content) ? $"<image src=\"{src}\" />" : $"<image src=\"{src}\" >{content}</image>";
            output = Comment(output);

            return output;
        }

        public virtual string Break(string time)
        {
            var output = $"<break time=\"{time}\" />";

            return output;
        }

        public virtual string Emphasis(string level, string content)
        {
            level = string.IsNullOrEmpty(level) ? "moderate " : level;
            var output = $"<emphasis level=\"{level}\">{content}</emphasis>";

            return output;
        }

        public virtual string Paragraph(string content, string breaks)
        {
            var output = "";
            if (this.Options.Text)
            {
                output += content;
            }
            else
            {
                output += $"<p>{content}</p>\n";
            }
            if (!string.IsNullOrEmpty(breaks))
            {
                output += $"{Break(breaks)}\n";
            }
            return output;
        }

        public virtual string Sentence(string content)
        {
            return $"<s>{content}</s>";
        }

        public virtual string Phoneme(string content, string ph)
        {
            var output = $"<phoneme ph=\"{ph}\">{content}</phoneme>";

            return output;
        }
        
        public virtual string Pitch(string content, string pitch, bool newInnerline)
        {
            return Prosody(content, pitch, null, null, false);
        }

        public virtual string Rate(string content, string rate, bool newInnerline)
        {
            return Prosody(content, null, rate, null, false);
        }

        public virtual string Volume(string content, string volume, bool newInnerline)
        {
            return Prosody(content, null, null, volume, false);
        }
        public virtual string Prosody(string content, string pitch, string rate, string volume, bool newInnerline)
        {
            return Prosody(content, pitch, rate, volume, newInnerline, false);
        }

        public virtual string Prosody(string content, string pitch, string rate, string volume, bool newInnerline, bool newOutline)
        {
            var output = "";
            if (!string.IsNullOrEmpty(content))
            {
                if (newOutline)
                {
                    output += $"\n";
                }
                output += $"<prosody ";
                if (!string.IsNullOrEmpty(pitch))
                {
                    output += $"pitch=\"{pitch}\" ";
                }
                if (!string.IsNullOrEmpty(rate))
                {
                    output += $"rate=\"{rate}\" ";
                }
                if (!string.IsNullOrEmpty(volume))
                {
                    output += $"volume=\"{volume}\" ";
                }
                if (newInnerline)
                {
                    output += $">\n{content}</prosody>";
                }
                else
                {
                    output += $">{content}</prosody>";
                }
                if (newOutline)
                {
                    output += $"\n";
                }
            }
            return output;
        }

        public virtual string Date(string content, string interpretas, string format)
        {
            return Sayas(content, interpretas, format, null);
        }

        public virtual string Telephone(string content, string interpretas, string format)
        {
            return Sayas(content, interpretas, format, null);
        }

        public virtual string Time(string content, string interpretas, string format)
        {
            return Sayas(content, interpretas, format, null);
        }

        public virtual string Sayas(string content, string interpretas)
        {
            return Sayas(content, interpretas, null, null);
        }

        public virtual string Sayas(string content, string interpretas, string format, string detail)
        {
            var output = "";
            if (!string.IsNullOrEmpty(content) && !string.IsNullOrEmpty(interpretas))
            {
                output = $"<say-as interpret-as=\"{interpretas}\" ";
                if (!string.IsNullOrEmpty(format))
                {
                    output += $"format=\"{format}\" ";
                }
                if (!string.IsNullOrEmpty(detail))
                {
                    output += $"detail=\"{detail}\"";
                }
                output += $"> {content} </say-as>";
            }
            return output;
        }

        public virtual string Sub(string alias, string content)
        {
            var output = $"<sub alias=\"{alias}\">{content}</sub>";

            return output;
        }

        public virtual string Voice(string name, string lang, string content)
        {
            return Voice(name, lang, content, true, true);
        }

        public virtual string Voice(string name, string lang, string content, bool isInerNewline)
        {
            return Voice(name, lang, content, isInerNewline, true);
        }

        public virtual string Voice(string name, string lang, string content, bool isInerNewline, bool isOutNewline)
        {
            var output = "";
            if (isOutNewline)
            {
                output += $"\n";
            }
            output = $"<voice name=\"{name}\" ";
            if (!string.IsNullOrEmpty(lang))
            {
                output += $"xml:lang=\"{lang}\" ";
            }
            if (isInerNewline)
            {
                output += $">\n{content}\n</voice>";
            }
            else
            {
                output += $">{content}</voice>";
            }
            if (isOutNewline)
            {
                output += $"\n";
            }
            return output;
        }

        public virtual string Repeat(string time, string breaks, string content)
        {
            int count = 0;
            var output = "";
            try
            {
                count = int.Parse(time);
            }
            catch
            {
                Console.WriteLine("无法完成转换！");
            }
            for(int i = 0; i < count; i++)
            {
                output += $"<s>{content}</s>";
                if (!string.IsNullOrEmpty(breaks))
                {
                    output += Break(breaks);
                }
            }

            return output;
        }

        public virtual string Question(string content)
        {
            content = "**The following areas are the test questions**\n" + content;
            var output = Comment(content, true, true);

            return output;
        }

        public virtual string Answer(string content)
        {
            content = "**The following areas are the test answer**\n" + content;
            var output = Comment(content, true, true);

            return output;
        }

        public virtual string Comment(string content)
        {
            var output = $"<!-- {content} -->\n";

            return output;
        }

        public virtual string Comment(string content, bool multiline)
        {
            var output = $"\n<!--\n{content}\n-->\n";

            return output;
        }

        public virtual string Comment(string content, bool multiline, bool cdata)
        {
            var output = $"\n<![CDATA[\n{content}\n]]>\n";

            return output;
        }

        public virtual string Text(string content)
        {
            return content;
        }
        #endregion
    }
}