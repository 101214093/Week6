using System;
using System.Collections.Generic;


namespace Snake
{
    class Obstacle
    {
        private List<Position> obstacles = new List<Position>();
        private int x { get; set; }
        private int y { get; set; }

        public Obstacle(int num)
        {
            Random random = new Random();

            //randomize the coordinates for the obstacles
            for (int i = 0; i < num; i++)
            {
                int x = random.Next(Console.WindowHeight);
                int y = random.Next(Console.WindowWidth);
                obstacles.Add(new Position(x, y));
            }

            //print out thr obstacles
            foreach (Position obstacle in obstacles)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.SetCursorPosition(obstacle.col, obstacle.row);
                Console.Write("=");
            }
        }

        public List<Position> GetObsPos
        {
            get { return obstacles; }
            set { obstacles = value; }
        }
    }
}