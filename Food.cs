using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    class Food
    {
        public int x { get; set; }
        public int y { get; set; }
        public Food()
        {
            //randomize the food coordinates
            Random random = new Random();
            this.x = random.Next(Console.WindowWidth);
            this.y = random.Next(Console.WindowHeight);
        }

        public void Generate_random_food()
        {
            //display the food
            Console.SetCursorPosition(this.x, this.y);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("@");
        }
    }
}
