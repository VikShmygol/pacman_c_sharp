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
           /* string fullpath = Environment.GetCommandLineArgs()[0];
            Console.WriteLine(fullpath);
            Console.ReadKey();*/
            string[] arrayFilename = { "Level_1.txt", "Level_2.txt" };
            List<string> map = new List<string>();
            GameSys game = new GameSys();

            // Wrapper to stop program execution in case of exception
            try
            {
                game.GameProcessing(arrayFilename, ref map);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            catch {}
            
        }
    }
}
