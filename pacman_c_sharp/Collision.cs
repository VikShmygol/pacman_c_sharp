using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pacman_c_sharp
{
    class Collision
    {
        public int CounterObjRow {get;}
        public int CounterObjCol { get; }
        public char CounterObjLook { get; }
        public DynamicObject Obj { get; }
        public Collision(DynamicObject obj, int counterObjectRow, int counterObjectCol, char counterObjLook)
        {
            CounterObjRow = counterObjectRow;
            CounterObjCol = counterObjectCol;
            CounterObjLook = counterObjLook;
            Obj = obj;
        }
        public void Processing(GameSys game, ref List<string> map, ref List<Ghost> ghosts)
        {
            if (CounterObjRow == 0 || CounterObjCol == 0) { return; }

            if (Obj.ObjType == "pacman")
            {
                switch (CounterObjLook)
                {
                    case Common.GhostLookScared:
                    case Common.GhostLook:
                        HandleGhostPacmanCase(game, ref ghosts, CounterObjRow, CounterObjCol, ref map);
                        break;
                    case Common.RegularDotLook:
                        game.NumDots--;
                        game.Score += 10;
                        break;
                    case Common.SuperDotLook:
                        game.NumDots--;
                        game.Score += 100;
                        game.ScareGhosts();
                        break;
                }
            } else if (Obj.ObjType == "ghost")
            {
                HandleGhostPacmanCase(game, ref ghosts, Obj.LocationRow, Obj.LocationCol, ref map);
            }
        }
        
        private int FindGhost(int row, int col, ref List<Ghost> ghosts)
        {
            int i;
            for (i = 0; i < ghosts.Count; )
            {
                if (ghosts[i].LocationRow == row && ghosts[i].LocationCol == col)
                { break; }
                ++i;
            }
            return i;
        }

        private void HandleGhostPacmanCase(GameSys game, ref List<Ghost> ghosts, 
                                            int row, int col, ref List<string> map)
        {
            int ghostIndex = FindGhost(row, col, ref ghosts);
            if (ghosts[ghostIndex].Scared)
            {
                ghosts[ghostIndex].Resurrection(ref map);
                game.Score += 200;
            } else { game.StartOverLevel(ref map); }
        }

    }
}
