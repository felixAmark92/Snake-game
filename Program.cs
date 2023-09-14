using Snake_game;

//Instantiate
var consoleDrawer = new ConsoleDrawer(GetBox(20, 20));
var playerSnake = new LinkedList<Point>();
var playerDirection = Direction.Right;
bool gotApple = true;

consoleDrawer.Draw();
Console.CursorVisible = false;

playerSnake.AddFirst(
    new Point(
    consoleDrawer.Box.GetLength(1) / 2, 
    consoleDrawer.Box.GetLength(0) / 2));

Console.ReadKey();
//Game loop
while (true)
{
    if (gotApple)
    {
        SetRandomApple(consoleDrawer.Box);
        gotApple = false;
    }
    else
    {
        consoleDrawer.UpdateScreenAt(playerSnake.Last(), ' ');
        playerSnake.RemoveLast();
    }

    if (Console.KeyAvailable)
    {
        SetDirection(ref playerDirection, Console.ReadKey().Key);
    }
    SetPosition(playerDirection, playerSnake.First());

    if (consoleDrawer.PointContains(playerSnake.First(), '#', '@'))
    {
        //Game over
        break;
    }
    else if (consoleDrawer.PointContains(playerSnake.First(), 'O'))
    {
        gotApple = true;
    }

    playerSnake.AddFirst(playerSnake.First().Copy());

    consoleDrawer.UpdateScreenAt(playerSnake.First(), '@', ConsoleColor.Green);
    Console.ResetColor();
    Thread.Sleep(100);
}

Console.Clear();
Console.WriteLine("Game over!");
Console.ReadLine();




//functions
void SetPosition(Direction playerDirection, Point playerPosition)
{
    switch (playerDirection)
    {
        case Direction.Left:
            playerPosition.X -= 1;
            break;
        case Direction.Right:
            playerPosition.X += 1;
            break;
        case Direction.Up:
            playerPosition.Y -= 1;
            break;
        case Direction.Down:
            playerPosition.Y += 1;
            break;
        default:
            break;
    }
}

void SetDirection(ref Direction playerDirection, ConsoleKey key)
{
    switch (key)
    {
        case ConsoleKey.DownArrow:
            playerDirection = playerDirection == Direction.Up ? Direction.Up : Direction.Down;
            break;
        case ConsoleKey.UpArrow:
            playerDirection = playerDirection == Direction.Down ? Direction.Down : Direction.Up;
            break;
        case ConsoleKey.LeftArrow:
            playerDirection = playerDirection == Direction.Right ? Direction.Right : Direction.Left;
            break;
        case ConsoleKey.RightArrow:
            playerDirection = playerDirection == Direction.Left ? Direction.Left : Direction.Right;
            break;
        default:
            break;
    }
}

void SetRandomApple(char[,] box)
{
    var rand = new Random();

    Point apple = new Point( rand.Next(1, box.GetLength(1) - 1), rand.Next(1, box.GetLength(0) - 1));

    if (box[apple.Y, apple.X] == '@')
    {
        SetRandomApple(box);
        return;
    }

    consoleDrawer.UpdateScreenAt(apple, 'O', ConsoleColor.Red);
}
char[,] GetBox(int width, int height)
{
    char[,] charBox = new char[height, width];


    int j;
    for (int i = 0; i < height; i++)
    {
        j = 0;
        charBox[i, j] = '#';
        for (j = 1; j < width - 1; j++)
        {
            if (i == 0 || i == height - 1)
            {
                charBox[i, j] = '#';
            }
            else
            {
                charBox[i, j] = ' ';
            }

        }
        charBox[i, j] = '#';
    }

    return charBox;
}

enum Direction
{
    Left,
    Right,
    Up,
    Down
}