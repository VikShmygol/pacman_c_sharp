using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pacman_c_sharp
{
    static class Common
    {
        //Constants used throughout the program
        public const string GhostPlaceToMove = "∙ <>v^0";
        public const char GhostLook = 'M';
        public const char GhostLookScared = 'W';
        public const string PacmanPlaceToMove = "MW ∙0";
        public const string PacmanLook = "v<^>";
        public const char RegularDotLook = '∙';
        public const char SuperDotLook = '0';

        //Method returns modified string
        public static string StringChanger(string stringToChange, int charIndex, char newCharacter)
        {
            StringBuilder sb = new StringBuilder(stringToChange);

            if (charIndex < 0 || charIndex > (sb.Length - 1))
            {
                throw new ArgumentOutOfRangeException("Wrong index value while running StringChanger: index was " + charIndex);
            }
           
            sb[charIndex] = newCharacter;
            return sb.ToString();
        }
    }

}
