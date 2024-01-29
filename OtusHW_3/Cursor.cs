using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtusHW_3
{
    internal class Cursor
    {
        private int _positionY = 1;
        private int _positionX = 5;

        public int PositionY
        {
            get { return _positionY; }
            set
            {
                if (value <= 1)
                {
                    _positionY = 1;
                }
                else if (value == 2)
                {
                    _positionY = 2;
                }
                else
                {
                    _positionY = 3;
                }
            }
        }

        public int PositionX {
            get { return _positionX; } 
            set
            {
                if (value <= 5)
                {
                    _positionY = 5;
                }
                else
                {
                    _positionY = value;
                }
            }


        }

        public Cursor() {
           
        }

        public void SetPositionCursor(int x, int y)
        {
            Console.CursorTop = y;
            Console.CursorLeft = x;
        }


    }
}
