using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pacman_c_sharp
{
    class Ghost : DynamicObject
    {
        private OnOffTimer timerToBlink = new OnOffTimer();
        public Ghost(int locationRow, int locationCol) : base("ghost", locationRow, locationCol) { }
        public bool Scared { set; get; }
        private bool FirstMove { set; get; } = true;
        private int Direction { set; get; } = 0;
        private char SubstituteSymbol { set; get; } = '.';

        public override Collision Action(ref List<string> map) 
        {
            char northNeighbour = map[LocationRow - 1][LocationCol];
            char eastNeighbour = map[LocationRow][LocationCol + 1];
            char southNeighbour = map[LocationRow + 1][LocationCol];
            char westNeighbour = map[LocationRow][LocationCol - 1];

            // quiting in case if ghost is surrounded by other ghosts or by walls
            if (!Common.GhostPlaceToMove.Contains(northNeighbour) &&
                !Common.GhostPlaceToMove.Contains(eastNeighbour) &&
                !Common.GhostPlaceToMove.Contains(westNeighbour) &&
                !Common.GhostPlaceToMove.Contains(southNeighbour))
            {
                return new Collision(this, 0, 0, ' ');
            }

            Dictionary<int, char> directionNeighboursDict = new Dictionary<int, char>
            { { 0, northNeighbour },
                { 1, eastNeighbour },
                { 2, southNeighbour },
                {3, westNeighbour } };

            Direction = GhostMoveDirection(ref directionNeighboursDict);

            if (!FirstMove)
            {
                map[LocationRow] = Common.StringChanger(map[LocationRow], LocationCol, SubstituteSymbol);
            } else
            {
                map[LocationRow] = Common.StringChanger(map[LocationRow], LocationCol, ' ');
                FirstMove = false;
            }

            if (!Common.PacmanLook.Contains(directionNeighboursDict[Direction]))
            {
                SubstituteSymbol = directionNeighboursDict[Direction];
            }

            char ghostLook = Common.GhostLook;

            //TODO: Add a timer here
            if (Scared && !timerToBlink.Activate(500, 500))
            {
                ghostLook = Common.GhostLookScared;
            }

            char objLook = ' ';
            bool pacmanAhead = Common.PacmanLook.Contains(directionNeighboursDict[Direction]);

            switch (Direction)
            {
                case 0:
                    objLook = map[LocationRow - 1][LocationCol];
                    if (!pacmanAhead)
                    {
                        --LocationRow;
                        map[LocationRow] = Common.StringChanger(map[LocationRow], LocationCol, ghostLook);
                    }
                    break;
                case 1:
                    objLook = map[LocationRow][LocationCol + 1];
                    if (!pacmanAhead)
                    {
                        ++LocationCol;
                        map[LocationRow] = Common.StringChanger(map[LocationRow], LocationCol, ghostLook);
                    }
                    break;
                case 2:
                    objLook = map[LocationRow + 1][LocationCol];
                    if (!pacmanAhead)
                    {
                        ++LocationRow;
                        map[LocationRow] = Common.StringChanger(map[LocationRow], LocationCol, ghostLook);
                    }
                    break;
                case 3:
                    objLook = map[LocationRow][LocationCol - 1];
                    if (!pacmanAhead)
                    {
                        --LocationCol;
                        map[LocationRow] = Common.StringChanger(map[LocationRow], LocationCol, ghostLook);
                    }
                    break;
            }
            if (Common.PacmanLook.Contains(objLook))
            {
                return new Collision(this, LocationRow, LocationCol, objLook);
            }
            return new Collision(this, 0, 0, objLook); ;
        }

        private int GhostMoveDirection(ref Dictionary<int, char> neighboursMap)
        {
            int sideDirection1 = (Direction + 1) % 4;
            int sideDirection2 = (Direction + 3) % 4;
            int oppositeDirection = (Direction + 2) % 4;
            int directionToAvoid = oppositeDirection;

            if (!Common.GhostPlaceToMove.Contains(neighboursMap[sideDirection1]) &&
                !Common.GhostPlaceToMove.Contains(neighboursMap[sideDirection2]) &&
                !Common.GhostPlaceToMove.Contains(neighboursMap[Direction]))
            {
                return oppositeDirection;
            } else if (!Common.GhostPlaceToMove.Contains(neighboursMap[sideDirection1]) &&
                !Common.GhostPlaceToMove.Contains(neighboursMap[sideDirection2]) &&
                Common.GhostPlaceToMove.Contains(neighboursMap[Direction]))
            {
                return Direction;
            } else if (!Common.GhostPlaceToMove.Contains(neighboursMap[Direction])) { }

            Random rnd = new Random();

            int newDirection;

            while (true)
            {
                newDirection = rnd.Next(0, 4);
                if ((newDirection != directionToAvoid) &&
                    Common.GhostPlaceToMove.Contains(neighboursMap[newDirection]))
                {
                    break;
                }
            }
            return newDirection;      
        }

        public void Resurrection(ref List<string> map)
        {
            map[LocationRow] = Common.StringChanger(map[LocationRow], LocationCol, SubstituteSymbol);
            SubstituteSymbol = ' ';
            SetRessurectionLocation();
            map[LocationRow] = Common.StringChanger(map[LocationRow], LocationCol, Common.GhostLook);
            Scared = false;
        }
    }
}
