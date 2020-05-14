using System.Collections.Generic;
using System.Linq;

namespace Snake
{
    class Snake
    {
        private Queue<Position> snakeElements;

        public Snake()
        {
            snakeElements = new Queue<Position>();
        }

        public Queue<Position> GetPos
        {
            get { return snakeElements; }
            set { snakeElements = value; }
        }

        //draw the snake
        public void DrawSnake()
        {
            for (int i = 0; i <= 3; i++)
            {
                snakeElements.Enqueue(new Position(0, i));
            }
        }

        //increase the length of the snake by 1
        public void AddSnake()
        {
            Position temp = new Position(snakeElements.Last().row, snakeElements.Last().col+1);
            snakeElements.Enqueue(temp);
        }
    }
}
