using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aesthetics
{
    #region HEADER CLASS
    public class Header
    {
        #region Getters & Setters
        public string Title { get; set; } = ">>> NO TITLE GIVEN! <<<";
        public string TagLine { get; set; } = "* CSC438 *";
        public char Symbol { get; set; } = (char)22;
        public bool Clear { get; set; } = false;
        #endregion

        #region Constructors
        public Header(string title, string tagLine, char aChar)
        {
            this.Title = title;
            this.TagLine = tagLine;
            this.Symbol = aChar;
        }
        public Header(string title, string tagLine)
        {
            this.Title = title;
            this.TagLine = tagLine;
        }
        public Header(string title)
        {
            this.Title = title;
        }
        public Header() : this(">> TITLE <<", "*", (char)22) { }
        #endregion

        #region Methods
        public void DisplayHeader(string title, string tag)
        {
            Title = title;
            TagLine = tag;
            FormatHeader();
        }
        public void DisplayHeader(string title)
        {
            Title = title;
            FormatHeader();
        }
        public void DisplayHeader()
        {
            DisplayHeader(Title);
        }
        public void FormatHeader()
        {
            int width = Console.WindowWidth;
            string title = Title;
            string tag = TagLine;
            char aChar = Symbol;
            
            Console.Clear();
            Console.Title = this.Title;
            Console.ForegroundColor = ConsoleColor.Green;
            Spacer sp = new Spacer(aChar, Console.WindowWidth);

            sp.ShowSpacer();
            Console.SetCursorPosition((width - title.Length) / 2, Console.CursorTop);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(title);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition((width - tag.Length) / 2, Console.CursorTop);
            Console.WriteLine(TagLine);
            sp.ShowSpacer();

            Console.ResetColor();
        }
        #endregion

    }
    #endregion
}