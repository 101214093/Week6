using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            //create users
            UserManagement userlist = new UserManagement();
            Console.WriteLine("Please enter your username: ");
            string userName = Console.ReadLine();
            while (userName.Length == 0) {
                Console.WriteLine("Please enter your username: ");
                userName = Console.ReadLine();
            }
            User user = new User(userName);

            //select difficulty
            Console.WriteLine("Please select difficulty: [1]Easy ; [2]Normal ; [3]Hard");
            string difficulty = Console.ReadLine();
            while (difficulty != "1" && difficulty != "2" && difficulty != "3") {
                Console.WriteLine("Please select difficulty: [1]Easy ; [2]Normal ; [3]Hard");
                difficulty = Console.ReadLine();
            }
            Console.Clear();

            //help
            var path = "help.txt";
            using (StreamReader file = new StreamReader(path))
            {
                string ln;
                while ((ln = file.ReadLine()) != null)
                {
                    Console.WriteLine(ln);
                }
            }
            
            //press enter to continue
            Console.WriteLine("\n Press ENTER to continue");
            ConsoleKeyInfo conKey = Console.ReadKey();
            while (conKey.Key != ConsoleKey.Enter)
                conKey = Console.ReadKey();
            Console.Clear();

            // initialize background music
            System.Media.SoundPlayer bgm = new System.Media.SoundPlayer();
            bgm.SoundLocation = "../../sound/bgm.wav";
            bgm.Play();
                        
            byte right = 0;
            byte left = 1;
            byte down = 2;
            byte up = 3;

            int lastFoodTime = 0;
            int foodDissapearTime = 16000;
            //bool isGameOver = false;
            Stopwatch stopwatch = new Stopwatch();

            Position[] directions = new Position[]
            {
                new Position(0, 1), // right
                new Position(0, -1), // left
                new Position(1, 0), // down
                new Position(-1, 0), // up
            };

            //change sleepTime parameter based on difficulty
            Console.BufferHeight = Console.WindowHeight;
            double sleepTime;
            if (difficulty == "1") {
                sleepTime = 100;
            }
            else if (difficulty == "2")
            {
                sleepTime = 70;
            }
            else
            {
                sleepTime = 30;
            }
            lastFoodTime = Environment.TickCount;

            // Initialize obstacle and draw obstacles
            int obsNum;
            if (difficulty == "1")
            {
                obsNum = 5;
            }
            else if (difficulty == "2")
            {
                obsNum = 10;
            }
            else
            {
                obsNum = 30;
            }
            Obstacle obs = new Obstacle(obsNum);


            // Iniatitlize food and draw food
            Food food = new Food();
            food.Generate_random_food();

            // Initialize snake and draw snake
            Snake snake = new Snake();
            snake.DrawSnake();
            int direct = right;


            // Begin timing.
            stopwatch.Start();

            // looping
            while (true)
            {
                // Set the score at the top right
                Console.SetCursorPosition(Console.WindowWidth - 10, Console.WindowHeight - 30);
                Console.WriteLine("Score: " + user.getScore);

                // check for key pressed
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo userInput = Console.ReadKey();
                    if (userInput.Key == ConsoleKey.LeftArrow)
                    {
                        if (direct != right) direct = left;
                    }
                    if (userInput.Key == ConsoleKey.RightArrow)
                    {
                        if (direct != left) direct = right;
                    }
                    if (userInput.Key == ConsoleKey.UpArrow)
                    {
                        if (direct != down) direct = up;
                    }
                    if (userInput.Key == ConsoleKey.DownArrow)
                    {
                        if (direct != up) direct = down;
                    }
                }

                // update snake position
                Position snakeHead = snake.GetPos.Last();
                Position nextDirection = directions[direct];
                Position snakeNewHead = new Position(snakeHead.row + nextDirection.row, snakeHead.col + nextDirection.col);

                // check for snake if exceed the width or height
                if (snakeNewHead.col < 0) {
                    snakeNewHead.col = Console.WindowWidth - 1; 
                }
                if (snakeNewHead.row < 0) {
                    snakeNewHead.row = Console.WindowHeight - 1; 
                }
                if (snakeNewHead.row >= Console.WindowHeight) { 
                    snakeNewHead.row = 0; 
                }
                if (snakeNewHead.col >= Console.WindowWidth) {
                    snakeNewHead.col = 0; 
                }


                // check for snake collison with self or obstacles
                if (snake.GetPos.Contains(snakeNewHead) || obs.GetObsPos.Contains(snakeHead))
                {
                    bgm.Stop();
                    Console.Clear();
                    Console.SetCursorPosition(Console.WindowWidth/2 - 5, Console.WindowHeight/2);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Game over!");
                    Console.WriteLine("\n Press ENTER to quit");
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    while (keyInfo.Key != ConsoleKey.Enter)
                        keyInfo = Console.ReadKey();
                    return;
                }

                // check for collision with the food
                if (snakeNewHead.col == food.x && snakeNewHead.row == food.y)
                {
                    System.Media.SoundPlayer eat = new System.Media.SoundPlayer();
                    eat.SoundLocation = "../../sound/coin.wav";
                    eat.Play();
                    user.ScoreIncrement(1);
                    Console.SetCursorPosition(Console.WindowWidth - 10, Console.WindowHeight - 30);
                    Console.WriteLine("Score: " + user.getScore);
                    bgm.Play();

                    //spawn new food while increasing the length of the snake
                    food = new Food();
                    food.Generate_random_food();
                    snake.AddSnake();
                }

                // draw the snake
                Console.SetCursorPosition(snakeHead.col, snakeHead.row);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("*");

                // moving
                snake.GetPos.Enqueue(snakeNewHead);
                Console.SetCursorPosition(snakeNewHead.col, snakeNewHead.row);
                Console.ForegroundColor = ConsoleColor.Gray;

                // draw the snake head
                if (direct == right)
                {
                    Console.Write(">");
                }
                if (direct == left)
                {
                    Console.Write("<");
                }
                if (direct == up)
                {
                    Console.Write("^");
                }
                if (direct == down)
                {
                    Console.Write("v");
                }

                // moving...
                Position last = snake.GetPos.Dequeue();
                Console.SetCursorPosition(last.col, last.row);
                Console.Write(" ");

                //change score needed to clear the game based on the difficulty
                int goal;
                if (difficulty == "1")
                {
                    goal = 5;
                }
                else if (difficulty == "2")
                {
                    goal = 10;
                }
                else
                {
                    goal = 20;
                }

                // set winning condition score               
                if (user.getScore == goal)
                {
                    Console.Clear();
                    Console.SetCursorPosition(Console.WindowWidth/2, Console.WindowHeight/2);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Stage Clear!");
                    userlist.AddUser(user);

                    // Stop timing.
                    stopwatch.Stop();
                    double ClearTime = stopwatch.Elapsed.TotalSeconds;
                    user.getTime = ClearTime;

                    //Record and display users data
                    var x = userlist.getUsers;
                    userlist.sortRecord();
                    userlist.recordUser();
                    userlist.readRecord();

                    //Press enter to quit
                    Console.WriteLine("\n Press ENTER to quit");
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    while (keyInfo.Key != ConsoleKey.Enter)
                        keyInfo = Console.ReadKey();
                    System.Environment.Exit(0);
                    return;
                }

                // food timer
                if (Environment.TickCount - lastFoodTime >= foodDissapearTime)
                {
                    Console.SetCursorPosition(food.x, food.y);
                    Console.Write(" ");
                    food = new Food();
                    food.Generate_random_food();

                    lastFoodTime = Environment.TickCount;
                }

                sleepTime -= 0.01;
                Thread.Sleep((int)sleepTime);
            }
        }
    }
}
