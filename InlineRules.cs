using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Kingstudio.Speach.Synthesis.Gymd2ssml
{
    /// <summary>
    /// Inline-Level Grammar
    /// </summary>
    public class InlineRules
    {
        #region Fields

        private static readonly Regex escape = new Regex(@"^\\([\\`*{}\[()#+\-.!_>])");
        private static readonly Regex text = new Regex(@"^[\s\S]+?(?=[\\<!\[\(\/_*`]| {2,}\n|$)");
        private static readonly Regex audio = new Regex(@"^!\(?((?:\([^\)\n\[\]]*\)|[^\(\)\n\[\]]|\)(?=[^\(\n\[\]]*\)))*)\)?\[['""]?(?:audio)?['""]?:?['""]?((?:https?|ftp|file):\/\/[-A-Za-z0-9+&@#\/%?=~_|!:,.]+wav)['""]?\]");
        private static readonly Regex image = new Regex(@"^!\(?((?:\([^\)\n\[\]]*\)|[^\(\)\n\[\]]|\)(?=[^\(\n\[\]]*\)))*)\)?\[['""]?(?:image)?['""]?:?['""]?((?:https?|ftp|file):\/\/[-A-Za-z0-9+&@#\/%?=~_|!:.]+[jpg|png|jpeg])['""]?\]");
        private static readonly Regex breaks = new Regex(@"^!?\[['""]?(?:break)?['""]?:?['""]?(\d+\.?\d?m?s)['""]?\]");
        private static readonly Regex emphasis = new Regex(@"^!?\(((?:\([^\)]*\)|[^\(\)]|\)(?=[^\(]*\)))*)\)\[\s*['""]?emphasis['""]?\s*:?\s*(?:\s*['""]*([\s\S]*?)['""]*)?\s*\]");
        private static readonly Regex sub = new Regex(@"^!?\(((?:\([^\)]*\)|[^\(\)]|\)(?=[^\(]*\)))*)\)\[\s*['""]?(?:sub)?['""]?\s*:?\s*(?:\s*['""]{1}([\s\S]*?)['""]{1})?\s*\]");
        private static readonly Regex prosody = new Regex(@"^!?\(([\s\S]*?)\)\[['""]?pitch['""]?:?['""]?([xlowmediuhghsfat-]+)['""]?,?['""]?rate['""]?:?['""]?([xlowmediuhghsfat-]+)['""]?,?['""]?volume['""]?:?['""]?([xlowmediuhghsfat-]+)['""]?\]");
        private static readonly Regex repeat = new Regex(@"^!?\(((?:\([^\)]*\)|[^\(\)]|\)(?=[^\(]*\)))*)\)\[\s*['""]?(?:repeat)?['""]?\s*:?\s*['""]?([\d]+)['""]?\s*,?\s*(?:break:)?['""]?((\d+\.?\d?m?s)?)['""]?\s*\]");
        private static readonly Regex voice = new Regex(@"^!?\(((?:\([^\)]*\)|[^\(\)]|\)(?=[^\(]*\)))*)\)\[\s*['""]?voice['""]?\s*:\s*['""]?([a-zA-Z0-9-_ ]+)['""]?\s*,?\s*lang\s*:\s*['""]?([a-zA-Z0-9-_]+)['""]?\s*\]");
        private static readonly Regex singlelineComment = new Regex(@"^\/\/(.*)");
        private static readonly Regex multilineComment = new Regex(@"^\/\*([\w\W]*?)\*\/");
        private static readonly Regex cardinal = new Regex(@"^!?\(([\d.,]+?)\)\[['""]?(cardinal|number)['""]?\]");
        private static readonly Regex ordinal = new Regex(@"^!?\(([\da-zA-Z]+?)\)\[['""]?(ordinal)['""]?\]");
        private static readonly Regex characters = new Regex(@"^!?\(([a-zA-Z0-9]+?)\)\[['""]?(characters)['""]?\]");
        private static readonly Regex date = new Regex(@"^!?\(([0-9-\/]+?)\)\[['""]?date['""]?:['""]?([ymd]+)['""]?\]");
        private static readonly Regex telephone = new Regex(@"^!?\(([\d\- \(\)]+?)\)\[['""]?(phone|telephone)['""]?:?['""]?([0-9]+)?['""]?\]");
        private static readonly Regex time = new Regex(@"^!?\(([\d: apm]+?)\)\[['""]?time['""]?:?['""]?(hms12|hms24)?['""]?\]");
        private static readonly Regex pitch = new Regex(@"^!?\((.*?)\)\[['""]?(?:pitch)['""]?:?['""]?([xlowmediuhghsfat-]+)['""]?\]");
        private static readonly Regex volume = new Regex(@"^!?\((.*?)\)\[['""]?(?:volume|vol)['""]?:?['""]?([xlowmediuhghsfat-]+)['""]?\]");
        private static readonly Regex rate = new Regex(@"^!?\((.*?)\)\[['""]?(?:rate)['""]?:?['""]?([xlowmediuhghsfat-]+)['""]?\]");
        private static readonly Regex phoneme = new Regex(@"^!?\((.*?)\)\[['""]?(?:phoneme)['""]?:?['""](.*?)['""]\]");

        #endregion

        #region Properties

        public virtual Regex Escape { get { return escape; } }
        public virtual Regex Text { get { return text; } }
        public virtual Regex Audio { get { return audio; } }
        public virtual Regex Image { get { return image; } }
        public virtual Regex Break { get { return breaks; } }
        public virtual Regex Emphasis { get { return emphasis; } }
        public virtual Regex Sub { get { return sub; } }
        public virtual Regex Repeat { get { return repeat; } }
        public virtual Regex Voice { get { return voice; } }
        public virtual Regex Prosody { get { return prosody; } }
        public virtual Regex SinglelineComment { get { return singlelineComment; } }
        public virtual Regex MultilineComment { get { return multilineComment; } }
        public virtual Regex Cardinal { get { return cardinal; } }
        public virtual Regex Ordinal { get { return ordinal; } }
        public virtual Regex Characters { get { return characters; } }
        public virtual Regex Date { get { return date; } }
        public virtual Regex Telephone { get { return telephone; } }
        public virtual Regex Time { get { return time; } }
        public virtual Regex Rate { get { return rate; } }
        public virtual Regex Volume { get { return volume; } }
        public virtual Regex Pitch { get { return pitch; } }
        public virtual Regex Phoneme { get { return phoneme; } }

        #endregion
    }

    /// <summary>
    /// Normal Inline Grammar
    /// </summary>
    public class NormalInlineRules : InlineRules
    {
    }
}
