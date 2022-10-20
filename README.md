# gymd

#### 介绍
谷雨语音MarkDown

#### 如何获取Gymd2ssml类库？

Gymd2ssml类库是一种采用木兰协议的开源类库，使用C#语言编写，可以将一段使GYMD编写的语音合同标记语言转换为大多数语音合成引擎能识别的SSML，也可以转换为没有任何标记的纯TEXT文档。项目代码托管在国内知名开源平台gitee.com，项目地址https://gitee.com/kingstudio/gymd，欢迎大家fork、加星和捐赠。

#### 软件架构

关于编译原理请参考《计算机科学丛书：编译原理（第2版）》，书籍全面、深入地探讨了编译器设计方面的重要主题，包括词法分析、语法分析、语法制导定义和语法制导翻译、运行时刻环境、目标代码生成、代码优化技术、并行性检测以及过程间分析技术，并在相关章节中给出大量的实例。

感谢[Marked .NET](https://github.com/alex-titarenko/markednet)这个项目带来灵感。

#### 安装教程

1.  git clone https://gitee.com/kingstudio/gymd

#### 使用说明

```C#
using Kingstudio.Speach.Synthesis.Gymd2ssml;

namespace Kingstudio.Speech.Synthesis
{
    public partial class MainForm : Form
    {
        string rate = "medium";
        string volume = "medium";
        string pitch = "medium";
        bool voice = true;
        bool prosody = false;
        bool speak = true;
        string gymd = null;
        string ssml = null;
        string voiceDefaultMale = null;
        string voiceDefaultFemale = null;
        string defaultLang = null;

        private Gymd2ssml _markdown;
		
        static void Main(string[] args)
        {
			//option 初始化
			Options options = new Options();
			options.SsmlLanguage = this.defaultLang;
			options.SsmlDefaultVoice = this.voiceDefaultMale;
			options.Rate = this.rate;
			options.Volume = this.volume;
			options.Pitch = this.pitch;
			options.Voice = this.voice;
			options.Prosody = this.prosody;
			options.Speak = this.speak;
			//markdown 初始化
			this._markdown = new Gymd2ssml(options);
			
			var gymd =  @"
(2022年6月大学英语六级考试)[voice:""VW Liang"",lang:""cn-ZH""] 
 
#[voice:""VW Paul"",lang:""en-US""] 
Part II. Listening Comprehension [1000ms]
Section A [1000ms] 

#[voice:""IVONA Brian"",lang:""en-GB""] 
Directions: In this section, you will hear 8 short conversations and 2 long conversations. At the end of each conversation, one or more questions will be asked about what was said. Both the conversation and the questions will be spoken only once. After each question there will be a pause. During the pause, you must read the four choices marked A), B), C) and 1)), and decide which is the best answer. Then mark the corresponding letter on Answer Sheet 1 with a single line through the centre.

#[voice:""VW Paul"",lang:""en-US""] 
1.  [1000ms] 

#[voice:""VW Paul"",lang:""en-US""] 
I'd like to take a trip to Florida for my spring break. Can you give me any idea where to go?

#[voice:""VW Kate"",lang:""en-US""] 
I could tell you about the places I've visited, but I think you'd better look up a travel agency to help with the arrangements.

#[voice:""IVONA Brian"",lang:""en-GB""] 
Question, What does the man suggest the woman do?


#[question] 
A) Go to a place he has visited.
B) Make her own arrangements.
C) Consult a travel agent.
D) Join in a package tour.

#[answer] 
答案：C";
			ssml = _markdown.Parse(gymd);
        }
	}
}
```

```xml
<?xml version="1.0" encoding="ISO-8859-1"?>
<speak version="1.0" xmlns="https://www.w3.org/2001/10/synthesis" xml:lang="en-US">
<voice name="VW Paul" xml:lang="en-US" >
<p> <voice name="VW Liang" xml:lang="cn-ZH" >2022年6月大学英语六级考试</voice>
 </p><voice name="VW Paul" xml:lang="en-US" > 
Part II. Listening Comprehension <break time="1000ms" />
Section A <break time="1000ms" /> 
</voice>
<voice name="IVONA Brian" xml:lang="en-GB" > 
Directions: In this section, you will hear 8 short conversations and 2 long conversations. At the end of each conversation, one or more questions will be asked about what was said. Both the conversation and the questions will be spoken only once. After each question there will be a pause. During the pause, you must read the four choices marked A), B), C) and 1)), and decide which is the best answer. Then mark the corresponding letter on Answer Sheet 1 with a single line through the centre.
</voice>
<voice name="VW Paul" xml:lang="en-US" > 
1.  <break time="1000ms" /> 
</voice>
<voice name="VW Paul" xml:lang="en-US" > 
I&#39;d like to take a trip to Florida for my spring break. Can you give me any idea where to go?
</voice>
<voice name="VW Kate" xml:lang="en-US" > 
I could tell you about the places I&#39;ve visited, but I think you&#39;d better look up a travel agency to help with the arrangements.
</voice>
<voice name="IVONA Brian" xml:lang="en-GB" > 
Question, What does the man suggest the woman do?
</voice>

<![CDATA[
**The following areas are the test questions**
 
A) Go to a place he has visited.
B) Make her own arrangements.
C) Consult a travel agent.
D) Join in a package tour.
]]>

<![CDATA[
**The following areas are the test answer**
 
答案：C
]]>

</voice>
</speak>
```
