using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pacman_c_sharp
{
    abstract class DynamicObject
    {
        public DynamicObject(string objType, int row, int col)
        {
            LocationRow = row;
            LocationCol = col;
            RessurectionRow = row;
            RessurectionCol = col;
            ObjType = objType;
        }
        public int LocationRow { set; get; }
        public int LocationCol { set; get; }

        public int RessurectionRow { get; }
        public int RessurectionCol { get; }

        public string ObjType { get; }

        public void SetRessurectionLocation()
        {
            LocationRow = RessurectionRow;
            LocationCol = RessurectionCol;
        }

        public abstract Collision Action(ref List<string> map);
       
    }
}
