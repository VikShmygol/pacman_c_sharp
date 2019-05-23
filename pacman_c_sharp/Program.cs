using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pacman_c_sharp
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] arrayFilename = { "Level_1.txt", "Level_2.txt"};
            List<string> map = new List<string>();
            GameSys game = new GameSys();

            game.GameProcessing(arrayFilename, ref map);
        }
    }
}
