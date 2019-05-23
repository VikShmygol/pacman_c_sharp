using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pacman_c_sharp
{
    class Pacman : DynamicObject
    {
        private char pacmanLook { set; get; } = 'v';
        public Pacman(int locationRow, int locationCol) : base("pacman", locationRow, locationCol) { }

        public override Collision Action(ref List<string> map)
        {
            char northNeighbour = map[LocationRow - 1][LocationCol];
            char eastNeighbour = map[LocationRow][LocationCol + 1];
            char southNeighbour = map[LocationRow + 1][LocationCol];
            char westNeighbour = map[LocationRow][LocationCol - 1];

            Dictionary<char, char> directionNeighboursDict = new Dictionary<char, char>
            {
                { 'w', northNeighbour },
                { 'd', eastNeighbour },
                { 's', southNeighbour },
                { 'a', westNeighbour }
            };

            bool moved = false;
            char input = 'e';
            char objLook = ' ';

            

            if (Console.KeyAvailable)
            {
                 input = Console.ReadKey(true).KeyChar;
            }
            else { return new Collision(this, 0, 0, objLook); }

            if (input == 'w' || input == 'a' || input == 's' || input == 'd')
            {
                moved = Common.PacmanPlaceToMove.Contains(directionNeighboursDict[input]);
            }

            if (moved)
            {
                bool ghostAhead = directionNeighboursDict[input] == Common.GhostLook ||
                                  directionNeighboursDict[input] == Common.GhostLookScared;
                map[LocationRow] = Common.StringChanger(map[LocationRow], LocationCol, ' ');
                int objRow = LocationRow, objCol = LocationCol;

                switch (input)
                {
                    case 'w':
                        objLook = map[--objRow][objCol];
                        if (!ghostAhead)
                        {
                            LocationRow--;
                            map[LocationRow] = Common.StringChanger(map[LocationRow], LocationCol, Common.PacmanLook[0]);
                        }
                        break;
                    case 'd':
                        objLook = map[objRow][++objCol];
                        if (!ghostAhead)
                        {
                            LocationCol++;
                            map[LocationRow] = Common.StringChanger(map[LocationRow], LocationCol, Common.PacmanLook[1]);
                        }
                        break;
                    case 's':
                        objLook = map[++objRow][objCol];
                        if (!ghostAhead)
                        {
                            LocationRow++;
                            map[LocationRow] = Common.StringChanger(map[LocationRow], LocationCol, Common.PacmanLook[2]);
                        }
                        break;
                    case 'a':
                        objLook = map[objRow][--objCol];
                        if (!ghostAhead)
                        {
                            LocationCol--;
                            map[LocationRow] = Common.StringChanger(map[LocationRow], LocationCol, Common.PacmanLook[3]);
                        }
                        break;
                }
                return new Collision(this, objRow, objCol, objLook);
            }
            return new Collision(this, 0, 0, objLook);
        }
        public void Ressurection(ref List<string> map)
        {
            map[LocationRow] = Common.StringChanger(map[LocationRow], LocationCol, ' ');
            SetRessurectionLocation();
            map[LocationRow] = Common.StringChanger(map[LocationRow], LocationCol, '>');
        }
    }
    
}
