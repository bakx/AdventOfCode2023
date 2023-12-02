StreamReader reader = new StreamReader("input.txt");

string[] items = reader
    .ReadToEnd()
    .Split(Environment.NewLine)
    .ToArray();

reader.Close();


string[] part1SplitList = { "blue", "red", "green" };

int sumPart1 = 0;
int sumPart2 = 0;

for (int i = 0; i < items.Length; i++)
{
    Game game = new Game(items[i]);

    bool result = PlayGame(game, 12, 13, 14);
    if (result)
    {
        sumPart1 += game.ID;
    }
    
    sumPart2 += (game.RedCount * game.GreenCount * game.BlueCount);
}

Console.WriteLine($"Part 1: sum {sumPart1}");
Console.WriteLine($"Part 2: sum {sumPart2}");


bool PlayGame(Game game, int maxR, int maxG, int maxB)
{
    game.ID = int.Parse(game.Line.Split(": ")[0].Trim().Split(' ')[1]); // Splits twice, could re-use.. But why bother ..
    string gameOutcome = game.Line.Split(": ")[1].Trim();

    string[] outcome = gameOutcome.Split(';');
    bool isPossible = true;

    foreach (string s in outcome)
    {
        string[] gameResult = s.Split(',');

        foreach (string s1 in gameResult)
        {
            foreach (string s2 in part1SplitList)
            {
                if (s1.Contains(s2))
                {
                    int value = int.Parse(s1.Split(s2)[0]);

                    if (s2 == "red")
                    {
                        game.RedCount = Math.Max(value, game.RedCount);

                        if (value > maxR)
                        {
                            isPossible = false;
                        }
                    }

                    if (s2 == "green")
                    {
                        game.GreenCount = Math.Max(value, game.GreenCount);

                        if (value > maxG)
                        {
                            isPossible = false;
                        }
                    }

                    if (s2 == "blue")
                    {
                        game.BlueCount = Math.Max(value, game.BlueCount);

                        if (value > maxB)
                        {
                            isPossible = false;
                        }
                    }
                }
            }
        }
    }

    return isPossible;
}

class Game
{
    public Game(string line)
    {
        Line = line;
    }

    public string Line { get; set; }
    public int ID { get; set; }
    public int BlueCount { get; set; }
    public int RedCount { get; set; }
    public int GreenCount { get; set; }
}