using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace pacman_c_sharp
{
    class GameSys
    {
        private int PacmanLives { set; get; } = 3;
        public int Score { set; get; } = 0;
        public int NumDots { set; get; } = 0;
        private int CurLevel { set; get; } = 0;

        private DelayOffTimer timerGhostsSlowDown = new DelayOffTimer();

        private DelayOffTimer timerScareGhosts = new DelayOffTimer();

        public List<Ghost> ghosts = new List<Ghost>();

        public List<Pacman> pacman = new List<Pacman>();
               
        private int MapHeight { set; get; }

        private int MapWidth { set; get; }
        public void GameProcessing(string[] levelsFilename, ref List<string> map)
        {
            while (true)
            {
                if ((NumDots == 0) && (PacmanLives != 0) && (CurLevel < levelsFilename.Length))
                {
                    LoadLevel(levelsFilename[CurLevel], ref map);
                    CurLevel++;
                } else if (NumDots == 0)
                {
                    Console.WriteLine("You won, congratulations!");
                    Console.ReadKey();
                    break;
                } else if (PacmanLives == 0)
                {
                    Console.WriteLine("Try again!");
                    Console.ReadKey();
                    break;

                } else
                {
                    ProcessingLevel(ref map);
                }

                ScreenUpdate(ref map, MapHeight, MapWidth);

            }
        }

        private void ScreenUpdate(ref List<string> map, int height, int width)
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(MapWidth , MapHeight + 3);
            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < width; ++j)
                {
                    Console.SetCursorPosition(j, i);
                    Console.Write(map[i][j]);
                }
            }
            Console.WriteLine();
            Console.WriteLine("Score: {0, 4}    Lives: {1, 1}    Level: {2, 1}", Score, PacmanLives, CurLevel);
        }
        public void LoadLevel(string levelFilename, ref List<string> map)
        {
            map.Clear();
            ghosts.Clear();
            pacman.Clear();

            string[] lines = null;
            string[] firstRow = null;

            try
            { 
                lines = File.ReadAllLines(levelFilename);
                firstRow = lines[0].Split();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Map file " + e.FileName + " not found");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                throw e;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while opening and reading map file: " + e.Message);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                throw e;
            }

            try
            {
                MapHeight = int.Parse(firstRow[0]);
                MapWidth = int.Parse(firstRow[1]);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while parsing first line of a map file: " + e.Message);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                throw e;
            }
          

            for (var i = 1; i < lines.Length; ++i)
            {
                map.Add(lines[i]);
            }

            try
            {
                for (int i = 0; i < MapHeight; ++i)
                {
                    for (int j = 0; j < MapWidth; ++j)
                    {
                        switch (map[i][j])
                        {
                            case Common.SuperDotLook:
                            case Common.RegularDotLook:
                                NumDots++;
                                break;
                            case Common.GhostLook:
                                ghosts.Add(new Ghost(i, j));
                                break;
                            case '>':
                                pacman.Add(new Pacman(i, j));
                                break;

                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while parsing map: " + e.Message);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                throw e;
            }

            if (pacman.Count != 1)
            {
                throw new ArgumentOutOfRangeException("Quantity of Pacman's characters is out of range");
            }
        }

        public void StartOverLevel(ref List<string> map)
        {
            PacmanLives--;
            pacman[0].Ressurection(ref map);
            foreach (var ghost in ghosts)
            {
                ghost.Resurrection(ref map);
            }
        }

        private void ProcessingLevel(ref List<string> map)
        {
            if (!timerGhostsSlowDown.Check(350))
            {
                foreach (var ghost in ghosts)
                {
                    ghost.Action(ref map).Processing(this, ref map, ref ghosts);
                }
                timerGhostsSlowDown.Start();
            }
            pacman[0].Action(ref map).Processing(this, ref map, ref ghosts);

            if (!timerScareGhosts.Check(10000))
            {
                EncourageGhosts();
            }
        }
        public void ScareGhosts()
        {
            foreach (var ghost in ghosts)
            {
                ghost.Scared = true;
            }
            timerScareGhosts.Start();
        }
        
        public void EncourageGhosts()
        {
            foreach (var ghost in ghosts)
            {
                ghost.Scared = false;
            }
        }

    }
}
