using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Kingstudio.Speach.Synthesis.Gymd2ssml
{
    public class Options
    {
        #region Fields

        private Renderer _renderer;

        #endregion

        #region Properties

        public Renderer Renderer
        {
            get { return _renderer; }
            set { _renderer = value; if (_renderer != null) _renderer.Options = this; }
        }

        public bool Smartypants { get; set; }

        public string Breaks { get; set; }
        public string SsmlVersion { get; set; }
        public string SsmlNameSpace { get; set; }
        public string SsmlLanguage { get; set; }
        public string SsmlDefaultVoice { get; set; }
        public bool Voice { get; set; }
        public bool Prosody { get; set; }
        public bool Speak { get; set; }
        public string Rate { get; set; }
        public string Volume { get; set; }
        public string Pitch { get; set; }
        public bool Text { get; set; }

        #endregion

        #region Constructors

        public Options()
        {
            Renderer = new Renderer(this);
            Smartypants = false;
            Breaks = null;
            Voice = true;
            Speak = true;
            Prosody = true;
            SsmlVersion = "1.0";
            SsmlNameSpace = "https://www.w3.org/2001/10/synthesis";
            SsmlLanguage = "en-US";
            SsmlDefaultVoice = "VW Paul";
            Rate = "medium";
            Volume = "medium";
            Pitch = "medium";
            Text = false;
        }

        #endregion
    }
}
