Console.WriteLine("Choose The Width Of The Area (min 5):");
String areaWidthString = Console.ReadLine()!;
int n;   
while (true)
{
    if (Int32.TryParse(areaWidthString, out int areaWidth) && areaWidth > 5)
    {
        n = areaWidth;
        break;
    }
    else
    {
        Console.WriteLine("Area Width Has To Be An Available Integer That is Greater Than 5");
        areaWidthString = Console.ReadLine()!;
    }
}



Random rnd = new Random();
int[,] grid = new int[n,n];

int x = grid.GetLength(0) / 2;
int y = grid.GetLength(1) / 2;
grid[x, y] = 1;
List<(int, int)> snake = new List<(int, int)>();
snake.Add((x, y));
// (3, 3)
// (3, 2)
bool loseCondition = false;

SpawnFood(ref grid, x, y);
bool foodSpawned = true;
PrintGrid(grid);
String direction = "Right";

while (true)
{
    Move(ref grid, ref x, ref y, direction);
    
    if (loseCondition)
    {
        Console.WriteLine("You Lose!");
        break;
    }
    
    if (!foodSpawned)
    {
        SpawnFood(ref grid, x, y);
    }
    
    PrintGrid(grid);
    Thread.Sleep(400);
    if (Console.KeyAvailable)
    {
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        switch (keyInfo.Key)
        {
            case ConsoleKey.DownArrow:
            {
                if (!direction.Equals("Up"))
                {
                    direction = "Down";
                }
                break;
            }
            case ConsoleKey.UpArrow:
            {
                if (!direction.Equals("Down"))
                {
                    direction = "Up";
                }
                break;
            }
            case ConsoleKey.RightArrow:
            {
                if (!direction.Equals("Left"))
                {
                    direction = "Right";
                }
                break;
            }
            case ConsoleKey.LeftArrow:
            {
                if (!direction.Equals("Right"))
                {
                    direction = "Left";
                }
                break;
            }
            default:
            {
                Console.WriteLine("Ignored");
                break;
            }
        }
        Console.WriteLine(direction);
    }
}

void PrintGrid(int[,] grid)
{
    Console.Clear();
    for (int i = 0; i < grid.GetLength(0); i++)
    {
        for (int j = 0; j < grid.GetLength(1); j++)
        {
            Console.Write(grid[i, j] + " ");
        }
        Console.WriteLine();
    }
}

void Move(ref int[,] grid, ref int headX, ref int headY, string direction)
{
    switch (direction)
    {
        case "Up":
        {
            if (headX == 0 || snake.Contains((headX - 1, headY)))
            {
                loseCondition = true;
                break;
            }

            if (CheckFood(grid, headX - 1, headY))
            {
                EatFood();
                snake.Add((headX - 1 , headY));
            } else
            {
                grid[snake[0].Item1, snake[0].Item2] = 0;
                snake.RemoveAt(0);
                snake.Add((headX - 1, headY));
            }
            grid[--headX, headY] = 1;

            break;
        }
        case "Down":
        {
            if (headX == grid.GetLength(0) - 1 || snake.Contains((headX + 1, headY)))
            {
                loseCondition = true;
                break;
            }
            if (CheckFood(grid, headX + 1, headY))
            {
                EatFood();
                snake.Add((headX + 1 , headY));
            }
            else
            {
                grid[snake[0].Item1, snake[0].Item2] = 0;
                snake.RemoveAt(0);
                snake.Add((headX + 1, headY));
            }
            grid[++headX, headY] = 1;

            break;
        }
        case "Left":
        {
            if (headY == 0 || snake.Contains((headX, headY - 1)))
            {
                loseCondition = true;
                break;
            }

            if (CheckFood(grid, headX, headY - 1))
            {
                EatFood();
                snake.Add((headX, headY - 1));
            }
            else
            {
                grid[snake[0].Item1, snake[0].Item2] = 0;
                snake.RemoveAt(0);
                snake.Add((headX, headY - 1));
            }
            grid[headX, --headY] = 1;
            break;
        }
        case "Right":
        {
            if (headY == grid.GetLength(1) - 1 || snake.Contains((headX, headY + 1)))
            {
                loseCondition = true;
                break;
            }
            if (CheckFood(grid, headX, headY + 1))
            {
                EatFood();
                snake.Add((headX, headY + 1));

            } else
            {
                grid[snake[0].Item1, snake[0].Item2] = 0;
                snake.Add((headX, headY + 1));
                snake.RemoveAt(0);
            }
            grid[headX, ++headY] = 1;
            break;
        }
    }
}

void SpawnFood(ref int[,] grid, int headX, int headY)
{
    int foodX = rnd.Next(0, grid.GetLength(0));
    int foodY = rnd.Next(0, grid.GetLength(0));
    while (snake.Contains((foodX, foodY)))
    {
        foodX = rnd.Next(0, grid.GetLength(0));
        foodY = rnd.Next(0, grid.GetLength(0));
    }

    grid[foodX, foodY] = 2;
    foodSpawned = true;
}

bool CheckFood(int[,] grid, int x, int y)
{
    return grid[x, y] == 2;
}

void EatFood()
{
    foodSpawned = false;
}
