using System;
using System.Diagnostics;
using System.Threading;

namespace ConsoleSnake
{
    class Head
    {
        static void Main(string[] args)
        {
            int BoxSize = 15;

            string Border = "#";
            string SnakeHead = "O";
            string SnakeBody = "*";
            string Food = "G";

            bool FoodIsAvaible = true;

            ConsoleKeyInfo key = new ConsoleKeyInfo((char)ConsoleKey.S, ConsoleKey.S, false, false, false); ;

            Vector2 foodPos = new Vector2(0,0);
            Vector2 Direction = new Vector2(0,0);
            Vector2 NewxPos = new Vector2(0,0);

            List<Vector2> snake = new List<Vector2>();
            List<Vector2> snakeClone = new List<Vector2>();

            snake.Add(new Vector2(1, 1));

            snakeClone = snake.ConvertAll(x => x);

            string[,] map = new string[BoxSize, BoxSize];

            Console.Clear();
            Console.Write("Prees Any key to Start");
            Console.ReadKey();
            Console.Clear();
            while (true)
            {
                Thread.Sleep(150);

                if (Console.KeyAvailable) { key = Console.ReadKey(); }

                Vector2 OldnewPos = NewxPos;

                if (key.Key == ConsoleKey.S) { Direction = new Vector2(1, 0); }
                if (key.Key == ConsoleKey.W) { Direction = new Vector2(-1, 0); }
                if (key.Key == ConsoleKey.D) { Direction = new Vector2(0, 1); }
                if (key.Key == ConsoleKey.A) { Direction = new Vector2(0, -1); }

                NewxPos = new Vector2(snake[0].x + Direction.x, snake[0].y + Direction.y);

                GenerateWalls(map, Border);

                string NextCell = map[NewxPos.x, NewxPos.y];
                bool body = false;

                for (int i = 0; i < snake.Count; i++){if (snake[i].x == NewxPos.x && snake[i].y == NewxPos.y) { body = true; break; }}

                if (!body && NextCell != Border) { snake[0] = NewxPos; }
                else
                {
                    Console.Clear();
                    Console.WriteLine("GameOver");
                    Console.WriteLine("Score:" + (snake.Count - 1));
                    break;
                }


                if (snake[0].x == foodPos.x && snake[0].y == foodPos.y)
                {
                    snake.Add(new Vector2(snake[snake.Count - 1].x, snake[snake.Count - 1].y));
                    FoodIsAvaible = true;
                }

                Add(map, foodPos.x, foodPos.y, Food);

                if (OldnewPos.x != NewxPos.x || OldnewPos.y != NewxPos.y)
                {
                    for (int i = 0; i < snake.Count; i++)
                    {
                        if (i < snake.Count - 1)
                        {
                            snake[i + 1] = snakeClone[i];
                        }
                    }
                }

                snakeClone = snake.ConvertAll(x => x);


                for (int i = 0; i < snake.Count; i++)
                {
                    if (i == 0) { Add(map, snake[i].x, snake[i].y, SnakeHead); }
                    else { Add(map, snake[i].x, snake[i].y, SnakeBody); }
                }

                if (FoodIsAvaible)
                {
                    FoodIsAvaible = false;
                    Random rand = new Random();

                    int x = 0;
                    int y = 0;

                    bool avaible = false;

                    while (!avaible)
                    {
                        x = rand.Next(2, BoxSize - 1);
                        y = rand.Next(2, BoxSize - 1);

                        avaible = true;

                        for (int i = 0; i < snake.Count; i++){if (snake[i].x == x && snake[i].y == y) {avaible = false;break;}}
                    }

                    foodPos = new Vector2(x, y);
                }

                Add(map, foodPos.x, foodPos.y, Food);

                Draw(map);
            }
        }

        public static void GenerateWalls(string[,] map, string c)
        {
            int index = (int)Math.Sqrt(map.Length);

            for (int x = 0; x < index; x++)
            {
                for (int y = 0; y < index; y++)
                {
                    if (x == 0 || x == index - 1) { map[x, y] = c; }
                    else { map[x, y] = null; }
                }
                map[x, 0] = c;
                map[x, index - 1] = c;
            }
        }

        public static void Add(string[,] map, int x, int y, string C)
        {
            map[x, y] = C;
        }
        public static void Remove(string[,] map, int x, int y)
        {
            map[x - 1, y - 1] = null;
        }

        public static void Draw(string[,] map)
        {
            Console.Clear();
            int index = (int)Math.Sqrt(map.Length);
            for (int x = 0; x < index; x++)
            {
                for (int y = 0; y < index; y++)
                {
                    if (map[x, y] != null) { Console.Write(map[x, y] + " "); }
                    else { Console.Write("  "); }
                }
                Console.WriteLine(string.Empty);
            }
        }
    }
}