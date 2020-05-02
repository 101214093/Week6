using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;

namespace Snake
{
    class Snake
    {
        public Queue<Position> snakeElements;

        public Snake()
        {
            snakeElements = new Queue<Position>();
        }

        public Queue<Position> GetPos
        {
            get { return snakeElements; }
            set { snakeElements = value; }
        }

        public void DrawSnake()
        {
            for (int i = 0; i <= 3; i++)
            {
                snakeElements.Enqueue(new Position(0, i));
            }
        }
    }
}
