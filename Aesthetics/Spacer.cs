/***********************************************************************
 * Spacer.cs
 * Copy of the Spacer class from my original Aesthetics project. This project 
 * has been useful on several of my projects for making the console output 
 * easier to read.
 * 
 * 15 AUG 2017 ~ Jason C. Brown
 ***********************************************************************/ 
using System;

namespace Aesthetics
{
    #region SPACER CLASS
    public class Spacer
    {
        #region Getters & Setters
        public char Char { get; set; } = '*';
        public int NumChar { get; set; } = 50;
        public string TagLine { get; set; } = ">>> ALL HAIL THE HYPNO TOAD! <<<";
        //public char Char { get => _char; set => _char = value; } = '*';
        //public int NumChar { get => _numChar; set => _numChar = value; } = 50;
        #endregion

        #region Constructors
        // Chaining these just for learning purposes
        public Spacer(char char1, int int1)
        {
            this.Char    = char1;
            this.NumChar = int1;
        }
        public Spacer(char char1) : this(char1, 75) { }
        public Spacer() : this('*') { }
        public Spacer(int rndInt)
        {
            this.NumChar = 75;
            this.Char    = (char)rndInt; // This makes ASCII chars...
#if SPACER
            // DEBUG:
            Console.WriteLine("\t>> CTOR: Spacer(int rndInt) <<");
#endif
        }
        #endregion

        #region Methods
       

        public void ShowSpacer()
        {
            for (int i = 0; i < this.NumChar; i++)
            {
                Console.Write(this.Char);
            }
        }

        public void ShowSpacer(char c)
        {
            this.Char = c;
            for (int i = 0; i < this.NumChar; i++)
            {
                Console.Write(this.Char);
            }
        }

        public void ShowSpacer(char c, int num)
        {
            this.Char = c;
            this.NumChar = num;
            for (int i = 0; i < this.NumChar; i++)
            {
                Console.Write(this.Char);
            }
        }

        public void ShowSpacer(char c, bool full)
        {
            this.Char = c;
            this.NumChar = (full == true) ? Console.WindowWidth : this.NumChar;
            for (int i = 0; i < this.NumChar; i++)
            {
                Console.Write(this.Char);
            }
        }

        public void AvailableChars()
        {
            Console.Write("Decimal".PadRight(10));
            Console.Write("ASCII".PadRight(10));
            Console.Write("Hex".PadRight(10));
            Console.WriteLine();

            int min = 0;
            int max = 128;
            for (int i = min; i < max; i++)
            {
                // Get ASCII character.
                char c = (char)i;

                // Get display string.
                string display = string.Empty;
                if (char.IsWhiteSpace(c))
                {
                    display = c.ToString();
                    switch (c)
                    {
                        case '\t':
                            display = "\\t";
                            break;
                        case ' ':
                            display = "space";
                            break;
                        case '\n':
                            display = "\\n";
                            break;
                        case '\r':
                            display = "\\r";
                            break;
                        case '\v':
                            display = "\\v";
                            break;
                        case '\f':
                            display = "\\f";
                            break;
                    }
                }
                //else if (char.IsControl(c))
                //{
                //    display = "control";
                //}
                else
                {
                    display = c.ToString();
                }
                // Write table row.
                Console.Write(i.ToString().PadRight(10));
                Console.Write(display.PadRight(10));
                Console.Write(i.ToString("X2"));
                Console.WriteLine();
            }
        }
        #endregion
    }
    #endregion


    #region EXPERIMENTAL / LEARNING / TESTING
    /* Experimenting with inheritance... 
     * Shelving this for now to get on with ADNS and FileIO */
    public class Disp : Spacer
    {
        public Disp(int value)
            : base(value)
        {
            Console.WriteLine("> Calling Disp constructor...");
        }
        public static void ShowSpacer(Spacer s)
        {
            for (int i = 0; i < s.NumChar; i++)
                Console.Write(s.Char);
            Console.WriteLine();
        }
    }
    #endregion
}
